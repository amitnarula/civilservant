using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_PossedQuarter : System.Web.UI.Page
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
        //drpOffice.DataSource = Offices.GetAlloffices();
        //drpOffice.DataBind();
    }
    protected void btnGenerateReport_click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }//modified by :anki :)
         //        string query = @"select ROW_NUMBER() OVER(ORDER BY a.dateofpossession DESC) AS [Sr no], a.quarternumber,u.fullname as [Name of allotte],d.name as Designation,o.name as office,CONVERT(VARCHAR(10), a.dateofpossession, 103) as [Date of possession] 
         //from dbo.tblAllottee a
         //inner join tblquarters q on q.quarternumber=a.quarternumber
         //inner join dbo.tblUsers u on a.aan=u.aan
         //inner join dbo.tblOffices o on o.id=u.baseofficeid
         //inner join dbo.tblDesignations d on u.designation=d.id
         //where a.status=2 and q.category=" + drpQuarterCat.SelectedValue + " and o.id=" + drpOffice.SelectedValue + " ORDER BY a.dateofpossession DESC";

        string query = @"select ROW_NUMBER() OVER(ORDER BY a.dateofpossession DESC) AS [Sr no], a.quarternumber,u.fullname as [Name of allotte],d.name as Designation,o.name as office,CONVERT(VARCHAR(10), a.dateofpossession, 103) as [Date of possession] 
from dbo.tblAllottee a
inner join tblquarters q on q.quarternumber=a.quarternumber
inner join dbo.tblUsers u on a.aan=u.aan
inner join dbo.tblOffices o on o.id=u.baseofficeid
inner join dbo.tblDesignations d on u.designation=d.id
where a.status=2 and q.category=" + drpQuarterCat.SelectedValue + " ORDER BY a.dateofpossession DESC";



        SqlDataAdapter adp = new SqlDataAdapter(query, con);
        try
        {
            DataSet ds = new DataSet();
            adp.Fill(ds);

            var dtResult = ds.Tables[0].AsEnumerable()
                .GroupBy(row => row["quarternumber"])
                .Select(g => g.OrderByDescending(row => Convert.ToDateTime(row["Date of possession"] != DBNull.Value ? row["Date of possession"] : DateTime.MinValue)).FirstOrDefault())
                .CopyToDataTable();

            ExportData.Export o = new ExportData.Export("Web");
            if (ds.Tables.Count > 0)
            {
                if (dtResult.Rows.Count <= 0)
                    dtResult.Rows.Add(ds.Tables[0].NewRow());

                //if (ds.Tables[0].Rows.Count <= 0)
                //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                o.ExportDetails(dtResult, ExportData.Export.ExportFormat.Excel, "Alloted Quarter report.xls");
            }
        }
        finally
        {
            adp.Dispose();
            con.Close();
        }
    }
}