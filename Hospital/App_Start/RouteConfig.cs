using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Hospital
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Sitemap", 
                url: "Sitemap.xml",
                defaults: new { controller = "Home", action = "Sitemap", id = UrlParameter.Optional });

            // Show a 404 error page for anything else.
            routes.MapRoute(
                        "404-PageNotFound",
                        "{*url}",
                new { controller = "Error", action = "Error404" }
            );

           
        }
    }
}
