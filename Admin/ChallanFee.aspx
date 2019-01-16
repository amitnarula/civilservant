<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="ChallanFee.aspx.cs" Inherits="Admin_ChallanFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>
                Challan Information</h2>
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
                <table border="0" class="table" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>Quarter Number:
                        </td>
                        <td>
                        <asp:TextBox CssClass="form-control" AutoPostBack="true" ID="txtQuarterNumber" runat="server" OnTextChanged="QuarterNumber_change"></asp:TextBox></div>
                <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter quarter number"
                    ControlToValidate="txtQuarterNumber" Display="None" ValidationGroup="licenfee"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Allottee AAN:
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtAllotteeAAN" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Challan Number:
                        </td>
                        <td>
                        <asp:TextBox CssClass="form-control" ID="txtChallanNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Bank Name:
                        </td>
                        <td>
                        <asp:TextBox CssClass="form-control" ID="txtBankName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Date Received:
                        </td>
                        <td> <asp:TextBox CssClass="form-control" ID="txtMonth" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cmpmonth" runat="server" ControlToValidate="txtMonth" ErrorMessage="please enter valid date"
                        Display="None" ValidationGroup="licenfee" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ValidationGroup="licenfee" ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtMonth" ErrorMessage="Please enter Date!"></asp:RequiredFieldValidator>
                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="Calendarextender1" runat="server"
                        TargetControlID="txtMonth" />
                        </td>
                    </tr>
                    <tr>
                        <td>Payment (Rs.)
                        </td>
                        <td><asp:TextBox CssClass="form-control" ID="txtPayment" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Remarks:
                        </td>
                        <td>
                        <asp:TextBox CssClass="form-control" ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="43"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtRemarks"
                        ErrorMessage="Remarks should be less than 500 charcters!" Display="None" ValidationGroup="licenfee"
                        Operator="GreaterThan" ValueToCompare="500"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td> <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" OnClick="btnadd_Click" Text="Add / Update" runat="server"
                            ValidationGroup="licenfee"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" ID="btnCancel"
                                CausesValidation="false" OnClick="btnCancel_Click" Text="Cancel" runat="server">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
        
</asp:Content>
