using ASP.NET_MVC5.Api.Models;
using ASP.NET_MVC5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ASP.NET_MVC5.Api.Controllers
{
    public class SuperApiController : ApiController
    {
        public SuperApiController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public SuperApiController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; set; }
        public string GetUserId()
        {
            return User.Identity.GetUserId();
        }
        public Response Response(object value)
        {
            return new Response() {  response = value };
        }
    }
}
