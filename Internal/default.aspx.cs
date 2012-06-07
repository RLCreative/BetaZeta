using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Viceroy.Internal
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole("retailer"))
            {
                divRetailer.Visible = true;
                divSalesrep.Visible = false;
                divAdmin.Visible = false;
                divAnonymous.Visible = false;                

            }
            else if (Roles.IsUserInRole("salesrep"))
            {
                divRetailer.Visible = false;
                divSalesrep.Visible = true;
                divAdmin.Visible = false;
                divAnonymous.Visible = false;
                
            }
            else if (Roles.IsUserInRole("admin"))
            {
                divRetailer.Visible = false;
                divSalesrep.Visible = false;
                divAdmin.Visible = true;
                divAnonymous.Visible = false;
                
            }            
            else
            {
                Response.Redirect("../home");
                //divRetailer.Visible = false;
                //divSalesrep.Visible = false;
                //divAdmin.Visible = false;
                //divAnonymous.Visible = true;
                
            }
        }
    }
}