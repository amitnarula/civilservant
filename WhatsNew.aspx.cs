using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WhatsNew : System.Web.UI.Page
{
    private void BindCirculars()
    {
        

        rptCirculars.DataSource = WhatsNewLogic.LoadContent();
        rptCirculars.DataBind();
    }

    private void DownloadFile(byte[] fileContent, string fileName)
    {
        try
        {

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.OutputStream.Write(fileContent, 0, fileContent.Length);
            Response.Flush();
        }
        catch (Exception ex)
        {
            // An error occurred.. 
        }
    }

    
    private string ResolveCSSClass(string fileExtension)
    {
        string cssClass = string.Empty;
        switch (fileExtension)
        {
            case "docx":
            case "doc":
                cssClass = "document-image";
                break;
            case "pdf":
                cssClass="pdf-image";
                break;
            case "xlsx":
            case "xls":
                cssClass = "xl-image";
                break;
            case "jpg":
            case "png":
            case "gif":
            case "jpeg":
                cssClass = "img-image";
                break;
            default:
                break;
        }
        return cssClass;
    }

    protected void Page_Init(object sender, EventArgs e){
        rptCirculars.ItemCommand+=new RepeaterCommandEventHandler(rptCirculars_ItemCommand);
        rptCirculars.ItemDataBound += new RepeaterItemEventHandler(rptCirculars_ItemDataBound);
    }

    void rptCirculars_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string cssClass = string.Empty;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
            var item = e.Item.DataItem as tblWhatsNew;
            var fileExtension = item.Name.Substring(item.Name.LastIndexOf(".") + 1);

            cssClass = ResolveCSSClass(fileExtension);

            ((Literal)e.Item.FindControl("litType")).Text = string.Format("<span class='span-img {0}'></span>", cssClass);
        }
    }

    void rptCirculars_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "downloadFile") { 
            var file = WhatsNewLogic.LoadFile(id);
            DownloadFile(file.ContentFile.ToArray(), file.Name);            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindCirculars();   
        }
    }
}