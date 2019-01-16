using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LicencefeeSubmission : System.Web.UI.Page
{
    tblUser _user; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Licence Fee Recovery");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
        btnAddUpdate.Enabled = true;
        if (!Page.IsPostBack)
            Intilize();
    }
    protected void QuarterNumber_change(object sender, EventArgs e)
    {
        tblAllottee _allotte = Allottee.GetAllottee(txtQuarterNumber.Text);
        if (_allotte != null)
        {
            tblUser _user = Users.getUserByAAN(_allotte.AAN);
            if (_user.designation.HasValue)
                drpDesignation.SelectedValue = _user.designation.Value.ToString();
            if(_user.BaseOfficeId.HasValue)
            drpOffice.SelectedValue = _user.BaseOfficeId.Value.ToString();
            txtAllotteeAAN.Text = _allotte.AAN;
            txtUserName.Text = _user.fullName;
            drpQuarterCatergory.SelectedValue = _allotte.tblQuarter.tblQuarterCategory.Id.ToString();
            tblQuarterCategoryFee fee = LicenceFee.GetQuarterCategeryFee(_allotte.tblQuarter.tblQuarterCategory.Id);
            if (fee != null && fee.LicenceFee.HasValue)
            {
                txtLicenceFee.Text = fee.LicenceFee.Value.ToString();
            }
        }
        else
        {
            lblmessage.Text = "This quarter is not allotted to anyone yet";
            lblmessage.Visible = true;
            btnAddUpdate.Enabled = false;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        tblAllottee _allotte = Allottee.GetAllottee(txtQuarterNumber.Text);
        if (_allotte != null)
        {
            tbQuarterLicenceFee fee = new tbQuarterLicenceFee();
            fee.AAN = _allotte.AAN;
            fee.ActualFee = 0;
            fee.Fee = Convert.ToDecimal(txtLicenceFee.Text);
            fee.Month = Convert.ToDateTime(txtMonth.Text);
            fee.QuarterId = _allotte.tblQuarter.Id;
            fee.ActualFee = LicenceFee.GetLicenceFeebyCategory(_allotte.tblQuarter.Category);
            fee.Remarks = txtRemarks.Text;
            LicenceFee.SaveMonthLicencefee(fee);
            lblmessage.Text = "Information saved sucessfully!";
            lblmessage.Visible = true;
            empty();
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Licence fee";
            _userhistory.description = _user.Username + " has added Licence fee for quarter " + _allotte.tblQuarter.QuarterNumber;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
        }
        else
        {
            lblmessage.Text = "This quarter is not allotted to anyone yet";
            lblmessage.Visible = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["returnurl"]))
        { Response.Redirect("~/" + Request["returnurl"]); }
        else { Response.Redirect("~/admin/default.aspx"); }
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
    private void empty()
    {
        txtQuarterNumber.Text = "";
        txtMonth.Text = "";
        txtLicenceFee.Text = "";
        txtAllotteeAAN.Text = "";
        drpDesignation.SelectedIndex = -1;
        drpOffice.SelectedIndex = -1;
        drpQuarterCatergory.SelectedIndex = -1;
    }


}
