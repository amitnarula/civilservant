<%@ Page Title="" Language="C#" MasterPageFile="~/EstateBlue.master" AutoEventWireup="true"
    CodeFile="GuestHouse.aspx.cs" Inherits="GuestHouse" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
    <style type="text/css">
        .borderless tbody tr td, .borderless tbody tr th, .borderless thead tr th
        {
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <asp:ValidationSummary runat="server" ShowSummary="false" ShowMessageBox="true" ID="validationSummary"
        ValidationGroup="valGuestHouse" />
    <div class="container">
        <h4>
            Reservation of Guest House in Sector 17 and 42, Chandigarh</h4>
        <asp:Label Text="" ID="lblMessage" CssClass="text-danger" runat="server" />
        <p class="text-danger">
            Note: Your reservation for Guest House will be considered from 12 afternoon on last
            of booking.</p>
        <div class="table-resposive">
            <table border="0" cellpadding="0" class="table-responsive table borderless" cellspacing="0">
                <colgroup>
                    <col width="10%" />
                    <col width="35%" />
                    <col width="10%" />
                    <col />
                    <col />
                    <col width="70%" />
                </colgroup>
                <tr>
                    <td>
                        Name*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter name" ValidationGroup="valGuestHouse"
                            ControlToValidate="txtName" runat="server" Display="None" />
                    </td>
                    <td>
                    </td>
                    <td>
                        Purpose of Visit*
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPurposeOfVisit" CssClass="form-control">
                            <asp:ListItem Text="--SELECT" Value="select" />
                            <asp:ListItem Text="On Duty" />
                            <asp:ListItem Text="On Transfer" />
                            <asp:ListItem Text="Not on Duty" />
                            <asp:ListItem Text="Retired Person" />
                            <asp:ListItem Text="Near relation of IA&AD personnel" />
                            <asp:ListItem Text="Near relation of other Government Department" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator InitialValue="select" ErrorMessage="Please select purpose of visit"
                            Display="None" ValidationGroup="valGuestHouse" ControlToValidate="ddlPurposeOfVisit"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Email ID*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter email address" ControlToValidate="txtEmailAddress"
                            runat="server" Display="None" ValidationGroup="valGuestHouse" />
                        <asp:RegularExpressionValidator ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                            ValidationGroup="valGuestHouse" ErrorMessage="Please enter valid email address"
                            ControlToValidate="txtEmailAddress" runat="server" Display="None" />
                    </td>
                    <td>
                    </td>
                    <td>
                        Mobile Number*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNumber" CssClass="form-control" MaxLength="20" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter mobile number" ControlToValidate="txtMobileNumber"
                            runat="server" Display="None" ValidationGroup="valGuestHouse" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Name of the Guest House*
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlGuestHouse" ValidationGroup="valGuestHouse"
                            CssClass="form-control">
                            <asp:ListItem Text="--SELECT--" Value="select"></asp:ListItem>
                            <asp:ListItem Text="Guest House Sector 17" Value="Guest House Sector 17" />
                            <asp:ListItem Text="Guest House Sector 42" Value="Guest House Sector 42" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please select name of guest house"
                            ControlToValidate="ddlGuestHouse" ValidationGroup="valGuestHouse" runat="server"
                            InitialValue="select" Display="None" />
                    </td>
                    <td>
                    </td>
                    <td>
                        No. of Rooms*<asp:DropDownList ID="ddlRooms" runat="server" Width="70%" CssClass="form-control">
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        No. of people*<asp:DropDownList ID="ddlPeople" runat="server" Width="30%" CssClass="form-control">
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        From Date*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control" />
                        <asp:CompareValidator ID="cmpVal" ControlToCompare="txtToDate" runat="server" ErrorMessage="Please enter valid date range"
                            Operator="LessThanEqual" ValueToCompare="" ValidationGroup="valGuestHouse" ControlToValidate="txtFromDate"
                            Type="Date" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="valGuestHouse"
                            runat="server" ControlToValidate="txtFromDate" ErrorMessage="please enter from date"
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="txtFromDateExtender" runat="server"
                            TargetControlID="txtFromDate" />
                    </td>
                    <td>
                    </td>
                    <td>
                        To Date*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="valGuestHouse"
                            runat="server" ControlToValidate="txtFromDate" ErrorMessage="please enter to date"
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server"
                            TargetControlID="txtToDate" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:RadioButton Text="" ID="radIAADStaff" GroupName="radStaff" Checked="true" runat="server"
                            AutoPostBack="True" OnCheckedChanged="rad_CheckedChanged" />
                        <asp:Label ID="Label2" Text="IA&AS" runat="server" />
                        <asp:RadioButton Text="" ID="radIAADStaffExtended" GroupName="radStaff" runat="server"
                            AutoPostBack="True" OnCheckedChanged="rad_CheckedChanged" />
                        <asp:Label ID="Label3" Text="IA&AD Staff" runat="server" />
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        Additional Information
                    </td>
                    <td>
                        <asp:TextBox runat="server" MaxLength="100" ID="txtAdditionalInformation" CssClass="form-control"
                            TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:RadioButton Checked="true" Text="" ID="radSelf" GroupName="radStaff_sub" runat="server" />
                        Self*&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton Text="" ID="radGuest" GroupName="radStaff_sub" 
                            runat="server" />
                        Guest*</td>
                    <td>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox Enabled="false" ID="txtOfficeID" placeholder="Office ID" runat="server"
                            CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValOfficeID" Enabled="false" ValidationGroup="valGuestHouse"
                            ErrorMessage="Please enter Office ID" ControlToValidate="txtOfficeID" Display="None"
                            runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                        </td>
                    <td>
                        <asp:RadioButton Text="" ID="radGovtEmployee" GroupName="radStaff" runat="server"
                            AutoPostBack="True" OnCheckedChanged="rad_CheckedChanged" />
                        <asp:Label ID="Label1" Text="Other Government Employee*" runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Office and Place of Posting*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtOfficeAndPlaceOfPosting" CssClass="form-control"
                            MaxLength="100" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter place of posting" ControlToValidate="txtOfficeAndPlaceOfPosting"
                            runat="server" Display="None" ValidationGroup="valGuestHouse" />
                    </td>
                    <td>
                    </td>
                    <td>
                        Code
                    </td>
                    <td>
                        <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                            CaptchaHeight="60" CaptchaWidth="200" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                            CaptchaMaxTimeout="240" FontColor="#529E00" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Designation*
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="50" ID="txtDesignation" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter designation" ControlToValidate="txtDesignation"
                            runat="server" ValidationGroup="valGuestHouse" Display="None " />
                    </td>
                    <td>
                    </td>
                    <td>
                        Type the code shown*
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCaptcha" CssClass="form-control" MaxLength="10" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter captcha" ControlToValidate="txtCaptcha"
                            runat="server" Display="None" ValidationGroup="valGuestHouse" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="valGuestHouse"
                            runat="server" />
                    </td>
                    <td>
                        <asp:Button Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-warning" CausesValidation="false"
                            ID="btnReset" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
