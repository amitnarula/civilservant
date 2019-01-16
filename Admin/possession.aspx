<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true" Inherits="Admin_possession" CodeFile="~/Admin/possession.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>Quarter Possession</h2>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                
                    <table border="0" cellpadding="0" class="table" cellspacing="0">
                        <tr>
                            <td>Quarter Type:
                            </td>
                            <td>
                            <asp:DropDownList CssClass="form-control" ID="drpQuarterCatergory" DataTextField="Name" DataValueField="Id" runat="server"></asp:DropDownList>
                        
                    </td>
                            <td><asp:Button CssClass="btn btn-warning" ID="detai" OnClick="detail_click" runat="server" Text="Get Detail" /></td>
                        </tr>
                    </table>
                    
                    
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    
                        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>

            </div>
            <br />
            <div class="">
                <div class="table-responsive">
                    <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" runat="server" Width="100%" OnRowDataBound="grdQuarters_RowDataBound" OnRowCommand="gridCommand">
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
                            <asp:HyperLinkField HeaderStyle-CssClass="gridHeader" HeaderText="AAN" DataNavigateUrlFields="AAN" DataNavigateUrlFormatString="~/admin/userinformation.aspx?id={0}&returnurl=admin/possession.aspx" DataTextField="AAN" />
                            <asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader" HeaderText="Name" DataField="UserName" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Dept" DataField="Dept" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Quarter Number" DataField="QuarterNumber" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Type" DataField="QuarterType" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="Designation" />
                            <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Already Allotted Quarter" DataField="AlreadyAllottedQuarter" />
                            <asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lnkPos" runat="server" CommandName="Possesed" OnClientClick="return confirmmessagealert() ;" CommandArgument='<%#Eval("Id") %>' Text="Possesed"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkWithdraw" runat="server" CommandName="Withdraw" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirmmessagealert() ;" Text="Withdraw"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkCancel" runat="server" CommandName="CancelApplication" Visible="false" CommandArgument='<%#Eval("Id") %>' OnClientClick="return confirmmessagealert() ;" Text="Cancel"></asp:LinkButton>
                                    <asp:HiddenField  runat="server" ID="hdnDateOfAllotement" Value='<%#Eval("DateOfAllottment") %>' />
                                    
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
                        <asp:Button ID="btnsaveMember" CssClass="btn btn-success" ValidationGroup="member" runat="server" Text="Save" OnClick="btnsaveMember_click" />&nbsp;&nbsp;&nbsp;<asp:Button CausesValidation="false" ID="btnCancemember" runat="server" Text="Cancel" CssClass="btn btn-danger" />
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
</asp:Content>

