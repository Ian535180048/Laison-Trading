using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkripsiIanKeefe.Entities;
using System.Data;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json;
using SkripsiIanKeefe.API;


namespace SkripsiIanKeefe
{
    public partial class CheckoutPembayaran : System.Web.UI.Page
    {
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                LAISONEntities context = new LAISONEntities();
                DataTable pmbyrn = GetDataPembayaran(Convert.ToInt64(id));

                gvpembayaran.DataSource = pmbyrn;
                gvpembayaran.DataBind();

                DataTable dt =  BindCity();

                dlltujuan.DataSource = dt;
                dlltujuan.DataTextField = "city_name";
                dlltujuan.DataValueField = "city_id";
                dlltujuan.DataBind();

                PLACE tujuan = GetKota(dlltujuan.SelectedValue);

                DataTable tipe = GetDataHargaOngkir(Convert.ToInt64(1), Convert.ToDecimal(lblberatproduk.Text), ddlkurir.SelectedValue);

                ddltipeongkir.DataSource = tipe;
                ddltipeongkir.DataTextField = "etd";
                ddltipeongkir.DataValueField = "value";
                ddltipeongkir.DataBind();

                lblhargaongkir.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue));
                uphargaongkir.Update();

                
                lbltotalsemua.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue) + Convert.ToDecimal(lbltotallempar.Text));

                uptotal.Update();
            }
        }
        #endregion

        #region toolbar
        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));

            foreach (PEMBAYARAN pm in pmbyrn)
            {
                PEMBAYARAN pmb = new PEMBAYARAN();
                pmb.pembayaran_id = pm.pembayaran_id;
                context.Entry(pmb).State = EntityState.Deleted;
                context.SaveChanges();
            }
            Session["id"] = id;
            Response.Redirect("Cart.aspx");

        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            TRANS_PENJUALAN jual = new TRANS_PENJUALAN();
            TRANS_PENJUALAN transjualid = GetDataTransPenjualanbyIdLatest();
            if (transjualid == null)
            {
                jual.TRANS_NO = Convert.ToString(DateTime.Today.Year) + Convert.ToString("JL") + "0001";
            }
            else
            {
                jual.TRANS_NO = Convert.ToString(DateTime.Today.Year) + Convert.ToString("JL") + transjualid.TRANS_PENJUALAN_ID.ToString("0000");
            }
            jual.TANGGAL = Convert.ToDateTime(DateTime.Today);
            jual.TOTAL = Convert.ToDecimal(lbltotallempar.Text);
            jual.STATUS = "PD_REQ";
            jual.REF_USER_ID = Convert.ToInt64(id);
            jual.ALAMAT = txtboxalamat.Text;
            jual.TOTAL = Convert.ToDecimal(lbltotallempar.Text);
            jual.PHONE_NUMBER = txtboxphonenumber.Text;

            PLACE tujuan = GetKota(dlltujuan.SelectedValue);
            jual.NAMA_PENERIMA = txtboxnamapenerima.Text;
            jual.ONGKIR_NAMA = Convert.ToString(ddlkurir.SelectedItem);
            jual.HARGA_ONGKIR = Convert.ToDecimal(ddltipeongkir.SelectedValue);
            jual.ONGKIR_TYPE = Convert.ToString(ddltipeongkir.SelectedItem);
            jual.PLACE_ID = Convert.ToInt32(tujuan.CITY_ID);

            List<CHECKOUT> listcheckout = new List<CHECKOUT>();
            int i = 1;
            foreach (GridViewRow row in gvpembayaran.Rows)
            {

                CHECKOUT chk = new CHECKOUT();

                Label prodid1 = (row.Cells[i].FindControl("lblprodid") as Label);
                Label prodidharga = (row.Cells[i].FindControl("lbl_harga") as Label);
                Label amtprod = (row.Cells[i].FindControl("lbL_amount") as Label);

                chk.PROD_ID = Convert.ToInt64(prodid1.Text);
                string harga = String.Format("{0}", prodidharga.Text.Remove(0, 2));
                harga = harga.Replace(@",", string.Empty);
                chk.HARGA_PRODUK = Convert.ToDecimal(harga);
                chk.JUMLAH_BARANG = Convert.ToInt32(amtprod.Text);
                i++;
                jual.CHECKOUTs.Add(chk);               
            }
            context.TRANS_PENJUALAN.Add(jual);
            context.SaveChanges();
            i = 1;
            foreach (GridViewRow row in gvpembayaran.Rows)
            {
                Label prodid1 = (row.Cells[i].FindControl("lblprodid") as Label);
                Label prodidharga = (row.Cells[i].FindControl("lbl_harga") as Label);
                Label amtprod = (row.Cells[i].FindControl("lbL_amount") as Label);

                PRODUCT prod = GetDataProductbyid(Convert.ToInt64(prodid1.Text));

                UpdateStokAmt(prod, amtprod.Text);
                i++;
            }

            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));

            foreach (PEMBAYARAN pm in pmbyrn)
            {
                PEMBAYARAN pmb = new PEMBAYARAN();
                CART cart = new CART();

                pmb.pembayaran_id = pm.pembayaran_id;
                cart.CART_ID = pm.CART_ID;
                context.Entry(pmb).State = EntityState.Deleted;
                context.SaveChanges();

                context.Entry(cart).State = EntityState.Deleted;
                context.SaveChanges();
            }
            Session["id"] = id;
            Response.Redirect("SuccessPembayaran.aspx");

        }
        #endregion

        #region index
        protected void ddlkurir_SelectedIndexChanged(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];

            PLACE tujuan = GetKota(dlltujuan.SelectedValue);

            DataTable tipe = GetDataHargaOngkir(Convert.ToInt64(tujuan.CITY_ID), Convert.ToDecimal(lblberatproduk.Text), ddlkurir.SelectedValue);

            ddltipeongkir.DataSource = tipe;
            ddltipeongkir.DataTextField = "etd";
            ddltipeongkir.DataValueField = "value";
            ddltipeongkir.DataBind();

            lblhargaongkir.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue));
            uphargaongkir.Update();


            lbltotalsemua.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue) + Convert.ToDecimal(lbltotallempar.Text));

            uptotal.Update();
        }

        protected void dlltujuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];

            PLACE tujuan = GetKota(dlltujuan.SelectedValue);

            DataTable tipe = GetDataHargaOngkir(Convert.ToInt64(tujuan.CITY_ID), Convert.ToDecimal(lblberatproduk.Text), ddlkurir.SelectedValue);

            ddltipeongkir.DataSource = tipe;
            ddltipeongkir.DataTextField = "etd";
            ddltipeongkir.DataValueField = "value";
            ddltipeongkir.DataBind();

            lblhargaongkir.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue));
            uphargaongkir.Update();


            lbltotalsemua.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue) + Convert.ToDecimal(lbltotallempar.Text));

            uptotal.Update();
        }

        protected void ddltipeongkir_SelectedIndexChanged(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];

            lblhargaongkir.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue));
            uphargaongkir.Update();


            lbltotalsemua.Text = String.Format("{0:Rp 0,0.00}", Convert.ToDecimal(ddltipeongkir.SelectedValue) + Convert.ToDecimal(lbltotallempar.Text));

            uptotal.Update();
        }

        #endregion
        #region method

        public void UpdateStokAmt(PRODUCT prod, string amt)
        {
            Int32 amtprod = Convert.ToInt32(amt);
            using (var context = new LAISONEntities())
            {
                var product = context.PRODUCTs.Where(u => u.PROD_ID == prod.PROD_ID).FirstOrDefault();

                product.STOCK_TOTAL = prod.STOCK_TOTAL - amtprod;
                context.SaveChanges();
            }
        }

        public virtual PRODUCT GetDataProductbyid(Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT stok = (from j in context.PRODUCTs where j.PROD_ID == prodid select j).FirstOrDefault();

            return stok;
        }

        public virtual DataTable GetDataPembayaran(Int64 id)
        {
            var userid = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            var query = (from cart in context.CARTs
                         join prod in context.PRODUCTs on cart.PROD_ID equals prod.PROD_ID
                         join pembayaran in context.PEMBAYARANs on cart.CART_ID equals pembayaran.CART_ID
                         where cart.REF_USER_ID == id
                         select new
                         {
                             cart.CART_ID,
                             prod.PROD_ID,
                             prod.PROD_NAME,
                             prod.PROD_NO,
                             TOTAL = prod.PRICE * cart.AMMOUNT,
                             pembayaran.pembayaran_id,
                             cart.AMMOUNT,
                             prod.PRICE,
                             prod.BERAT_PRODUK
                         }).ToList();

            var totalharga = query.Sum(a => a.PRICE * a.AMMOUNT);
            var totalhargalempar = query.Sum(a => a.PRICE * a.AMMOUNT);
            var beratproduk = query.Sum(a => a.BERAT_PRODUK);
            
            lbltotalharga.Text = String.Format("{0:Rp 0,0.00}", totalharga);
            lbltotallempar.Text = totalhargalempar.ToString();
            lblberatproduk.Text = beratproduk.ToString();

            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable BindCity()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from j in context.PLACEs
                         select j).ToList();

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




        public virtual PLACE GetKota(string tujuan)
        {
            LAISONEntities context = new LAISONEntities();
            Int64 kota = Convert.ToInt64(tujuan);
            PLACE query = (from j in context.PLACEs
                           where j.CITY_ID == kota
                           select j).FirstOrDefault();

            return query;
        }


        public virtual DataTable GetDataHargaOngkir(Int64 city, Decimal berat, string ongkir)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();
            string citystring = Convert.ToString(city);
            string beratproduk = Convert.ToString(berat);

            var client = new RestClient("https://api.rajaongkir.com/starter/cost");
            var request = new RestRequest(Method.POST);
            request.AddHeader("key", "7aedd9edf72b61fb1edfc0b6fe1c473f");
            request.AddParameter("application/x-www-form-urlencoded", "origin=151&destination=" + citystring + "&weight="+beratproduk+"&courier="+ongkir, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Root content = JsonConvert.DeserializeObject<Root>(response.Content);

            Result result = (from harga in content.rajaongkir.results
                             select harga).FirstOrDefault();

            var costs1 = (from costs in result.costs
                                  select costs).ToList();

            var listcost = costs1.SelectMany(cost => cost.cost, (service, costvalue) => new { service.service, costvalue.value, etd =(service.service +" "+ costvalue.etd + " hari") });


            DataTable dt = LINQResultToDataTable(listcost);

            return dt;
        }



        public virtual List<PEMBAYARAN> GetDataPembayaranCart(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            List<PEMBAYARAN> query = (from pmbyrn in context.PEMBAYARANs
                                      join cart in context.CARTs on pmbyrn.CART_ID equals cart.CART_ID
                                      where cart.REF_USER_ID == id
                                      select pmbyrn
                                      ).ToList();

            return query;
        }

        public virtual TRANS_PENJUALAN GetDataTransPenjualanbyIdLatest()
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_PENJUALAN a = (from j in context.TRANS_PENJUALAN
                                 orderby j.TRANS_PENJUALAN_ID descending
                                 select j).FirstOrDefault();

            return a;
        }


        #endregion

        
    }
}