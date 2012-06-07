using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class admin_ManageUsers : Telerik.Web.UI.RadAjaxPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    
    protected void rgUsers_UpdateCommand(object source, GridCommandEventArgs e)
    {
        //Get the GridDataItem of the RadGrid
        System.Threading.Thread.Sleep(1000);
        GridEditableItem editedItem = e.Item as GridEditableItem;

        //Get the primary key value using the DataKeyValue.
        string userID = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["UserID"].ToString();        
        string userName = (editedItem.FindControl("txtUserName") as TextBox).Text;
        string email = (editedItem.FindControl("txtEmail") as TextBox).Text;
        string customerID = (editedItem.FindControl("txtCustomerID") as TextBox).Text;
        string roleName = (editedItem.FindControl("lblRoleName") as Label).Text;
        bool IsLockedOut = (editedItem.FindControl("ckbLockedOut") as CheckBox).Checked;
        bool dropshipper = (editedItem.FindControl("ckbDropshipper") as CheckBox).Checked;
        bool freightcollect = (editedItem.FindControl("ckbFreightCollect") as CheckBox).Checked;
        DropDownList carrier = (editedItem.FindControl("ddlCarrier") as DropDownList);
        string accountnum = (editedItem.FindControl("txtAccountNum") as TextBox).Text;
        RadioButtonList rblRole = (editedItem.FindControl("rblRoles") as RadioButtonList);
        string[] user = new string[]
        {
            userName
        };



        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETDB"].ToString());
        SqlCommand myCommand = new SqlCommand("aspnet_Membership_UpdateUserFromWebSite", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar, 256));
        myCommand.Parameters["@UserID"].Value = userID;
        myCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 256));
        myCommand.Parameters["@Email"].Value = email;
        myCommand.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 256));
        myCommand.Parameters["@UserName"].Value = userName;
        myCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar, 10));
        myCommand.Parameters["@CustomerID"].Value = customerID;
        myCommand.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 256));
        myCommand.Parameters["@RoleName"].Value = roleName;
        myCommand.Parameters.Add(new SqlParameter("@IsLockedOut", SqlDbType.Bit));
        myCommand.Parameters["@IsLockedOut"].Value = IsLockedOut;
        myCommand.Parameters.Add(new SqlParameter("@Dropshipper", SqlDbType.Bit));
        myCommand.Parameters["@Dropshipper"].Value = dropshipper;
        myCommand.Parameters.Add(new SqlParameter("@FreightCollect", SqlDbType.Bit));
        myCommand.Parameters["@FreightCollect"].Value = freightcollect;
        myCommand.Parameters.Add(new SqlParameter("@Carrier", SqlDbType.NVarChar, 50));
        myCommand.Parameters["@Carrier"].Value = carrier.SelectedValue.ToString();
        myCommand.Parameters.Add(new SqlParameter("@AccountNum", SqlDbType.NVarChar, 25));
        myCommand.Parameters["@AccountNum"].Value = accountnum;
        myCommand.Connection.Open();

        try
        {
            int rows = 0;
            rows = myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();
            //lblError.Text = "Error received: " + ex.Message;
            //rgUsers.Controls.Add(new LiteralControl("Unable to update Users Record. Reason: " + ex.Message));
            //e.Canceled = true;
        }
        catch (Exception ex)
        {
            lblError.Text = "Error received: " + ex.Message;
            //rgUsers.Controls.Add(new LiteralControl("Unable to update Users Record. Reason: " + ex.Message));
            //e.Canceled = true;
        }

        RadioButtonList rblRoles = new RadioButtonList();
        
        //CHECK TO SEE IF A NEW ROLE HAS BEEN SELECTED

        if (rblRole.SelectedValue == "admin" && roleName == "")
        {
            Roles.AddUserToRole(userName, "admin");
        }
        else if (rblRole.SelectedValue == "customercare" && roleName == "")
        {
            Roles.AddUserToRole(userName, "customercare");
        }
        else if (rblRole.SelectedValue == "retailer" && roleName == "")
        {
            Roles.AddUserToRole(userName, "retailer");
        }
        else if (rblRole.SelectedValue == "sales" && roleName == "")
        {
            Roles.AddUserToRole(userName, "sales");
        }
        else if (rblRole.SelectedValue == "salesrep" && roleName == "")
        {
            Roles.AddUserToRole(userName, "salesrep");
        }
        else if (rblRole.SelectedValue == "admin" && roleName != "admin")
        {
            Roles.RemoveUserFromRole(userName, roleName);
            Roles.AddUserToRole(userName, "admin");
        }
        else if (rblRole.SelectedValue == "customercare" && roleName != "customercare")
        {
            Roles.RemoveUserFromRole(userName, roleName);
            Roles.AddUserToRole(userName, "customercare");
        }
        else if (rblRole.SelectedValue == "retailer" && roleName != "retailer")
        {
            Roles.RemoveUserFromRole(userName, roleName);
            Roles.AddUserToRole(userName, "retailer");
        }
        else if (rblRole.SelectedValue == "sales" && roleName != "sales")
        {
            Roles.RemoveUserFromRole(userName, roleName);
            Roles.AddUserToRole(userName, "sales");
        }
        else if (rblRole.SelectedValue == "salesrep" && roleName != "salesrep")
        {
            Roles.RemoveUserFromRole(userName, roleName);
            Roles.AddUserToRole(userName, "salesrep");
        }
        else
        {
            if (roleName == "")
            {
                lblError.Text = "Error received: No Role has been selected!";
                //rgUsers.Controls.Add(new LiteralControl("Unable to update Users Record. Reason: " + ex.Message));
                //e.Canceled = true;
            }
            // DO NOTHING
        }       

        //REMOVE FROM ROLE
        //Roles.RemoveUserFromRole(userName, "Administrators");

        //CHECK TO SEE IF USER EXISTS IN 

        //if (customerID == "")
        //    {
        //        rgUsers.Controls.Add(new LiteralControl("You must fill out a CustomerID for this user!"));
        //        e.Canceled = true;
        //    }
        //else
        //{
            //UPDATE aspnet_Users_CustomerID with CustomerID and Dropship
            
            SqlCommand updateCmd = new SqlCommand("aspnet_Membership_UpdateUserCustomerIDDropship", myConnection);
            updateCmd.CommandType = CommandType.StoredProcedure;        
            updateCmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 256));
            updateCmd.Parameters["@UserName"].Value = userName;
            updateCmd.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.NVarChar, 10));
            updateCmd.Parameters["@CustomerID"].Value = customerID;
            updateCmd.Parameters.Add(new SqlParameter("@Dropship", SqlDbType.Bit));
            updateCmd.Parameters["@Dropship"].Value = dropshipper;
            updateCmd.Parameters.Add(new SqlParameter("@FreightCollect", SqlDbType.Bit));
            updateCmd.Parameters["@FreightCollect"].Value = freightcollect;
            updateCmd.Parameters.Add(new SqlParameter("@Carrier", SqlDbType.NVarChar, 50));
            updateCmd.Parameters["@Carrier"].Value = carrier.SelectedValue.ToString();
            updateCmd.Parameters.Add(new SqlParameter("@AccountNum", SqlDbType.NVarChar, 25));
            updateCmd.Parameters["@AccountNum"].Value = accountnum;
            if (myConnection.State == ConnectionState.Open)
            {

            }
            else
            {        
                updateCmd.Connection.Open();
            }

            try
            {
                int rows = 0;
                rows = updateCmd.ExecuteNonQuery();
                updateCmd.Connection.Close();
                
                //rgUsers.Controls.Add(new LiteralControl("Unable to update Users Record. Reason: " + ex.Message));
                //e.Canceled = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error received: " + ex.Message;
                //rgUsers.Controls.Add(new LiteralControl("Unable to update Users Record. Reason: " + ex.Message));
                //e.Canceled = true;
            }
        
        //}

    }

    protected void rgUsers_ItemCommand(object source, GridCommandEventArgs e)
    {
              
        if (e.CommandArgument == "Delete")
        {
            string strUser = "";
            string strEmail = "";
            string strAppID = "5C63DC26-5E44-4123-93CE-4CA4470C273D";
            
            GridDataItem dataItem = e.Item as GridDataItem;
            strUser = dataItem["UserName"].Text;
            strEmail = dataItem["Email"].Text; 

            // DELETE FROM 

            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETDB"].ToString());
            SqlCommand deleteCommand = new SqlCommand("aspnet_Users_DeleteUser", myConnection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.Add(new SqlParameter("@ApplicationName", SqlDbType.NVarChar, 256));
            deleteCommand.Parameters["@ApplicationName"].Value = strAppID;
            deleteCommand.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 256));
            deleteCommand.Parameters["@UserName"].Value = strUser;
            deleteCommand.Parameters.Add(new SqlParameter("@TablesToDeleteFrom", SqlDbType.Int));
            deleteCommand.Parameters["@TablesToDeleteFrom"].Value = 0;
            deleteCommand.Parameters.Add(new SqlParameter("@NumTablesDeletedFrom", SqlDbType.Int));
            deleteCommand.Parameters["@NumTablesDeletedFrom"].Direction = ParameterDirection.Output;
            deleteCommand.Connection.Open();

            //try
            //{
                deleteCommand.ExecuteNonQuery();
                deleteCommand.Connection.Close();                
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = "Error received: " + ex.Message;                
            //}

            // Delete shopping cart items items
            
            String deleteCmd = "DELETE FROM aspnet_Users_CustomerID WHERE USERNAME = '" + strUser + "'";
            String deleteCmd1 = "DELETE FROM aspnet_Membership WHERE USERID = (SELECT UserID FROM aspnet_Users where userName = '" + strUser + "')";
            SqlCommand myCommand1 = new SqlCommand(deleteCmd, myConnection);
            SqlCommand myCommand2 = new SqlCommand(deleteCmd1, myConnection);
            //try
            //{
                myCommand1.Connection.Open();
                myCommand1.ExecuteNonQuery();
                myCommand1.Connection.Close();
                myCommand2.Connection.Open();
                myCommand2.ExecuteNonQuery();
                myCommand2.Connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    lblError.Text = "Error received: " + ex.Message;
            //}                                   
        }

    }
}