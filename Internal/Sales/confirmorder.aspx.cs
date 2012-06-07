using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sales_confirmorder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CustomerID"] != null)
        {
            lblCustomerID.Text = Session["CustomerID"].ToString();
            
        }
        if (Session["ShoppingCart"] != null)
        {
            lblGuid.Text = Session["ShoppingCart"].ToString();            
        }
        if (Session["HeaderOrderID"] != null)
        { 
            lblHeaderOrderID.Text = Session["HeaderOrderID"].ToString();
        }
        if (Session["PONumber"] != null)
        {
            lblPONumber.Text = Session["PONumber"].ToString();
            lblPONum.Text = Session["PONumber"].ToString();
            lblConfirmPO.Text = Session["PONumber"].ToString();
        }

        BindData();
        
    }

    protected string GetOrderTotal()
    {
        string currGuid = lblGuid.Text;
        Guid newGuid = new Guid(currGuid);
        string sql = "Select ItemNum, Description, Price, Quantity, (Price * Quantity) as TotalPrice from SOPShoppingCart WHERE GUID = '" + newGuid + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        DataTable dt = new DataTable();
        da.Fill(dt);
        DataRow objDR;
        decimal decRunningTotal = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            objDR = dt.Rows[i];
            decRunningTotal += ((decimal)objDR["Price"] * (decimal)objDR["Quantity"]);
        }

        return (string)decRunningTotal.ToString();
    }

    protected void BindData()
    {
        string currGuid = lblGuid.Text;

        // Convert Guid string back to a Guid
        Guid newGuid = new Guid(currGuid);
        // Rebind to Datagrid
        string sql = "Select ItemNum, Description, Price, Quantity, (Price * Quantity) as TotalPrice from SOPShoppingCart WHERE GUID = '" + newGuid + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvOrderConfirm.DataSource = dt;
        gvOrderConfirm.DataBind();       
 
        // Get the Customer Name using the GUID

        string CustomerName;
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        String strSQL = "SELECT SOPShoppingCartHeader.Company FROM SOPShoppingCartHeader INNER JOIN SOPShoppingCartInfo ON SOPShoppingCartHeader.PONum = SOPShoppingCartInfo.CartName WHERE GUID = '" + newGuid + "'";
        SqlCommand myCommand = new SqlCommand(strSQL, myConnection);
        myCommand.Connection.Open();
        CustomerName = Convert.ToString(myCommand.ExecuteScalar());
        lblCustomer.Text = CustomerName.ToString();     
    }

    protected void gvOrderConfirm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // If we are binding the footer row, let's add in our total
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total: $" + GetOrderTotal().ToString();
        }
    }

    protected void gvOrderConfirm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Remove")
            {
                string ItemNum = Convert.ToString(e.CommandArgument);
                //RemoveFromCart(ItemNum);

                string currGuid = lblGuid.Text;

                // Convert Guid string back to a Guid
                Guid newGuid = new Guid(currGuid);
                // Insert New line item
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                String insertCmd = "delete from SOPShoppingCart where GUID = '" + currGuid + "' and ItemNum='" + ItemNum + "'";
                SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);

                myCommand.Connection.Open();
                // Test whether the new row can be added and  display the 
                // appropriate message box to the user.
                try
                {
                    myCommand.ExecuteNonQuery();
                    lblMessage.Text = "Record added ...";
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                    lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
                }
                myCommand.Connection.Close();
                // We now have to re-setup the data so that the GridView doesn't keep
                // displaying the old data
                BindData();
                lblMessage.Text = "Record removed ...";
            }
            if (e.CommandName == "Fix")
            {
                string ItemNum = Convert.ToString(e.CommandArgument);
                string Quantity = "";
                string currGuid = "";
                currGuid = lblGuid.Text;

                foreach (GridViewRow row in gvOrderConfirm.Rows)
                {
                    //if (row.FindControl("lblItemNum")).Text == ItemNum) 
                    if (gvOrderConfirm.Rows[row.RowIndex].Cells[0].Text == ItemNum)
                    {
                        Quantity = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtQuantity")).Text;
                        // Update 

                        // Convert Guid string back to a Guid
                        Guid newGuid = new Guid(currGuid);
                        // Insert New line item
                        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                        String insertCmd = "Update SOPShoppingCart set Quantity = '" + Convert.ToDecimal(Quantity) + "' where GUID = '" + newGuid + "' and ItemNum='" + ItemNum + "'";
                        SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);

                        myCommand.Connection.Open();
                        // Test whether the new row can be added and  display the 
                        // appropriate message box to the user.
                        try
                        {
                            myCommand.ExecuteNonQuery();
                            lblMessage.Text = "Record added ...";
                        }
                        catch (Exception ex)
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                            lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
                        }

                        myCommand.Connection.Close();
                        // Rebind the data
                        BindData();
                        lblMessage.Text = "Record updated ...";
                    }

                    // Do nothing
                }
            }
        }

        catch (Exception ex)
        {
            lblMessage.Text = "An error occured during the insert ..." + ex.Message;
        }

    }

    protected void btnNewOrder_Click(object sender, ImageClickEventArgs e)
    {
        // REMOVE THE CURRENT SHOPPING CART VARIABLE
        Session["ShoppingCart"] = "";
        // SEND THE USER TO THE NEW ORDER PAGE
        Response.Redirect("selectretailer.aspx");
    }

    protected void btnViewOrder_Click(object sender, ImageClickEventArgs e)
    {
        //Session["ShoppingCart"] = "";
        Response.Redirect("vieworders.aspx");
    }

    protected void btnEditOrder_Click(object sender, ImageClickEventArgs e)
    {
        string currGuid = lblGuid.Text;
        // Convert Guid string back to a Guid
        Guid newGuid = new Guid(currGuid);
        Session["ShoppingCart"] = newGuid;
        Response.Redirect("createorder.aspx");
    }

    protected void btnDeleteOrder_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            /////
            ////  PUT IN CHECKS FOR NULLS FOR THESE VARIABLES
            ////
            string currGuid = lblGuid.Text;
            string custID = lblCustomerID.Text;
            string headerOrderID = lblHeaderOrderID.Text;
            
            // Convert Guid string back to a Guid
            Guid newGuid = new Guid(currGuid);

            // Delete shopping cart items items
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
            String deleteCartInfoCmd = "DELETE from SOPShoppingCartInfo WHERE GUID = '" + newGuid + "'";
            String deleteCartItemsCmd = "DELETE from SOPShoppingCart WHERE GUID = '" + newGuid + "'";
            String deleteHeaderCmd = "UPDATE SOPShoppingCartHeader SET Status = 'D' WHERE OrderID = '" + headerOrderID + "'";
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
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
        }
        
    }

    protected void btnSubmitOrder_Click(object sender, ImageClickEventArgs e)
    {
        
        int PDFRefNum = 0;
        bool errFlag = false;
        string tmpItemNum = "";
        string tmpQuantity = "";
        decimal tmpTotal = 0;
        string currGuid = lblGuid.Text;       
        string headerOrderID = lblHeaderOrderID.Text;
        Guid newGuid = new Guid(currGuid);        

        try
        {
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
            SqlCommand myCommand = new SqlCommand("SOP_InsertPDFOrderFromSOPShoppingCartHeader", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.Add("@OrderID", SqlDbType.Int);
            myCommand.Parameters["@OrderID"].Value = headerOrderID;
            myCommand.Connection.Open();
            PDFRefNum = Convert.ToInt32(myCommand.ExecuteScalar());
            lblMessage.Text = "Value Returned was " + PDFRefNum + "! ";
            if (PDFRefNum == 2)
            {
                errFlag = true;
            }            
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Value Returned was " + PDFRefNum + "! " + ex.Message;
        }

        if (errFlag == true)
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error(1) occured and the following value was returned " + PDFRefNum + "!";
        }
        else
        {
            try
            {
                // GO GET THE DATA FROM THE SHOPPINGHEADER TABLE USING THE CURRENT ORDERID        
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                SqlCommand mycmd = new SqlCommand("SOP_GetShoppingCartItemsByGuid", conn);
                mycmd.CommandType = CommandType.StoredProcedure;
                mycmd.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier);
                mycmd.Parameters["@GUID"].Value = newGuid;
                SqlDataAdapter da = new SqlDataAdapter(mycmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // ESTABLISH A SQL CONNECTION
                SqlConnection econn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                econn.Open();

                // ESTABLISH A SQL COMMAND AND COMMAND TYPE
                SqlCommand ecmd = new SqlCommand("SOP_InsertPDFDetailsPerNode", econn);
                ecmd.CommandType = CommandType.StoredProcedure;

                ecmd.Parameters.Add("@PDFRefNum", SqlDbType.Int);
                ecmd.Parameters.Add("@ItemNum", SqlDbType.VarChar);
                ecmd.Parameters.Add("@NumQty", SqlDbType.VarChar);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    object temp;
                    // GET SINGLE NODE VALUES
                    tmpItemNum = dt.Rows[i]["ItemNum"].ToString();
                    tmpQuantity = dt.Rows[i]["Quantity"].ToString();
                    tmpTotal += Convert.ToDecimal(dt.Rows[i]["LineTotal"].ToString());

                    if (tmpItemNum != "")
                    {
                        // TEST QUANTITY VALUE
                        if ((tmpQuantity == "") || (tmpQuantity == "0") || (tmpQuantity == "0.00000"))
                        // IF BLANK OR ZERO SET i = NODECOUNT AND FLAG AS AN ERROR AND REMOVE ALL RECORDS INSERTED SO FAR
                        {
                            //i = m_DetailCount;
                            errFlag = true;
                            lblErrorMessage.Text = "The error(2) occurred during details insertion. An item selected had no quantity assigned to it. Please edit the quantities by selecting edit cart and resubmit the order with quantities assigned for each item selected.";

                            try
                            {
                                // GET RID OF VALUES
                                // ESTABLISH A SQL CONNECTION
                                SqlConnection xconn = new SqlConnection(ConfigurationManager.AppSettings["SqlConnectionString"]);
                                xconn.Open();
                                // ESTABLISH A SQL COMMAND AND COMMAND TYPE
                                SqlCommand xcmd = new SqlCommand("SOP_DeletePDFOrder", xconn);
                                xcmd.CommandType = CommandType.StoredProcedure;

                                xcmd.Parameters.Add("@PDFRefNum", SqlDbType.Int);
                                xcmd.Parameters["@PDFRefNum"].Value = PDFRefNum;

                                xcmd.ExecuteNonQuery();
                                xconn.Close();
                                xconn.Dispose();
                            }
                            catch (Exception ex)
                            {
                                lblErrorMessage.Text = "The error(3) occurred. Message retruned: " + ex.Message.ToString();  //UNCOMMENT TO GET ERROR FROM SQL

                            }
                        }
                        else
                        {
                            try
                            {
                                ecmd.Parameters["@PDFRefNum"].Value = PDFRefNum;
                                ecmd.Parameters["@ItemNum"].Value = tmpItemNum;
                                ecmd.Parameters["@NumQty"].Value = tmpQuantity;
                                temp = ecmd.ExecuteScalar();
                            }
                            catch
                            {
                                errFlag = true;
                                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                                lblMessage.Font.Bold = true;
                                lblErrorMessage.Text = "An error(4) occured. Please try to resubmit your order.";

                            }
                        }
                    }
                    
                }
                econn.Close();
                econn.Dispose();
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = ex.Message;
            }

            // FINALLY UPDATE THE ORDER TOTAL COLUMN IN THE SOPPDFHEADER TABLE   
            try
            {
                // Delete shopping cart items items
                SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                String updateCmd = "UPDATE SOPPDFHeader SET OrderTotal = '" + tmpTotal + "' WHERE PDFRefNum = '" + PDFRefNum + "'";
                SqlCommand myCommand1 = new SqlCommand(updateCmd, myConn);
                myCommand1.Connection.Open();
                myCommand1.ExecuteNonQuery();
                myCommand1.Connection.Close();


                if (errFlag == false)
                {
                    // SET SHOPPING CART TO INACTIVE
                    SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                    SqlCommand myCommand = new SqlCommand("SOP_UpdateSOPShoppingCartInfo", myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@GUID", SqlDbType.UniqueIdentifier));
                    myCommand.Parameters["@GUID"].Value = newGuid;
                    myCommand.Connection.Open();

                    // Test whether the new row can be added and  display the 
                    // appropriate message box to the user.
                    try
                    {
                        myCommand.ExecuteNonQuery();                      
                    }
                    catch (Exception ex)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                        lblMessage.Font.Bold = true;
                        lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
                    }
                                                            
                    // SET SESSION VARIABLES FOR THIS ORDER TO NOTHING
                    Session["ShoppingCart"] = "";
                    Session["HeaderOrderID"] = "";
                    Session["PONumber"] = "";
                    Response.Redirect("vieworders.aspx");
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                    lblMessage.Font.Bold = true;
                    lblMessage.Text = "An error(5) occured the following value was returned " + PDFRefNum + "!";
                }

            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblErrorMessage.Text = ex.Message.ToString();  //UNCOMMENT TO GET ERROR FROM SQL
            }
        }
           
    }
}
