using mySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace mySite.Controllers
{
    public class PostController : Controller
    {
        //Site
        public ActionResult Index()
        {
            return View();
        }


        //Api
        public JsonResult getPosts()
        {
            PostModel pm = new PostModel();
            int count = pm.getAllPost().Count();

            MsgModel msg = new MsgModel();
            if (pm.response == "500")
            {
                msg.status = "500";
                msg.data = null;
            }
            else
            {
                msg.status = "200";
                if (count < 1)
                {
                    msg.data = null;
                }
                else
                {
                    msg.data = pm.getAllPost();
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        

        //Admin
        public ActionResult PostList()
        {
            if (Session["adInfo"] != null)
            {
                PostModel pm = new PostModel();
                List<PostModel> pmlist = new List<PostModel>();

                foreach (PostModel post in pm.getAllPost())
                {
                    pmlist.Add(post);
                }

                if (pm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    string data = "";

                    foreach (PostModel p in pmlist)
                    {
                        data = data + "<tr><td><a href='" + Url.Action("GetPost", "Post", new { id = p.post_id }) + "'><b>" + p.post_name + "</b></a></td>"
                               + "<td><a class='btn btn-secondary' href='" + Url.Action("DeletePost", "Post", new { id = p.post_id }) + "'>Delete</a></td></tr>";
                    }

                    ViewBag.postList = data;

                    return View();
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        public ActionResult GetPost(string id)
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel catm = new CategoryModel();
                List<CategoryModel> catList = new List<CategoryModel>();
                foreach (CategoryModel cat in catm.getAllCatagory())
                {
                    catList.Add(cat);
                }

                PostModel pm = new PostModel();
                pm.post_id = id;
                pm = pm.getPostById();

                if (pm.response == "500" || catm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    ViewBag.pId = id;
                    ViewBag.pT = pm.post_name;
                    ViewBag.pCov = pm.post_cover;
                    ViewBag.pCon = pm.post_content;

                    string cList = "";
                    foreach (CategoryModel c in catList)
                    {
                        if (c.cat_id == pm.post_category)
                        {
                            cList = cList + "<option value='" + c.cat_id + "' selected>" + c.cat_name + "</option>";
                        }
                        else
                        {
                            cList = cList + "<option value='" + c.cat_id + "'>" + c.cat_name + "</option>";
                        }
                    }

                    ViewBag.pCat = cList;

                    return View();
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        public ActionResult AddPost()
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel catm = new CategoryModel();
                List<CategoryModel> catList = new List<CategoryModel>();
                foreach (CategoryModel cat in catm.getAllCatagory())
                {
                    catList.Add(cat);
                }

                if (catm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    string data = "";
                    foreach (CategoryModel c in catList)
                    {
                        data = data + "<option value='" + c.cat_id + "'>" + c.cat_name + "</option>";
                    }
                    ViewBag.catList = data;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(string pN, string pCov, string pCon, string pCat)
        {
            if (Session["adInfo"] != null)
            {
                PostModel pm = new PostModel();
                pm.post_name = pN;
                pm.post_cover = pCov;
                pm.post_content = pCon;
                pm.post_category = pCat;

                string slug = createSlug(pN);
                pm.post_slug = slug;

                int count = pm.findPostMatches();
                if (count > 0)
                {
                    pm.post_slug = slug + "-" + (count + 1).ToString();
                }

                var bdTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
                DateTime bdTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, bdTimeZone);
                pm.post_date = bdTime.ToString("dd/MM/yyyy");

                pm.addPost();

                if (pm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    return RedirectToAction("PostList", "Post");
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(string id, string pN, string pCov, string pCon, string pCat)
        {
            if (Session["adInfo"] != null)
            {
                PostModel pm = new PostModel();
                pm.post_id = id;
                pm.post_name = pN;
                pm.post_cover = pCov;
                pm.post_content = pCon;
                pm.post_category = pCat;

                pm.updatePost();

                if (pm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    return RedirectToAction("PostList", "Post");
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        public ActionResult DeletePost(string id)
        {
            if (Session["adInfo"] != null)
            {
                PostModel pm = new PostModel();
                pm.post_id = id;
                pm.deletePost();

                if (pm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    return RedirectToAction("PostList", "Post");
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }

        //Functions
        private string createSlug(string str)
        {
            string output = Regex.Replace(str, @"\s+", "-");
            StringBuilder sb = new StringBuilder();
            foreach (char c in output)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '-')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().ToLower(); ;
        }

	}
}