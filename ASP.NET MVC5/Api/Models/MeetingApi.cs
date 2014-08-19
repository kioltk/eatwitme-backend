using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Models
{
    public class MeetingApiModel
    {
        public string Id { get; set; }
        public string creator { get; set; } 
        public string description { get; set; }
        public string longtitude { get; set; }
        public string latitude { get; set; }
        public int time { get; set; }

    }
}