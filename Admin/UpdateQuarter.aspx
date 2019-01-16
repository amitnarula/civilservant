<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/estateblueprivate.master"
    CodeFile="UpdateQuarter.aspx.cs" Inherits="Admin_UpdateQuarter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
<AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>Update Quarter Status</h2>
        </div>
        <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    <asp:Label ID="lblmessage" runat="server"></asp:Label></div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
            <span>Search Quarter:</span>
            <table border="0" cellpadding="0" cellspacing="0" width="50%">
                <tr>
                    <td>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtSearch" Width="300" />
                    </td>
                    <td>
                        <asp:Button CssClass="btn btn-warning" Text="Search" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            </div>
        </div>
        <br />
        <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView CssClass="table table-bordered table-hover" runat="server" ID="grdQuarters"
                    AutoGenerateColumns="false" OnRowCommand="grdQuarters_RowCommand" OnRowDataBound="grdQuarters_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Quarter No.
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("QuarterNumber") %>' ID="lblQuarterNumber" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Status
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButton Text="CPWD" CssClass="dummyCheck" GroupName="grpStatus" ID="radBtnCPWD" runat="server" />
                                <asp:RadioButton Text="Damaged" CssClass="dummyCheck" GroupName="grpStatus" ID="radBtnDamaged" runat="server" />
                                <asp:RadioButton Text="Vacant" CssClass="dummyCheck" GroupName="grpStatus" ID="radBtnVacant" runat="server" />
                                <asp:RadioButton Text="Surrender" CssClass="dummyCheck" GroupName="grpStatus" ID="radBtnSurrender" runat="server" />
                                <asp:HiddenField runat="server" ID="hdnStatus" Value='<%#Eval("Status") %>' />
                                <asp:HiddenField runat="server" ID="hdnDateOfVacation" Value='<%#Eval("DateOfVacation") %>' />
                                
                                <br /><asp:TextBox runat="server" ID="txtDateOfVacation" CssClass="dateOfVacation"  placeholder="Date of vacation" />
                                
                                <AjaxToolkit:CalendarExtender ID="calExtTxtDateOfVacation"
                                Format="dd/MM/yyyy"  runat="server"
                        TargetControlID="txtDateOfVacation" ></AjaxToolkit:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Remarks
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox CssClass="form-control" runat="server" ID="txtRemarks" MaxLength="50"
                                    Text='<%#Eval("Remarks") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Action
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button Text="Update" CssClass="btn btn-primary" ID="btnUpdateStatus" CommandName="UpdateStatus"
                                    CommandArgument='<%#Eval("Id") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No data to display
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".dummyCheck").click(function () {
                var txtDateOfVacation = $(this).parent().find('input.dateOfVacation');
                if ($($(this).children("input")[0]).attr("id").indexOf("radBtnVacant") > 0) { //Vacant clicked
                    $(txtDateOfVacation).css({ 'display': 'inline' })
                }
                else {
                    $(txtDateOfVacation).css({ 'display': 'none' });
                }
            });
        });
    </script>
</asp:Content>
