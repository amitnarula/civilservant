<%@ Page Language="C#" StylesheetTheme="SkinFile" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true" CodeFile="retentedAllottes.aspx.cs" Inherits="Admin_retentedAllottes" %>

<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div id="managerContainerHeader">
        <h2>Allottment</h2>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12">
        <div class="row">
            <div class="col-lg-2 fl label-h">Quarter Type:</div>
            <div class="col-lg-8 fl">
                <asp:DropDownList ID="drpQuarterCatergory" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server"></asp:DropDownList>
                
            </div>
            <div class="col-lg-2">
                <asp:Button ID="detai" OnClick="detail_click" CssClass="btn btn-warning" runat="server" Text="Get Detail" />
            </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div>
            <div>
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>

        </div>
    </div>
    <br />
    <br />
    
    <div class="row" style="overflow: visible !important">
        <div class="table-responsive">
            <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" runat="server" Width="100%" OnRowCommand="grdAllotte_command">
                <EmptyDataTemplate>
                    No Records!
                </EmptyDataTemplate>
                <Columns>
                    <%--START : For displaying serial number--%>
                    <asp:TemplateField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader">
                        <HeaderTemplate>Sr. No.</HeaderTemplate>
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                    </asp:TemplateField>
                    <%--END : For displaying serial number--%>
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Quarter" DataField="QuarterNumber" />
                    <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader" HeaderText="Name of allotte" DataField="Name" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="designation" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="office" DataField="office" ItemStyle-Wrap="true" ItemStyle-Width="15px" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Possession" DataFormatString="{0:dd/MM/yyyy}" DataField="DateOfPossession" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Retension" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" DataField="Dateofretension" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Retension Upto" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" DataField="DateOfRetensionUpto" />

                    <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkchgretension" OnClientClick="return confirmmessagealert() ;" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Change Retention Period" Visible="true" CausesValidation="false" CommandName="Retention"></asp:LinkButton>
                            <asp:HiddenField  ID="hdnDateOfRetention" runat="server" Value='<%#Eval("DateOfRetensionUpto") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>




    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <asp:LinkButton ID="lnkAddMember" runat="server" Text="Add" Style="display: none;"></asp:LinkButton>
    <div id="Div2" runat="server" class="modal-dialog">
    <div class="modal-content">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valRetension" ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:" />
        <div class="modal-header">Date of Retention</div>
        <div class="modal-body">
            <table border="0" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td>Reason:</td>
                    <td>
                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlRetentionRule">
                            <asp:ListItem Text="--Select--" Value="0" />
                            <asp:ListItem Text="Resignation, dismissal or removal from service" />
                            <asp:ListItem Text="Termination of service or unauthorized absence without permission" />
                            <asp:ListItem Text="Retirement or terminal leave" />
                            <asp:ListItem Text="Death of the allottee" />
                            <asp:ListItem Text="Transfer to a place outside the place of posting" />
                            <asp:ListItem Text="Transfer to an ineligible office at the place of posting" />
                            <asp:ListItem Text="On proceeding on foreign service in India" />
                            <asp:ListItem Text="Deputation outside India" />
                            <asp:ListItem Text="Leave on medical grounds" />
                            <asp:ListItem Text="Training (Departmental) in India or abroad" />
                            <asp:ListItem Text="Study Leave in or outside India" />

                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDdlRetentionRule"
                         runat="server" InitialValue="0" ErrorMessage="Please select a valid retention reason" ControlToValidate="ddlRetentionRule" ValidationGroup="valRetension" Display="None"></asp:RequiredFieldValidator>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                    Select retention period:
                    </td>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:DropDownList CssClass="form-control" runat="server" ID="ddlRetentionPeriod" AutoPostBack="true" OnSelectedIndexChanged="ddlRetentionPeriod_SelectedIndexChanged">
                                <asp:ListItem Text="--Select--" Value="0" />
                                <asp:ListItem Text="4 Months" Value="4" />
                                <asp:ListItem Text="6 Months" Value="6" />
                                <asp:ListItem Text="8 Months" Value="8" />
                                <asp:ListItem Text="12 Months" Value="12" />
                                <asp:ListItem Text="2 Years" Value="24" />
                            </asp:DropDownList>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:RequiredFieldValidator ID="rfvRetentionMonth" runat="server" InitialValue="0" ErrorMessage="Please select a valid retention period" ControlToValidate="ddlRetentionPeriod" ValidationGroup="valRetension" Display="None"></asp:RequiredFieldValidator>
                    
                    </td>
                </tr>
                <tr>
                    <td>
                    Date of Retention:
                    </td>
                    <td><asp:TextBox ID="txtDateOfRetension" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Date of Retention upto:
                    </td>
                    <td>
                    <asp:UpdatePanel runat="server" ID="upPanelDateOfRetentionUpto" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtdateofretensionupto" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                </tr>
            </table>

        
        
        </div>
        <div class="modal-footer">
             <asp:Button ID="btnRetenstion" CssClass="btn btn-success" ValidationGroup="valRetension" runat="server" Text="Save" OnClick="btnSaveRetension_click" />&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" CausesValidation="false" ID="Button2" runat="server" Text="Cancel" />
        </div>
        
        </div>
    </div>
    <AjaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="overlay" runat="server" PopupControlID="Div2" TargetControlID="lnkAddMember" CancelControlID="Button2"></AjaxToolkit:ModalPopupExtender>

    <asp:HiddenField ID="hdnselected" Value="0" runat="server" />
</asp:Content>

