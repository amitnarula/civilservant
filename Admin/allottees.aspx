<%@ Page Title="" Language="C#" StylesheetTheme="SkinFile" MasterPageFile="~/estateblueprivate.master"
    AutoEventWireup="true" Inherits="Admin_allottees" CodeFile="~/Admin/allottees.aspx.cs" %>

<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div id="managerContainerHeader">
        <h2>
            Allottment</h2>
    </div>
    <br />
    <div class="row">
        <table border="0" class="table" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Quarter Type:
                </td>
                <td>
                    <asp:DropDownList CssClass="form-control" ID="drpQuarterCatergory" DataTextField="Name"
                        DataValueField="Id" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-warning" ID="detai" OnClick="detail_click" runat="server"
                        Text="Get Detail" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div>
                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>
        </div>
    </div>
    <br />
    <div class="row" style="overflow: visible !important">
        <div class="table-responsive">
            <asp:GridView ID="grdQuarters" Width="100%" CssClass="table table-bordered table-hover"
                OnRowDataBound="grdQuarters_RowDataBound" AutoGenerateColumns="false" runat="server"
                OnRowCommand="grdAllotte_command">
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
                    <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                        HeaderText="Name of allotte" DataField="Name" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="designation" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="office" DataField="office" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Possession"
                        DataField="DateOfPossession" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField HeaderStyle-CssClass="gridHeader" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderText="Date Of Retention Upto" DataField="DateOfRetensionUpto" />
                    <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="vacant" OnClientClick="return confirmmessagealert() ;" runat="server"
                                CommandArgument='<%# Eval("Id") %>' Text="Vacant" CausesValidation="false" CommandName="vacant"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="LinkButton1" OnClientClick="return confirmmessagealert() ;" runat="server"
                                CommandArgument='<%# Eval("Id") %>' Text="Retention" CausesValidation="false"
                                CommandName="Retention"></asp:LinkButton>
                            <asp:LinkButton ID="lnkchgretension" runat="server" OnClientClick="return confirmmessagealert() ;"
                                CommandArgument='<%# Eval("Id") %>' Text="Change Retention Period" Visible="false"
                                CausesValidation="false" CommandName="Retention"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="addmembers"  runat="server" class="modal-dialog">
        <asp:ValidationSummary ID="valsummary" runat="server" ValidationGroup="member" ShowMessageBox="true"
            ShowSummary="false" HeaderText="The form is not submitted due to:" />
            <div class="modal-content">            <div class="modal-header">
                Date of Vacation
            </div>
            <div class="modal-body">
                <table border="0" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>Name:
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Quarter Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuarternumber" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Date of Vacaction:
                        </td>
                        <td>
                        <asp:TextBox ID="txtDateOfVacation" CssClass="form-control" runat="server"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server"
                        TargetControlID="txtDateOfVacation" />
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please select date of Vacation."
                        ControlToValidate="txtDateOfVacation" ValidationGroup="member" Display="None"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modal-footer">
                <table border="0" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                        <asp:Button ID="btnsaveMember" CssClass="btn btn-success" ValidationGroup="member" OnClick="btnVacant_click"
                        runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;<asp:Button CausesValidation="false"
                            ID="btnCancemember" CssClass="btn btn-danger" runat="server" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </div>
            </div>

        
    </div>
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <AjaxToolkit:ModalPopupExtender ID="popVacant" BackgroundCssClass="overlay" runat="server"
        PopupControlID="addmembers" TargetControlID="lnkAddMember" CancelControlID="btnCancemember">
    </AjaxToolkit:ModalPopupExtender>
    <asp:LinkButton ID="lnkAddMember" runat="server" Text="Add" Style="display: none;"></asp:LinkButton>
    <div id="Div2"  runat="server" class="modal-dialog">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valRetension"
            ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:" />
            <div class="modal-content">
                <div class="modal-header">Date of Retention</div>
                <div class="modal-body">
                    <table width="100%" class="table">
            
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:TextBox CssClass="form-control" ID="txtRetName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Quarter Number:
                </td>
                <td>
                    <asp:TextBox CssClass="form-control" ID="txtRetQuarerNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Retention Start Date:
                </td>
                <td>
                    <asp:TextBox CssClass="form-control" ID="txtDateOfRetension" runat="server"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server"
                        TargetControlID="txtDateOfRetension" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select start date of retention."
                        ControlToValidate="txtDateOfRetension" ValidationGroup="valRetension" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Select Retention Period:
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
                    <asp:RequiredFieldValidator ID="rfvRetentionMonth" runat="server" InitialValue="0"
                        ErrorMessage="Please select a valid retention period" ControlToValidate="ddlRetentionPeriod"
                        ValidationGroup="valRetension" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Reason</td>
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
                    Date of Retention upto:
                </td>
                <td>
                    <asp:UpdatePanel runat="server" ID="upPanelDateOfRetentionUpto" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox CssClass="form-control" runat="server" ID="txtdateofretensionupto" Enabled="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--<asp:TextBox ID="txtdateofretensionupto" runat="server"></asp:TextBox>
                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender3" runat="server" TargetControlID="txtdateofretensionupto" />--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select date of retension upto."
                        ControlToValidate="txtdateofretensionupto" ValidationGroup="valRetension" Display="None"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Remarks:
                </td>
                <td>
                    <asp:TextBox CssClass="form-control" runat="server" ID="txtRemarks" TextMode="MultiLine" MaxLength="50" Width="98%" />
                    <asp:RequiredFieldValidator runat="server" ID="rfvTxtRemarks" ErrorMessage="Please enter remarks"
                        ControlToValidate="txtRemarks" Display="None" ValidationGroup="valRetension"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
        </table>
                </div>
                <div class="modal-footer">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                            <asp:Button ID="btnRetenstion" CssClass="btn btn-success" ValidationGroup="valRetension" runat="server" Text="Save"
                        OnClick="btnSaveRetension_click" />&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" CausesValidation="false"
                            ID="Button2" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        
    </div>
    <AjaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="overlay"
        runat="server" PopupControlID="Div2" TargetControlID="lnkAddMember" CancelControlID="Button2">
    </AjaxToolkit:ModalPopupExtender>
    <asp:HiddenField ID="hdnselected" Value="0" runat="server" />
    <asp:Button Text="dummyButton" ID="btnDummy" Style="display: none;" OnClick="btnDummy_Click"
        runat="server" />
</asp:Content>
