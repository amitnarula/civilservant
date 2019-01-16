using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Roles : System.Web.UI.Page
{
    tblUser _user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            _user = Users.getUserByUserName(HttpContext.Current.User.Identity.Name);
            tblRoleRModule m = tblModules.GetRolePermission(_user.Roleid.Value, "User management");
            if (m == null)
            {
                Response.Redirect("~/unauthorize.aspx");
            }
        }
        
        if (!Page.IsPostBack)
        {
            bindData();
            drpRoles_SelectedIndexChanged(null, null);
        }
    }
    private void bindData()
    {
        drpRoles.DataSource = Roles.GetRoles();
        drpRoles.DataBind();
        chkLstModules.DataSource = tblModules.GetAllModules();
        chkLstModules.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        List<tblRoleRModule> inpermissions = new List<tblRoleRModule>();
        int roleId = Convert.ToInt32(drpRoles.SelectedValue.ToString());
        foreach (ListItem item in chkLstModules.Items)
        {
            if (item.Selected)
            {
                tblRoleRModule rmodule = new tblRoleRModule();
                rmodule.Roleid = roleId;
                rmodule.ModuleId = Convert.ToInt32(item.Value);
                inpermissions.Add(rmodule);
            }
        }
        
        tblModules.SaveModulePermission(roleId, inpermissions);
        //Response.Redirect("~/admin/role.aspx?id=" + drpRoles.SelectedValue);
    }
    protected void drpRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        int roleId = Convert.ToInt32(drpRoles.SelectedValue.ToString());
        List<tblRoleRModule> permissions = tblModules.GetRolePermission(roleId);
        foreach (ListItem item in chkLstModules.Items)
        {
            item.Selected = false;  
        }
        foreach (tblRoleRModule role in permissions)
        {
            ListItem item = chkLstModules.Items.FindByValue(role.ModuleId.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }

        tbluserhistory _userhistory = new tbluserhistory();
        _userhistory.Action = "Role";
        _userhistory.description = _user.Username + " has updated permission for role " + roleId;
        _userhistory.time = DateTime.Now;
        _userhistory.useraan = _user.AAN;
        userHistory.Save(_userhistory);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/default.aspx");
    }
}
