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
        public ActionResult GetSupport()
        {
            return View();
        }

        public ActionResult Faqtest()
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
        //public ActionResult Request()
        //{
        //    Person newPerson = new Person();
        //    return View(newPerson);
        //}
        //[HttpPost]
        //public ActionResult Request(Person validatePerson, FormCollection form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            string strHTML;
        //            // INTERNAL EMAIL 

        //            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
        //            MailAddress from = new MailAddress("Regal Lager, Inc. <alerts@regallager.com>");
        //            MailAddress to = new MailAddress("bill@regallager.com", "Sales Department");
        //            //MailAddress to = new MailAddress("tech@regallager.com", "Regal Lager Customer Care");

        //            //string strHTMLInternal = "";
        //            //string strPathInternal = Server.MapPath("retailerinquiry.htm");

        //            //System.IO.StreamReader srInternal = new System.IO.StreamReader(strPathInternal);
        //            //srInternal = File.OpenText(strPathInternal);

        //            //strHTMLInternal = srInternal.ReadToEnd().ToString();
        //            //strHTMLInternal = strHTMLInternal.Replace("{0}", this.txtwebsite.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{1}", Server.HtmlDecode(this.txtComments.Text));
        //            //strHTMLInternal = strHTMLInternal.Replace("{2}", this.txtFirstName.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{3}", this.txtLastName.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{4}", this.txtStoreName.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{13}", this.txtyearsbusiness.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{5}", this.txtAddress1.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{6}", this.txtAddress2.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{7}", this.txtCity.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{8}", this.ddlState.SelectedValue.ToString());
        //            //strHTMLInternal = strHTMLInternal.Replace("{9}", this.txtZip.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{10}", this.ddlCountry.SelectedValue.ToString());
        //            //strHTMLInternal = strHTMLInternal.Replace("{11}", this.txtPhone.Text);
        //            //strHTMLInternal = strHTMLInternal.Replace("{12}", this.txtEmail.Text);
        //            //srInternal.Close();
        //            //srInternal.Dispose();

        //            strHTML = "<html><head></head><body>";
        //            strHTML = strHTML + "First Name: " + form["FirstName"] + "<br />";
        //            strHTML = strHTML + "First Name: " + form["LastName"] + "<br />";
        //            strHTML = strHTML + "First Name: " + form["Email"] + "<br />";
        //            strHTML = strHTML + "</body></html>";
        //            MailMessage message = new MailMessage(from, to);
        //            message.To.Add("allen@regallager.com");
        //            message.Subject = "Email Request";
        //            message.IsBodyHtml = true;
        //            message.Body = strHTML;
        //            client.EnableSsl = true;
        //            mailer.BypassCertificateError();
        //            client.Send(message);

        //            TempData["Message"] = ">We will try our best to respond to your email within one business day.";
        //            ViewBag.show = "visibility: hidden;";
        //            return View();
        //            //return Redirect("/Home/EmailSuccess");
        //        }
        //        catch (Exception)
        //        {
        //            TempData["Message"] = "Uh oh it didn't work!!!";
        //            ViewBag.show = "visibility: visible;";
        //            return View();
        //        }
        //        // Note to self if valid and want to capture data, this is where the magic happens.
        //        // Redirect them to the home page
        //        //return Redirect("/");
        //    }
       
        //    // if invalid
        //    return View(validatePerson);
        //}
        //public ActionResult Validation()
        //{
        //    Person newPerson = new Person();
        //   return View(newPerson);
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
