using CardinalHub.DAL;
using CardinalHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardinalHub.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private CardHubContext db = new CardHubContext();

        public ActionResult Index()
        {
            CardHubUser user;
            EventViewClass eventView;
            DateTime date;

            date = DateTime.Now.AddHours(-4);
            user = CardHubUserMaster.GetCurrentUser(db);

            try
            {
                var events = db.CardEvents.Where(x => x.EventStartDateTime >= date).OrderBy(s => s.EventStartDateTime).Take(15);
                eventView = new EventViewClass { CardEvents = events.ToArray(), CardHubUser = user };
            }
            catch (Exception e)
            {
                e.ToString();
                eventView = new EventViewClass { CardEvents = new List<CardEvent> { }, CardHubUser = user };
            }

            return View(eventView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CardEvents(float neLat, float neLong, float swLat, float swLong, int? srchType, string srchTerm)
        {
            var events = new List<object>();
            CardHubUser user;
            EventType type;
            string city;

            if (srchType != null)
            {
                type = (EventType)srchType;
            }
            else
            {
                type = EventType.Other;
            }

            user = CardHubUserMaster.GetCurrentUser(db);

            if (user.Location == null)
            {
                city = "Louisville";
            }
            else
            {
                city = user.Location.City;
            }
            

            var cardevents = from e in db.CardEvents
                              where e.Location.City == city
                              && e.Location.Latitude <= neLat
                              && e.Location.Latitude >= swLat
                              && e.Location.Longitude <= neLong
                              && e.Location.Longitude >= swLong
                              && (srchType == null || e.EventType == type)
                              && (srchTerm == null || e.EventName.Contains(srchTerm))
                              select e;

            var _goingcounts = db.SocialInteractions.Where(m => m.EventStatus == EventStatus.Going)
                .GroupBy(m => m.CardEventID).ToDictionary(d => d.Key, d => d.Count());

            var _maybecounts = db.SocialInteractions.Where(m => m.EventStatus == EventStatus.Maybe)
                .GroupBy(m => m.CardEventID).ToDictionary(d => d.Key, d => d.Count());

            var _invitedcounts = db.SocialInteractions.Where(m => m.EventStatus == EventStatus.Invited)
                .GroupBy(m => m.CardEventID).ToDictionary(d => d.Key, d => d.Count());

            foreach (CardEvent e in cardevents.ToArray())
            {
                   e.GoingCount = (_goingcounts.ContainsKey(e.CardEventID)) ? _goingcounts[e.CardEventID] : 0;
                   e.MaybeCount = (_maybecounts.ContainsKey(e.CardEventID)) ? _maybecounts[e.CardEventID] : 0;
                   e.InvitedCount = (_invitedcounts.ContainsKey(e.CardEventID)) ? _invitedcounts[e.CardEventID] : 0;

                events.Add(new
                {
                    id = e.CardEventID,
                    IWString = FormatInfoWindow(e),
                    PlaceName = e.EventName,
                    vicinity = (e.Location.Address == null ? "" : e.Location.Address),
                    phone = (e.PhoneNo == null ? "" : e.PhoneNo),
                    GeoLong = e.Location.Longitude,
                    GeoLat = e.Location.Latitude,
                    IconName = FormatIconName(e),
                    markertype = e.Location.MapIcon.IconMarker.ToString(),
                    markercolor = e.Location.MapIcon.fillColor,
                    rtype = EnumNinja.DisplayName(e.EventType),
                    goingcount = FormatCount(e.GoingCount),
                    maybecount = FormatCount(e.MaybeCount),
                    invitedcount = FormatCount(e.InvitedCount),
                    goingflag = FindIfGoing(e,user.CardHubUserID),
                    website = (e.WebSiteURL == null ? "" : e.WebSiteURL)
                });
            }

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        private string FormatInfoWindow(CardEvent e)
        {
            return e.Description == null ? "" :e.Description;
        }

        private string FormatIconName(CardEvent e)
        {
            return e.Location.MapIcon.IconType.ToString().Replace('_','-') ;
        }

        private bool FindIfGoing(CardEvent e, int userid)
        {
            bool returnVal;
            returnVal = false;

            foreach (SocialInteraction s in e.SocialInteractions)
            {
                if (s.EventStatus == EventStatus.Going && s.CardHubUserID == userid)
                {
                    returnVal = true;
                }
            }
            return returnVal;
        }

        [HttpPost]
        public int? CreateEvent(CardEvent cardEvent, Location loc, MapIcon icon)
        {
            if (ModelState.IsValid)
            {
                CardHubUser user;

                user = CardHubUserMaster.GetCurrentUser(db);
                loc.City = "Louisville";
                loc.State = "KY";
                loc.MapIcon = icon;
                cardEvent.Location = loc;
                cardEvent.CardHubUserID = user.CardHubUserID;
                db.MapIcons.Add(icon);
                db.Locations.Add(loc);
                db.CardEvents.Add(cardEvent);

                try
                {
                    db.SaveChanges();
                    return cardEvent.CardEventID;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        private int? FormatCount(int? count)
        {
            return (count == null ? 0 : count);
        }

    }
}