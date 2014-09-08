using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Models
{
    public class MeetingApiModel
    {
        public string id { get; set; }
        public string creator { get; set; } 
        public string description { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public int time { get; set; }
        public MeetingAcceptApiModel accept { get; set; }
        public int acceptsCount { get; set; }
        public UserApiModel owner { get; set; }
        public UserApiModel confirmer { get; set; }
    }
}