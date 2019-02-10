using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_allottees : System.Web.UI.Page
{
    tblUser _user; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Allotment");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }

        if (!Page.IsPostBack)
            Bindcategory();
    }
    private void BindData()
    {
        grdQuarters.DataSource = Allottee.GetPossessedAllotte(Convert.ToInt64(drpQuarterCatergory.SelectedValue));
        grdQuarters.DataBind();
    }

    public void grdAllotte_command(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "vacant")
        {

            long id = Convert.ToInt64(e.CommandArgument);
            hdnselected.Value = id.ToString();
            //Allottee.VacantAllottement(id);
            //BindData();
            //tbluserhistory _userhistory = new tbluserhistory();
            //_userhistory.Action = "Allotement";
            //_userhistory.description = _user.Username + " has marked vacant with id " + id;
            //_userhistory.time = DateTime.Now;
            //_userhistory.useraan = _user.AAN;
            //userHistory.Save(_userhistory);
            tblAllottee _allotte = Allottee.GetAllottee(Convert.ToInt64(e.CommandArgument));
            txtName.Text = AllotementApplications.GetApplication(_allotte.ApplicationId.Value).tblUser.fullName;
            txtQuarternumber.Text = _allotte.QuarterNumber;
            popVacant.Show();
        }
        else if (e.CommandName == "Retention")
        {

            long id = Convert.ToInt64(e.CommandArgument);
            tblAllottee _allotte = Allottee.GetAllottee(Convert.ToInt64(e.CommandArgument));
            txtRetName.Text = AllotementApplications.GetApplication(_allotte.ApplicationId.Value).tblUser.fullName; 
            txtRetQuarerNumber.Text = _allotte.QuarterNumber;
            txtDateOfRetension.Text = string.Empty;
            txtdateofretensionupto.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            //ddlRetentionPeriod.SelectedIndex = -1;
            ddlRetentionRule.SelectedIndex = -1;
            hdnselected.Value = id.ToString();
            ModalPopupExtender1.Show();
        }
    }
    public void btnSaveRetension_click(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(hdnselected.Value);
        tblAllottee _allotte = Allottee.GetAllottee(id);
        if (_allotte != null)
        {
            DateTime dateofRetension;
            DateTime.TryParse(txtDateOfRetension.Text, out dateofRetension);
            DateTime dateofRetensionupto;
            DateTime.TryParse(txtdateofretensionupto.Text, out dateofRetensionupto);
            
            _allotte.Status = (int)AllotementStatus.Retension;
            _allotte.DateOfRetension = dateofRetension;
            _allotte.DateOfRetensionUpto = dateofRetensionupto;
            _allotte.Remarks = txtRemarks.Text.Trim();
            _allotte.RetentionReason = ddlRetentionRule.SelectedValue;
            Allottee.Update(_allotte);
            BindData();
            Quarters.UpdateQuarterStatus(_allotte.tblQuarter.Id, QuarterStatus.Retention);
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Allotement";
            _userhistory.description = _user.Username + " has marked retented with id " + id;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
            
        }
    }
    protected void grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            uspGetAllotteResult _allotte = e.Row.DataItem as uspGetAllotteResult;
            if (_allotte.status.HasValue && _allotte.status.Value == (int)AllotementStatus.Retension)
            {
                LinkButton lnkRetension = e.Row.FindControl("LinkButton1") as LinkButton;
                lnkRetension.Enabled = false;
                LinkButton lnkchgretension = e.Row.FindControl("lnkchgretension") as LinkButton;
                lnkRetension.Visible = true;//lnkchgretension
            }

        }
    }
    protected void btnVacant_click(object sender,EventArgs e)
    {
        int id = Convert.ToInt32(hdnselected.Value);
        tblAllottee _allotte = Allottee.GetAllottee(id);
        if (_allotte != null)
        {
            DateTime dateofVacation;
            DateTime.TryParse(txtDateOfVacation.Text, out dateofVacation);
            _allotte.Status = (int)AllotementStatus.vacant;
            _allotte.DateOfVacation = dateofVacation;
            Allottee.Update(_allotte);
            BindData();
            Quarters.UpdateQuarterStatus(_allotte.tblQuarter.Id, (int)QuarterStatus.Vacant,"Quarter marked as vacant.",txtDateOfVacation.Text);
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Allotement";
            _userhistory.description = _user.Username + " has marked vacant with id " + id;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
            
        }
    }
    protected void detail_click(object sender, EventArgs e)
    {
        lblMessage.Text = "Applications for Quarter Category: " + drpQuarterCatergory.SelectedItem.Text;
        lblMessage.Visible = true;
        BindData();
    }
    private void Bindcategory()
    {
        drpQuarterCatergory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCatergory.DataBind();
    }
    protected void ddlRetentionPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(txtDateOfRetension.Text))
        //{
        //    txtdateofretensionupto.Text = Convert.ToDateTime(txtDateOfRetension.Text).AddMonths(Convert.ToInt32(ddlRetentionPeriod.SelectedValue)).ToShortDateString();
        //    upPanelDateOfRetentionUpto.Update();
        //}
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(txtDateOfRetension.Text))
        //{
        //    txtdateofretensionupto.Text = Convert.ToDateTime(txtDateOfRetension.Text).AddMonths(Convert.ToInt32(ddlRetentionPeriod.SelectedValue)).ToShortDateString();
        //    upPanelDateOfRetentionUpto.Update();
        //}
    }
}
