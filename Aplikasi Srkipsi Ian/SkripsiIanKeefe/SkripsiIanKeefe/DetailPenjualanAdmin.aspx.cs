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
    public partial class DetailPenjualanAdmin : System.Web.UI.Page
    {
        #region page load
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
            REF_USER user = GetUser(transpenjualan.REF_USER_ID);

            lblusername.Text = user.USERNAME;
            lblalamat.Text = transpenjualan.ALAMAT + ", " + tujuan.CITY_NAME + ", " + tujuan.PROVINCE;
            lbltransno.Text = transpenjualan.TRANS_NO;
            lblnamacust.Text = transpenjualan.NAMA_PENERIMA;
            lblongkirnama.Text = transpenjualan.ONGKIR_NAMA;
            lblongkirtype.Text = transpenjualan.ONGKIR_TYPE;
            lbltotalharga.Text = String.Format("{0:Rp 0,0.00}", transpenjualan.TOTAL);
            lbltotalongkir.Text = String.Format("{0:Rp 0,0.00}", transpenjualan.HARGA_ONGKIR);
            lblphone.Text = transpenjualan.PHONE_NUMBER;

            if(transpenjualan.STATUS == "PD_REQ")
            {
                lblstatus.Text = "Menunggu Pembayaran";
            }
            else if (transpenjualan.STATUS == "PD_APV_REQ")
            {
                lblstatus.Text = "Menunggu Konfirmasi Pembayaran";

                hype_bukti.NavigateUrl = transpenjualan.BUKTI_TRANSAKSI;

                upbukti.Visible = true;
                upbukti.Update();
            }
            else if (transpenjualan.STATUS == "PAID")
            {
                lblstatus.Text = "Terbayar";

                hype_bukti.NavigateUrl = transpenjualan.BUKTI_TRANSAKSI;

                upbukti.Visible = true;
                upbukti.Update();
            }
            else if (transpenjualan.STATUS == "SENT")
            {
                lblstatus.Text = "Terkirim";

                hype_resi.NavigateUrl = transpenjualan.RESI_PENGIRIMAN;

                upresi.Visible = true;
                upresi.Update();
            }
            else if (transpenjualan.STATUS == "FINISH")
            {
                lblstatus.Text = "Diterima";
                if (transpenjualan.NOTES != null)
                {
                    lblnotes.Text = transpenjualan.NOTES;
                    upreason.Visible = true;
                    upreason.Update();
                }
            }
            else if (transpenjualan.STATUS == "CAN_REQ")
            {
                lblstatus.Text = "Request Pembatalan";
            }
            else if (transpenjualan.STATUS == "CAN")
            {
                lblstatus.Text = "Dibatalkan";
            }
            else if (transpenjualan.STATUS == "REQ_REFUND")
            {
                lblstatus.Text = "Mengajukan Refund";
            }

            if (transpenjualan.TANGGAL_BAYAR == null)
            {
                lbltanggalterbayar.Text = "";
            }

            else
            {
                DateTime tanggalbayar = Convert.ToDateTime(transpenjualan.TANGGAL_BAYAR);
                lbltanggalterbayar.Text = tanggalbayar.ToString("dddd, dd MMMM yyyy");
            }

            if (transpenjualan.TANGGAL_TERIMA == null)
            {
                lbltanggalterima.Text = "";
            }

            else
            {
                DateTime tanggalterima = Convert.ToDateTime(transpenjualan.TANGGAL_TERIMA);
                lbltanggalterima.Text = tanggalterima.ToString("dddd, dd MMMM yyyy");
            }

            DateTime tanggal = Convert.ToDateTime(transpenjualan.TANGGAL);
            lbltanggal.Text = tanggal.ToString("dddd, dd MMMM yyyy");


        }
        #endregion

        #region method
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


        public virtual TRANS_PENJUALAN GetDataTransPenjualan(Int64 transid)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];

            TRANS_PENJUALAN trans = (from j in context.TRANS_PENJUALAN
                                     where j.TRANS_PENJUALAN_ID == transid
                                     select j).FirstOrDefault();

            return trans;
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