using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShippingRates.RateShop;

public partial class retailers_confirmorder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string freightCollect = "0";
        string carrier = "";
        string accountNum = "";

        if (HttpContext.Current.Session["CustomerID"] != null)
        {
            lblCustomerID.Text = HttpContext.Current.Session["CustomerID"].ToString();
            
           
            // FREIGHT COLLECT STUFF
            SqlConnection myConnection2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ASPNETDB"].ToString());
            String getFreightCollectCmd = "SELECT RTRIM(FreightCollect) as FreightCollect, RTRIM(Carrier) as Carrier, RTRIM(Account#) as Account# FROM aspnet_Users_CustomerID where CustomerID = '" + lblCustomerID.Text + "' order by FreightCollect Desc";
            SqlDataAdapter da = new SqlDataAdapter(getFreightCollectCmd, myConnection2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            freightCollect = dt.Rows[0]["FreightCollect"].ToString();
            carrier = dt.Rows[0]["Carrier"].ToString();
            accountNum = dt.Rows[0]["Account#"].ToString();

            lblFreightCollect.Text = freightCollect;
            lblCarrier.Text = carrier;
            lblAccountNum.Text = accountNum;
            

           
        }
        if (HttpContext.Current.Session["ShoppingCart"] != null)
        {
            lblGuid.Text = HttpContext.Current.Session["ShoppingCart"].ToString();
        }
        if (HttpContext.Current.Session["HeaderOrderID"] != null)
        { 
            lblHeaderOrderID.Text = HttpContext.Current.Session["HeaderOrderID"].ToString();

            int isDropShip = 0;            

            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
            String getDropShipCmd = "SELECT Dropship FROM SOPShoppingCartHeader where OrderID = '" + lblHeaderOrderID.Text + "'";
            SqlCommand myCommand = new SqlCommand(getDropShipCmd, myConnection);

            myCommand.Connection.Open();
            isDropShip = Convert.ToInt32(myCommand.ExecuteScalar());
            myCommand.Connection.Close();
            myCommand.Connection.Dispose();

            if (isDropShip == 1)
            {
                pnlShipping.Visible = true;
                lblDropship.Text = "1";
            }             

        }
        if (HttpContext.Current.Session["PONumber"] != null)
        {
            lblPONumber.Text = HttpContext.Current.Session["PONumber"].ToString();
            lblPONum.Text = HttpContext.Current.Session["PONumber"].ToString();
            lblConfirmPO.Text = HttpContext.Current.Session["PONumber"].ToString();
        }
                     
        BindData();

        if (!Page.IsPostBack)
        {
            if (freightCollect == "0")
            {
                Guid currGuid = new Guid(lblGuid.Text);          
                getRates(lblHeaderOrderID.Text, currGuid);
                string shippingCosts = lblReturnString.Text;
                string[] split = shippingCosts.Split('/');
                lbl1DayCost.Text = "$" + split[0].ToString();
                lbl1DayTime.Text = split[1].ToString();
                lbl2DayCost.Text = "$" + split[2].ToString();
                lbl2DayTime.Text = split[3].ToString();
                lbl3DayCost.Text = "$" + split[4].ToString();
                lbl3DayTime.Text = split[5].ToString();
                lblGrndCost.Text = "$" + split[6].ToString();
                lblGrndTime.Text = split[7].ToString();
            }
            if (freightCollect == "1")
            {
                Guid currGuid = new Guid(lblGuid.Text);          
                getRates(lblHeaderOrderID.Text, currGuid);
                string shippingCosts = lblReturnString.Text;
                string[] split = shippingCosts.Split('/');
                lbl1DayCost.Text = "**";
                lbl1DayTime.Text = split[1].ToString();
                lbl2DayCost.Text = "**";
                lbl2DayTime.Text = split[3].ToString();
                lbl3DayCost.Text = "**";
                lbl3DayTime.Text = split[5].ToString();
                lblGrndCost.Text = "**";
                lblGrndTime.Text = split[7].ToString();

                lblFreightCollectAccount.Text = "** Will be shipped freight collect via " + carrier + " - " + accountNum;
                lblFreightCollectAccount.Visible = true;
            }
            
        }
        else
        {
            this.lblErrorMessage.Text = "Error!";
        }

        
    }

    protected void getRates(string orderID, Guid GUID)
    {
        ShippingRates.ups_GPOrder RateShop = new ShippingRates.ups_GPOrder();
        RateShop.estimateFreight(orderID, GUID);

        if (RateShop.RatesResultCode == "0")
        {            
            this.lblReturnString.Text = RateShop.getRates();
        }
        else
        {            
            this.lblReturnString.Text = RateShop.getRates();
        }
    }

    private double calcTotalPallets(string strShipmentCube)
    {
        int palletCube = 60;
        double temp = Convert.ToDouble(strShipmentCube);

        return Math.Ceiling(temp / palletCube);
    }

    private double calcTotalWght(string strShipmentWght, string strShipmentCube)
    {
        int palletWght = 45;
        double temp = Convert.ToDouble(strShipmentWght);

        return Math.Round(temp + (palletWght * calcTotalPallets(strShipmentCube)), 1);
    }

    private string calcFrghtClass(ShippingRates.FreightClassDetail[] arrFreightClasses)
    {
        string result = "";

        result = "<table width=\"200\"  border=\"0\" cellspacing=\"0\" cellpadding=\"1\">";
        result = result + "<tr>";
        result = result + "<td><u>Class</u></td>";
        result = result + "<td align=\"right\"><u>Weight</u></td>";
        result = result + "<td align=\"right\"><u>Cube</u></td>";
        result = result + "</tr>";

        // Create table rows
        foreach (ShippingRates.FreightClassDetail fc in arrFreightClasses)
        {
            result = result + "<tr>";
            result = result + "<td nowrap>" + fc.freightClass + "</td>";
            result = result + "<td nowrap align=\"right\">" + Math.Round(fc.weight, 1) + "&nbsp;lbs. </td>";
            result = result + "<td nowrap align=\"right\">" + Math.Round(fc.cube, 1) + "&nbsp;ft^3</td>";
            result = result + "</tr>";
        }

        result = result + "</table>";

        return result;
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
        Response.Redirect("orderinfo.aspx");
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
        string dropShip = lblDropship.Text;
        
        if (dropShip == "1")
        {
            if (chkb1Day.Checked == true && (chkb2Day.Checked == true || chkb3Day.Checked == true || chkbGrnd.Checked == true))
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "You may only select one shipping method!";
            }
            else if (chkb2Day.Checked == true && (chkb1Day.Checked == true || chkb3Day.Checked == true || chkbGrnd.Checked == true))
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "You may only select one shipping method!";
            }
            else if (chkb3Day.Checked == true && (chkb1Day.Checked == true || chkb2Day.Checked == true || chkbGrnd.Checked == true))
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "You may only select one shipping method!";
            }
            else if (chkbGrnd.Checked == true && (chkb1Day.Checked == true || chkb2Day.Checked == true || chkb3Day.Checked == true))
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "You may only select one shipping method!";
            }
            else if (chkbGrnd.Checked == false && chkb1Day.Checked == false && chkb2Day.Checked == false && chkb3Day.Checked == false)
            {
                lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                lblMessage.Font.Bold = true;
                lblMessage.Text = "You MUST select a shipping method!";
            }
            else
            {
                // GO AHEAD
                SubmitOrder();
            }            
        }
        else
        {
            SubmitOrder();
        }                   
    }

    protected void SubmitOrder()
    {
        int PDFRefNum = 0;
        bool errFlag = false;
        string tmpItemNum = "";
        string tmpQuantity = "";
        string dropShip = lblDropship.Text;
        decimal tmpTotal = 0;
        string currGuid = lblGuid.Text;
        string headerOrderID = lblHeaderOrderID.Text;
        string shipMethod = "";
        Guid newGuid = new Guid(currGuid);

        if ((lblDropship.Text == "1") && (lblFreightCollect.Text == "0"))
        {
            if (chkb1Day.Checked == true)
            {
                shipMethod = "UPS1DAY/" + lbl1DayCost.Text;
                //tmpTotal = Convert.ToDecimal(lbl1DayCost.Text);
            }
            else if (chkb2Day.Checked == true)
            {
                shipMethod = "UPS2DAY/" + lbl2DayCost.Text;
                //tmpTotal = Convert.ToDecimal(lbl2DayCost.Text);
            }
            else if (chkb3Day.Checked == true)
            {
                shipMethod = "UPS3DAY/" + lbl3DayCost.Text;
                //tmpTotal = Convert.ToDecimal(lbl3DayCost.Text);
            }
            else if (chkbGrnd.Checked == true)
            {
                shipMethod = "UPSGRND/" + lblGrndCost.Text;
                //tmpTotal = Convert.ToDecimal(lblGrndCost.Text);
            }
            else
            {
                shipMethod = "ERROR";
            }
                                    
        }
        // FREIGHT COLLECT USED
        if ((lblDropship.Text == "1") && (lblFreightCollect.Text == "1"))
        {
            if ((chkb1Day.Checked == true) && (lblCarrier.Text == "Fedex"))
            {
                shipMethod = "FEDEX1DAY-FC/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lbl1DayCost.Text);
            }
            else if ((chkb1Day.Checked == true) && (lblCarrier.Text == "UPS"))
            {
                shipMethod = "UPS1DAY-COLLECT/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lbl2DayCost.Text);
            }
            else if ((chkb2Day.Checked == true) && (lblCarrier.Text == "Fedex"))
            {
                shipMethod = "FEDEX2DAY-FC/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lbl2DayCost.Text);
            }
            else if ((chkb2Day.Checked == true) && (lblCarrier.Text == "UPS"))
            {
                shipMethod = "UPS2DAY-COLLECT/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lbl2DayCost.Text);
            }
            else if ((chkb3Day.Checked == true) && (lblCarrier.Text == "Fedex"))
            {
                shipMethod = "FEDEX3DAY-FC/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lbl3DayCost.Text);
            }
            else if ((chkb3Day.Checked == true) && (lblCarrier.Text == "UPS"))
            {
                shipMethod = "UPS3DAY-COLLECT/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lbl3DayCost.Text);
            }
            else if ((chkbGrnd.Checked == true) && (lblCarrier.Text == "Fedex"))
            {
                shipMethod = "FEDEXGRND-FC/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lblGrndCost.Text);
            }
            else if ((chkbGrnd.Checked == true) && (lblCarrier.Text == "UPS"))
            {
                shipMethod = "UPSGRND-COLLECT/#" + lblAccountNum.Text;
                //tmpTotal = Convert.ToDecimal(lblGrndCost.Text);
            }
            else
            {
                shipMethod = "ERROR";
            }

        }


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
                String updateCmd = "UPDATE SOPPDFHeader SET OrderTotal = '" + tmpTotal + "', ShippingMethod = '" + shipMethod + "' WHERE PDFRefNum = '" + PDFRefNum + "'";
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
                    finally
                    {
                        myCommand.Connection.Close();
                    }
                    
                    if (lblDropship.Text == "1")
                    {
                        // SELECT ADDRESS ITEMS FROM ORDER AND INSERT IT INTO GREAT PLAINS
                        // SET SHOPPING CART TO INACTIVE
                        SqlConnection myConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                        SqlCommand myCommand2 = new SqlCommand("SOP_InsertDropshipAddressIntoGP", myConnection1);
                        myCommand2.CommandType = CommandType.StoredProcedure;
                        myCommand2.Parameters.Add(new SqlParameter("@PDFRefNum", SqlDbType.Int));
                        myCommand2.Parameters["@PDFRefNum"].Value = PDFRefNum;
                        myCommand2.Connection.Open();

                        // Test whether the new row can be added and  display the 
                        // appropriate message box to the user.
                        try
                        {
                            myCommand2.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Firebrick;
                            lblMessage.Font.Bold = true;
                            lblMessage.Text = "An error occured during submission of dropshipping address ..." + ex.Message;
                        }
                        finally
                        {
                            myCommand2.Connection.Close();
                        }

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
