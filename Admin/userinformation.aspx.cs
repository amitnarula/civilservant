using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_userinformation : System.Web.UI.Page
{
    tblUser _user;
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Bindoffice();
            BindRoles();
        }
        if (!string.IsNullOrEmpty(Request["id"]))
        {
            string aan = Request["id"].ToString();
            _user = Users.getUserByAAN(aan);
            if (_user ==null)
            {
                Response.Redirect("~/admin/users.aspx");
                Response.End();
            }
            if (!Page.IsPostBack)
            {
               getData();
            }
        }
        //userinformation.aspx
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (Request["returnurl"] != null)
            Response.Redirect("~/" + Request["returnurl"].ToString());
        else
            Response.Redirect("~/admin/default.aspx");
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
       
        txtEmailId.Text = _user.EmailID;
        txtFullName.Text = _user.fullName;
        txtUsername.Text = _user.Username;
        //txtDateOfRetirement.Text = _user.DateOfRetirement.Value.ToShortDateString();
       
        //if(_user.DateOfJoining.HasValue)
        //txtDob.Text = _user.DateOfJoining.Value.ToShortDateString();
        // txtAddress.Text = _user.Addressline1;
        
      
        drpOffice.SelectedValue = _user.BaseOfficeId.ToString();
        drpRoles.SelectedValue = _user.Roleid.ToString();
    }
}
