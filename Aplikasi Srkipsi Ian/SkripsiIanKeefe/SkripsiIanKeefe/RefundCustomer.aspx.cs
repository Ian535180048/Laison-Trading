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
    public partial class RefundCustomer : System.Web.UI.Page
    {
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

        #region button
        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();

            DataTable dt = GetSearchDataPenjualanByRefUser(Convert.ToInt64(id), txtboxproduknama.Text);

            gvbayar.DataSource = dt;
            gvbayar.DataBind();
        }
        #endregion

        #region index
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
                Response.Redirect("RefundUploadBukti.aspx");
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

        public virtual DataTable GetSearchDataPenjualanByRefUser(Int64 id, string prodname)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.TRANS_PENJUALAN
                         join chck in context.CHECKOUTs on trans.TRANS_PENJUALAN_ID equals chck.TRANS_PENJUALAN_ID
                         join prod in context.PRODUCTs on chck.PROD_ID equals prod.PROD_ID
                         where (trans.STATUS == "SENT" || trans.STATUS == "FINISH")
                         && trans.REF_USER_ID == id
                         && prod.PROD_NAME.Contains(prodname)
                         && trans.NOTES == null
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
                             trans.ALAMAT
                         });

            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataPenjualanByRefUser(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.TRANS_PENJUALAN
                         where (trans.STATUS == "SENT" || trans.STATUS =="FINISH")
                         && trans.REF_USER_ID == id && trans.NOTES == null
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
                             trans.ALAMAT
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