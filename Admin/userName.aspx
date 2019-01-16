<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    Inherits="Admin_userName" CodeFile="~/Admin/userName.aspx.cs" %>

<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div>
        <div>
            <h2>
                Add User</h2>
        </div>
        <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label>
        <asp:ValidationSummary ID="valsummary" runat="server" ShowMessageBox="true" ShowSummary="false"
            HeaderText="The form is not submitted due to:" />
        <br />
        <table border="0" cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="10%" />
                <col width="35%" />
                <col width="10%" />
                <col width="10%" />
                <col width="35%" />
            </colgroup>
            <tr>
                <td>
                    Username:
                </td>
                <td>
                    <asp:TextBox CssClass="form-control" ID="txtUsername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter user name"
                        ControlToValidate="txtUsername" Display="None"></asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
                <td>
                    Fullname:
                </td>
                <td>
                    <asp:TextBox ID="txtFullName" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter full name"
                        ControlToValidate="txtFullName" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                
                
                <td>
                    Office:
                </td>
                <td>
                    <asp:DropDownList ID="drpOffice" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server">
                        </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    EmailId:
                </td>
                <td>
                    <asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="regemail" ErrorMessage="Please enter valid email address"
                                    ControlToValidate="txtEmailId" runat="server" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter email"
                                    ControlToValidate="txtEmailId" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Password:
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter password"
                            ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                </td>
                <td>
                </td>
                <td>
                Confirm Password:
                </td>
                <td>
                    <asp:TextBox ID="txtconfirmpassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter confirm password"
                            ControlToValidate="txtconfirmpassword" Display="None"></asp:RequiredFieldValidator>
                        <asp:CompareValidator Display="None" ID="cmpVal" ControlToCompare="txtPassword" ControlToValidate="txtconfirmpassword"
                            Operator="Equal" runat="server" ErrorMessage="Password and confirm password mismatch."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Role:
                </td>
                <td>
                    <asp:DropDownList ID="drpRoles" CssClass="form-control" DataValueField="Id" DataTextField="name" runat="server">
                            </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        
            <div>
                <br />
                <div>
                    <div>
                        <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Add / Update" OnClick="btnadd_Click"
                            runat="server"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger"
                                CausesValidation="false" ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel"
                                runat="server"></asp:Button></div>
                </div>
            </div>
</asp:Content>
