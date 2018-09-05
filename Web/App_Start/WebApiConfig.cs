using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "imageApi1",
                routeTemplate: "api/{controller}/{id}"
            );

            config.Routes.MapHttpRoute(
                name: "imageApi2",
                routeTemplate: "api/{controller}/{id}/{islarge}"
            );
        }
    }
}
