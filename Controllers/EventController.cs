using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CardinalHub.Models;
using CardinalHub.DAL;

namespace CardinalHub.Controllers
{
    public class EventController : Controller
    {
        private CardHubContext db = new CardHubContext();

        // GET: Event
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventPageViewClass EventPage;
            CardHubUser user;

            user = CardHubUserMaster.GetCurrentUser(db);
            CardEvent evnt = GetEventByID(id);

            if (evnt == null)
            {
                return HttpNotFound();
            }

            try
            {
                evnt.InvitedCount = db.SocialInteractions.Where(x => x.CardEventID == evnt.CardEventID && x.EventStatus == Models.EventStatus.Invited).Count();
                evnt.GoingCount = db.SocialInteractions.Where(x => x.CardEventID == evnt.CardEventID && x.EventStatus == Models.EventStatus.Going).Count();
                evnt.MaybeCount = db.SocialInteractions.Where(x => x.CardEventID == evnt.CardEventID && x.EventStatus == Models.EventStatus.Maybe).Count();

                EventPage = new EventPageViewClass { CardEvent = evnt, CurrentUserId = user.CardHubUserID };
                EventPage.EventDate = EventPage.CardEvent.EventStartDateTime;
                EventPage.EventStartTime = EventPage.CardEvent.EventStartDateTime;
                EventPage.EventEndTime = EventPage.CardEvent.EventStartEndTime;

                return View(EventPage);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: EditEvent
        [HttpPost]
        public bool EditEvent(EventPageViewClass eventPage, Location loc, MapIcon icon)
        {
            if (ModelState.IsValid)
            {
                CardEvent ce = db.CardEvents.Find(eventPage.CardEvent.CardEventID);

                ce.Location.Latitude = loc.Latitude;
                ce.Location.Longitude = loc.Longitude;
                ce.Location.MapIcon = icon;

                if (eventPage.EventStartTime != null)
                {
                    ce.EventStartDateTime = eventPage.EventDate.Value.Add(eventPage.EventStartTime.Value.TimeOfDay);
                }

                if (eventPage.EventEndTime != null)
                {
                    ce.EventStartEndTime = eventPage.EventDate.Value.Add(eventPage.EventEndTime.Value.TimeOfDay);
                }

                if (eventPage.EventDate != null)
                {
                   // ce.EventStartDateTime = eventPage.EventDate.Value.Add( (DateTime?)eventPage.EventDate.Value.Date);
                }

                if (eventPage.CardEvent.WebSiteURL != null)
                {
                    ce.WebSiteURL = eventPage.CardEvent.WebSiteURL;
                }

                if (eventPage.CardEvent.PhoneNo != null)
                {
                    ce.EventName = eventPage.CardEvent.PhoneNo;
                }

                if (eventPage.CardEvent.EventName != null)
                {
                    ce.EventName = eventPage.CardEvent.EventName;
                }

                if (eventPage.CardEvent.Description != null)
                {
                    ce.Description = eventPage.CardEvent.Description;
                }

                db.CardEvents.Attach(ce);
                db.Entry(ce).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            return false;
        }
        //public void Edit

        public CardEvent GetEventByID(int? evntID)
        {
            if (evntID != null)
            {

                CardEvent evnt = db.CardEvents.Find(evntID);

                return evnt;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public bool EventStatus(SocialInteraction social)
        {
            if (ModelState.IsValid)
            {
                CardHubUser user;
                user = CardHubUserMaster.GetCurrentUser(db);
                social.CardHubUserID = user.CardHubUserID;
                SocialInteraction socialInteraction = db.SocialInteractions.FirstOrDefault(m => m.CardHubUserID == user.CardHubUserID && m.CardEventID == social.CardEventID);

                if (socialInteraction != null)
                {
                    socialInteraction.EventStatus = social.EventStatus;
                    db.SocialInteractions.Attach(socialInteraction);
                    var entry = db.Entry(socialInteraction);
                    entry.Property(e => e.EventStatus).IsModified = true;
                } else
                {
                    db.SocialInteractions.Add(social);
                }

                try
                {
                    db.SaveChanges();
                    return true;
                } catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}