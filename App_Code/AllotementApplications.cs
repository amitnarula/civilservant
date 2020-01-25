using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AllotementApplications
/// </summary>
public class AllotementApplications
{
    public AllotementApplications()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static tbAllotmentApplication SaveApplications(tbAllotmentApplication application)
{
    DataClassesDataContext dataContext = new DataClassesDataContext();
    if (application.ID <= 0)
        dataContext.tbAllotmentApplications.InsertOnSubmit(application);
    else
    {
        var _tempapplication = from temp in dataContext.tbAllotmentApplications where temp.ID == application.ID select temp;
        tbAllotmentApplication _application = _tempapplication.FirstOrDefault();
        _application.DateOfBirth = application.DateOfBirth;
        _application.DateOfjoining = application.DateOfjoining;
        _application.GradePay = application.GradePay;
        _application .Name = application.Name;
        _application .Designation = application.Designation;
        _application .OfficeId = application.OfficeId;
        _application .OtherQuarterAddress = application.OtherQuarterAddress;
        _application .OtherQuarterNumber = application.OtherQuarterNumber;
        _application .JobType = application.JobType;
        _application .Sex = application.Sex;
        _application .Status = application.Status;
        
        _application .Cast = application.Cast;
        _application .IsSublet = application.IsSublet;
        _application .IsDebarred = application.IsDebarred;
        _application .QuarterCategory = application.QuarterCategory;
        _application.ContactNumber = application.ContactNumber;
        _application.MedicalCategory = application.MedicalCategory;
        if (_application.IsDebarred.HasValue && _application.IsDebarred.Value)
        {
            
            _application .DebarredDate = application.DebarredDate;
        }
        _application .IsOtherAccomendation = application.IsOtherAccomendation;
        if (_application.IsOtherAccomendation.HasValue && _application.IsOtherAccomendation.Value)
        {
            _application .OtherAllotteName = application.OtherAllotteName;
            _application .OtherQuarterAddress = application.OtherQuarterAddress;
            _application.OtherQuarterNumber = application.OtherQuarterNumber;
        }

    }
    dataContext.SubmitChanges();
    return application;
}
    public static tbAllotmentApplication updateApplications(tbAllotmentApplication application)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        
        //dataContext.tbAllotmentApplications.InsertOnSubmit(application);
        dataContext.SubmitChanges();
        return application;
    }
    public static List<AllotementApplication> GetPendingApplications(ApplicationStatus status)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications=(from application in dataContext.tbAllotmentApplications where application.Status==Convert.ToInt32(status) select application).OrderBy(x=>x.DateOfjoining);
        List<AllotementApplication> allotements = new List<AllotementApplication>();
      
        foreach (var application in applications)
        {
            AllotementApplication obj = new AllotementApplication();
            obj.Id = application.ID;
            obj.Status = (ApplicationStatus)application.Status;
            obj.AAN = application.tblUser.AAN;
            obj.Designation = application.tblDesignation.Name;
            obj.Dept = application.tblOffice.Name;
            obj.UserName = application.tblUser.fullName;
            obj.QuarterType = application.tblQuarterCategory.Name;
            obj.Cast = application.Cast;
            allotements.Add(obj);
        }
        return allotements;
    }

    public static List<AllotementApplication> GetPendingApplications(ApplicationStatus status,int baseofficeid)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = (from application in dataContext.tbAllotmentApplications where (application.tblUser.BaseOfficeId.HasValue==false || application.tblUser.BaseOfficeId.Value==baseofficeid)&& application.Status == Convert.ToInt32(status) select application).OrderBy(x => x.DateOfjoining);
        List<AllotementApplication> allotements = new List<AllotementApplication>();

        foreach (var application in applications)
        {
            AllotementApplication obj = new AllotementApplication();
            obj.Id = application.ID;
            obj.Status = (ApplicationStatus)application.Status;
            obj.AAN = application.tblUser.AAN;
            obj.Dept = application.tblOffice.Name;
            obj.Designation = application.tblDesignation.Name;
            obj.UserName = application.tblUser.fullName;
            obj.QuarterType = application.tblQuarterCategory.Name;
            obj.Cast = application.Cast;
            allotements.Add(obj);
        }
        return allotements;
    }


    public static List<AllotementApplication> GetPendingApplications(ApplicationStatus status,long categoryId)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = (from application in dataContext.tbAllotmentApplications
                            where application.Status == Convert.ToInt32(status) && application.QuarterCategory == categoryId 
                            select application).OrderBy(x => x.DateOfjoining).ThenBy(x=>x.DateOfBirth);
        List<AllotementApplication> allotements = new List<AllotementApplication>();

        foreach (var application in applications)
        {
            AllotementApplication obj = new AllotementApplication();
            obj.Id = application.ID;
            obj.Status = (ApplicationStatus)application.Status;
            obj.AAN = application.tblUser.AAN;
            obj.Dept = application.tblOffice.Name;
            obj.AlreadyAllottedQuarter = application.OtherQuarterNumber == "-1" ? "N/A" : application.OtherQuarterNumber;
            obj.GradePay = GradePay.GetPayGradesById(Convert.ToInt64(application.GradePay));
            obj.Designation = application.tblDesignation.Name;
            obj.UserName = application.tblUser.fullName;
            obj.QuarterType = application.tblQuarterCategory.Name;
            obj.Cast = application.Cast == "-select--" ? "N/A" : application.Cast;
            obj.QuarterNumber = application.QuarterNumber;
            obj.MedicalCategory = application.MedicalCategory.HasValue && application.MedicalCategory.Value >= 0 ? true : false;
            if(application.tblUser.DateOfJoining.HasValue)
            obj.dateOfJoining = application.tblUser.DateOfJoining.Value;
            allotements.Add(obj);
            
        }
        return allotements;
    }
    public static List<AllotementApplication> GetPendingAndVerifiedApplications(long categoryId)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = (from application in dataContext.tbAllotmentApplications where (application.Status == Convert.ToInt32(ApplicationStatus.Verified) || application.Status == Convert.ToInt32(ApplicationStatus.Pending)) && application.QuarterCategory == categoryId select application).OrderBy(x => x.DateOfjoining);
        List<AllotementApplication> allotements = new List<AllotementApplication>();

        foreach (var application in applications)
        {
            AllotementApplication obj = new AllotementApplication();
            obj.Id = application.ID;
            obj.Status = (ApplicationStatus)application.Status;
            obj.AAN = application.tblUser.AAN;
            obj.Dept = application.tblOffice.Name;
            obj.AlreadyAllottedQuarter = application.OtherQuarterNumber;
            obj.GradePay = GradePay.GetPayGradesById(Convert.ToInt64(application.GradePay));
            obj.Designation = application.tblDesignation.Name;
            obj.UserName = application.tblUser.fullName;
            obj.QuarterType = application.tblQuarterCategory.Name;
            obj.Cast = application.Cast;
            if (application.tblUser.DateOfJoining.HasValue)
                obj.dateOfJoining = application.tblUser.DateOfJoining.Value;
            allotements.Add(obj);
        }
        return allotements;
    }
    public static void UpdateApplicationStaus(long id,ApplicationStatus status)
    {
       
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = from application in dataContext.tbAllotmentApplications where application.ID ==id select application;
        tbAllotmentApplication updateApplication = applications.FirstOrDefault();
        if (updateApplication != null)
        {
            updateApplication.Status = (int)status;
        }
        
        dataContext.SubmitChanges();
    }
    public static tbAllotmentApplication GetApplication(long id)
    {

        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = from application in dataContext.tbAllotmentApplications where application.ID == id select application;
       // tbAllotmentApplication updateApplication = applications.FirstOrDefault();
        return applications.FirstOrDefault();
    }
    public static tbAllotmentApplication GetApplicationByAAN(string AAN)
    {

        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = from application in dataContext.tbAllotmentApplications where application.tblUser.AAN == AAN && application.SubmissionDate.Value.Year==DateTime.Now.Year  select application;
        // tbAllotmentApplication updateApplication = applications.FirstOrDefault();
        return applications.FirstOrDefault();
    }

    public static tbAllotmentApplication GetApplicationByAANIrrespectiveOfSubmissionDate(string AAN)
    {

        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = from application in dataContext.tbAllotmentApplications where application.tblUser.AAN == AAN select application;
        // tbAllotmentApplication updateApplication = applications.FirstOrDefault();
        return applications.FirstOrDefault();
    }

    public static List<AllotementApplication> GetApplicationsByAAn(string AAN)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = (from application in dataContext.tbAllotmentApplications where application.tblUser.AAN==AAN select application).OrderBy(x => x.DateOfjoining);
        List<AllotementApplication> allotements = new List<AllotementApplication>();

        foreach (var application in applications)
        {
            AllotementApplication obj = new AllotementApplication();
            obj.Id = application.ID;
            obj.Status = (ApplicationStatus)application.Status;
            obj.AAN = application.tblUser.AAN;
            obj.Dept = application.tblOffice.Name;
            obj.Designation = application.tblDesignation.Name;
            obj.UserName = application.tblUser.fullName;
            obj.QuarterType = application.tblQuarterCategory.Name;
            obj.Cast = application.Cast;
            obj.ContactNumber = application.ContactNumber;
            allotements.Add(obj);
        }
        return allotements;
    }
    public static List<AllotementApplication> GetAllotedUsers(ApplicationStatus status, long categoryId)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var applications = dataContext.uspGetAllotedUsers(Convert.ToInt32(status), Convert.ToInt32(categoryId));
        List<AllotementApplication> allotements = new List<AllotementApplication>();

        foreach (var application in applications)
        {
            AllotementApplication obj = new AllotementApplication();
            obj.Id = application.ID;
            obj.Status = (ApplicationStatus)application.Status;
            obj.AAN = application.AAN;
            obj.Dept = application.OfficeName;
            obj.AlreadyAllottedQuarter = application.OtherQuarterNumber;
            obj.GradePay = GradePay.GetPayGradesById(Convert.ToInt64(application.GradePay));
            obj.Designation = application.DesignationName;
            obj.UserName = application.fullName;
            obj.QuarterType = application.QuarterCategoryName;
            obj.Cast = application.Cast;
            obj.QuarterNumber = application.QuarterNumber;
            obj.DateOfAllottment = application.DateOfAllotement.HasValue ? application.DateOfAllotement.Value : DateTime.Now;
            if (application.DateOfjoining.HasValue)
                obj.dateOfJoining = application.DateOfjoining.Value;
            allotements.Add(obj);

        }
        return allotements;
    }
    
}
