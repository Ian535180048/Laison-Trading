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
    public partial class ApproveRefund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var jualid = (string)Session["jualid"];
            Int64 halo = new Int64();

            DataTable dtproduct = GetDataProduct(Convert.ToInt64(jualid));
            gvproduct.DataSource = dtproduct;
            gvproduct.DataBind();

            TRANS_PENJUALAN transpenjualan = GetDataTransPenjualan(Convert.ToInt64(jualid));
            PLACE tujuan = GetKota(Convert.ToString(transpenjualan.PLACE_ID));
            REF_USER user = GetUser(Convert.ToInt64(transpenjualan.REF_USER_ID));

            lblusername.Text = user.USERNAME;
            lblalamat.Text = transpenjualan.ALAMAT + ", " + tujuan.CITY_NAME + ", " + tujuan.PROVINCE;
            lbltransno.Text = transpenjualan.TRANS_NO;
            lblnamacust.Text = transpenjualan.NAMA_PENERIMA;
            lblongkirnama.Text = transpenjualan.ONGKIR_NAMA;
            lblongkirtype.Text = transpenjualan.ONGKIR_TYPE;
            lbltotalharga.Text = String.Format("{0:Rp 0,0.00}", transpenjualan.TOTAL);
            lbltotalongkir.Text = String.Format("{0:Rp 0,0.00}", transpenjualan.HARGA_ONGKIR);
            lblstatus.Text = "Mengajukan Refund";
            lblrefund.Text = transpenjualan.NOTES;
            lblphone.Text = transpenjualan.PHONE_NUMBER;
            if(transpenjualan.BUKTI_REFUND == null)
            {
                hype_bukti.Text = "Tidak ada bukti refund";
            }
            else
            {
                hype_bukti.NavigateUrl = transpenjualan.BUKTI_REFUND;
            }

            if (transpenjualan.TANGGAL_BAYAR == null)
            {
                lbltanggalterbayar.Text = "";
            }

            else
            {
                DateTime tanggalbayar = Convert.ToDateTime(transpenjualan.TANGGAL_BAYAR);
                lbltanggalterbayar.Text = tanggalbayar.ToString("MM/dd/yyyy");
            }

            DateTime tanggal = Convert.ToDateTime(transpenjualan.TANGGAL);
            lbltanggal.Text = tanggal.ToString("MM/dd/yyyy");

        }

        protected void lbapprove_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var jualid = (string)Session["jualid"];

            TRANS_PENJUALAN trans = GetDataTransPenjualan(Convert.ToInt64(jualid));

            EditTransAPV(trans);
            TRANS_PENJUALAN penjualan = GetDataTransPenjualan(Convert.ToInt64(jualid));



            Session["id"] = id;
            Response.Redirect("PagingApproveRefundTransaksi.aspx");
        }

        protected void lbrjc_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var transid = (string)Session["transid"];

            TRANS_PENJUALAN trans = GetDataTransPenjualan(Convert.ToInt64(transid));

            EditTransRJC(trans);
            TRANS_PENJUALAN penjualan = GetDataTransPenjualan(Convert.ToInt64(transid));

            Session["id"] = id;
            Response.Redirect("PagingApproveRefundTransaksi.aspx");
        }

        public virtual void EditTransAPV(TRANS_PENJUALAN trans)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.TRANS_PENJUALAN.Where(u => u.TRANS_PENJUALAN_ID == trans.TRANS_PENJUALAN_ID).FirstOrDefault();
                penjualan.STATUS = "FINISH";
                penjualan.NOTES = "Refund Telah Disetujui";
                if(penjualan.TANGGAL_TERIMA != null)
                {

                }
                else
                {
                    penjualan.TANGGAL_TERIMA = DateTime.Today;
                }

                context1.SaveChanges();
            }
        }

        public virtual void EditTransRJC(TRANS_PENJUALAN trans)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.TRANS_PENJUALAN.Where(u => u.TRANS_PENJUALAN_ID == trans.TRANS_PENJUALAN_ID).FirstOrDefault();
                penjualan.STATUS = "FINISH";
                penjualan.NOTES = "Refund Tidak Disetujui";
                context1.SaveChanges();
            }
        }

        public virtual REF_USER GetUser(Int64 iduser)
        {
            LAISONEntities context = new LAISONEntities();

            REF_USER query = (from j in context.REF_USER
                              where j.REF_USER_ID == iduser
                              select j).FirstOrDefault();

            return query;
        }

        public virtual PLACE GetKota(string tujuan)
        {
            LAISONEntities context = new LAISONEntities();
            Int64 kota = Convert.ToInt64(tujuan);
            PLACE query = (from j in context.PLACEs
                           where j.CITY_ID == kota
                           select j).FirstOrDefault();

            return query;
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

        public virtual DataTable GetDataProduct(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.TRANS_PENJUALAN
                         join chk in context.CHECKOUTs on trans.TRANS_PENJUALAN_ID equals chk.TRANS_PENJUALAN_ID
                         join prod in context.PRODUCTs on chk.PROD_ID equals prod.PROD_ID
                         where trans.TRANS_PENJUALAN_ID == id
                         select new
                         {
                             prod.PROD_NAME,
                             prod.PROD_ID,
                             prod.PROD_NO,
                             chk.JUMLAH_BARANG,
                             prod.BERAT_PRODUK,
                             chk.HARGA_PRODUK,
                             TOTAL =
                             (
                                chk.JUMLAH_BARANG * chk.HARGA_PRODUK
                             )
                         }).ToList();

            DataTable dt = LINQResultToDataTable(query);
            return dt;
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
    }
}