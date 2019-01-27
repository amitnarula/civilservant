<%@ Page Title="" Language="C#" MasterPageFile="~/EstateBluePrivate.master" AutoEventWireup="true"
    CodeFile="ManageWhatsNew.aspx.cs" Inherits="ManageWhatsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div>
        <asp:Literal Text="" ID="litMessage" runat="server" />
    </div>
    <table class="table table-condensed">
        <tr>
            <td>
                Date:
            </td>
            <td>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtDate" />
                <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="dtExtender" runat="server"
                    TargetControlID="txtDate">
                </AjaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator Display="Dynamic" ErrorMessage="Please enter date" ValidationGroup="save"
                    ControlToValidate="txtDate" runat="server" />
                
            </td>
        </tr>
        <tr>
            <td>
                Description:
            </td>
            <td>
                <asp:TextBox MaxLength="100" CssClass="form-control" runat="server" TextMode="MultiLine"
                    ID="txtDescription" />
            </td>
        </tr>
        <tr>
            <td>
                File:
            </td>
            <td>
                <asp:FileUpload CssClass="form-control" runat="server" ID="fileUpload" />
                <asp:CustomValidator ID="cusValidateFile" Display="Dynamic" ClientValidationFunction="validateFile"  ValidationGroup="save" ForeColor="Red" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button Text="Save" CssClass="btn btn-info" ID="btnSave" ValidationGroup="save"
                    runat="server" />
                <asp:Button Text="Cancel" CssClass="btn btn-danger" runat="server" ID="btnCancel" />
            </td>
        </tr>
    </table>
    <table class="table table-bordered">
        <asp:Repeater runat="server" ID="rptWhatsNew">
            <HeaderTemplate>
                <tr>
                    <th>
                        Date
                    </th>
                    <th>
                        Description
                    </th>
                    <th>
                        Preview
                    </th>
                    <th>
                        Actions
                    </th>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Literal ID="litDate" Text='<%#Eval("Date","{0:d}") %>' runat="server" />
                    </td>
                    <td>
                        <asp:Literal ID="litDescription" Text='<%#Eval("Description") %>' runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton Text="Preview" CommandName="previewFile" CommandArgument='<%#Eval("Id") %>' ID="lnkPreview" runat="server" />
                    </td>
                    <td>
                        <asp:Button Text="Delete" ID="btnDelete" CommandName="deleteContent" CommandArgument='<%#Eval("Id") %>'
                            runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="4">
                        <asp:Literal ID="litNothingToShow" Text="Nothing to show." Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>'
                            runat="server" />
                    </td>
                </tr>
            </FooterTemplate>
        </asp:Repeater>
    </table>
    
</asp:Content>
