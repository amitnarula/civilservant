using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageGuestHouses : System.Web.UI.Page
{
    private void BindData()
    {
        if (!string.IsNullOrEmpty(Request["status"]) &&
            Convert.ToString(Request["status"]) == "allotted")
        {
            var data = GuestHouseRequest.GetGuestHouseRequestByStatus((int)GuestHouseRequest.GuestHouseRequestStatus.Allotted);

            grdGuestHouseRequests.DataSource = data;
            grdGuestHouseRequests.DataBind();
            grdGuestHouseRequests.Columns[11].Visible = false;
            grdGuestHouseRequests.Columns[12].Visible = true;

            lblLegend.Text = "Booked";

        }
        else
        {
            var data = GuestHouseRequest.GetGuestHouseRequestByStatus((int)GuestHouseRequest.GuestHouseRequestStatus.Pending);

            grdGuestHouseRequests.DataSource = data;
            grdGuestHouseRequests.DataBind();

            grdGuestHouseRequests.Columns[grdGuestHouseRequests.Columns.Count - 1].Visible = false;
            grdGuestHouseRequests.Columns[grdGuestHouseRequests.Columns.Count - 2].Visible = true;
            grdGuestHouseRequests.Columns[grdGuestHouseRequests.Columns.Count - 3].Visible = false;
            grdGuestHouseRequests.Columns[grdGuestHouseRequests.Columns.Count - 4].Visible = false;



        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            tblUser _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "GuestHouse");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }

        if (!IsPostBack)
        {
            BindData();
        }
        grdGuestHouseRequests.RowDataBound += new GridViewRowEventHandler(grdGuestHouseRequests_RowDataBound);
        btnSave.Click += new EventHandler(btnSave_Click);
        btnCancel.Click += new EventHandler(btnCancel_Click);
    }

    void btnCancel_Click(object sender, EventArgs e)
    {
        hdnRequestID.Value = string.Empty;
        ddlRoom.SelectedIndex = -1;
        ddlRoom.Items.Clear();
    }

    void btnSave_Click(object sender, EventArgs e)
    {
        GuestHouseRequest.AllotGuestHouse(Convert.ToInt32(hdnRequestID.Value), ddlRoom.SelectedValue);
        lblLegend.Text = "Guest house allotted successfully";
        BindData();
    }

    void grdGuestHouseRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblIsPaid = e.Row.FindControl("lblIsPaid") as Label;
            LinkButton lnkMarkAsPaid = e.Row.FindControl("lnkMarkAsPaid") as LinkButton;
            bool isPaid = Convert.ToBoolean(lblIsPaid.Text);

            lblIsPaid.Visible = isPaid;
            lblIsPaid.Text = "Paid";

            lnkMarkAsPaid.Visible = !isPaid;
        }
    }

    protected void grdGuestHouseRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int requestID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "deleteRequest")
        {
            GuestHouseRequest.DeleteRequest(requestID);
        }
        else if (e.CommandName == "allotRequest")
        {
            //Bind dropdowns as per request
            var currentRow = ((LinkButton)e.CommandSource).Parent.NamingContainer as GridViewRow;
            Label lblGuestHouse = currentRow.FindControl("lblGuestHouse") as Label;

            ddlRoom.Items.Clear();

            if (lblGuestHouse.Text == "Guest House Sector 17")
            {
                ddlRoom.Items.Add(new ListItem("Ravi"));//DELUX
                ddlRoom.Items.Add(new ListItem("Beas"));//DELUX
                ddlRoom.Items.Add(new ListItem("Satluj"));//DELUX
                ddlRoom.Items.Add(new ListItem("Chinab")); //VIP
                
            }
            else if (lblGuestHouse.Text == "Guest House Sector 42")
            {
                ddlRoom.Items.Add(new ListItem("Room 1"));//DELUX
                ddlRoom.Items.Add(new ListItem("Room 2"));//DELUX
                ddlRoom.Items.Add(new ListItem("Room 3"));//DELUX
            }

            hdnRequestID.Value = requestID.ToString();
            popSelectRoom.Show();

            
        }
        else if (e.CommandName == "markPaid")
        {
            GuestHouseRequest.MarkPaid(requestID);
            lblLegend.Text = "Payment received successfully and the record has been marked as paid";
        }
        else if (e.CommandName == "cancelBooking")
        {
            GuestHouseRequest.DeleteRequest(requestID);
            lblLegend.Text = "The booking request has been cancelled";
            //Send booking confirmation email
            var currentRow = ((LinkButton)e.CommandSource).Parent.NamingContainer as GridViewRow;
            Label lblEmail = currentRow.FindControl("lblEmail") as Label;

            SendmailMessage.SendMail(lblEmail.Text, "support@estatepagpb.org", "Request #" + requestID + " for Guest house has been cancelled"
                , "Guest house booking cancelled");
        }

        BindData();
    }
}