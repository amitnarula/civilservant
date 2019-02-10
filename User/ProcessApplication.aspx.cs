﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_ProcessApplication : System.Web.UI.Page
{

    class QuarterData {
        public int NumberOfChangeRequests { get; set; }
        public tblQuarter Quarter { get; set; }
        public bool IsVacant { get; set; }
    }

    string applicationId = string.Empty;
    long categoryId = 0;
    List<tblChangeRequest> changeRequests=new List<tblChangeRequest>();

    void  grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
 	    if(e.Row.RowType==DataControlRowType.DataRow)
        {
            Label lblQuarterNumber = e.Row.FindControl("lblQuarterNumber") as Label;
            QuarterData quarterData = e.Row.DataItem as QuarterData;

            if (quarterData != null && quarterData.IsVacant) {

                var row = lblQuarterNumber.NamingContainer as GridViewRow;
                row.CssClass = quarterData.IsVacant ? "alert alert-success" : "alert alert-danger";
                
            }

            //int numberOfChangeRequestsForQuarter=0;
            //string quarterNumber = lblQuarterNumber.Text;

            //if(changeRequests.Any())
            //{
            //    if(changeRequests.Where(cr=>cr.FirstPerference==quarterNumber ||
            //        cr.SecondPerference==quarterNumber || 
            //        cr.ThirdPerference==quarterNumber).Any())
            //    {
            //        numberOfChangeRequestsForQuarter++;
            //    }
            //}
            //lblQuarterNumber.ToolTip =numberOfChangeRequestsForQuarter +" Change requests against this quarter";
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

        if (application.AllowChangeRequestInPreviousCategory)
            categoryId = GradePay.GetQuarterCategoryByGradePay(Convert.ToInt32(application.PreviousGradePay));

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
                //submissionClosedPanel.Visible = true;
                //return;

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
                    bool isSpecialCase = application.AllowChangeRequestInPreviousCategory;
                    
                    if (application.MedicalCategory.HasValue && application.MedicalCategory.Value >= 0) //Medical grounds
                    {
                        applyForQuarterMedicalGroundsPanel.Visible = true;
                        btnChangeRequestMedical.Enabled = isChangeRequestEnabled;

                        if(isSpecialCase)//special case of pay downgrade which is done manually in system tballotmentapplications
                        {
                            pnlApplyForChangeRequestSpecialCaseOfGradeDowngrade.Visible = true;
                            btnChangeRequestSpecialCase.Text = "Apply for change request (medical ground) in previous category";
                        }

                    }
                    else
                    {
                        applyForQuarterPanel.Visible = true;
                        btnChangeRequest.Enabled = isChangeRequestEnabled;

                        if (isSpecialCase)//special case of pay downgrade which is done manually in system tballotmentapplications
                        {
                            pnlApplyForChangeRequestSpecialCaseOfGradeDowngrade.Visible = true;
                            btnChangeRequestSpecialCase.Text = "Apply for change request in previous category";
                        }
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
                    lblQuarterCategory.Text = changeRequest.QuarterCategory.ToString();
                    lblFirstPreference.Text = changeRequest.FirstPerference ?? "N/A";
                    lblSecondPreference.Text = changeRequest.SecondPerference ?? "N/A";
                    lblThirdPreference.Text = changeRequest.ThirdPerference ?? "N/A";

                }
                else
                    pnlChangeRequestInformation.Visible = false;

                
            }
            //BindData();
            BindDataImprovised();
        }

        changeRequests = new DataClassesDataContext().tblChangeRequests.ToList();
        
    }

    protected void Page_Init(object sender, EventArgs e) {
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
        BindDataImprovised();
        //BindData();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Save change request
        DataClassesDataContext dataContext = new DataClassesDataContext();

        string aan = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).AAN;
        var _alreadyRequested = from _requested in dataContext.tblChangeRequests
                                where _requested.AAN == aan && _requested.QuarterCategory == categoryId && _requested.Status != (int)ChangeRequestStatus.Deleted
                                select _requested;
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

        if (string.IsNullOrEmpty(firstPreferrence) ||
            string.IsNullOrEmpty(secondPreference) ||
            string.IsNullOrEmpty(thirdPreference))
        {
            lblStatus.Text = "Please select all three preferred quarters.";
            lblStatus.CssClass = "alert alert-danger";
            return;
        }

        if(pnlApplyForChangeRequestSpecialCaseOfGradeDowngrade.Visible)
            RestoreUserApplicationAndPreviousChangeRequests(aan);

        //adding new change request
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

    private void RestoreUserApplicationAndPreviousChangeRequests(string aan)
    {
        //rollback the special case now and put application back to normal and also delete the previous change request
        //of the user as that is not valid because this is a special case and user has submitted new change request in previous category
        using (var dc = new DataClassesDataContext())
        {
            var changeRequests = dc.tblChangeRequests.Where(x => x.AAN == aan).ToList();
            //mark all change requests to delete now
            if (changeRequests.Any())
            {
                changeRequests.ForEach(x =>
                {
                    x.Status = (int)ChangeRequestStatus.Deleted;
                });
            }

            var app = dc.tbAllotmentApplications.FirstOrDefault(x => x.ID == Convert.ToInt32(applicationId));
            if (app != null)
            {
                app.AllowChangeRequestInPreviousCategory = false;
                app.PreviousGradePay = null;
            }
            dc.SubmitChanges();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/user/application.aspx");
    }

    private void BindDataImprovised() {
        bool searchForMedicalGround = btnChangeRequestMedical.Enabled ? true : false;

        if (pnlApplyForChangeRequestSpecialCaseOfGradeDowngrade.Visible)
        {
            searchForMedicalGround = btnChangeRequestSpecialCase.Text.Contains("medical");
        }

        //Bind the quarters here
        List<tblQuarter> quarters = Quarters.GetQuarters();
        List<QuarterData> quartersData = new List<QuarterData>();
        var changeRequests = Quarters.GetChangeRequests();

        //Filter by category
        quarters = quarters.Where(q => q.Category == categoryId).ToList();

        //Search by medical grounds
        if (searchForMedicalGround)
        {
            quarters = quarters.Where(quarter => !(quarter.QuarterNumber.Contains('A') ||
                quarter.QuarterNumber.Contains('B') ||
                quarter.QuarterNumber.Contains('C') ||
                quarter.QuarterNumber.Contains('D'))).ToList();

        }
        else
        {
            quarters = quarters.Where(quarter => (quarter.QuarterNumber.Contains('A') ||
                quarter.QuarterNumber.Contains('B') ||
                quarter.QuarterNumber.Contains('C') ||
                quarter.QuarterNumber.Contains('D'))).ToList();
        }

        quarters.ForEach((quarter) =>
        {
            var noOfChangeRequestsForQuarter = changeRequests.Count(q => (q.FirstPerference == quarter.QuarterNumber ||
                        q.SecondPerference == quarter.QuarterNumber ||
                        q.ThirdPerference == quarter.QuarterNumber) && q.Status != (int)ChangeRequestStatus.Deleted);
            quartersData.Add(new QuarterData() {
                IsVacant = quarter.Status.HasValue && quarter.Status.Value == ((int)QuarterStatus.Vacant),
                NumberOfChangeRequests = noOfChangeRequestsForQuarter,
                Quarter = quarter
            });
            
        });

        grdQuarters.DataSource = quartersData.OrderBy(x=>x.IsVacant==false).ToList();
        grdQuarters.DataBind();
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