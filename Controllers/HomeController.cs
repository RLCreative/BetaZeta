using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace Zeta.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MetaDescription = "Akord incontinence disposal offers discreet, hygienic disposal of your adult incontinence products.";
            ViewBag.MetaKeywords = "Akord incontinence disposal offers discreet, hygienic disposal of your adult incontinence products."; 
            ViewBag.StylesheetURL = "../../Content/nivo-slider.css";
            ViewBag.ScriptJQ171 = "Scripts/jquery-1.7.1.min.js";
            ViewBag.ScriptURL = "/Scripts/jquery.nivo.slider.js";
            return View();
        }

        public ActionResult News()
        {
            
           return View();
        }

        public ActionResult CPS()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }
        
        public ActionResult Terms()
        {
            return View();
        }
        
        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Test()
        {
            //var db = new RLEntities();

            //return View(db.SOPPDFHeaders.ToList().OrderByDescending(pdf => pdf.PDFRefNum));
            return View();
        }

        public ActionResult Email()
        {
            //var db = new RLEntities();

            //return View(db.SOPPDFHeaders.ToList().OrderByDescending(pdf => pdf.PDFRefNum));
            ViewBag.show = "visibility: visible;";
            return View();
        }

        [HttpPost]
        public ActionResult Email(FormCollection form)
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
                strHTML = strHTML + "First Name: " + form["txtFirstName"] + "<br />";
                strHTML = strHTML + "First Name: " + form["txtLastName"] + "<br />";
                strHTML = strHTML + "First Name: " + form["txtEmail"] + "<br />";
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
            
        }

        public ActionResult EmailSuccess()
        {            
            return View();
        }
    }
}
