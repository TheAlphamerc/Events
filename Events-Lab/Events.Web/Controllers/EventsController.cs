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
    }
}
