<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/estateblueprivate.master"
    CodeFile="pendinglicensefee.aspx.cs" Inherits="Reports_pendinglicensefee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
            EnableScriptGlobalization="true">
        </AjaxToolkit:ToolkitScriptManager>
        <asp:ValidationSummary ID="val123" runat="server" ShowMessageBox="true" ValidationGroup="submitapplication"
            ShowSummary="false" HeaderText="the form could't submitted due to:" />
        <div id="managerContainerHeader">
            <h2>Quarter Report</h2>
        </div>
        <div class="">
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Quarter Category :</div>
                    <div>
                        <asp:DropDownList CssClass="form-control" ID="drpQuarterCat" DataTextField="Name" DataValueField="Id" runat="server">
                        </asp:DropDownList>
                        &nbsp; &nbsp;
                    </div>
                </div>
                
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Office/Department:</div>
                    <div>
                        <asp:DropDownList CssClass="form-control" ID="drpOffice" DataTextField="Name" DataValueField="Id" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Start date:</div>
                    <div>
                        <asp:TextBox CssClass="form-control" ID="txtstartdate" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid start Date"
                            Operator="DataTypeCheck" ValidationGroup="submitapplication" ControlToValidate="txtstartdate"
                            Type="Date" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="submitapplication"
                            runat="server" ControlToValidate="txtstartdate" ErrorMessage="please enter start Date."
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server"
                            TargetControlID="txtstartdate" />
                    </div>
                </div>
                
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        End date:</div>
                    <div>
                        <asp:TextBox CssClass="form-control" ID="txtenddate" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter valid end Date"
                            Operator="DataTypeCheck" ValidationGroup="submitapplication" ControlToValidate="txtstartdate"
                            Type="Date" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="submitapplication"
                            runat="server" ControlToValidate="txtenddate" ErrorMessage="please enter end Date."
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server"
                            TargetControlID="txtenddate" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                        <div>
                            <asp:Button CssClass="btn btn-primary" ID="btnGenerateReport" Text="Generate report" runat="server" OnClick="btnGenerateReport_click" />
                        </div>
                    
                </div>
                
            </div>
        </div>
    </div>
</asp:Content>
