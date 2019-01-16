<%@ page title="" language="C#" masterpagefile="~/sitemaster.master" autoeventwireup="true" inherits="Admin_Licencefee" CodeFile="~/Admin/Licencefee.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<AjaxToolkit:ToolkitScriptManager  ID="scriptManager12"  runat="server"  EnableScriptLocalization="true" EnableScriptGlobalization="true">
 </AjaxToolkit:ToolkitScriptManager>
<div class="contentContainer">
<div id="managerContainerHeader" >
<SPAN >Quarter Licence fee</SPAN> 
</div>
<div class="row">
        <div class="leftMiddleColumn">
            
            <div><asp:Label ID ="lblmessage" Visible=false runat="server" ></asp:Label></div>
        </div>
        
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Quarter Number :</div>
            <div><asp:TextBox ID="txtQuarterNumber" runat="server" ></asp:TextBox></div>
        </div>
        
    </div>
    <div class="row">
    <div class="leftMiddleColumn">
            <div>Licence Fee:</div>
            <div><asp:TextBox ID="txtLicenceFee" runat="server" ></asp:TextBox></div>
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Month:</div>
            <div><asp:TextBox ID="txtmonth" runat="server"  ></asp:TextBox>
                <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="txtDobCalendarExtender" runat="server" TargetControlID="txtmonth" />
</div>
        </div>
        <div class="rightMiddleColumn">
          
        </div>
    </div>
    
 <div class="row"></div>
    <div class="row">
        <div class="leftMiddleColumn">
            <center>
              <asp:Button ID="btnAddUpdate" Text="Add / Update" OnClick="btnadd_Click" runat="server" ></asp:Button>
               &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel" runat="server" ></asp:Button>
            </center>
        </div>
        <div class="rightMiddleColumn">
            <center>
               
            </center>
        </div>
    </div>
</div>
</asp:Content>

