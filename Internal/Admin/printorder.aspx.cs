using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.ReportViewer;
using Telerik.Reporting;
using Telerik.Pdf;

public partial class admin_printorder : System.Web.UI.Page
{    

    protected void Page_Load(object sender, EventArgs e)
    {
        string param = Server.UrlDecode(this.Request.QueryString["PDFRefNum"]);
        //if (!IsPostBack)
        //{
        //    //Report OrderDetails = new Report();
        //    //ReportViewer1.Report = OrderDetails;
        //    //ReportViewer1.Report.ReportParameters["PDFRefNum"].Value = Request.QueryString["PDFRefNum"].ToString(); 
            
        //    // UPDATE REQUIRED FOR Q1 2009 RELEASE           
            Report report = (Report)this.ReportViewer1.Report;
            //report.ReportParameters[0].Value = param;
            report.ReportParameters["PDFRefNum"].Value = param;


        //}
    }
}
