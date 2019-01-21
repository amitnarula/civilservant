using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_ProcessApplication : System.Web.UI.Page
{
    string applicationId = string.Empty;
    long categoryId = 0;
    List<tblChangeRequest> changeRequests=new List<tblChangeRequest>();

    void  grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
 	    if(e.Row.RowType==DataControlRowType.DataRow)
        {
            Label lblQuarterNumber = e.Row.FindControl("lblQuarterNumber") as Label;
            int numberOfChangeRequestsForQuarter=0;
            string quarterNumber = lblQuarterNumber.Text;

            if(changeRequests.Any())
            {
                if(changeRequests.Where(cr=>cr.FirstPerference==quarterNumber ||
                    cr.SecondPerference==quarterNumber || 
                    cr.ThirdPerference==quarterNumber).Any())
                {
                    numberOfChangeRequestsForQuarter++;
                }
            }
            lblQuarterNumber.ToolTip =numberOfChangeRequestsForQuarter +" Change requests against this quarter";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["applicationId"] == null)
        {
            Response.Redirect("~/user/application.aspx");
        }
        else if (Request["applicationId"] != null)
        {
            applicationId = Convert.ToString(Request["applicationId"]);
        }
        tbAllotmentApplication application = AllotementApplications.GetApplication(Convert.ToInt64(applicationId));
        categoryId = application.QuarterCategory.Value;

        if (!Page.IsPostBack)
        {
            tblUser user = Users.getUser(application.Userid.Value);

            DateTime? dt=null;
            //Check if user is debarred or deferred
            if(Users.IsUserDeferred(user.Id,out dt))
            {
                Response.Redirect("~/user/notallowed.aspx");
            }

            if (Users.IsUserDebarred(user.Id, out dt))
            {
                Response.Redirect("~/user/notallowed.aspx?debar=true");
            }


            lblFullname.Text = user.fullName;
            lblDesignation.Text = Users.GetUserDesignation(user.Id);
            lblAllottedQuarter.Text = "N/A";

            var allottee = Allottee.GetAllotteeByAAN(user.AAN);
            if (allottee != null && (allottee.Status.Value == ((int)AllotementStatus.Possessed) || allottee.Status.Value == ((int)AllotementStatus.ChangeRequested)))
            {
                string allottedQuarter = allottee.QuarterNumber;
                lblAllottedQuarter.Text = allottedQuarter;
            }            
            
            if (application.QuarterCategory.HasValue && !Quarters.IsQuarterCategoryActive(application.QuarterCategory.Value))
            {
                submissionClosedPanel.Visible = true;
                return;
            }
            else
            { 
                //Quarter category is active
                if (lblAllottedQuarter.Text == "N/A" || application.Status==(int)ApplicationStatus.Pending) //New allottment cases
                {
                    if (application.MedicalCategory.HasValue && application.MedicalCategory.Value >= 0) //Medical grounds
                    {
                        applyForQuarterMedicalGroundsPanel.Visible = true;
                        btnNewAllottmentMedical.Enabled = true;
                    }
                    else
                    {
                        applyForQuarterPanel.Visible = true;
                        btnNewAllottment.Enabled = true;
                    }
                }
                else
                {
                    //only then when application is in possessed state.
                    var isChangeRequestEnabled = application.Status == (int)ApplicationStatus.Pos; 
                    
                    if (application.MedicalCategory.HasValue && application.MedicalCategory.Value >= 0) //Medical grounds
                    {
                        applyForQuarterMedicalGroundsPanel.Visible = true;
                        btnChangeRequestMedical.Enabled = isChangeRequestEnabled;
                    }
                    else
                    {
                        applyForQuarterPanel.Visible = true;
                        btnChangeRequest.Enabled = isChangeRequestEnabled;
                    }

                }

                DataClassesDataContext dataContext = new DataClassesDataContext();

                string aan = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).AAN;
                var _alreadyRequested = from _requested in dataContext.tblChangeRequests
                                        where _requested.AAN == aan 
                                        && (_requested.Status == (int)ChangeRequestStatus.Pending 
                                        || _requested.Status == (int)ChangeRequestStatus.Approved)
                                        select _requested;

                if (_alreadyRequested.FirstOrDefault() != null)
                {
                    var changeRequest = _alreadyRequested.FirstOrDefault();
                    pnlChangeRequestInformation.Visible = true;
                    btnChangeRequest.Enabled = false;
                    btnChangeRequestMedical.Enabled = false;

                    lblRequestID.Text = changeRequest.Id.ToString();
                    lblFirstPreference.Text = changeRequest.FirstPerference ?? "N/A";
                    lblSecondPreference.Text = changeRequest.SecondPerference ?? "N/A";
                    lblThirdPreference.Text = changeRequest.ThirdPerference ?? "N/A";

                }
                else
                    pnlChangeRequestInformation.Visible = false;

                
            }
            BindData();
        }

        changeRequests = new DataClassesDataContext().tblChangeRequests.ToList();
        grdQuarters.RowDataBound += new GridViewRowEventHandler(grdQuarters_RowDataBound);
    }

    protected void btnApplyNew_Click(object sender, EventArgs e)
    {
        tbAllotmentApplication application = AllotementApplications.GetApplication(Convert.ToInt64(applicationId));

        if (application.Status == (int)ApplicationStatus.Verified)
        {
            lblStatus.Text = "You have already applied for the new allotment application, and your application is under process.";    
        }
        else if (application.Status == (int)ApplicationStatus.Pending
            || application.Status == (int)ApplicationStatus.withdraw) //added on 30.12.2016 as per request from amrinder
        {
            //Change application status to verified
            AllotementApplications.UpdateApplicationStaus(Convert.ToInt64(applicationId), ApplicationStatus.Verified);
            lblStatus.Text = "Your application request is processed further, thanks for your application";
            Response.Redirect("~/user/confirmation.aspx");
        }
    }
    protected void btnChangeRequest_Click(object sender, EventArgs e)
    {
        pnlChangeRequest.Visible = true;
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    { 
        //Save change request
        DataClassesDataContext dataContext = new DataClassesDataContext();

        string aan = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).AAN;
        var _alreadyRequested = from _requested in dataContext.tblChangeRequests where _requested.AAN == aan select _requested;
        if (_alreadyRequested.FirstOrDefault() != null)
        {
            lblStatus.Text = "You have already submitted a change request!";
            return;
        }

        string firstPreferrence = string.Empty;
        string secondPreference = string.Empty;
        string thirdPreference = string.Empty;

        foreach (GridViewRow row in grdQuarters.Rows)
        {
            RadioButton radPriority1 = row.FindControl("radPriority1") as RadioButton;
            RadioButton radPriority2 = row.FindControl("radPriority2") as RadioButton;
            RadioButton radPriority3 = row.FindControl("radPriority3") as RadioButton;

            if (firstPreferrence == string.Empty && radPriority1.Checked)
                firstPreferrence = ((Label)row.FindControl("lblQuarterNumber")).Text;

            if (secondPreference == string.Empty && radPriority2.Checked)
                secondPreference = ((Label)row.FindControl("lblQuarterNumber")).Text;

            if (thirdPreference == string.Empty && radPriority3.Checked)
                thirdPreference = ((Label)row.FindControl("lblQuarterNumber")).Text;

        }

        if (string.IsNullOrEmpty(firstPreferrence) &&
            string.IsNullOrEmpty(secondPreference) &&
            string.IsNullOrEmpty(thirdPreference))
        {
            lblStatus.Text = "Please select a valid preference.";
            return;
        }

        tblChangeRequest changeRequest = new tblChangeRequest();
        changeRequest.AAN = aan;
        changeRequest.FirstPerference = firstPreferrence;
        if (string.IsNullOrEmpty(secondPreference)) { changeRequest.SecondPerference = string.Empty; } else { changeRequest.SecondPerference = secondPreference; }
        if (string.IsNullOrEmpty(thirdPreference)) { changeRequest.ThirdPerference = string.Empty; } else { changeRequest.ThirdPerference = thirdPreference; }
        
        changeRequest.QuarterCategory = Convert.ToInt32(categoryId);
        changeRequest.QuarterNumber = lblAllottedQuarter.Text;
        changeRequest.Name = lblFullname.Text;
        changeRequest.dateofsubmission = DateTime.Now;
        dataContext.tblChangeRequests.InsertOnSubmit(changeRequest);
        dataContext.SubmitChanges();

        Response.Redirect("~/user/confirmation.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/user/application.aspx");
    }

    private void BindData()
    {
        bool searchForMedicalGround = btnChangeRequestMedical.Enabled ? true : false;

        //Bind the quarters here
        List<tblQuarter> quarters = Quarters.GetQuarters();

        //Filter by category
        quarters = quarters.Where(q => q.Category == categoryId).ToList();

        //Search by medical grounds
        if (searchForMedicalGround)
        {
            quarters = quarters.Where(quarter => !(quarter.QuarterNumber.Contains('A')|| 
                quarter.QuarterNumber.Contains('B') ||
                quarter.QuarterNumber.Contains('C') ||
                quarter.QuarterNumber.Contains('D'))).ToList();

        }
        else
        {
            quarters = quarters.Where(quarter => (quarter.QuarterNumber.Contains('A')|| 
                quarter.QuarterNumber.Contains('B') ||
                quarter.QuarterNumber.Contains('C') ||
                quarter.QuarterNumber.Contains('D'))).ToList();
        }
        
        //Filter by status
        if (radListVacantQuarters.Checked)
        {
            quarters = quarters.Where(q => q.Status.HasValue && q.Status.Value == ((int)QuarterStatus.Vacant)).ToList();
        }
        else if (radListOccupiedQuarters.Checked)
        {
            quarters = quarters.Where(q => q.Status.HasValue && q.Status.Value == ((int)QuarterStatus.Alloted)).ToList();
        }

        //Filter by change requests
        List<tblQuarter> quartersByChangeRequests = new List<tblQuarter>();


        var changeRequests = Quarters.GetChangeRequests();

        if (changeRequests != null)
        {
            quarters.ForEach((quarter) =>
            {

                if (radNoChangeRequest.Checked)
                {
                    if (!changeRequests.Any(q => q.FirstPerference == quarter.QuarterNumber &&
                        q.SecondPerference == quarter.QuarterNumber &&
                        q.ThirdPerference == quarter.QuarterNumber))
                    {
                        quartersByChangeRequests.Add(quarter);
                    }
                }
                else if (radChangeRequestReceived.Checked)
                {
                    if (changeRequests.Any(q => q.FirstPerference == quarter.QuarterNumber ||
                        q.SecondPerference == quarter.QuarterNumber ||
                        q.ThirdPerference == quarter.QuarterNumber))
                    {
                        quartersByChangeRequests.Add(quarter);
                    }
                }

            });
        }

        grdQuarters.DataSource = quartersByChangeRequests;
        grdQuarters.DataBind();
    }
}