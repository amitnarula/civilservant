<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="circulars.aspx.cs" Inherits="User_circulars" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>Approved priority list</h2>
        </div>
        <div class="">
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        Quarter Type:</div>
                    <div>
                        <asp:DropDownList CssClass="form-control" ID="drpQuarterCatergory" DataTextField="Name" DataValueField="Id"
                            runat="server">
                        </asp:DropDownList>
                        <br />
                        <asp:Button ID="detai" OnClick="detail_click" CssClass="btn btn-warning" runat="server" Text="Get Detail" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label></div>
                </div>
            </div>
            <br />
            <div class="table-responsive">
                <asp:GridView ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" Width="100%" runat="server">
                    <EmptyDataTemplate>
                        No Records!
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:HyperLinkField HeaderStyle-CssClass="gridHeader" HeaderText="AAN" DataNavigateUrlFields="AAN"
                            DataNavigateUrlFormatString="~/admin/userinformation.aspx?id={0}&returnurl=admin/Prioritizeapplications.aspx"
                            DataTextField="AAN" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Name" DataField="UserName" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="Designation" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Office" DataField="Dept" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Date of joining" DataField="dateOfJoining" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Category" DataField="Cast" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Grade pay" DataField="GradePay" />
                        <asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Already Allotted" DataField="AlreadyAllottedQuarter" />
                        <%--<asp:TemplateField HeaderStyle-CssClass="gridHeader" HeaderText="Action" >
<ItemTemplate>
<asp:LinkButton ID="lnkAllotte" runat="server" CommandName="Allotte" CommandArgument='<%#Eval("Id") %>' Text="Allotte" ></asp:LinkButton>
<asp:LinkButton ID="lnkPos" runat="server" CommandName="Possesed" Enabled=false CommandArgument='<%#Eval("Id") %>' Text="Possesed" ></asp:LinkButton>
<asp:LinkButton ID="lnkWithdraw" runat="server" CommandName="Withdraw" Enabled=false CommandArgument='<%#Eval("Id") %>' Text="Withdraw" ></asp:LinkButton>

</ItemTemplate>
</asp:TemplateField>
                        --%>
                        <%--<asp:LinkButton ID="lnkVerify" runat="server" Text="Verify" CommandName="Allotte" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        --%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
