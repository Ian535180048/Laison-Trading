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
    public partial class EditStok : System.Web.UI.Page
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

                lbltransno.Text = stok.TRANS_NO;
                lblprodnama.Text = prod.PROD_NAME;

                txtboxammount.Text = Convert.ToString(stok.STOCK_AMT);
                txtboxtanggal.Text = Convert.ToString(stok.TANGGAL);
                txtboxharga.Text = Convert.ToString(stok.HARGA);
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var stokid1 = (string)Session["stokid"];
            LAISONEntities context = new LAISONEntities();

            Int64 stokid = Convert.ToInt64(stokid1);

            using (var context1 = new LAISONEntities())
            {
                var stok = context1.TRANS_STOCK_BARANG.Where(u => u.TRANS_STOCK_BARANG_ID == stokid).FirstOrDefault();

                

                if (txtboxtanggal.Text != null)
                {
                    stok.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                }

                if (txtboxharga.Text != null)
                {
                    stok.HARGA = Convert.ToDecimal(txtboxharga.Text);
                }

                var jurnal = context1.JURNALs.Where(u => u.TRANS_STOCK_BARANG_ID == stokid).ToList();

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

                if (txtboxharga.Text != null)
                {
                    int i = 0;
                    foreach (JURNAL jurnals in jurnal)
                    {
                        if (i == 0)
                        {
                            jurnals.IN_AMT = Convert.ToDecimal(txtboxharga.Text);
                            i++;
                        }

                        else if (i == 1)
                        {
                            jurnals.OUT_AMT = Convert.ToDecimal(txtboxharga.Text);
                            i++;
                        }
                    }
                }

                Int64 prodid = Convert.ToInt64(stok.PROD_ID);

                var product = context1.PRODUCTs.Where(u => u.PROD_ID == prodid).FirstOrDefault();

                if (txtboxammount.Text != null)
                {
                    product.STOCK_TOTAL = product.STOCK_TOTAL - stok.STOCK_AMT + Convert.ToInt32(txtboxammount.Text);
                    stok.STOCK_AMT = Convert.ToInt32(txtboxammount.Text);
                }

                context1.SaveChanges();
                Session["id"] = id;
                Response.Redirect("PembelianStockBarangPagingAdmin.aspx");
            }
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var stokid = (string)Session["stokid"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PembelianStockBarangPagingAdmin.aspx");
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

        protected void txtboxharga_TextChanged(object sender, EventArgs e)
        {
            double ammount = Convert.ToDouble(txtboxharga.Text);
            txtboxharga.Text = ammount.ToString("#,##0");
        }
    }
}