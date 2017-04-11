using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Dal;

namespace Sys.WebUI
{
    public partial class Sql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btn1_Click(object sender, EventArgs e)
        {
            var sql = tb1.Text.Trim();
            if (!string.IsNullOrEmpty(sql))
            {
                lb1.Text = DALFactory.SqlAction(sql);
            }
        }
    }
}