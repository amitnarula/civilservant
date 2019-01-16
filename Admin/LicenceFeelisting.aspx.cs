using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LicenceFeelisting : System.Web.UI.Page
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
        pnl.Visible = false;
        if (!Page.IsPostBack)
            Intilize();
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
   protected void  btnadd_Click(object sender,EventArgs e)
    {
        tblQuarter _quarter = Quarters.GetQuarter(txtQuarterNumber.Text);
        if (_quarter != null)
        {
            pnl.Visible = true;
            drpQuarterCatergory.SelectedValue = _quarter.tblQuarterCategory.Id.ToString();
            grdLicenceFee.DataSource = LicenceFee.GetLicencefee(txtQuarterNumber.Text);
            grdLicenceFee.DataBind();
            
        }
        else
        {
            lblmessage.Text = "Invalid quarter";
            lblmessage.Visible = true;
            return;
        }
        tblAllottee _allotte = Allottee.GetAllottee(txtQuarterNumber.Text);
        if (_allotte != null)
        {
            tblUser _user = Users.getUserByAAN(_allotte.AAN);
            if (_user.designation.HasValue)
                drpDesignation.SelectedValue = _user.designation.Value.ToString();
            if (_user.BaseOfficeId.HasValue)
                drpOffice.SelectedValue = _user.BaseOfficeId.Value.ToString();

            txtAllotteName.Text = _user.fullName;
            txtAllotteeAAN.Text = _allotte.AAN;
            if (_allotte.DateOfPossession.HasValue)
            {
                txtDateOfPossession.Text = _allotte.DateOfPossession.Value.ToShortDateString();
            }
            if (_allotte.DateOfVacation.HasValue)
            {
                txtActualDateOfVacation.Text = _allotte.DateOfVacation.Value.ToShortDateString();
            }
            if (_allotte.DateOfRetension.HasValue)
            {
                txtDueDateOfVacation.Text = _allotte.DateOfRetension.Value.AddMonths(8).ToShortDateString();
            }
            txtRetentionPeriod.Text = "8";
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Licence fee";
            _userhistory.description = _user.Username + " has added Licence fee for quarter " + _allotte.tblQuarter.QuarterNumber;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
        }
        
   }
}
