using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class security_forgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void PasswordRecovery1_SendingMail(object sender, MailMessageEventArgs e)
    {

        SmtpClient smtpClient = new SmtpClient();

        smtpClient.EnableSsl = true;
        smtpClient.Send(e.Message);

        e.Cancel = true;
    }

}
