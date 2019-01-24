using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WhatsNew : System.Web.UI.Page
{
    enum ItemType{
        Excel=0,
        Image=1,
        Document=2
    }
    class WhatsNewItem {
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
        public ItemType ItemType { get; set; }
        public string DownloadLink { get; set; }
    }

    private void BindCirculars()
    {
        var circulars = Enumerable.Repeat(new WhatsNewItem()
        {
            Description = "Some test description",
            ItemType = ItemType.Image,
            PublishDate = DateTime.Now
        }, 5);

        rptCirculars.DataSource = circulars.ToList();
        rptCirculars.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindCirculars();   
        }
    }
}