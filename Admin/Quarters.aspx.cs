using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Quarters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Quarters");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
      
        lblMessage.Visible = false;
        if (!Page.IsPostBack)
            Bindcategory();
    }
    private void BindData()
    {
        //grdQuarters.DataSource = Quarters.GetAllQuarters(Convert.ToInt64(drpQuarterCatergory.SelectedValue));
        var result = from n in Quarters.GetAllQuarters(Convert.ToInt64(drpQuarterCatergory.SelectedValue))
                     group n by n.quarternumber into g
                     select g.OrderByDescending(t => t.DateOfAllotement).FirstOrDefault();

        grdQuarters.DataSource = result.ToList();

        grdQuarters.DataBind();
    }
    private void Bindcategory()
    {
        drpQuarterCatergory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCatergory.DataBind();
    }
    protected void detail_click(object sender, EventArgs e)
    {
        lblMessage.Text = "List of Total Quarters " + drpQuarterCatergory.SelectedItem.Text;
        lblMessage.Visible = true;
        BindData();
    }
    protected void grdQuarters_databound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label l = e.Row.FindControl("lblstatus") as Label;
            uspGetQuarterResult qr= e.Row.DataItem as uspGetQuarterResult;
            if (qr.Status.HasValue)
            {
                string  qs= Enum.GetName(typeof(QuarterStatus), qr.Status.Value);
                l.Text = qs;
            }
            

        }
    }
}
