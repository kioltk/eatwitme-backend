using ASP.NET_MVC5.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Errors
{
    public class InternalError : Response
    {

        public InternalError()
        {
            _code = 500;
            response = "You did it. Nice.";
        }

        public InternalError(string response):this()
        {
            this.response = response;
        }
    }
}