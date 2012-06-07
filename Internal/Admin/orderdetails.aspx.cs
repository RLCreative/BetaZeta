using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_orderdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.Text = "Results for Order: " + Request.QueryString["PDFRefNum"];
        hypBack.NavigateUrl = "vieworders.aspx";
        hypBackText.NavigateUrl = "vieworders.aspx";
        hypPrint.NavigateUrl = "printorder.aspx?PDFRefNum=" + Request.QueryString["PDFRefNum"];
        hypPrintText.NavigateUrl = "printorder.aspx?PDFRefNum=" + Request.QueryString["PDFRefNum"];
        hypTrack.NavigateUrl = "trackorders.aspx?PDFRefNum=" + Request.QueryString["PDFRefNum"];
        hypTrackText.NavigateUrl = "trackorders.aspx?PDFRefNum=" + Request.QueryString["PDFRefNum"];
    }
}
