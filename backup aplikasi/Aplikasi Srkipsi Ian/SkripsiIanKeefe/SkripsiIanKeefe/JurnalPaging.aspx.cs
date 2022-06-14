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
    public partial class JurnalPaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();

        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dtjurnal = GetDataJurnal(Convert.ToInt32(txtboxjurnalbulan.Text), Convert.ToInt32(txtboxtahun.Text));

            DataTable dtneraca = GetDataNeraca(Convert.ToInt32(txtboxjurnalbulan.Text), Convert.ToInt32(txtboxtahun.Text));

            gvjurnal.DataSource = dtjurnal;
            gvjurnal.DataBind();

            gvneraca.DataSource = dtneraca;
            gvneraca.DataBind();
            
        }

        public virtual DataTable GetDataNeraca(Int32 bulan, Int32 tahun)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from jurnal in context.JURNALs
                         join coa in context.COAs
                         on jurnal.COA_ID equals coa.COA_ID
                         where jurnal.TANGGAL.Value.Year == tahun & jurnal.TANGGAL.Value.Month == bulan
                         orderby jurnal.JURNAL_ID
                         select new
                         {
                             jurnal.DESCR,
                             coa.COA_NO,
                             jurnal.IN_AMT,
                             jurnal.OUT_AMT
                         }).GroupBy(a => new { a.DESCR, a.COA_NO }, (key, group) => new
                         {
                             COA_NO = key.COA_NO,
                             DESCR = key.DESCR,
                             SUM_IN_AMT =
                               (
                                  group.Sum(z => z.IN_AMT) - group.Sum(z => z.OUT_AMT) > 0 ? group.Sum(z => z.IN_AMT) - group.Sum(z => z.OUT_AMT) :
                                  group.Sum(z => z.OUT_AMT) == null ? group.Sum(z => z.IN_AMT) : null
                               ),
                             SUM_OUT_AMT =
                               (
                                 group.Sum(z => z.OUT_AMT) - group.Sum(z => z.IN_AMT) > 0 ? group.Sum(z => z.OUT_AMT) - group.Sum(z => z.IN_AMT) :
                                 group.Sum(z => z.IN_AMT) == null ? group.Sum(z => z.OUT_AMT) : null
                               )
                         });



            DataTable dt = LINQResultToDataTable(query);

            var totaldebit = query.Sum(a => a.SUM_IN_AMT);

            var totalcredit = query.Sum(a => a.SUM_OUT_AMT);

            lbl_debit_neraca.Text = String.Format("{0:Rp 0,0.00}", totaldebit);

            lbl_kredit_neraca.Text = String.Format("{0:Rp 0,0.00}", totalcredit);

            return dt;          
        }

        public virtual DataTable GetDataJurnal(Int32 bulan, Int32 tahun)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from jurnal in context.JURNALs
                         join coa in context.COAs
                         on jurnal.COA_ID equals coa.COA_ID
                         where jurnal.TANGGAL.Value.Year == tahun & jurnal.TANGGAL.Value.Month == bulan
                         orderby jurnal.TANGGAL
                         select new
                         {
                             jurnal.IN_AMT,
                             jurnal.OUT_AMT,
                             jurnal.DESCR,
                             coa.COA_NO,
                             jurnal.TANGGAL
                         }).AsEnumerable().Select(x => new
                         {
                             x.COA_NO,
                             x.DESCR,
                             x.IN_AMT,
                             x.OUT_AMT,
                             TANGGAL = 
                             (
                                x.IN_AMT == null ? "":
                                x.IN_AMT != null ? x.TANGGAL.Value.ToString("MM/dd/yyyy") : "Error"
                             )
                         });

            var totaldebit = query.Sum(a => a.IN_AMT);

            var totalcredit = query.Sum(a => a.OUT_AMT);

            lbltotaldebit.Text = String.Format("{0:Rp 0,0.00}", totaldebit);

            lbltotalkredit.Text = String.Format("{0:Rp 0,0.00}", totalcredit);

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
    }
}