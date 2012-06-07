using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Zeta.Controllers
{
    public class RegisterController : Controller
    {

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
                return Redirect("/Register/Cybex");
            }
            if (form["CategoryID"] == "2")
            {
                return Redirect("/Register/CybexCar");
            }
            if (form["CategoryID"] == "3")
            {
                return Redirect("/Register/Lascal");
            }
           
            return View();

        }
        public ActionResult Cybex()
        {
           return View();
        }

        public ActionResult CybexCar()
        {

            return View();
        }

        public ActionResult Lascal()
        {
            return View();
        }


    }
}







