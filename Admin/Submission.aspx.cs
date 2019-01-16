using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_Submission : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
        DataTable dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (!Convert.ToBoolean(dt.Rows[0][0]))
            {
                lblmessage.Text = "Application submission is disabled";
                lnkSbmission.Text = "Enable Submission";
                lnkSbmission.CommandName = "Enable";
            }
            else
            {
                lblmessage.Text = "Application submission is enabled";
                lnkSbmission.Text = "Disable Submission";
                lnkSbmission.CommandName = "Disable";
            }
        }
    }
    protected void lnkSbmission_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("~/submissionVerfication.xml"));
        DataTable dt = ds.Tables[0];

        if (lnkSbmission.CommandName == "Enable")
        {
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0][0] = true;
                lblmessage.Text = "Application submission is enabled";
                lnkSbmission.Text = "Disable Submission";
            }
        }
        else
        {
            if (dt.Rows.Count > 0)
            {
                lblmessage.Text = "Application submission is disabled";
                dt.Rows[0][0] = false;
                lnkSbmission.Text = "Enable Submission";
            }
        }
        ds.WriteXml(Server.MapPath("~/submissionVerfication.xml"));
        ds.Dispose();
    }
}
