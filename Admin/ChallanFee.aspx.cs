using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChallanFee : System.Web.UI.Page
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
    }
    protected void QuarterNumber_change(object sender, EventArgs e)
    {
        tblAllottee _allotte = Allottee.GetAllottee(txtQuarterNumber.Text);
        if (_allotte != null)
        {
            tblUser _user = Users.getUserByAAN(_allotte.AAN);
            txtAllotteeAAN.Text = _allotte.AAN;

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
            decimal charge = 0;
            decimal.TryParse(txtPayment.Text, out charge);
            tbChallan quarterDamage = new tbChallan();
            quarterDamage.AAN = _allotte.AAN;
            quarterDamage.Payment = charge;
            quarterDamage.ChallanNumber= txtChallanNumber.Text;
            quarterDamage.BankName = txtBankName.Text;
            quarterDamage.DateRecevied = Convert.ToDateTime(txtMonth.Text);
            quarterDamage.QuarterNumber = _allotte.tblQuarter.QuarterNumber;
            quarterDamage.Remarks = txtRemarks.Text;
            Quarters.AddChallan(quarterDamage);
            lblmessage.Text = "Information saved sucessfully!";
            lblmessage.Visible = true;
            empty();
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Licence fee";
            _userhistory.description = _user.Username + " has added quarter damage charges for quarter " + _allotte.tblQuarter.QuarterNumber;
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
    private void empty()
    {
        txtQuarterNumber.Text = "";
        txtMonth.Text = "";
        txtAllotteeAAN.Text = "";
        txtBankName.Text = "";
        txtChallanNumber.Text = "";
        txtPayment.Text = "";
        txtRemarks.Text = "";

    }
}
