using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SendmailMessage.SendMail("support@estatepagpb.org", "support@estatepagpb.org", "Hi","test");
    }
}
