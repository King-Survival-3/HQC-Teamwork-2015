﻿using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace KingSurvival.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // var cors = new EnableCorsAttribute("*", "*", "*");
            // config.EnableCors();

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}