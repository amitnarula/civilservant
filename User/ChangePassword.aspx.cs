using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
        if (_user != null)
        {
            if (_user.Password == txtOldPassword.Text)
            {
                _user.Password = txtNewPassword.Text;
                _user.IsPasswordChanged = true;
                Users.SaveUser(_user);
                lblmessage.Text = "Your password has been changed sucessfully, Redirecting to my applications";
                lblmessage.Visible = true;
                Response.Redirect("~/user/application.aspx");
            }
            else
            {
                lblmessage.Text = "Invalid Old password";
                lblmessage.Visible = true;
            }
        }
    
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    { }
}
