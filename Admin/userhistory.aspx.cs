using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_userhistory : System.Web.UI.Page
{
    tblUser _user; 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "User History");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }   
    }
    protected void btnHistory_click(object sender, EventArgs e)
    {
        Bind();
    }
    private void Bind()
    {
        tblUser _user=Users.getUserByUserName(txtUser.Text);
        if (_user != null)
        {
            grdUsers.DataSource = userHistory.GetUserHistory(_user.AAN);
            grdUsers.DataBind();
        }
        else
        {
            lblMessage.Text = "Invalid user!";
            lblMessage.Visible = true;
        }
    }
}
