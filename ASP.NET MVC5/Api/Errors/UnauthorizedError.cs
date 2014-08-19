using ASP.NET_MVC5.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Errors
{
    public class UnauthorizedError : Response
    {

        public UnauthorizedError()
        {
            _code = 401;
            response = "Bad token";
        }

        public UnauthorizedError(string response):this()
        {
            this.response = response;
        }
    }
}