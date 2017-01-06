using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CardinalHub.DAL;
using CardinalHub.Models;
using Microsoft.AspNet.Identity;

namespace CardinalHub.Controllers
{
    public class CampusMapController : Controller
    {
        private CardHubContext db = new CardHubContext();

        [Authorize]
        // GET: CampusMap
        public ActionResult Index()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;

            CardHubUser user = CardHubUserMaster.GetCurrentUser(db);

            if (user == null)
            {
               return RedirectToAction("Login", "Account");
            }

            EventViewClass eventclass = new EventViewClass { };

            eventclass.CardHubUser = user;

            return View(eventclass);
        }


        // GET: CampusMap/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            CardHubUser user = CardHubUserMaster.GetCurrentUser(db);

            return View(user);
        }

        [Authorize]
        // GET: CampusMap/Create
        public ActionResult Create()
        {
            CardHubUser user = CardHubUserMaster.GetCurrentUser(db);
            CardEvent cardEvent = new CardEvent();

            return View(cardEvent);
        }

        // POST: CampusMap/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CardHubUserID,UserName,Avatar,LastName,FirstName,EnrollmentDate")] CardHubUser cardHubUser)
        {
            if (ModelState.IsValid)
            {
                db.CardHubUsers.Add(cardHubUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cardHubUser);
        }

        // GET: CampusMap/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardHubUser cardHubUser = db.CardHubUsers.Find(id);
            if (cardHubUser == null)
            {
                return HttpNotFound();
            }
            return View(cardHubUser);
        }

        // POST: CampusMap/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CardHubUserID,UserName,Avatar,LastName,FirstName,EnrollmentDate")] CardHubUser cardHubUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cardHubUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cardHubUser);
        }

        // GET: CampusMap/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardHubUser cardHubUser = db.CardHubUsers.Find(id);
            if (cardHubUser == null)
            {
                return HttpNotFound();
            }
            return View(cardHubUser);
        }

        // POST: CampusMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CardHubUser cardHubUser = db.CardHubUsers.Find(id);
            db.CardHubUsers.Remove(cardHubUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetCurrentDate()
        {
            return DateTime.Now.ToString();
        }

        public string AddNewEvent(Location loc, MapIcon icon, CardEvent ce)
        {
            if (ModelState.IsValid)
            {
                CardHubUser user; 
                SocialInteraction si;

                user = CardHubUserMaster.GetCurrentUser(db);

                if(icon.fillColor.ToUpper().Trim() == "#FFFFFF")
                {
                    icon.fillColor = "#FF0000";
                }
                si = new SocialInteraction { };
                si.CardEventID = ce.CardEventID;
                si.CardHubUserID = user.CardHubUserID;
                si.EventStatus = EventStatus.Maybe;
                //user.SocialInteractions.Add(si);
                ce.Location = loc;
                loc.MapIcon = icon;
                loc.City = "Louisville";
                ce.CardHubUserID = user.CardHubUserID;
                db.Locations.Add(loc);
                db.SocialInteractions.Add(si);
                db.CardEvents.Add(ce);
                db.SaveChanges();

                return "Added New Event!";
            }
            return null;
        }
    }
}
