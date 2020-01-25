using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegratedMessages;

public partial class Admin_AllotteQuarter : System.Web.UI.Page
{
    tblUser _user;
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlAllocationMode.SelectedIndexChanged += new EventHandler(ddlAllocationMode_SelectedIndexChanged);
        grdQuarters.RowDataBound += new GridViewRowEventHandler(grdQuarters_RowDataBound);

        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
             _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Priority list");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
        if (!Page.IsPostBack)
        {
            //BindData();
            Bindcategory();

            if (!string.IsNullOrEmpty(Request.QueryString["selectedCategory"]))
            {
                //Retaining state
                drpQuarterCatergory.SelectedValue = Convert.ToString(Request.QueryString["selectedCategory"]);
                ddlAllocationMode.SelectedValue = "Manual";
                BindData();
                
            }
        }

    }

    void grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlAllocationMode.SelectedValue == "Manual")
            {
                LinkButton lnkAllotte = e.Row.FindControl("lnkAllotte") as LinkButton;
                LinkButton lnkManualAllot = e.Row.FindControl("lnkManualAllot") as LinkButton;

                lnkAllotte.Visible = false;
                lnkManualAllot.Visible = true;
            }
        }
    }

    void ddlAllocationMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPriorityListGrid();
    }

    private void BindData()
    {
        BindPriorityListGrid();

        BindStatusItems();

        if (grdQuarters.Rows.Count > 0)
        {
            pnlControls.Visible = true;
        }
        else
            pnlControls.Visible = false;
    }

    private void BindPriorityListGrid()
    {
        List<AllotementApplication> lstAllottmentApplication = AllotementApplications.GetPendingApplications(ApplicationStatus.Verified, Convert.ToInt64(drpQuarterCatergory.SelectedValue));
        //lstAllottmentApplication = lstAllottmentApplication.OrderBy(x => x.UserName).ToList(); //Order by name

        long floor = 2; //First floor

        if (!string.IsNullOrEmpty(Request["type"]) && Convert.ToString(Request["type"]) == "medical")
        {
            floor = 1; //Ground floor
        }

        List<Quarerinfo> lstQuarterInfo = Quarters.GetAllVacantQuarters(Convert.ToInt64(drpQuarterCatergory.SelectedValue));

        if (floor == 2)
        {
            lstQuarterInfo = lstQuarterInfo.Where(quarter => quarter.QuarterNumber.Contains('A') ||
                quarter.QuarterNumber.Contains('B') ||
                quarter.QuarterNumber.Contains('C') ||
                quarter.QuarterNumber.Contains('D')).ToList();
        }
        else if (floor == 1)
        {
            lstQuarterInfo = lstQuarterInfo.Where(quarter => !(quarter.QuarterNumber.Contains('A') ||
                quarter.QuarterNumber.Contains('B') ||
                quarter.QuarterNumber.Contains('C') ||
                quarter.QuarterNumber.Contains('D'))).ToList();

            if (lstAllottmentApplication != null)
                //Filter allotment applications as per medical conditions
                lstAllottmentApplication = lstAllottmentApplication.Where(x => x.MedicalCategory).ToList();
        }

        lstQuarterInfo = lstQuarterInfo.Take(lstAllottmentApplication.Count).ToList(); //Taking as many records there

        int totalApplications = Math.Min(lstAllottmentApplication.Count, lstQuarterInfo.Count);

        for (int count = 0; count < totalApplications; count++)
        {
            string quarterNumber = "N/A";
            if (lstQuarterInfo.Any() && lstQuarterInfo[count] != null && !string.IsNullOrEmpty(lstQuarterInfo[count].QuarterNumber))
            {

                quarterNumber = lstQuarterInfo[count].QuarterNumber;
            }
            lstAllottmentApplication[count].QuarterNumber = quarterNumber;
        }

        grdQuarters.DataSource = lstAllottmentApplication;
        grdQuarters.DataBind();
    }

    private void BindStatusItems()
    {
        btnApproved.Enabled = false;
        btnPublished.Enabled = false;
        btnValidated.Enabled = false;
        btnVerified.Enabled = false;

        btnVerified.Text = "Verify";
        btnValidated.Text = "Validate";
        btnApproved.Text = "Approve";
        btnPublished.Text = "Publish";

        //Bind category status and change status buttons
        tblCategoryWorkflow catWorkflow = new DataClassesDataContext().tblCategoryWorkflows.Where(cw => cw.CategoryId == Convert.ToInt64(drpQuarterCatergory.SelectedValue)).SingleOrDefault();

        //AAO=6, SAO=7, DAG=8
        int currentUserRoleId = _user.Roleid.Value;

        if (catWorkflow.Status == -1)
        {
            btnVerified.Enabled = currentUserRoleId == 6; //AAO
            btnVerified.Text = "Verify";
            btnValidated.Text = "Validate";
            btnApproved.Text = "Approve";
            btnPublished.Text = "Publish";
        }
        else if (catWorkflow.Status == 0)
        {
            btnVerified.Text = "Verified";
            btnValidated.Enabled = currentUserRoleId == 7; //SAO
        }
        else if (catWorkflow.Status == 1)
        {
            btnVerified.Text = "Verified";
            btnValidated.Text = "Validated";
            btnApproved.Enabled = currentUserRoleId == 8; //DAG
        }
        else if (catWorkflow.Status == 2)
        {
            btnVerified.Text = "Verified";
            btnValidated.Text = "Validated";
            btnApproved.Text = "Approved";
            btnPublished.Enabled = currentUserRoleId == 6; //AAO will finally Publish
        }
        else if (catWorkflow.Status == 3)
        {
            btnVerified.Text = "Verified";
            btnValidated.Text = "Validated";
            btnApproved.Text = "Approved";
            btnPublished.Text = "Published";

            btnPublished.Enabled = false;
            btnApproved.Enabled = false;
            btnValidated.Enabled = false;
            btnVerified.Enabled = false;

            //Show grid view action column accordingly
            grdQuarters.Columns[10].Visible = true;

            BindPriorityListGrid();
        }
        
    }

    protected void gridCommand(object sender,GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Allotte")
        {
            long id;
            string aan=string.Empty;
            string quarterNumber=string.Empty;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            //AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Allotted);

            tblAllottee _Allottee=null;
            //if (id > 0)
            //{
              //  _Allottee = Allottee.GetAllottee(id);
            //}
            //else
            //{
                var gridRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

                var hidAAN = gridRow.FindControl("hidAAN") as HiddenField;
                aan=hidAAN.Value;
                tblAllottee a = Allottee.GetAllotteeByAAN(aan);

                if (a != null)
                {
                    //lblMessage.Text = "User has already allotted quarter!";
                    //lblMessage.Visible = true;
                    //return;
                    _Allottee = a;
                }
                else
                {
                    _Allottee = new tblAllottee();
                }
                Label lblQuarterNumber = gridRow.FindControl("lblQuarterNumber") as Label;
                quarterNumber = lblQuarterNumber.Text;

                tblQuarter quarter = Quarters.GetQuarter(quarterNumber);
                if (quarter == null)
                {
                    lblMessage.Text = "Quarter number selected is not valid";
                    lblMessage.Visible = true;
                    return;

                }
                else if (quarter.Status.HasValue && quarter.Status != (int)QuarterStatus.Vacant)
                {
                    lblMessage.Text = "Quarter is not vacant!";
                    lblMessage.Visible = true;
                    return;

                }
                quarter.DateOfAllottement = DateTime.Now;
                quarter.Status = (int)QuarterStatus.Alloted;
                Quarters.Save(quarter);

                //_Allottee = new tblAllottee();

            //}
            _Allottee.QuarterNumber = quarterNumber;
            _Allottee.AAN = aan;
            _Allottee.ApplicationId = id;
            _Allottee.Status = (int)AllotementStatus.Allotted;
            _Allottee.DateOfAllotement = DateTime.Now;
            //_Allottee.OfficeId = Convert.ToInt32(drpOffice.SelectedValue);
            //_Allottee.Designation =Convert.ToInt32( drpDesignation.SelectedValue);
            //_Allottee.DateOfJoining = Convert.ToDateTime(txtDoj.Text);
            //_Allottee.DateOfRetirement = Convert.ToDateTime(txtDor.Text);
            //_Allottee.Name = txtAllotteeName.Text;
            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "Allotement";
            _userhistory.description = _user.Username + " has allotted quarter " + _Allottee.QuarterNumber + " to user " + _Allottee.AAN;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);

            Allottee.Save(_Allottee);
            if (id > 0)
            {
                AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Allotted);

                try
                {
                    //SEND SMS
                    new IntegratedMessageSender().SendMessage("QUARTER_ALLOTTED", _Allottee.QuarterNumber, AllotementApplications.GetApplicationByAANIrrespectiveOfSubmissionDate(aan).ContactNumber);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            BindData();

            ///Response.Redirect("~/admin/Allottee.aspx?Applicationid="+id+"&returnurl=admin/Prioritizeapplications.aspx");
        }
        else if (e.CommandName == "Withdraw")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.withdraw);
            BindData();
            // AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Pos);
            
        }
        else if (e.CommandName == "Allotte")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            //AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.withdraw);
            Response.Redirect("~/admin/Allottee.aspx?Applicationid=" + id + "&returnurl=admin/Prioritizeapplications.aspx");
        }
        else if (e.CommandName == "AllotteManual")
        {
            //Manual allocation
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            //AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.withdraw);
            Response.Redirect("~/admin/Allottee.aspx?Applicationid=" + id + "&returnurl=admin/Prioritizeapplications.aspx?selectedCategory="+drpQuarterCatergory.SelectedValue);
        }
        //DataNavigateUrlFormatString="~/admin/Allottee.aspx?Applicationid={0}&returnurl=admin/Prioritizeapplications.aspx" 
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
        BindData();
    }

    protected void btnChangeStatus_Click(object sender, EventArgs e)
    { 
        //Change status
        //Bind category status and change status buttons
        var dc =new DataClassesDataContext();
        tblCategoryWorkflow catWorkflow = dc.tblCategoryWorkflows.Where(cw => cw.CategoryId == Convert.ToInt64(drpQuarterCatergory.SelectedValue)).SingleOrDefault();

        catWorkflow.Status += 1;
        dc.SubmitChanges();

        //Bind Data or status again
        BindStatusItems();

        //Show notification
        lblMessage.Text = "Category status is changed and processed successfully";
        lblMessage.Visible = true;

        //Show grid view column as per status
        
    }
}
