<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    Inherits="Admin_Allottee" CodeFile="~/Admin/Allottee.aspx.cs" %>

<%@ MasterType VirtualPath="~/sitemaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <div class="contentContainer">
        <div id="managerContainerHeader">
            <h2>Allotement</h2>
        </div>
        <br />
        <asp:ValidationSummary ID="valsummary" runat="server" ShowMessageBox="true" ShowSummary="false"
            HeaderText="The form is not submitted due to:" />
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <asp:Label ID="lblmessage" ReadOnly="true" runat="server"></asp:Label></div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    AAN:</div>
                <div>
                    <asp:TextBox ID="txtAllotteeName" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox></div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Quarter Number:</div>
                <div>
                    <asp:DropDownList ID="drpQuarter" CssClass="form-control" runat="server" DataValueField="QuarterNumber" DataTextField="QuarterNumber">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="reqdrpdesignations" Display="None" InitialValue="-1"
                    runat="server" ErrorMessage="Please select Quarter Number." ControlToValidate="drpQuarter"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    Quarter Type:</div>
                <div>
                    <%--<asp:DropDownList ID="drpQuarterCatergory" DataTextField="Name"  
                    DataValueField="Id"  runat="server" AutoPostBack="True" 
                    onselectedindexchanged="drpQuarterCatergory_SelectedIndexChanged" ></asp:DropDownList>--%>
                    <asp:DropDownList CssClass="form-control" ID="drpQuarterCatergory" DataTextField="Name" DataValueField="Id"
                        runat="server" AutoPostBack="True">
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
                    <asp:DropDownList ID="drpOffice" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="rightMiddleColumn">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    D.O.J :</div>
                <div>
                    <asp:TextBox ID="txtDoj" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox><%--<ajaxtoolkit:calendarextender id="Calendarextender1" runat="server" targetcontrolid="txtDoj" />--%></div>
            </div>
            <div class="rightMiddleColumn">
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div>
                    D.O.R (retirement):</div>
                <div>
                    <asp:TextBox ID="txtDor" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox><%--<ajaxtoolkit:calendarextender id="txtdateofretirementcalendarextender" runat="server" targetcontrolid="txtDor" />--%></div>
            </div>
            <div class="rightMiddleColumn">
            </div>
        </div>
        <div class="row">
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                
                    <div>
                        
                        <asp:Button ID="btnAddUpdate" CssClass="btn btn-primary" Text="Add / Update" OnClick="btnadd_Click" runat="server">
                        </asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click"
                            CausesValidation="false" Text="Cancel" runat="server"></asp:Button></div>
                
            </div>
            <div class="rightMiddleColumn">
                <center>
                    <div>
                    </div>
                </center>
            </div>
        </div>
    </div>
</asp:Content>
