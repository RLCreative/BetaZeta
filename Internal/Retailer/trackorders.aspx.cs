using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Web.Security;

public partial class retailers_trackorders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PDFRefNum"] == "NEW" || Request.QueryString["PDFRefNum"] == "")
        {
            pnlTrackInfo.Visible = true;
        }
        else if (Request.QueryString["PDFRefNum"] != "")
        {
            //Get the tracking number from RL Database
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
            String insertCmd = "SELECT RTRIM(SOP10107.Tracking_Number) as TrackingNumber FROM SOPPDFResults INNER JOIN SOPPDFHeader ON SOPPDFResults.SOPPDFResultsID = SOPPDFHeader.SOPPDFResultsID INNER JOIN ";
            insertCmd += " SOP10107 ON SOPPDFResults.GPOrderID = SOP10107.SOPNUMBE WHERE SOPPDFHeader.PDFRefNum = @PDFRefNum";
            SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);
            myCommand.Parameters.Add(new SqlParameter("@PDFRefNum", SqlDbType.VarChar));
            myCommand.Parameters["@PDFRefNum"].Value = Request.QueryString["PDFRefNum"].ToString();
            myCommand.Connection.Open();
            // Test whether the new row can be added and  display the 
            // appropriate message box to the user.
            try
            {
                string trkNumber = "";
                string strURL = "";
                trkNumber = myCommand.ExecuteScalar().ToString();

                if (trkNumber != "")
                {
                    if (trkNumber.Substring(0, 2) == "1Z") // UPS
                    {
                        strURL = "http://wwwapps.ups.com/WebTracking/track?HTMLVersion=5.0&loc=en_US&Requester=UPSHome&trackNums=" + trkNumber + "&track.x=Track";
                        Response.Redirect(strURL);
                    }
                    else if ((trkNumber.Length == 20) && (!IsAlpha(trkNumber.Substring(0, 1)))) // FEDEX
                    {
                        strURL = "http://www.fedex.com/Tracking?ascend_header=1&clienttype=dotcom&cntry_code=us&language=english&tracknumbers=" + trkNumber;
                        Response.Redirect(strURL);
                    }
                    else // OTHER
                    {
                        //Response.Redirect(
                        pnlOther.Visible = true;
                        lblTrackingInfo.Text = trkNumber;
                    }
                }
                else
                {
                    pnlError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.ForeColor = System.Drawing.Color.Firebrick;
                lblError.Font.Bold = true;
                lblError.Text = "An error occured inserting that item please try again ..." + ex.Message;
            }
            myCommand.Connection.Close();
        }
        else
        {
            pnlError.Visible = true;
            lblError.ForeColor = System.Drawing.Color.Firebrick;
            lblError.Font.Bold = true;
            lblError.Text = "We did not find any tracking information for this order.  Please make sure that the order has been processed.  If you have";
            lblError.Text += " additional questions please call customer care at Our Customer Care Department is ready to assist you with any questions or concerns you have,";
            lblError.Text += " Monday – Friday, 8:30 AM – 5:30 PM EST. Dial +1 800-593-5522.";       
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string pdfNum = "";
        string gpNum = "";
        string trkNumber = "";
        string strURL = "";

        if (txtGPOrderNum.Text == "" && txtPDFRefNum.Text == "" && txtTrackingNum.Text == "")
        {
            pnlError.Visible = true;
            lblError.ForeColor = System.Drawing.Color.Firebrick;
            lblError.Font.Bold = true;
            lblError.Text = "You need to enter a PDF Reference Number, a Regal Lager Order Number, or a Tracking Number!";// + ex.Message;
        }
        else
        {
            if (txtPDFRefNum.Text != "")
            {
                pdfNum = txtPDFRefNum.Text;
                //Get the tracking number from RL Database
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                String insertCmd = "SELECT RTRIM(SOP10107.Tracking_Number) as TrackingNumber FROM SOPPDFResults INNER JOIN SOPPDFHeader ON SOPPDFResults.SOPPDFResultsID = SOPPDFHeader.SOPPDFResultsID INNER JOIN ";
                insertCmd += " SOP10107 ON SOPPDFResults.GPOrderID = SOP10107.SOPNUMBE WHERE SOPPDFHeader.PDFRefNum = @PDFRefNum";
                SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);
                myCommand.Parameters.Add(new SqlParameter("@PDFRefNum", SqlDbType.VarChar));
                myCommand.Parameters["@PDFRefNum"].Value = pdfNum;
                myCommand.Connection.Open();
                // Test whether the new row can be added and  display the 
                // appropriate message box to the user.
                try
                {

                    trkNumber = myCommand.ExecuteScalar().ToString();

                    if (trkNumber != "")
                    {
                        if (trkNumber.Substring(0, 2) == "1Z")
                        {
                            strURL = "http://wwwapps.ups.com/WebTracking/track?HTMLVersion=5.0&loc=en_US&Requester=UPSHome&trackNums=" + trkNumber + "&track.x=Track";
                            Response.Redirect(strURL);
                        }
                        else if (trkNumber.Length == 20)
                        {
                            strURL = "http://www.fedex.com/Tracking?ascend_header=1&clienttype=dotcom&cntry_code=us&language=english&tracknumbers=" + trkNumber;
                            Response.Redirect(strURL);
                        }
                        else
                        {
                            if (trkNumber == "")
                            {
                                trkNumber = " An error occured inserting that item please try again ...";
                            }
                            else
                            {
                                lblTrackingInfo.Text = trkNumber;
                            }
                        }
                    }
                    else
                    {
                        pnlError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.ForeColor = System.Drawing.Color.Firebrick;
                    lblError.Font.Bold = true;
                    lblError.Text = "An error occured inserting that item please try again ..." + ex.Message;
                }
                myCommand.Connection.Close();
            }

            if (txtGPOrderNum.Text != "")
            {
                gpNum = txtPDFRefNum.Text;
                //Get the tracking number from RL Database
                SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
                String insertCmd = "SELECT RTRIM(SOP10107.Tracking_Number) as TrackingNumber FROM SOPPDFResults INNER JOIN SOPPDFHeader ON SOPPDFResults.SOPPDFResultsID = SOPPDFHeader.SOPPDFResultsID INNER JOIN ";
                insertCmd += " SOP10107 ON SOPPDFResults.GPOrderID = SOP10107.SOPNUMBE WHERE SOPPDFResults.GPOrderID = @GPOrderID";
                SqlCommand myCommand = new SqlCommand(insertCmd, myConnection);
                myCommand.Parameters.Add(new SqlParameter("@GPOrderID", SqlDbType.VarChar));
                myCommand.Parameters["@PDFRefNum"].Value = gpNum;
                myCommand.Connection.Open();
                // Test whether the new row can be added and  display the 
                // appropriate message box to the user.
                try
                {

                    trkNumber = myCommand.ExecuteScalar().ToString();

                    if (trkNumber != "")
                    {
                        if (trkNumber.Substring(0, 2) == "1Z")
                        {
                            strURL = "http://wwwapps.ups.com/WebTracking/track?HTMLVersion=5.0&loc=en_US&Requester=UPSHome&trackNums=" + trkNumber + "&track.x=Track";
                            Response.Redirect(strURL);
                        }
                        else if (trkNumber.Length == 20)
                        {
                            strURL = "http://www.fedex.com/Tracking?ascend_header=1&clienttype=dotcom&cntry_code=us&language=english&tracknumbers=" + trkNumber;
                            Response.Redirect(strURL);
                        }
                        else
                        {
                            pnlOther.Visible = true;
                            if (trkNumber == "")
                            {
                                trkNumber = " An error occured inserting that item please try again ...";
                            }
                            else
                            {
                                lblTrackingInfo.Text = trkNumber;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    pnlError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Firebrick;
                    lblError.Font.Bold = true;
                    lblError.Text = " An error occured inserting that item please try again ..." + ex.Message;
                }
                myCommand.Connection.Close();
            }

            if (txtTrackingNum.Text != "")
            {
                try
                {
                    if (trkNumber.Substring(0, 2) == "1Z")
                    {
                        strURL = "http://wwwapps.ups.com/WebTracking/track?HTMLVersion=5.0&loc=en_US&Requester=UPSHome&trackNums=" + txtTrackingNum.Text + "&track.x=Track";
                        Response.Redirect(strURL);
                    }
                    else if (trkNumber.Length == 20)
                    {
                        strURL = "http://www.fedex.com/Tracking?ascend_header=1&clienttype=dotcom&cntry_code=us&language=english&tracknumbers=" + txtTrackingNum.Text;
                        Response.Redirect(strURL);
                    }
                    else
                    {
                        pnlOther.Visible = true;
                        if (trkNumber == "")
                        {
                            trkNumber = " An error occured inserting that item please try again ...";
                        }
                        else
                        {
                            lblTrackingInfo.Text = trkNumber;
                        }
                    }

                }
                catch (Exception ex)
                {
                    pnlError.Visible = true;
                    lblError.ForeColor = System.Drawing.Color.Firebrick;
                    lblError.Font.Bold = true;
                    lblError.Text = "An error occured inserting that item please try again ..." + ex.Message;
                }

            }

            else
            {
                pnlTrackInfo.Visible = true;
                lblError.Text = " An error has occurred please enter new information and try again.";
            }
        }

    }

    public bool IsAlpha(string strToCheck)
    {
        Regex objAlphaPattern = new Regex("[^a-zA-Z]");

        return !objAlphaPattern.IsMatch(strToCheck);
    }


}
