using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
public partial class photogallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder str = new StringBuilder();
        List<tblImage> images = tblPhotos.GetPhotos();
        foreach (tblImage img in images)
        {
            str.Append(" <img src='"+ResolveUrl(img.Imagepath)+"'>");
        }
        galleria.InnerHtml=str.ToString();
    }
}
