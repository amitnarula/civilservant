using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Images : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        if (!Page.IsPostBack)
        {
            Bind();
        }
    }
    private void Bind()
    {
        grdQuarters.DataSource = tblPhotos.GetPhotos();
        grdQuarters.DataBind();
    }
    protected void gridCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteImage")
        {
            long id;
            Int64.TryParse(e.CommandArgument.ToString(), out id);
            tblPhotos.Delete(id);
            lblMessage.Text = "Information has been saved sucessfully!";
            lblMessage.Visible = true;
            Bind();

        }
    }
}
