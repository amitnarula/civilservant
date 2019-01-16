<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="Images.aspx.cs" Inherits="Admin_Images" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>Manage Images</h2>
        </div>
        <div class="">
            <div>
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>
        </div>
        <div class="">
            <br />
            <br />
            <div class="searchResults">
                <div class="table-responsive">
                    <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false"
                        runat="server" Width="100%" OnRowCommand="gridCommand">
                        <EmptyDataTemplate>
                            No Records!
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                                HeaderText="Title" DataField="title" />
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="img" runat="server" Height="100" Width="100" ImageUrl='<%# Eval("Imagepath")  %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPos" runat="server" CommandName="DeleteImage" CommandArgument='<%#Eval("id") %>'
                                        Text="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:LinkButton ID="lnkVerify" runat="server" Text="Verify" CommandName="Allotte" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                            --%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
