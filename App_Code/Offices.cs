using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Offices
/// </summary>
public class Offices
{
	public Offices()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static List<tblOffice> GetAlloffices()
    {
         DataClassesDataContext dataContext=new DataClassesDataContext();
    
        var varoffices = from office in dataContext.tblOffices select office;
        return varoffices.ToList();
    }
    public static string GetOfficeName(int id)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();

        var varoffices = from office in dataContext.tblOffices where office.Id==id select office;
        return varoffices.FirstOrDefault() == null ? "" : varoffices.FirstOrDefault().Name;
    }
    public static string GetOfficeCode(int id)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varoffices = from office in dataContext.tblOffices where office.Id == id select office;
        return varoffices.FirstOrDefault() == null ? "" : varoffices.FirstOrDefault().officeCode;
    }
}
