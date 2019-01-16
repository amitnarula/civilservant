<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="userinformation.aspx.cs" Inherits="Admin_userinformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>
                User Information</h2>
        </div>
        <asp:ValidationSummary ID="valsummary" runat="server" ShowMessageBox="true" ShowSummary="false"
            HeaderText="The form is not submitted due to:" />
        <br />
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Username :</div>
                <div>
                    <asp:TextBox CssClass="form-control" ID="txtUsername" ReadOnly="true" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter user name"
                        ControlToValidate="txtUsername" Display="None"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Full Name :</div>
                <div>
                    <asp:TextBox ID="txtFullName" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter full name"
                        ControlToValidate="txtFullName" Display="None"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Email Id :</div>
                <div>
                    <asp:TextBox CssClass="form-control" ID="txtEmailId" ReadOnly="true" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regemail" ErrorMessage="Please enter valid email address"
                        ControlToValidate="txtEmailId" runat="server" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter email"
                        ControlToValidate="txtEmailId" Display="None"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Office</div>
                <div>
                    <asp:DropDownList ID="drpOffice" CssClass="form-control" Enabled="false" DataTextField="Name"
                        DataValueField="Id" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Role:</div>
                <div>
                    <asp:DropDownList CssClass="form-control" Enabled="false" ID="drpRoles" DataValueField="Id"
                        DataTextField="name" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Ok" OnClick="btnadd_Click"
                        CausesValidation="false" runat="server"></asp:Button>&nbsp;&nbsp;&nbsp;</div>
            </div>
        </div>
    </div>
</asp:Content>
