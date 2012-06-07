using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class sales_vieworders : Telerik.Web.UI.RadAjaxPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SalesRep"].ToString() != null)
        {
            Session["ShoppingCart"] = null;
        }
        else if (Session["SalesRep"].ToString() == null)
        {
            Response.Redirect("~/security/login.aspx");
        }
        else
        {
            Response.Redirect("~/security/login.aspx");
        }
    }


    protected void rgPending_ItemCommand(object source, GridCommandEventArgs e)
    {
            
        if (e.CommandName == "Select")
        {
            string strGuid = "";
            string strPONum = "";
            string strHeaderOrderID = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            strGuid = dataItem["GUID"].Text;
            strPONum = dataItem["CartName"].Text;
            strHeaderOrderID = dataItem["OrderID"].Text;
            Session["ShoppingCart"] = strGuid;
            Session["PONumber"] = strPONum;
            Session["HeaderOrderID"] = strHeaderOrderID;
            Response.Redirect("confirmorder.aspx");
        }
        if (e.CommandName == "Delete")
        {
            string strGuid = "";
            string strPONum = "";
            string strHeaderOrderID = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            strGuid = dataItem["GUID"].Text;
            strPONum = dataItem["CartName"].Text;
            strHeaderOrderID = dataItem["OrderID"].Text;
            
            
            // Convert Guid string back to a Guid
            Guid newGuid = new Guid(strGuid);

            // Delete shopping cart items items
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
            String deleteCartInfoCmd = "DELETE from SOPShoppingCartInfo WHERE GUID = '" + newGuid + "'";
            String deleteCartItemsCmd = "DELETE from SOPShoppingCart WHERE GUID = '" + newGuid + "'";
            String deleteHeaderCmd = "UPDATE SOPShoppingCartHeader SET Status = 'D' WHERE OrderID = '" + strHeaderOrderID + "'";
            SqlCommand myCommand1 = new SqlCommand(deleteCartInfoCmd, myConnection);
            SqlCommand myCommand2 = new SqlCommand(deleteCartItemsCmd, myConnection);
            SqlCommand myCommand3 = new SqlCommand(deleteHeaderCmd, myConnection);
            myCommand1.Connection.Open();
            myCommand1.ExecuteNonQuery();
            myCommand1.Connection.Close();
            myCommand2.Connection.Open();
            myCommand2.ExecuteNonQuery();
            myCommand2.Connection.Close();
            myCommand3.Connection.Open();
            myCommand3.ExecuteNonQuery();
            myCommand3.Connection.Close();

            Response.Redirect("vieworders.aspx");
        }
       
    }
    
}
