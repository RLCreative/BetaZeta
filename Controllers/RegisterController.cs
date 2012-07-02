using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeta.Models;
using System.Configuration;
using System.Net.Mail;

namespace Zeta.Controllers
{
    public class RegisterController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Quick validatePerson, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string strHTML;
                    // INTERNAL EMAIL 

                    SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                    MailAddress from = new MailAddress("Regal Lager, Inc. <alerts@regallager.com>");
                    MailAddress to = new MailAddress("bill@regallager.com", "Whatever");

                    strHTML = "<html><head></head><body>";
                    strHTML = strHTML + "First Name: " + form["FirstName"] + "<br />";
                    strHTML = strHTML + "Last Name: " + form["LastName"] + "<br />";
                    strHTML = strHTML + "Address: " + form["Address"] + "<br />";
                    strHTML = strHTML + "Address2: " + form["Address2"] + "<br />";
                    strHTML = strHTML + "City: " + form["City"] + "<br />";
                    strHTML = strHTML + "State: " + form["State"] + "<br />";
                    strHTML = strHTML + "Zip: " + form["Zip"] + "<br />";
                    strHTML = strHTML + "Country: " + form["Country"] + "<br />";
                    strHTML = strHTML + "Phone: " + form["Phone"] + "<br />";
                    strHTML = strHTML + "Email: " + form["Email"] + "<br />";
                    strHTML = strHTML + "</body></html>";

                    MailMessage message = new MailMessage(from, to);
                    message.To.Add("allen@regallager.com");
                    message.Subject = "Email Request";
                    message.IsBodyHtml = true;
                    message.Body = strHTML;
                    client.EnableSsl = true;
                    mailer.BypassCertificateError();
                    client.Send(message);

                    
                    return View("Success");
                }
                catch (Exception)
                {
                    TempData["Message"] = "Please fill in all the required fields";
                    //ViewBag.show = "visibility: visible;";
                    return View();
                }
                // Note to self if valid and want to capture data, this is where the magic happens.
                // Redirect them to the home page
                //return Redirect("/");
            }

            // if invalid
            return View(validatePerson);

        }

    }

}

    




