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
            //ViewBag.RegHeader = "";
            //ViewBag.RegList = "";
            //ViewBag.Cybex = "display: none;";
            //ViewBag.CybexCar = "display: none;";
            //ViewBag.Lascal = "display: none;";
            //ViewBag.ContactInfo = "display: none;";
            return View();
        }
        [HttpPost]
        public ActionResult Index(Person validatePerson, FormCollection form)
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
                        strHTML = strHTML + "Email Name: " + form["Email"] + "<br />";
                        strHTML = strHTML + "State: " + form["State"] + "<br />";
                        strHTML = strHTML + "</body></html>";

                        MailMessage message = new MailMessage(from, to);
                        message.To.Add("allen@regallager.com");
                        message.Subject = "Email Request";
                        message.IsBodyHtml = true;
                        message.Body = strHTML;
                        client.EnableSsl = true;
                        mailer.BypassCertificateError();
                        client.Send(message);

                        //TempData["Message"] = ">We will try our best to respond to your email within one business day.";
                        //ViewBag.show = "visibility: hidden;";
                        return View();
                        //return Redirect("/Home/EmailSuccess");
                    }
                    catch (Exception)
                    {
                        //TempData["Message"] = "Uh oh it didn't work!!!";
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

        
        //public ActionResult Register()
        //{
        //    ViewBag.Header = "visibility: visible;";
        //    return View();
        //}




    }








