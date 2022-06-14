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
    public partial class PembelianStockBarangPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable dthutang = GetTransDataStok();

                if (gvstokbarang.Rows.Count == 0)
                {
                    gvstokbarang.DataSource = dthutang;
                    gvstokbarang.DataBind();
                }            
            }
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dthutang = GetDataStok(txtboxtransno.Text, txtsuppliernama.Text, txtboxtanggalawal.Text, txtboxtanggalakhir.Text, txtproduknama.Text, txtsupplierno.Text);

            gvstokbarang.DataSource = dthutang;
            gvstokbarang.DataBind();
        }

        public virtual DataTable GetTransDataStok()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transstok in context.TRANS_STOCK_BARANG
                         join supplier in context.SUPPLIERs on transstok.SUPPLIER_ID equals supplier.SUPPLIER_ID
                         join produk in context.PRODUCTs on transstok.PROD_ID equals produk.PROD_ID
                         select new
                         {
                             produk.PROD_NAME,
                             transstok.HARGA,
                             transstok.STOCK_AMT,
                             transstok.TRANS_NO,
                             transstok.TANGGAL,
                             supplier.SUPPLIER_NO,
                             supplier.SUPPLIER_NAME,
                             transstok.TRANS_STOCK_BARANG_ID
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataStok(string transno, string suppliernama, string dateawal, string dateakhir, string produknama, string supplierno)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transstok in context.TRANS_STOCK_BARANG
                         join supplier in context.SUPPLIERs on transstok.SUPPLIER_ID equals supplier.SUPPLIER_ID
                         join produk in context.PRODUCTs on transstok.PROD_ID equals produk.PROD_ID
                         where transstok.TRANS_NO.Contains(transno) & transstok.TANGGAL <= dateakhir1 & transstok.TANGGAL >= dateawal1
                         & produk.PROD_NAME.Contains(produknama) & supplier.SUPPLIER_NO.Contains(supplierno) & supplier.SUPPLIER_NAME.Contains(suppliernama)
                         select new
                         {
                             produk.PROD_NAME,
                             transstok.HARGA,
                             transstok.STOCK_AMT,
                             transstok.TRANS_NO,
                             transstok.TANGGAL,
                             supplier.SUPPLIER_NO,
                             supplier.SUPPLIER_NAME,
                             transstok.TRANS_STOCK_BARANG_ID
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void ltladd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("AddStokBarang.aspx");
        }

        protected void gvstokbarang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 stokid;

            if (e.CommandName == "ED")
            {
                stokid = Int64.Parse(((Label)gvstokbarang.Rows[index].FindControl("lb_trans_id")).Text);
                string stokid2 = Convert.ToString(stokid);

                TRANS_STOCK_BARANG stok = GetDataStokbarangbyid(stokid);
                Session["id"] = userid;
                Session["stokid"] = stokid2;
                Response.Redirect("EditStok.aspx");
            }

            if (e.CommandName == "DTL")
            {
                stokid = Int64.Parse(((Label)gvstokbarang.Rows[index].FindControl("lb_trans_id")).Text);
                string stokid2 = Convert.ToString(stokid);

                TRANS_STOCK_BARANG stok = GetDataStokbarangbyid(stokid);
                Session["id"] = userid;
                Session["stokid"] = stokid2;
                Response.Redirect("DetailPembelianStok.aspx");
            }

            if (e.CommandName == "DEL")
            {
                stokid = Int64.Parse(((Label)gvstokbarang.Rows[index].FindControl("lb_trans_id")).Text);
                string stokid2 = Convert.ToString(stokid);

                TRANS_STOCK_BARANG stok = GetDataStokbarangbyid(stokid);
                List<JURNAL> jurnal = GetDatajurnalstok(stokid);

                foreach (JURNAL j in jurnal)
                {
                    JURNAL ju = new JURNAL();
                    ju.JURNAL_ID = j.JURNAL_ID;
                    context.Entry(ju).State = EntityState.Deleted;
                }


                using (var context1 = new LAISONEntities())
                {
                    var product = context1.PRODUCTs.Where(u => u.PROD_ID == stok.PROD_ID).FirstOrDefault();

                    product.STOCK_TOTAL = product.STOCK_TOTAL - stok.STOCK_AMT;
                    context1.SaveChanges();
                }

                TRANS_STOCK_BARANG stok1 = new TRANS_STOCK_BARANG();
                stok1.TRANS_STOCK_BARANG_ID = stok.TRANS_STOCK_BARANG_ID;

                context.Entry(stok1).State = EntityState.Deleted;
                context.SaveChanges();

                DataTable dthutang = GetTransDataStok();

                gvstokbarang.DataSource = dthutang;
                gvstokbarang.DataBind();
            }
        }

        public virtual TRANS_STOCK_BARANG GetDataStokbarangbyid(Int64 stokid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_STOCK_BARANG biaya = (from j in context.TRANS_STOCK_BARANG where j.TRANS_STOCK_BARANG_ID == stokid select j).FirstOrDefault();

            return biaya;
        }

        public virtual List<JURNAL> GetDatajurnalstok(Int64 stokid)
        {
            LAISONEntities context = new LAISONEntities();

            List<JURNAL> biaya = (from j in context.JURNALs where j.TRANS_STOCK_BARANG_ID == stokid select j).ToList();

            return biaya;
        }

        public virtual PRODUCT GetProdById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT a = (from j in context.PRODUCTs
                         where j.PROD_ID == id
                         select j).FirstOrDefault();
            return a;
        }
    }
}