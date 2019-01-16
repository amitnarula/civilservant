<%@ Page Title="" Language="C#" MasterPageFile="~/EstateBluePrivate.master" AutoEventWireup="true" CodeFile="Submission.aspx.cs" Inherits="Admin_Submission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="contentContainer">
<div id="managerContainerHeader" >
<SPAN >Enable/Disable submission</SPAN> 
</div>

    <div class="row">
        <div class="leftMiddleColumn">
            <div><asp:Label ID="lblmessage" runat="server"></asp:Label></div>
            
        </div>
        
    </div>
     <div class="row">
        <div class="leftMiddleColumn">
            <div><asp:Button ID="lnkSbmission" OnClick="lnkSbmission_Click" runat="server"></asp:Button></div>
            
        </div>
        
    </div>
    </div>
</asp:Content>

