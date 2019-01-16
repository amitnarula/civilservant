using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstateBluePrivate : System.Web.UI.MasterPage
{
    public string _url { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        _url = Page.ResolveUrl("~");
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            
            if (_user != null && _user.Roleid != 4)
            {
                pnlAdminNavigation.Visible = true;
                pnlUserNavigation.Visible = false;
            }
            else
            {
                Tr2.Visible = true;
                SIGNUP.Visible = false;
                pnlAdminNavigation.Visible = false;
                pnlUserNavigation.Visible = true;
                login.InnerText = "Logout";
                login.HRef = "~/logout.aspx";
            }

            litWelcome.Text = "Welcome, " + _user.fullName;
            aHrefLogin.HRef = "default.aspx";
            aHrefGuestHouse.Visible = false;
            aHrefLogout.Visible = true;
            aHrefLogout.HRef = _url + "logout.aspx";
        }
        else
        {
            aHrefLogin.HRef = _url + "signin.aspx";
        }
    }

    protected void Allotment_click(object sender, EventArgs e)
    {
        //if (!changerequest.Visible)
        //{
        //    changerequest.Visible = true;
        //    request1.Visible = true;
        //    Tr1.Visible = true;
        //}
        //else
        //{
        //    changerequest.Visible = false;
        //    request1.Visible = false;
        //    Tr1.Visible = false;
        //}
    }
}
