using System;
using System.Collections.Generic;
using System.Text;

namespace Events.Data
{
   public class Comment
    {
        public Comment()
        {
            this.Date = DateTime.Now;
            this.EventId = 0;

            //this.EventId = Event.Id;
        }

        public int Id { get; set; }

        public string  Text  { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
