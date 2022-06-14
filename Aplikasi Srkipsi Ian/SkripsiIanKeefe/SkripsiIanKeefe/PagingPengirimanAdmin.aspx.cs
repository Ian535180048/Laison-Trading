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
    public partial class PagingPengirimanAdmin : System.Web.UI.Page
    {
        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable transpenjualan = GetDataPenjualanAPV();
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

            DataTable transpenjualan = GetDataPenjualanAPVSearch(txtboxnamacustomer.Text, txtboxtransno.Text, txtboxtanggalawal.Text, txtboxtanggalakhir.Text);
            gvpenjualan.DataSource = transpenjualan;
            gvpenjualan.DataBind();
        }

        #endregion

        #region command
        protected void gvpenjualan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 transid;

            if (e.CommandName == "APV")
            {
                transid = Int64.Parse(((Label)gvpenjualan.Rows[index].FindControl("lbl_trans_id")).Text);
                string transid2 = Convert.ToString(transid);
                Session["id"] = userid;
                Session["transid"] = transid2;
                Response.Redirect("BuktiApprovalPengiriman.aspx");

            }

            if (e.CommandName == "DTL")
            {
                transid = Int64.Parse(((Label)gvpenjualan.Rows[index].FindControl("lbl_trans_id")).Text);
                string jualid2 = Convert.ToString(transid);
                Session["id"] = userid;
                Session["jualid"] = jualid2;
                Response.Redirect("DetailPenjualanAdmin.aspx");
            }
        }

        #endregion

        #region method

        public virtual DataTable GetDataPenjualanAPVSearch(string name, string transno, string tanggalawal, string tanggalakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(tanggalawal);
            DateTime dateakhir1 = Convert.ToDateTime(tanggalakhir);

            var query = (from trans in context.TRANS_PENJUALAN
                         where trans.STATUS == "PAID" && trans.NAMA_PENERIMA.Contains(name)
                         && trans.TANGGAL >= dateawal1 & trans.TANGGAL <= dateakhir1 && trans.TRANS_NO.Contains(transno)
                         select new
                         {
                             trans.TRANS_PENJUALAN_ID,
                             trans.TRANS_NO,
                             trans.TOTAL,
                             trans.TANGGAL,
                             trans.REF_USER_ID,
                             trans.ONGKIR_NAMA,
                             trans.NAMA_PENERIMA,
                             trans.ONGKIR_TYPE,
                             trans.ALAMAT,
                             trans.BUKTI_TRANSAKSI
                         });

            DataTable dt = LINQResultToDataTable(query);

            return dt;
        }

        public virtual DataTable GetDataPenjualanAPV()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.TRANS_PENJUALAN
                         where trans.STATUS == "PAID"

                         select new
                         {
                             trans.TRANS_PENJUALAN_ID,
                             trans.TRANS_NO,
                             trans.TOTAL,
                             trans.TANGGAL,
                             trans.REF_USER_ID,
                             trans.ONGKIR_NAMA,
                             trans.NAMA_PENERIMA,
                             trans.ONGKIR_TYPE,
                             trans.ALAMAT,
                             trans.BUKTI_TRANSAKSI
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