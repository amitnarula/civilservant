<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/estateblueprivate.master"
    CodeFile="ViewChangeRequest.aspx.cs" Inherits="Admin_ViewChangeRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>
                Change Request</h2>
        </div>
        <br />
        <div class="">
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-2 label-h fl">
                            Quarter Type:</div>
                        <div class="col-lg-8 fl">
                            <asp:DropDownList ID="drpQuarterCatergory" CssClass="form-control" DataTextField="Name" DataValueField="Id"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-2 fl">
                            <asp:Button ID="detai" CssClass="btn btn-warning" OnClick="detail_click" runat="server"
                                Text="Get Detail" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div>
                <div>
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
                        <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader"
                            HeaderText="Name" DataField="Name" />
                        <asp:HyperLinkField HeaderStyle-CssClass="gridHeader" HeaderText="Quarter Number"
                            DataTextField="QuarterNumber" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="Designation" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Office" DataField="Dept" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="First perference" DataField="FirstPerference" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Second perference"
                            DataField="SecondPerference" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Third perference" DataField="ThirdPerference" />
                        <%--
<asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Already Allotted" DataField="AlreadyAllottedQuarter" />
                        --%>
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Submission"
                            DataField="dateofsubmission" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField Visible="false" HeaderStyle-CssClass="gridHeader" HeaderText="Date Of Allotment"
                            DataField="DateOfAllotment" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                        
                        <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hidAllotmentStatus" Value='<%#Eval("AllotmentStatus") %>' />
                                <asp:HiddenField runat="server" ID="hidDateOfAllotment" Value='<%#Eval("DateOfAllotment") %>' />
                                <asp:HiddenField runat="server" ID="hidApplicationId" Value='<%#Eval("AppId") %>' />
                                <asp:LinkButton ID="lnkAllotte" Visible="false" runat="server" CommandName="Allotte" OnClientClick="return confirmmessagealert() ;"
                                    CommandArgument='<%#Eval("Id")%>' Text="Allotte"></asp:LinkButton>
                                <asp:LinkButton ID="lnkPos" runat="server" CommandName="Possesed" Visible="false"
                                    CommandArgument='<%#Eval("AppId") %>' OnClientClick="return confirmmessagealert() ;"
                                    Text="Possesed"></asp:LinkButton>
                                <asp:LinkButton ID="lnkWithdraw" runat="server" CommandName="Withdraw" Visible="false" Enabled="true"
                                    CommandArgument='<%#Eval("Id") %>' Text="Cancel" OnClientClick="return confirmmessagealert() ;"></asp:LinkButton>
                                <asp:Literal Text="N/A" Visible="false" ID="litNotAvailable" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:LinkButton ID="lnkVerify" runat="server" Text="Verify" CommandName="Allotte" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        --%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="addmembers"  runat="server" class="modal-dialog">
            <asp:ValidationSummary ID="valsummary" runat="server" ValidationGroup="member" ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:" />
            <div class="modal-content">
            <div class="modal-header">Date of Possession</div>
            <div class="modal-body">
            <table width="100%" class="table">
                
                <tr>
                    <td class="simple_text">Name:</td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="simple_text">Quarter Number:</td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="txtQuarternumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="simple_text">Date of Possession:</td>
                    <td>
                        <asp:TextBox CssClass="form-control" ID="txtDateOfPossession" runat="server"></asp:TextBox>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfPossession" />
                        <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please select date of possession." ControlToValidate="txtDateOfPossession" ValidationGroup="member" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                
            </table>
            </div>
            <div class="modal-footer">
                <table border="0" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                        <asp:Button ID="btnsaveMember" CssClass="btn btn-success" OnClick="btnsaveMember_click" ValidationGroup="member" runat="server" Text="Save" />&nbsp;&nbsp;&nbsp;<asp:Button CausesValidation="false" ID="btnCancemember" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                        </td>
                    </tr>
                </table>
            </div>
            </div>
            
        </div>

        <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true">
        </AjaxToolkit:ToolkitScriptManager>
        <AjaxToolkit:ModalPopupExtender ID="pop" BackgroundCssClass="overlay" runat="server" PopupControlID="addmembers" TargetControlID="lnkAddMember" CancelControlID="btnCancemember"></AjaxToolkit:ModalPopupExtender>
        <asp:LinkButton ID="lnkAddMember" runat="server" Text="Add" Style="display: none;"></asp:LinkButton>
        <asp:HiddenField ID="hdnSelected" Value="0" runat="server" />
    <asp:HiddenField  ID="hdnChangeRequestId" runat="server" />
</asp:Content>
