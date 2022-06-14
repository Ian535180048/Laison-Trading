using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkripsiIanKeefe.Entities;
using System.Reflection;
using System.Data;

namespace SkripsiIanKeefe
{
    public partial class BiayaPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable dtpenjualan = GetDataTransBiaya();

                if (gvbeban.Rows.Count == 0)
                {
                    gvbeban.DataSource = dtpenjualan;
                    gvbeban.DataBind();
                }
            }
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dtpenjualan = GetDataBiaya(txtboxtransno.Text, ddlbebantipe.SelectedValue, txtboxtanggalawal.Text, txtboxtanggalakhir.Text);

            gvbeban.DataSource = dtpenjualan;
            gvbeban.DataBind();
        }

        public virtual DataTable GetDataTransBiaya()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transbiaya in context.TRANS_BIAYA

                         select new
                         {
                             transbiaya.TRANS_NO,
                             transbiaya.TANGGAL,
                             DESCR =
                             (
                                transbiaya.DESCR == "BL" ? "Beban Listrik" :
                                transbiaya.DESCR == "BI" ? "Beban Internet" :
                                transbiaya.DESCR == "BA" ? "Beban Air" :
                                transbiaya.DESCR == "BP" ? "Beban Pajak" :
                                transbiaya.DESCR == "BDLL" ? "Beban Lain-lain" :
                                transbiaya.DESCR == "BAD" ? "Beban Administrasi" :
                                "Error"
                             ),
                             transbiaya.AMT,
                             transbiaya.TRANS_BIAYA_ID
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataBiaya(string transno, string tipebeban, string dateawal, string dateakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transbiaya in context.TRANS_BIAYA
                         where transbiaya.TRANS_NO.Contains(transno) &
                         transbiaya.DESCR.Contains(tipebeban) &
                         transbiaya.TANGGAL >= dateawal1 & transbiaya.TANGGAL <= dateakhir1

                         select new
                         {
                             transbiaya.TRANS_NO,
                             transbiaya.TANGGAL,
                             DESCR =
                             (
                                transbiaya.DESCR == "BL" ? "Beban Listrik" :
                                transbiaya.DESCR == "BI" ? "Beban Internet" :
                                transbiaya.DESCR == "BA" ? "Beban Air" :
                                transbiaya.DESCR == "BP" ? "Beban Pajak" :
                                transbiaya.DESCR == "BDLL" ? "Beban Lain-lain" :
                                "Error"
                             ),
                             transbiaya.AMT,
                             transbiaya.TRANS_BIAYA_ID
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
            Response.Redirect("AddBiaya.aspx");
        }

        protected void gvbeban_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 bebanid;

            if (e.CommandName == "ED")
            {
                bebanid = Int64.Parse(((Label)gvbeban.Rows[index].FindControl("lb_trans_id")).Text);
                string bebanid2 = Convert.ToString(bebanid);

                TRANS_BIAYA hutang = GetDataBebanbyid(bebanid);

                Session["id"] = userid;
                Session["bebanid"] = bebanid2;
                Response.Redirect("EditBiaya.aspx");
            }

            if (e.CommandName == "DEL")
            {
                bebanid = Int64.Parse(((Label)gvbeban.Rows[index].FindControl("lb_trans_id")).Text);
                string bebanid2 = Convert.ToString(bebanid);

                TRANS_BIAYA biaya = GetDataBebanbyid(bebanid);
                List<JURNAL> jurnal = GetDatajurnalbiaya(bebanid);

                foreach (JURNAL j in jurnal)
                {
                    JURNAL ju = new JURNAL();
                    ju.JURNAL_ID = j.JURNAL_ID;
                    context.Entry(ju).State = EntityState.Deleted;
                }
                TRANS_BIAYA biaya1 = new TRANS_BIAYA();
                biaya1.TRANS_BIAYA_ID = biaya.TRANS_BIAYA_ID;

                context.Entry(biaya1).State = EntityState.Deleted;
                context.SaveChanges();

                DataTable dtpenjualan = GetDataTransBiaya();

                gvbeban.DataSource = dtpenjualan;
                gvbeban.DataBind();
            }

            if (e.CommandName == "DTL")
            {
                bebanid = Int64.Parse(((Label)gvbeban.Rows[index].FindControl("lb_trans_id")).Text);
                string bebanid2 = Convert.ToString(bebanid);

                TRANS_BIAYA hutang = GetDataBebanbyid(bebanid);

                Session["id"] = userid;
                Session["bebanid"] = bebanid2;
                Response.Redirect("DetailBeban.aspx");
            }

        }

        public virtual TRANS_BIAYA GetDataBebanbyid(Int64 bebanid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_BIAYA biaya = (from j in context.TRANS_BIAYA where j.TRANS_BIAYA_ID == bebanid select j).FirstOrDefault();

            return biaya;
        }

        public virtual List<JURNAL> GetDatajurnalbiaya(Int64 bebanid)
        {
            LAISONEntities context = new LAISONEntities();

            List<JURNAL> biaya = (from j in context.JURNALs where j.TRANS_BIAYA_ID == bebanid select j).ToList();

            return biaya;
        }


    }
}