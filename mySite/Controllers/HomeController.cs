using mySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mySite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return RedirectToAction("Tag", "Home", new { cat_id = "all" });
        }

        public ActionResult Tag(string cat_id)
        {
            if (cat_id == "" || cat_id == null)
            {
                return RedirectToAction("Index", "Home", new { cat_id = "all" });
            }
            else
            {
                //Categories
                CategoryModel catm = new CategoryModel();
                string allCat = "";
                foreach (CategoryModel cat in catm.getAllCatagory())
                {
                    allCat = allCat + "<a href='" + Url.Action("Tag", "Home", new { cat_id = cat.cat_id }) + "'><span class='pills'>#" + cat.cat_name + "</span></a>";
                }

                //Posts
                PostModel pm = new PostModel();
                string allPost = "";

                if (cat_id == "all")
                {
                    foreach (PostModel post in pm.getAllPost())
                    {
                        allPost = allPost + "<div class='card'>" +
                                            "<a href='" + Url.Action("Post", "Home", new { slug = post.post_slug }) + "'>" +
                                            "<img class='card-img-top probootstrap-animate' src='" + post.post_cover + "' alt='Card image cap'>" +
                                            "<div class='card-caption probootstrap-animate'>" +
                                            "<h3 class='card-header'>" + captionMaker(post.post_name) + "</h3>" +
                                            "<p class='card-date'>" + post.post_date + "</p></div></a></div>";
                    }
                }
                else if (cat_id == "latest")
                {
                    foreach (PostModel post in pm.getAllPostByDesc())
                    {
                        allPost = allPost + "<div class='card'>" +
                                            "<a href='" + Url.Action("Post", "Home", new { slug = post.post_slug }) + "'>" +
                                            "<img class='card-img-top probootstrap-animate' src='" + post.post_cover + "' alt='Card image cap'>" +
                                            "<div class='card-caption probootstrap-animate'>" +
                                            "<h3 class='card-header'>" + captionMaker(post.post_name) + "</h3>" +
                                            "<p class='card-date'>" + post.post_date + "</p></div></a></div>";
                    }
                }
                else
                {
                    pm.post_category = cat_id;
                    foreach (PostModel post in pm.getAllPostByCat())
                    {
                        allPost = allPost + "<div class='card'>" +
                                            "<a href='" + Url.Action("Post", "Home", new { slug = post.post_slug }) + "'>" +
                                            "<img class='card-img-top probootstrap-animate' src='" + post.post_cover + "' alt='Card image cap'>" +
                                            "<div class='card-caption probootstrap-animate'>" +
                                            "<h3 class='card-header'>" + captionMaker(post.post_name) + "</h3>" +
                                            "<p class='card-date'>" + post.post_date + "</p></div></a></div>";
                    }
                }

                if (catm.response == "500" || pm.response == "500")
                {
                    return RedirectToAction("ServerErr", "Home");
                }
                else
                {
                    ViewBag.cList = allCat;
                    ViewBag.pList = allPost;
                    return View();
                }
            }
        }
        public ActionResult Post(string slug)
        {
            if (slug == "" || slug == null)
            {
                return RedirectToAction("Tag", "Home", new { cat_id = "all" });
            }
            else
            {
                //Categories
                CategoryModel catm = new CategoryModel();
                string allCat = "";
                foreach (CategoryModel cat in catm.getAllCatagory())
                {
                    allCat = allCat + "<a href='" + Url.Action("Tag", "Home", new { cat_id = cat.cat_id }) + "'><span class='pills'>#" + cat.cat_name + "</span></a>";
                }

                //Posts
                PostModel pm = new PostModel();
                pm.post_slug = slug;
                pm = pm.getPostBySlug();

                if (catm.response == "500" || pm.response == "500")
                {
                    return RedirectToAction("ServerErr", "Home");
                }
                else
                {
                    ViewBag.Cover = pm.post_cover;
                    ViewBag.Content = pm.post_content;
                    ViewBag.Name = pm.post_name;
                    ViewBag.cList = allCat;
                    return View();
                }
            }
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult ServerErr()
        {
            return View();
        }

        private string captionMaker(string str)
        {
            if (str.Length < 50)
            {
                return str;
            }
            else
            {
                char[] arr = str.ToCharArray();
                string data = "";
                int i = 0;
                while (i < 45)
                {
                    data = data + arr[i].ToString();
                    i++;
                }
                data = data + "...";
                return data;
                
            }
        }
	}
}