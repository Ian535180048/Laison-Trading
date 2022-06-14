using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using SkripsiIanKeefe.Entities;

namespace SkripsiIanKeefe
{
    public partial class DetailPembelianStok : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var stokid = (string)Session["stokid"];
                LAISONEntities context = new LAISONEntities();
                TRANS_STOCK_BARANG stok = GetDataStokBarang(Convert.ToInt64(stokid));

                PRODUCT prod = GetDataProductbyid(Convert.ToInt64(stok.PROD_ID));

                SUPPLIER suppl = GetSuppById(Convert.ToInt64(stok.SUPPLIER_ID));

                lbltransno.Text = stok.TRANS_NO;
                lblprodnama.Text = prod.PROD_NAME;

                DateTime tanggal = stok.TANGGAL;
                lbltanggal.Text = tanggal.ToString("dddd, dd MMMM yyyy");


                lblsuppliernama.Text = Convert.ToString(suppl.SUPPLIER_NAME);
                lbltotal.Text = String.Format("{0:Rp 0,0.00}", stok.HARGA);
                lblstokamt.Text = Convert.ToString(stok.STOCK_AMT);
                
            }
        }

        public virtual TRANS_STOCK_BARANG GetDataStokBarang(Int64 stokid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_STOCK_BARANG stok = (from j in context.TRANS_STOCK_BARANG where j.TRANS_STOCK_BARANG_ID == stokid select j).FirstOrDefault();

            return stok;
        }

        public virtual PRODUCT GetDataProductbyid(Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT stok = (from j in context.PRODUCTs where j.PROD_ID == prodid select j).FirstOrDefault();

            return stok;
        }

        public virtual SUPPLIER GetSuppById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            SUPPLIER a = (from j in context.SUPPLIERs
                          where j.SUPPLIER_ID == id
                          select j).FirstOrDefault();
            return a;
        }
    }
}