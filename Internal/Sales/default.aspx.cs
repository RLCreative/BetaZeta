using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class sales_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Roles.IsUserInRole("retailer"))
        {
            Response.Redirect("vieworders.aspx");
        }
        if (Roles.IsUserInRole("salesrep"))
        {
            Response.Redirect("~/sales/vieworders.aspx");
        }
        if (Roles.IsUserInRole("admin"))
        {
            Response.Redirect("~/admin/manageusers.aspx");
        }
        else
        {
            Response.Redirect("~/sales/vieworders.aspx");
        }
    }
}
