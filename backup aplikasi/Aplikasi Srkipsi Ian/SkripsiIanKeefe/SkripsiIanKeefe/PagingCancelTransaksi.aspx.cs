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
    public partial class PagingCancelTransaksi : System.Web.UI.Page
    {
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable transpenjualan = GetDataPenjualan();
                if (gvpenjualan.Rows.Count == 0)
                {
                    gvpenjualan.DataSource = transpenjualan;
                    gvpenjualan.DataBind();
                }
            }
        }
        #endregion

        #region button

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();

            DataTable transpenjualan = GetDataPenjualanSearch(txtboxnamacustomer.Text, txtboxproduk.Text, txtboxtransno.Text, ddlstatus.SelectedValue, txtboxtanggalawal.Text, txtboxtanggalakhir.Text);
            gvpenjualan.DataSource = transpenjualan;
            gvpenjualan.DataBind();
        }
        #endregion

        #region index
        protected void gvpenjualan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 transid;

            if (e.CommandName == "DTL")
            {
                transid = Int64.Parse(((Label)gvpenjualan.Rows[index].FindControl("lbl_trans_id")).Text);
                string jualid2 = Convert.ToString(transid);
                Session["id"] = userid;
                Session["jualid"] = jualid2;
                Response.Redirect("DetailPenjualanAdmin.aspx");
            }

            if (e.CommandName == "CAN")
            {
                transid = Int64.Parse(((Label)gvpenjualan.Rows[index].FindControl("lbl_trans_id")).Text);
                string transid2 = Convert.ToString(transid);

                TRANS_PENJUALAN trans = GetDataTransPenjualan(Convert.ToInt64(transid2));

                List<CHECKOUT> chk = GetDataCheckout(Convert.ToInt64(trans.TRANS_PENJUALAN_ID));

                EditTrans(trans);

               
                foreach(CHECKOUT chks in chk)
                {
                    EditProduct(Convert.ToInt64(chks.PROD_ID), Convert.ToInt32(chks.JUMLAH_BARANG));
                }
                

                List<JURNAL> jurnal = GetDatajurnal(trans.TRANS_PENJUALAN_ID);

                foreach (JURNAL j in jurnal)
                {
                    JURNAL ju = new JURNAL();
                    ju.JURNAL_ID = j.JURNAL_ID;
                    context.Entry(ju).State = EntityState.Deleted;
                }

                context.SaveChanges();

                DataTable transpenjualan = GetDataPenjualan();
                gvpenjualan.DataSource = transpenjualan;
                gvpenjualan.DataBind();

            }
        }
        #endregion

        #region method

        public virtual void EditProduct(Int64 prod, Int32 jumlah)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.PRODUCTs.Where(u => u.PROD_ID == prod).FirstOrDefault();

                penjualan.STOCK_TOTAL = penjualan.STOCK_TOTAL + jumlah;

                context1.SaveChanges();

            }
        }

        public virtual List<CHECKOUT> GetDataCheckout(Int64 jualid)
        {
            LAISONEntities context = new LAISONEntities();

            List<CHECKOUT> chk = (from j in context.CHECKOUTs where j.TRANS_PENJUALAN_ID == jualid select j).ToList();

            return chk;
        }

        public virtual List<JURNAL> GetDatajurnal(Int64 jualid)
        {
            LAISONEntities context = new LAISONEntities();

            List<JURNAL> biaya = (from j in context.JURNALs where j.TRANS_PENJUALAN_ID == jualid select j).ToList();

            return biaya;
        }

        public virtual void EditTrans(TRANS_PENJUALAN trans)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.TRANS_PENJUALAN.Where(u => u.TRANS_PENJUALAN_ID == trans.TRANS_PENJUALAN_ID).FirstOrDefault();

                penjualan.STATUS = "CAN";

                context1.SaveChanges();

            }
        }

        public virtual TRANS_PENJUALAN GetDataTransPenjualan(Int64 transid)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];

            TRANS_PENJUALAN trans = (from j in context.TRANS_PENJUALAN
                                     where j.TRANS_PENJUALAN_ID == transid
                                     select j).FirstOrDefault();

            return trans;
        }

        public virtual DataTable GetDataPenjualan()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transpenjualan in context.TRANS_PENJUALAN
                         join checkouts in context.CHECKOUTs
                         on transpenjualan.TRANS_PENJUALAN_ID equals checkouts.TRANS_PENJUALAN_ID
                         join product in context.PRODUCTs
                         on checkouts.PROD_ID equals product.PROD_ID
                         join cust in context.REF_USER
                         on transpenjualan.REF_USER_ID equals cust.REF_USER_ID
                         where transpenjualan.STATUS != "FINISH" & transpenjualan.STATUS != "SENT" & transpenjualan.STATUS !="CAN" & transpenjualan.STATUS !="CAN_REQ"
                         select new
                         {
                             transpenjualan.TRANS_PENJUALAN_ID,
                             transpenjualan.NAMA_PENERIMA,
                             product.PROD_NAME,
                             transpenjualan.TRANS_NO,
                             transpenjualan.TANGGAL,
                             STATUS =
                             (
                                transpenjualan.STATUS == "PAID" ? "Terbayar" :
                                transpenjualan.STATUS == "PD_APV_REQ" ? "Menunggu Konfirmasi Pembayaran" :
                                transpenjualan.STATUS == "PD_REQ" ? "Menunggu Pembayaran" :
                                "Error"
                             ),
                             checkouts.HARGA_PRODUK,
                             checkouts.JUMLAH_BARANG
                         });

            DataTable dt = LINQResultToDataTable(query);

            return dt;
        }

        public virtual DataTable GetDataPenjualanSearch(string custname, string produknama, string transno, string status, string dateawal, string dateakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transpenjualan in context.TRANS_PENJUALAN
                         join checkouts in context.CHECKOUTs
                         on transpenjualan.TRANS_PENJUALAN_ID equals checkouts.TRANS_PENJUALAN_ID
                         join product in context.PRODUCTs
                         on checkouts.PROD_ID equals product.PROD_ID
                         where transpenjualan.NAMA_PENERIMA.Contains(custname)
                         & product.PROD_NAME.Contains(produknama) & transpenjualan.TRANS_NO.Contains(transno) & transpenjualan.STATUS.Contains(status)
                         & transpenjualan.TANGGAL >= dateawal1 & transpenjualan.TANGGAL <= dateakhir1 & transpenjualan.STATUS != "FINISH" & transpenjualan.STATUS != "SENT" & transpenjualan.STATUS != "CAN" &
                         transpenjualan.STATUS != "CAN_REQ"

                         select new
                         {
                             transpenjualan.TRANS_PENJUALAN_ID,
                             transpenjualan.NAMA_PENERIMA,
                             product.PROD_NAME,
                             transpenjualan.TRANS_NO,
                             transpenjualan.TANGGAL,
                             STATUS =
                             (
                                transpenjualan.STATUS == "PAID" ? "Terbayar" :
                                transpenjualan.STATUS == "PD_APV_REQ" ? "Menunggu Konfirmasi Pembayaran" :
                                transpenjualan.STATUS == "PD_REQ" ? "Menunggu Pembayaran" :
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
        #endregion
    }
}