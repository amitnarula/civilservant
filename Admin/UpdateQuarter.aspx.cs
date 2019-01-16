using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UpdateQuarter : System.Web.UI.Page
{
    private void SearchAndBindQuarters(string quarterNumber)
    {
        List<tblQuarter> lstQuarter = Quarters.GetQuartersByQuarterNumber(quarterNumber);
        grdQuarters.DataSource = lstQuarter;
        grdQuarters.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Quarters");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchAndBindQuarters(txtSearch.Text.Trim());
    }
    protected void grdQuarters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateStatus")
        {
            Button btnUpdateStatus = e.CommandSource as Button;
            if (btnUpdateStatus != null)
            {
                long quarterId = Convert.ToInt64(e.CommandArgument);
                GridViewRow selectedRow = btnUpdateStatus.NamingContainer as GridViewRow;
                if (selectedRow != null)
                {
                    int status = 0;
                    RadioButton radBtnCPWD = (RadioButton)selectedRow.FindControl("radBtnCPWD");
                    RadioButton radBtnDamaged = (RadioButton)selectedRow.FindControl("radBtnDamaged");
                    RadioButton radBtnVacant = (RadioButton)selectedRow.FindControl("radBtnVacant");
                    RadioButton radBtnSurrender = (RadioButton)selectedRow.FindControl("radBtnSurrender");
                    TextBox txtDateOfVacation = (TextBox)selectedRow.FindControl("txtDateOfVacation");

                    TextBox txtRemarks = (TextBox)selectedRow.FindControl("txtRemarks");

                    if (radBtnCPWD.Checked)
                    {
                        status = (int)QuarterStatus.CPWD;
                    }
                    else if (radBtnDamaged.Checked)
                    {
                        status = (int)QuarterStatus.Damaged;
                    }
                    else if (radBtnVacant.Checked)
                    {
                        status = (int)QuarterStatus.Vacant;
                    }
                    else if (radBtnSurrender.Checked)
                    {
                        status = (int)QuarterStatus.Surrender;
                    }


                    Quarters.UpdateQuarterStatus(quarterId, status, txtRemarks.Text, txtDateOfVacation.Text);
                }
                
            }
        }
        SearchAndBindQuarters(txtSearch.Text.Trim());
    }
    protected void grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnStatus = e.Row.FindControl("hdnStatus") as HiddenField;
            HiddenField hdnDateOfVacation = e.Row.FindControl("hdnDateOfVacation") as HiddenField;
            RadioButton radStatus;
            TextBox txtDateOfVacation = e.Row.FindControl("txtDateOfVacation") as TextBox;

            if(!string.IsNullOrEmpty(hdnDateOfVacation.Value))
            {
                txtDateOfVacation.Attributes.Add("value", DateTime.Parse(hdnDateOfVacation.Value).ToString("dd/MM/yyyy"));
            }

            txtDateOfVacation.Style.Add("display", "none");
            int status = Convert.ToInt32(hdnStatus.Value);

            if (status == (int)QuarterStatus.CPWD)
            {
                txtDateOfVacation.Style.Add("display", "none");
                radStatus = e.Row.FindControl("radBtnCPWD") as RadioButton;
                radStatus.Checked = true;
            }
            else if (status == (int)QuarterStatus.Damaged)
            {
                txtDateOfVacation.Style.Add("display", "none");
                radStatus = e.Row.FindControl("radBtnDamaged") as RadioButton;
                radStatus.Checked = true;
            }
            else if (status == (int)QuarterStatus.Vacant)
            {
                txtDateOfVacation.Style.Add("display", "inline");
                radStatus = e.Row.FindControl("radBtnVacant") as RadioButton;
                radStatus.Checked = true;
            }
            else if (status == (int)QuarterStatus.Surrender)
            {
                txtDateOfVacation.Style.Add("display", "none");
                radStatus = e.Row.FindControl("radBtnSurrender") as RadioButton;
                radStatus.Checked = true;
            }

        }
    }
}