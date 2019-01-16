<%@ page title="" language="C#" masterpagefile="~/sitemaster.master" autoeventwireup="true" inherits="Admin_verificattion" CodeFile="~/Admin/verification.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="contentContainer" style="width:100%">
<div id="managerContainerHeader">
<SPAN >Application Verification</SPAN> 
</div>

<div class=""><br /><br />
<br />
   <div class="row">
    <div class="leftMiddleColumn">
            <div><asp:Label ID="lblMessage" runat="server" Visible=false></asp:Label></div>
           
        </div>
    </div> <br />
<br /><br />
<div class="searchResults">
<asp:GridView ID="grdQuarters"  AutoGenerateColumns="false" Width="100%" runat="server" OnRowCommand="GridRowCommand">
<EmptyDataTemplate>
No Records!
</EmptyDataTemplate>
<Columns>
<%--START : For displaying serial number--%>
<asp:TemplateField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader" >
<HeaderTemplate>Sr. No.</HeaderTemplate>
<ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
</asp:TemplateField>
<%--END : For displaying serial number--%>
<asp:HyperLinkField HeaderStyle-CssClass="gridHeader" HeaderText="AAN" DataNavigateUrlFields="AAN" DataNavigateUrlFormatString="~/admin/userinformation.aspx?id={0}&returnurl=admin/verification.aspx" DataTextField="AAN" />
<asp:BoundField ItemStyle-CssClass="uppercase" HeaderStyle-CssClass="gridHeader" HeaderText="Name" DataField="UserName" />
<asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Designation" DataField="Designation" />
<asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Dept" DataField="Dept" />
<asp:BoundField HeaderStyle-CssClass="gridHeader" HeaderText="Type" DataField="QuarterType" />
<asp:TemplateField HeaderText="Action">
<ItemTemplate>
<asp:LinkButton ID="lnkVerify" OnClientClick="return confirmmessagealert() ;" runat="server" Text="Verify" CommandName="Verify" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
<asp:LinkButton ID="lnkView" runat="server" Text="View" Visible="false" OnClientClick="return confirmmessagealert() ;" CommandName="view" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
<asp:LinkButton ID="LinkButton1" runat="server" Text="Reject" CommandName="reject" OnClientClick="return confirmmessagealert() ;" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>

</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
</div>
</div>
</asp:Content>

