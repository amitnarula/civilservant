<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="Quarter.aspx.cs" Inherits="Reports_Quarter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>
                Quarter Report</h2>
        </div>
        <div class="">
            <br />
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <table border="0" class="table" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                Quarter Status:
                            </td>
                            <td>
                                <asp:DropDownList CssClass="form-control" ID="drpQuarterStatus" runat="server">
                                    <asp:ListItem Value="0">Vacant</asp:ListItem>
                                    <asp:ListItem Value="1">Alloted</asp:ListItem>
                                    <asp:ListItem Value="2">Possessed</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnGenerateReport" CssClass="btn btn-primary" Text="Generate report"
                                    runat="server" OnClick="btnGenerateReport_click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
