using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LicenceFee
/// </summary>
public class LicenceFee
{
	public LicenceFee()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void Save(tblQuarterCategoryFee licFee)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = from iLicenceFee in dataContext.tblQuarterCategoryFees where iLicenceFee.QuarterCategoryId == licFee.QuarterCategoryId select iLicenceFee;
        tblQuarterCategoryFee oLicenceFee = varlicFee.FirstOrDefault();
        if (oLicenceFee != null)
        {
            oLicenceFee.EffectiveDate = licFee.EffectiveDate;
            oLicenceFee.QuarterCategoryId = licFee.QuarterCategoryId;
            //oLicenceFee.Month = licFee.Month;
            oLicenceFee.LicenceFee = licFee.LicenceFee;
            //oLicenceFee.LastUpdatedBy = licFee.LastUpdatedBy;
            //oLicenceFee.LastUpdatedDate = DateTime.Now;
        }
        if (oLicenceFee == null)
        {
            dataContext.tblQuarterCategoryFees.InsertOnSubmit(licFee);
            
        }

        dataContext.SubmitChanges();
    }

    public static tblQuarterCategoryFee GetQuarterCategeryFee(long category)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = from iLicenceFee in dataContext.tblQuarterCategoryFees where iLicenceFee.QuarterCategoryId == category select iLicenceFee;
        return varlicFee.FirstOrDefault();
    }
    public static void SaveMonthLicencefee(tbQuarterLicenceFee licFee)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = from iLicenceFee in dataContext.tbQuarterLicenceFees where iLicenceFee.Month.Value.Month== licFee.Month.Value.Month select iLicenceFee;
        tbQuarterLicenceFee oLicenceFee = varlicFee.FirstOrDefault();
        if (oLicenceFee != null)
        {
            oLicenceFee.AAN = licFee.AAN;
            oLicenceFee.Month = licFee.Month;
            oLicenceFee.Fee =oLicenceFee.Fee+ licFee.Fee;
            oLicenceFee.QuarterId = licFee.QuarterId;
            oLicenceFee.ActualFee = licFee.ActualFee;
            oLicenceFee.Remarks = licFee.Remarks;

        }
        if (oLicenceFee == null)
        {
            dataContext.tbQuarterLicenceFees.InsertOnSubmit(licFee);

        }

        dataContext.SubmitChanges();
    }

    public static decimal GetLicenceFeebyCategory(long category)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varlicFee = from iLicenceFee in dataContext.tblQuarterCategoryFees where iLicenceFee.QuarterCategoryId== category select iLicenceFee;
        tblQuarterCategoryFee  val = varlicFee.FirstOrDefault();
        if (val != null && val.LicenceFee.HasValue)
            return Convert.ToDecimal(val.LicenceFee);
        return 0;        
    }
    public static List<uspGetLicenceFeeResult> GetLicencefee(string QuarterNumber)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        
         return dataContext.uspGetLicenceFee(QuarterNumber).ToList();
    }

  
}
