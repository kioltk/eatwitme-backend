using ASP.NET_MVC5.Api.Errors;
using ASP.NET_MVC5.Api.Models;
using ASP.NET_MVC5.Mappers;
using ASP.NET_MVC5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ASP.NET_MVC5.Api.Controllers
{
    [ApiAuthorize]
    public class AccountController : SuperApiController
    {


        private IAuthenticationManager AuthenticationManager
        {
            get
            {

                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Response> Login(string login, string password)
        {

            var user = await UserManager.FindAsync(login, password);
            if (user != null)
            {
                await SignInAsync(user, true);

                return Response(CommonMapper.Map(user));
            }
            return new BadRequestError("Bad credits");
        }

        [HttpGet]
        public string Index()
        {
            return "Try me";
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<Response> Register(string login, string password)
        {
            var user = new ApplicationUser() { UserName = login };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: false);
                return Response(CommonMapper.Map(user));
            }
            return new BadRequestError("Bad credits");
        }



    }
}
