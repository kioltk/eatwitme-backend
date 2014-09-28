    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Models
{
    public partial class AspNetUser
    {
        public string Role { get {
            if(! AspNetUserRoles.Any())
                return null;
            return AspNetUserRoles.First().AspNetRole.Name; } }



        public int getMeetingsCount()
        {
            return Meetings1.Where(x=>x.confirmer!=null).Count() + Meetings.Count;
            
        }
    }
}