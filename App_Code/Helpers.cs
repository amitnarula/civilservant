using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Data;
using System.IO;
/// <summary>
/// Summary description for Helpers
/// </summary>
public class Helpers
{
	public Helpers()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}


public enum CategoryWorkflowStatus
{ 
    VERIFIED=0,
    VALIDATED=1,
    APPROVED=2,
    PUBLISHED=3
}

public enum ApplicationStatus
{
    Pending=0,
    Verified=1,
    Allotted=2,
    Pos=3,
    withdraw=4,
    rejected=5,
    RequestedAgain=6,
    ChangeRequested=7
}
public enum AllotementStatus
{
    Pending = 0,
    Allotted = 1,
    Possessed = 2,
    withdraw = 3,
    Retension=4,
    vacant=5,
    ChangeRequested=6
}

public enum ChangeRequestStatus
{ 
    Pending=0,
    Approved=1,
    Done=2,
    Deleted=3
}

public class AllotementApplication
{
    private string _AAN;
    private string _UserName;
    private string _Dept;
    private string _QuarterType;
    private string _cast;
    private long _ApplicationId;
    private string _gradePay;
    DateTime _dateOfJoining;
    string _quarterNumber;
    private ApplicationStatus _Status;
    private String _AlreadyAllottedQuarter,_Designation;
    public string AAN { get { return _AAN; } set { _AAN = value; } }
    public string UserName { get { return _UserName; } set { _UserName = value; } }
    public string Dept { get { return _Dept; } set { _Dept = value; } }
    public string QuarterType { get { return _QuarterType; } set { _QuarterType = value; } }
    public string AlreadyAllottedQuarter { get { return _AlreadyAllottedQuarter; } set { _AlreadyAllottedQuarter = value; } }
    public string Designation { get { return _Designation; } set { _Designation = value; } }
    public string GradePay { get { return _gradePay; } set { _gradePay = value; } }
    public string Cast { get { return _cast; } set { _cast = value; } }
    public long Id { get { return _ApplicationId; } set { _ApplicationId = value; } }
    public ApplicationStatus Status { get { return _Status; } set { _Status = value; } }
    public string QuarterNumber { get { return _quarterNumber; } set { _quarterNumber = value; } }
    
    public DateTime dateOfJoining { get { return _dateOfJoining; } set { _dateOfJoining = value; } }
    public DateTime DateOfAllottment { get; set; }
    public bool MedicalCategory { get; set; }
    public string ContactNumber { get; set; }
}

public enum QuarterStatus
{
    Vacant=0,
    Alloted=1,
    Possessed=2,
    CPWD=3,
    Damaged=4,
    Surrender=5,
    Retention=6
}

public class AllotementInformattion
{
    long _id;
    private string _userName;
    private string _QuarterNumber;
    private string _office;
    private string _aan;
    public string Name { get { return _userName; } set { _userName = value; } }
    public string Office { get { return _office; } set { _office = value; } }
    public long Id { get { return _id; } set { _id = value; } }
    public string AAN { get { return _aan; } set { _aan = value; } }
    public string QuarterNumber { get { return _QuarterNumber; } set { _QuarterNumber = value; } }
}

public class Quarerinfo { 
string _CategoryName;
string    _FloorName;
string    _QuarterNumber;
long _Id;
DateTime? _DateOfVacation, _DateOfAllottement;
public DateTime? DateOfVacation { get { return _DateOfVacation; } set { _DateOfVacation = value; } }
public DateTime? DateOfAllottement { get { return _DateOfAllottement; } set { _DateOfAllottement = value; } }

//DateOfVacationDateOfAllottement
public string CategoryName { get { return _CategoryName; } set { _CategoryName = value; } }
public string FloorName { get { return _FloorName; } set { _FloorName = value; } }
public string QuarterNumber{get{return _QuarterNumber;}set{_QuarterNumber=value;}}
public long Id { get { return _Id; } set { _Id = value; } }
}


public class Memberinfo
{
    string _Name;
    string _sex;
    int _age;
    string  _employedLocation;
    bool _isEmployed;
    string _relationship;
    public string Name { get { return _Name; } set { _Name = value; } }
    public string sex { get { return _sex; } set { _sex = value; } }
    public int age { get { return _age; } set { _age = value; } }
    //public string employedLocation { get { return _employedLocation; } set { _employedLocation = value; } }
    public bool isEmployed { get { return _isEmployed; } set { _isEmployed = value; } }
    public string Relationship { get { return _relationship; } set { _relationship = value; } }

    //public Memberinfo(string _Name,string _sex,int _age,string  employedLocation;
    //bool _isEmployed;)
    //{ 
    //}
}


public class ApplicationModules
{
    public static string Quarters = "Quarters", Allotment = "Allotment", PendingCases = "Pending Cases", LicenceFeeRecovery = "Licence Fee Recovery", UserHistory = "User History", ApprovalGroup = "Approval Group", QuarterHistory = "Quarter History", Noticegeneration = "Notice generation", Pendingrecoveries = "Pending recoveries", Reports = "Reports";

}






public class GradePay {
    public static List<tblGradePay> GetPayGradesByCategroy(long category)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = from igradePay in dataContext.tblGradePays where igradePay.QuarterType == category select igradePay;

        return varlicFee.ToList();
    }
    public static string GetPayGradesById(long id)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = from igradePay in dataContext.tblGradePays where igradePay.id == id select igradePay;

        return varlicFee.FirstOrDefault()==null?"":varlicFee.FirstOrDefault().GradePayText;
    }
    public static List<tblGradePay> GetAllGradePays()
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = dataContext.tblGradePays.ToList();

        return varlicFee.ToList();
    }

    public static long GetQuarterCategoryByGradePay(int gradePayId)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = dataContext.tblGradePays.Where(g => g.id == gradePayId).Select(g => g.QuarterType.Value).ToList().FirstOrDefault();

        return varlicFee;
    }

}


public class SendmailMessage {
    public static bool sendEmailMessage(string strFromAddress, string strToAddress, string strBCCAddress, string strSubject, string strBody, string strAttachmentFileName, out string errormsg)
    {
        errormsg = "";

        try
        {
            //using System.Net.Mail

            System.Net.Mail.MailMessage Mailer = new MailMessage(strFromAddress, strToAddress);
            Mailer.Subject = strSubject;
            Mailer.Body = strBody;
            //Mailer.IsBodyHtml = boolIsEmailBodyHTML;
            if (strBCCAddress != "")
            {
                MailAddress bcc = new MailAddress(strBCCAddress);
                Mailer.Bcc.Add(bcc);
            }

            System.Net.Mail.SmtpClient SmtpMail = new SmtpClient();
            if (strAttachmentFileName != "")
            {
                strAttachmentFileName = strAttachmentFileName.Trim();
                strAttachmentFileName = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(""), strAttachmentFileName);
                System.Net.Mail.Attachment mailAttachment = new Attachment(strAttachmentFileName);
                Mailer.Attachments.Add(mailAttachment);
            }

            SmtpMail.Host = ConfigurationManager.AppSettings["smtphost"];
            SmtpMail.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            SmtpMail.Port = 8889;
            SmtpMail.EnableSsl = false;



            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["emailaddress"], ConfigurationManager.AppSettings["emailpassword"]);
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;

            SmtpMail.Send(Mailer);
            return true;
        }
        catch (Exception e)
        {
            errormsg = e.Message;
            return false;
        }
    }
    
    public static void SendMail(string toEmail,string fromEmail,string body, string subject)
    {
        SmtpClient smtpClient = new SmtpClient();

        smtpClient.Credentials = new NetworkCredential("postmaster@estatepagpb.org", "Rambo@6544");
        smtpClient.Host="mail.estatepagpb.org";
        smtpClient.Port=8889;
        

        MailMessage message = new MailMessage();


        MailAddress fromAddress = new MailAddress(fromEmail);

            // You can specify the host name or ipaddress of your server

            // Default in IIS will be localhost 

        //smtpClient.Host = "mail.estatepagpb.org.cws7.my-hosting-panel.com";

            //Default port will be 25

          //  smtpClient.Port = 25;

            //From address will be given as a MailAddress Object

            message.From = fromAddress;
        

            // To address collection of MailAddress

            message.To.Add(toEmail);
            message.To.Add("amrinder.bhagtana@gmail.com"); //Administration email
            message.Subject = subject;

            // CC and BCC optional

            // MailAddressCollection class is used to send the email to various users

            // You can specify Address as new MailAddress("admin1@yoursite.com")

            //message.CC.Add("admin1@yoursite.com");
            //message.CC.Add("admin2@yoursite.com");

            //// You can specify Address directly as string

            //message.Bcc.Add(new MailAddress("admin3@yoursite.com"));
            //message.Bcc.Add(new MailAddress("admin4@yoursite.com"));

            //Body can be Html or text format

            //Specify true if it  is html message

            message.IsBodyHtml = false;

            // Message body content

            message.Body = body;
         
            // Send SMTP mail

            smtpClient.Send(message);


 

    }

}


public class Tblmemberinfo {

    public static void Save(List<tblMember> lstinformatation)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        dataContext.tblMembers.InsertAllOnSubmit(lstinformatation);
        dataContext.SubmitChanges();
    }
    public static List<tblMember> GetMemberinfo(long applicationId)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varmemembers = from mem in dataContext.tblMembers where mem.ApplicationId == applicationId select mem;
        return varmemembers.ToList();
    }
}

public class tblPhotos {

    public static void Save(tblImage lstinformatation)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        dataContext.tblImages.InsertOnSubmit(lstinformatation);
        dataContext.SubmitChanges();
    }
    public static List<tblImage> GetPhotos()
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varmemembers = from mem in dataContext.tblImages select mem;
        return varmemembers.ToList();
    }
    public static void Delete(long id)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varmemembers = from mem in dataContext.tblImages where mem.id==id select mem;
        tblImage _ima = varmemembers.FirstOrDefault();
        if(_ima !=null)
            dataContext.tblImages.DeleteOnSubmit(_ima);
        dataContext.SubmitChanges();
    }
}



public class userHistory {
    public static void Save(tbluserhistory _userhistory)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        dataContext.tbluserhistories.InsertOnSubmit(_userhistory);
        dataContext.SubmitChanges();
    }
    public static List<tbluserhistory> GetUserHistory(string aan)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var history = from _userhistory in dataContext.tbluserhistories where _userhistory.useraan == aan select _userhistory;
        return history.ToList();
    }
}

public class ExcelHelper
{
    public static void ExportToExcel(DataSet ds, string sheetName, System.Web.UI.Page page)
    {
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count <= 0)
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }


        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        wbook.Worksheets.Add(ds.Tables[0], sheetName);
        // Prepare the response
        HttpResponse httpResponse = page.Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename='" + sheetName + ".xlsx'");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
    }
}

