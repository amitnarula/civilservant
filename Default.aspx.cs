using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);

            if (_user != null)
            {
                bool isPasswordChanged = _user.IsPasswordChanged.HasValue ? _user.IsPasswordChanged.Value : false;

                if (!isPasswordChanged)
                {
                    Response.Redirect("~/user/changepassword.aspx");
                }
            }
        }
    }
}
