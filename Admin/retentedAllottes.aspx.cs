using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_retentedAllottes : System.Web.UI.Page
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
        grdQuarters.DataSource = Allottee.GetRetentedAllotte(Convert.ToInt64(drpQuarterCatergory.SelectedValue));
        grdQuarters.DataBind();
    }

    protected void ddlRetentionPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDateOfRetension.Text))
        {
            txtdateofretensionupto.Text = Convert.ToDateTime(txtDateOfRetension.Text).AddMonths(Convert.ToInt32(ddlRetentionPeriod.SelectedValue)).ToShortDateString();
            upPanelDateOfRetentionUpto.Update();
        }
    }

    public void grdAllotte_command(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Retention")
        {
            var gridRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
            HiddenField hdnDateOfRetention = gridRow.FindControl("hdnDateOfRetention") as HiddenField;

            string dateOfRetention = Convert.ToDateTime(hdnDateOfRetention.Value).ToShortDateString();

            txtDateOfRetension.Text = string.Empty;
            txtdateofretensionupto.Text = string.Empty;
            ddlRetentionPeriod.SelectedIndex = -1;
            ddlRetentionRule.SelectedIndex = -1;

            txtDateOfRetension.Text = dateOfRetention;

            long id = Convert.ToInt64(e.CommandArgument);
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
            _allotte.DateOfRetensionUpto = dateofRetensionupto;
            
            _allotte.Status = (int)AllotementStatus.Retension;
            _allotte.DateOfRetension = dateofRetension;
            _allotte.RetentionReason = ddlRetentionRule.SelectedValue;
            Allottee.Update(_allotte);
            BindData();
            Quarters.UpdateQuarterStatus(_allotte.tblQuarter.Id, QuarterStatus.Vacant);
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
}