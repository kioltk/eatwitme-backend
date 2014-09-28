using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Models
{
    public class UserApiModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string photo { get; set; }
        // public string latitude { get; set; }
        // public string longitude { get; set; }

        public int meetingsCount { get; set; }
    }
}