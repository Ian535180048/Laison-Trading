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
    public partial class PagingPembayaranProduk : System.Web.UI.Page
    {
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable transpenjualan = GetDataPenjualanByRefUser(Convert.ToInt64(id));

                if (gvbayar.Rows.Count == 0)
                {
                    gvbayar.DataSource = transpenjualan;
                    gvbayar.DataBind();
                }
            }

        }
        #endregion

        #region toolbar

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();

            DataTable dt = GetSearchDataPenjualanByRefUser(txtboxnamacustomer.Text, txtboxproduk.Text, txtboxtransno.Text, txtboxtanggalawal.Text, txtboxtanggalakhir.Text, Convert.ToInt64(id));

            gvbayar.DataSource = dt;
            gvbayar.DataBind();
        }

        #endregion

        #region rowcommand
        protected void gvbayar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 transid;

            if (e.CommandName == "PMBYRN")
            {
                transid = Int64.Parse(((Label)gvbayar.Rows[index].FindControl("lbl_trans_id")).Text);
                string transid2 = Convert.ToString(transid);
                Session["id"] = userid;
                Session["transid"] = transid2;

                Response.Redirect("BuktiPembayaran.aspx");

            }

            if (e.CommandName == "DTL")
            {
                transid = Int64.Parse(((Label)gvbayar.Rows[index].FindControl("lbl_trans_id")).Text);
                string jualid2 = Convert.ToString(transid);
                Session["id"] = userid;
                Session["jualid"] = jualid2;
                Response.Redirect("DetailPenjualanCust.aspx");
            }
        }
        #endregion
        #region method
        public virtual DataTable GetDataPenjualanByRefUser(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transpenjualan in context.TRANS_PENJUALAN
                         join checkouts in context.CHECKOUTs
                         on transpenjualan.TRANS_PENJUALAN_ID equals checkouts.TRANS_PENJUALAN_ID
                         join product in context.PRODUCTs
                         on checkouts.PROD_ID equals product.PROD_ID
                         join cust in context.REF_USER
                         on transpenjualan.REF_USER_ID equals cust.REF_USER_ID
                         where transpenjualan.STATUS == "PD_REQ" & cust.REF_USER_ID == id
                         select new
                         {
                             transpenjualan.TRANS_PENJUALAN_ID,
                             transpenjualan.NAMA_PENERIMA,
                             product.PROD_NAME,
                             transpenjualan.TRANS_NO,
                             transpenjualan.TANGGAL,
                             checkouts.HARGA_PRODUK,
                             checkouts.JUMLAH_BARANG,
                             transpenjualan.ALAMAT,
                             transpenjualan.ONGKIR_NAMA,
                             transpenjualan.ONGKIR_TYPE,
                             transpenjualan.TOTAL
                         });

            DataTable dt = LINQResultToDataTable(query);

            return dt;
        }

        public virtual DataTable GetSearchDataPenjualanByRefUser(string custname, string produknama, string transno, string dateawal, string dateakhir, Int64 id)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from trans in context.TRANS_PENJUALAN
                         join chck in context.CHECKOUTs on trans.TRANS_PENJUALAN_ID equals chck.TRANS_PENJUALAN_ID
                         join prod in context.PRODUCTs on chck.PROD_ID equals prod.PROD_ID
                         join cust in context.REF_USER on trans.REF_USER_ID equals cust.REF_USER_ID
                         where trans.NAMA_PENERIMA.Contains(custname)
                         & prod.PROD_NAME.Contains(produknama) & trans.TRANS_NO.Contains(transno) 
                         & trans.TANGGAL >= dateawal1 & trans.TANGGAL <= dateakhir1 & cust.REF_USER_ID == id
                         && trans.REF_USER_ID == id
                         && prod.PROD_NAME.Contains(produknama) && trans.STATUS =="PD_REQ"
                         select new
                         {
                             trans.TRANS_PENJUALAN_ID,
                             trans.NAMA_PENERIMA,
                             prod.PROD_NAME,
                             trans.TRANS_NO,
                             trans.TANGGAL,
                             chck.HARGA_PRODUK,
                             chck.JUMLAH_BARANG,
                             trans.ALAMAT,
                             trans.ONGKIR_NAMA,
                             trans.ONGKIR_TYPE,
                             trans.TOTAL
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

        #endregion


    }
}