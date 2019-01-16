<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="Confirmation.aspx.cs" Inherits="Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer">
        <asp:ValidationSummary ID="valsummary" runat="server" ShowMessageBox="true" ShowSummary="false"
            HeaderText="The form is not submitted due to:" />
        <div id="managerContainerHeader">
            <h2>
                User</h2>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-lg-12">
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    Your account has been created sucessfully. You will receive email shortly.</div>
            </div>
        </div>
        <div class="row">
        </div>
    </div>
</asp:Content>
