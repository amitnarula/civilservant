using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Users : System.Web.UI.Page
{
    tblUser _user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "User management");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
      
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["status"]))
            {
                lblMessage.Text = "Record " + Request["status"] + " successfully!";
                lblMessage.Visible = true;
            }
            bindRoles();
            //BindUsers();
        }

        grdUsers.RowDataBound += new GridViewRowEventHandler(grdUsers_RowDataBound);
        btnSave.Click += new EventHandler(btnSave_Click);
        btnCancel.Click += new EventHandler(btnCancel_Click);
    }

    void btnCancel_Click(object sender, EventArgs e)
    {
        hdnSelectedUserId.Value = string.Empty;
    }

    void btnSave_Click(object sender, EventArgs e)
    {
        Users.Defer(Convert.ToInt32(hdnSelectedUserId.Value), 
            DateTime.Now.AddMonths(Convert.ToInt32(ddlDeferPeriod.SelectedValue)).Date); // ask for defer date
        grdUsers.DataBind();
        //BindUsers();
        lblMessage.Text = "Record deferred successfully!";
        lblMessage.Visible = true;
        tbluserhistory _userhistory = new tbluserhistory();
        _userhistory.Action = "User deferred";
        _userhistory.description = _user.Username + " has deferred user with userid " + hdnSelectedUserId.Value;
        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);
    }

    void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDebarUser = e.Row.FindControl("lnkDebar") as LinkButton;
            LinkButton lnkDeferUser = e.Row.FindControl("lnkDefer") as LinkButton;
            LinkButton lnkRemoveBar = e.Row.FindControl("lnkRemoveBar") as LinkButton;
            LinkButton lnkRemoveDefer = e.Row.FindControl("lnkRemoveDefer") as LinkButton;


            long userId = Convert.ToInt64(lnkDebarUser.CommandArgument);
            DateTime? deferUntil = null;
            DateTime? debarUntil = null;

            if (Users.IsUserDebarred(userId, out debarUntil))
            {
                if (debarUntil <= DateTime.Now.Date)
                {
                    lnkDebarUser.Visible = true;
                    lnkRemoveBar.Visible = false;
                }
                else
                {
                    lnkRemoveBar.Visible = true;
                    lnkDebarUser.Visible = false;
                }
            }
            else
            {
                lnkDebarUser.Visible = true;
                lnkRemoveBar.Visible = false;
            }

            if (Users.IsUserDeferred(userId, out deferUntil))
            {
                if (deferUntil <= DateTime.Now.Date)
                {
                    lnkDeferUser.Visible = true;
                    lnkRemoveDefer.Visible = false;
                }
                else
                {
                    lnkDeferUser.Visible = false;
                    lnkRemoveDefer.Visible = true;
                }
            }
            else
            {
                lnkDeferUser.Visible = true;
                lnkRemoveDefer.Visible = false;
            }
        }
    }
    private void BindUsers()
    {
        int total = 0;
       
        grdUsers.DataSource = Users.getAllUsers();
        
        grdUsers.DataBind();
       
    }
    protected void grdCommad(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "deleteuser")
        {
            long userid = Convert.ToInt64(e.CommandArgument);
            Users.Delete(userid);
            grdUsers.DataBind();
            //BindUsers();
            lblMessage.Text = "Record deleted successfully!";
            lblMessage.Visible = true;
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "User deletion";
            _userhistory.description = _user.Username +" has deleted user with userid "+userid;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
        }
        else if (e.CommandName == "debarUser")
        {
            long userid = Convert.ToInt64(e.CommandArgument);
            Users.Debar(userid, DateTime.Parse("31/12/" + DateTime.Now.Year));
            grdUsers.DataBind();
            //BindUsers();
            lblMessage.Text = "Record debarred until 31/12/"+DateTime.Now.Year+" successfully!";
            lblMessage.Visible = true;
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "User debarred";
            _userhistory.description = _user.Username + " has debarred user with userid " + userid;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
            
        }
        else if (e.CommandName == "deferUser")
        {
            popDefer.Show();
            long userid = Convert.ToInt64(e.CommandArgument);
            hdnSelectedUserId.Value = userid.ToString();
            
        }
        else if (e.CommandName == "removeBar")
        {
            long userid = Convert.ToInt64(e.CommandArgument);
            Users.ReactivateUser(userid, true, false);
            grdUsers.DataBind();
            //BindUsers();
            lblMessage.Text = "Record reactivated successfully!";
            lblMessage.Visible = true;
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "User reactivated";
            _userhistory.description = _user.Username + " has reactivated user with userid " + userid;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
        }
        else if (e.CommandName == "removeDefer")
        {
            long userid = Convert.ToInt64(e.CommandArgument);
            Users.ReactivateUser(userid, false, true);
            grdUsers.DataBind();
            //BindUsers();
            lblMessage.Text = "Record reactivated successfully!";
            lblMessage.Visible = true;
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "User reactivated";
            _userhistory.description = _user.Username + " has reactivated user with userid " + userid;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
        }
    }
    
    protected void ObjectDataSource1_Filtering(object sender, ObjectDataSourceFilteringEventArgs e)
    {
        if (!string.IsNullOrEmpty(drpRoles.SelectedValue))
        {
            e.ParameterValues.Clear();
            e.ParameterValues.Add("Roleid", drpRoles.SelectedValue);
            e.ParameterValues.Add("Name", txtSearchByName.Text);
        }
    }
    protected void grdUsers_pageindexchanged(object sender, GridViewPageEventArgs e)
    {
        grdUsers.PageIndex = e.NewPageIndex;
        BindUsers();
    }
    private void bindRoles()
    {
        drpRoles.DataSource = Roles.GetRoles();
        drpRoles.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}
