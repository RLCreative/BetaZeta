using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace Viceroy.Internal
{
    public partial class _internal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = "";
        string userID = "";
        bool dropShipper = false;

        if (Page.User.Identity.IsAuthenticated == true)
        {
            userName = Membership.GetUser().UserName;

            try
            {
                string sql = "SELECT CustomerID, Dropshipper from [ASPNET].[dbo].[aspnet_Users_CustomerID] where UserName = '" + userName + "'";
                SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow objDR;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objDR = dt.Rows[i];
                    userID = ((string)objDR["CustomerID"]);
                    dropShipper = ((bool)objDR["Dropshipper"]);
                }
            }

            //SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETDB"].ToString());
            //String insertCmd = "SELECT CustomerID from [ASPNET].[dbo].[aspnet_Users_CustomerID] where UserName = @UserName";
            //SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);
            //myCommand.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 256));
            //myCommand.Parameters["@UserName"].Value = userName;
            //myCommand.Connection.Open();

            // EXECUTE SQL AND GET CURRENT USER             

                      
            
            catch
            {

            }
            //myCommand.Connection.Close();
        }

        if (Roles.IsUserInRole("retailer"))
        {
            Session["CustomerID"] = userID;
            Session["SalesRep"] = null;
            Session["Dropship"] = dropShipper.ToString();

        }
        else if (Roles.IsUserInRole("salesrep"))
        {
            Session["CustomerID"] = null;
            Session["SalesRep"] = userID;
            Session["Dropship"] = "false";
        }
        else if (Roles.IsUserInRole("admin"))
        {
            Session["CustomerID"] = null;
            Session["SalesRep"] = null;
            Session["Dropship"] = "false";
        }
        
        else
        {
            Session["CustomerID"] = null;
            Session["SalesRep"] = null;
            Session["Dropship"] = "false";
        }
    }
        }
    
}