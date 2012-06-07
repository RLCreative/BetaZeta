using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class retailers_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
             
     if (Roles.IsUserInRole("retailer"))
     {
         Response.Redirect("~/internal/retailers/vieworders.aspx");
     }
     else if (Roles.IsUserInRole("salesrep"))
     {
         Response.Redirect("~/internal/sales/vieworders.aspx");
     }
     else if (Roles.IsUserInRole("admin"))
     {
         Response.Redirect("~/internal/admin/vieworders.aspx");
     }
     else
     {
         Response.Redirect("~/internal/security/login.aspx");
     }
        
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (txtUserName.Text == "rlretailers" && txtPassword.Text == "#1customers")
        //{
        //    Response.Redirect("~/old/retailers/default.aspx");
        //}
        //else
        //{
        //    lblErrorMessage.ForeColor = System.Drawing.Color.Firebrick;
        //    lblErrorMessage.Text = "Your username and password did not match. Please try again.";
        //}
    }
}
