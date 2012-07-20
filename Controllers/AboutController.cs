using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zeta.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult AboutUs()
        {
            ViewBag.MetaDescription = "Akord incontinence disposal offers discreet, hygienic disposal of your adult incontinence products.";
            ViewBag.MetaKeywords = "Akord incontinence disposal offers discreet, hygienic disposal of your adult incontinence products.";
            
            return View();
        }

        public ActionResult Contact()
        {
            
            return View();
        }

        public ActionResult Mission()
        {
           
            return View();
        }

        public ActionResult OurTeam()
        {
            
            return View();
        }

    }
}
