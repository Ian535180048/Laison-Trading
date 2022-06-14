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
    public partial class DetailPembayaranHutang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var hutangid = (string)Session["hutangid"];
            LAISONEntities context = new LAISONEntities();
            TRANS_PEMBAYARAN_HUTANG hutang = GetDataPembayaranHutangById(Convert.ToInt64(hutangid));
            TRANS_HUTANG hutang1 = GetDataHutangById(Convert.ToInt64(hutang.TRANS_HUTANG_ID));

            lbltransno.Text = hutang.TRANS_NO;
            lbltranshutangno.Text = hutang1.TRANS_NO;
            lblamt.Text = String.Format("{0:Rp 0,0.00}", hutang.TOTAL);

            DateTime tanggal = hutang.TANGGAL;
            lbltanggal.Text = tanggal.ToString("dddd, dd MMMM yyyy");


        }

        public virtual TRANS_PEMBAYARAN_HUTANG GetDataPembayaranHutangById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_PEMBAYARAN_HUTANG hutang = (from j in context.TRANS_PEMBAYARAN_HUTANG where j.TRANS_BYR_UTANG_ID == hutangid select j).FirstOrDefault();

            return hutang;
        }

        public virtual TRANS_HUTANG GetDataHutangById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG hutang = (from j in context.TRANS_HUTANG where j.TRANS_HUTANG_ID == hutangid select j).FirstOrDefault();

            return hutang;
        }
    }
}