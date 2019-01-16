<%@ page title="" language="C#" masterpagefile="~/estateblueprivate.master" autoeventwireup="true" inherits="Admin_quarter" CodeFile="~/Admin/quarter.aspx.cs" %>
<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="contentContainer">
<div id="managerContainerHeader" >
<h2 >Quarters</h2> 
</div>
<br />
<asp:ValidationSummary ID="valsummary" runat="server"  ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:"/> 

    <div class="row">
        <div class="col-lg-12">
        <div class="row">
            <div class="col-lg-2 fl label-h">Category :</div>
            <div class="col-lg-8 fl"><asp:DropDownList ID="drpQuarterCat" CssClass="form-control" DataTextField="Name"  DataValueField="Id"  runat="server" ></asp:DropDownList></div>
        </div>
        </div>
        
    </div>
    <br />
    <div class="row">
    <div class="col-lg-12">
    <div class="row">
            <div class="col-lg-2 fl label-h">Quarter No:</div>
            <div class="col-lg-10 fl"><asp:TextBox ID="txtQuarterNo" runat="server" CssClass="form-control" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter quarter number" ControlToValidate="txtQuarterNo" Display="None"></asp:RequiredFieldValidator>
            </div>
            </div>
        </div>
    </div>
    
    
    <div class="row"></div>
    <br />
    <div>
        <div>
            
              <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Add / Update" OnClick="btnadd_Click" runat="server" ></asp:Button>
               &nbsp;&nbsp;<asp:Button CausesValidation="false" CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click" Text="Cancel" runat="server" ></asp:Button>
            
        </div>
        <div class="rightMiddleColumn">
            <center>
               
            </center>
        </div>
    </div>
</div>
</asp:Content>

