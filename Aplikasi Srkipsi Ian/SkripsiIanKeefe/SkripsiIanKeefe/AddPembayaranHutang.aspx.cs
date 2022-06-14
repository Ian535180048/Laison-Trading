using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkripsiIanKeefe.Entities;
using System.Data;
using System.Reflection;

namespace SkripsiIanKeefe
{
    public partial class AddPembayaranHutang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];

                LAISONEntities context = new LAISONEntities();

                TRANS_HUTANG hutang = GetDataHutangByID(Convert.ToInt64(hutangid));

                CLIENT client = GetDataClientById(Convert.ToInt64(hutang.CLIENT_ID));

                lblclientname.Text = client.CLIENT_NAME;
                lblsisaammount.Text = Convert.ToString(hutang.SISA);
                lbltransno.Text = hutang.TRANS_NO;
                lblhutangammount.Text = Convert.ToString(hutang.HUTANG_AMT);
                lbltanggal.Text = Convert.ToString(hutang.TANGGAL);

            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            lblerrorammount.Visible = false;
            lblerrortanggal.Visible = false;

            if (Convert.ToDecimal(txtboxammount.Text) > Convert.ToDecimal(lblsisaammount.Text))
            {
                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];
                lblerrorammount.Visible = true;
            }

            else if (Convert.ToDateTime(txtboxtanggal.Text) < Convert.ToDateTime(lbltanggal.Text))
            {
                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];
                lblerrortanggal.Visible = true;
            }

            else
            {
                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];

                LAISONEntities context = new LAISONEntities();

                TRANS_PEMBAYARAN_HUTANG transpembayaranhutang = new TRANS_PEMBAYARAN_HUTANG();

                transpembayaranhutang.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                transpembayaranhutang.TOTAL = Convert.ToDecimal(txtboxammount.Text);
                transpembayaranhutang.TRANS_HUTANG_ID = Convert.ToInt64(hutangid);

                TRANS_PEMBAYARAN_HUTANG transid = GetDataTransPembayaranbyIdLatest();

                transpembayaranhutang.TRANS_NO = Convert.ToString(DateTime.Today.Year) + Convert.ToString("PHT") + transid.TRANS_BYR_UTANG_ID.ToString("0000");

                TRANS_HUTANG hutang = GetDataHutangByID(Convert.ToInt64(hutangid));

                UpdateStatSisahutang(hutang);

                for (int i = 0; i < 2; i++)
                {
                    JURNAL jurnal = new JURNAL();
                    if (i == 0)
                    {
                        jurnal.DESCR = "Hutang Dagang";
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                        jurnal.COA_ID = GetCOA("2001");
                    }

                    else if (i == 1)
                    {
                        jurnal.COA_ID = GetCOA("1001");
                        jurnal.OUT_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Kas";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }
                    transpembayaranhutang.JURNALs.Add(jurnal);
                }

                context.TRANS_PEMBAYARAN_HUTANG.Add(transpembayaranhutang);
                context.SaveChanges();

                Session["id"] = id;
                Response.Redirect("PembayaranHutangPagingAdmin.aspx");
            }
            
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("HutangPagingAdmin.aspx");
        }

        public virtual TRANS_HUTANG GetDataHutangByID(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG query = (from j in context.TRANS_HUTANG
                                  where j.TRANS_HUTANG_ID == hutangid select j).FirstOrDefault();

            return query;
        }

        public virtual CLIENT GetDataClientById(Int64 clientid)
        {
            LAISONEntities context = new LAISONEntities();

            CLIENT query = (from j in context.CLIENTs
                                  where j.CLIENT_ID == clientid select j).FirstOrDefault();
            return query;

        }

        public virtual TRANS_PEMBAYARAN_HUTANG GetDataTransPembayaranbyIdLatest()
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_PEMBAYARAN_HUTANG a = (from j in context.TRANS_PEMBAYARAN_HUTANG
                                         orderby j.TRANS_BYR_UTANG_ID descending
                                          select j).FirstOrDefault();
            return a;
        }

        public void UpdateStatSisahutang(TRANS_HUTANG hutang)
        {
            using (var context = new LAISONEntities())
            {
                var hutang1 = context.TRANS_HUTANG.Where(u => u.TRANS_HUTANG_ID == hutang.TRANS_HUTANG_ID).FirstOrDefault();
  
                if (hutang1.SISA - Convert.ToInt32(txtboxammount.Text) == 0)
                {
                    hutang1.IS_ACTIVE = "EXP";
                }
                hutang1.SISA = hutang1.SISA - Convert.ToInt32(txtboxammount.Text);

                context.SaveChanges();
            }
        }

        public virtual Int64 GetCOA(string value)
        {
            LAISONEntities context = new LAISONEntities();

            Int64 a = (from j in context.COAs
                       where j.COA_NO == value
                       select j.COA_ID).FirstOrDefault();

            return a;
        }

    }
}