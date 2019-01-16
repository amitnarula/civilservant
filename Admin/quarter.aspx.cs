using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_quarter : System.Web.UI.Page
{
    int id;
    tblUser _user; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
             _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Quarters");
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
                Response.Redirect("~/admin/Quarters.aspx");
                Response.End();
            }
            btnAddUpdate.Text = "Update";
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }
    }

    private void Intilize()
    {
        drpQuarterCat.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCat.DataBind();
        //drpFloors.DataSource = Quarters.GetQuarterFloors();
        //drpFloors.DataBind();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        tblQuarter quarter;
        if (id > 0)
        {
            quarter = Quarters.GetQuarter(id);
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Quarter";
            _userhistory.description = _user.Username + " has updated quarter " + txtQuarterNo.Text;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);
        }
        else
        {
            quarter = new tblQuarter();
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Quarter";
        _userhistory.description = _user.Username + " has added quarter " + txtQuarterNo.Text;
        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);
            
        }
        quarter.QuarterNumber = txtQuarterNo.Text;
        quarter.Category = Convert.ToInt32(drpQuarterCat.SelectedValue);
        //quarter.Floor = Convert.ToInt32(drpFloors.SelectedValue);
        quarter.Floor = 1;
        quarter.Status = 0;
        Quarters.Save(quarter);

        Response.Redirect("~/admin/Quarters.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    // code to redirect 

        Response.Redirect("~/admin/Quarters.aspx");
    }

    private void BindData()
    {
        tblQuarter quarter = Quarters.GetQuarter(id);
        if(quarter!=null)
        {
            //drpFloors.SelectedValue = quarter.Floor.ToString();
            drpQuarterCat.SelectedValue = quarter.Category.ToString();
            txtQuarterNo.Text = quarter.QuarterNumber;
        }
    }
}
