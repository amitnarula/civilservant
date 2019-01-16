using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_changerequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        if (!Page.IsPostBack)
        {
            Bindoffice();
            BindDesignations();
            BindQuarterCategory();
            drpQuarterCategory_SelectedIndexChanged(null, null);
            BIndUserData();
        }
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
    private void BindQuarterCategory()
    {
        drpQuarterCategory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCategory.DataBind();
        drpQuarterCategory.Items.Insert(0,new ListItem("--Select--","-1"));

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["returnurl"]))
        { Response.Redirect("~/" + Request["returnurl"]); }
        else
            Response.Redirect("~/default.aspx");

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        
        string aan = Users.getUserByUserName(HttpContext.Current.User.Identity.Name).AAN;
        var _alreadyRequested = from _requested in dataContext.tblChangeRequests where _requested.AAN == aan select _requested;
        if (_alreadyRequested.FirstOrDefault() != null)
        {
            empty();
            lblmessage.Text = "You have already submitted request!";
            lblmessage.Visible = true;
            return;
        }

        tblChangeRequest changeRequest = new tblChangeRequest();
        changeRequest.AAN = aan;
        changeRequest.FirstPerference = drpFirstPerference.SelectedValue;
        if (drpsecondPerference.SelectedValue == "-1")  { changeRequest.SecondPerference =  string.Empty;}  else { changeRequest.SecondPerference = drpsecondPerference.SelectedValue; }
        if (drpThirdPerference.SelectedValue == "-1") { changeRequest.ThirdPerference = string.Empty; } else { changeRequest.ThirdPerference = drpThirdPerference.SelectedValue; }
        //changeRequest.SecondPerference = drpsecondPerference.SelectedValue;
        //changeRequest.ThirdPerference = drpThirdPerference.SelectedValue;
        changeRequest.QuarterCategory = Convert.ToInt32(drpQuarterCategory.SelectedValue);
        changeRequest.QuarterNumber = drpAllotedQuarter.SelectedValue;
        changeRequest.Name = txtUsername.Text;
        changeRequest.dateofsubmission = DateTime.Now;
        dataContext.tblChangeRequests.InsertOnSubmit(changeRequest);
        dataContext.SubmitChanges();
        empty();
        lblmessage.Text = "Information has been saved sucessfully!";
        lblmessage.Visible = true;

    }
    private void empty()
    {
        txtUsername.Text = "";
        drpAllotedQuarter.SelectedIndex = -1;
        drpFirstPerference.SelectedIndex = -1;
        drpsecondPerference.SelectedIndex = -1;
        drpThirdPerference.SelectedIndex = -1;
        }
    protected void bindQuarter()
    {
        drpFirstPerference.DataSource = Quarters.GetAllQuarters(Convert.ToInt64(drpQuarterCategory.SelectedValue));
        drpFirstPerference.DataBind();
        drpFirstPerference.Items.Insert(0, new ListItem("-select--", "-1"));

        drpsecondPerference.DataSource = Quarters.GetAllQuarters(Convert.ToInt64(drpQuarterCategory.SelectedValue));
        drpsecondPerference.DataBind();
        drpsecondPerference.Items.Insert(0, new ListItem("-select--", "-1"));

        drpThirdPerference.DataSource = Quarters.GetAllQuarters(Convert.ToInt64(drpQuarterCategory.SelectedValue));
        drpThirdPerference.DataBind();
        drpThirdPerference.Items.Insert(0, new ListItem("-select--", "-1"));
        drpAllotedQuarter.DataSource = Quarters.GetAllAllotedQuarters(Convert.ToInt64(drpQuarterCategory.SelectedValue));
        drpAllotedQuarter.DataBind();
        drpAllotedQuarter.Items.Insert(0, new ListItem("-select--", "-1"));

    }

    protected void drpQuarterCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindQuarter();
    }


    private void BIndUserData()
    {
        tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
        if (_user!=null)
        {
            txtUsername.Text = _user.fullName;
            if (_user.BaseOfficeId.HasValue)
                drpOffice.SelectedValue = _user.BaseOfficeId.Value.ToString();
            if (_user.designation.HasValue)
                drpdesignations.SelectedValue = _user.designation.Value.ToString();
        }               
    }
}