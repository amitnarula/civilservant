using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class signup : System.Web.UI.Page
{
    tblUser _user;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        if (!Page.IsPostBack)
        {
            Bindoffice();
            BindDesignation();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e) 
    {
        Response.Redirect("~/default.aspx");
    }
    private void Bindoffice()
    {
        drpOffice.DataSource = Offices.GetAlloffices();
        drpOffice.DataBind();
        drpOffice.Items.Insert(0, new ListItem("-select--", "-1"));
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        string status = "saved";
        if (Users.CheckEmail(txtEmailId.Text))
        {
            lblMessage.Text = "Email address already exists!";
            lblMessage.Visible = true;
            return;
        }
        if (Users.getUserByUserName(txtUsername.Text) != null)
        {
            lblMessage.Text = "User name already exists!";
            lblMessage.Visible = true;
            return;
        }
        _user = new tblUser();
        _user.Password = txtPassword.Text;
        _user.designation = Convert.ToInt64(drpDesignation.SelectedValue);
        _user.Roleid = 4;
        _user.EmailID = txtEmailId.Text;
        _user.fullName = txtFullName.Text;
        _user.Username = txtUsername.Text;
        //_user.DateOfRetirement = Convert.ToDateTime(txtDob.Text).AddYears(60);
        //_user.DateOfJoining = Convert.ToDateTime(txtDob.Text);
        ////_user.Addressline1 = txtAddress.Text;
        _user.BaseOfficeId = Convert.ToInt32(drpOffice.SelectedValue);
        Users.SaveUser(_user);
        //Response.Redirect("~/admin/users.aspx?status=" + status);
        string outstring;
        SendmailMessage.sendEmailMessage("support@estatepagpb.org", _user.EmailID, "", "Account created", "Your account has been created sucessfully!", "", out outstring);
        lblMessage.Text = "Information saved sucessfully!";
        lblMessage.Visible = true;
        Response.Redirect("~/Confirmation.aspx");
    }
    private void BindDesignation()
    {
        drpDesignation.DataSource = Allottee.GetDesignations();
        drpDesignation.DataBind();
        drpDesignation.Items.Insert(0, new ListItem("-select--", "-1"));
    }
}
