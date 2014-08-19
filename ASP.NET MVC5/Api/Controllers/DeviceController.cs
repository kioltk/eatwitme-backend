using ASP.NET_MVC5.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ASP.NET_MVC5.Api.Controllers
{
    public class DeviceController : SuperApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [ActionName("testnotify")]
        public Response Notify(string message,string registrationid)
        {
            try
            {
                var result = Service.Notify.SendCommandToPhone(message, registrationid);
                return Response(new { result = true, message = result });
            }
            catch (Exception exp)
            {
                return Response(new { result = false, message = exp.Message });
            }
        }
    }
}
