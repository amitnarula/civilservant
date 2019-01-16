<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/sitemaster.master"
    CodeFile="changerequest.aspx.cs" Inherits="User_changerequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
<script type="text/javascript">
   



    function confirmmessage() {
        if (Page_ClientValidate('submitapplication')) {
            if (confirm("Are you sure to continue?")) {
                return true;
            }
            else {
                return false;
            }
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <asp:ValidationSummary ID="val123" runat="server" ShowMessageBox="true" ValidationGroup="submitapplication"
        ShowSummary="false" HeaderText="the form could't submitted due to:" />
    <div class="contentContainer" style="overflow: scroll;">
        <div id="managerContainerHeader">
            <span>Request for Change of Residence under Rule 13 of the Allotment Rules</span>
        </div>
        <br />
        <br />
        <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    <asp:Label ID="lblmessage" runat="server" Visible="false"></asp:Label></div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    Name :</div>
                <div>
                    <asp:TextBox ID="txtUsername" runat="server" ></asp:TextBox></div>
                <asp:RequiredFieldValidator ID="regname" ValidationGroup="submitapplication" runat="server"
                    ControlToValidate="txtUsername" ErrorMessage="please enter name." Display="None"></asp:RequiredFieldValidator>
            </div>
            <div class="rightMiddleColumn">
                <div>
                    Designation :</div>
                <div>
                    <asp:DropDownList ID="drpdesignations" DataTextField="Name" DataValueField="Id" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqdrpdesignations" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select designation." ControlToValidate="drpdesignations" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div style="font-weight:bold;">
                Particulars of present accommodation:
            </div>
        </div>
        <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    Quarter Number :</div>
                <div>
                    <asp:DropDownList 
                        ID="drpAllotedQuarter" DataTextField="QuarterNumber" DataValueField="QuarterNumber" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select alloted quarter." ControlToValidate="drpAllotedQuarter" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                
                    </div>
            </div>
            <div class="rightMiddleColumn">
                <div>
                    Quarter Type :</div>
                <div>
                    <asp:DropDownList 
                        ID="drpQuarterCategory" AutoPostBack="true" OnSelectedIndexChanged="drpQuarterCategory_SelectedIndexChanged" DataTextField="Name" DataValueField="Id" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select quarter category." ControlToValidate="drpQuarterCategory" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div style="font-weight:bold;">
                Preferences for change of accommodation:
            </div>
        </div>
         <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    First :</div>
                <div>
                <asp:DropDownList ID="drpFirstPerference" runat="server" DataValueField="QuarterNumber" DataTextField="QuarterNumber"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select Quarter Number." ControlToValidate="drpFirstPerference" ></asp:RequiredFieldValidator>
            
                </div>
            </div>
            <div class="rightMiddleColumn">
                <div>
                    Second :</div>
                <div>
                <asp:DropDownList ID="drpsecondPerference" runat="server" DataValueField="QuarterNumber" DataTextField="QuarterNumber"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select Quarter Number." ControlToValidate="drpsecondPerference" ></asp:RequiredFieldValidator>
            
                </div>
            </div>
        </div>
         <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    Third :</div>
                <div>
                <asp:DropDownList ID="drpThirdPerference" runat="server" DataValueField="QuarterNumber" DataTextField="QuarterNumber"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select Quarter Number." ControlToValidate="drpThirdPerference" ></asp:RequiredFieldValidator>
            
                </div>
            </div>
            <div class="rightMiddleColumn">
                
            </div>
        </div>
        <br />
        <div class="row">
            <div>
               <b>Undertaking</b>
            </div>
        </div>
        <div class="row">
            <div>
               I hereby declare that:
               <ul style="list-style-type:decimal">
               <li>I have not availed any change in the category of Quartr I am living in at persent under Rule 13 of the Allotment Rules</li>
               <li>This is my first application for change in this category</li>
               </ul>
            </div>
        </div>
        <div class="row">
            <div class="leftMiddleColumn">
                <div>
                    office :</div>
                <div>
                    <asp:DropDownList ID="drpOffice" DataTextField="Name" DataValueField="Id" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" InitialValue="-1" runat="server" ErrorMessage="Please select office." ControlToValidate="drpOffice" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                
                </div>
            </div>
        </div>

        <div class="row">
        </div>
        <div class="row">
            <div class="leftMiddleColumn">
                <center>
                    <div>
                     <asp:Button ID="btnAddUpdate" Text="Add / Update"
                                OnClientClick="return confirmmessage();" CausesValidation="true" ValidationGroup="submitapplication"
                                OnClick="btnadd_Click" runat="server"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                        ID="btnCancel" CausesValidation="false" OnClick="btnCancel_Click" Text="Cancel"
                                        runat="server"></asp:Button></div>
                </center>
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
