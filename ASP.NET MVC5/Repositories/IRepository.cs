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
        protected ConnectorDataContext context = new ConnectorDataContext(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void SubmitChanges()
        {
            context.SubmitChanges();
        }
    }
}
