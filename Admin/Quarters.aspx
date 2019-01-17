<%@ Page Title="" Language="C#" StylesheetTheme="SkinFile" MasterPageFile="~/estateblueprivate.master"
    AutoEventWireup="true" Inherits="Admin_Quarters" CodeFile="~/Admin/Quarters.aspx.cs" %>

<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>
                Quarter</h2>
        </div>
        <div>
            <br />
            <br />
            <div class="row">
                <div class="col-lg-12"> 
                <div class="row">
                    <div class="col-lg-2 fl">
                        Quarter Type:</div>
                    <div class="col-lg-8 fl">
                        <asp:DropDownList ID="drpQuarterCatergory" CssClass="form-control" DataTextField="Name" DataValueField="Id"
                            runat="server">
                        </asp:DropDownList>
                        
                    </div>
                    <div class="col-lg-2 fl">
                        <asp:Button ID="detai" CssClass="btn btn-warning" OnClick="detail_click" runat="server" Text="Get Detail" />
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <div>
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" OnRowDataBound="grdQuarters_databound" Width="100%"
                    AutoGenerateColumns="false" runat="server">
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
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Quarter" DataField="QuarterNumber" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="AAN" DataField="aan" />
                        <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                            HeaderText="Name of allotte" DataField="fullname" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="designation" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="office" DataField="office" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Possession"
                            DataField="DateOfPossession" DataFormatString="{0:d}" />
                        <%--<asp:BoundField DataField="Status" />
                        --%><asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Staus">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
