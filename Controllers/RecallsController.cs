using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zeta.Controllers
{
    public class RecallsController : Controller
    {
        //
        // GET: /Recalls/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            TempData["Message"] = form["CategoryID"];
            if (form["CategoryID"] == "1")
            {
                //return View();
                return Redirect("/Recalls/XFix");
            }
            if (form["CategoryID"] == "2")
            {
                return Redirect("/Recalls/TwoGo");
            }
            if (form["CategoryID"] == "3")
            {
               return Redirect("/Recalls/Stroller");
            }
            if (form["CategoryID"] == "4")
            {
                return Redirect("/Recalls/Board");
            }
            return View();

        }

        public ActionResult XFix()
        {
            return View();
        }
        
        public ActionResult TwoGO()
        {
            return View();
        }
        
        public ActionResult Stroller()
        {
            return View();
        }

        public ActionResult Board()
        {
            return View();
        }
    }
}
