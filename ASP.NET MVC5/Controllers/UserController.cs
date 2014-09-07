using ASP.NET_MVC5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC5.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            UserRepository rep = new UserRepository();
            var users = rep.AspNetUsers;
            return View(users);
        }
        public ActionResult Details(string id)
        {
            UserRepository rep = new UserRepository();
            var users = rep.AspNetUsers;
            
            if (id == null)
            {
                return View(users.First());
            }
            return View(users.First(x=>x.Id==id));
        }
    }
}