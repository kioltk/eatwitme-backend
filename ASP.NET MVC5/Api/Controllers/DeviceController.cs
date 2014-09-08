using ASP.NET_MVC5.Api.Errors;
using ASP.NET_MVC5.Api.Models;
using ASP.NET_MVC5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASP.NET_MVC5.Api.Controllers
{
    [ApiAuthorize]
    public class DeviceController : SuperApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [ActionName("testnotify")]
        public Response Notify(string message, string registrationid)
        {
            try
            {
                var result = Service.Notify.SendCommandToPhone(message,"notify", registrationid);
                return Response(new { result = true, message = result });
            }
            catch (Exception exp)
            {
                return Response(new { result = false, message = exp.Message });
            }
        }


        [HttpPost]
        public Response Register(string registrationid)
        {
            var userRep = new UserRepository();
            var currentUser = userRep.AspNetUsers.FirstOrDefault(x => x.Id == GetUserId());
            if (currentUser == null)
            {
                return new InternalError("No such user");
            }
            currentUser.registrationId = registrationid;
            if (userRep.Update(currentUser))
                return Response(true);
            return new InternalError();
        }
        [HttpPost]
        public Response Unregister()
        {
            {
                var userRep = new UserRepository();
                var currentUser = userRep.AspNetUsers.FirstOrDefault(x => x.Id == GetUserId());
                if (currentUser == null)
                {
                    return new InternalError("No such user");
                }
                currentUser.registrationId = null;
                if (userRep.Update(currentUser))
                    return Response(true);
                return new InternalError();
            }
        }
        [HttpPost]
        public Response ResetLocation()
        {
            var userRep = new UserRepository();
            var currentUser = userRep.AspNetUsers.FirstOrDefault(x => x.Id == GetUserId());
            if (currentUser == null)
            {
                return new InternalError("No such user");
            }
            currentUser.longitude = null;
            currentUser.latitude = null;
            if (userRep.Update(currentUser))
                return Response(true);
            return new InternalError();

        }
        [HttpPost]
        public Response CheckLocation(string longitude, string latitude)
        {
            var userRep = new UserRepository();
            var currentUser = userRep.AspNetUsers.FirstOrDefault(x => x.Id == GetUserId());
            if (currentUser == null)
            {
                return new InternalError("No such user");
            }
            currentUser.longitude = longitude;
            currentUser.latitude = latitude;
            if (userRep.Update(currentUser))
                return Response(true);
            return new InternalError();
        }

    }
}
