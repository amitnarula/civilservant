using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_MasterPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Adduser.Visible = false;
    }
    protected void lnkMenu_Click(object sender, EventArgs e)
    { 
    }
    protected void lnklogOut_Click(object sender, EventArgs e)
    {
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("default.aspx", true);
    }
    public void showAddUser()
    {
        Adduser.Visible = true;
    }
    public void showAddQuater()
    {
        addquater.Visible = true;
    }
    public void showAddAllottee()
    {
        addAllottee.Visible = false;
    }
}
