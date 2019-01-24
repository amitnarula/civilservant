<%@ Page Title="" Language="C#" StylesheetTheme="SkinFile" MasterPageFile="~/estateblueprivate.master"
    AutoEventWireup="true" Inherits="Admin_Users" CodeFile="~/Admin/Users.aspx.cs" %>

<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div id="managerContainerHeader">
        <h2>Users</h2>
    </div>
    <br />

    <%--<asp:ScriptManager ID="scrpmgr" runat="server">
    </asp:ScriptManager>--%>
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="updusers" runat="server">
        <ContentTemplate>
            <div>
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            </div>
            
            
    <table class="table">
        <tr>
            <td><asp:DropDownList ID="drpRoles" CssClass="form-control" runat="server" AutoPostBack="true"
                    DataValueField="Id" DataTextField="Name">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:TextBox runat="server" ID="txtSearchByName" CssClass="form-control" /></td>
            <td><asp:Button Text="Search by name" ID="btnSearch" runat="server" CssClass="btn btn-info"
                    OnClick="btnSearch_Click" /></td>
        </tr>
    </table>

    
<div class="table-responsive">
                <asp:GridView ID="grdUsers" CssClass="table table-bordered table-hover" DataSourceID="ObjectDataSource1"
                    AllowPaging="true" PageSize="20" Width="100%" AutoGenerateColumns="false" runat="server"
                    OnRowCommand="grdCommad">
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
                        <asp:HyperLinkField HeaderText="UserName" HeaderStyle-CssClass="gridHeader" DataNavigateUrlFields="Id"
                            DataNavigateUrlFormatString="~/admin/userName.aspx?id={0}" DataTextField="Username" />
                        <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                            HeaderText="Full Name" DataField="fullName" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Email ID" DataField="EmailID" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Joining" DataField="DateOfJoining" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Retirement"
                            DataField="DateOfRetirement" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' ID="lnk" CommandName="deleteuser"
                                    Text="Delete" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' ID="lnkDebar" CommandName="debarUser"
                                    Text="Debar" runat="server"></asp:LinkButton>
                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' ID="lnkDefer" CommandName="deferUser"
                                    Text="Defer" runat="server"></asp:LinkButton>
                                <asp:LinkButton Text="Remove Bar" ID="lnkRemoveBar" CommandName="removeBar"
                                CommandArgument='<%# Eval("Id") %>' runat="server" />
                                <asp:LinkButton Text="Remove Defer" ID="lnkRemoveDefer" CommandName="removeDefer"
                                CommandArgument='<%# Eval("Id") %>' runat="server" />

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

          
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="Users" SelectMethod="SelectPage"
                FilterExpression="RoleId='{0}' and fullName like '%{1}%'" EnablePaging="false" OnFiltering="ObjectDataSource1_Filtering">
                <FilterParameters>
                    <asp:ControlParameter Name="Roleid" ControlID="drpRoles" PropertyName="SelectedValue" />
                    <asp:ControlParameter Name="Name"  ControlID="txtSearchByName" PropertyName="Text" />
                </FilterParameters>
            </asp:ObjectDataSource>
            <AjaxToolkit:ModalPopupExtender ID="popDefer" BackgroundCssClass="overlay" runat="server"
        PopupControlID="divDefer" TargetControlID="lnkDummy" >
    </AjaxToolkit:ModalPopupExtender>
    <asp:LinkButton ID="lnkDummy" runat="server" Text="Add" Style="display: none;"></asp:LinkButton>
            <div id="divDefer" runat="server" class="modal-dialog">
               
                <div class="modal-content">
                    <div class="modal-header">
                        Select defer period</div>
                    <div class="modal-body">
                        <table width="100%" class="table">
                            <tr>
                                <td>Defer for:</td>
                                <td>
                                    <asp:DropDownList ID="ddlDeferPeriod" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="3 Months" Value="3" />
                                        <asp:ListItem Text="6 Months" Value="6" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                            <asp:HiddenField runat="server" ID="hdnSelectedUserId" />
                            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save" />
                            &nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger"
                            ID="btnCancel" runat="server" Text="Cancel"  />
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
            </div>
    
        </ContentTemplate>

        
    

    </asp:UpdatePanel>
    <asp:UpdateProgress AssociatedUpdatePanelID="updusers" runat="server" ID="prog">
        <ProgressTemplate>
            <div id="IMGDIV" align="center" style="position: absolute; padding-top: 25px; top: 250px;
                display: none; left: 400px; background-color: #292929; vertical-align: middle;
                border-style: inset; border-color: black; background-color: White; width: 100px;
                height: 50px; z-index: 1000;">
                <img alt="Wait" src="../images/ajaxloader.gif" /><br />
                <%--<input type="button" onclick="CancelPostBack()" value="Cancel" />--%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
