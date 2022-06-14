using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SkripsiIanKeefe
{
    public partial class SuccessPembayaran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();
            }
        }
    }
}