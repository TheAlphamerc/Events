using Events.Data;
using Events.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Events.Web.Controllers
{
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
                    // Description = model.Description,
                    Location = model.Location,
                    //  Ispublic = model.IsPublic

                };
                this.db.Events.Add(e);
                db.SaveChanges();
                return this.RedirectToAction("My");
            }
            return this.View(model);
        }

        public ActionResult My()
        {
            return View();
        }
    }
}
