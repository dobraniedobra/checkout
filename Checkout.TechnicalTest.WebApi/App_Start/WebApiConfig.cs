using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Checkout.TechnicalTest.WebApi.Filters;

namespace Checkout.TechnicalTest.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ControllerExceptionAttribute());

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
