using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for Users
/// </summary>
public class Users
{
	public Users()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet Select()
    {
        //DataClassesDataContext db = new DataClassesDataContext();
        //return db.tblUsers.Where(x=>!x.bIsDeleted.HasValue||!x.bIsDeleted.Value);

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EMSConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter("select * from tblusers", con);
        DataSet res = new DataSet();
        sda.Fill(res);
        return res;
    }

    public static DataSet SelectPage(int startRowIndex, int maximumRows)
    {
        //return null;
        return Select();
    }
    public static DataSet SelectPage()
    {
        //return null;
        return Select();
    }
    public static int SelectCount()
    {
        return Select().Tables[0].Rows.Count;
;
    }
    public static List<tblUser> getAllUsers()
    {
       
        DataClassesDataContext dataContext=new DataClassesDataContext();

        List<tblUser> users = new List<tblUser>();
                var varUsers=from user in dataContext.tblUsers select user;
        users = varUsers.ToList();
   
        return users;
    }
    public static tblUser getUser(long id)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where user.Id==id select user;
        return varUsers.FirstOrDefault();
    }
    public static void SaveUser(tblUser user)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == user.Id select userinfo;
        tblUser oUser = varUser.FirstOrDefault();
        if (oUser != null) {
            oUser.fullName = user.fullName;
            oUser.EmailID = user.EmailID;
            oUser.DateOfJoining = user.DateOfJoining;
            oUser.DateOfRetirement = user.DateOfRetirement;
            oUser.Addressline1 = user.Addressline1;
            oUser.BaseOfficeId = user.BaseOfficeId;
            oUser.Roleid = user.Roleid;
            oUser.EmployeeCode = user.EmployeeCode;
            oUser.AadharNumber = user.AadharNumber;
            oUser.PanNumber = user.PanNumber;
            if (!string.IsNullOrEmpty(user.Password))
            {
                oUser.Password = user.Password;
                oUser.IsPasswordChanged = true;
            }
        }
        if (user.Id <= 0)
        {
            dataContext.tblUsers.InsertOnSubmit(user);
            dataContext.SubmitChanges();
            //user.AAN =Offices.GetOfficeCode(user.BaseOfficeId.Value)+ user.Id ;
        }

        dataContext.SubmitChanges();

    }
    public static void Delete(long userid)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == userid select userinfo;
        tblUser oUser = varUser.FirstOrDefault();
        if (oUser != null)
        {
            oUser.bIsDeleted = true;
            //dataContext.tblUsers.DeleteOnSubmit(oUser);
        }
        dataContext.SubmitChanges();
    }

    public static void Debar(long userId,DateTime debarUntil)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == userId select userinfo;
        tblUser oUser = varUser.FirstOrDefault();
        if (oUser != null)
        {
            oUser.IsDebarred = true;
            oUser.DebarUntil = debarUntil;
            //dataContext.tblUsers.DeleteOnSubmit(oUser);
        }
        dataContext.SubmitChanges();  
    }

    public static void Defer(long userId,DateTime deferUntil)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == userId select userinfo;
        tblUser oUser = varUser.FirstOrDefault();
        if (oUser != null)
        {
            oUser.IsDefferred = true;
            oUser.DeferUntil = deferUntil;
            //dataContext.tblUsers.DeleteOnSubmit(oUser);
        }
        dataContext.SubmitChanges();
    }

    public static void ReactivateUser(long userId, bool removeBar,bool removeDefer)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == userId select userinfo;
        tblUser oUser = varUser.FirstOrDefault();
        if (oUser != null)
        {
            if (removeBar)
            {
                oUser.IsDebarred = false;
                oUser.DebarUntil = null;

            }
            if (removeDefer)
            {
                oUser.IsDefferred = false;
                oUser.DeferUntil = null;
            }

            //dataContext.tblUsers.DeleteOnSubmit(oUser);
        }
        dataContext.SubmitChanges();
    }

    public static bool IsUserDebarred(long userId,out DateTime? debarUntil)
    {

        debarUntil = DateTime.Now;

        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == userId select userinfo;
        tblUser oUser = varUser.FirstOrDefault();

        if (oUser != null)
        {
            if (oUser.IsDebarred)
            {
                debarUntil = oUser.DebarUntil;
                return true;
            }
            else
            {
                debarUntil = null;
                return false;
            }
        }
        return false;
    }

    public static bool IsUserDeferred(long userId, out DateTime? deferUntil)
    {

        deferUntil = DateTime.Now;

        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUser = from userinfo in dataContext.tblUsers where userinfo.Id == userId select userinfo;
        tblUser oUser = varUser.FirstOrDefault();

        if (oUser != null)
        {
            if (oUser.IsDefferred)
            {
                deferUntil = oUser.DebarUntil;
                return true;
            }
            else
            {
                deferUntil = null;
                return false;
            }
        }
        return false;
    }

    public static bool ValidateUser(string userName,string password)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where (!user.bIsDeleted.HasValue || !user.bIsDeleted.Value) && user.Username.ToLower() == userName.ToLower() && user.Password == password select user;
        
        return varUsers.FirstOrDefault()==null?false:true;
    }

    public static bool ValidateUserByAAN(string userName, string password, out tblUser userOut)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where (!user.bIsDeleted.HasValue || !user.bIsDeleted.Value) && 
                           user.AAN.ToLower() == userName.ToLower() && 
                           user.Password == password select user;

        userOut = varUsers.FirstOrDefault();

        return varUsers.FirstOrDefault() == null ? false : true;
    }

    public static tblUser getUserByAAN(string aan)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where user.AAN ==aan  select user;
        return varUsers.FirstOrDefault();
    }
    public static tblUser getUserByUserName(string username)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where user.Username.ToLower() == username.ToLower() select user;
        return varUsers.FirstOrDefault();
    }
    public static bool CheckEmail(string userEmail)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where user.EmailID.ToLower() == userEmail.ToLower() select user;

        return varUsers.FirstOrDefault() == null ? false : true;
    }
    public static tblUser getUserEmail(string userEmail)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        var varUsers = from user in dataContext.tblUsers where user.EmailID.ToLower() == userEmail.ToLower() select user;

        return varUsers.FirstOrDefault();
    }
    public static string GetUserDesignation(long userId)
    {
        DataClassesDataContext dataContext = new DataClassesDataContext();
        string designation = dataContext.tblUsers.Where(u => u.Id == userId).FirstOrDefault().tblDesignation.Name;
        return designation;

    }
    
}
