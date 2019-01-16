<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printapplication.aspx.cs" Inherits="User_printapplication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/GlobalStyle.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function showAudit(value) {
            if (value == 1)
                document.getElementById('auditpool').style.display = "block";
            else
                document.getElementById('auditpool').style.display = "none";
        } function showsublet(value) {
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



        
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="contentContainer" style="overflow:scroll;">
<div id="managerContainerHeader">
<SPAN >Submission</SPAN> 
</div>

<br /><br />
    <div class="row">
        <div class="leftMiddleColumn">
            <div><asp:Label ID="lblmessage" runat="server" Visible=false></asp:Label></div>
        </div>
           </div>
           <br />
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Quarter Type :</div>
            <div><asp:Label  ID="lblQuarterCategory"  runat="server" ></asp:Label></div>
        </div>
        <div class="rightMiddleColumn">
            <div>AAN :</div>
            <div><asp:Label ID="txtAAN" runat="server"  ReadOnly="true"></asp:Label></div>
        </div>
           </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Name(in block letters) :</div>
            <div><asp:Label ID="txtUsername" runat="server" ></asp:Label>
            </div>
        </div>
        <div class="rightMiddleColumn">
            <div>Designation :</div>
            <div><asp:Label ID="lbldesignations"  runat="server" ></asp:Label></div>
             
                
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>office :</div>
            <div><asp:Label ID="lblOffice" runat="server" ></asp:Label></div>
                
        </div>
        <div class="rightMiddleColumn">
            <div>Grade pay as on 30th November of the Present Year(Rs):</div>
            <div>
            <asp:Label id="lblGradePay" runat="server" DataValueField="id" DataTextField="GradePayText"></asp:Label>
            <%--<asp:Label ID="txtGradePay" runat="server" ></asp:Label>
            <asp:CompareValidator ID="cmpgradepay" runat="server" ErrorMessage="Please enter valid grade pay" Operator=DataTypeCheck ValidationGroup="submitapplication" ControlToValidate="txtGradePay" Type=Double Display="None"></asp:CompareValidator>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="submitapplication" runat="server" ControlToValidate="txtGradePay" ErrorMessage="please enter grade pay." Display="None"></asp:RequiredFieldValidator>
           --%> </div>
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Category:</div>
            <div>
            <asp:Label ID="lblcategory" runat=server >
            
            </asp:Label>            </div>
        </div>
        <div class="rightMiddleColumn">
            <div style="white-space:normal;">Date from which continuously emplyed under Central Government including foreign service. If any i.e. Date of Priority:</div>
            <div>
                <asp:Label ID="txtEmployedfrom" runat="server"  ></asp:Label>
                </div>
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Date of Birth :</div>
            <div>
                <asp:Label ID="txtDob" runat="server"  ></asp:Label>
                </div>
        </div>
        <div class="rightMiddleColumn">
            <div>Date of Retirement :</div>
            <div>
                <asp:Label ID="txtDateOfRetirement"  runat="server"  ></asp:Label>
                </div>
        </div>
    </div>
      <div class="row">
        <div class="leftMiddleColumn">
            <div>Sex :</div>
            <div>
                <asp:Label ID="lblSex" runat="server" >
                 </asp:Label>
                
            </div>
        </div>
        <div class="rightMiddleColumn">
            <div>Whether Temporary/Permanent :</div>
            <div>
                <asp:Label ID="drpJobType" runat="server" >
                </asp:Label>
                
            </div>
        </div>
    </div>
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Are you/your sppouse occupying accomodation, in Audit Pool or any order departmental pool :&nbsp;
            <asp:Label ID="lblauditpooyes" runat="server"></asp:Label>
            <asp:RadioButton ID="isauditpooyes" Visible="false" GroupName="audit" Text="Y" onclick="javascript:showAudit(1)" runat=server />
    <asp:RadioButton ID="isauditpoono" GroupName="audit" Visible="false" Text="N" onclick="javascript:showAudit(0)" runat=server />
    
          <%--  <input type="radio" id="isauditpooyes" runat="server" name="audit"  title="Y"  value="yes"/>
            <input type="radio" id="isauditpoono" runat="server" name="audit" onclick="javascript:showAudit(0)" value="no"/>
          --%>  
            </div>
        </div>
       
    </div>
    <div id="auditpool" style="display:none">
    <div class="row">
        <div class="leftMiddleColumn">
            <div>Name of allottee :</div>
            <div> <asp:Label ID="txtAuditAllotteName"  runat="server"  ></asp:Label></div>
        </div>
       
    </div>
      <div class="row">
        <div class="leftMiddleColumn">
            <div>Already allotted Quarter :</div>
            <div> <asp:Label ID="txtQuartertype"  runat="server"  ></asp:Label></div>
        </div>
       
    </div>
          <div class="row">
        <div class="leftMiddleColumn">
            <div>Sector or Address in Chandigarh:</div>
            <div> <asp:Label ID="txtAuditAddress"  runat="server"  ></asp:Label></div>
        </div>
       
    </div>
     </div>
     
     <div class="row" >
        <div class="leftMiddleColumn">
            <div>Number of members residing with you who will continue to do  so on your allotment of Government Accomodation:</div>
            <div>
            <asp:GridView ID="grdmembers" runat="server" Width="100%" AutoGenerateColumns=false>
            <EmptyDataTemplate>
            no records
            </EmptyDataTemplate>
            <Columns>
            <asp:BoundField HeaderText="Member Name" DataField="MemberName" />
            <asp:BoundField HeaderText="sex" DataField="sex" />
            <asp:BoundField HeaderText="Age" DataField="Age" />
            <asp:BoundField HeaderText="Relationship" DataField="Relationship" />
            <asp:BoundField HeaderText="isEmployed" DataField="isEmployed" />
            </Columns>
            </asp:GridView>
            
            </div>
        </div>
       
    </div>
       <div class="row">
        <div class="leftMiddleColumn">
            <div>Have you ever been found to sublet government residence :&nbsp;
            <asp:Label ID="lblSublet" runat="server"></asp:Label>
            <asp:RadioButton ID="isSubletTrue" runat="server" Visible="false"  GroupName="sublet" Text="Y" />
            <%--<input type="radio" id="isSubletTrue" runat=server name="sublet"   value="yes"/>--%>
            <asp:RadioButton ID="isSubletFalse" runat="server" Visible="false"  GroupName="sublet" Text="N" />
            
            <%--<input type="radio" id="isSubletFalse" runat=server  name="sublet"  value="no" />--%></div>
          
        </div>
       
    </div>
    <div id="sublet" >
       <div class="row">
        <div class="leftMiddleColumn">
            <div>If yes have you been debarred for allottment of government residence :&nbsp;
            <asp:Label ID="lblDebarred" runat="server"></asp:Label>
            
            <asp:RadioButton ID="isDebarred" Visible="false" GroupName="Date" Text="Y" onclick="javascript:showbarredDate(1)" runat=server/>
            <%--<input type="radio" name="Date"  id="isDebarred" runat="server" onclick="javascript:showbarredDate(1)" value="yes"/>--%>
            <asp:RadioButton ID="isDebarredNO" Visible="false" GroupName="Date" onclick="javascript:showbarredDate(0)" runat="server" Text="N" />
            <%--<input type="radio" name="Date" onclick="javascript:showbarredDate(0)" runat="server" id="isDebarredNO" value="no" />--%></div>
          
        </div>
       
    </div>
       <div class="row" id="barredDate" style="display:none">
        <div class="leftMiddleColumn">
            <div>If yes, upto which date : <asp:Label ID="txtDebaredDate"  runat="server"  ></asp:Label>
                
</div>
        </div>
       
    </div>
    <div class="row">
       
        <div class="leftMiddleColumn">
            <div>Contact Number :&nbsp;
                 <asp:Label ID="txtContactNumber" runat="server"></asp:Label>
        </div>
        
        </div>
     
     </div>
    </div>
    <div class="row"></div>
    <div class="row">
        <div class="leftMiddleColumn">
            <center>
                <asp:Button ID="btnCancel" CausesValidation="false" Text="Print" runat="server" ></asp:Button>
            </center>
        </div>
        <div class="rightMiddleColumn">
            <center>
                <div></div>
            </center>
        </div>
    </div>
</div>

    </div>
    </form>
</body>
</html>
