using Events.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Events.Web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDateTIme { get; set; }

        public TimeSpan? Duration { get; set; }

     //   public string Description { get; set; }

        public string Author { get; set; }

        public string Location { get; set; }

        //      public bool IsPublic { get; set; }

        public static Expression<Func<Event, EventViewModel>> viewModel
        {
            get
            {
                return e => new EventViewModel()
                {

                    Id = e.Id,
                    Title = e.Title,
                    StartDateTIme = e.StartDateTIme,
                    Duration = e.Duration,
                    Location = e.Location,
                    Author = e.Author.Fullname
                };

            }

        }
    }
}