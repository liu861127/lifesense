using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.Admin
{
    public partial class Site_Head : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblmsg.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        int i=0;
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
            Timer1.Interval = 1000;
            lblmsg.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}