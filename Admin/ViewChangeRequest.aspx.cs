using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;

public partial class Admin_ViewChangeRequest : System.Web.UI.Page
{
    tblUser _user; 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "Allotment");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }

        if (!Page.IsPostBack)
        {
            Bindcategory();
        }

        grdQuarters.RowDataBound += new GridViewRowEventHandler(grdQuarters_RowDataBound);
    }

    void grdQuarters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hidAllotmentStatus = e.Row.FindControl("hidAllotmentStatus") as HiddenField;
            HiddenField hidDateOfAllotment = e.Row.FindControl("hidDateOfAllotment") as HiddenField;
            if (Convert.ToInt32(hidAllotmentStatus.Value) == ((int)(AllotementStatus.ChangeRequested)))
            {
                ((LinkButton)e.Row.FindControl("lnkAllotte")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnkPos")).Visible = true;
                ((LinkButton)e.Row.FindControl("lnkSurrender")).Visible = true;
                ((LinkButton)e.Row.FindControl("lnkWithdraw")).Visible = false;
                DateTime dtOfAllotment = Convert.ToDateTime(hidDateOfAllotment.Value);
                if (dtOfAllotment.Date.AddDays(30) <= DateTime.Now.Date)
                {
                    ((LinkButton)e.Row.FindControl("lnkAllotte")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkPos")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lnkWithdraw")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lnkSurrender")).Visible = false;

                }/*commented on 30/09/2016 purposely, uncommented on 28/11/2016 as per request purposely*/
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lnkAllotte")).Visible = true;
                ((LinkButton)e.Row.FindControl("lnkPos")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnkWithdraw")).Visible = false;
            }

        }
    }
    protected void detail_click(object sender, EventArgs e)
    {
        lblMessage.Text = "Applications for Quarter Category: " + drpQuarterCatergory.SelectedItem.Text;
        lblMessage.Visible = true;
        BindGrid();
    }
    private void Bindcategory()
    {
        drpQuarterCatergory.DataSource = Quarters.GetQuarterCategory();
        drpQuarterCatergory.DataBind();
    }
    //modified by :anki :)
    private void BindGrid()
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var requests = from request in dataContext.tblChangeRequests
                       join request1 in dataContext.tblUsers on request.AAN equals request1.AAN
                       join request2 in dataContext.tblDesignations on request1.designation equals request2.Id
                       join request3 in dataContext.tblOffices on request1.BaseOfficeId equals request3.Id
                       join request4 in dataContext.tblAllottees on request.AAN equals request4.AAN
                       join request5 in dataContext.tbAllotmentApplications on request1.Id equals request5.Userid
                       where request.QuarterCategory == Convert.ToInt32(drpQuarterCatergory.SelectedValue)
                       && request.Status==(int)ChangeRequestStatus.Pending //show only pending one
                       orderby request.dateofsubmission ascending
                       select new
                       {
                           request.Id,
                           request.AAN,
                           dateofsubmission = ResolveToIndianDateTime(request.dateofsubmission),
                           FirstPerference = 
                             (
                              request.FirstPerference== "-1"? string.Empty : request.FirstPerference
                             ),
                           SecondPerference =
                               (
                               request.SecondPerference == "-1" ? string.Empty : request.SecondPerference
                               ),
                           ThirdPerference =
                               (
                               request.ThirdPerference == "-1" ? string.Empty : request.ThirdPerference
                               ),
                           //request.SecondPerference,
                           //request.ThirdPerference,
                           request.Name,
                           request.QuarterCategory,
                           request.QuarterNumber,
                           Designation  = request2.Name ,
                           Dept = request3.Name,
                           AllotmentStatus = request4.Status.Value,
                           DateOfAllotment = request4.DateOfAllotement.Value,
                           DateOfChangeSubmission = ResolveToIndianDateTime(request.dateofsubmission.Value),
                           AppId = request5.ID
                       };
               
//        = "select cr.*, d.name as Designation, o.name as Office from tblchangerequests cr"+
//"inner join tblusers u on u.fullname = cr.name"+
//"inner join tbldesignations d on u.designation = d.id" +
//"inner join tbloffices o on u.baseofficeid = o.id"
        grdQuarters.DataSource = requests.ToList();
        grdQuarters.DataBind();
    }

    private DateTime ResolveToIndianDateTime(DateTime? input) {
        DateTime dt = input.HasValue ? input.Value : DateTime.Now;
        //DateTime utc = TimeZoneInfo.ConvertTimeToUtc(dt);
        //DateTime temp = new DateTime(utc.Ticks, DateTimeKind.Utc);
        //DateTime ist = TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        //return ist;

        return dt.AddHours(13).AddMinutes(30); //pst and ist difference

        /*TimeZoneInfo timeZoneInfo;
        DateTime dateTime;
        //Set the time zone information to US Mountain Standard Time 
        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //Get date and time in US Mountain Standard Time 
        dateTime = TimeZoneInfo.ConvertTime(input, timeZoneInfo);
        //Print out the date and time
        //Console.WriteLine(dateTime.ToString("yyyy-MM-dd HH-mm-ss"));
        return dateTime;*/


        /*DateTime dt = DateTime.UtcNow;
        TimeZoneInfo usEasternZone = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time"); // say
        TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"); // say
        DateTime usEasternTime = TimeZoneInfo.ConvertTimeFromUtc(dt, usEasternZone);
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(dt, indianZone);
        TimeSpan timeDiff = indianTime - usEasternTime;
        usEasternTime = input; // or whatever the datetime to convert is
        //Console.WriteLine("US Eastern Time {0}", usEasternTime);
        indianTime = usEasternTime + timeDiff;
        //Console.WriteLine("India Time Zone {0}", indianTime);
        return indianTime;*/
    }

    protected void gridCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Allotte")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);

            var gridRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;


            HiddenField hidApplicationId = gridRow.FindControl("hidApplicationId") as HiddenField;

            //AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Allotted);
            Response.Redirect("~/admin/Allottee.aspx?appid=" + hidApplicationId.Value + "&ChangeRequestID=" + id + "&returnurl=admin/ViewChangeRequest.aspx");
        }
        else if (e.CommandName == "Withdraw")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            DataClassesDataContext dataContext = new DataClassesDataContext();
           var requests = from request in dataContext.tblChangeRequests where request.Id == id select request;
           tblChangeRequest tblreject = requests.FirstOrDefault();
           if (tblreject != null)
           {
                //Added on January 24,2017
                tblAllottee allottee = Allottee.GetAllotteeByAAN(tblreject.AAN);
                Quarters.UpdateQuarterStatus(allottee.QuarterNumber, QuarterStatus.Vacant);

                //update allottee
                //Reset the quarter to previous one at the time of change request
                allottee.QuarterNumber = tblreject.QuarterNumber;
                Allottee.Update(allottee);
                //Added on January 24,2017 end

                //dataContext.tblChangeRequests.DeleteOnSubmit(tblreject);
                tblreject.Status = (int)ChangeRequestStatus.Deleted;
               dataContext.SubmitChanges();
           }
            BindGrid();
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.rejected);
            //Quarters.UpdateQuarterStatus(tblreject.QuarterNumber, QuarterStatus.Vacant); //Commented on Dec 12,2016 purposely

        }
        else if (e.CommandName == "Possesed")
        {
            var gridRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
            LinkButton lnkAllotte = gridRow.FindControl("lnkAllotte") as LinkButton;
            hdnChangeRequestId.Value = lnkAllotte.CommandArgument;

            hdnSelected.Value = e.CommandArgument.ToString();
            tblAllottee _allotte = Allottee.getAllotteByApplicationid(Convert.ToInt64(e.CommandArgument));
            txtName.Text = AllotementApplications.GetApplication(Convert.ToInt64(e.CommandArgument)).tblUser.fullName;
            if (_allotte != null)
                txtQuarternumber.Text = _allotte.QuarterNumber;

            pop.Show();
        }
        else if(e.CommandName == "Surrender")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            DataClassesDataContext dataContext = new DataClassesDataContext();
            var requests = from request in dataContext.tblChangeRequests where request.Id == id select request;
            tblChangeRequest tblsurrendered = requests.FirstOrDefault();
            if (tblsurrendered != null)
            {
                //Added on January 24,2017
                tblAllottee allottee = Allottee.GetAllotteeByAAN(tblsurrendered.AAN);
                Quarters.UpdateQuarterStatus(allottee.QuarterNumber, QuarterStatus.Vacant);

                //update allottee
                //Reset the quarter to previous one at the time of change request
                allottee.QuarterNumber = tblsurrendered.QuarterNumber;
                Allottee.Update(allottee);
                //Added on January 24,2017 end

                tblsurrendered.Status = (int)ChangeRequestStatus.Deleted;
                dataContext.SubmitChanges();
            }
            BindGrid();
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.rejected);
            //Quarters.UpdateQuarterStatus(tblreject.QuarterNumber, QuarterStatus.Vacant); //Commented on Dec 12,2016 purposely
        }

    }

    protected void btnsaveMember_click(object sender, EventArgs e)
    {
        possession();
        BindGrid();
    }
    private void possession()
    {
        long id;
        Int64.TryParse(hdnSelected.Value, out id);

        long changeRequestId;
        Int64.TryParse(hdnChangeRequestId.Value, out changeRequestId);


        AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Pos);
        tbAllotmentApplication app = AllotementApplications.GetApplication(id);
        tblAllottee _allotte = Allottee.getAllotteByApplicationid(id);
        if (_allotte != null)
        {
            DateTime _dateofPossession;
            DateTime.TryParse(txtDateOfPossession.Text, out _dateofPossession);
            _allotte.Status = (int)AllotementStatus.Possessed;
            _allotte.DateOfPossession = _dateofPossession;
            Allottee.Update(_allotte);

        }

        if (id > 0)
            AllotementApplications.UpdateApplicationStaus(id, ApplicationStatus.Pos);

        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            var changeRequests = dc.tblChangeRequests.Where(x => x.AAN == _allotte.AAN).ToList();
            //mark all change requests to delete now
            if (changeRequests.Any()) {
                changeRequests.ForEach(x => {
                    x.Status = (int)ChangeRequestStatus.Deleted;
                });
            }
            dc.SubmitChanges();
            
        }

        /*DataClassesDataContext dataContext = new DataClassesDataContext();
        var requests = from request in dataContext.tblChangeRequests where request.Id == changeRequestId select request;
        /*tblChangeRequest tblDelete = requests.FirstOrDefault();
        if (tblDelete != null)
        {
            dataContext.tblChangeRequests.DeleteOnSubmit(tblDelete);
            dataContext.SubmitChanges();
        } //Finally deleting the change request changed on 02-06-2016*/

        /*tblChangeRequest tblUpdate = requests.FirstOrDefault();
        if (tblUpdate != null)
        {
            //tblUpdate.Status = (int)ChangeRequestStatus.Approved;
            tblUpdate.Status = (int)ChangeRequestStatus.Deleted;
            dataContext.SubmitChanges(); //Updating the status of request to approved
        }*/

        
        tbluserhistory _userhistory = new tbluserhistory();
        _userhistory.Action = "Possession";
        _userhistory.description = _user.Username + " has marked possesed application with " + id;
        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);
    }
}