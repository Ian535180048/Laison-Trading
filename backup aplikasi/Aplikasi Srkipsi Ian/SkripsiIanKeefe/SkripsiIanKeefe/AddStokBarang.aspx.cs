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
    public partial class AddStokBarang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();
                LAISONEntities context = new LAISONEntities();

                List<PRODUCT> productlist = GetDataProductList();

                ddlproduk.DataSource = productlist;

                ddlproduk.DataTextField = "PROD_NAME";
                ddlproduk.DataValueField = "PROD_ID";
                ddlproduk.DataBind();

                List<SUPPLIER> supllierlist = GetDataSupplierList();

                ddlsupplier.DataSource = supllierlist;

                ddlsupplier.DataTextField = "SUPPLIER_NAME";
                ddlsupplier.DataValueField = "SUPPLIER_ID";
                ddlsupplier.DataBind();
            }

        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            TRANS_STOCK_BARANG transstok = new TRANS_STOCK_BARANG();


            transstok.SUPPLIER_ID = Convert.ToInt64(ddlsupplier.SelectedValue);
            transstok.PROD_ID = Convert.ToInt64(ddlproduk.SelectedValue);
            transstok.HARGA = Convert.ToDecimal(txtboxharga.Text);
            transstok.STOCK_AMT = Convert.ToInt32(txtboxammount.Text);

            TRANS_STOCK_BARANG transid = GetDataTransStockbyIdLatest();

            transstok.TRANS_NO = Convert.ToString(DateTime.Today.Year) + Convert.ToString("STK") + transid.TRANS_STOCK_BARANG_ID.ToString("0000");
            transstok.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);

            PRODUCT prod = GetProdById(Convert.ToInt64(ddlproduk.SelectedValue));

            UpdateStokAmt(prod);

            for (int i = 0; i < 2; i++)
            {
                JURNAL jurnal = new JURNAL();
                if (i == 0)
                {
                    jurnal.DESCR = "Persediaan Barang Dagang";
                    jurnal.IN_AMT = Convert.ToDecimal(txtboxharga.Text);
                    jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    jurnal.COA_ID = GetCOA("1002");
                }

                else if (i == 1)
                {
                    jurnal.COA_ID = GetCOA("1001");
                    jurnal.OUT_AMT = Convert.ToDecimal(txtboxharga.Text);
                    jurnal.DESCR = "Kas";
                    jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                }
                transstok.JURNALs.Add(jurnal);
            }
            
            context.TRANS_STOCK_BARANG.Add(transstok);
            context.SaveChanges();
            Session["id"] = id;
            Response.Redirect("PembelianStockBarangPagingAdmin.aspx");
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("PembelianStockBarangPagingAdmin.aspx");
        }

        public virtual List<PRODUCT> GetDataProductList()
        {
            LAISONEntities context = new LAISONEntities();

            List<PRODUCT> product = (from j in context.PRODUCTs select j).ToList();

            return product;

        }

        public virtual List<SUPPLIER> GetDataSupplierList()
        {
            LAISONEntities context = new LAISONEntities();

            List<SUPPLIER> supplier = (from j in context.SUPPLIERs select j).ToList();

            return supplier;

        }

        public virtual TRANS_STOCK_BARANG GetDataTransStockbyIdLatest()
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_STOCK_BARANG a = (from j in context.TRANS_STOCK_BARANG
                             orderby j.TRANS_STOCK_BARANG_ID descending
                             select j).FirstOrDefault();
            return a;
        }

        public virtual PRODUCT GetProdById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT a = (from j in context.PRODUCTs
                         where j.PROD_ID == id
                         select j).FirstOrDefault();
            return a;
        }

        public virtual Int64 GetCOA(string value)
        {
            LAISONEntities context = new LAISONEntities();

            Int64 a = (from j in context.COAs
                       where j.COA_NO == value
                       select j.COA_ID).FirstOrDefault();

            return a;
        }

        public void UpdateStokAmt(PRODUCT prod)
        {
            using (var context = new LAISONEntities())
            {
                var product = context.PRODUCTs.Where(u => u.PROD_ID == prod.PROD_ID).FirstOrDefault();

                product.STOCK_TOTAL = prod.STOCK_TOTAL + Convert.ToInt32(txtboxammount.Text);
                context.SaveChanges();
            }
        }

        protected void txtboxharga_TextChanged(object sender, EventArgs e)
        {
            double ammount = Convert.ToDouble(txtboxharga.Text);
            txtboxharga.Text = ammount.ToString("#,##0");
        }
    }
}