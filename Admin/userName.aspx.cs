using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_userName : System.Web.UI.Page
{
    tblUser _user;
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "User management");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
        lblMessage.Visible = false;
      
        btnAddUpdate.Text = "Add";
        if (!Page.IsPostBack)
        {
            Bindoffice();
            BindRoles();
        }
        if (!string.IsNullOrEmpty(Request["id"]))
        {
            
            int.TryParse(Request["id"], out id);
            if (id <= 0)
            {
                Response.Redirect("~/admin/users.aspx");
                Response.End();
            }
            if (!Page.IsPostBack)
            {
                txtUsername.ReadOnly = true; RequiredFieldValidator4.Enabled = false; RequiredFieldValidator5.Enabled = false; cmpVal.Enabled = false; ; getData();
            }
        }
    }
    private void Bindoffice()
    {  
        drpOffice.DataSource = Offices.GetAlloffices();
        drpOffice.DataBind();
    }
    private void BindRoles()
    {
        drpRoles.DataSource = Roles.GetRoles();
        drpRoles.DataBind();
    }
    private void getData()
    {
        _user = Users.getUser(id);
        txtEmailId.Text = _user.EmailID;
        txtFullName.Text = _user.fullName;
        txtUsername.Text = _user.Username;
        //txtDateOfRetirement.Text = _user.DateOfRetirement.Value.ToShortDateString();
        txtPassword.Text = _user.Password;
        //if(_user.DateOfJoining.HasValue)
        //txtDob.Text = _user.DateOfJoining.Value.ToShortDateString();
       // txtAddress.Text = _user.Addressline1;
        txtconfirmpassword.Text = _user.Password;
        btnAddUpdate.Text = "Update";
        txtPassword.Text = _user.Password;
        drpOffice.SelectedValue = _user.BaseOfficeId.ToString();
        drpRoles.SelectedValue = _user.Roleid.ToString();
    }
        protected void btnCancel_Click(object sender,EventArgs e) 
        {
            Response.Redirect("~/admin/users.aspx");
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
        string status = "saved";
        if (id > 0) 
        {
        _user = Users.getUser(id);
        status = "updated";
        }
        else 
        {
            if (Users.CheckEmail(txtEmailId.Text)) {
                lblMessage.Text = "Email address already exists!";
                lblMessage.Visible = true;
                return;
            }
            if (Users.getUserByUserName(txtUsername.Text)!=null)
            {
                lblMessage.Text = "User name already exists!";
                lblMessage.Visible = true;
                return;
            }
            _user = new tblUser(); 
        }
        _user.EmailID = txtEmailId.Text;
        _user.fullName= txtFullName.Text ;
        _user.Username =txtUsername.Text;
       // _user.DateOfRetirement = Convert.ToDateTime(txtDob.Text).AddYears(60);
       //_user.DateOfJoining= Convert.ToDateTime(txtDob.Text );
      // _user.Addressline1 = txtAddress.Text;
       if(id<=0)
            _user.Password = txtPassword.Text;
       _user.BaseOfficeId = Convert.ToInt32(drpOffice.SelectedValue);
       _user.Roleid = Convert.ToInt32(drpRoles.SelectedValue);
            Users.SaveUser(_user);
            if (id <= 0)
            {
                tbluserhistory _userhistory = new tbluserhistory();
                _userhistory.Action = "User creation";
                _userhistory.description = _user.Username + " has created user with userid " + _user.Id;
                _userhistory.time = DateTime.Now;
                _userhistory.useraan = _user.AAN;
                userHistory.Save(_userhistory);
                string outstring;
                SendmailMessage.sendEmailMessage("postmaster@estatepagpb.org", _user.EmailID, "", "Account created", "Your account has been created sucessfully!", "", out outstring);
            }
            else
            {

                tbluserhistory _userhistory = new tbluserhistory();
                _userhistory.Action = "User creation";
                _userhistory.description = _user.Username + " has updated user with userid " + _user.Id;
                _userhistory.time = DateTime.Now;
                _userhistory.useraan = _user.AAN;
                userHistory.Save(_userhistory);
            }
        Response.Redirect("~/admin/users.aspx?status=" + status);
    }
}
