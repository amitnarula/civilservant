<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="QuarterDamage.aspx.cs" Inherits="Admin_QuarterDamage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>Quarter Damage Charges</h2>
        </div>
        <asp:ValidationSummary ID="valsummary" runat="server" ValidationGroup="licenfee"
            ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:" />
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <asp:Label ID="lblmessage" ReadOnly="true" Font-Bold="true" runat="server"></asp:Label></div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <table border="0" cellpadding="0" class="table" cellspacing="0">
                    <tr>
                        <td>Quarter Number:
                        </td>
                        <td>
                            <asp:TextBox CssClass="form-control" AutoPostBack="true" OnTextChanged="QuarterNumber_change" ID="txtQuarterNumber"
                        runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter quarter number"
                    ControlToValidate="txtQuarterNumber" Display="None" ValidationGroup="licenfee"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Allottee AAN:
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtAllotteeAAN" ReadOnly="true" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Assessment Date:
                        </td>
                        <td>
                        <asp:TextBox CssClass="form-control" ID="txtMonth" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cmpmonth" runat="server" ControlToValidate="txtMonth" ErrorMessage="please enter valid date"
                        Display="None" ValidationGroup="licenfee" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ValidationGroup="licenfee" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtMonth" ErrorMessage="Please enter Date!"></asp:RequiredFieldValidator>
                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="Calendarextender1" runat="server"
                        TargetControlID="txtMonth" /></td>
                    </tr>
                    <tr>
                        <td>Damage Charges:
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtDamageCharge" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtDamageCharge"
                        ErrorMessage="please valid damage charge" Display="None" ValidationGroup="licenfee"
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ValidationGroup="licenfee" ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="txtDamageCharge" ErrorMessage="Please enter damage charge!"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td>Remarks:
                        </td>
                        <td> <asp:TextBox CssClass="form-control" ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="43"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtRemarks"
                        ErrorMessage="Remarks should be less than 500 charcters!" Display="None" ValidationGroup="licenfee"
                        Operator="GreaterThan" ValueToCompare="500"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button  CssClass="btn btn-primary" ID="btnAddUpdate" Text="Add / Update" OnClick="btnadd_Click" runat="server"
                            ValidationGroup="licenfee"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" ID="btnCancel"
                                OnClick="btnCancel_Click" CausesValidation="false" Text="Cancel" runat="server">
                            </asp:Button></td>
                    </tr>
                </table>
            </div>
        </div>
        
</asp:Content>
