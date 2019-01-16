using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QuarterHistory : System.Web.UI.Page
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
    }
    private void BindData()
    {
        grdQuarters.DataSource = Quarters.GetQuarterHistroy(txtQuarterNo.Text); ;
        grdQuarters.DataBind();
    }
    protected void getHistory_click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQuarterNo.Text))
        {
            BindData();
        }
    }
}
