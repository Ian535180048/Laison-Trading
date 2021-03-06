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
    public partial class PagingApprovePembayaranAdmin : System.Web.UI.Page
    {
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

                TRANS_PENJUALAN trans = GetDataTransPenjualan(Convert.ToInt64(transid2));

                EditTrans(trans);

                List<JURNAL> listjurnal = new List<JURNAL>();
                for (int i = 0; i < 2; i++)
                {
                    JURNAL jurnal = new JURNAL();
                    if (i == 0)
                    {
                        jurnal.DESCR = "Kas";
                        jurnal.IN_AMT = Convert.ToDecimal(trans.TOTAL);
                        jurnal.TANGGAL = Convert.ToDateTime(trans.TANGGAL);
                        jurnal.TRANS_PENJUALAN_ID = Convert.ToInt64(transid);
                        jurnal.COA_ID = GetCOA("1001");
                    }

                    else if (i == 1)
                    {
                        jurnal.COA_ID = GetCOA("4001");
                        jurnal.OUT_AMT = Convert.ToDecimal(trans.TOTAL);
                        jurnal.DESCR = "Penjualan";
                        jurnal.TRANS_PENJUALAN_ID = Convert.ToInt64(transid);
                        jurnal.TANGGAL = Convert.ToDateTime(trans.TANGGAL);
                    }
                    listjurnal.Add(jurnal);
                }

                foreach (JURNAL jurnal in listjurnal)
                {
                    context.JURNALs.Add(jurnal);
                    context.SaveChanges();
                }

                DataTable transpenjualan = GetDataPenjualanAPV();
                gvpenjualan.DataSource = transpenjualan;
                gvpenjualan.DataBind();

            }

            if (e.CommandName == "RJC")
            {
                transid = Int64.Parse(((Label)gvpenjualan.Rows[index].FindControl("lbl_trans_id")).Text);
                string transid2 = Convert.ToString(transid);

                TRANS_PENJUALAN trans = GetDataTransPenjualan(Convert.ToInt64(transid2));

                EditRJCTrans(trans);

                DataTable transpenjualan = GetDataPenjualanAPV();
                gvpenjualan.DataSource = transpenjualan;
                gvpenjualan.DataBind();

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


        public virtual Int64 GetCOA(string value)
        {
            LAISONEntities context = new LAISONEntities();

            Int64 a = (from j in context.COAs
                       where j.COA_NO == value
                       select j.COA_ID).FirstOrDefault();

            return a;
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

        public virtual void EditRJCTrans(TRANS_PENJUALAN trans)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.TRANS_PENJUALAN.Where(u => u.TRANS_PENJUALAN_ID == trans.TRANS_PENJUALAN_ID).FirstOrDefault();

                penjualan.TANGGAL_BAYAR = DateTime.Today;
                penjualan.STATUS = "PD_REQ";
                context1.SaveChanges();
            }
        }

        public virtual void EditTrans(TRANS_PENJUALAN trans)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.TRANS_PENJUALAN.Where(u => u.TRANS_PENJUALAN_ID == trans.TRANS_PENJUALAN_ID).FirstOrDefault();

                penjualan.TANGGAL_BAYAR = DateTime.Today;
                penjualan.STATUS = "PAID";
                context1.SaveChanges();

            }
        }

        public virtual DataTable GetDataPenjualanAPV()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.TRANS_PENJUALAN
                         where trans.STATUS == "PD_APV_REQ"
                         
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

        public virtual DataTable GetDataPenjualanAPVSearch(string name, string transno, string tanggalawal, string tanggalakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(tanggalawal);
            DateTime dateakhir1 = Convert.ToDateTime(tanggalakhir);

            var query = (from trans in context.TRANS_PENJUALAN
                         where trans.STATUS == "PD_APV_REQ" && trans.NAMA_PENERIMA.Contains(name) 
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