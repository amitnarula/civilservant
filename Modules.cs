using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Modules
/// </summary>
public class tblModules
{
    public tblModules()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static List<tblRoleRModule> GetRolePermission(int roleid)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var modules = from per in datacontext.tblRoleRModules where per.Roleid == roleid select per;
        return modules.ToList();
    }
    public static tblRoleRModule GetRolePermission(int roleid,string modulename)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var modules = from per in datacontext.tblRoleRModules where per.Roleid == roleid && per.tblModule.Name.ToLower()==modulename.ToLower() select per;
        return modules.FirstOrDefault();
    }
    public static List<tblRoleRModule> GetRolePermission(int roleid,int moduleid)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var modules = from per in datacontext.tblRoleRModules where per.Roleid == roleid select per;
        return modules.ToList();
    }
    public static List<tblModule> GetAllModules()
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var modules = from module in datacontext.tblModules select module;
        return modules.ToList();
    }
    public static void SaveModulePermission(int roleid,List<tblRoleRModule> inpermissions)
    {
        DataClassesDataContext datacontext = new DataClassesDataContext();
        var permissions = from per in datacontext.tblRoleRModules where per.Roleid == roleid select per;
        datacontext.tblRoleRModules.DeleteAllOnSubmit(permissions);
        datacontext.SubmitChanges();
        datacontext.tblRoleRModules.InsertAllOnSubmit(inpermissions);
        datacontext.SubmitChanges();
        
    }
}
