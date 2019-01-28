<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="processapplication.aspx.cs" Inherits="User_ProcessApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <script type="text/javascript">
        function reset() {
            var gridview = document.getElementById('ctl00_phEmsWebApp_grdQuarters');
            var inputs = gridview.getElementsByTagName('input');

            for (var i = 0; i < inputs.length; i++) {
                $(inputs[i]).attr('checked', false);
            }
        }

        function onRadioButtonClick(src, arg) {
            

            // Asp.net radio button gets wrapped inside span whenever any attributes are applied to it, 
            // hence 'src' is actually span and not a radio button itself. So we have to get radio button 
            // from its wrapper (span).
            var srcRdb = src.childNodes[0];

            var gridview = document.getElementById('ctl00_phEmsWebApp_grdQuarters');
            var inputs = gridview.getElementsByTagName('input');

            var inputElements = new Array();
            // Get radio buttons from same column
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].id.indexOf(arg) > -1)
                    inputElements.push(inputs[i]);
            }

            // Loop through radio buttons in Gridview
            for (var i = 0; i < inputElements.length; i++) {
                var ele = inputElements[i];
                // Check if input element is radio button
                if (ele.type == 'radio') {
                    // Uncheck radio buttons (except srcRdb) which are checked
                    if (ele != srcRdb && ele.checked) {
                        ele.checked = false;

                        // There will always be only one checked radio button (other than srcRdb).
                        // So stop looping as soon as that is unchecked.
                        break;
                    }
                }
            }
        }
    </script>
    <div class="contentContainer" style="width: 100%">
        <div id="managerContainerHeader">
            <h2>Estate Management & Allotment System</h2>
        </div>
        <br />
        <asp:Label ID="lblStatus" Text="" runat="server" />
        <br />
        <br />
        
        <table border="0" cellpadding="0" cellspacing="5" class="table" width="100%">
            <tr>
                <th align="left">
                    Name
                </th>
                <th align="left">
                    Designation
                </th>
                <th align="left">
                    Allotted Quarter
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Label Text="" ID="lblFullname" runat="server" />
                </td>
                <td>
                    <asp:Label Text="" ID="lblDesignation" runat="server" />
                </td>
                <td>
                    <asp:Label Text="" ID="lblAllottedQuarter" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Panel runat="server" ID="pnlChangeRequestInformation" Visible="false">
            <table border="0" cellpadding="0" cellspacing="0" class="table">
                <tr>
                <th align="left">
                    Change Request
                </th>
                <th align="left">
                    First Preference
                </th>
                <th align="left">
                    Second Preference
                </th>
                <th>Third Preference</th>
            </tr>
                
                <tr>
                    <td>
                        <asp:Label Text="N/A" runat="server" ID="lblRequestID" />
                    </td>
                    <td>
                        <asp:Label Text="N/A" runat="server"  ID="lblFirstPreference" /></td>
                    <td>
                        <asp:Label Text="N/A" ID="lblSecondPreference" runat="server" /></td>
                    <td>
                        <asp:Label Text="N/A" ID="lblThirdPreference" runat="server" /></td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel runat="server" ID="submissionClosedPanel" Visible="false">
            Submission to the selected category has been closed, Please contact your Administrator.
        </asp:Panel>
        <asp:Panel runat="server" ID="applyForQuarterPanel" Visible="false">
            <asp:Button CssClass="btn btn-success" Text="Apply for new allottment" runat="server" ID="btnNewAllottment"
                OnClick="btnApplyNew_Click" Enabled="false" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button CssClass="btn btn-success" Text="Apply for change request" runat="server" ID="btnChangeRequest"
                OnClick="btnChangeRequest_Click" Enabled="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="applyForQuarterMedicalGroundsPanel" Visible="false">
            <asp:Button CssClass="btn btn-success" Text="Apply for new allottment(Medical Ground)" runat="server" OnClick="btnApplyNew_Click"
                ID="btnNewAllottmentMedical" Enabled="false" />&nbsp;&nbsp;&nbsp;
            <asp:Button CssClass="btn btn-success" Text="Apply for change request(Medical Ground)" runat="server" OnClick="btnChangeRequest_Click"
                ID="btnChangeRequestMedical" Enabled="false" />
        </asp:Panel>
        <br />
        <asp:Panel runat="server" ID="pnlChangeRequest" CssClass="table-responsive" Visible="false" Width="100%">
            
    <div class="alert alert-info">
        <div>Please Note:</div> <div>1. Quarters marked in Green colour are Vacant as of
            now.</div> <div>2. Allotment for Change option is based on the criteria of “First
                Come First Serve” basis.</div>
    </div>
            <table border="0" cellpadding="5" cellspacing="5" width="100%" class="table" style="display:none">
                <tr>
                    <td>
                        <asp:RadioButton Text="List of Vacant Quarters" Checked="true" ID="radListVacantQuarters" GroupName="radUpper"
                            runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton Text="List of Occupied Quarters"  ID="radListOccupiedQuarters" GroupName="radUpper"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton Text="No change request" Checked="true" ID="radNoChangeRequest" GroupName="radLower"
                            runat="server" />
                    </td>
                    <td>
                        <asp:RadioButton Text="Change Request Already Received" ID="radChangeRequestReceived"
                            GroupName="radLower" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button Text="Search" ID="btnSearch" CssClass="btn btn-warning" OnClick="btnSearch_Click" runat="server" />
                    </td>
                </tr>
            </table>
            <div class="table-responsive">
            <asp:GridView runat="server" ID="grdQuarters" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" >
                <EmptyDataTemplate>
                    No records found for the search criteria!
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Quarter number
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%#Eval("Quarter.QuarterNumber") %>' ID="lblQuarterNumber" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Priority 1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:RadioButton ID="radPriority1" Text="" onchange="onRadioButtonClick(this,'radPriority1')" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Priority 2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:RadioButton ID="radPriority2" Text="" onchange="onRadioButtonClick(this,'radPriority2')" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Priority 3
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:RadioButton ID="radPriority3" Text="" onchange="onRadioButtonClick(this,'radPriority3')" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            No. of change requests already received
                        </HeaderTemplate>
                        <ItemTemplate>
                            
                            <asp:Literal ID="litChangeRequests" Text='<%#Eval("NumberOfChangeRequests") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
            <asp:Button Text="Save" OnClientClick="return confirm('No further modification will be allowed after submission.If you are ready to submit please click OK or if you want to modify please click CANCEL.')" CssClass="btn btn-success" ID="btnSave" OnClick="btnSave_Click" runat="server" />&nbsp;&nbsp;
            <input type="button" id="inpBtnReset" onclick="reset()" class="btn btn-warning" name="name" value="Reset" />&nbsp;&nbsp;
            <asp:Button Text="Cancel" CssClass="btn btn-danger" ID="btnCancel" OnClick="btnCancel_Click" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
