<%@ Page Title="" Language="C#" MasterPageFile="~/estateblue.master" AutoEventWireup="true"
    CodeFile="Forgot.aspx.cs" Inherits="Forgot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="container">
        <div id="managerContainerHeader">
            <h2>Forgot password</h2>
        </div>
        <br />
        <asp:ValidationSummary ID="valsummary" runat="server" ShowMessageBox="true" ShowSummary="false"
            HeaderText="The form is not submitted due to:" />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <asp:Label ID="lblmessage" Visible="false" runat="server"></asp:Label></div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Email :</div>
                <div>
                    <asp:TextBox ID="txtEmailId" CssClass="input input-md" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="Button1" CssClass="btn btn-success" Text="Submit" OnClick="btnSend_Click" runat="server"></asp:Button>
                    <asp:RegularExpressionValidator ID="regemail" ErrorMessage="Please enter valid email address"
                        ControlToValidate="txtEmailId" runat="server" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter email"
                        ControlToValidate="txtEmailId" Display="None"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>
