﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Events.Web.Models
{
    public class EventInputModel
    {
      //  public int Id { get; set; }

        [Required(ErrorMessage ="Event Title is required.")]
        [StringLength(200,ErrorMessage ="The {0} must be between {2} and {1} character long.", MinimumLength = 1)]
        [Display(Name = "Title *")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name ="Data and Time")]
        public DateTime StartDateTIme { get; set; }

        public TimeSpan? Duration { get; set; }

      //  public string AuthorId { get; set; }


        public string Description { get; set; }

        [MaxLength(200)]
        public string Location { get; set; }

        [Display(Name = "Is Public?")]
        public bool Ispublic { get; set; }

    }
}