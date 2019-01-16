<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="Category.aspx.cs" Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server"> 
<h2>Submission</h2>
<br />
    <asp:Label Text="" ID="lblMessage" Visible="false" runat="server" />
    <div class="table-responsive">
        <table class="table table-bordered table-hover" border="0" cellpadding="10" cellspacing="10"
            width="60%">
            <thead>
                <tr>
                    <th>
                        Category
                    </th>
                    <th>
                        Submission Allowed
                    </th>
                </tr>
            </thead>
            <tr>
                <td>
                    Type I
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Type II
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType2" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Type III
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType3" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Type IV
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType4" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Type V
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType5" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Type VI
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType6" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Type-IV (Reserved for IA&amp;AS)
                </td>
                <td>
                    <asp:CheckBox Text="" ID="chkType7" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />

    <asp:Button Text="Save" ID="btnSave" runat="server" 
    onclick="btnSave_Click" CssClass="btn btn-success" />&nbsp;
    <asp:Button Text="Reset" ID="btnReset" CssClass="btn btn-warning" OnClick="btnReset_Click" runat="server" />
    <asp:Button Text="Cancel" ID="btnCancel" CssClass="btn btn-danger" runat="server" 
    onclick="btnCancel_Click" />


</asp:Content>
