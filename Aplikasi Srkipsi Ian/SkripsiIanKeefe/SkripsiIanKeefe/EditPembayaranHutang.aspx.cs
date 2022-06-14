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
    public partial class EditPembayaranHutang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];
                LAISONEntities context = new LAISONEntities();
                TRANS_PEMBAYARAN_HUTANG hutang = GetDataPembayaranHutangById(Convert.ToInt64(hutangid));


                lbltransno.Text = hutang.TRANS_NO;

                txtboxammount.Text = Convert.ToString(hutang.TOTAL);
                txtboxtanggal.Text = Convert.ToString(hutang.TANGGAL);
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var hutangid = (string)Session["hutangid"];

            Int64 hutangid2 = Convert.ToInt64(hutangid);
            TRANS_PEMBAYARAN_HUTANG transbyrhutang = GetDataPembayaranHutangById(Convert.ToInt64(hutangid));


            using (var context1 = new LAISONEntities())
            {
                var byr = context1.TRANS_PEMBAYARAN_HUTANG.Where(u => u.TRANS_BYR_UTANG_ID == hutangid2).FirstOrDefault();

                Int64 hutangid3 = Convert.ToInt64(byr.TRANS_HUTANG_ID);
                var hutang = context1.TRANS_HUTANG.Where(u => u.TRANS_HUTANG_ID == hutangid3).FirstOrDefault();

                if (txtboxammount.Text != null)
                {
                    if (Convert.ToDecimal(txtboxammount.Text) > hutang.SISA)
                    {
                        lblerrorammount.Visible = true;
                    }

                    else
                    {
                        hutang.SISA = hutang.SISA + transbyrhutang.TOTAL - Convert.ToDecimal(txtboxammount.Text);

                        if (hutang.SISA != 0)
                        {
                            hutang.IS_ACTIVE = "ACT";
                        }

                        else if (hutang.SISA == 0)
                        {
                            hutang.IS_ACTIVE = "EXP";
                        }

                        byr.TOTAL = Convert.ToDecimal(txtboxammount.Text);
                    }
                    
                }

                if (txtboxtanggal.Text != null)
                {
                    if(Convert.ToDateTime(txtboxtanggal.Text) < hutang.TANGGAL)
                    {
                        lblerrortanggal.Visible = true;
                    }
                    else
                    {
                        byr.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }
                }

                var jurnal = context1.JURNALs.Where(u => u.TRANS_BYR_UTANG_ID == hutangid2).ToList();

                if (txtboxammount.Text != null)
                {
                    int i = 0;
                    foreach (JURNAL jurnals in jurnal)
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
            Session["id"] = id;
            Response.Redirect("PembayaranHutangPagingAdmin.aspx");
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var hutangid = (string)Session["hutangid"];

            Session["id"] = id;
            Response.Redirect("PembayaranHutangPagingAdmin.aspx");
        }

        public virtual TRANS_HUTANG GetDataHutangById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG hutang = (from j in context.TRANS_HUTANG where j.TRANS_HUTANG_ID == hutangid select j).FirstOrDefault();

            return hutang;
        }

        public virtual TRANS_PEMBAYARAN_HUTANG GetDataPembayaranHutangById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_PEMBAYARAN_HUTANG hutang = (from j in context.TRANS_PEMBAYARAN_HUTANG where j.TRANS_BYR_UTANG_ID == hutangid select j).FirstOrDefault();

            return hutang;
        }
    }
}