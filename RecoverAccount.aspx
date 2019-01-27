<%@ Page Title="" Language="C#" MasterPageFile="~/EstateBlue.master" AutoEventWireup="true"
    CodeFile="RecoverAccount.aspx.cs" Inherits="RecoverAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlEnterInfo" CssClass="center-block" Width="50%">
        <div class="alert alert-info" runat="server" id="msgAreaAan" visible="false">
        </div>
        <table class="table table-condensed" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Enter AAN:
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAAN" MaxLength="50" />
                    <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="recover" ErrorMessage="Please enter AAN"
                        ControlToValidate="txtAAN" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-warning" Text="Recover password" ValidationGroup="recover"
                        ID="btnRecoverPassword" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlInfoOTP" Visible="false" CssClass="center-block"
        Width="40%">
        <div class="alert alert-success">
            An OTP has been sent to
            <asp:Literal Text="" ID="litPhoneNumber" runat="server" />. OTP is valid for 5 minutes
            only. Please enter OTP to change your password.</div>
        <div class="alert alert-info" id="msgAreaOtp"  runat="server" visible="false">
            </div>
        <table border="0" cellpadding="0" class="table table-condensed" cellspacing="0">
            <tr>
                <td>
                    Enter OTP:
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtOTP" ValidationGroup="otp"
                        MaxLength="10" />
                    <asp:RequiredFieldValidator ErrorMessage="Please enter OTP." ControlToValidate="txtOTP"
                        runat="server" ValidationGroup="otp" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button Text="Proceed" CssClass="btn btn-warning" ID="btnProceed" ValidationGroup="otp"
                        runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlChangePassword" Visible="false" CssClass="center-block"
        Width="60%">
       <div class="alert alert-info" runat="server" id="msgAreaPassword" visible="false"></div>
        <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" ErrorMessage="New password and confirm password should be same."
            ControlToValidate="txtNewPassword" runat="server" ControlToCompare="txtConfirmPassword"
            ValidationGroup="password" />
        <table border="0" cellpadding="0" class="table table-condensed" cellspacing="0">
            <tr>
                <td>
                    New password:
                </td>
                <td>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtNewPassword"
                        ValidationGroup="password" MaxLength="15" />
                    <asp:RequiredFieldValidator ID="reqNewPassword" ErrorMessage="Please enter password."
                        ControlToValidate="txtNewPassword" runat="server" ValidationGroup="password"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                    Confirm password:
                </td>
                <td>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control" ID="txtConfirmPassword"
                        ValidationGroup="password" MaxLength="15" />
                    <asp:RequiredFieldValidator ID="reqConfirmPassword" ErrorMessage="Please enter confirm password."
                        ControlToValidate="txtConfirmPassword" runat="server" ValidationGroup="password"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button Text="Change password" CssClass="btn btn-success" ID="btnChangePassword"
                        ValidationGroup="password" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
