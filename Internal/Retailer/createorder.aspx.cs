using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class retailers_createorder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string currGuid = "";
        lblCustomerID.Text = Session["CustomerID"].ToString();             

        if (!IsPostBack)
        {
            txtQty.Focus();
            lblDropShip.Text = isDropShip().ToString();

            if (HttpContext.Current.Session["ShoppingCart"] != null)
            {
                if (HttpContext.Current.Session["ShoppingCart"].ToString() == "")
                {
                    currGuid = makeCart();
                    lblGuid.Text = currGuid.ToString();
                }
                else
                {                    
                    currGuid = Session["ShoppingCart"].ToString();
                    lblGuid.Text = currGuid.ToString();
                    BindData();                  
                }
            }
            else
            {
                currGuid = makeCart();
                lblGuid.Text = currGuid.ToString();                
            }

        }
        else
        {   
            lblGuid.Text = HttpContext.Current.Session["ShoppingCart"].ToString();
            lblDropShip.Text = isDropShip().ToString();
        }

        if (HttpContext.Current.Session["HeaderOrderID"] != null)
        {
            lblHeaderOrderID.Text = Session["HeaderOrderID"].ToString();
            lblDropShip.Text = isDropShip().ToString();
        }

        if (HttpContext.Current.Session["PONumber"] != null)
        {
            lblPONumber.Text = Session["PONumber"].ToString();
        }
    }

    protected bool isDropShip()
    {
        bool dropShip = false;
        int strHeaderID = System.Convert.ToInt32(Session["HeaderOrderID"].ToString());
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        SqlCommand myCommand = new SqlCommand("SOP_GetDropShipFromSOPShoppingCartHeader", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.Int));
        myCommand.Parameters["@OrderID"].Value = strHeaderID;     
        myCommand.Connection.Open();

        try
        {
            myCommand.ExecuteNonQuery();
            dropShip = System.Convert.ToBoolean(myCommand.ExecuteScalar());
        }
        catch(Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
        }
        finally
        {
            myConnection.Close();
        }

        return dropShip;
    }

    protected string makeCart()
    {
        string currGuid;
        // If the cart is not in the session, create one and put it there
        // Otherwise, get it from the session
        if (HttpContext.Current.Session["ShoppingCart"] == null)
        {
            // Create new Guid and convert to a string  
            currGuid = System.Guid.NewGuid().ToString();
            HttpContext.Current.Session["ShoppingCart"] = currGuid;
        }
        else
        {
            // Get current Guid
            currGuid = (string)HttpContext.Current.Session["ShoppingCart"];
        }

        return currGuid;
    }

    protected void AddToCart()
    {             
        string currGuid = lblGuid.Text;

        //if lblDropShip.Text = "Tr

        // Convert Guid string back to a Guid
        Guid newGuid = new Guid(currGuid);
        // Insert New line item
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        String insertCmd = "insert into SOPShoppingCart values (@GUID, @ItemNum, @Description, @Price, @Quantity)";
        SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);
        myCommand.Parameters.Add(new SqlParameter("@GUID", SqlDbType.UniqueIdentifier));
        myCommand.Parameters["@GUID"].Value = newGuid;
        myCommand.Parameters.Add(new SqlParameter("@ItemNum", SqlDbType.VarChar, 20));
        myCommand.Parameters["@ItemNum"].Value = txtItemNum.Text;
        myCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100));
        myCommand.Parameters["@Description"].Value = txtDescription.Text;
        myCommand.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal));
        myCommand.Parameters["@Price"].Value = Convert.ToDecimal(lblPrice.Text);
        myCommand.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Decimal));
        myCommand.Parameters["@Quantity"].Value = Convert.ToDecimal(txtQty.Text);

        myCommand.Connection.Open();
        // Test whether the new row can be added and  display the 
        // appropriate message box to the user.
        try
        {
            myCommand.ExecuteNonQuery();

            if (lblDropShip.Text == "True")
            {
                try
                {
                    UpdateDropShipTotals();
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                    lblMessage.Font.Bold = true;
                    lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
                }

            }
            else
            {
                lblMessage.Text = "Record added ...";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                lblMessage.Font.Bold = false;
            }            
            
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
        }
        myCommand.Connection.Close();

        // Rebind to Datagrid
        string sql = "Select ItemNum, Description, Price, Quantity, (Price * Quantity) as TotalPrice from SOPShoppingCart WHERE GUID = '" + newGuid + "'";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvShoppingCart.DataSource = dt;
        gvShoppingCart.DataBind();

        // Clear values
        txtItemNum.Text = "";
        txtItemNum.ReadOnly = false;
        txtItemNum.Focus();
        txtDescription.Text = "";
        txtDescription.ReadOnly = false;
        txtQty.Text = "";
        lblPrice.Text = "";
    }

    protected void UpdateDropShipTotals()
    {
        string currGuid = lblGuid.Text;
        // Convert Guid string back to a Guid
        Guid newGuid = new Guid(currGuid);
        // Insert New line item
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());

        try
        {
            // INSERT/UPDATE DS-100 and DS-200
            SqlCommand myCmd2 = new SqlCommand("SOP_UpdateDropShipItems", myConnection);
            myCmd2.CommandType = CommandType.StoredProcedure;
            myCmd2.Parameters.Add(new SqlParameter("@GUID", SqlDbType.UniqueIdentifier));
            myCmd2.Parameters["@GUID"].Value = newGuid;
            myCmd2.Connection.Open();
            myCmd2.ExecuteNonQuery();
                
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
        }
        finally
        {
            myConnection.Close();
            myConnection.Dispose();
        }

        
    }

    protected string GetOrderTotal()
    {
        string currGuid = lblGuid.Text;
        string sql = "Select ItemNum, Description, Price, Quantity, (Price * Quantity) as TotalPrice from SOPShoppingCart WHERE GUID = '" + currGuid + "'";
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

    protected void txtItemNum_TextChanged(object sender, EventArgs e)
    {
        if (txtItemNum.Text != "")
        {
            try
            {
                string strSQL = "SELECT  RTRIM(wsPF_ProductExtraInfo.ProductID) as ProductID, RTRIM(wsPF_ProductExtraInfo.Name) as Name, CAST(RTRIM(IV00108.UOMPRICE) as Decimal(10,2)) as Price";
                strSQL = strSQL + " FROM  IV00108 INNER JOIN wsPF_ProductExtraInfo ON IV00108.ITEMNMBR = wsPF_ProductExtraInfo.ProductID";
                strSQL = strSQL + " WHERE     (wsPF_ProductExtraInfo.ProductID = '" + txtItemNum.Text.ToString() + "') and DisplayOnOrderForm = '1' and";
                strSQL = strSQL + " IV00108.PRCLEVEL = 'WHOLE'";

                SqlDataAdapter da = new SqlDataAdapter(strSQL, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                txtItemNum.Text = dt.Rows[0]["ProductID"].ToString();
                txtDescription.Text = dt.Rows[0]["Name"].ToString();
                lblPrice.Text = dt.Rows[0]["Price"].ToString();
                txtDescription.ReadOnly = true;
                txtQty.Focus();
            }
            catch
            {
                txtItemNum.Text = "";
                txtItemNum.ReadOnly = false;
                txtDescription.ReadOnly = false;
            }
        }
    }

    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {
        if (txtDescription.Text != "")
        {
            try
            {
                string strSQL = "SELECT  RTRIM(wsPF_ProductExtraInfo.ProductID) as ProductID, RTRIM(wsPF_ProductExtraInfo.Name) as Name, CAST(RTRIM(IV00108.UOMPRICE) as Decimal(10,2)) as Price";
                strSQL = strSQL + " FROM  IV00108 INNER JOIN wsPF_ProductExtraInfo ON IV00108.ITEMNMBR = wsPF_ProductExtraInfo.ProductID";
                strSQL = strSQL + " WHERE     (wsPF_ProductExtraInfo.Name = '" + txtDescription.Text.ToString() + "') and DisplayOnOrderForm = '1' and";
                strSQL = strSQL + " IV00108.PRCLEVEL = 'WHOLE'";

                SqlDataAdapter da = new SqlDataAdapter(strSQL, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                DataTable dt = new DataTable();
                da.Fill(dt);
                txtItemNum.Text = dt.Rows[0]["ProductID"].ToString();
                txtDescription.Text = dt.Rows[0]["Name"].ToString();
                lblPrice.Text = dt.Rows[0]["Price"].ToString();
                txtItemNum.ReadOnly = true;
                txtQty.Focus();
            }
            catch
            {
                txtItemNum.Text = "";
                txtItemNum.ReadOnly = false;
                txtDescription.ReadOnly = false;
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtItemNum.Text = "";
        txtItemNum.ReadOnly = false;
        txtItemNum.Focus();
        txtDescription.Text = "";
        txtDescription.ReadOnly = false;
        txtQty.Text = "";
        lblPrice.Text = "";
    }

    protected void btnAddToCart_Click(object sender, ImageClickEventArgs e)
    {
        // Hide Panel if visible
        //pnlPopup.Visible = false;
        string goNoGo = "go";
        string itemNum = "";
        //int itemNumIndex = 0;
        string tmpItemNum = "";
        //int tmpItemNumIndex = 0;

        if (txtQty.Text == "")
        {
            // Make Panel Visible
            //pnlPopup.Visible = true;
            // Give Error Message
            lblMessage.Text = "You need to enter a quantity! Please enter a quantity and try again.";
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            //lblDupes.Text = "You need to enter a quantity! Please enter a quantity and try again.";
            goNoGo = "nogo";
            txtQty.Focus();
        }
        if (txtQty.Text == null)
        {
            // Make Panel Visible
            //pnlPopup.Visible = true;
            // Give Error Message
            lblMessage.Text = "You need to enter a quantity! Please enter a quantity and try again.";
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            //lblDupes.Text = "You need to enter a quantity! Please enter a quantity and try again.";
            goNoGo = "nogo";
            txtQty.Focus();
        }
        if (txtQty.Text == "0")
        {
            // Make Panel Visible
            //pnlPopup.Visible = true;
            // Give Error Message
            lblMessage.Text = "You may not enter zero as a quantity! Please enter a quantity and try again.";
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            //lblDupes.Text = "You need to enter a quantity! Please enter a quantity and try again.";
            goNoGo = "nogo";
            txtQty.Focus();
        }
        else
        {

            foreach (GridViewRow row in gvShoppingCart.Rows)
            {
                tmpItemNum = gvShoppingCart.Rows[row.RowIndex].Cells[0].Text;

                itemNum = txtItemNum.Text;


                if (tmpItemNum == itemNum)
                {
                    // Make Panel Visible
                    pnlPopup.Visible = true;
                    // Give Error Message
                    lblMessage.Text = "You have already added this item! You may update the quantity in the cart below.";
                    lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                    lblMessage.Font.Bold = true;
                    goNoGo = "nogo";
                    txtItemNum.Text = "";
                    txtItemNum.ReadOnly = false;
                    txtItemNum.Focus();
                    txtDescription.Text = "";
                    txtDescription.ReadOnly = false;
                    txtQty.Text = "";
                    lblPrice.Text = "";
                }
            }

            if (goNoGo == "go")
            {
                AddToCart();
            }
        }
        //AddToCart();

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

        gvShoppingCart.DataSource = dt;
        gvShoppingCart.DataBind();

        // Clear values
        txtItemNum.Text = "";
        txtItemNum.ReadOnly = false;
        txtItemNum.Focus();
        txtDescription.Text = "";
        txtDescription.ReadOnly = false;
        txtQty.Text = "";
        lblPrice.Text = "";

    }

    protected void gvShoppingCart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // If we are binding the footer row, let's add in our total
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total: $" + GetOrderTotal().ToString();
        }
    }

    protected void gvShoppingCart_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    if (lblDropShip.Text == "True")
                    {
                        try
                        {
                            UpdateDropShipTotals();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                            lblMessage.Font.Bold = true;
                            lblMessage.Text = "An error occured removing that item please try again ..." + ex.Message;
                        }

                    }
                    else
                    {
                        lblMessage.Text = "Record removed ...";
                        lblMessage.ForeColor = System.Drawing.Color.Black;
                        lblMessage.Font.Bold = false;
                    }    
                    
                }
                catch (Exception ex)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                    lblMessage.Text = "An error occured removing that item please try again ..." + ex.Message;
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

                foreach (GridViewRow row in gvShoppingCart.Rows)
                {
                    //if (row.FindControl("lblItemNum")).Text == ItemNum) 
                    if (gvShoppingCart.Rows[row.RowIndex].Cells[0].Text == ItemNum)
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

                            if (lblDropShip.Text == "True")
                            {
                                try
                                {
                                    UpdateDropShipTotals();
                                }
                                catch (Exception ex)
                                {
                                    lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                                    lblMessage.Font.Bold = true;
                                    lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
                                }

                            }
                            else
                            {
                                lblMessage.Text = "Record added ...";
                                lblMessage.ForeColor = System.Drawing.Color.Black;
                                lblMessage.Font.Bold = false;
                            }    

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
    
    protected void btnSaveOrder_Click(object sender, ImageClickEventArgs e)
    {
        string currGuid = lblGuid.Text;
        string customerID = Session["CustomerID"].ToString();
        string PONum = Session["PONumber"].ToString();

        // Convert Guid string back to a Guid  [SOP_InsertSOPShoppingCartInfo]
        Guid newGuid = new Guid(currGuid);
        // Insert New line item
        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        SqlCommand myCommand = new SqlCommand("SOP_InsertSOPShoppingCartInfo", myConnection);
        myCommand.CommandType = CommandType.StoredProcedure;
        myCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.VarChar, 10));
        myCommand.Parameters["@CustomerID"].Value = customerID;
        myCommand.Parameters.Add(new SqlParameter("@GUID", SqlDbType.UniqueIdentifier));
        myCommand.Parameters["@GUID"].Value = newGuid;
        myCommand.Parameters.Add(new SqlParameter("@CartName", SqlDbType.VarChar, 50));
        myCommand.Parameters["@CartName"].Value = PONum;
        myCommand.Parameters.Add(new SqlParameter("@Updated", SqlDbType.DateTime));  
        myCommand.Parameters["@Updated"].Value = DateTime.Now.Date;
        myCommand.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit));
        myCommand.Parameters["@Active"].Value = true;

        myCommand.Connection.Open();

        // Test whether the new row can be added and  display the 
        // appropriate message box to the user.
        try
        {
            myCommand.ExecuteNonQuery();
            lblMessage.Text = "Order has been saved ...";
            lblMessage.ForeColor = System.Drawing.Color.Black;
            lblMessage.Font.Bold = false;
            Response.Redirect("confirmorder.aspx");
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error occured inserting that item please try again ..." + ex.Message;
        }
        myCommand.Connection.Close();
    }

    protected void btnDeleteCart_Click(object sender, ImageClickEventArgs e)
    {
        // DELETE CURRENT SHOPPING CART
        if (HttpContext.Current.Session["ShoppingCart"] != null)
        {
            try
            {
                //currGuid = HttpContext.Current.Session["ShoppingCart"].ToString();
                string deleteGuid = Session["ShoppingCart"].ToString();
                
                // Convert Guid string back to a Guid
                Guid newGuid = new Guid(deleteGuid);
                // Insert New line item
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                String insertCmd = "DELETE from  SOPShoppingCart WHERE GUID = @GUID";
                SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);
                myCommand.Parameters.Add(new SqlParameter("@GUID", SqlDbType.UniqueIdentifier));
                myCommand.Parameters["@GUID"].Value = newGuid;

                Response.Redirect("vieworders.aspx");
            }
            catch(Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "An error occured deleting this cart..." + ex.Message;
            }
            
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
            lblMessage.Font.Bold = true;
            lblMessage.Text = "An error occured deleting this cart please try again. If the issue persists create a new order ...";
        }
        
    }

    protected void btnNewCart_Click(object sender, ImageClickEventArgs e)
    {
        makeCart();
    }
}
