using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Reports_pendinglicensefee : System.Web.UI.Page
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
        DateTime startdate, enddate;
        DateTime.TryParse(txtstartdate.Text, out startdate);
        DateTime.TryParse(txtenddate.Text, out enddate);
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        string query = @"
select Q.quarternumber,u.fullname as [Name of allotte],fee,actualfee,balance from tbQuarterLicenceFee ql
inner join tblquarters q on q.id=ql.quarterid
inner join dbo.tblUsers u on ql.aan=u.aan
inner join dbo.tblOffices o on o.id=u.baseofficeid
where q.category= " + drpQuarterCat.SelectedValue + "and o.id = " + drpOffice.SelectedValue + " and ql.month > convert(datetime,'" + startdate.ToShortDateString() + "',103) and ql.month < convert(datetime,'" + enddate.ToShortDateString() + "',103)";

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
                o.ExportDetails(ds.Tables[0], ExportData.Export.ExportFormat.Excel, "Licence fee report.xls");
            }
        }
        finally
        {
            adp.Dispose();
            con.Close();
        }
    }
}