using ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Repositories
{
    public class MeetingRepository : Repository
    {

        public IQueryable<Meeting> Meetings
        {
            get { return  context.Meetings; }
        }

        public int Create(Meeting instance)
        {
            if (instance.Id == 0)
            {
                context.Meetings.InsertOnSubmit(instance);
                SubmitChanges();
                return instance.Id;
            }
            return -1;
        }

        public void Update(object instance)
        {
            SubmitChanges();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MeetingAccept> MeetingAccepts
        {
            get
            {
                return context.MeetingAccepts;
            }
        }
        
        public int Create(MeetingAccept accept){

            if (accept.Id == 0)
            {
                try
                {

                    context.MeetingAccepts.InsertOnSubmit(accept);
                    SubmitChanges();


                    return accept.Id;
                }
                catch (Exception)
                {

                }
            }
            return -1;
        }
    }
}