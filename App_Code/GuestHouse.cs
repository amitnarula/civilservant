using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntegratedMessages;

/// <summary>
/// Summary description for GuestHouse
/// </summary>
public class GuestHouseRequest
{
    private static decimal CalculateBalanceDueForRoom(tblGuestHouse guestHouse)
    {
        string purposeOfVisit = guestHouse.PurposeOfVisit;
        decimal perPersonPrice = 0;

        if (purposeOfVisit == "On Duty")
            perPersonPrice = 0;
        else if (purposeOfVisit == "On Transfer" || purposeOfVisit == "Not on Duty" ||
            purposeOfVisit == "Retired Person")
        {
            if (guestHouse.RoomName == "Chinab")
                perPersonPrice = 300;
            else
                perPersonPrice = 200;

        }
        else if (purposeOfVisit == "Near relation of IA&AD personnel" ||
            purposeOfVisit == "Near relation of other Government Department")
        {
            if (guestHouse.RoomName == "Chinab")
                perPersonPrice = 600;
            else
                perPersonPrice = 400;
        }
        return perPersonPrice;

    }

    private static decimal CalculateBalanceDue(tblGuestHouse guestHouse)
    { 
        string purposeOfVisit = guestHouse.PurposeOfVisit;
        int typeOfAccomodation = guestHouse.TypeOfAccomodation.Value;
        decimal perPersonPrice = 0;

        if(purposeOfVisit=="On Duty")
            perPersonPrice = 0;
        else if(purposeOfVisit=="On Transfer" || purposeOfVisit=="Not on Duty" || 
            purposeOfVisit=="Retired Person")
        {
            if(typeOfAccomodation==1)
                perPersonPrice = 50;
            else if(typeOfAccomodation==2)
                perPersonPrice = 100;
            else if(typeOfAccomodation==3)
                perPersonPrice = 200;
            else if(typeOfAccomodation==4)
                perPersonPrice = 300;

        }
        else if (purposeOfVisit == "Near relation of IA&AD personnel" || 
            purposeOfVisit == "Near relation of other Government Department")
        {
            if (typeOfAccomodation == 1)
                perPersonPrice = 100;
            else if (typeOfAccomodation == 2)
                perPersonPrice = 200;
            else if (typeOfAccomodation == 3)
                perPersonPrice = 400;
            else if (typeOfAccomodation == 4)
                perPersonPrice = 600;
        }
        return perPersonPrice;

    }

    public enum GuestHouseRequestStatus
    { 
        Pending=0,
        Allotted=1,
    }

    public GuestHouseRequest()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static void SaveGuestHouseRequest(tblGuestHouse request)
    {
        DataClassesDataContext dc = new DataClassesDataContext();

        tblGuestHouse guestHouse = new tblGuestHouse();
        guestHouse.DateFrom = request.DateFrom;
        guestHouse.DateTo = request.DateTo;
        guestHouse.Designation = request.Designation;
        guestHouse.EmailID = request.EmailID;
        guestHouse.GuestHouse = request.GuestHouse;
        guestHouse.IsEmployee = request.IsEmployee;
        guestHouse.IsStaff = request.IsStaff;
        guestHouse.Name = request.Name;
        guestHouse.Notes = request.Notes;
        guestHouse.PlaceOfHosting = request.PlaceOfHosting;
        guestHouse.PurposeOfVisit = request.PurposeOfVisit;
        guestHouse.Status = (int)GuestHouseRequestStatus.Pending;
        guestHouse.MobileNumber = request.MobileNumber;
        guestHouse.NumberOfRooms = request.NumberOfRooms;
        guestHouse.NumberOfPeople = request.NumberOfPeople;
        guestHouse.IsSelf = request.IsSelf;
        guestHouse.IsGuest = request.IsGuest;
        guestHouse.OfficeID = request.OfficeID;
        //guestHouse.TypeOfAccomodation = request.TypeOfAccomodation;

        dc.tblGuestHouses.InsertOnSubmit(guestHouse);
        dc.SubmitChanges();
        
    }

    public static void MarkPaid(int requestID)
    {
        DataClassesDataContext dc = new DataClassesDataContext();
        tblGuestHouse guestHouse = dc.tblGuestHouses.Where(x => x.ID == requestID).SingleOrDefault();

        guestHouse.IsPaid = true;
        dc.SubmitChanges();
    }

    public static void AllotGuestHouse(int requestID, string roomName)
    {
        DataClassesDataContext dc = new DataClassesDataContext();
        tblGuestHouse guestHouse = dc.tblGuestHouses.Where(x => x.ID == requestID).SingleOrDefault();

        guestHouse.RoomName = roomName;

        //Calculate balance due as per norms and update the room accordingly
        guestHouse.BalanceDue = CalculateBalanceDueForRoom(guestHouse);
        
        guestHouse.Status = (int)GuestHouseRequestStatus.Allotted;

        //SEND MESSAGE
        try
        {
            new IntegratedMessageSender().SendMessage("GUEST_HOUSE_BOOKING", guestHouse.GuestHouse, guestHouse.MobileNumber);
        }
        catch (Exception)
        {

        }

        dc.SubmitChanges();
        
    }

    public static bool IsGuestHouseAvailable(string guestHouse, DateTime from ,DateTime to)
    {
        DataClassesDataContext dc = new DataClassesDataContext();
        var result = dc.tblGuestHouses.Where(x => x.GuestHouse == guestHouse &&
            x.DateTo >= to &&
            x.DateFrom <= from &&
            x.Status == (int)GuestHouseRequestStatus.Allotted);

        if (result.Any())
            return false;
        return true;

    }

    public static void DeleteRequest(int requestID) {
        DataClassesDataContext dc = new DataClassesDataContext();
        tblGuestHouse guestHouse = dc.tblGuestHouses.Where(x => x.ID == requestID).SingleOrDefault();

        dc.tblGuestHouses.DeleteOnSubmit(guestHouse);
        dc.SubmitChanges();
    }

    public static List<tblGuestHouse> GetGuestHouseRequestByStatus(int status)
    {
        DataClassesDataContext dc = new DataClassesDataContext();

        return dc.tblGuestHouses.Where(x => x.Status == status).OrderByDescending(x => x.DateTo).ToList();
    }
}