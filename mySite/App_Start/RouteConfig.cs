using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace mySite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
             * */

            //Site
            routes.MapRoute(
                name: "Site",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "tag/{cat_id}",
                defaults: new { controller = "Home", action = "Tag" }
            );

            routes.MapRoute(
                name: "Post",
                url: "post/{slug}",
                defaults: new { controller = "Home", action = "Post" }
            );
            routes.MapRoute(
                name: "500",
                url: "err",
                defaults: new { controller = "Home", action = "ServerErr" }
            );

            //Admin
            routes.MapRoute(
                name: "admin-cat-list",
                url: "admin/cat/",
                defaults: new { controller = "Category", action = "CatList" }
            );

            routes.MapRoute(
                name: "admin-cat-view",
                url: "admin/cat/view/{id}",
                defaults: new { controller = "Category", action = "GetCat" }
            );

            routes.MapRoute(
                name: "admin-cat-add",
                url: "admin/cat/add",
                defaults: new { controller = "Category", action = "AddCat" }
            );

            routes.MapRoute(
                name: "admin-cat-update",
                url: "admin/cat/update",
                defaults: new { controller = "Category", action = "UpdateCat" }
            );

            routes.MapRoute(
                name: "admin-cat-delete",
                url: "admin/cat/delete/{id}",
                defaults: new { controller = "Category", action = "DeleteCat" }
            );

            routes.MapRoute(
                name: "admin-allpost",
                url: "admin/post/",
                defaults: new { controller = "Post", action = "PostList" }
            );

            routes.MapRoute(
                name: "admin-viewpost",
                url: "admin/post/view/{id}",
                defaults: new { controller = "Post", action = "GetPost" }
            );

            routes.MapRoute(
                name: "admin-newpost",
                url: "admin/post/add",
                defaults: new { controller = "Post", action = "AddPost" }
            );

            routes.MapRoute(
                name: "admin-updatepost",
                url: "admin/post/update",
                defaults: new { controller = "Post", action = "UpdatePost" }
            );

            routes.MapRoute(
                name: "admin-deletepost",
                url: "admin/post/delete/{id}",
                defaults: new { controller = "Post", action = "DeletePost" }
            );

            //User
            routes.MapRoute(
                name: "admin-login",
                url: "admin",
                defaults: new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                name: "admin-logout",
                url: "admin/logout",
                defaults: new { controller = "Admin", action = "Logout" }
            );

            routes.MapRoute(
                name: "admin-err-auth",
                url: "admin/err/auth",
                defaults: new { controller = "Admin", action = "ErrUnauth" }
            );

            routes.MapRoute(
                name: "admin-err-server",
                url: "admin/err/server",
                defaults: new { controller = "Admin", action = "ErrServer" }
            );
        }
    }
}
