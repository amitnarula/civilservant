<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    Inherits="Admin_AddLicenceFee" CodeFile="~/Admin/AddLicenceFee.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>Quarter Licence Fee</h2>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <asp:Label ID="lblmessage" runat="server" Visible="false"></asp:Label></div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <table border="0" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>Category:
                        </td>
                        <td><asp:DropDownList ID="drpQuarterCat" CssClass="form-control" DataTextField="Name" OnSelectedIndexChanged="QuarterCategory_selectedindex"
                        AutoPostBack="true" DataValueField="Id" runat="server">
                    </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>License Fee:
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtLicenceFee" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Effective Date:
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtEffectiveDate" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid Date"
                        Operator="DataTypeCheck" ControlToValidate="txtEffectiveDate" Type="Date" Display="None"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEffectiveDate"
                        ErrorMessage="please enter Date." Display="None"></asp:RequiredFieldValidator>
                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server"
                        TargetControlID="txtEffectiveDate" /></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td> <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Add / Update" OnClick="btnadd_Click" runat="server">
                    </asp:Button>
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" Text="Cancel"
                        runat="server"></asp:Button></td>
                    </tr>
                </table>
            </div>
        </div>
        
    </div>
</asp:Content>
