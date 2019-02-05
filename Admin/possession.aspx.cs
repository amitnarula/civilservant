using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_possession : System.Web.UI.Page
{
    tblUser _user; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Possessions");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
        if (!Page.IsPostBack)
        {
            Bindcategory();
        }
    }
    private void Bindcategory()
    {
        drpQuarterCatergory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCatergory.DataBind();
    }
    private void BindData()
    {
        List<AllotementApplication> lstAllotmentApplications = AllotementApplications.GetAllotedUsers(ApplicationStatus.Allotted, Convert.ToInt64(drpQuarterCatergory.SelectedValue));

        if (lstAllotmentApplications != null)
            lstAllotmentApplications = lstAllotmentApplications.OrderBy(x => x.UserName).ToList(); //Order by

        grdQuarters.DataSource = lstAllotmentApplications;
        grdQuarters.DataBind();

    }

    protected void gridCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Allotte")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
           // AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Allotted);
            Response.Redirect("~/admin/Allottee.aspx?Applicationid=" + id + "&returnurl=admin/Prioritizeapplications.aspx");
        }
        else if (e.CommandName == "Possesed")
        {
            hdnSelected.Value = e.CommandArgument.ToString();
            tblAllottee _allotte = Allottee.getAllotteByApplicationid(Convert.ToInt64(e.CommandArgument));
            txtName.Text = AllotementApplications.GetApplication(Convert.ToInt64(e.CommandArgument)).tblUser.fullName;
            txtQuarternumber.Text = _allotte.QuarterNumber;

            pop.Show();
        }
        else if (e.CommandName == "Withdraw")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.withdraw);
            Allottee.UpdateAllottementStatus(id, AllotementStatus.withdraw);
            tblAllottee _allotte = Allottee.getAllotteByApplicationid(id);
            if (_allotte != null)
            {
                long quarterid = _allotte.tblQuarter.Id;
                _allotte.Status = (int)AllotementStatus.withdraw;
                _allotte.DateOfwithdraw = DateTime.Now;
                 Allottee.Update(_allotte);

                Quarters.UpdateQuarterStatus(_allotte.tblQuarter.Id, QuarterStatus.Vacant);
            
            }
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Withdraw";
            _userhistory.description = _user.Username + " has marked Withdraw application with " + id;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
           // Response.Redirect("~/admin/Allottee.aspx?Applicationid=" + id + "&returnurl=admin/Prioritizeapplications.aspx");
        }
        else if (e.CommandName == "CancelApplication")
        { 
            //Move quarter to vacant list
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.rejected);
            Allottee.UpdateAllottementStatus(id, AllotementStatus.vacant);
            tblAllottee _allotte = Allottee.getAllotteByApplicationid(id);
            if (_allotte != null)
            {
                long quarterid = _allotte.tblQuarter.Id;
                _allotte.Status = (int)AllotementStatus.vacant;
                _allotte.DateOfVacation = DateTime.Now;
                Allottee.Update(_allotte);

                Quarters.UpdateQuarterStatus(_allotte.tblQuarter.Id, QuarterStatus.Vacant);

            }
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Cancel";
            _userhistory.description = _user.Username + " has marked cancel application with " + id;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
            
        }
        BindData();
        //DataNavigateUrlFormatString="~/admin/Allottee.aspx?Applicationid={0}&returnurl=admin/Prioritizeapplications.aspx" 
    }
    protected void btnsaveMember_click(object sender, EventArgs e)
    {
        possession();
        BindData();
    }
    private void possession()
    {
        long id;
        Int64.TryParse(hdnSelected.Value, out id);
        AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Pos);
        tbAllotmentApplication app = AllotementApplications.GetApplication(id);
        tblAllottee _allotte = Allottee.getAllotteByApplicationid(id);
        if (_allotte != null)
        {
            DateTime _dateofPossession;
            DateTime.TryParse(txtDateOfPossession.Text, out _dateofPossession);
            _allotte.Status = (int)AllotementStatus.Possessed;
            _allotte.DateOfPossession = _dateofPossession;
            Allottee.Update(_allotte);

        }

        if (id > 0)
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Pos);

        using (DataClassesDataContext dc = new DataClassesDataContext())
        {
            var changeRequests = dc.tblChangeRequests.Where(x => x.AAN == _allotte.AAN).ToList();
            //mark all change requests to delete now
            if (changeRequests.Any())
            {
                changeRequests.ForEach(x => {
                    x.Status = (int)ChangeRequestStatus.Deleted;
                });
            }
            dc.SubmitChanges();

        }


        tbluserhistory _userhistory = new tbluserhistory();
        _userhistory.Action = "Possession";
        _userhistory.description = _user.Username + " has marked possesed application with " + id;
        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);
    }
    protected void detail_click(object sender, EventArgs e)
    {
        lblMessage.Text = "Applications for Quarter Category: " + drpQuarterCatergory.SelectedItem.Text;
        lblMessage.Visible = true;
        BindData();
    }
    protected void grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnDateOfAllotement = e.Row.FindControl("hdnDateOfAllotement") as HiddenField;
            
            if (!string.IsNullOrEmpty(hdnDateOfAllotement.Value))
            {
                DateTime dateOfAllotement = Convert.ToDateTime(hdnDateOfAllotement.Value);

                if (DateTime.Now.Subtract(dateOfAllotement).Days > 30) //If possession pending for more than 30 days from the date of allotment
                {
                    LinkButton lnkPos = e.Row.FindControl("lnkPos") as LinkButton;
                    LinkButton lnkWithdraw = e.Row.FindControl("lnkWithdraw") as LinkButton;
                    LinkButton lnkCancel = e.Row.FindControl("lnkCancel") as LinkButton;

                    lnkPos.Visible = false;
                    lnkWithdraw.Visible = false;
                    lnkCancel.Visible = true;
                }
            }
        }
    }
}
