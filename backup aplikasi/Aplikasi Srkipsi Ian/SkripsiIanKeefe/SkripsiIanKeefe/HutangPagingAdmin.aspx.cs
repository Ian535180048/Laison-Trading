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
    public partial class HutangPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
            string userid = Convert.ToString(id);
            LAISONEntities context = new LAISONEntities();

            DataTable dthutang = GetDataHutang(txtboxtransno.Text, txtclientnama.Text, txtboxtanggalawal.Text, txtboxtanggalakhir.Text, ddllunas.SelectedValue, txtboxclientno.Text);

            gvhutang.DataSource = dthutang;
            gvhutang.DataBind();
        }

        public virtual DataTable GetDataTransHutang()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from transhutang in context.TRANS_HUTANG
                         join clients in context.CLIENTs
                         on transhutang.CLIENT_ID equals clients.CLIENT_ID
                         select new
                         {
                             IS_ACTIVE =
                             (
                                transhutang.IS_ACTIVE == "ACT" ? "Aktif" :
                                transhutang.IS_ACTIVE == "EXP" ? "Lunas" :
                                "Error"
                             ),
                             transhutang.TANGGAL,
                             transhutang.TRANS_NO,
                             transhutang.SISA,
                             transhutang.HUTANG_AMT,
                             clients.CLIENT_NO,
                             clients.CLIENT_NAME,
                             transhutang.TRANS_HUTANG_ID
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataHutang(string transno, string clientname,  string dateawal, string dateakhir, string active, string clientno)
        {
            LAISONEntities context = new LAISONEntities();
            DateTime dateawal1 = Convert.ToDateTime(dateawal);
            DateTime dateakhir1 = Convert.ToDateTime(dateakhir);

            var query = (from transhutang in context.TRANS_HUTANG
                         join clients in context.CLIENTs
                         on transhutang.CLIENT_ID equals clients.CLIENT_ID
                         where transhutang.TRANS_NO.Contains(transno) &
                         transhutang.TANGGAL >= dateawal1 & transhutang.TANGGAL <= dateakhir1 &
                         clients.CLIENT_NAME.Contains(clientname) & clients.CLIENT_NO.Contains(clientno) &
                         transhutang.IS_ACTIVE.Contains(active)
                         select new
                         {
                             IS_ACTIVE =
                             (
                                transhutang.IS_ACTIVE == "ACT" ? "Aktif" :
                                transhutang.IS_ACTIVE == "EXP" ? "Lunas" :
                                "Error"
                             ),
                             transhutang.TANGGAL,
                             transhutang.TRANS_NO,
                             transhutang.SISA,
                             transhutang.HUTANG_AMT,
                             clients.CLIENT_NO,
                             clients.CLIENT_NAME,
                             transhutang.TRANS_HUTANG_ID
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
            Session["id"] = id;
            Response.Redirect("AddHutang.aspx");
        }

        protected void gvhutang_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 hutangid;

            if (e.CommandName == "PMBYRN")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_HUTANG hutang = GetDataHutangbyid(hutangid);

                if (hutang.IS_ACTIVE == "EXP" && hutang.SISA == Convert.ToDecimal(0))
                {
                    lblerrorhutang.Visible = true;
                }

                else
                {
                    Session["id"] = userid;
                    Session["hutangid"] = hutangid2;

                    Response.Redirect("AddPembayaranHutang.aspx");
                }
            }

            if (e.CommandName == "ED")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_HUTANG hutang = GetDataHutangbyid(hutangid);

                List<TRANS_PEMBAYARAN_HUTANG> pmbyaran = GetDataPembayaranById(hutangid);

                if (hutang.IS_ACTIVE == "EXP")
                {
                    lblerrorhutang.Visible = true;
                }

                else if (pmbyaran.Count > 0)
                {
                    lblerrorpmbyrn.Visible = true;
                }

                else
                {
                    Session["id"] = userid;
                    Session["hutangid"] = hutangid2;

                    Response.Redirect("EditHutang.aspx");
                }
            }

            if (e.CommandName == "DEL")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                TRANS_HUTANG hutang = GetDataHutangbyid(hutangid);

                List<TRANS_PEMBAYARAN_HUTANG> pmbyaran = GetDataPembayaranById(hutangid);

                if (hutang.IS_ACTIVE == "EXP")
                {
                    lblerrorhutang.Visible=true;
                }

                else if (pmbyaran.Count > 0)
                {
                    lblerrorpmbyrn.Visible = true;
                }

                else
                {
                    List<JURNAL> jurnal = GetDatajurnalhutang(hutangid);

                    foreach (JURNAL j in jurnal)
                    {
                        JURNAL ju = new JURNAL();
                        ju.JURNAL_ID = j.JURNAL_ID;
                        context.Entry(ju).State = EntityState.Deleted;
                    }
                    TRANS_HUTANG hutang1 = new TRANS_HUTANG();
                    hutang1.TRANS_HUTANG_ID = hutang.TRANS_HUTANG_ID;

                    context.Entry(hutang1).State = EntityState.Deleted;
                    context.SaveChanges();

                    DataTable dthutang = GetDataTransHutang();

                    gvhutang.DataSource = dthutang;
                    gvhutang.DataBind();
                }
            }
            if (e.CommandName == "DTL")
            {
                hutangid = Int64.Parse(((Label)gvhutang.Rows[index].FindControl("lbl_trans_id")).Text);
                string hutangid2 = Convert.ToString(hutangid);

                Session["id"] = userid;
                Session["hutangid"] = hutangid2;

                Response.Redirect("DetailHutang.aspx");
            }
        }
        public virtual TRANS_HUTANG GetDataHutangbyid(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG a = (from j in context.TRANS_HUTANG
                              where j.TRANS_HUTANG_ID == hutangid
                             select j).FirstOrDefault();
            return a;
        }

        public virtual List<JURNAL> GetDatajurnalhutang(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            List<JURNAL> biaya = (from j in context.JURNALs where j.TRANS_HUTANG_ID == hutangid select j).ToList();

            return biaya;
        }

        public virtual List<TRANS_PEMBAYARAN_HUTANG> GetDataPembayaranById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            List<TRANS_PEMBAYARAN_HUTANG> hutang = (from j in context.TRANS_PEMBAYARAN_HUTANG where j.TRANS_HUTANG_ID == hutangid select j).ToList();

            return hutang;
        }
    }
}