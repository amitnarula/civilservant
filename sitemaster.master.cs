using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class sitemaster : System.Web.UI.MasterPage
{
    protected string _url;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //DataSet ds = new DataSet();
        //ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
        //DataTable dt = ds.Tables[0];
        //if (dt.Rows.Count > 0)
        //{
        //    if (!Convert.ToBoolean(dt.Rows[0][0]))
        //    {
        //        lnkSbmission.Text = "Enable Submission";
        //        lnkSbmission.CommandName = "Enable";
        //    }
        //    else
        //    {
        //        lnkSbmission.Text = "Disable Submission";
        //        lnkSbmission.CommandName = "Disable";
        //    }
        //}
        curdate.Text = DateTime.Now.ToString("dddd, d MMMM yyyy");
        _url=Page.ResolveUrl("~");
        if (!Page.IsPostBack)
        {
            changerequest.Visible = false;
            request1.Visible = false;
        }
        changerequest.Visible = false;
        request1.Visible = false;
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            //if (_user.tblRole.name.ToLower().Trim() == "admin")
            //{
            //    trSubmission.Visible = true;
            //}
            if (_user != null && _user.Roleid != 4)
            {
                Table1.Visible = true;
                unauthenticated.Visible = false;
            }
            else
            {
                Tr2.Visible = true;
                SIGNUP.Visible = false;
                Table1.Visible = false;
                unauthenticated.Visible = true;
                login.InnerText = "Logout";
                login.HRef = "~/logout.aspx";
            }
        }
        else {
            SIGNUP.Visible = true;
            Table1.Visible = false;
            unauthenticated.Visible = true;
            login.InnerText = "Login";
            login.HRef = "~/signin.aspx";
        }
    }
    protected void lnkMenu_Click(object sender, EventArgs e)
    {
    }
    protected void lnklogOut_Click(object sender, EventArgs e)
    {
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("default.aspx", true);
    }


    protected void Allotment_click(object sender, EventArgs e)
    {
        if (!changerequest.Visible)
        {
            changerequest.Visible = true;
            request1.Visible = true;
            Tr1.Visible = true;
        }
        else
        {
            changerequest.Visible = false;
            request1.Visible = false;
            Tr1.Visible = false;
        }
    }

    //protected void lnkSbmission_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
    //    DataTable dt = ds.Tables[0];

    //    if (lnkSbmission.CommandName == "Enable")
    //    {
    //        if (dt.Rows.Count > 0)
    //        {
    //            dt.Rows[0][0] = true;
    //            lnkSbmission.Text = "Disable Submission";
    //        }
    //    }
    //    else
    //    {
    //        if (dt.Rows.Count > 0)
    //        {
    //            dt.Rows[0][0] = false;
    //            lnkSbmission.Text = "Enable Submission";
    //        }
    //    }
    //    ds.WriteXml(Server.MapPath("~/submissionVerfication.xml"));
    //    ds.Dispose();
    //}
}
