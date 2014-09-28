using ASP.NET_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_MVC5.Repositories
{
    public class Repository
    {
        protected static ConnectorDataContext context = new ConnectorDataContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected bool SubmitChanges()
        {
            try
            {

                context.SubmitChanges();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public void Dispose()
        {
            
            context.Dispose();
        }
    }
}
