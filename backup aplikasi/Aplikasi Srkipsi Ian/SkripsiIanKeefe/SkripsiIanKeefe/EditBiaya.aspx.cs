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
    public partial class EditBiaya : System.Web.UI.Page
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
                if(biaya.DESCR == "BL")
                {
                    lbltipebiaya.Text = "Beban Listrik";
                }
                else if (biaya.DESCR == "BP")
                {
                    lbltipebiaya.Text = "Beban Pajak";
                }
                else if (biaya.DESCR == "BA")
                {
                    lbltipebiaya.Text = "Beban Air";
                }
                else if (biaya.DESCR == "BI")
                {
                    lbltipebiaya.Text = "Beban Internet";
                }
                else if (biaya.DESCR == "BDLL")
                {
                    lbltipebiaya.Text = "Beban Lain-lain";
                }

                txtboxammount.Text = Convert.ToString(biaya.AMT);
                txtboxtanggal.Text = Convert.ToString(biaya.TANGGAL);
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var bebanid = (string)Session["bebanid"];

            Int64 bebanid2 = Convert.ToInt64(bebanid);

            using (var context1 = new LAISONEntities())
            {
                var beban = context1.TRANS_BIAYA.Where(u => u.TRANS_BIAYA_ID == bebanid2).FirstOrDefault();

                if (txtboxammount.Text != null )
                {
                    beban.AMT = Convert.ToDecimal(txtboxammount.Text);
                }

                if (txtboxtanggal.Text != null)
                {
                    beban.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                }

                var jurnal = context1.JURNALs.Where(u => u.TRANS_BIAYA_ID == bebanid2).ToList();

                if (txtboxammount.Text != null)
                {
                    int i = 0;
                    foreach(JURNAL jurnals in jurnal)
                    {
                        if (i == 0)
                        {
                            jurnals.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                            i++;
                        }

                        else if (i == 1)
                        {
                            jurnals.OUT_AMT = Convert.ToDecimal(txtboxammount.Text);
                            i++;
                        }
                    }
                }

                if (txtboxtanggal.Text != null)
                {
                    int i = 0;
                    foreach (JURNAL jurnals in jurnal)
                    {
                        if (i == 0)
                        {
                            jurnals.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                            i++;
                        }

                        else if (i == 1)
                        {
                            jurnals.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                            i++;
                        }
                    }
                }

                context1.SaveChanges();
            }

            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("BiayaPagingAdmin.aspx");
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

        protected void txtboxammount_TextChanged(object sender, EventArgs e)
        {
            double ammount = Convert.ToDouble(txtboxammount.Text);
            txtboxammount.Text = ammount.ToString("#,##0");
        }
    }
}