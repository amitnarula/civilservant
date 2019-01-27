<%@ Page Title="" Language="C#" MasterPageFile="~/estateblue.master" AutoEventWireup="true"
    CodeFile="whatsnew.aspx.cs" Inherits="WhatsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    
    
    <table class="table table-condensed">
    <asp:Repeater runat="server" ID="rptCirculars">
        
        <HeaderTemplate>
            <tr>
                <th>Date</th>
                <th>Description</th>
                <th>Type</th>
                <th>Download</th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:literal text='<%#Eval("Date","{0:d}") %>' runat="server" />
                </td>
                <td>
                    <asp:Literal Text='<%#Eval("Description") %>' runat="server" />
                </td>
                <td>
                    <asp:Literal Text="" ID="litType" runat="server" />
                </td>
                <td>
                    <asp:LinkButton Text="Download" ID="lnkDownload" CommandName="downloadFile" CommandArgument='<%#Eval("Id") %>' runat="server" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <tr>
                <td colspan="3">
                <asp:Literal ID="Literal1" Text="Nothing to show." Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' runat="server" />
                </td>
            </tr>
            
        </FooterTemplate>
    </asp:Repeater>
    </table>
</asp:Content>
