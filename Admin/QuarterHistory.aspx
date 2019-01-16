<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    Inherits="Admin_QuarterHistory" CodeFile="~/Admin/QuarterHistory.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div id="managerContainerHeader">
        <h2>Quarter History</h2>
    </div>
    <asp:ValidationSummary ID="valsummary" runat="server" ShowMessageBox="true" ShowSummary="false"
        HeaderText="The form is not submitted due to:" />
    <br />
    
    <div class="row">
        <div class="col-lg-12">
            <table border="0" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td>Quarter Number:
                    </td>
                    <td><asp:TextBox CssClass="form-control" ID="txtQuarterNo" runat="server"></asp:TextBox></td>
                    <td><asp:Button ID="Button1" CssClass="btn btn-primary" Text="GetHistory" runat="server" OnClick="getHistory_click" />
                <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter quarter number"
                    ControlToValidate="txtQuarterNo" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
            </table>
            
        </div>
    </div>
    <br />
    <div class="table-responsive">
        <asp:GridView ID="grdQuarters" Width="100%" CssClass="table table-bordered table-hover"
            AutoGenerateColumns="false" runat="server">
            <Columns>
                <asp:HyperLinkField HeaderText="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/admin/allottee.aspx?id={0}"
                    DataTextField="Name" />
                <asp:BoundField ItemStyle-Width="100px" ItemStyle-Wrap="true" HeaderText="Office"
                    DataField="Office" />
                <asp:BoundField HeaderText="AAN" DataField="AAN" />
                <asp:BoundField HeaderText="QuarterNumber" DataField="QuarterNumber" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
