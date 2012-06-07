using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class sales_selectretailer : Telerik.Web.UI.RadAjaxPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

             
    }
    
    protected void rgRetailers_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string CustomerNum = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            CustomerNum = dataItem["CustomerNum"].Text;
            Session["CustomerID"] = CustomerNum.ToString();
            Response.Redirect("orderinfo.aspx");
        }
    }

    protected void rgRetailers_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            string onHold;
            GridDataItem dataItem = e.Item as GridDataItem;
            onHold = dataItem["HOLD"].Text;

            if (onHold == "1")
            {
                
                (dataItem["Status"].Controls[0] as ImageButton).ImageUrl = "~/images/onhold.png";
                //dataItem["Status"].Text = "This retailer is on currently on hold!";
            }
            else
            {
                (dataItem["Status"].Controls[0] as ImageButton).ImageUrl = "~/images/notonhold.png";
            }
        }
    } 
}
