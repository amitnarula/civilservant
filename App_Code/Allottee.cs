using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Allottee
/// </summary>
public class Allottee
{
	public Allottee()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Save(tblAllottee allottee)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varQuarters = from quarterInfo in datacontext.tblAllottees where quarterInfo.Id == allottee.Id select quarterInfo;
        tblAllottee quaterValue = varQuarters.FirstOrDefault();
        if (quaterValue == null)
        {
            //datacontext.tblAllottees.InsertOnSubmit(allottee);
            insert(allottee);
        }
        else
        {
            update(allottee);
            //quaterValue.DateOfJoining = allottee.DateOfJoining;
            //quaterValue.DateOfRetirement = allottee.DateOfRetirement;
            //quaterValue.Designation = allottee.Designation;
            //quaterValue.Name = allottee.Name;
            //quaterValue.OfficeId = allottee.OfficeId;
        }
        datacontext.SubmitChanges();
    }
    public static List<tblDesignation> GetDesignations()
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varDesignation = from designation in dataContext.tblDesignations select designation;
        return varDesignation.ToList();
    }
    public static tblAllottee GetAllottee(long id)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varAllottees = from allotte in datacontext.tblAllottees where allotte.Id==id select allotte;
        return varAllottees.FirstOrDefault();
    }
    public static tblAllottee GetAllottee(string quarterNumber)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varAllottees = from allotte in datacontext.tblAllottees where allotte.QuarterNumber == quarterNumber select allotte;
        return varAllottees.FirstOrDefault();
    }
    public static List<uspGetAllotteResult> GetAllottees()
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        long? j=null;
        //var varAllottees = from allotte in datacontext.tblAllottees select new AllotementInformattion { Id = allotte.Id, Name = allotte.tblUser.fullName, AAN = allotte.AAN, Office = allotte.tblUser.tblOffice.Name, QuarterNumber = allotte.QuarterNumber };
        var varAllottees = datacontext.uspGetAllotte(j,null);
        return varAllottees.ToList();
    }
    public static string  GetDesignationName(long id)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varDesignation = from designation in dataContext.tblDesignations where designation.Id == id select designation;
        return varDesignation.FirstOrDefault() == null ? "" : varDesignation.FirstOrDefault().Name;
    }

    private static void update(tblAllottee allotte)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();

        tblAllottee allotteeToUpdate = datacontext.tblAllottees.Where(x => x.Id == allotte.Id).SingleOrDefault();

        allotteeToUpdate.AAN = allotte.AAN;
        allotteeToUpdate.QuarterNumber = allotte.QuarterNumber;
        allotteeToUpdate.ApplicationId = allotte.ApplicationId;
        allotteeToUpdate.Status = allotte.Status;
        allotteeToUpdate.DateOfAllotement = allotte.DateOfAllotement;

        datacontext.SubmitChanges();
    }

    private static void insert(tblAllottee allotte)
    {
        //string query = "insert into tblAllottee(aan,quarternumber,ApplicationId,Status,DateOfAllottement)values('" + allotte.AAN + "','" + allotte.QuarterNumber + "'," + allotte.ApplicationId + "," + allotte.Status + ","+allotte.DateOfAllotement+")";
        DataClassesDataContext datacontext = new DataClassesDataContext();
        //datacontext.ExecuteCommand(query, "");

        tblAllottee tblAlottee = new tblAllottee();
        tblAlottee.AAN = allotte.AAN;
        tblAlottee.QuarterNumber = allotte.QuarterNumber;
        tblAlottee.ApplicationId = allotte.ApplicationId;
        tblAlottee.Status = allotte.Status;
        tblAlottee.DateOfAllotement = allotte.DateOfAllotement;

        datacontext.tblAllottees.InsertOnSubmit(tblAlottee);
        datacontext.SubmitChanges();

    }
    public static void VacantAllottement(long id)
    {
        //DataClassesDataContext datacontext = new DataClassesDataContext();
        //var varAllottees = from allotte in datacontext.tblAllottees where allotte.Id==id select allotte;
        //tblAllottee all = varAllottees.FirstOrDefault();
        //Quarters.UpdateQuarterStatus(all.tblQuarter.Id, QuarterStatus.Vacant);
        //Allottee.Delete(id);
  }
    private static void Delete(long id)
    {
        string query = "delete from tblAllottee where id=" + id;
        DataClassesDataContext datacontext = new DataClassesDataContext();
        datacontext.ExecuteCommand(query, "");
    }
    public static tblAllottee GetAllotteeByAAN(string AAN)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varAllottees = from allotte in datacontext.tblAllottees where allotte.AAN == AAN select allotte;
        return varAllottees.FirstOrDefault();
    }
    public static List<uspGetAllotteResult> GetPossessedAllotte()
    {
        List<uspGetAllotteResult> _lst = new List<uspGetAllotteResult>();
        DataClassesDataContext datacontext = new DataClassesDataContext();
        long? j = (long)AllotementStatus.Possessed;
        //var varAllottees = from allotte in datacontext.tblAllottees select new AllotementInformattion { Id = allotte.Id, Name = allotte.tblUser.fullName, AAN = allotte.AAN, Office = allotte.tblUser.tblOffice.Name, QuarterNumber = allotte.QuarterNumber };
        var varAllottees = datacontext.uspGetAllotte(j,null);
        List<uspGetAllotteResult> _lstPossed = varAllottees.ToList();
        if (_lstPossed.Count > 0)
            _lst.AddRange(_lstPossed);
        j = (long)AllotementStatus.Retension;

        varAllottees = datacontext.uspGetAllotte(j,null);
        List<uspGetAllotteResult> _lstRete = varAllottees.ToList();
        if (_lstRete.Count > 0)
            _lst.AddRange(_lstRete);
        return _lst;
    }
    public static List<uspGetAllotteResult> GetPossessedAllotte(long category)
    {
        List<uspGetAllotteResult> _lst = new List<uspGetAllotteResult>();
        DataClassesDataContext datacontext = new DataClassesDataContext();
        long? j = (long)AllotementStatus.Possessed;
        //var varAllottees = from allotte in datacontext.tblAllottees select new AllotementInformattion { Id = allotte.Id, Name = allotte.tblUser.fullName, AAN = allotte.AAN, Office = allotte.tblUser.tblOffice.Name, QuarterNumber = allotte.QuarterNumber };
        var varAllottees = datacontext.uspGetAllotte(j,category);
        List<uspGetAllotteResult> _lstPossed = varAllottees.ToList();
        if (_lstPossed.Count > 0)
            _lst.AddRange(_lstPossed);
        //j = (long)AllotementStatus.Retension;

        //varAllottees = datacontext.uspGetAllotte(j,category);
        //List<uspGetAllotteResult> _lstRete = varAllottees.ToList();
        //if (_lstRete.Count > 0)
        //    _lst.AddRange(_lstRete);

        return _lst;
    }
    public static void UpdateAllottementStatus(long applicationid,AllotementStatus status)
    {
        string query = "update tblAllottee set Status=" + (int)status + " where ApplicationId=" + applicationid;
        DataClassesDataContext datacontext = new DataClassesDataContext();
        datacontext.ExecuteCommand(query, "");
    }
    public static void Update(tblAllottee allottee)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varQuarters = from quarterInfo in datacontext.tblAllottees where quarterInfo.Id == allottee.Id select quarterInfo;
        tblAllottee quaterValue = datacontext.tblAllottees.Where(x => x.Id == allottee.Id).FirstOrDefault();
        if (quaterValue != null)
        {
            if (allottee.DateOfAllotement.HasValue)
                quaterValue.DateOfAllotement = allottee.DateOfAllotement;
            if (allottee.DateOfPossession.HasValue)
                quaterValue.DateOfPossession = allottee.DateOfPossession;
            if (allottee.DateOfRetension.HasValue)
                quaterValue.DateOfRetension = allottee.DateOfRetension.Value;
            if (allottee.DateOfVacation.HasValue)
                quaterValue.DateOfVacation = allottee.DateOfVacation;
            if (allottee.DateOfRetensionUpto.HasValue)
                quaterValue.DateOfRetensionUpto = allottee.DateOfRetensionUpto;
            if (!string.IsNullOrEmpty(allottee.Remarks))
                quaterValue.Remarks = allottee.Remarks;
            if (!string.IsNullOrEmpty(allottee.RetentionReason))
                quaterValue.RetentionReason = allottee.RetentionReason;
            if (!string.IsNullOrEmpty(allottee.QuarterNumber))
                quaterValue.QuarterNumber = allottee.QuarterNumber;
            
            quaterValue.Status = allottee.Status.Value;
            datacontext.SubmitChanges();
           
        }       
    }
    public static tblAllottee getAllotteByApplicationid(long id)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varQuarters = from quarterInfo in datacontext.tblAllottees where quarterInfo.ApplicationId == id select quarterInfo;
        return varQuarters.FirstOrDefault();
        
    }
    public static List<uspGetAllotteResult> GetRetentedAllotte(long category)
    {
        List<uspGetAllotteResult> _lst = new List<uspGetAllotteResult>();
        DataClassesDataContext datacontext = new DataClassesDataContext();
        long? j = (long)AllotementStatus.Retension;
        //var varAllottees = from allotte in datacontext.tblAllottees select new AllotementInformattion { Id = allotte.Id, Name = allotte.tblUser.fullName, AAN = allotte.AAN, Office = allotte.tblUser.tblOffice.Name, QuarterNumber = allotte.QuarterNumber };
        var varAllottees = datacontext.uspGetAllotte(j, category);
        List<uspGetAllotteResult> _lstPossed = varAllottees.ToList();
        if (_lstPossed.Count > 0)
            _lst.AddRange(_lstPossed);
        return _lst;
    }
}
