using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WhatsNew
/// </summary>
public class WhatsNewLogic
{

    public static void SaveContent(string description, DateTime date, byte[] content,string fileName) {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            dc.tblWhatsNews.InsertOnSubmit(new tblWhatsNew()
            {
                Date = date,
                Description = description,
                ContentFile = content,
                Name=fileName
            });

            dc.SubmitChanges();
        }
    }

    public static List<tblWhatsNew> LoadContent() {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            return dc.tblWhatsNews.OrderByDescending(x=>x.Date).ToList();
        }
    }

    public static tblWhatsNew LoadFile(int id) {
        using (DataClassesDataContext dc = new DataClassesDataContext()) {
            return dc.tblWhatsNews.SingleOrDefault(x => x.Id == id);
        }
    }

    public static void DeleteContent(int id){
        using (DataClassesDataContext dc = new DataClassesDataContext()) { 
            var entity = dc.tblWhatsNews.SingleOrDefault(x=>x.Id == id);
            if (entity != null)
            {
                dc.tblWhatsNews.DeleteOnSubmit(entity);
                dc.SubmitChanges();
            }
        }  
    }

}