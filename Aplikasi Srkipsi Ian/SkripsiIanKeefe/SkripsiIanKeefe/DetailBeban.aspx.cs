using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using SkripsiIanKeefe.Entities;

namespace SkripsiIanKeefe
{
    public partial class DetailBeban : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var bebanid = (string)Session["bebanid"];
                LAISONEntities context = new LAISONEntities();
                TRANS_BIAYA biaya = GetDataBebanbyid(Convert.ToInt64(bebanid));               

                lbltransno.Text = biaya.TRANS_NO;
                if (biaya.DESCR == "BL")
                {
                    lbltipe.Text = "Beban Listrik";
                }
                else if (biaya.DESCR == "BP")
                {
                    lbltipe.Text = "Beban Pajak";
                }
                else if (biaya.DESCR == "BA")
                {
                    lbltipe.Text = "Beban Air";
                }
                else if (biaya.DESCR == "BI")
                {
                    lbltipe.Text = "Beban Internet";
                }
                else if (biaya.DESCR == "BDLL")
                {
                    lbltipe.Text = "Beban Lain-lain";
                }

                lblammount.Text = String.Format("{0:Rp 0,0.00}", biaya.AMT);
                lbltanggal.Text = biaya.TANGGAL.ToString("dddd, dd MMMM yyyy");
            }
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var bebanid = (string)Session["bebanid"];

            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("BiayaPagingAdmin.aspx");
        }

        public virtual TRANS_BIAYA GetDataBebanbyid(Int64 bebanid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_BIAYA biaya = (from j in context.TRANS_BIAYA where j.TRANS_BIAYA_ID == bebanid select j).FirstOrDefault();

            return biaya;
        }
    }
}