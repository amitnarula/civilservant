using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IntegratedMessages;
public partial class User_Submission : System.Web.UI.Page
{
    long applicationid;
    bool isverification = false;
    string currentUserRoleId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            currentUserRoleId = _user.Roleid.ToString().ToLower();
        }

        if (!Page.IsPostBack)
        {
            Bindoffice();
            BindDesignations();
            BindQuarterCategory();
            bindAAN();
            BindAllottedQuarter();
            BindAllottedQuarterNumbers();
            BindGradePay();
            Session["members"] = null;

        }

        if (Request["applicationid"] != null)
        {
            long.TryParse(Request["applicationid"], out applicationid);

            if (applicationid > 0 && currentUserRoleId == "1")
            {
                //btnCancel.Text = "OK";
                if (!Page.IsPostBack)
                {
                    //btnAddUpdate.Visible = false;
                    btnUpdate.Visible = true;
                    btnAddUpdate.Visible = false;
                    BindApplicationData();

                }
            }

        }

        if (Request["applicationid"] == null && currentUserRoleId == "4")
        {
            Response.Redirect("~/user/application.aspx");
        }

        if (Request["applicationid"] == null && currentUserRoleId == "1") //New mode of application for admin
        {
            btnAddUpdate.Visible = true;
            btnUpdate.Visible = false;
        }

        if (Request["verify"] != null)
        {
            bool.TryParse(Request["verify"], out isverification);
            btnVerify.Visible = true;
            if (isverification)
            {
                btnCancel.Text = "Cancel";
                if (!Page.IsPostBack)
                {

                    btnAddUpdate.Visible = false;
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
                        if (_user.Roleid.ToString().ToLower() == "1")
                        {
                            btnUpdate.Visible = true;
                        }
                    }
                    BindApplicationData();
                }
            }

        }



        if (currentUserRoleId.ToLower() == "4") // A normal user 
        {
            
            btnAddUpdate.Visible = false;
            btnCancel.Visible = false;
            btnUpdate.Visible = false;
            btnVerify.Visible = false;
            btnAccept.Visible = true;
            btnReject.Visible = true;
            btnCancelAndReturn.Visible = true;
            EnableDisableScreen(false);
            if (Page.IsPostBack == false)
                BindApplicationData();
        }


        if (applicationid <= 0)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (!Convert.ToBoolean(dt.Rows[0][0]))
                {
                    //Server.Transfer("~/user/SubmissionClosed.aspx");
                }
            }
        }
        lblmessage.Visible = false;

        if (isauditpooyes.Checked == true)
        {
            auditpool.Visible = true;
        }
        else
        {
            isauditpoono.Checked = true;
            auditpool.Visible = false;
        }
        //   System.Web.UI.Control auditpool = (System.Web.UI.Control)this.Page.FindControl("auditpool");
        //  if (auditpool is System.Web.UI.HtmlControls.HtmlGenericControl)
        //{
        //  System.Web.UI.HtmlControls.HtmlGenericControl htmlCtrl = (System.Web.UI.HtmlControls.HtmlGenericControl)auditpool;            
        //htmlCtrl.Attributes["style"] = "display:block";
        // }

    }
    private void BindGradePay()
    {
        drpGradePay.SelectedIndex = -1;
        //drpGradePay.DataSource = GradePay.GetPayGradesByCategroy(Convert.ToInt64(drpQuarterCategory.SelectedValue));
        drpGradePay.DataSource = GradePay.GetAllGradePays();
        drpGradePay.DataBind();
        drpGradePay.Items.Insert(0, new ListItem("-select--", "-1"));

    }
    protected void drpQuarterCategory_selectedindex(object sender, EventArgs e)
    {
        //BindGradePay();
    }
    private void bindAAN()
    {
        if (applicationid <= 0)
        {
            /*tblUser _usr = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            txtAAN.Text = _usr.AAN;
            txtUsername.Text = _usr.fullName;
            if (_usr.BaseOfficeId.HasValue)
                drpOffice.SelectedValue = _usr.BaseOfficeId.Value.ToString();
            if (_usr.designation.HasValue)
                drpdesignations.SelectedValue = _usr.designation.Value.ToString();*/
        }
        else
        {
            txtAAN.Text = AllotementApplications.GetApplication(applicationid).tblUser.AAN;
        }

    }
    private void BindQuarterCategory()
    {
        drpQuarterCategory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCategory.DataBind();
        drpQuarterCategory.Items.Insert(0, new ListItem("-select--", "-1"));
    }

    private void BindAllottedQuarter()
    {
        txtQuartertype.DataSource = Quarters.GetQuarterCategory();
        if (txtQuartertype.SelectedIndex != -1)
        {

            txtQuartertype.DataBind();
            txtQuartertype.Items.Insert(0, new ListItem("-select--", "-1"));
        }
    }
    private void BindAllottedQuarterNumbers()
    {
        if (drpAllotedQuarter.SelectedValue != "")
        {
            drpAllotedQuarter.DataSource = Quarters.GetAllAllotedQuarters(txtQuartertype.SelectedIndex);
            drpAllotedQuarter.DataBind();
        }
        //  
        if (drpAllotedQuarter.Items.Count <= 0)
        {
            drpAllotedQuarter.Items.Insert(0, new ListItem("-select--", "-1"));
        }

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (chkDeclaration.Checked == true)
        {
            validatedeclaration.Visible = false;
            tbAllotmentApplication application=new tbAllotmentApplication();
            if (applicationid <= 0)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (!Convert.ToBoolean(dt.Rows[0][0]))
                    {
                        lblmessage.Text = "Application submission has been closed";
                        lblmessage.Visible = true;
                        return;
                    }
                }
                /*application = AllotementApplications.GetApplicationByAAN(Users.getUserByUserName(HttpContext.Current.User.Identity.Name).AAN);
                if (application == null)
                {
                    application = new tbAllotmentApplication();
                    application.SubmissionDate = DateTime.Now;
                }
                else
                {
                    lblmessage.Text = "Only one application can be submitted!";
                    lblmessage.Visible = true;
                    return;
                }*/
                
            }
            else
            {
                application = AllotementApplications.GetApplication(applicationid);
            }
            DateTime DateOfBirth, DateOfjoining, DateOfDebared, dateofret;
            DateTime.TryParse(txtDob.Text, out DateOfBirth);
            DateTime.TryParse(txtEmployedfrom.Text, out DateOfjoining);
            DateTime.TryParse(txtDateOfRetirement.Text, out dateofret);
            application.DateOfBirth = DateOfBirth;
            application.DateOfjoining = DateOfjoining;
            application.GradePay = drpGradePay.SelectedValue;
            application.Name = txtUsername.Text;
            application.Designation = Convert.ToInt64(drpdesignations.SelectedValue);
            application.OfficeId = Convert.ToInt32(drpOffice.SelectedValue);
            application.OtherQuarterAddress = txtAuditAddress.Text;
            if ((txtQuartertype.SelectedValue != null) && (txtQuartertype.SelectedValue != "-1") && (txtQuartertype.SelectedValue != ""))
            {
                application.OtherQuarterType = Convert.ToInt32(txtQuartertype.SelectedValue);
            }
            application.OtherQuarterNumber = drpAllotedQuarter.SelectedValue;
            application.MedicalCategory = Convert.ToInt32(ddlMedicalCategory.SelectedValue);
            application.JobType = Convert.ToInt32(drpJobType.SelectedValue);
            application.Sex = Convert.ToInt32(drpSex.SelectedValue);
            application.Status = 0;
            application.ContactNumber = txtContactNumber.Text;
            application.SubmissionDate = DateTime.Now;
            if (applicationid <= 0)
            {
                tblUser user = new tblUser();
                //user.Username = txtUsername.Text;
                user.EmailID = txtEmailAddress.Text;
                user.EmployeeCode = txtPUCDACode.Text;
                user.IsPasswordChanged = false;
                user.fullName = txtUsername.Text;
                user.AAN = Offices.GetOfficeCode(Convert.ToInt32(drpOffice.SelectedValue))+user.EmployeeCode;
                user.Username = user.AAN;
                user.Password = "Qan89lo";
                user.BaseOfficeId = Convert.ToInt32(drpOffice.SelectedValue);
                user.Roleid = 4;//by default viewer role
                user.designation = Convert.ToInt64(drpdesignations.SelectedValue);
                user.bIsDeleted = false;

                //application.Userid = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).Id;
                //tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
                user.DateOfJoining = DateOfjoining;
                user.DateOfRetirement = dateofret;
                user.AadharNumber = txtAadharNumber.Text.Trim();
                user.PanNumber = txtPAN.Text.Trim();

                var app = AllotementApplications.GetApplicationByAAN(user.AAN);

                if (app != null)
                {
                    lblmessage.Text = "Only one application can be submitted!";
                    lblmessage.Visible = true;
                    return;
                }
                
                Users.SaveUser(user);

                string emailBody = "Your username/AAN = "+user.AAN +" and Password is "+user.Password;

                try
                {
                    SendmailMessage.SendMail(user.EmailID, "support@estatepagpb.org", emailBody, "Account created");
                }
                catch (Exception ex)
                {
                    
                }
                //Send email here for user AAN and Password
                
                //Send SMS
                try
                {
                    //new IntegratedMessageSender().SendMessage("NEW_USER", "Username:" + user.AAN + ",Password:" + user.Password, application.ContactNumber);
                }
                catch (Exception)
                {
                    
                }

                application.Userid = user.Id;
            }
            else
            {

            }
            application.Cast = drpcategory.SelectedItem.Text;
            application.IsSublet = isSubletTrue.Checked;
            application.IsDebarred = isDebarred.Checked;

            //int quarterCategoryId = Convert.ToInt32(drpQuarterCategory.SelectedValue);
            long quarterCategoryId = GradePay.GetQuarterCategoryByGradePay(Convert.ToInt32(drpGradePay.SelectedValue));

            application.QuarterCategory = quarterCategoryId;
            if (isDebarred.Checked)
            {
                DateTime.TryParse(txtDebaredDate.Text, out DateOfDebared);
                application.DebarredDate = DateOfDebared;
            }
            application.IsOtherAccomendation = isauditpooyes.Checked;
            if (isauditpooyes.Checked)
            {
                application.OtherAllotteName = txtAuditAllotteName.Text;
                application.OtherQuarterAddress = txtAuditAddress.Text;
                application.OtherQuarterType = Convert.ToInt64(txtQuartertype.SelectedValue);
                application.OtherQuarterNumber = drpAllotedQuarter.SelectedValue;
            }

            if (!IsQuarterCategoryActive(quarterCategoryId))
            {
                lblmessage.Text = "The application cannot be saved, because specified category is not active";
                lblmessage.Visible = true;
                return;
            }

            application = AllotementApplications.SaveApplications(application);
            if (Session["members"] != null)
            {
                List<tblMember> mem = (List<tblMember>)Session["members"];
                foreach (tblMember meminfo in mem)
                {
                    meminfo.ApplicationId = application.ID;
                }
                Tblmemberinfo.Save(mem);
                Session["members"] = null;
            }
            lblmessage.Visible = true;
            lblmessage.Text = "Information saved sucessfully";
            Empty();
            lblmessage.Font.Bold = true;
            Response.Redirect("~/user/Confirmation.aspx");
        }
        else
        {
            validatedeclaration.Visible = true;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (chkDeclaration.Checked == true)
        {
            validatedeclaration.Visible = false;
            tbAllotmentApplication application;
            if (applicationid <= 0)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (!Convert.ToBoolean(dt.Rows[0][0]))
                    {
                        lblmessage.Text = "Application submission has been closed";
                        lblmessage.Visible = true;
                        return;
                    }
                }
                application = AllotementApplications.GetApplication(applicationid);
                if (application == null)
                {
                    application = new tbAllotmentApplication();
                    application.SubmissionDate = DateTime.Now;
                }
                else
                {
                    lblmessage.Text = "Only one application can be submitted!";
                    lblmessage.Visible = true;
                    return;
                }
            }
            else
            {
                application = AllotementApplications.GetApplication(applicationid);
            }
            DateTime DateOfBirth, DateOfjoining, DateOfDebared, dateofret;
            DateTime.TryParse(txtDob.Text, out DateOfBirth);
            DateTime.TryParse(txtEmployedfrom.Text, out DateOfjoining);
            DateTime.TryParse(txtDateOfRetirement.Text, out dateofret);
            application.DateOfBirth = DateOfBirth;
            application.DateOfjoining = DateOfjoining;
            application.GradePay = drpGradePay.SelectedValue;
            application.Name = txtUsername.Text;
            application.Designation = Convert.ToInt64(drpdesignations.SelectedValue);
            application.OfficeId = Convert.ToInt32(drpOffice.SelectedValue);
            application.OtherQuarterAddress = txtAuditAddress.Text;
            
            if ((txtQuartertype.SelectedValue != null) && (txtQuartertype.SelectedValue != "-1") && (txtQuartertype.SelectedValue != ""))
            {
                application.OtherQuarterType = Convert.ToInt32(txtQuartertype.SelectedValue);
            }
            application.MedicalCategory = Convert.ToInt32(ddlMedicalCategory.SelectedValue);
            application.OtherQuarterNumber = drpAllotedQuarter.SelectedValue;
            application.JobType = Convert.ToInt32(drpJobType.SelectedValue);
            application.Sex = Convert.ToInt32(drpSex.SelectedValue);
            //application.Status = 0;
            application.ContactNumber = txtContactNumber.Text;
            //if (applicationid <= 0)
            {
                //application.Userid = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).Id;
                tblUser user = Users.getUser(application.Userid.Value);

                //user.Username = txtUsername.Text;
                user.EmailID = txtEmailAddress.Text;
                user.fullName = txtUsername.Text;
                user.BaseOfficeId = Convert.ToInt32(drpOffice.SelectedValue);
                user.Roleid = 4;//by default viewer role
                user.designation = Convert.ToInt64(drpdesignations.SelectedValue);
                user.EmployeeCode = txtPUCDACode.Text;
                user.AadharNumber = txtAadharNumber.Text.Trim();
                user.PanNumber = txtPAN.Text.Trim();
                
                //application.Userid = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).Id;
                //tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
                
                user.DateOfJoining = DateOfjoining;
                user.DateOfRetirement = dateofret;
                Users.SaveUser(user);
            }
            //else
            //{

            //}
            application.Cast = drpcategory.SelectedItem.Text;
            application.IsSublet = isSubletTrue.Checked;
            application.IsDebarred = isDebarred.Checked;

            //int quarterCategoryId = Convert.ToInt32(drpQuarterCategory.SelectedValue);
            long quarterCategoryId = GradePay.GetQuarterCategoryByGradePay(Convert.ToInt32(drpGradePay.SelectedValue));

            if (application.QuarterCategory != quarterCategoryId) //Previous category is different than the new one
            { 
                //Put status of the application under pending state again, to be reviewed further
                application.Status = 0;

                //Delete the change requests taken by the user if any to changerequested state
                //And when user category is changed
                tblUser user = Users.getUser(application.Userid.Value);
                var dataContext = new DataClassesDataContext();
                tblChangeRequest changeRequestToUpdate = dataContext.tblChangeRequests
                    .Where(x => x.AAN.ToLower()==user.AAN.ToLower()
                        && (x.Status == (int)ChangeRequestStatus.Pending
                        || x.Status == (int)ChangeRequestStatus.Approved)).FirstOrDefault();

                if (changeRequestToUpdate != null)
                {
                    //changeRequestToUpdate.Status = (int)ChangeRequestStatus.Done;
                    //changeRequestToUpdate.Status = (int)ChangeRequestStatus.Pending;
                    //changeRequestToUpdate.Status = (int)ChangeRequestStatus.Deleted;

                    //Nothing to do with change requests when grade pay is changed

                    //dataContext.tblChangeRequests.DeleteOnSubmit(changeRequestToDelete);
                    dataContext.SubmitChanges();
                }

            }

            application.QuarterCategory = quarterCategoryId;
            if (isDebarred.Checked)
            {
                DateTime.TryParse(txtDebaredDate.Text, out DateOfDebared);
                application.DebarredDate = DateOfDebared;
            }
            application.IsOtherAccomendation = isauditpooyes.Checked;
            if (isauditpooyes.Checked)
            {
                application.OtherAllotteName = txtAuditAllotteName.Text;
                application.OtherQuarterAddress = txtAuditAddress.Text;
                application.OtherQuarterType = Convert.ToInt32(txtQuartertype.SelectedValue);
                application.OtherQuarterNumber = drpAllotedQuarter.SelectedValue;
            }

            if (!IsQuarterCategoryActive(quarterCategoryId))
            {
                lblmessage.Text = "The application cannot be saved, because specified category is not active";
                lblmessage.Visible = true;
                return;
            }

            application = AllotementApplications.SaveApplications(application);
            if (Session["members"] != null)
            {
                List<tblMember> mem = (List<tblMember>)Session["members"];
                foreach (tblMember meminfo in mem)
                {
                    meminfo.ApplicationId = application.ID;
                }
                Tblmemberinfo.Save(mem);
                Session["members"] = null;
            }
            lblmessage.Visible = true;
            lblmessage.Text = "Information saved sucessfully";
            Empty();
            lblmessage.Font.Bold = true;
            if (!string.IsNullOrEmpty(Request["returnurl"]))
            { Response.Redirect("~/" + Request["returnurl"]); }
            else
                Response.Redirect("~/user/Confirmation.aspx");
        }
        else
        {

            validatedeclaration.Visible = true;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["returnurl"]))
        { Response.Redirect("~/" + Request["returnurl"]); }
        else
            Response.Redirect("~/default.aspx");

    }

    private bool IsQuarterCategoryActive(long categoryId)
    {
        return Quarters.IsQuarterCategoryActive(categoryId);
    }

    private void Bindoffice()
    {
        drpOffice.DataSource = Offices.GetAlloffices();
        drpOffice.DataBind();
        drpOffice.Items.Insert(0, new ListItem("--Select--", "-1"));
    }
    private void BindDesignations()
    {
        drpdesignations.DataSource = Allottee.GetDesignations();
        drpdesignations.DataBind();
        drpdesignations.Items.Insert(0, new ListItem("--Select--", "-1"));
    }

    protected void btnsaveMember_click(object sender, EventArgs e)
    {
        tblMember _mem = new tblMember();
        _mem.Age = Convert.ToInt32(txtMemberAge.Text);
        _mem.Relationship = txtRelationShip.Text;
        _mem.isEmployed = rdbemplyed.SelectedValue == "0" ? "No" : "Yes";
        _mem.MemberName = txtMemberName.Text;
        _mem.sex = drpMemberSex.SelectedItem.Text;
        if (Session["members"] == null)
        {
            Session["members"] = new List<tblMember>();
        }
        ((List<tblMember>)Session["members"]).Add(_mem);
        grdmembers.DataSource = ((List<tblMember>)Session["members"]);
        grdmembers.DataBind();

    }

    private void Empty()//
    {
        txtAAN.Text = "";
        txtAuditAddress.Text = "";
        txtAuditAllotteName.Text = "";
        txtDateOfRetirement.Text = "";
        txtDebaredDate.Text = "";
        txtDob.Text = "";
        txtEmployedfrom.Text = "";
        drpGradePay.SelectedIndex = -1;
        txtMemberAge.Text = "";
        txtMemberName.Text = "";
        //txtPayBand.Text = "";
        txtQuartertype.SelectedIndex = -1;
        txtRelationShip.Text = "";
        txtUsername.Text = "";
        txtContactNumber.Text = "";
    }

    private void EnableDisableScreen(bool enable)
    {
        drpQuarterCategory.Enabled = enable;
        drpcategory.Enabled = enable;
        txtAAN.Enabled = enable;
        txtUsername.Enabled = enable;
        drpdesignations.Enabled = enable;
        txtPUCDACode.Enabled = enable;
        txtContactNumber.Enabled = enable;
        txtEmailAddress.Enabled = enable;
        ddlMedicalCategory.Enabled = enable;
        drpOffice.Enabled = enable;
        drpGradePay.Enabled = enable;
        drpcategory.Enabled = enable;
        txtEmployedfrom.Enabled = enable;
        txtDob.Enabled = enable;
        txtDateOfRetirement.Enabled = enable;
        drpSex.Enabled = enable;
        txtAadharNumber.Enabled = enable;
        txtPAN.Enabled = enable;
        drpJobType.Enabled = enable;
        isauditpoono.Enabled = enable;
        isauditpooyes.Enabled = enable;
        txtAuditAllotteName.Enabled = enable;
        txtQuartertype.Enabled = enable;
        drpAllotedQuarter.Enabled = enable;
        txtAuditAddress.Enabled = enable;
        lnkAddMember.Enabled = enable;
        isSubletTrue.Enabled = enable;
        isSubletFalse.Enabled = enable;
        isDebarred.Enabled = enable;
        isDebarredNO.Enabled = enable;
        txtDebaredDate.Enabled = enable;
        chkDeclaration.Enabled = enable;
        addmembers.Visible = enable;
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
            drpGradePay.SelectedValue = application.GradePay;
            txtUsername.Text = application.Name;
            
            if (application.Designation.HasValue)
                drpdesignations.SelectedValue = application.Designation.Value.ToString();
            if (application.OfficeId.HasValue)
                drpOffice.SelectedValue = application.OfficeId.Value.ToString();
            txtAuditAddress.Text = application.OtherQuarterAddress;
            txtQuartertype.SelectedValue = Convert.ToString(application.OtherQuarterType);
            drpAllotedQuarter.SelectedValue = application.OtherQuarterNumber;
            if (application.JobType.HasValue)
                drpJobType.SelectedValue = application.JobType.Value.ToString();
            drpSex.SelectedValue = application.Sex.ToString();
            ddlMedicalCategory.SelectedValue = application.MedicalCategory.ToString();
            txtContactNumber.Text = application.ContactNumber;
            //application.Status = 0;
            //application.Userid = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).Id;

            tblUser _user = Users.getUser(application.Userid.Value);
            if (application.DateOfBirth.HasValue)
                txtDob.Text = application.DateOfBirth.Value.ToShortDateString();
            //txtPayBand.Text = application.QuarterNumber;
            foreach (ListItem ino in drpcategory.Items)
            {
                if (!string.IsNullOrEmpty(application.Cast) && ino.Text.ToLower() == application.Cast.ToLower())
                {
                    ino.Selected = true;
                }
            }
            if (_user.DateOfJoining.HasValue)
                txtEmployedfrom.Text = _user.DateOfJoining.Value.ToShortDateString();
            if (_user.DateOfRetirement.HasValue)
            {
                txtDateOfRetirement.Text = _user.DateOfRetirement.Value.ToShortDateString();
            }

            txtAAN.Text = _user.AAN;
            txtEmailAddress.Text = _user.EmailID;
            txtPUCDACode.Text = _user.EmployeeCode;
            txtAadharNumber.Text = _user.AadharNumber;
            txtPAN.Text = _user.PanNumber;
            
            //_user.DateOfJoining = DateOfjoining;
            //_user.DateOfRetirement = dateofret;
            if (application.IsSublet.HasValue)
                isSubletTrue.Checked = application.IsSublet.Value;


            if (!isSubletTrue.Checked)
            {
                isSubletFalse.Checked = true;
            }
            if (application.IsDebarred.HasValue)
                isDebarred.Checked = application.IsDebarred.Value;
            if (application.QuarterCategory.HasValue)
                drpQuarterCategory.SelectedValue = application.QuarterCategory.Value.ToString();
            if (isDebarred.Checked)
            {
                if (application.DebarredDate.HasValue)
                    txtDebaredDate.Text = application.DebarredDate.Value.ToShortDateString();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "showdebarred", "showbarredDate(1);", true);
            }
            else
            {
                isDebarredNO.Checked = true;
            }
            if (application.IsOtherAccomendation.HasValue)
            {
                isauditpooyes.Checked = application.IsOtherAccomendation.Value;


            }
            if (!isauditpooyes.Checked)
            {
                isauditpoono.Checked = true;
            }
            else
            {
                txtAuditAllotteName.Text = application.OtherAllotteName;
                txtAuditAddress.Text = application.OtherQuarterAddress;
                txtQuartertype.SelectedValue = Convert.ToString(application.OtherQuarterType);
                drpAllotedQuarter.SelectedValue = application.OtherQuarterNumber;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "showaudit", "showAudit(1);", true);
            }
            grdmembers.DataSource = Tblmemberinfo.GetMemberinfo(application.ID);
            grdmembers.DataBind();
        }
    }
    protected void bindQuarter()
    {
        drpAllotedQuarter.DataSource = Quarters.GetAllAllotedQuarters(Convert.ToInt64(drpQuarterCategory.SelectedValue));
        drpAllotedQuarter.DataBind();
        drpAllotedQuarter.Items.Insert(0, new ListItem("-select--", "-1"));

    }

    protected void txtQuarterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllottedQuarterNumbers();
    }

    protected void btnverify_Click(object sender, EventArgs e)
    {
        AllotementApplications.UpdateApplicationStaus(applicationid, ApplicationStatus.Verified);
        Response.Redirect("~/" + Request["returnurl"]);
    }
    protected void isauditpooyes_Checked(object sender, EventArgs e)
    {
        auditpool.Visible = true;
    }

    protected void isauditpoono_Checked(object sender, EventArgs e)
    {
        auditpool.Visible = false;
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/user/processapplication.aspx?applicationId={0}", applicationid));
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        pnlInfo.Visible = true;
    }

    protected void btnSubmitInfo_Click(object sender, EventArgs e)
    {
        try
        {
            SendmailMessage.SendMail("amrinder.bhagtana@gmail.com", "support@estatepagpb.org", txtInformation.Text, "Please update my information, AAN : "+txtAAN.Text);

        }
        catch (Exception)
        {

        }
        finally
        {
            lblmessage.Text = "Your information has successfully been sent to the Administrator";
            lblmessage.Visible = true;

            pnlInfo.Visible = false;
        }

    }

    protected void btnCancelInfo_Click(object sender, EventArgs e)
    {
        pnlInfo.Visible = false;
        txtInformation.Text = string.Empty;
    }

    protected void btnCancelAndReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/" + Request["returnurl"]);
    }
}