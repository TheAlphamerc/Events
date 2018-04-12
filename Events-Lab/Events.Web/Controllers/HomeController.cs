﻿using Events.Data;
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
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var events = this.db.Events
                             .OrderBy(e => e.StartDateTIme)
                             .Where(e => e.Ispublic)
                             .Select(EventViewModel.viewModel);
                              
            var upcomingEvents = events.Where(e => e.StartDateTIme > DateTime.Now);
            var passedEvents = events.Where(e => e.StartDateTIme <= DateTime.Now);
            return View( new UpcomingPassedEventsViewModel()
                       {
                            UpcomingEvents  = upcomingEvents,
                            PassedEvents = passedEvents
                       });
        }
        public ActionResult EventDetailsById(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var isAdmin = this.IsAdmin();
            var eventDetails = this.db.Events
                                    .Where(e => e.Id == id)
                                    .Where(e => e.Ispublic || isAdmin || (e.AuthorId != null && e.AuthorId == currentUserId))
                                    .Select(EventDetailsViewModel.ViewModel)
                                    .FirstOrDefault();
            var isOwner = (eventDetails != null && eventDetails.AuthorId != null && eventDetails.AuthorId == currentUserId);
            this.ViewBag.CanEdit = isOwner || isAdmin;
            return this.PartialView("_EventDetails",eventDetails);
        }

        public JsonResult NewComment(Comment C)
        {
            ApplicationDbContext dba = new ApplicationDbContext();

           bool status =false;
            if (C != null)
            { Comment comment = new Comment()
            {
                Author = C.Author,
                Text = C.Text,
                Date = DateTime.Now,
                EventId = 1
                
                    
                    
                };
                dba.SaveChanges();
                //Comments.SaveChanges();
                this.db.SaveChanges();
                this.AddNotification("Comment added.", NotificationType.SUCCESS);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }



    }
}