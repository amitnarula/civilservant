using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_circulars : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindData();
            Bindcategory();
        }
    }
    private void BindData()
    {
        grdQuarters.DataSource = AllotementApplications.GetPendingApplications(ApplicationStatus.Verified, Convert.ToInt64(drpQuarterCatergory.SelectedValue));
        grdQuarters.DataBind();

    }

    private bool IsCategoryPublished(long categoryId)
    {
        DataClassesDataContext dc = new DataClassesDataContext();
        var catWorkflow = dc.tblCategoryWorkflows.Where(x => x.CategoryId == categoryId).SingleOrDefault();

        if (catWorkflow != null)
        {
            if (catWorkflow.Status == 3) //If category is published
                return true;
            return false;
        }
        return false;
    }

    private void Bindcategory()
    {
        drpQuarterCatergory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCatergory.DataBind();
    }
    protected void detail_click(object sender, EventArgs e)
    {
        lblMessage.Text = "Applications for Quarter Category: " + drpQuarterCatergory.SelectedItem.Text;
        lblMessage.Visible = true;

        if (IsCategoryPublished(Convert.ToInt64(drpQuarterCatergory.SelectedValue)))
            BindData();
    }
}
