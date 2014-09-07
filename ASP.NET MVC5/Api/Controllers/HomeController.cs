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
    public class HomeController : SuperApiController
    {
        [HttpGet]
        public string Index()
        {
            return "hello";
        }


        [AllowAnonymous]
        [HttpGet]
        [ActionName("test")]
        public Response test()
        {
            try
            {
                var accept = new MeetingRepository().MeetingAccepts.First();
                var result = "hello";
                    Service.Notify.NewAccept(accept);
                return Response(new { result = true, message = result });
            }
            catch (Exception exp)
            {
                return Response(new { result = false, message = exp.Message });
            }
        }
    }
}
