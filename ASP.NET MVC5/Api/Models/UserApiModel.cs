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
        public string photo
        {
            get
            {
                return "http://cs616230.vk.me/v616230567/151af/cP4wx9MZT-w.jpg";
            }
        }
        public string latitude { get; set; }
        public string longtitude { get; set; }
    }
}