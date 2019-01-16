<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="User_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="contentContainer">
<div id="managerContainerHeader" >
<h2 >Change Password</h2> 
</div>
<br />
<asp:ValidationSummary ID="valsummary" runat="server"  ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:"/> 

<div>
        <div>
            
            <div><asp:Label ID ="lblmessage" Visible=false runat="server" ></asp:Label></div>
        </div>
        
    </div>
    <div>
        <div>
            <div>Old Password:</div>
            <div><asp:TextBox ID="txtOldPassword" CssClass="form-control" runat="server" TextMode=Password></asp:TextBox>
            
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter old password." ControlToValidate="txtOldPassword" Display="None"></asp:RequiredFieldValidator>
            </div>
        </div>
        
    </div>
    <div>
    <div>
            <div>New Password:</div>
            <div><asp:TextBox ID="txtNewPassword" CssClass="form-control" TextMode=Password runat="server" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter password." ControlToValidate="txtNewPassword" Display="None"></asp:RequiredFieldValidator>
           
            </div>
        </div>
    </div>
    <div>
        <div>
            <div>Confirm New Password:</div>
            <div><asp:TextBox ID="txtConfrimPassword" CssClass="form-control" TextMode=Password runat="server"  ></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter confirm password." ControlToValidate="txtConfrimPassword" Display="None"></asp:RequiredFieldValidator>
               <asp:CompareValidator ID="cmpPassword" runat="server" Display="None" ControlToCompare="txtNewPassword" ControlToValidate="txtConfrimPassword" ErrorMessage="New Password mismatch!"></asp:CompareValidator>
</div>
        </div>
        <div class="rightMiddleColumn">
          
        </div>
    </div>
    
 <div class="row"></div>
 <br />
    <div>
        <div>
            
              <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Add / Update" OnClick="btnadd_Click" runat="server" ></asp:Button>
               &nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel" runat="server" ></asp:Button>
            
        </div>
        
    </div>
</div>

</asp:Content>

