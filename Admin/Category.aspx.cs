using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Category : System.Web.UI.Page
{
    DataClassesDataContext dc = new DataClassesDataContext();
    
    private void BindCategoryStatus()
    {
        List<tblQuarterCategory> lstQuarterCategory = dc.tblQuarterCategories.ToList();
        lstQuarterCategory.ForEach((category) => {

            if (category.Id == 1)
            {
                chkType1.Checked = category.IsActive;
            }
            if (category.Id == 2)
            {
                chkType2.Checked = category.IsActive;
            }
            if (category.Id == 3)
            {
                chkType3.Checked = category.IsActive;
            }
            if (category.Id == 4)
            {
                chkType4.Checked = category.IsActive;
            }
            if (category.Id == 5)
            {
                chkType5.Checked = category.IsActive;
            }
            if (category.Id == 6)
            {
                chkType6.Checked = category.IsActive;
            }
            if (category.Id == 7)
            {
                chkType7.Checked = category.IsActive;
            }

        });
    }

    private void SaveCategoryStatus()
    {
        List<tblQuarterCategory> lstQuarterCategory = dc.tblQuarterCategories.ToList();
        lstQuarterCategory.ForEach((category) =>
        {

            if (category.Id == 1)
            {
                category.IsActive = chkType1.Checked;
            }
            if (category.Id == 2)
            {
                category.IsActive = chkType2.Checked;
            }
            if (category.Id == 3)
            {
                category.IsActive = chkType3.Checked;
            }
            if (category.Id == 4)
            {
                category.IsActive = chkType4.Checked;
            }
            if (category.Id == 5)
            {
                category.IsActive = chkType5.Checked;
            }
            if (category.Id == 6)
            {
                category.IsActive = chkType6.Checked;
            }
            if (category.Id == 7)
            {
                category.IsActive = chkType7.Checked;
            }

            dc.SubmitChanges();

        });

        lblMessage.Text = "Saved successfully";
        lblMessage.Visible = true;
        
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

        if (!Page.IsPostBack)
        {
            BindCategoryStatus();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveCategoryStatus();
        BindCategoryStatus();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        var dc = new DataClassesDataContext();
        var tbCatWorkFlow = dc.tblCategoryWorkflows.ToList();

        foreach (var tcw in tbCatWorkFlow)
        {
            tcw.Status = -1;
        }
        dc.SubmitChanges();

        lblMessage.Text = "All category status changed sucessfully";
        lblMessage.Visible = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Default.aspx");
    }
}