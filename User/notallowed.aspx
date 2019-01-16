<%@ Page Language="C#" AutoEventWireup="true"
MasterPageFile="~/estateblueprivate.master" CodeFile="notallowed.aspx.cs" Inherits="User_notallowed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>
                You are not allowed to perform this action!</h2>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <h4><asp:Label Text="" ID="lblReason" runat="server" /></div></h4>
            </div>
        </div>
    </div>
</asp:Content>

