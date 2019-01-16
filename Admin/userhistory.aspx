<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="userhistory.aspx.cs" Inherits="Admin_userhistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>User History</h2>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <table border="0" cellpadding="0" class="table" cellspacing="0">
                    <tr>
                        <td>Username:
                        </td>
                        <td><asp:TextBox ID="txtUser" CssClass="form-control" runat="server"></asp:TextBox></td>
                        <td><asp:Button CssClass="btn btn-primary" Text="Get History" runat="server" OnClick="btnHistory_click" ID="btnHistory" /></td>
                    </tr>
                </table>

                
            </div>
        </div>
        <div class="row">
        <div class="table-responsive">
            <asp:GridView ID="grdUsers" Width="100%" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" runat="server">
                <EmptyDataTemplate>
                    No Records!
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField ItemStyle-CssClass="uppercase" HeaderText="Action" DataField="Action" />
                    <asp:BoundField HeaderText="Description" DataField="description" />
                    <asp:BoundField HeaderText="Time" DataField="time" />
                </Columns>
            </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
