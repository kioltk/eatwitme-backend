using ASP.NET_MVC5.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using ASP.NET_MVC5.App_Start;
using Newtonsoft.Json;

namespace ASP.NET_MVC5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // config.Filters.Add(new ApiAuthorizeAttribute());
            // Web API routes

            config.Filters.Add(new ApiExceptionAttribute());
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {  id = RouteParameter.Optional, controller = "Home", action = "Index" }
            );
        }
    }
}
