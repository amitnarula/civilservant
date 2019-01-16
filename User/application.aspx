<%@ Page Title="" Language="C#" MasterPageFile="~/EstateBluePrivate.master" AutoEventWireup="true"
    CodeFile="application.aspx.cs" Inherits="User_application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>
                <asp:Label ID="lblStatus" runat="server" Text="My Applications"></asp:Label></h2>
        </div>
        <div class="">
            <br />
            <br />
            <div class="searchResults">
                <asp:Panel runat="server" ID="pnlSearch" Width="40%" Visible="false">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Button Text="New application" CssClass="btn btn-warning" ID="btnNewApplication"
                                runat="server" OnClick="btnNewApplication_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        
                        <table border="0" cellpadding="0" width="100%" cellspacing="0" class="table">
                            <tr>
                                
                                <td><asp:TextBox CssClass="form-control" placeholder="Search by AAN" runat="server" ID="txtSearch" /></td>
                                <td><asp:Button Text="Search" CssClass="btn btn-warning" ID="btnSearch" OnClick="btnSearch_Click"
                                        runat="server" /></td>
                            </tr>
                        </table>

                    </div>
                </asp:Panel>
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="grdQuarters" class="table table-bordered table-hover" AutoGenerateColumns="false"
                        Width="100%" runat="server" OnRowCommand="GridRowCommand" OnRowDataBound="gridrowdatabound">
                        <EmptyDataTemplate>
                            No Records!
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:HyperLinkField HeaderStyle-CssClass="gridHeader" HeaderText="AAN" DataNavigateUrlFields="AAN"
                                DataNavigateUrlFormatString="~/admin/userinformation.aspx?id={0}&returnurl=user/application.aspx"
                                DataTextField="AAN" />
                            <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                                HeaderText="Name" DataField="UserName" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="Designation" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Dept" DataField="Dept" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Type" DataField="QuarterType" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="view" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                    <asp:HyperLink ID="hypprint" Visible="false" runat="server" Text="Print" Target="_blank"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div class="table-responsive">
            </div>
        </div>
    </div>
</asp:Content>
