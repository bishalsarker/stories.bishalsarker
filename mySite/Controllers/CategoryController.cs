using mySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mySite.Controllers
{
    public class CategoryController : Controller
    {
        //Site
        public ActionResult Index()
        {
            return View();
        }

        //Api
        public JsonResult getCategories()
        {
            CategoryModel cgm = new CategoryModel();
            int count = cgm.getAllCatagory().Count();

            MsgModel msg = new MsgModel();
            if (cgm.response == "500")
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
                    msg.data = cgm.getAllCatagory();
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getCategory(string id)
        {
            CategoryModel cgm = new CategoryModel();
            cgm.cat_id = id;
            cgm = cgm.getCatagory();

            MsgModel msg = new MsgModel();
            if (cgm.response == "500")
            {
                msg.status = "500";
                msg.data = null;
            }
            else
            {
                msg.status = "200";
                msg.data = cgm.cat_name;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        //Admin
        public ActionResult CatList()
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel catm = new CategoryModel();
                List<CategoryModel> catlist = new List<CategoryModel>();

                foreach (CategoryModel cat in catm.getAllCatagory())
                {
                    catlist.Add(cat);
                }

                if (catm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    string data = "";

                    foreach (CategoryModel p in catlist)
                    {
                        data = data + "<tr><td><a href='" + Url.Action("GetCat", "Category", new { id = p.cat_id }) + "'><b>" + p.cat_name + "</b></a></td>"
                               + "<td><a class='btn btn-secondary' href='" + Url.Action("DeleteCat", "Category", new { id = p.cat_id }) + "'>Delete</a></td></tr>";
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
        public ActionResult GetCat(string id)
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel catm = new CategoryModel();
                catm.cat_id = id;
                catm = catm.getCatagory();

                if (catm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    ViewBag.cId = id;
                    ViewBag.cN = catm.cat_name;

                    return View();
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        public ActionResult AddCat()
        {
            if (Session["adInfo"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        [HttpPost]
        public ActionResult AddCat(string cat_name)
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel cgm = new CategoryModel();
                cgm.cat_name = cat_name;
                cgm.addCatagory();

                if (cgm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    return RedirectToAction("CatList", "Category");
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        [HttpPost]
        public ActionResult UpdateCat(string cat_id, string cat_name)
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel cgm = new CategoryModel();
                cgm.cat_id = cat_id;
                cgm.cat_name = cat_name;
                cgm.updateCatagory();

                if (cgm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    return RedirectToAction("CatList", "Category");
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
        public ActionResult DeleteCat(string id)
        {
            if (Session["adInfo"] != null)
            {
                CategoryModel cgm = new CategoryModel();
                cgm.cat_id = id;
                cgm.deleteCatagory();

                if (cgm.response == "500")
                {
                    return RedirectToAction("ErrServer", "Admin");
                }
                else
                {
                    return RedirectToAction("CatList", "Category");
                }
            }
            else
            {
                return RedirectToAction("ErrUnauth", "Admin");
            }
        }
	}
}