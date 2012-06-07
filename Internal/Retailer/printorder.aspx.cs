using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.ReportViewer;
using Telerik.Reporting;
using Telerik.Pdf;

public partial class retailers_printorder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string param = Server.UrlDecode(this.Request.QueryString["PDFRefNum"]);

            Report report = (Report)this.ReportViewer1.Report;
            report.ReportParameters["PDFRefNum"].Value = param;
        }
    }
}
