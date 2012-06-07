using System;
using System.Security.Authentication;
using System.Security;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

public partial class security_login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetFocus(Login1.FindControl("UserName"));    
            
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole(Login1.UserName, "admin"))
                Response.Redirect("~/internal/admin/vieworders.aspx");
            else if (Roles.IsUserInRole(Login1.UserName, "retailer"))
                Response.Redirect("~/internal/retailer/vieworders.aspx");
            else if (Roles.IsUserInRole(Login1.UserName, "salesrep"))
                Response.Redirect("~/internal/sales/vieworders.aspx");
        }
        
}

