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
    public partial class PenjualanPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();

            DataTable dtpenjualan = GetDataTransPenjualan();

            if (gvpenjualan.Rows.Count == 0)
            {
                gvpenjualan.DataSource = dtpenjualan;
                gvpenjualan.DataBind();
            }

        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();
            DataTable dtpenjualan = new DataTable();

            if (ddlstatus.SelectedValue == "")
            {
                dtpenjualan = GetDataPenjualanSemua(txtboxnamacustomer.Text, txtboxproduk.Text, txtboxtransno.Text, ddlstatus.SelectedValue, txtboxtanggalawal.Text, txtboxtanggalakhir.Text);
            }
            else
            {
                dtpenjualan = GetDataPenjualan(txtboxnamacustomer.Text, txtboxproduk.Text, txtboxtransno.Text, ddlstatus.SelectedValue, txtboxtanggalawal.Text, txtboxtanggalakhir.Text);
            }
            gvpenjualan.DataSource = dtpenjualan;
            gvpenjualan.DataBind();

        }

        public virtual DataTable GetDataPenjualanSemua(string custname, string produknama, string transno, string status, string dateawal, string dateakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transpenjualan in context.TRANS_PENJUALAN
                         join checkouts in context.CHECKOUTs
                         on transpenjualan.TRANS_PENJUALAN_ID equals checkouts.TRANS_PENJUALAN_ID
                         join cust in context.REF_USER
                         on transpenjualan.REF_USER_ID equals cust.REF_USER_ID
                         join product in context.PRODUCTs
                         on checkouts.PROD_ID equals product.PROD_ID
                         where transpenjualan.NAMA_PENERIMA.Contains(custname)
                         & product.PROD_NAME.Contains(produknama) & transpenjualan.TRANS_NO.Contains(transno) & transpenjualan.STATUS.Contains(status)
                         & transpenjualan.TANGGAL >= dateawal1 & transpenjualan.TANGGAL <= dateakhir1
                         select new
                         {
                             transpenjualan.TRANS_PENJUALAN_ID,
                             transpenjualan.NAMA_PENERIMA,
                             product.PROD_NAME,
                             transpenjualan.TRANS_NO,
                             transpenjualan.TANGGAL,
                             STATUS =
                             (
                                transpenjualan.STATUS == "PD_REQ" ? "Menunggu Pembayaran" :
                                transpenjualan.STATUS == "PD_APV_REQ" ? "Menunggu Konfirmasi Pembayaran" :
                                transpenjualan.STATUS == "PAID" ? "Terbayar" :
                                transpenjualan.STATUS == "SENT" ? "Terkirim" :
                                transpenjualan.STATUS == "CAN" ? "Dibatalkan" :
                                transpenjualan.STATUS == "FINISH" ? "Diterima" :
                                transpenjualan.STATUS == "CAN_REQ" ? "Request Pembatalan" :
                                transpenjualan.STATUS == "REQ_REFUND" ? "Menunggu Persetujuan Refund" :
                                "Error"
                             ),
                             checkouts.HARGA_PRODUK,
                             checkouts.JUMLAH_BARANG
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataTransPenjualan()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transpenjualan in context.TRANS_PENJUALAN
                         join checkouts in context.CHECKOUTs
                         on transpenjualan.TRANS_PENJUALAN_ID equals checkouts.TRANS_PENJUALAN_ID
                         join cust in context.REF_USER
                         on transpenjualan.REF_USER_ID equals cust.REF_USER_ID
                         join product in context.PRODUCTs
                         on checkouts.PROD_ID equals product.PROD_ID
                         select new
                         {
                             transpenjualan.TRANS_PENJUALAN_ID,
                             transpenjualan.NAMA_PENERIMA,
                             cust.FIRST_NAME,
                             cust.LAST_NAME,
                             product.PROD_NAME,
                             transpenjualan.TRANS_NO,
                             transpenjualan.TANGGAL,
                             STATUS =
                             (
                                transpenjualan.STATUS == "PD_REQ" ? "Menunggu Pembayaran" :
                                transpenjualan.STATUS == "PD_APV_REQ" ? "Menunggu Konfirmasi Pembayaran" :
                                transpenjualan.STATUS == "PAID" ? "Terbayar" :
                                transpenjualan.STATUS == "SENT" ? "Terkirim" :
                                transpenjualan.STATUS == "CAN" ? "Dibatalkan" :
                                transpenjualan.STATUS == "FINISH" ? "Diterima" :
                                transpenjualan.STATUS == "CAN_REQ" ? "Request Pembatalan" :
                                transpenjualan.STATUS == "REQ_REFUND" ? "Menunggu Persetujuan Refund" :
                                "Error"
                             ),
                             checkouts.HARGA_PRODUK,
                             checkouts.JUMLAH_BARANG
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }


        public virtual DataTable GetDataPenjualan(string custname, string produknama, string transno, string status, string dateawal, string dateakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transpenjualan in context.TRANS_PENJUALAN
                             join checkouts in context.CHECKOUTs
                             on transpenjualan.TRANS_PENJUALAN_ID equals checkouts.TRANS_PENJUALAN_ID
                             join cust in context.REF_USER
                             on transpenjualan.REF_USER_ID equals cust.REF_USER_ID
                             join product in context.PRODUCTs
                             on checkouts.PROD_ID equals product.PROD_ID
                             where transpenjualan.NAMA_PENERIMA.Contains(custname)
                             & product.PROD_NAME.Contains(produknama) & transpenjualan.TRANS_NO.Contains(transno) & transpenjualan.STATUS ==status
                             & transpenjualan.TANGGAL >= dateawal1 & transpenjualan.TANGGAL <= dateakhir1
                             select new
                             {
                                 transpenjualan.TRANS_PENJUALAN_ID,
                                 transpenjualan.NAMA_PENERIMA,
                                 product.PROD_NAME,
                                 transpenjualan.TRANS_NO,
                                 transpenjualan.TANGGAL,
                                 STATUS = 
                                 (
                                    transpenjualan.STATUS == "PD_REQ" ? "Menunggu Pembayaran" :
                                    transpenjualan.STATUS == "PD_APV_REQ"? "Menunggu Konfirmasi Pembayaran" :
                                    transpenjualan.STATUS == "PAID" ?  "Terbayar" :
                                    transpenjualan.STATUS == "SENT" ? "Terkirim" :
                                    transpenjualan.STATUS == "CAN" ? "Dibatalkan":
                                    transpenjualan.STATUS == "FINISH" ? "Diterima":
                                    transpenjualan.STATUS == "CAN_REQ" ? "Request Pembatalan" :
                                    transpenjualan.STATUS == "REQ_REFUND" ? "Menunggu Persetujuan Refund" :
                                    "Error"
                                 ),
                                 checkouts.HARGA_PRODUK,
                                 checkouts.JUMLAH_BARANG
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

        protected void gvpenjualan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 jualid;

            if (e.CommandName == "DTL")
            {
                jualid = Int64.Parse(((Label)gvpenjualan.Rows[index].FindControl("lbl_trans_id")).Text);
                string jualid2 = Convert.ToString(jualid);
                Session["id"] = userid;
                Session["jualid"] = jualid2;
                Response.Redirect("DetailPenjualanAdmin.aspx");
            }
        }
    }
}