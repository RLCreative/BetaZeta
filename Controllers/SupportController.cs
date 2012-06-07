using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using Zeta.Models;

namespace Zeta.Controllers
{

    public class SupportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faqs()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Videos()
        {
            return View();
        }

        public ActionResult Recalls()
        {
            return View();
        }

        public ActionResult Instructions()
        {
            return View();
        }

        public ActionResult Warranty()
        {
            return View();
        }
        public ActionResult Testing()
        {
            return View();
        }
        public ActionResult Request()
        {
            return View();
        }
        //public ActionResult Validation()
        //{
        //    Person newPerson = new Person();
        //    return View(newPerson);
        //}

        //[HttpPost]
        //public ActionResult Validation(Person validatePerson)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Note to self if valid and want to capture data, this is where the magic happens.
        //        // Redirect them to the home page
        //        return Redirect("/");
        //    }

        //    // if invalid
        //    return View(validatePerson);
        //}
    }
}
