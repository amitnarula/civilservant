<%@ Page Title="" Language="C#" MasterPageFile="~/estateblue.master" AutoEventWireup="true"
    CodeFile="whatsnew.aspx.cs" Inherits="WhatsNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <table class="table table-responsive">
    <asp:Repeater runat="server" ID="rptCirculars">
        
        <HeaderTemplate>
            <tr>
                <th>Date</th>
                <th>Description</th>
                <th>Download</th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:literal text='<%#Eval("PublishDate") %>' runat="server" />
                </td>
                <td>
                    <asp:Literal Text='<%#Eval("Description") %>' runat="server" />
                </td>
                <%--<td>
                    <asp:Literal Text='<%#Eval("ItemType") %>' runat="server" />
                </td>--%>
                <td>
                    <asp:HyperLink Text="Download" NavigateUrl="http://google.com" Target="_blank" runat="server" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>

        
        </FooterTemplate>
    </asp:Repeater>
    </table>
</asp:Content>
