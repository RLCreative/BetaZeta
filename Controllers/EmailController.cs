using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeta.Models;
using System.Net.Mail;
using System.IO;
using System.Configuration;

namespace Zeta.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Consumer()
        {
            Person newPerson = new Person();
            return View(newPerson);
        }
        public ActionResult Media()
        {
            Person newPerson = new Person();
            return View(newPerson);
        }
        [HttpPost]
        public ActionResult Consumer(Person validatePerson, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string strHTML;
                    // INTERNAL EMAIL 

                    SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                    MailAddress from = new MailAddress("Regal Lager, Inc. <alerts@regallager.com>");
                    MailAddress to = new MailAddress("bill@regallager.com", "Sales Department");
                    //MailAddress to = new MailAddress("tech@regallager.com", "Regal Lager Customer Care");

                    //string strHTMLInternal = "";
                    //string strPathInternal = Server.MapPath("retailerinquiry.htm");

                    //System.IO.StreamReader srInternal = new System.IO.StreamReader(strPathInternal);
                    //srInternal = File.OpenText(strPathInternal);

                    //strHTMLInternal = srInternal.ReadToEnd().ToString();
                    //strHTMLInternal = strHTMLInternal.Replace("{0}", this.txtwebsite.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{1}", Server.HtmlDecode(this.txtComments.Text));
                    //strHTMLInternal = strHTMLInternal.Replace("{2}", this.txtFirstName.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{3}", this.txtLastName.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{4}", this.txtStoreName.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{13}", this.txtyearsbusiness.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{5}", this.txtAddress1.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{6}", this.txtAddress2.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{7}", this.txtCity.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{8}", this.ddlState.SelectedValue.ToString());
                    //strHTMLInternal = strHTMLInternal.Replace("{9}", this.txtZip.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{10}", this.ddlCountry.SelectedValue.ToString());
                    //strHTMLInternal = strHTMLInternal.Replace("{11}", this.txtPhone.Text);
                    //strHTMLInternal = strHTMLInternal.Replace("{12}", this.txtEmail.Text);
                    //srInternal.Close();
                    //srInternal.Dispose();

                    strHTML = "<html><head></head><body>";
                    strHTML = strHTML + "First Name: " + form["FirstName"] + "<br />";
                    strHTML = strHTML + "First Name: " + form["LastName"] + "<br />";
                    strHTML = strHTML + "First Name: " + form["Email"] + "<br />";
                    strHTML = strHTML + "</body></html>";
                    MailMessage message = new MailMessage(from, to);
                    message.To.Add("allen@regallager.com");
                    message.Subject = "Email Request";
                    message.IsBodyHtml = true;
                    message.Body = strHTML;
                    client.EnableSsl = true;
                    mailer.BypassCertificateError();
                    client.Send(message);

                    TempData["Message"] = ">We will try our best to respond to your email within one business day.";
                    ViewBag.show = "visibility: hidden;";
                    return View();
                    //return Redirect("/Home/EmailSuccess");
                }
                catch (Exception)
                {
                    TempData["Message"] = "Uh oh it didn't work!!!";
                    ViewBag.show = "visibility: visible;";
                    return View();
                }
                // Note to self if valid and want to capture data, this is where the magic happens.
                // Redirect them to the home page
                //return Redirect("/");
            }

            // if invalid
            return View(validatePerson);
        }

        [HttpPost]
        public ActionResult Attachment(AddAttachment model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string strHTML;
            // INTERNAL EMAIL 

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            MailAddress from = new MailAddress("Regal Lager, Inc. <alerts@regallager.com>");
            MailAddress to = new MailAddress("bill@regallager.com", "Sales Department");
            strHTML = "<html><head></head><body>";
            strHTML = strHTML + "Question: " + model.Question + "<br />";
            strHTML = strHTML + "</body></html>";
            MailMessage message = new MailMessage(from, to);
            message.To.Add("allen@regallager.com");
            message.Subject = "Email Request";
            message.IsBodyHtml = true;
            message.Body = strHTML;
            //message.Attachments = new Attachment(model.Attachment.InputStream;
            //if (model.Attachment != null && model.Attachment.ContentLength > 0)
            //{
            //var attachment = new Attachment(model.Attachment.InputStream, model.Attachment.FileName);
            //    message.Attachments.Add(attachment);
            //}

            client.EnableSsl = true;
            mailer.BypassCertificateError();
            client.Send(message);

            TempData["Message"] = ">We will try our best to respond to your email within one business day.";
            ViewBag.show = "visibility: hidden;";
            //return View();
            
            return Content("email sent", "text/plain");
        }

        public ActionResult AutoComplete()
        {
            
            return View();
        }
        public ActionResult Sales()
        {

            return View();
        }
    }
}
