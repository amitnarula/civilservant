using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OTP
/// </summary>
public class OTP
{
	public OTP()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void SaveOTP(string aan,string phoneNmber,string otp) {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            dc.tblOTPs.InsertOnSubmit(new tblOTP() { 
                AAN=aan,
                GeneratedOn=DateTime.Now,
                PhoneNumber=phoneNmber,
                OTP = otp
            });
            dc.SubmitChanges();
        }
    
    }

    public static tblOTP GetOTP(string aan) {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            return dc.tblOTPs.SingleOrDefault(x => x.AAN == aan);
        }
    }

    public static void DeleteAllInvalidOTPs(string aan) {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            var invalidOtps = dc.tblOTPs.Where(x => x.AAN == aan && x.GeneratedOn.AddMinutes(5) <= DateTime.Now).ToList();
            if (invalidOtps != null)
            {
                dc.tblOTPs.DeleteAllOnSubmit(invalidOtps);
                dc.SubmitChanges();
            }
        }
    }

    public static bool IsOTPValid(string otp,string aan) {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            var otpInDB = dc.tblOTPs.SingleOrDefault(x => x.OTP == otp && x.AAN == aan);

            if (otpInDB != null)
            {
                if (otpInDB.GeneratedOn.AddMinutes(5) >= DateTime.Now)
                {
                    return true;
                }
            }
            else return false;
        }
        return false;
    }

}