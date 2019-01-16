using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Licencefee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        tblQuarter quarter = Quarters.GetQuarter(txtQuarterNumber.Text);
        if (quarter == null)
        {
            lblmessage.Text = "invalid quarter number!";
            lblmessage.Visible = true;
            return;
        }
        tbQuarterLicenceFee fee = new tbQuarterLicenceFee();
        fee.QuarterId = quarter.Id;
        decimal Lfee=0;
        decimal.TryParse(txtLicenceFee.Text, out Lfee);
        fee.Fee = Lfee;
        DateTime dMonth;
        DateTime.TryParse(txtmonth.Text, out dMonth);
        if (dMonth == DateTime.MinValue)
        {
            lblmessage.Text = "invalid Date!";
            lblmessage.Visible = true;
            return;
        }
        DataClassesDataContext datacontext = new DataClassesDataContext();
        datacontext.tbQuarterLicenceFees.InsertOnSubmit(fee);
        datacontext.SubmitChanges();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
