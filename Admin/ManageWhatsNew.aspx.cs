using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class ManageWhatsNew : System.Web.UI.Page
{

    private void DownloadFile(byte[] fileContent,string fileName) {
        try
        {

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename="+fileName);
            Response.OutputStream.Write(fileContent, 0, fileContent.Length);
            Response.Flush();
        }
        catch (Exception ex)
        {
            // An error occurred.. 
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        btnCancel.Click += new EventHandler(btnCancel_Click);
        btnSave.Click += new EventHandler(btnSave_Click);
        rptWhatsNew.ItemCommand += new RepeaterCommandEventHandler(rptWhatsNew_ItemCommand);
        cusValidateFile.ServerValidate += new ServerValidateEventHandler(cusValidateFile_ServerValidate);
        
    }

    void cusValidateFile_ServerValidate(object source, ServerValidateEventArgs e)
    {
        //System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
        //int height = img.Height;
        //int width = img.Width;
        decimal size = Math.Round(((decimal)fileUpload.PostedFile.ContentLength / (decimal)1024), 2);
        string fileName = fileUpload.PostedFile.FileName;
        string fileExtension = fileName.Substring(fileName.LastIndexOf(".")+1);
        string[] validFileTypes = { "bmp", "gif", "png","jpg", "jpeg", "doc","docx","pdf","xls","xlsx" };

        if (size > 4096)
        {
            cusValidateFile.ErrorMessage = "File size must not exceed 4 MB.";
            e.IsValid = false;
        }
        if (!validFileTypes.Contains(fileExtension))
        {
            cusValidateFile.ErrorMessage = "Valid file types are JPG,PNG,GIF,DOC,DOCX,PDF,XLS,XLSX";
            e.IsValid = false;
        }
        //if (height > 100 || width > 100)
        //{
        //    CustomValidator1.ErrorMessage = "Height and Width must not exceed 100px.";
        //    e.IsValid = false;
        //}
    }

    void rptWhatsNew_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "deleteContent")
        {
            
            WhatsNewLogic.DeleteContent(id);
            litMessage.Text = "Content deleted successfully.";
            BindData();
            ClearForm();
        }
        else if (e.CommandName == "previewFile")
        {
            var file = WhatsNewLogic.LoadFile(id);
            byte[] content = file.ContentFile.ToArray();
            string fileName = file.Name;
            DownloadFile(content, fileName);

        }
        
    }

    void btnSave_Click(object sender, EventArgs e)
    {
        if (cusValidateFile.IsValid)
        {
            //Read the uploaded File as Byte Array from FileUpload control.
            Stream fs = fileUpload.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(fs);
            byte[] content = br.ReadBytes((Int32)fs.Length);

            WhatsNewLogic.SaveContent(txtDescription.Text,
                DateTime.Parse(txtDate.Text),
                content,
                fileUpload.PostedFile.FileName);

            litMessage.Text = "Content saved successfully.";

            BindData();
            ClearForm();
        }

    }

    private void ValidateFile()
    {
        string[] validFileTypes = { "bmp", "gif", "png","jpg", "jpeg", "doc","pdf","xls","xlsx" };

        string ext = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);

        bool isValidFile = false;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + validFileTypes[i])
            {

                isValidFile = true;

                break;

            }

        }

        if (!isValidFile)
        {

            litMessage.Text = "Please upload a file with extension" + string.Join(",", validFileTypes);

        }

        //else
        //{

        //    Label1.ForeColor = System.Drawing.Color.Green;

        //    Label1.Text = "File uploaded successfully.";

        //}
    }

    private void BindData()
    {
        var files = WhatsNewLogic.LoadContent();

        rptWhatsNew.DataSource = files;
        rptWhatsNew.DataBind();

    }

    void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        txtDate.Text = string.Empty;
        txtDescription.Text = string.Empty;
        fileUpload.Attributes.Clear();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            ClearForm();
        }
    }
}