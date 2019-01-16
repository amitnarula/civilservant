<%@ page title="" language="C#" masterpagefile="~/sitemaster.master" autoeventwireup="true" inherits="signup" CodeFile="~/signup.aspx.cs"%>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<AjaxToolkit:ToolkitScriptManager  ID="scriptManager12"  runat="server"  EnableScriptLocalization="true" EnableScriptGlobalization="true">
 </AjaxToolkit:ToolkitScriptManager>
<div class="contentContainer">
<asp:ValidationSummary ID="valsummary" runat="server"  ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:"/> 

<div id="managerContainerHeader">
<SPAN >User</SPAN> 
</div>

<br /><br />
 <div class="row">
        <div class="leftMiddleColumn">
            <asp:Label ID="lblMessage" runat="server" Visible=false></asp:Label>
        </div>
       
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Username :</div>
            <div><asp:TextBox ID="txtUsername" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter user name" ControlToValidate="txtUsername" Display="None"></asp:RequiredFieldValidator></div>
        </div>
        <div class="rightMiddleColumn">
            <div>Full Name :</div>
            <div><asp:TextBox ID="txtFullName" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter full name" ControlToValidate="txtFullName" Display="None"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Email Id :</div>
            <div><asp:TextBox ID="txtEmailId" runat="server" ></asp:TextBox>
            <asp:RegularExpressionValidator Display="None" ID="regemail" ErrorMessage="Please enter valid email address" ControlToValidate="txtEmailId" runat="server" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter email" ControlToValidate="txtEmailId" Display="None"></asp:RequiredFieldValidator>
            
            </div>
        </div>
        <div class="rightMiddleColumn">
            <div>Office</div>
            <div><asp:DropDownList ID="drpOffice" DataTextField="Name" DataValueField="Id" runat="server" ></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ErrorMessage="Please select office" ControlToValidate="drpOffice" 
                    Display="None" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Designation :</div>
            <div><asp:DropDownList ID="drpDesignation" DataTextField="Name"  DataValueField="Id"  runat="server" ></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ErrorMessage="Please select designation" ControlToValidate="drpDesignation" 
                    Display="None" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
        </div>
        <div class="rightMiddleColumn">
            <div>Password:</div>
            <div>
              <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"  ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter password" ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
            
            </div>
        </div>
    </div>
    <div class="row">
     <div class="leftMiddleColumn">
         <div>Confirm Password:</div>
            <div>
                <asp:TextBox ID="txtconfirmpassword" TextMode="Password" runat="server"  ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter confirm password" ControlToValidate="txtconfirmpassword" Display="None"></asp:RequiredFieldValidator>
            <asp:CompareValidator Display="None" ID="cmpVal" ControlToCompare="txtPassword" ControlToValidate="txtconfirmpassword" Operator="Equal" runat="server"  ErrorMessage="Password and confirm password mismatch."></asp:CompareValidator>
            </div>
       </div>
    </div>
    <div class="row"></div>
    <div class="row">
        <div class="leftMiddleColumn">
            <center>
                <div><asp:Button ID="btnAddUpdate" Text="Add / Update" OnClick="btnadd_Click" runat="server" ></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button CausesValidation="false" ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel" runat="server" ></asp:Button></div>
            </center>
        </div>
        <div class="rightMiddleColumn">
            <center>
                <div></div>
            </center>
        </div>
    </div>
</div>
</asp:Content>

