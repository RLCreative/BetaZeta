using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net.Mail;
using System.IO;

public partial class retailerinquiry : System.Web.UI.Page
{
    private bool emailSent = false;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // Put user code to initialize the page here
    }

    #region Web Form Designer generated code
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion

    private void sendEmail()
    {
        try
        {

            // INTERNAL EMAIL 

            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
            MailAddress from = new MailAddress("Regal Lager, Inc. <alerts@regallager.com>");
            MailAddress to = new MailAddress("jeff@regallager.com", "Sales Department");
            //MailAddress to = new MailAddress("tech@regallager.com", "Regal Lager Customer Care");

            string strHTMLInternal = "";
            string strPathInternal = Server.MapPath("retailerinquiry.htm");

            System.IO.StreamReader srInternal = new System.IO.StreamReader(strPathInternal);
            srInternal = File.OpenText(strPathInternal);

            strHTMLInternal = srInternal.ReadToEnd().ToString();
            strHTMLInternal = strHTMLInternal.Replace("{0}", this.txtwebsite.Text);
            strHTMLInternal = strHTMLInternal.Replace("{1}", Server.HtmlDecode(this.txtComments.Text));
            strHTMLInternal = strHTMLInternal.Replace("{2}", this.txtFirstName.Text);
            strHTMLInternal = strHTMLInternal.Replace("{3}", this.txtLastName.Text);
            strHTMLInternal = strHTMLInternal.Replace("{4}", this.txtStoreName.Text);
            strHTMLInternal = strHTMLInternal.Replace("{13}", this.txtyearsbusiness.Text);
            strHTMLInternal = strHTMLInternal.Replace("{5}", this.txtAddress1.Text);
            strHTMLInternal = strHTMLInternal.Replace("{6}", this.txtAddress2.Text);
            strHTMLInternal = strHTMLInternal.Replace("{7}", this.txtCity.Text);
            strHTMLInternal = strHTMLInternal.Replace("{8}", this.ddlState.SelectedValue.ToString());
            strHTMLInternal = strHTMLInternal.Replace("{9}", this.txtZip.Text);
            strHTMLInternal = strHTMLInternal.Replace("{10}", this.ddlCountry.SelectedValue.ToString());
            strHTMLInternal = strHTMLInternal.Replace("{11}", this.txtPhone.Text);
            strHTMLInternal = strHTMLInternal.Replace("{12}", this.txtEmail.Text);

            srInternal.Close();
            srInternal.Dispose();

            MailMessage message = new MailMessage(from, to);
            message.Subject = "New Akord Retailer Request";
            message.IsBodyHtml = true;
            message.Body = strHTMLInternal;
            client.EnableSsl = true;
            mailer.BypassCertificateError();
            client.Send(message);


            emailSent = true;

        }
        catch (Exception ex)
        {
            this.lblSendError.Text = "There was an error submitting information. Please try again!&nbsp;" + ex.Message.ToString();
        }

    }

    private void sendEmailCopy()
    {
        //EmailMessage objEmail = new EmailMessage();

        //try
        //{
        //    objEmail.Server = ConfigurationManager.AppSettings["SMTPServer"];
        //    objEmail.To = this.txtEmail.Text;
        //    objEmail.FromAddress = "info@regallager.com";
        //    objEmail.FromName = "Regal Lager Sales Department";
        //    objEmail.Subject = "Request Received";

        //    objEmail.Body = "Thank you for contacting Regal Lager, your request has been received. We will respond as soon as possible. Thank you for your patience.\r\n\r\nIf this is urgent, please call 800-593-5522. Our office is open Monday-Friday from 8:30am to 5:30pm EST.\r\n";
        //    objEmail.Body = objEmail.Body + "\r\n";

        //    objEmail.Body = objEmail.Body + "Request Date: " + System.DateTime.Now + "\r\n";
        //    objEmail.Body = objEmail.Body + "Dropshipping: ";

        //    for (int i = 0; i < this.chkbDropship.Items.Count; i++)
        //    {
        //        if (this.chkbDropship.Items[i].Selected)
        //        {
        //            objEmail.Body = objEmail.Body + this.chkbDropship.Items[i].Value + ",";
        //        }
        //    }
        //    objEmail.Body = objEmail.Body.Remove(objEmail.Body.Length - 1, 1) + "\r\n";

        //    objEmail.Body = objEmail.Body + "Comments: " + Server.HtmlDecode(this.txtComments.Text) + "\r\n";

        //    objEmail.Send();
        //}
        //catch
        //{

        //}
    }

    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        int brandCount = 0;

        for (int i = 0; i < this.chkbDropship.Items.Count; i++)
        {
            if (this.chkbDropship.Items[i].Selected)
           {
                brandCount++;
         }
        }

        if (brandCount > 0)
        {

            sendEmail();
            sendEmailCopy();

            if (emailSent == true)
            {
                this.pnlForm.Visible = false;
                this.lblSendError.Text = "Thank you for your interest in selling Akord Products. Our Sales Team will be  in contact with you shortly.";
            }
        }
        else
        {
            this.lblSendError.Text = "Error submitting information. You must select at least one brand. Please try again!<br>&nbsp;";
        }
    }
}