<%@ Page Title="" Language="C#" MasterPageFile="~/EstateBluePrivate.master" AutoEventWireup="true" CodeFile="ManageGuestHouses.aspx.cs" Inherits="Admin_ManageGuestHouses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
 <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <h2>Guest House Request <asp:Label Text="Pending" ID="lblLegend" runat="server" /></h2>
    <br />
    <div class="table-responsive">
        <asp:GridView runat="server" OnRowCommand="grdGuestHouseRequests_RowCommand" CssClass="table table-bordered table-hover"
            ID="grdGuestHouseRequests" AutoGenerateColumns="false">
            <EmptyDataTemplate>
                No data to display!
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        ID
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblID" Text='<%#Eval("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Name
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" CssClass="uppercase" ID="lblName" Text='<%#Eval("Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Email
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("EmailID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Guest House
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblGuestHouse" Text='<%#Eval("GuestHouse") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        From
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("DateFrom","{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        To
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("DateTo","{0:dd/MM/yyyy}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Purpose
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPurposeOfVisit" Text='<%#Eval("PurposeOfVisit") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Place
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPlaceOfHosting" Text='<%#Eval("PlaceOfHosting") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Designation
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDesignation" Text='<%#Eval("Designation") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField>
                    <HeaderTemplate>
                        Room
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblRoomName" Text='<%#Eval("RoomName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Due Balance
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblBalanceDue" Text='<%#Eval("BalanceDue") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton Text="Book" runat="server" ID="lnkAllot" CommandName="allotRequest"
                            CommandArgument='<%#Eval("ID") %>' />&nbsp;
                        <asp:LinkButton Text="Reject" ID="lnkDelete" CommandName="deleteRequest" CommandArgument='<%#Eval("ID") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <HeaderTemplate>
                        Action
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton Text="Mark as paid" Visible="true" ID="lnkMarkAsPaid" CommandName="markPaid"
                            CommandArgument='<%#Eval("ID") %>' runat="server" />
                        <asp:LinkButton Text="Cancel" Visible="true" ID="lnkCancelBooking" CommandName="cancelBooking"
                            CommandArgument='<%#Eval("ID") %>' runat="server" />

                        <asp:Label Text='<%#Eval("IsPaid") %>' Visible="false" ID="lblIsPaid" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
     <AjaxToolkit:ModalPopupExtender ID="popSelectRoom" BackgroundCssClass="overlay" runat="server"
        PopupControlID="divSelectRoom" TargetControlID="lnkDummy" >
    </AjaxToolkit:ModalPopupExtender>
    <asp:LinkButton ID="lnkDummy" runat="server" Text="Add" Style="display: none;"></asp:LinkButton>
    <div id="divSelectRoom" runat="server" class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                Select Room</div>
            <div class="modal-body">
                <table width="100%" class="table">
                    <tr>
                        <td>Room:</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlRoom" CssClass="form-control">
                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modal-footer">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                        <asp:HiddenField runat="server" ID="hdnRequestID" />
                            <asp:Button ID="btnSave" CssClass="btn btn-success"
                                runat="server" Text="Save"  />&nbsp;&nbsp;&nbsp;<asp:Button
                                    CssClass="btn btn-danger" CausesValidation="false" ID="btnCancel" runat="server"
                                    Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

