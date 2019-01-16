using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forgot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (!Users.CheckEmail(txtEmailId.Text))
        {
            lblmessage.Text = "Email address does't exists!";
            lblmessage.Visible = true;
            return;
        }
        tblUser _user=Users.getUserEmail(txtEmailId.Text);
        string outstring = "";
        if (_user != null)
            SendmailMessage.sendEmailMessage("support@estatepagpb.org", _user.EmailID, "", "Account Password", "Your password is :" + _user.Password, "", out outstring);
        else
        {
            lblmessage.Text = "Invalid user!";
            lblmessage.Visible = true;
        }
        //SendmailMessage.SendMail(txtEmailId.Text, "support@estatepagpb.org");
    }
}
