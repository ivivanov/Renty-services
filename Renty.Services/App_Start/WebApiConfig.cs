using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Renty.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Routes.MapHttpRoute(
                name: "ItemApi",
                routeTemplate: "api/item/{action}/{id}",
                defaults: new
                {
                    controller = "Item",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "UserApi",
                routeTemplate: "api/user/{action}",
                defaults: new
                {
                    controller = "User"
                }
            );

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
