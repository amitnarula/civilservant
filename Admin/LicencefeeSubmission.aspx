<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true" CodeFile="LicencefeeSubmission.aspx.cs" Inherits="Admin_LicencefeeSubmission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<AjaxToolkit:ToolkitScriptManager  ID="scriptManager12"  runat="server"  EnableScriptLocalization="true" EnableScriptGlobalization="true">
 </AjaxToolkit:ToolkitScriptManager>
<div class="contentContainer">
<div id="managerContainerHeader">
<h2 >Licence fee submission</h2> 
</div>
<br />
<asp:ValidationSummary ID="valsummary" runat="server" ValidationGroup="licenfee" ShowMessageBox="true" ShowSummary="false" HeaderText="The form is not submitted due to:"/> 

<div class="row">
    <div class="col-lg-12">
            <div><asp:Label ID="lblmessage" ReadOnly="true" Font-Bold=true runat="server" ></asp:Label></div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <table border="0" class="table" cellpadding="0" cellspacing="0">
                <tr>
                    <td>Quarter Number:
                    </td>
                    <td><asp:TextBox CssClass="form-control"  AutoPostBack="true" OnTextChanged="QuarterNumber_change" ID="txtQuarterNumber" runat="server" ></asp:TextBox></div>
           <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter quarter number" ControlToValidate="txtQuarterNumber" Display="None" ValidationGroup="licenfee"  ></asp:RequiredFieldValidator>
        </td>
                </tr>
                <tr>
                    <td>AAN:
                    </td>
                    <td><asp:TextBox CssClass="form-control"  ID="txtAllotteeAAN" ReadOnly="true" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Name:
                    </td>
                    <td><asp:TextBox CssClass="form-control"  ID="txtUserName" ReadOnly="true" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Quarter Type:
                    </td>
                    <td><asp:DropDownList CssClass="form-control" ID="drpQuarterCatergory" DataTextField="Name"  DataValueField="Id"  runat="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Designation :
                    </td>
                    <td><asp:DropDownList ID="drpDesignation" CssClass="form-control" DataTextField="Name"  DataValueField="Id"  runat="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Office/Department:</td>
                    <td><asp:DropDownList ID="drpOffice" CssClass="form-control" DataTextField="Name"  DataValueField="Id"  runat="server" ></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Month:</td>
                    <td><asp:TextBox CssClass="form-control" ID="txtMonth"  runat="server" ></asp:TextBox>
            <asp:CompareValidator ID="cmpmonth" runat="server" ControlToValidate="txtMonth" ErrorMessage="please enter valid date" Display="None" ValidationGroup="licenfee"  Operator=DataTypeCheck Type=Date></asp:CompareValidator>
            <asp:RequiredFieldValidator ValidationGroup="licenfee" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMonth" ErrorMessage="Please enter Date!"></asp:RequiredFieldValidator>
            <ajaxtoolkit:calendarextender Format="dd/MM/yyyy" id="Calendarextender1" runat="server" targetcontrolid="txtMonth" /></td>
            
                </tr>
                <tr>
                    <td>Licence fee:</td>
                    <td><asp:TextBox CssClass="form-control" ID="txtLicenceFee"  runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ValidationGroup="licenfee" ID="reqLicenceFee" runat="server" ControlToValidate="txtLicenceFee" ErrorMessage="Please enter Licence fee!"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cmpLicencefee" runat="server" Operator="DataTypeCheck" ControlToValidate="txtLicenceFee" Display="None" ErrorMessage="Please enter valid licence fee" Type=Double ValidationGroup="licenfee"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td>Remarks:</td>
                    <td><asp:TextBox CssClass="form-control" ID="txtRemarks"  runat="server" TextMode="MultiLine" Rows="5" Columns="43" ></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat=server ControlToValidate="txtRemarks" ErrorMessage="Remarks should be less than 500 charcters!" Display="None" ValidationGroup="licenfee"  Operator="GreaterThan" ValueToCompare="500"></asp:CompareValidator>
        </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Add / Update" OnClick="btnadd_Click" runat="server"
                        ValidationGroup="licenfee"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel"
                            OnClick="btnCancel_Click" CssClass="btn btn-danger" CausesValidation="false" Text="Cancel" runat="server">
                        </asp:Button></td>
                </tr>
            </table>
        </div>
    </div>
    
    <div class="row">
        <div class="col-lg-12">
            <center>
                <div class="table-responsive">
                    <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" runat="server">
                        <EmptyDataTemplate>
                            No Records!
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField ItemStyle-Width="100px" ItemStyle-Wrap="true" HeaderText="Month"
                                DataField="Office" />
                            <asp:BoundField HeaderText="Amount received" DataField="AAN" />
                            <asp:BoundField HeaderText="Balance" DataField="QuarterNumber" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="vacant" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Vacant"
                                        CommandName="vacant"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </center>
        </div>
    </div>
    <div class="row">
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
           
                <div>
                    </div>
            
        </div>
        
    </div>
    </div>
</asp:Content>
