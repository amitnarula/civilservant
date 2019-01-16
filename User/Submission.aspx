<%@ Page Title="" Language="C#" MasterPageFile="~/estateblueprivate.master" AutoEventWireup="true"
    CodeFile="Submission.aspx.cs" Inherits="User_Submission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" runat="Server">
    <script type="text/javascript">
        //    function showAudit(value) {
        //    if(value==1)
        //        document.getElementById('auditpool').style.display = "block";
        //    else
        //        document.getElementById('auditpool').style.display = "none";
        //} 
        function showsublet(value) {
            if (value == 1)
                document.getElementById('sublet').style.display = "block";
            else
                document.getElementById('sublet').style.display = "none";
        }
        function showbarredDate(value) {
            if (value == 1)
                document.getElementById('barredDate').style.display = "block";
            else
                document.getElementById('barredDate').style.display = "none";
        }



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
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager12" runat="server" EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </AjaxToolkit:ToolkitScriptManager>
    <asp:ValidationSummary ID="val123" runat="server" ShowMessageBox="true" ValidationGroup="submitapplication"
        ShowSummary="false" HeaderText="the form could't submitted due to:" />
    <div class="contentContainer" style="overflow: scroll;">
        <div id="managerContainerHeader">
            <h2>
                Submission</h2>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <div>
                    <asp:Label ID="lblmessage" runat="server" Visible="false"></asp:Label></div>
            </div>
        </div>
        <br />
        <div class="row" style="display: none;">
            <div class="col-lg-12">
                <div>
                    Quarter Type :</div>
                <div>
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="drpQuarterCategory_selectedindex"
                        ID="drpQuarterCategory" DataTextField="Name" DataValueField="Id" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please select quarter type"
                        ControlToValidate="drpQuarterCategory" Display="None" InitialValue="-1" ValidationGroup="submitapplicationx"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="rightMiddleColumn">
                <div>
                    AAN :</div>
                <div>
                    <asp:TextBox ID="txtAAN" runat="server" ReadOnly="true"></asp:TextBox></div>
            </div>
        </div>
        <div class="table-responsive">
            <table border="0" cellpadding="0" cellspacing="0" class="table">
                <colgroup>
                    <col width="20%" />
                    <col width="28%" />
                    <col width="4%" />
                    <col width="20%" />
                    <col width="28%" />
                </colgroup>
                <tr>
                    <td>
                        Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="regname" ValidationGroup="submitapplication" runat="server"
                            ControlToValidate="txtUsername" ErrorMessage="please enter name." Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                        Designation:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpdesignations" CssClass="form-control" DataTextField="Name"
                            DataValueField="Id" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqdrpdesignations" Display="None" InitialValue="-1"
                            runat="server" ErrorMessage="Please select designation." ControlToValidate="drpdesignations"
                            ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Employee Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPUCDACode" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValTxtPUCDACode" ValidationGroup="submitapplication"
                            runat="server" ControlToValidate="txtPUCDACode" ErrorMessage="Please enter employee code"
                            Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                        Mobile number:(+91 for India)
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please fill your Contact Number."
                            ControlToValidate="txtContactNumber" Display="None" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtContactNumber" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqValEmail" ControlToValidate="txtEmailAddress"
                            Display="None" ValidationGroup="submitapplication" ErrorMessage="Please fill your email address"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ID="regExpEmail" ControlToValidate="txtEmailAddress"
                            Display="None" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                            ValidationGroup="submitapplication" ErrorMessage="Please enter valid email address."></asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                        Medical Category
                    </td>
                    <td>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMedicalCategory">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Medical" Value="0" />
                            <asp:ListItem Text="55+Age" Value="1" />
                            <asp:ListItem Text="Medical Emergency" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Office:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpOffice" CssClass="form-control" DataTextField="Name" DataValueField="Id"
                            runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" InitialValue="-1"
                            runat="server" ErrorMessage="Please select office." ControlToValidate="drpOffice"
                            ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                        Grade pay as on 30th November of the Present Year(Rs):
                    </td>
                    <td>
                        <asp:DropDownList ID="drpGradePay" CssClass="form-control" runat="server" DataValueField="id"
                            DataTextField="GradePayText">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtGradePay" runat="server" ></asp:TextBox>
            <asp:CompareValidator ID="cmpgradepay" runat="server" ErrorMessage="Please enter valid grade pay" Operator="DataTypeCheck" ValidationGroup="submitapplication" ControlToValidate="txtGradePay" Type=Double Display="None"></asp:CompareValidator>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="submitapplication" runat="server" ControlToValidate="txtGradePay" ErrorMessage="please enter grade pay." Display="None"></asp:RequiredFieldValidator>
                        --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please select grade pay"
                            ControlToValidate="drpGradePay" Display="None" InitialValue="-1" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Category:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpcategory" CssClass="form-control" runat="server">
                            <asp:ListItem Value="-1">-select--</asp:ListItem>
                            <asp:ListItem Value="0">SC</asp:ListItem>
                            <asp:ListItem Value="1">ST</asp:ListItem>
                            <asp:ListItem Value="2">Others</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td>
                        Date from which continuously emplyed under Central Government including foreign
                        service. If any i.e. Date of Priority:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmployedfrom" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid Date from which continuously emplyed"
                            Operator="DataTypeCheck" ValidationGroup="submitapplication" ControlToValidate="txtEmployedfrom"
                            Type="Date" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="submitapplication"
                            runat="server" ControlToValidate="txtEmployedfrom" ErrorMessage="please enter Date from which continuously emplyed."
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server"
                            TargetControlID="txtEmployedfrom" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Date of Birth:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDob" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Please enter valid Date of birth"
                            Operator="DataTypeCheck" ValidationGroup="submitapplication" ControlToValidate="txtDob"
                            Type="Date" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="submitapplication"
                            runat="server" ControlToValidate="txtDob" ErrorMessage="please enter Date of birth."
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="txtDobCalendarExtender" runat="server"
                            TargetControlID="txtDob" />
                    </td>
                    <td>
                    </td>
                    <td>
                        Date of Retirement
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateOfRetirement" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Please enter valid  Date of Retirement"
                            Operator="DataTypeCheck" ValidationGroup="submitapplication" ControlToValidate="txtDateOfRetirement"
                            Type="Date" Display="None"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="submitapplication"
                            runat="server" ControlToValidate="txtDateOfRetirement" ErrorMessage="please enter Date of Retirement."
                            Display="None"></asp:RequiredFieldValidator>
                        <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="txtdateofretirementcalendarextender"
                            runat="server" TargetControlID="txtdateofretirement" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Gender:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpSex" CssClass="form-control" DataTextField="Name" DataValueField="Id"
                            runat="server">
                            <asp:ListItem Value="-1">-select--</asp:ListItem>
                            <asp:ListItem Value="0">Female</asp:ListItem>
                            <asp:ListItem Value="1">Male</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please select gender type"
                            ControlToValidate="drpSex" Display="None" InitialValue="-1" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                    <td>
                        Whether Temporary/Permanent:
                    </td>
                    <td>
                        <asp:DropDownList ID="drpJobType" CssClass="form-control" DataTextField="Name" DataValueField="Id"
                            runat="server">
                            <asp:ListItem Value="-1">-select--</asp:ListItem>
                            <asp:ListItem Value="0">Permanent</asp:ListItem>
                            <asp:ListItem Value="1">Temporary</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select job type"
                            ControlToValidate="drpJobType" Display="None" InitialValue="-1" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Aadhar Card No.</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtAadharNumber" MaxLength="20" CssClass="form-control" />
                        <asp:RequiredFieldValidator ValidationGroup="submitapplication" ErrorMessage="Please enter Aadhar card number" ControlToValidate="txtAadharNumber"
                            runat="server" Display="None" />
                    </td>
                    <td></td>
                    <td>PAN</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPAN" MaxLength="10" CssClass="form-control" />
                        <asp:RequiredFieldValidator ErrorMessage="Please enter PAN" ControlToValidate="txtPAN"
                            runat="server" Display="None" ValidationGroup="submitapplication" />
                    </td>

                </tr>
                <tr>
                    <td colspan="5">
                        Are you/your spouse occupying accomodation, in Audit Pool or any order departmental
                        pool :&nbsp;
                        <asp:RadioButton ID="isauditpooyes" GroupName="audit" Text="Y" OnCheckedChanged="isauditpooyes_Checked"
                            runat="server" AutoPostBack="True" /><%-- onclick="javascript:showAudit(1)"--%>
                        <asp:RadioButton ID="isauditpoono" GroupName="audit" Text="N" OnCheckedChanged="isauditpoono_Checked"
                            runat="server" AutoPostBack="True" Checked="False" /><%--onclick="javascript:showAudit(0)"--%>
                        <%--  <input type="radio" id="isauditpooyes" runat="server" name="audit"  title="Y"  value="yes"/>
            <input type="radio" id="isauditpoono" runat="server" name="audit" onclick="javascript:showAudit(0)" value="no"/>
            "display:none"
                        --%>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div id="auditpool" runat="server">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div>
                                        Name of allottee :</div>
                                    <div>
                                        <asp:TextBox ID="txtAuditAllotteName" CssClass="form-control" runat="server"></asp:TextBox></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div>
                                        Already allotted Quarter :</div>
                                    <div>
                                        <%--<asp:TextBox ID="txtQuartertype"  runat="server"  ></asp:TextBox>--%>
                                        <div>
                                            <asp:DropDownList CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtQuarterType_SelectedIndexChanged"
                                                ID="txtQuartertype" DataTextField="Name" DataValueField="Id" runat="server">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ErrorMessage="Please select already alloted" ControlToValidate="drpQuarterCategory" 
                    Display="None" InitialValue="-1" ValidationGroup="submitapplication"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div>
                                        Quarter Number :</div>
                                    <div>
                                        <asp:DropDownList CssClass="form-control" ID="drpAllotedQuarter" DataTextField="QuarterNumber"
                                            DataValueField="QuarterNumber" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" InitialValue="-1"
                                            runat="server" ErrorMessage="Please select alloted quarter." ControlToValidate="drpAllotedQuarter"
                                            ValidationGroup="submitapplication"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div>
                                        Sector or Address in Chandigarh:</div>
                                    <div>
                                        <asp:TextBox CssClass="form-control" ID="txtAuditAddress" runat="server"></asp:TextBox></div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:LinkButton ID="lnkAddMember" runat="server" Text="Add Member"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="row">
                            <div class="col-lg-12">
                                <div>
                                    Number of members residing with you who will continue to do so on your allotment
                                    of Government Accomodation:</div>
                                <div class="table-responsive">
                                    <asp:GridView ID="grdmembers" runat="server" Width="100%" AutoGenerateColumns="false">
                                        <EmptyDataTemplate>
                                            no records
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField HeaderText="Member Name" DataField="MemberName" />
                                            <asp:BoundField HeaderText="sex" DataField="sex" />
                                            <asp:BoundField HeaderText="Age" DataField="Age" />
                                            <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
                                            <asp:BoundField HeaderText="Employment Status" DataField="isEmployed" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="row">
                            <div class="col-lg-12">
                                <div>
                                    Have you ever been found to sublet government residence :&nbsp;
                                    <asp:RadioButton ID="isSubletTrue" runat="server" GroupName="sublet" Text="Y" />
                                    <%--<input type="radio" id="isSubletTrue" runat="server" name="sublet"   value="yes"/>--%>
                                    <asp:RadioButton ID="isSubletFalse" runat="server" GroupName="sublet" Text="N" />
                                    <%--<input type="radio" id="isSubletFalse" runat="server"  name="sublet"  value="no" />--%></div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="row">
                            <div class="col-lg-12">
                                <div>
                                    If yes have you been debarred for allottment of government residence :&nbsp;
                                    <asp:RadioButton ID="isDebarred" GroupName="Date" Text="Y" onclick="javascript:showbarredDate(1)"
                                        runat="server" />
                                    <%--<input type="radio" name="Date"  id="isDebarred" runat="server" onclick="javascript:showbarredDate(1)" value="yes"/>--%>
                                    <asp:RadioButton ID="isDebarredNO" GroupName="Date" onclick="javascript:showbarredDate(0)"
                                        runat="server" Text="N" />
                                    <%--<input type="radio" name="Date" onclick="javascript:showbarredDate(0)" runat="server" id="isDebarredNO" value="no" />--%></div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="row" id="barredDate" style="display: none">
                            <div class="leftMiddleColumn">
                                <div>
                                    If yes, upto which date :
                                    <asp:TextBox ID="txtDebaredDate" runat="server"></asp:TextBox>
                                    <AjaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server"
                                        TargetControlID="txtDebaredDate" />
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="row">
                            <div class="col-lg-12">
                                <div>
                                    &nbsp;
                                    <asp:CheckBox ID="chkDeclaration" runat="server"></asp:CheckBox>
                                    Declaration
                                    <asp:Label ID="validatedeclaration" Visible="false" ForeColor="Red" runat="server">* Please tick the declaration</asp:Label>
                                    <br />
                                    1. I solemnly affirm and declare that the information given above is correct to
                                    the best of my knowledge and no part thereof
                                    <br />
                                    is false or concealed.
                                    <br />
                                    2. I shall abide by the provisions of the RULES FOR ALLOTMENT OF GOVT. RESIDENTIAL
                                    ACCOMMODATION AND
                                    <br />
                                    DEPARTMENTAL GUEST HOUSES IN IA&AD, 2006 as amended from time to time.
                                    <br />
                                    3. I am aware of the penalties to be imposed in the event of refusal of acceptance
                                    of allotment of
                                    <br />
                                    accommodation of the entitled type or furnishing false information.
                                    <br />
                                    4. I am working in eligible office.
                                    <br />
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div class="row">
                            <div class="col-lg-12">
                                <div>
                                    <asp:Button ID="btnUpdate" CssClass="btn btn-primary" Text="Update" OnClientClick="return confirmmessage();"
                                        CausesValidation="true" ValidationGroup="submitapplication" OnClick="btnUpdate_Click"
                                        runat="server" Visible="false"></asp:Button><asp:Button CssClass="btn btn-primary"
                                            ID="btnAddUpdate" Text="Add / Update" OnClientClick="return confirmmessage();"
                                            CausesValidation="true" ValidationGroup="submitapplication" OnClick="btnadd_Click"
                                            runat="server"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnVerify" Text="Verify"
                                                CssClass="btn btn-default" Visible="false" CausesValidation="true" ValidationGroup="submitapplication"
                                                OnClick="btnverify_Click" runat="server"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button
                                                    ID="btnCancel" CausesValidation="false" CssClass="btn btn-danger" OnClick="btnCancel_Click"
                                                    Text="Cancel" runat="server"></asp:Button></div>
                                <asp:Button Text="Accept" CssClass="btn btn-success" CausesValidation="false" runat="server"
                                    ID="btnAccept" OnClick="btnAccept_Click" Visible="false" />&nbsp;&nbsp;
                                <asp:Button Text="Reject" CssClass="btn btn-warning" CausesValidation="false" runat="server"
                                    ID="btnReject" OnClick="btnReject_Click" Visible="false" />&nbsp;&nbsp;
                                <asp:Button Text="Cancel" CssClass="btn btn-danger" CausesValidation="false" runat="server"
                                    ID="btnCancelAndReturn" OnClick="btnCancelAndReturn_Click" Visible="false" />
                                <asp:Panel runat="server" ID="pnlInfo" Visible="false">
                                    <asp:TextBox CssClass="form-control" runat="server" ID="txtInformation" Width="400"
                                        Height="100" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="submitInfo"
                                        ErrorMessage="Please enter your information to be corrected" ControlToValidate="txtInformation"
                                        runat="server" />
                                    <br />
                                    <asp:Button Text="Submit" CssClass="btn btn-success" ValidationGroup="submitInfo"
                                        runat="server" ID="btnSubmit" OnClick="btnSubmitInfo_Click" />&nbsp;
                                    <asp:Button Text="Cancel" CssClass="btn btn-danger" ID="btnCancelInfo" OnClick="btnCancelInfo_Click"
                                        runat="server" />
                                </asp:Panel>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="addmembers" runat="server" class="modal-dialog">
        <asp:ValidationSummary ID="valsummary" runat="server" ValidationGroup="member" ShowMessageBox="true"
            ShowSummary="false" HeaderText="The form is not submitted due to:" />
        <div class="modal-content">
            <div class="modal-header">
                Add members</div>
            <div class="modal-body">
                <table width="100%">
                    <div class="table-responsive">
                        <table class="table">
                            <tr>
                                <td class="simple_text">
                                    Name of the Member
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMemberName" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Please enter member name"
                                        ControlToValidate="txtMemberName" ValidationGroup="member" Display="None"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="simple_text">
                                    Age
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMemberAge" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter member Age"
                                        ControlToValidate="txtMemberAge" ValidationGroup="member" Display="None"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cmpdate" runat="server" Display="None" Operator="DataTypeCheck"
                                        ControlToValidate="txtMemberAge" ErrorMessage="please enter valid age" ValidationGroup="member"
                                        Type="Integer"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="simple_text">
                                    Gender
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpMemberSex" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Selected="true">Female</asp:ListItem>
                                        <asp:ListItem Value="1">Male</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="simple_text">
                                    Relationship of Allottee
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRelationShip" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="simple_text">
                                    Whether Employed
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdbemplyed" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="true">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnsaveMember" CssClass="btn btn-success" ValidationGroup="member"
                                        runat="server" Text="Save" OnClick="btnsaveMember_click" />&nbsp;&nbsp;&nbsp;<asp:Button
                                            CausesValidation="false" ID="btnCancemember" CssClass="btn btn-danger" runat="server"
                                            Text="Cancel" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>
    <AjaxToolkit:ModalPopupExtender ID="pop" BackgroundCssClass="overlay" runat="server"
        PopupControlID="addmembers" TargetControlID="lnkAddMember" CancelControlID="btnCancemember">
    </AjaxToolkit:ModalPopupExtender>
</asp:Content>
