using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Models
{
    public partial class Meeting
    {
        private bool permissionChecked = false;
        public bool isOwner {get;private set;}

        public bool IsAccepter { get; private set; }
        
        public MeetingAccept CurrentUserAccept { get; set; }

        public AspNetUser Owner { get { return AspNetUser1; } }
        public AspNetUser Confirmer { get { return AspNetUser; } }

        public void checkPermissions(string userid)
        {
            permissionChecked = true;
            isOwner = creator == userid;
            if(isOwner)
                return;
            CurrentUserAccept = Accepts.FirstOrDefault(x => x.acceptorId == userid);
            IsAccepter = CurrentUserAccept != null;
        }
        public IEnumerable<MeetingAccept> Accepts
        {
            get
            {
                return MeetingAccepts.AsEnumerable();
            }
        }




        public bool? isAccepter()
        {
            if (permissionChecked)
            {
                return IsAccepter;
            }
            return null;
        }
    }
}