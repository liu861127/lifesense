﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace lifesense.Web.Admin
{
    public partial class Main : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("尊敬的用户：您好！欢迎使用本系统！");
        }
    }
}