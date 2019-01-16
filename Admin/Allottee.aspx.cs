using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Allottee : System.Web.UI.Page
{
    int id;
    long applicationid;
    tblUser _user;
    long changeReuestId;
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

        btnAddUpdate.Text = "Add";
        if (!Page.IsPostBack)
        {
            Intilize();

        }
        if (!string.IsNullOrEmpty(Request["id"]))
        {
            int.TryParse(Request["id"], out id);
            if (id <= 0)
            {
                Response.Redirect("~/admin/allottees.aspx");
                //Response.End();
            }
            btnAddUpdate.Text = "Update";
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
        if (!string.IsNullOrEmpty(Request["Applicationid"]))
        {
            Int64.TryParse(Request["Applicationid"], out applicationid);
            if (applicationid <= 0)
            {
                Response.Redirect("~/admin/allottees.aspx");
                //Response.End();
            }
            btnAddUpdate.Text = "Add";
            if (!Page.IsPostBack)
            {
                BindApplicationData();
            }
        }
        if (!string.IsNullOrEmpty(Request["ChangeRequestID"]))
        {
            Int64.TryParse(Request["ChangeRequestID"], out changeReuestId);
            long.TryParse(Request["appid"], out applicationid);
            if (changeReuestId <= 0)
            {
                Response.Redirect("~/admin/allottees.aspx");
                //Response.End();
            }
            btnAddUpdate.Text = "Add Change Request";
            if (!Page.IsPostBack)
            {
                BindRequestData();
            }
        }
    }

    protected void bindQuarter()
    {
        drpQuarter.DataSource = changeReuestId > 0 ? Quarters.GetQuartersByChangeRequestId(changeReuestId) :
        Quarters.GetAllVacantQuarters(Convert.ToInt64(drpQuarterCatergory.SelectedValue));
        drpQuarter.DataBind();
        drpQuarter.Items.Insert(0, new ListItem("-select--", "-1"));
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {

        tblAllottee _Allottee = null;
        if (id > 0)
        {
            _Allottee = Allottee.GetAllottee(id);
        }
        else
        {
            tblAllottee a = Allottee.GetAllotteeByAAN(txtAllotteeName.Text);
            if (a != null)
            {
                _Allottee = a;
                //lblmessage.Text = "User has already allotted quarter!";
                //lblmessage.Visible = true;
                //return;
            }

            if (a == null)
            {
                _Allottee = new tblAllottee();
            }

            tblQuarter quarter = Quarters.GetQuarter(drpQuarter.SelectedValue);
            if (quarter == null)
            {
                lblmessage.Text = "Please enter valid number";
                lblmessage.Visible = true;
                return;

            }
            else if (quarter.Status.HasValue && quarter.Status != (int)QuarterStatus.Vacant)
            {
                lblmessage.Text = "Quarter is not vacant!";
                lblmessage.Visible = true;
                return;

            }
            quarter.DateOfAllottement = DateTime.Now;
            quarter.Status = (int)QuarterStatus.Alloted;
            Quarters.Save(quarter);

            //_Allottee = new tblAllottee();

        }
        _Allottee.QuarterNumber = drpQuarter.SelectedValue;
        _Allottee.AAN = txtAllotteeName.Text;
        _Allottee.ApplicationId = applicationid;
        _Allottee.Status = btnAddUpdate.Text == "Add Change Request" ? (int)AllotementStatus.ChangeRequested : (int)AllotementStatus.Possessed;
        _Allottee.DateOfAllotement = DateTime.Now;
        //_Allottee.OfficeId = Convert.ToInt32(drpOffice.SelectedValue);
        //_Allottee.Designation =Convert.ToInt32( drpDesignation.SelectedValue);
        //_Allottee.DateOfJoining = Convert.ToDateTime(txtDoj.Text);
        //_Allottee.DateOfRetirement = Convert.ToDateTime(txtDor.Text);
        //_Allottee.Name = txtAllotteeName.Text;
        tbluserhistory _userhistory = new tbluserhistory();
        _userhistory.Action = "Allotement";
        _userhistory.description = _user.Username + " has allotted quarter " + _Allottee.QuarterNumber + " to user " + _Allottee.AAN;
        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);

        Allottee.Save(_Allottee);
        if (applicationid > 0)
            AllotementApplications.UpdateApplicationStaus(applicationid, btnAddUpdate.Text == "Add Change Request" ? ApplicationStatus.ChangeRequested : ApplicationStatus.Allotted);
        if (!string.IsNullOrEmpty(Request["returnurl"]))
        { Response.Redirect("~/" + Request["returnurl"]);}
        else { Response.Redirect("~/admin/Allottees.aspx"); }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["returnurl"]))
        { Response.Redirect("~/" + Request["returnurl"]); }
        else { Response.Redirect("~/admin/Allottees.aspx"); }
    }
    private void Intilize()
    {
        drpDesignation.DataSource = Allottee.GetDesignations();
        drpDesignation.DataBind();
        drpOffice.DataSource = Offices.GetAlloffices();
        drpOffice.DataBind();
        drpQuarterCatergory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCatergory.DataBind();
        
    }
    private void BindData()
    {
        tblAllottee quarter = Allottee.GetAllottee(id);
        if (quarter != null)
        {
            //
            //drpDesignation.SelectedValue = quarter.Designation.ToString();
            tblUser user = Users.getUserByAAN(quarter.AAN);
            drpQuarterCatergory.SelectedValue = quarter.tblQuarter.Category.ToString();
            drpOffice.SelectedValue = user.BaseOfficeId.ToString();
            txtAllotteeName.Text = user.AAN;
            if(user.DateOfRetirement.HasValue)
            txtDor.Text = user.DateOfRetirement.Value.ToShortDateString();
            if(user.DateOfJoining.HasValue)
            txtDoj.Text = user.DateOfJoining.Value.ToShortDateString();
            bindQuarter();
            drpQuarter.SelectedValue = quarter.QuarterNumber;
            drpQuarterCatergory.Enabled = false;
            drpOffice.Enabled = false;
            drpDesignation.Enabled = false;

        }
    }
private void BindApplicationData()
{
    tbAllotmentApplication appication = AllotementApplications.GetApplication(applicationid);
    tblUser user = Users.getUserByAAN(appication.tblUser.AAN);
    drpOffice.SelectedValue = user.BaseOfficeId.ToString();
    drpQuarterCatergory.SelectedValue = appication.QuarterCategory.ToString();
    txtDor.Text = user.DateOfRetirement.Value.ToShortDateString();
    txtDoj.Text = user.DateOfJoining.Value.ToShortDateString();
    txtAllotteeName.Text = user.AAN;
    bindQuarter();
    drpQuarterCatergory.Enabled = false;
    drpOffice.Enabled = false;
    drpDesignation.Enabled = false;
}
private void BindRequestData()
{
    DataClassesDataContext datacontext = new DataClassesDataContext();
    var changeReuest = from request in datacontext.tblChangeRequests where request.Id == changeReuestId select request;
    tblChangeRequest _request = changeReuest.FirstOrDefault();
    tblUser user = Users.getUserByAAN(_request.AAN);
    drpOffice.SelectedValue = user.BaseOfficeId.ToString();
    drpQuarterCatergory.SelectedValue = _request.QuarterCategory.ToString();
    if(user.DateOfRetirement.HasValue)
    txtDor.Text = user.DateOfRetirement.Value.ToShortDateString();
    if(user.DateOfJoining.HasValue)
    txtDoj.Text = user.DateOfJoining.Value.ToShortDateString();
    txtAllotteeName.Text = user.AAN;
    bindQuarter();
    drpQuarterCatergory.Enabled = false;
    drpOffice.Enabled = false;
    drpDesignation.Enabled = false;
}
}
