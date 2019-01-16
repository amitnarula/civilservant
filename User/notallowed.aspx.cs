using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_notallowed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["debar"]))
            lblReason.Text = "You are not allowed to perform this action because,you have been debarred by System Administrator";
        else
            lblReason.Text = "You are not allowed to perform this action because,you have been deferred by the System Administrator";
    }
}