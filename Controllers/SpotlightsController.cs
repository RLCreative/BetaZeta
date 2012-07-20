using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zeta.Views.Spotlights
{
    public class SpotlightsController : Controller
    {
        //
        // GET: /Spotlights/

        public ActionResult Index()
        {
            
           return View();
        }
        public ActionResult Miosolo()
        {
            return View();
        }
        
    }
}
