using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
public partial class Admin_UploadImage : System.Web.UI.Page
{
    tblUser _user;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmessage.Visible = true;
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
        }
    }
    protected void btnUpload_click(object sender, EventArgs e)
    {
        //if (Page.IsValid)
        {
            string filepath = Server.MapPath("~/Images/photogallery");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            filepath += "/" + fileImage.FileName;
            fileImage.SaveAs(filepath);
            tblImage newimage = new tblImage();
            newimage.title = txtTitle.Text;
            newimage.CreatedDate = DateTime.Now;
            newimage.CreatedBy = HttpContext.Current.User.Identity.Name;
            newimage.Imagepath = "~/Images/photogallery/" + fileImage.FileName;
            tblPhotos.Save(newimage);
            lblmessage.Text = "Information has been saved sucessfully!";
            lblmessage.Visible = true;

            tbluserhistory _userhistory = new tbluserhistory();
            _userhistory.Action = "User creation";
            _userhistory.description = _user.Username + " has created user with userid " + _user.Id;
            _userhistory.time = DateTime.Now;
            _userhistory.useraan = _user.AAN;
            userHistory.Save(_userhistory);

        }
        //else
        //{
        //    lblmessage.Text = "Only jpeg,gif and png allowed!";
        //    lblmessage.Visible = true;

        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/default.aspx");
    }
}
