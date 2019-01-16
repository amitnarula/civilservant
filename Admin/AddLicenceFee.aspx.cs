using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddLicenceFee : System.Web.UI.Page
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
        lblmessage.Visible = false;
        if (!Page.IsPostBack)
        {
            Intilize();
            QuarterCategory_selectedindex(null, null);
        }
    }
    private void Intilize()
    {
        drpQuarterCat.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCat.DataBind();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        tblQuarterCategoryFee categoryfee = new tblQuarterCategoryFee();
        //categoryfee.LastUpdatedBy = 1;
        //categoryfee.Month = Convert.ToDateTime(txtmonth.Text);
        categoryfee.QuarterCategoryId = Convert.ToInt32(drpQuarterCat.SelectedValue);
        categoryfee.LicenceFee = Convert.ToDouble(txtLicenceFee.Text);
        categoryfee.EffectiveDate = Convert.ToDateTime(txtEffectiveDate.Text);
        LicenceFee.Save(categoryfee);
        tbluserhistory _userhistory = new tbluserhistory();
        _userhistory.Action = "Licence fee";
        //_userhistory.description = _user.Username + " has added Licence fee for quarter category " + categoryfee.tblQuarterCategory.Name;
        
        // Quartercategory name is selected from the drop down, not from database table..
        _userhistory.description = _user.Username + " has added Licence fee for quarter category " + drpQuarterCat.SelectedItem.Text;

        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);
        lblmessage.Visible = true;
        lblmessage.Text = "Information saved successfully!";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/default.aspx");
    }

    protected void QuarterCategory_selectedindex(object sender, EventArgs e)
    {
        tblQuarterCategoryFee o=LicenceFee.GetQuarterCategeryFee(Convert.ToInt64(drpQuarterCat.SelectedValue));
        if(o!=null){
            if (o.LicenceFee.HasValue)
                txtLicenceFee.Text = o.LicenceFee.Value.ToString();
        }
    }
}
