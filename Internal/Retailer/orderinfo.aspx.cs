using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class retailers_orderinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dropShip = Session["Dropship"].ToString();
        //Session["ShoppingCart"] = null;
        if (dropShip == "True")
        {
            pnlDropShipHeader.Visible = true;
            ckbDropShip.Visible = true;
        }
        else
        {
            pnlDropShipHeader.Visible = true;
            ckbDropShip.Visible = false;
        }

        lblErrorMsg.Visible = false;

        if (!Page.IsPostBack)
        {
            if (Session["CustomerID"].ToString() != null)
            {
                Session["ShoppingCart"] = null;
            }
            else if (Session["CustomerID"].ToString() == null)
            {
                Response.Redirect("~/internal/security/login.aspx");
            }
            else
            {
                Response.Redirect("~/internal/security/login.aspx");
            }
        }

    }

    protected void gvShippingAddress_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "CreateOrder")
        {
            // Convert the row index stored in the CommandArgument
            // property to an Integer.
            int index = Convert.ToInt32(e.CommandArgument);
            string goNoGo = "go";

            // Check to see if dates have been selected
            //if (calOrderDate.SelectedDate.Ticks.ToString() == "0")
            // {
            //    goNoGo = "nogo";
            //    lblErrorMsg.Text = "You must select an order date.";
            //    lblErrorMsg.Visible = true;
            //  lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
            //    lblErrorMsg.Font.Bold = true;
            //   }
            // else if (calShipDate.SelectedDate.Ticks.ToString() == "0")
            if (calShipDate.SelectedDate.Ticks.ToString() == "0")
            {
                goNoGo = "nogo";
                lblErrorMsg.Text = "You must select an ship date.";
                lblErrorMsg.Visible = true;
                lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
                lblErrorMsg.Font.Bold = true;
            }
            //else if (calCancelDate.SelectedDate.Ticks.ToString() == "0")
            //{
            //    goNoGo = "nogo";
            //    lblErrorMsg.Text = "You must select an Cancel date.";
            //    lblErrorMsg.Visible = true;
            //    lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
            //    lblErrorMsg.Font.Bold = true;
            //}
            else if (txtPONum.Text == "")
            {
                goNoGo = "nogo";
                lblErrorMsg.Text = "Each order must have a PONumber!";
                lblErrorMsg.Visible = true;
                lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
                lblErrorMsg.Font.Bold = true;
                txtPONum.Focus();
            }

            DateTime FormCreationDate = DateTime.Now;
            string FormSpecialType = lblFormSpecialType.Text;
            string CustomerNum = Session["CustomerID"].ToString();
            string PONum = txtPONum.Text;
            DateTime OrderDate = DateTime.Now;
            // DateTime OrderDate = calOrderDate.SelectedDate;
            DateTime ShipDate = calShipDate.SelectedDate;
            DateTime CancelDate = calShipDate.SelectedDate;
            string Comments = txtComments.Text;
            decimal OrderTotal = 0;
            string Status = "N";
            string OrderedBy = "";
            string Company = "";
            string Phone = "";
            string Fax = "";
            // Status 'N' = New, 'S' = Processed, 'C' = Confirmed, 'E' = Error
            // Move through Formview looking at zero based row results
            //Label LabelContact = (Label)fvRetailer.FindControl("lblContact");
            // Label LabelCompany = (Label)fvRetailer.FindControl("lblCompany");
            // Label LabelPhone = (Label)fvRetailer.FindControl("lblPhone");
            //Label LabelFax = (Label)fvRetailer.FindControl("lblFax");
            //if (LabelContact != null)
            //{
            //    if (txtOrderedBy.Text == "")
            //    {
            //        OrderedBy = LabelContact.Text;
            //    }
            //    else
            //    {
            //        OrderedBy = txtOrderedBy.Text;
            //    }
            //}
            //if (LabelCompany != null)
            //{
            //    Company = LabelCompany.Text;
            //}
            //if (LabelPhone != null)
            //{
            //    Phone = LabelPhone.Text;
            //    Phone = Phone.Replace("(", "");
            //    Phone = Phone.Replace(")", "");
            //    Phone = Phone.Replace("-", "");
            //    Phone = Phone.Replace(" ", "");
            //}
            //if (LabelFax != null)
            //{
            //    Fax = LabelFax.Text;
            //    Fax = Fax.Replace("(", "");
            //    Fax = Fax.Replace(")", "");
            //    Fax = Fax.Replace("-", "");
            //    Fax = Fax.Replace(" ", "");
            //}

            string Address = "";
            string Address1 = "";
            string Address2 = "";
            string City = "";
            string StateProv = "";
            string ZipCode = "";
            string Country = "";
            bool DropShip = false;
            int OrderID = 0;
            bool errFlag = false;

            GridViewRow selectedRow = gvShippingAddress.Rows[index];

            Label LabelAddress1 = (Label)selectedRow.FindControl("lblGVAddress1");
            Label LabelAddress2 = (Label)selectedRow.FindControl("lblGVAddress2");
            Label LabelCity = (Label)selectedRow.FindControl("lblGVCity");
            Label LabelState = (Label)selectedRow.FindControl("lblGVState");
            Label LabelStateProv = (Label)selectedRow.FindControl("lblGVState");
            Label LabelZipCode = (Label)selectedRow.FindControl("lblGVZip");
            Label LabelCountry = (Label)selectedRow.FindControl("lblGVCountry");

            if (LabelAddress1 != null)
            {
                Address1 = LabelAddress1.Text;
            }
            if (LabelAddress2 != null)
            {
                Address2 = LabelAddress2.Text;
            }
            if (LabelCity != null)
            {
                City = LabelCity.Text;
            }
            if (LabelState != null)
            {
                StateProv = LabelStateProv.Text;
            }
            if (LabelZipCode != null)
            {
                ZipCode = LabelZipCode.Text;
            }
            if (LabelCountry != null)
            {
                Country = LabelCountry.Text;
            }
            if (ZipCode.Length > 7)
            {
                ZipCode = ZipCode.Substring(0, 5);
            }
            if (ckbDropShip.Checked == true)
            {
                DropShip = true;
            }

            Address = Address1 + " " + Address2;

            if (goNoGo == "go")
            {
                try
                {
                    // Insert into the SOPShoppingCartHeader Table
                    // MAKE CONNECTION TO SQL Server	
                    // ESTABLISH A SQL COMMAND AND COMMAND TYPE
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                    SqlCommand cmd = new SqlCommand("SOP_InsertSOPShoppingCartHeader", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ADD PARAMETERS FOR STORED PROCEDURE

                    cmd.Parameters.Add("@FormCreationDate", SqlDbType.DateTime);
                    cmd.Parameters["@FormCreationDate"].Value = FormCreationDate;
                    cmd.Parameters.Add("@FormSpecialType", SqlDbType.VarChar, 31);
                    cmd.Parameters["@FormSpecialType"].Value = FormSpecialType.ToString();
                    cmd.Parameters.Add("@CustomerNum", SqlDbType.VarChar, 31);
                    cmd.Parameters["@CustomerNum"].Value = CustomerNum;
                    cmd.Parameters.Add("@PONum", SqlDbType.VarChar, 31);
                    cmd.Parameters["@PONum"].Value = PONum.ToString();
                    cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime);
                    cmd.Parameters["@OrderDate"].Value = OrderDate;
                    cmd.Parameters.Add("@ShipDate", SqlDbType.DateTime);
                    cmd.Parameters["@ShipDate"].Value = ShipDate;
                    cmd.Parameters.Add("@CancelDate", SqlDbType.DateTime);
                    cmd.Parameters["@CancelDate"].Value = CancelDate;
                    cmd.Parameters.Add("@OrderedBy", SqlDbType.VarChar, 31);
                    cmd.Parameters["@OrderedBy"].Value = OrderedBy.ToString();
                    cmd.Parameters.Add("@Company", SqlDbType.VarChar, 31);
                    cmd.Parameters["@Company"].Value = Company.ToString();
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 31);
                    cmd.Parameters["@Address"].Value = Address.ToString();
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 31);
                    cmd.Parameters["@City"].Value = City.ToString();
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar, 21);
                    cmd.Parameters["@Country"].Value = Country.ToString();
                    cmd.Parameters.Add("@StateProv", SqlDbType.VarChar, 29);
                    cmd.Parameters["@StateProv"].Value = StateProv.ToString();
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 11);
                    cmd.Parameters["@ZipCode"].Value = ZipCode.ToString();
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 21);
                    cmd.Parameters["@Phone"].Value = Phone.ToString();
                    cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 21);
                    cmd.Parameters["@Fax"].Value = Fax.ToString();
                    cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 200);
                    cmd.Parameters["@Comments"].Value = Comments.ToString();
                    cmd.Parameters.Add("@OrderTotal", SqlDbType.Decimal, 19);
                    cmd.Parameters["@OrderTotal"].Value = Convert.ToDecimal(OrderTotal.ToString());
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 5);
                    cmd.Parameters["@Status"].Value = Status.ToString();
                    cmd.Parameters.Add("@DropShip", SqlDbType.Bit);
                    cmd.Parameters["@DropShip"].Value = DropShip;

                    // CREATE AND EXECUTE A DATAREADER TO GET THE IDENTITY BACK
                    cmd.Connection.Open();
                    OrderID = System.Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    conn.Dispose();

                    // SET SESSION EQUAL TO THE ORDERID
                    Session["HeaderOrderID"] = OrderID.ToString();
                    Session["PONumber"] = PONum.ToString();

                    //CHECK IF PDF REFERENCE NUMBER IS VALID (NOT NULL OR EQUAL TO ZERO)

                    if (OrderID == 2)
                    {
                        errFlag = true;
                        lblErrorMsg.Text = "There is a duplicate order matching this Customer Number and PO Number! Please use another PO Number.";
                        lblErrorMsg.Visible = true;
                    }

                    if ((OrderID == 0) || (errFlag == true))// IF PDF REFERENCE NUMBER HAS NO VALUE DISPLAY ERROR
                    {
                        object temp;
                        string message = "An error has occurred trying to create this order. Please check that all fields are filled out properly and try again! ";
                        lblErrorMsg.Text = message;
                        lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
                        lblErrorMsg.Font.Bold = true;

                        // IN THIS CASE YOU WOULD DELETE THE ENTRY YOU MADE
                        SqlConnection dconn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                        SqlCommand dcmd = new SqlCommand("SOP_DeleteSOPShoppingCartHeader", dconn);
                        dcmd.CommandType = CommandType.StoredProcedure;
                        dcmd.Parameters.Add("@OrderID", SqlDbType.Int);
                        dcmd.Parameters["@OrderID"].Value = OrderID;
                        dcmd.Connection.Open();
                        temp = dcmd.ExecuteScalar();
                        dconn.Close();
                        dconn.Dispose();
                    }
                    else
                    {
                        if (goNoGo == "go")
                        {
                            string currGuid;
                            currGuid = System.Guid.NewGuid().ToString();
                            HttpContext.Current.Session["ShoppingCart"] = currGuid;
                            Response.Redirect("createorder.aspx");

                        }

                    }
                }

                catch (Exception ex)
                {
                    string message = "An error has occurred trying to create this order.  Please check that all fields are filled out properly and try again!";
                    lblErrorMsg.Text = message + " " + ex.Message.ToString();
                    lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
                    lblErrorMsg.Font.Bold = true;
                }
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string goNoGo = "go";

        // Check to see if dates have been selected
        //  if (calOrderDate.SelectedDate.Ticks.ToString() == "0")
        //  {
        //    goNoGo = "nogo";
        //   lblErrorMsg.Text = "You must select an order date.";
        //    lblErrorMsg.Visible = true;
        //    lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
        //    lblErrorMsg.Font.Bold = true;
        //   }
        //else if (calShipDate.SelectedDate.Ticks.ToString() == "0")
        if (calShipDate.SelectedDate.Ticks.ToString() == "0")
        {
            goNoGo = "nogo";
            lblErrorMsg.Text = "You must select an ship date.";
            lblErrorMsg.Visible = true;
            lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
            lblErrorMsg.Font.Bold = true;
        }
        //else if (calCancelDate.SelectedDate.Ticks.ToString() == "0")
        //{
        //    goNoGo = "nogo";
        //    lblErrorMsg.Text = "You must select an Cancel date.";
        //    lblErrorMsg.Visible = true;
        //    lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
        //    lblErrorMsg.Font.Bold = true;
        //}
        else if (txtPONum.Text == "")
        {
            goNoGo = "nogo";
            lblErrorMsg.Text = "Each order must have a PONumber!";
            lblErrorMsg.Visible = true;
            lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
            lblErrorMsg.Font.Bold = true;
            txtPONum.Focus();
        }

        DateTime FormCreationDate = DateTime.Now;
        string FormSpecialType = lblFormSpecialType.Text;
        string CustomerNum = Session["CustomerID"].ToString();
        string PONum = txtPONum.Text;
        DateTime OrderDate = DateTime.Now; ;
        // DateTime OrderDate = calOrderDate.SelectedDate;
        DateTime ShipDate = calShipDate.SelectedDate;
        DateTime CancelDate = calShipDate.SelectedDate;
        string Comments = txtComments.Text;
        decimal OrderTotal = 0;
        string Status = "N";
        string OrderedBy = "";
        string Company = "";
        string Phone = "";
        string Fax = "";
        // Status 'N' = New, 'S' = Processed, 'C' = Confirmed, 'E' = Error
        // Move through Formview looking at zero based row results
        // Label LabelContact = (Label)fvRetailer.FindControl("lblContact");
        // Label LabelCompany = (Label)fvRetailer.FindControl("lblCompany");
        // Label LabelPhone = (Label)fvRetailer.FindControl("lblPhone");
        // Label LabelFax = (Label)fvRetailer.FindControl("lblFax");
        //  if (LabelContact != null)
        //{
        //    if (txtOrderedBy.Text == "")
        //    {
        //        OrderedBy = LabelContact.Text;
        //    }
        //    else
        //    {
        //        OrderedBy = txtOrderedBy.Text;
        //    }
        //}
        //if (LabelCompany != null)
        //{
        //    Company = txtFirstName.Text + " " + txtLastName.Text;
        //}
        //if (LabelPhone != null)
        //{
        //    Phone = LabelPhone.Text;
        //    Phone = Phone.Replace("(", "");
        //    Phone = Phone.Replace(")", "");
        //    Phone = Phone.Replace("-", "");
        //    Phone = Phone.Replace(" ", "");
        //}
        //if (LabelFax != null)
        //{
        //    Fax = LabelFax.Text;
        //    Fax = Fax.Replace("(", "");
        //    Fax = Fax.Replace(")", "");
        //    Fax = Fax.Replace("-", "");
        //    Fax = Fax.Replace(" ", "");
        //}

        string CustomerName = "";
        string FirstName = "";
        string LastName = "";
        string Address = "";
        string Address1 = "";
        string Address2 = "";
        string City = "";
        string StateProv = "";
        string ZipCode = "";
        string Country = "";
        bool DropShip = false;
        int OrderID = 0;
        bool errFlag = false;

        if (txtFirstName.Text != null)
        {
            FirstName = txtFirstName.Text;
        }
        if (txtLastName.Text != null)
        {
            LastName = txtLastName.Text;
        }
        if (txtAddress1.Text != null)
        {
            Address1 = txtAddress1.Text;
        }
        if (txtAddress2.Text != null)
        {
            Address2 = txtAddress2.Text;
        }
        if (txtCity.Text != null)
        {
            City = txtCity.Text;
        }
        if (ddlState.SelectedValue.ToString() != null)
        {
            StateProv = ddlState.SelectedValue.ToString();
        }
        if (txtZip.Text != null)
        {
            ZipCode = txtZip.Text;
        }
        if (ddlCountry.SelectedValue.ToString() != null)
        {
            Country = ddlCountry.SelectedValue.ToString();
        }
        if (ZipCode.Length > 7)
        {
            ZipCode = ZipCode.Substring(0, 5);
        }
        if (ckbDropShip.Checked == true)
        {
            DropShip = true;
        }

        CustomerName = FirstName + " " + LastName;
        Address = Address1 + " " + Address2;


        if (goNoGo == "go")
        {
            try
            {
                // Insert into the SOPShoppingCartHeader Table
                // MAKE CONNECTION TO SQL Server	
                // ESTABLISH A SQL COMMAND AND COMMAND TYPE
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand("SOP_InsertSOPShoppingCartHeader", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // ADD PARAMETERS FOR STORED PROCEDURE

                cmd.Parameters.Add("@FormCreationDate", SqlDbType.DateTime);
                cmd.Parameters["@FormCreationDate"].Value = FormCreationDate;
                cmd.Parameters.Add("@FormSpecialType", SqlDbType.VarChar, 31);
                cmd.Parameters["@FormSpecialType"].Value = FormSpecialType.ToString();
                cmd.Parameters.Add("@CustomerNum", SqlDbType.VarChar, 31);
                cmd.Parameters["@CustomerNum"].Value = CustomerNum.ToString();
                cmd.Parameters.Add("@PONum", SqlDbType.VarChar, 31);
                cmd.Parameters["@PONum"].Value = PONum.ToString();
                cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime);
                cmd.Parameters["@OrderDate"].Value = OrderDate;
                cmd.Parameters.Add("@ShipDate", SqlDbType.DateTime);
                cmd.Parameters["@ShipDate"].Value = ShipDate;
                cmd.Parameters.Add("@CancelDate", SqlDbType.DateTime);
                cmd.Parameters["@CancelDate"].Value = CancelDate;
                cmd.Parameters.Add("@OrderedBy", SqlDbType.VarChar, 31);
                cmd.Parameters["@OrderedBy"].Value = OrderedBy.ToString();
                cmd.Parameters.Add("@Company", SqlDbType.VarChar, 31);
                cmd.Parameters["@Company"].Value = CustomerName.ToString();
                cmd.Parameters.Add("@Address", SqlDbType.VarChar, 31);
                cmd.Parameters["@Address"].Value = Address.ToString();
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 31);
                cmd.Parameters["@City"].Value = City.ToString();
                cmd.Parameters.Add("@Country", SqlDbType.VarChar, 21);
                cmd.Parameters["@Country"].Value = Country.ToString();
                cmd.Parameters.Add("@StateProv", SqlDbType.VarChar, 29);
                cmd.Parameters["@StateProv"].Value = StateProv.ToString();
                cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 11);
                cmd.Parameters["@ZipCode"].Value = ZipCode.ToString();
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 21);
                cmd.Parameters["@Phone"].Value = Phone.ToString();
                cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 21);
                cmd.Parameters["@Fax"].Value = Fax.ToString();
                cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 200);
                cmd.Parameters["@Comments"].Value = Comments.ToString();
                cmd.Parameters.Add("@OrderTotal", SqlDbType.Decimal, 19);
                cmd.Parameters["@OrderTotal"].Value = Convert.ToDecimal(OrderTotal.ToString());
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 5);
                cmd.Parameters["@Status"].Value = Status.ToString();
                cmd.Parameters.Add("@DropShip", SqlDbType.Bit);
                cmd.Parameters["@DropShip"].Value = DropShip;

                // CREATE AND EXECUTE A DATAREADER TO GET THE IDENTITY BACK
                cmd.Connection.Open();
                OrderID = System.Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                conn.Dispose();

                // SET SESSION EQUAL TO THE ORDERID
                Session["HeaderOrderID"] = OrderID.ToString();
                Session["PONumber"] = PONum.ToString();

                //CHECK IF PDF REFERENCE NUMBER IS VALID (NOT NULL OR EQUAL TO ZERO)

                if (OrderID == 2)
                {
                    errFlag = true;
                    lblErrorMsg.Text = "There is a duplicate order matching this Customer Number and PO Number! Please use another PO Number.";
                    lblErrorMsg.Visible = true;
                }

                if ((OrderID == 0) || (errFlag == true))// IF PDF REFERENCE NUMBER HAS NO VALUE DISPLAY ERROR
                {
                    object temp;
                    string message = "An error has occurred trying to create this order. Please check that all fields are filled out properly and try again! ";
                    lblErrorMsg.Text = message;
                    lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
                    lblErrorMsg.Font.Bold = true;

                    // IN THIS CASE YOU WOULD DELETE THE ENTRY YOU MADE
                    SqlConnection dconn = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                    SqlCommand dcmd = new SqlCommand("SOP_DeleteSOPShoppingCartHeader", dconn);
                    dcmd.CommandType = CommandType.StoredProcedure;
                    dcmd.Parameters.Add("@OrderID", SqlDbType.Int);
                    dcmd.Parameters["@OrderID"].Value = OrderID;
                    dcmd.Connection.Open();
                    temp = dcmd.ExecuteScalar();
                    dconn.Close();
                    dconn.Dispose();
                }
                else
                {
                    if (goNoGo == "go")
                    {
                        string currGuid;
                        currGuid = System.Guid.NewGuid().ToString();
                        HttpContext.Current.Session["ShoppingCart"] = currGuid;
                        Response.Redirect("createorder.aspx");

                    }

                }
            }

            catch (Exception ex)
            {
                string message = "An error has occurred trying to create this order.  Please check that all fields are filled out properly and try again!";
                lblErrorMsg.Text = message + " " + ex.Message.ToString();
                lblErrorMsg.ForeColor = System.Drawing.Color.Firebrick;
                lblErrorMsg.Font.Bold = true;
            }
        }

    }

    protected void ckbDropShip_CheckedChanged(object sender, EventArgs e)
    {
        if (ckbDropShip.Checked == true)
        {
            pnlDropShip.Visible = true;
            pnlRetailer.Visible = false;
            pnlDropShipHeader.Visible = true;
        }
        if (ckbDropShip.Checked == false)
        {
            pnlDropShip.Visible = false;
            pnlRetailer.Visible = true;
            pnlDropShipHeader.Visible = true;
        }

    }
}