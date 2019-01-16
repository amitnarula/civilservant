using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_printapplication : System.Web.UI.Page
{
    long applicationid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["applicationid"] != null)
        {
            long.TryParse(Request["applicationid"], out applicationid);
        }
                btnCancel.Text = "Print";
                btnCancel.OnClientClick = "print();return false;";
                if (!Page.IsPostBack)
                {

                    bindAAN();
                    BindApplicationData();
                }
            

        
    }
    private void BindApplicationData()
    {
        tbAllotmentApplication application = AllotementApplications.GetApplication(applicationid);
        if (application != null)
        {
            //DateTime DateOfBirth, DateOfjoining, DateOfDebared, dateofret;
            //DateTime.TryParse(txtDob.Text, out DateOfBirth);
            //DateTime.TryParse(txtEmployedfrom.Text, out DateOfjoining);
            //DateTime.TryParse(txtDateOfRetirement.Text, out dateofret);
            lblGradePay.Text = GradePay.GetPayGradesById(Convert.ToInt64(application.GradePay)); 
            txtUsername.Text = application.Name;
            
            if (application.Designation.HasValue)
                lbldesignations.Text= Allottee.GetDesignationName(application.Designation.Value).ToString();
            if (application.OfficeId.HasValue)
                lblOffice.Text = Offices.GetOfficeName(application.OfficeId.Value).ToString();
            txtAuditAddress.Text = application.OtherQuarterAddress;
            txtQuartertype.Text = application.OtherQuarterNumber;
            txtContactNumber.Text = application.ContactNumber;
            if (application.JobType.Value==0)
            {
                drpJobType.Text = "Permanent";
            }
            else
                drpJobType.Text = "Temporary";
            if (application.Sex.Value==0)
            lblSex.Text = "Female";
            else
                lblSex.Text = "Male";
            
            
            tblUser _user = Users.getUser(application.Userid.Value);
            if (application.DateOfBirth.HasValue)
                txtDob.Text = application.DateOfBirth.Value.ToShortDateString();
            lblcategory.Text = application.Cast;
            
            if (_user.DateOfJoining.HasValue)
                txtEmployedfrom.Text = _user.DateOfJoining.Value.ToShortDateString();
            if (_user.DateOfRetirement.HasValue)
            {
                txtDateOfRetirement.Text = _user.DateOfRetirement.Value.ToShortDateString();
            }
            //_user.DateOfJoining = DateOfjoining;
            //_user.DateOfRetirement = dateofret;
            if (application.IsSublet.HasValue)
                isSubletTrue.Checked = application.IsSublet.Value;


            if (!isSubletTrue.Checked)
            {
                lblSublet.Text = "Yes";
                isSubletFalse.Checked = true;
            }
            else
            {
                lblSublet.Text = "No";
            }
           
            if (application.IsDebarred.HasValue)
                isDebarred.Checked = application.IsDebarred.Value;
            if (application.QuarterCategory.HasValue)
                lblQuarterCategory.Text = Quarters.GetQuarterCategoryName(application.QuarterCategory.Value).ToString();
            if (isDebarred.Checked)
            {
                lblDebarred.Text = "Yes";
                if (application.DebarredDate.HasValue)
                    txtDebaredDate.Text = application.DebarredDate.Value.ToShortDateString();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "showdebarred", "showbarredDate(1);", true);
            }
            else
            {
                lblDebarred.Text = "No";
                isDebarredNO.Checked = true;
            }
            if (application.IsOtherAccomendation.HasValue)
            {
                isauditpooyes.Checked = application.IsOtherAccomendation.Value;


            }
            if (!isauditpooyes.Checked)
            {
                lblauditpooyes.Text = "No";
                isauditpoono.Checked = true;
            }
            else
            {
                lblauditpooyes.Text = "Yes";
                txtAuditAllotteName.Text = application.OtherAllotteName;
                txtAuditAddress.Text = application.OtherQuarterAddress;
                txtQuartertype.Text = application.OtherQuarterNumber;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "showaudit", "showAudit(1);", true);
            }
            grdmembers.DataSource = Tblmemberinfo.GetMemberinfo(application.ID);
            grdmembers.DataBind();
        }
    }
    private void bindAAN()
    {
        if (applicationid <= 0)
        {
            tblUser _usr = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            txtAAN.Text = _usr.AAN;
            txtUsername.Text = _usr.fullName;
            
        }
        else
        {
            txtAAN.Text = AllotementApplications.GetApplication(applicationid).tblUser.AAN;
        }

    }
}