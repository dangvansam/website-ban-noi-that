using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteNoiThat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
              name: "Category",
              url: "danh-muc/{cateid}",
              defaults: new { controller = "Product", action = "CategoryShow", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteNoiThat.Controllers" }
          );
            routes.IgnoreRoute("{*botdetect}",
           new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
             name: "CategoryId",
             url: "admin-sp/{cateid}",
             defaults: new { controller = "Product", action = "Show", id = UrlParameter.Optional },
             namespaces: new[] { "WebsiteNoiThat.Admin.Controllers" }
         );
            routes.MapRoute(
              name: "Search",
              url: "tim-kiem",
              defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteNoiThat.Controllers" }
          );
            routes.MapRoute(
           name: "Add CArt",
           url: "them-gio-hang",
           defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
           namespaces: new[] { "WebsiteNoiThat.Controllers" }
       );
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNoiThat.Controllers" }
            );
            routes.MapRoute(
                name: "Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteNoiThat.Controllers" }
            );
            routes.MapRoute(
               name: "Error",
               url: "loi-thanh-toan",
               defaults: new { controller = "Cart", action = "Error", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteNoiThat.Controllers" }
           );
            routes.MapRoute(
              name: "DeleteError",
              url: "loi-huy-hang",
              defaults: new { controller = "Cart", action = "DeleteError", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteNoiThat.Controllers" }
          );
            routes.MapRoute(
              name: "Payment",
              url: "thanh-toan",
              defaults: new { controller = "Cart", action = "PayBy", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteNoiThat.Controllers" }
          );
            routes.MapRoute(
              name: "Register",
              url: "dang-ky",
              defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteNoiThat.Controllers" }
          );
            routes.MapRoute(
              name: "Login",
              url: "dang-nhap",
              defaults: new { controller = "RegisterAndLogin", action = "Login", id = UrlParameter.Optional },
              namespaces: new[] { "WebsiteNoiThat.Controllers" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "WebsiteNoiThat.Controllers" }

            );
           

        }
    }
}
