using ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC5.Repositories
{
    public class UserRepository : Repository
    {

        public IQueryable<AspNetUser> AspNetUsers
        {
            get { return  context.AspNetUsers; }
        }

        public bool Create(AspNetUser instance)
        {
            throw new NotImplementedException();
        }

        public bool Update(object instance)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}