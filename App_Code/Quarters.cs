using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Quarters
/// </summary>
public class Quarters
{
	public Quarters()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Save(tblQuarter quater)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varQuarters = from quarterInfo in datacontext.tblQuarters where quarterInfo.Id == quater.Id select quarterInfo;
        tblQuarter quaterValue = varQuarters.FirstOrDefault();
        if (quaterValue == null)
        {
            datacontext.tblQuarters.InsertOnSubmit(quater);
        }
        else
        {
            quaterValue.Floor = quater.Floor;
            quaterValue.Category = quater.Category;
            quaterValue.QuarterNumber = quater.QuarterNumber;
            quaterValue.DateOfAllottement = quater.DateOfAllottement;
            quaterValue.DateOfVacation = quater.DateOfVacation;
            quaterValue.Status = quater.Status;
        }
        datacontext.SubmitChanges();
    }
    public static List<tblQuarterCategory> GetQuarterCategory()
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varCategories = from quarterCategory in datacontext.tblQuarterCategories select quarterCategory;
        return varCategories.ToList();
    }

    public static bool IsQuarterCategoryActive(long categoryId)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var category = datacontext.tblQuarterCategories.Where(_ => _.Id == categoryId).SingleOrDefault();

        if (category != null)
            return category.IsActive;

        return false;
    
    }

    public static List<tblQuarter> GetQuartersByQuarterNumber(string quarterNumber)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        return datacontext.tblQuarters.Where(_ => _.QuarterNumber==quarterNumber).ToList();
    }

    public static List<tblQuarterFloor> GetQuarterFloors()
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varFloors= from quarterCategory in datacontext.tblQuarterFloors select quarterCategory;
        return varFloors.ToList();
    }
    public static tblQuarter GetQuarter(int id)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = from quarterCategory in datacontext.tblQuarters select quarterCategory;
        return varquarter.FirstOrDefault();
    }
    public static List<Quarerinfo> GetAllQuarters()
    {
        
        DataClassesDataContext datacontext = new DataClassesDataContext();
    
        var varquarter = from quarter in datacontext.tblQuarters select new Quarerinfo {CategoryName=quarter.tblQuarterCategory.Name, FloorName=quarter.tblQuarterFloor.Name, Id=quarter.Id,QuarterNumber=quarter.QuarterNumber};
        return varquarter.ToList();
    }

    public static List<tblQuarter> GetQuarters()
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        return datacontext.tblQuarters.ToList();
    }

    public static List<tblChangeRequest> GetChangeRequests()
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        return datacontext.tblChangeRequests.ToList();
    }

    public static List<uspGetQuarterResult> GetAllQuarters(long categroyid)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        long? j = null;
        if(categroyid>0)
            j=categroyid;
        var varquarter = datacontext.uspGetQuarter(j).ToList();
        uspGetQuarterResult p = new uspGetQuarterResult();
        
        
        return varquarter;
    }
    public static string GetQuarterCategoryName(long id)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varCategories = from quarterCategory in datacontext.tblQuarterCategories where quarterCategory.Id==id select quarterCategory;
        return varCategories.FirstOrDefault() == null ? "" : varCategories.FirstOrDefault().Name;
    }
    public static string GetQuarterFloorsName(long id)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varFloors = from quarterCategory in datacontext.tblQuarterFloors where quarterCategory.Id == id select quarterCategory;
        return varFloors.FirstOrDefault() == null ? "" : varFloors.FirstOrDefault().Name;
    }
    public static void UpdateQuarterStatus(long id,QuarterStatus status)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = from quarterCategory in datacontext.tblQuarters where quarterCategory.Id==id select quarterCategory;
        tblQuarter quarter = varquarter.FirstOrDefault();
        quarter.Status = (int)status;
        datacontext.SubmitChanges();
    }

    public static void UpdateQuarterStatus(long id, int status, string remarks, string dateOfVacation)
    {
        DateTime dateOfVacationParsed = DateTime.Now;
        

        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = from quarterCategory in datacontext.tblQuarters where quarterCategory.Id == id select quarterCategory;
        tblQuarter quarter = varquarter.FirstOrDefault();
        quarter.Status = status;
        quarter.Remarks = remarks;
        if (!string.IsNullOrEmpty(dateOfVacation) && DateTime.TryParse(dateOfVacation, out dateOfVacationParsed))
        {
            quarter.DateOfVacation = dateOfVacationParsed;
        }

        //if status is surrendered, delete the change request against this quarter
        var quarterStatus = (QuarterStatus)status;
        if (quarterStatus == QuarterStatus.Surrender)
        {
            var changeRequests = datacontext
                .tblChangeRequests
                .Where(x => x.QuarterNumber == quarter.QuarterNumber)
                .ToList();
            if (changeRequests.Any())
                changeRequests.ForEach((x) =>
                {
                    x.Status = (int)ChangeRequestStatus.Deleted;
                });

            //datacontext.tblChangeRequests.DeleteAllOnSubmit(changeRequests);
        }
        
        datacontext.SubmitChanges();
    }

    public static void UpdateQuarterStatus(string quarterNumber, QuarterStatus status)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = from quarterCategory in datacontext.tblQuarters where quarterCategory.QuarterNumber.ToLower()==quarterNumber.ToLower() select quarterCategory;
        tblQuarter quarter = varquarter.FirstOrDefault();
        quarter.Status = (int)status;
        datacontext.SubmitChanges();
        
    }

    public static List<uspQuarterHistoryResult> GetQuarterHistroy(string quarterNumber)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = datacontext.uspQuarterHistory(quarterNumber);
        return varquarter.ToList();
    }
    public static tblQuarter GetQuarter(string quarterNumber)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = from quarter in datacontext.tblQuarters where quarter.QuarterNumber.ToLower() == quarterNumber.ToLower() select quarter;
        return varquarter.FirstOrDefault();
    }

    public static List<Quarerinfo> GetQuartersByChangeRequestId(long id)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var changeRequest = datacontext.tblChangeRequests.Where(creq => creq.Id == id).SingleOrDefault();

        List<Quarerinfo> lstQuarterInfos = new List<Quarerinfo>();

        if (!string.IsNullOrEmpty(changeRequest.FirstPerference))
        {
            Quarerinfo qInfo = new Quarerinfo();
            qInfo.QuarterNumber = changeRequest.FirstPerference;
            lstQuarterInfos.Add(qInfo);
        }
        if (!string.IsNullOrEmpty(changeRequest.SecondPerference))
        {
            Quarerinfo qInfo = new Quarerinfo();
            qInfo.QuarterNumber = changeRequest.SecondPerference;
            if (!lstQuarterInfos.Any(i => i.QuarterNumber == qInfo.QuarterNumber))
                lstQuarterInfos.Add(qInfo);
        }
        if (!string.IsNullOrEmpty(changeRequest.ThirdPerference))
        {
            Quarerinfo qInfo = new Quarerinfo();
            qInfo.QuarterNumber = changeRequest.ThirdPerference;
            if (!lstQuarterInfos.Any(i => i.QuarterNumber == qInfo.QuarterNumber))
                lstQuarterInfos.Add(qInfo);
        }

        return lstQuarterInfos.ToList();
    }

    public static List<Quarerinfo> GetAllVacantQuarters(long category)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        List<Quarerinfo> lstQuarterInfo;

            lstQuarterInfo = (from quarter in datacontext.tblQuarters
                              orderby quarter.DateOfVacation descending
                              where (quarter.Status.HasValue == false || quarter.Status == (int)QuarterStatus.Vacant)
                              && quarter.Category == category
                              select new Quarerinfo
                              {
                                  CategoryName = quarter.tblQuarterCategory.Name,
                                  FloorName = quarter.tblQuarterFloor.Name,
                                  Id = quarter.Id,
                                  QuarterNumber = quarter.QuarterNumber,
                                  DateOfAllottement = quarter.DateOfAllottement.Value,
                                  DateOfVacation = quarter.DateOfVacation.Value
                              }).ToList();


            return lstQuarterInfo;
    }
    public static void AddQuarterDamage(tbquarterDamage quarterDamage)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        datacontext.tbquarterDamages.InsertOnSubmit(quarterDamage);
        datacontext.SubmitChanges();
    }
    public static void AddChallan(tbChallan quarterDamage)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        datacontext.tbChallans.InsertOnSubmit(quarterDamage);
        datacontext.SubmitChanges();
    }
    public static List<Quarerinfo> GetAllAllotedQuarters(Int64 quarterType)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var varquarter = from quarter in datacontext.tblQuarters where ((quarter.Category == quarterType) && (quarter.Status.HasValue == false || quarter.Status == (int)QuarterStatus.Alloted || quarter.Status == (int)QuarterStatus.Vacant)) select new Quarerinfo { CategoryName = quarter.tblQuarterCategory.Name, FloorName = quarter.tblQuarterFloor.Name, Id = quarter.Id, QuarterNumber = quarter.QuarterNumber, DateOfAllottement = quarter.DateOfAllottement.Value, DateOfVacation = quarter.DateOfVacation.Value };
        return varquarter.ToList();
    }
}
