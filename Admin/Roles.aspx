<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    Inherits="Admin_Roles" CodeFile="~/Admin/Roles.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div id="managerContainerHeader">
        <h2>Roles</h2>
    </div>
    <br />
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-2 fl">
                    User Roles:</div>
                <div class="col-lg-10 fl">
                    <asp:DropDownList ID="drpRoles" CssClass="form-control" Width="150px" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="drpRoles_SelectedIndexChanged" DataValueField="Id" DataTextField="Name">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div>
        <div>
            Modules:
        </div>
    </div>
    <div>
        <div class="leftMiddleColumn">
            <asp:CheckBoxList DataTextField="Name" DataValueField="id" runat="server" ID="chkLstModules"
                RepeatColumns="2" RepeatDirection="Horizontal">
            </asp:CheckBoxList>
        </div>
    </div>
    <div>
        <div>
            
                <div>
                    <asp:Button ID="btnAddUpdate" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmit_Click" runat="server">
                    </asp:Button>&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click"
                        Text="Cancel" runat="server"></asp:Button></div>
            
        </div>
        <div class="rightMiddleColumn">
            <center>
                <div>
                </div>
            </center>
        </div>
    </div>
</asp:Content>
