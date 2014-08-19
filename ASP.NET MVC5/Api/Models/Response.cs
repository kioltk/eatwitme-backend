using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Models
{
    public class Response
    {
        protected int _code = 1;

        public int code { get { return _code; } set { _code = value; } }
        public object response { get; set; }

    }
}