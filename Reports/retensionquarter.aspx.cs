using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_retensionquarter : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            BindData();
        }

    }
    private void BindData()
    {
        drpQuarterCat.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCat.DataBind();
        drpOffice.DataSource = Offices.GetAlloffices();
        drpOffice.DataBind();
    }
    protected void btnGenerateReport_click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = @"select ROW_NUMBER() OVER(ORDER BY a.DateOfRetensionUpto DESC) AS [Sr no], a.quarternumber,u.fullname as [Name of allotte],d.name as Designation,o.name as office,CONVERT(VARCHAR(10), a.DateOfRetensionUpto, 103) as [Date Of RetensionUpto] 
from dbo.tblAllottee a
inner join tblquarters q on q.quarternumber=a.quarternumber
inner join dbo.tblUsers u on a.aan=u.aan
inner join dbo.tblOffices o on o.id=u.baseofficeid
inner join dbo.tblDesignations d on u.designation=d.id
where a.status=4 and q.category=" + drpQuarterCat.SelectedValue + " and o.id=" + drpOffice.SelectedValue + " ORDER BY a.DateOfRetensionUpto DESC";

        SqlDataAdapter adp = new SqlDataAdapter(query, con);
        try
        {
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ExportData.Export o = new ExportData.Export("Web");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count <= 0)
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                o.ExportDetails(ds.Tables[0], ExportData.Export.ExportFormat.Excel, "Retension Quarter report.xls");
            }
        }
        finally
        {
            adp.Dispose();
            con.Close();
        }
    }
}