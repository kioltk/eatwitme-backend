using ASP.NET_MVC5.Api.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace ASP.NET_MVC5.App_Start
{
    class ApiExceptionAttribute : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            
            if (context.Exception is SystemException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.OK, new InternalError());
                return;
            }
            context.Response = context.Request.CreateResponse(HttpStatusCode.OK, new InternalError());
            
        }
    }
}
