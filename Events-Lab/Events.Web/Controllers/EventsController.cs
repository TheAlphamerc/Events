using Events.Data;
using Events.Web.Extensions;
using Events.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Web.Controllers
{
    [Authorize]
    public class EventsController : BaseController
    {
        // GET: Events
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventInputModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                var e = new Event()
                {
                    AuthorId = this.User.Identity.GetUserId(),
                    Title = model.Title,
                    StartDateTIme = model.StartDateTIme,
                    Duration = model.Duration,
                    Location = model.Location,

                };
                this.db.Events.Add(e);
                db.SaveChanges();
                this.AddNotification("Events created", NotificationType.INFO);
                return this.RedirectToAction("My");
            }
            return this.View(model);
        }

        public ActionResult My()
        {
            string currentUserId = this.User.Identity.GetUserId();
            var events = this.db.Events
                             .Where(e => e.AuthorId == currentUserId)
                             .OrderBy(e => e.StartDateTIme)
                             .Select(EventViewModel.viewModel);
            var upcomingEvents = events.Where(e => e.StartDateTIme > DateTime.Now);
            var passedEvents = events.Where(e => e.StartDateTIme <= DateTime.Now);

            return View(new UpcomingPassedEventsViewModel()
            { 
                UpcomingEvents = upcomingEvents,
                PassedEvents = passedEvents
            });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                this.AddNotification("Cannot edit event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            var model = EventInputModel.CreateFromEvent(eventToEdit);
            
            return this.View(model);
        }

        private Event LoadEvent(int id)
        {
            var currentId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var eventToEdit = this.db.Events
                                  .Where(e => e.Id == id)
                                   .FirstOrDefault(e => e.AuthorId == currentId || isAdmin);
            return eventToEdit;
        }

        [HttpPost]
        public ActionResult Edit(int id, EventInputModel model)
        {
            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                this.AddNotification("Cannot edit event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            //  var model = EventInputModel.CreateFromEvent(eventToEdit);
            if (model != null && this.ModelState.IsValid)
            {
                eventToEdit.Title = model.Title;
                eventToEdit.StartDateTIme = model.StartDateTIme;
                eventToEdit.Duration = model.Duration;
                eventToEdit.Description = model.Description;
                eventToEdit.Location = model.Location;
                eventToEdit.Ispublic = model.Ispublic;

                this.db.SaveChanges();
                this.AddNotification("Event edited.", NotificationType.INFO);
                return this.RedirectToAction("My");
            }
            return this.View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            var eventToEdit = this.LoadEvent(id);
            if (eventToEdit == null)
            {
                this.AddNotification("Cannot edit event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            var model = EventInputModel.CreateFromEvent(eventToEdit);
            ViewBag.id = id;
            return this.View(model);
        }
        
        public ActionResult Delete_post(int id)
        {
            var eventTodelete = this.LoadEvent(id);
            if (eventTodelete == null)
            {
                this.AddNotification("Cannot edit event #" + id, NotificationType.ERROR);
                return this.RedirectToAction("My");
            }
            var model = EventInputModel.CreateFromEvent(eventTodelete);
            if (eventTodelete != null)
            {
                this.db.Events.Remove(eventTodelete);
                this.db.SaveChanges();
                this.AddNotification("Event Deleted.", NotificationType.SUCCESS);
                return this.RedirectToAction("My");
            }
            return this.RedirectToAction("My");
        }




















    }
       
   
}
