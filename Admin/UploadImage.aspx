<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true" CodeFile="UploadImage.aspx.cs" Inherits="Admin_UploadImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="contentContainer">
<div id="managerContainerHeader">
<h2>Upload image</h2> 
</div>
<asp:ValidationSummary ID="valsummary" runat="server"  ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:"/> 

<br /><br />
<div>
    <div>
            <div><asp:Label ID="lblmessage" ReadOnly="true" runat="server" ></asp:Label></div>
        </div>
    </div>
    <div>
    <div>
            <div>Title:</div>
            <div><asp:TextBox ID="txtTitle" CssClass="form-control input-sm" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter title." ControlToValidate="txtTitle" Display="None"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
<div>
    <div>
            <div>File:</div>
            <div><asp:FileUpload ID="fileImage" runat="server" ></asp:FileUpload>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select file." ControlToValidate="fileImage" Display="None"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rexfiletype" runat="server" ControlToValidate="fileImage" ErrorMessage="Only png,jpeg,gif allowed" Display="None"  ValidationExpression="^([^\s]+(\.(?i)(jpg|png|gif|bmp))$)" ></asp:RegularExpressionValidator>
            
            </div>
        </div>
    </div>
    <br />
<div>
        <div>
            
                <div><asp:Button ID="btnUpload" CssClass="btn btn-primary" runat="server" onClick="btnUpload_click" Text="Upload" />
            &nbsp;<asp:Button ID="btnCancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation=false Text="Cancel" runat="server" ></asp:Button></div>
            
        </div>
        
    </div>
</div>
</asp:Content>

