using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
public partial class _Signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        tblUser userOut = null;
        if (Users.ValidateUserByAAN(txtUsername.Text, txtPassword.Text, out userOut))
        {
            FormsAuthentication.RedirectFromLoginPage(userOut.Username,
                false);
        }
        else
        {
            lblmessage.Text = "Invalid user/password";
            lblmessage.Visible = true;
        }

    }
}
