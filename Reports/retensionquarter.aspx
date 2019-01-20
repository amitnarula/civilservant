<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/estateblueprivate.master"
    CodeFile="retensionquarter.aspx.cs" Inherits="Reports_retensionquarter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>Retention Quarter Report</h2>
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
            <%--<div class="row">
                <div class="col-lg-12">
                    <div>
                        Office/Department:</div>
                    <div>
                        <asp:DropDownList CssClass="form-control" ID="drpOffice" DataTextField="Name" DataValueField="Id" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>--%>
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
