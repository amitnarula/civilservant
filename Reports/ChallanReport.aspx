<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true" CodeFile="ChallanReport.aspx.cs" Inherits="Reports_ChallanReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="contentContainer" style="width:100%">
<div id="managerContainerHeader">
<h2 >Challan Report</h2> 
</div>

<div class=""><br /><br />
    <div class="row">
        <div class="col-lg-12">
            <div>
            <asp:Button ID="btnGenerateReport" CssClass="btn btn-primary" Text="Generate report" runat="server" onclick="btnGenerateReport_click"/>
            </div>
        </div>
        
    </div>
</div>
</div>
</asp:Content>

