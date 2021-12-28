using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChatCrypto.WebUI
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
            #region Sayfalar
            routes.MapRoute(
               name: "Anasayfa",
               url: "",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Register",
               url: "",
               defaults: new { controller = "Register", action = "Register", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "LogIn",
               url: "",
               defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
           );
            #endregion
            #region parçalı sayfalar
            routes.MapRoute(
               name: "PartialUsersList",
               url: "PartialUsersList",
               defaults: new { controller = "Home", action = "PartialUsersList", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "PartialUsersDesc",
              url: "PartialUsersDesc",
              defaults: new { controller = "Home", action = "PartialUsersDesc", id = UrlParameter.Optional }
          );
            #endregion
        }
    }
}
