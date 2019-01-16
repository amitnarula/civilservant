<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="LicenceFeelisting.aspx.cs" Inherits="Admin_LicenceFeelisting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>Licence fee submission</h2>
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
            <table border="0" class="table" cellpadding="0" cellspacing="0">
                <colgroup>
                    <col width="20%" />
                    <col width="60%" />
                    <col width="20%" />

                </colgroup>
                <tr>
                    <td>Quarter Number:
                    </td>
                    <td><asp:TextBox AutoPostBack="true" CssClass="form-control" ID="txtQuarterNumber" runat="server"></asp:TextBox>&nbsp;</td>
                        <td><asp:Button CssClass="btn btn-primary" ID="btnAddUpdate" Text="Get Deatil" OnClick="btnadd_Click"
                            runat="server" ValidationGroup="licenfee"></asp:Button>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter quarter number"
                        ControlToValidate="txtQuarterNumber" Display="None" ValidationGroup="licenfee"></asp:RequiredFieldValidator></td>
                </tr>
            </table>

        </div>
        <asp:Panel ID="pnl" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Name of Allottee:</div>
                    <div>
                        <asp:TextBox ID="txtAllotteName" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        AAN:</div>
                    <div>
                        <asp:TextBox ID="txtAllotteeAAN" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Quarter Type:</div>
                    <div>
                        <asp:DropDownList ID="drpQuarterCatergory" CssClass="form-control" DataTextField="Name" DataValueField="Id"
                            runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Designation :</div>
                    <div>
                        <asp:DropDownList ID="drpDesignation" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Office/Department:</div>
                    <div>
                        <asp:DropDownList ID="drpOffice" DataTextField="Name" CssClass="form-control" DataValueField="Id" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="rightMiddleColumn">
                </div>
            </div>
             <div class="row">
                <div class="col-lg-12">
                    <div>
                        Date of Possession:</div>
                    <div>
                        <asp:TextBox ID="txtDateOfPossession" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Retention Period (in Month):</div>
                    <div>
                        <asp:TextBox ID="txtRetentionPeriod" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Due Date of Vacation
                    </div>
                    <div>
                        <asp:TextBox ID="txtDueDateOfVacation" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Actual Date of Vacation
                    </div>
                    <div>
                        <asp:TextBox ID="txtActualDateOfVacation" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Damage Charges
                    </div>
                    <div>
                        <asp:TextBox ID="txtDamageCharges" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Changed to Quarter No. :</div>
                    <div>
                        <asp:TextBox ID="txtChangedQuarter" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Date of Change :</div>
                    <div>
                        <asp:TextBox ID="TextBox6" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox></div>
                </div>
            </div>
           
            <br />
            <br>
            <div class="row">
                <div class="col-lg-12">
                    
                        <div class="table-responsive">
                            <asp:GridView ID="grdLicenceFee" CssClass="table table-bordered table-hover" Width="500px" AutoGenerateColumns="false" runat="server">
                                <EmptyDataTemplate>
                                    No Records!
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="100px" ItemStyle-Wrap="true" HeaderText="Month"
                                        DataField="Month" DataFormatString="{0:MMM, yyyy}" HtmlEncode="false" />
                                    <asp:BoundField HeaderText="Assessment"  DataField="ActualFee"/>
                                    <asp:BoundField HeaderText="Amount received" DataField="Fee" />
                                    <asp:BoundField HeaderText="Month of adjustment" />
                                    <asp:BoundField HeaderText="Balance" DataField="Balance" />
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                   
                </div>
            </div>
            <div class="row">
            </div>
        </asp:Panel>
    </div>
</asp:Content>
