﻿using OnlineShoppingAPI.Controllers.Attribute;
using OnlineShoppingAPI.Controllers.Filter;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OnlineShoppingAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services
            config.Filters.Add(new ExceptionLogFilter());
            config.Filters.Add(new BearerAuthAttribute());

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