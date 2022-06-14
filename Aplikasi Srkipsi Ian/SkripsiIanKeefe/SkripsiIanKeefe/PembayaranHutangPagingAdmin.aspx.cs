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
    public partial class PembayaranHutangPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];

                DataTable dthutang = GetDataTransHutang();

                if (gvhutang.Rows.Count == 0)
                {
                    gvhutang.DataSource = dthutang;
                    gvhutang.DataBind();
                }               
            }
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dthutang = GetDataHutang(txtboxtransnopmbyrn.Text, txtboxtransnohutang.Text, txtboxtanggalawal.Text, txtboxtanggalakhir.Text);

            gvhutang.DataSource = dthutang;
            gvhutang.DataBind();
        }

        public virtual DataTable GetDataTransHutang()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transhutang in context.TRANS_HUTANG
                         join transpmbyrn in context.TRANS_PEMBAYARAN_HUTANG
                         on transhutang.TRANS_HUTANG_ID equals transpmbyrn.TRANS_HUTANG_ID
                         let TRANS_NO_HUTANG = transhutang.TRANS_NO
                         select new
                         {
                             transpmbyrn.TRANS_NO,
                             transpmbyrn.TOTAL,
                             transpmbyrn.TANGGAL,
                             TRANS_NO_HUTANG,
                             transpmbyrn.TRANS_BYR_UTANG_ID
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataHutang(string transnopmbyrn, string transnohutang, string dateawal, string dateakhir)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transhutang in context.TRANS_HUTANG
                         join transpmbyrn in context.TRANS_PEMBAYARAN_HUTANG
                         on transhutang.TRANS_HUTANG_ID equals transpmbyrn.TRANS_HUTANG_ID
                         let TRANS_NO_HUTANG = transhutang.TRANS_NO
                         where TRANS_NO_HUTANG.Contains(transnohutang) &
                         transpmbyrn.TRANS_NO.Contains(transnopmbyrn) &
                         transpmbyrn.TANGGAL >= dateawal1 & transpmbyrn.TANGGAL <= dateakhir1
                         select new
                         {
                             transpmbyrn.TRANS_NO,
                             transpmbyrn.TOTAL,
                             transpmbyrn.TANGGAL,
                             TRANS_NO_HUTANG,
                             transpmbyrn.TRANS_BYR_UTANG_ID
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

        protected void gvhutang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 hutangid;

            if (e.CommandName == "ED")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_PEMBAYARAN_HUTANG hutang = GetDataPembayaranHutangbyid(hutangid);
                Session["id"] = userid;
                Session["hutangid"] = hutangid2;
                Response.Redirect("EditPembayaranHutang.aspx");
            }

            if (e.CommandName == "DTL")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_PEMBAYARAN_HUTANG hutang = GetDataPembayaranHutangbyid(hutangid);
                Session["id"] = userid;
                Session["hutangid"] = hutangid2;
                Response.Redirect("DetailPembayaranHutang.aspx");
            }

            if (e.CommandName == "DTL_HUTANG")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_PEMBAYARAN_HUTANG hutang = GetDataPembayaranHutangbyid(hutangid);
                Session["id"] = userid;
                Session["hutangid"] = Convert.ToString(hutang.TRANS_HUTANG_ID);
                Response.Redirect("DetailHutang.aspx");
            }

            if (e.CommandName == "DEL")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_PEMBAYARAN_HUTANG hutang = GetDataPembayaranHutangbyid(hutangid);

                List<JURNAL> jurnal = GetDatajurnalpmbyrn(hutangid);

                foreach (JURNAL j in jurnal)
                {
                    JURNAL ju = new JURNAL();
                    ju.JURNAL_ID = j.JURNAL_ID;
                    context.Entry(ju).State = EntityState.Deleted;
                }

                TRANS_PEMBAYARAN_HUTANG hutang1 = new TRANS_PEMBAYARAN_HUTANG();
                hutang1.TRANS_BYR_UTANG_ID = hutang.TRANS_BYR_UTANG_ID;

                TRANS_HUTANG transhutang = GetDataHutangbyid(Convert.ToInt64(hutang.TRANS_HUTANG_ID));

                Int64 transhutangid = Convert.ToInt64(transhutang.TRANS_HUTANG_ID);

                using (var context1 = new LAISONEntities())
                {

                    var edithutang = context1.TRANS_HUTANG.Where(u => u.TRANS_HUTANG_ID == transhutangid).FirstOrDefault();

                    edithutang.SISA = edithutang.SISA + hutang.TOTAL;
                    edithutang.IS_ACTIVE = "ACT";

                    context1.SaveChanges();
                }

                context.Entry(hutang1).State = EntityState.Deleted;
                context.SaveChanges();

                DataTable dthutang = GetDataTransHutang();

                gvhutang.DataSource = dthutang;
                gvhutang.DataBind();

            }
        }

        public virtual TRANS_PEMBAYARAN_HUTANG GetDataPembayaranHutangbyid(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_PEMBAYARAN_HUTANG a = (from j in context.TRANS_PEMBAYARAN_HUTANG
                                         where j.TRANS_BYR_UTANG_ID == hutangid
                                         select j).FirstOrDefault();
            return a;
        }

        public virtual List<JURNAL> GetDatajurnalpmbyrn(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            List<JURNAL> biaya = (from j in context.JURNALs where j.TRANS_BYR_UTANG_ID == hutangid select j).ToList();

            return biaya;
        }

        public virtual TRANS_HUTANG GetDataHutangbyid(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG a = (from j in context.TRANS_HUTANG
                              where j.TRANS_HUTANG_ID == hutangid
                              select j).FirstOrDefault();
            return a;
        }
    }
}