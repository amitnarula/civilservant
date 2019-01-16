using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Reports_ChallanReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Reports");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
    }
    protected void btnGenerateReport_click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlDataAdapter adp = new SqlDataAdapter("select * from tbChallan" , con);
        try
        {
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ExportData.Export o = new ExportData.Export("Web");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count <= 0)
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                o.ExportDetails(ds.Tables[0], ExportData.Export.ExportFormat.Excel, "Quarter report.xls");
            }
        }
        finally
        {
            adp.Dispose();
            con.Close();
        }
    }
}
