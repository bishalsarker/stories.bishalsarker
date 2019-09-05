using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mySite.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            if (Session["adInfo"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("PostList", "Post");
            }
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (!(username.Equals("mr.bishalsarker@gmail.com")) && !(password.Equals("@1B1c1d1")))
            {
                TempData["Msg"] = "Wrong username or password!";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                Session["adInfo"] = username;
                return RedirectToAction("PostList", "Post");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult ErrUnauth()
        {
            return View();
        }

        public ActionResult ErrServer()
        {
            return View();
        }
	}
}