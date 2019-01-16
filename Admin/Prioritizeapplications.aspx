<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    Inherits="Admin_AllotteQuarter" CodeFile="~/Admin/Prioritizeapplications.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>Prioritize Application</h2>
        </div>

        <div class="">
            <br />
            <table border="0" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    Quarter Type:
                    </td>
                    <td>
                    <asp:DropDownList CssClass="form-control" ID="drpQuarterCatergory" DataTextField="Name" DataValueField="Id"
                            runat="server">
                        </asp:DropDownList>
                        
                    </td>
                    <td>
                    <asp:Button CssClass="btn btn-success" ID="detai" OnClick="detail_click" runat="server" Text="Get Detail" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Allocation Mode:
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAllocationMode" AutoPostBack="true" CssClass="form-control">
                            <asp:ListItem Text="Automatic" />
                            <asp:ListItem Text="Manual" />
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>
                </div>
            </div>
            <br />
            <div class="table-responsive">
                <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" Width="100%" runat="server"
                    OnRowCommand="gridCommand">
                    <EmptyDataTemplate>
                        No Records!
                    </EmptyDataTemplate>
                    <Columns>
                        <%--START : For displaying serial number--%>
                        <asp:TemplateField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader">
                            <HeaderTemplate>
                                Sr. No.</HeaderTemplate>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <%--END : For displaying serial number--%>
                        <asp:HyperLinkField HeaderStyle-CssClass="gridHeader" HeaderText="AAN" DataNavigateUrlFields="AAN"
                            DataNavigateUrlFormatString="~/admin/userinformation.aspx?id={0}&returnurl=admin/Prioritizeapplications.aspx"
                            DataTextField="AAN" />
                        <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                            HeaderText="Name" DataField="UserName" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="Designation" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Dept" DataField="Dept" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date of joining" DataFormatString="{0:dd-MM-yyyy}"
                            DataField="dateOfJoining" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Category" DataField="Cast" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Grade pay" DataField="GradePay" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Already Allotted" DataField="AlreadyAllottedQuarter" />
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridHeader" />
                            <HeaderTemplate>
                                Quarter Number
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblQuarterNumber" Text='<%#Eval("QuarterNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton Text="Allotte" ID="lnkManualAllot" CommandArgument='<%#Eval("Id") %>'
                                CommandName="AllotteManual" Visible="false" runat="server" />
                                <asp:LinkButton ID="lnkAllotte" runat="server" CommandName="Allotte" OnClientClick="return confirmmessagealert() ;"
                                    CommandArgument='<%#Eval("Id") %>' Text="Allotte"></asp:LinkButton>
                                <asp:HiddenField ID="hidAAN" runat="server" Value='<%#Eval("AAN") %>' />
                                <asp:LinkButton ID="lnkWithdraw" runat="server" CommandName="Withdraw" Enabled="true"
                                    CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirmmessagealert() ;"
                                    Text="Withdraw"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                        <%--<asp:LinkButton ID="lnkVerify" runat="server" Text="Verify" CommandName="Allotte" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        --%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlControls" Visible="false">
        <table border="0" cellpadding="10" class="table" cellspacing="0" >
            <tr>
                <td>
                    <asp:Button Text="Verify" ID="btnVerified" CssClass="btn btn-primary" OnClick="btnChangeStatus_Click" Enabled="false" runat="server" />
                </td>
                <td>
                    <asp:Button Text="Validate" ID="btnValidated" CssClass="btn btn-primary" OnClick="btnChangeStatus_Click" Enabled="false" runat="server" />
                </td>
                <td>
                    <asp:Button Text="Approve" ID="btnApproved" CssClass="btn btn-success" OnClick="btnChangeStatus_Click" Enabled="false" runat="server" />
                </td>
                <td>
                    <asp:Button Text="Publish" ID="btnPublished" CssClass="btn btn-primary" OnClick="btnChangeStatus_Click" Enabled="false" runat="server" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        
    </div>
</asp:Content>
