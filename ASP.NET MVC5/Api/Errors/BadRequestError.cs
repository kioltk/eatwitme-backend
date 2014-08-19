using ASP.NET_MVC5.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Errors
{
    public class BadRequestError : Response
    {

        public BadRequestError()
        {
            code = 400;
            response = "Bad request.";
        }

        public BadRequestError(string response):this()
        {
            this.response = response;
        }
    }
}