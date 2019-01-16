using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_application : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);

        if (_user.Roleid == 1) //Admin
        {
            lblStatus.Text = "User applications";
            pnlSearch.Visible = true;
        }

        if (pnlSearch.Visible)
        {
            _user.AAN = txtSearch.Text.Trim();
        }

        grdQuarters.DataSource = AllotementApplications.GetApplicationsByAAn(_user.AAN);
        grdQuarters.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void GridRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Verify")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            Response.Redirect(string.Format("~/user/submission.aspx?applicationid={0}&returnurl=admin/verification.aspx&verify=true", id));

        }
        if (e.CommandName == "view")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);

            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            //or Admin is viewing the application,application is pending, allow user through this step
            if (AllotementApplications.GetApplication(id).Status == 0 || _user.Roleid==1) 
            {
                Response.Redirect(string.Format("~/user/submission.aspx?applicationid={0}&returnurl=user/application.aspx", id));
            }
            else
            {
                Response.Redirect(string.Format("~/user/processapplication.aspx?applicationId={0}", id));       
            }
        }
    }

    protected void gridrowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AllotementApplication _app = e.Row.DataItem as AllotementApplication;
                HyperLink hypprint=e.Row.FindControl("hypprint") as HyperLink;
                hypprint.NavigateUrl = "~/User/printapplication.aspx?applicationid="+_app.Id; 
        }
    }

    protected void btnNewApplication_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/user/submission.aspx");
    }

}
