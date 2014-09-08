using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Api.Models
{
    public class MeetingAcceptApiModel
    {
        public string id { get; set; }
        public string meetingId { get; set; }
        public UserApiModel acceptor { get; set; }
        public string message { get; set; }
        public int time { get; set; }
        public bool confirmed { get; set; }
    }
}