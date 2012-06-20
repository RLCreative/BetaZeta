using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AutocompletItemNumbers
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.  
[System.Web.Script.Services.ScriptService]
public class AutocompletItemNumbers : System.Web.Services.WebService
{

    public AutocompletItemNumbers()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent();
    }

    [WebMethod]
    public string[] AutoCompleteItemNum(string prefixText)
    {
        
        string sql = "SELECT RTRIM([ProductID]) as ProductID FROM wsPF_ProductExtraInfo WHERE DisplayOnOrderForm = '1' and [ProductID] not like '15-%' and [ProductID] like '" + @prefixText + "%'";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        //da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.Char, 10).Value = prefixText+ "%"; 
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue("'" + dr["ProductID"].ToString() + "'", i);
            i++;
        }
        return items;
    }

    [WebMethod]
    public string[] AutoCompleteDescription(string prefixText)
    {

        string sql = "SELECT RTRIM([Name]) as Name FROM wsPF_ProductExtraInfo WHERE DisplayOnOrderForm = '1' and [Name] not like 'Akord%' and [Name] like '" + @prefixText + "%'";
        SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["RLConnectionString"].ToString());
        //da.SelectCommand.Parameters.Add("@prefixText", SqlDbType.Char, 10).Value = prefixText+ "%"; 
        DataTable dt = new DataTable();
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Name"].ToString(), i);
            i++;
        }
        return items;
    }

}
