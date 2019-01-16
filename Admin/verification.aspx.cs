using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_verificattion : System.Web.UI.Page
{
     protected void Page_Load(object sender, EventArgs e)
    {
        

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Verify");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
        lblMessage.Visible = false;
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
           if(_user.tblRole.name.ToLower().Contains("entry")){
               grdQuarters.DataSource = AllotementApplications.GetPendingApplications(ApplicationStatus.Pending, _user.BaseOfficeId.Value);
               grdQuarters.DataBind();
        
           }
           else
           {
        grdQuarters.DataSource = AllotementApplications.GetPendingApplications(ApplicationStatus.Pending);
        grdQuarters.DataBind();
           }
    }

    protected void GridRowCommand(object sender,GridViewCommandEventArgs  e)
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
            Response.Redirect(string.Format("~/user/submission.aspx?applicationid={0}&returnurl=admin/verification.aspx", id));
        }
        if (e.CommandName == "reject")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            tbAllotmentApplication application=AllotementApplications.GetApplication(id);
            application.Status =(int) ApplicationStatus.rejected;
            AllotementApplications.SaveApplications(application);
            lblMessage.Text = "Application has been rejected sucessfully!";
            lblMessage.Visible = true;
            BindData();
        }
        
    }
}
