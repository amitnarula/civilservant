using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Roles
/// </summary>
public class Roles
{
	public Roles()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static List<tblRole> GetRoles()
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varRoles = from role in dataContext.tblRoles select role;
        return varRoles.ToList();
    }
}
