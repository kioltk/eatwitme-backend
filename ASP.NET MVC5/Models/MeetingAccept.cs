using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Models
{
    public partial class MeetingAccept
    {
        public AspNetUser Acceptor
        {
            get
            {
                return AspNetUser;
            }
        }
    }
}