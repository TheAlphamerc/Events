using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Events.Data
{
  public  class Event
  {
        public Event()
        {
            this.Ispublic = true;
            this.StartDateTIme = DateTime.Now;
            this.Comments = new HashSet<Comment>();

        }
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public DateTime StartDateTIme { get; set; }

        public TimeSpan? Duration { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string Description { get; set;}

        [MaxLength(200)]
        public string  Location { get; set; }

        public bool Ispublic{ get; set; }

        public ICollection<Comment> Comments { get; set; }
  }
}
