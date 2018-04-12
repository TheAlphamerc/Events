using Events.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Events.Web.Controllers
{
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        protected ApplicationDbContext  db = new ApplicationDbContext();
        public bool IsAdmin()
        {
            var currentUserId = this.User.Identity.GetUserId();
           var isAdmin = (currentUserId != null && this.User.IsInRole("Administrator"));
            return isAdmin;
        }

        //private bool IsInRole(string v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}