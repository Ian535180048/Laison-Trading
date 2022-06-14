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
    public partial class ProductPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable dtproduct = GetDataProducts();

                if (gvproduct.Rows.Count == 0)
                {
                    gvproduct.DataSource = dtproduct;
                    gvproduct.DataBind();
                }
            }
            
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dtproduct = GetDataProduct(txtboxproductname.Text, txtboxproductno.Text);

            gvproduct.DataSource = dtproduct;
            gvproduct.DataBind();
        }

        public virtual DataTable GetDataProducts()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from product in context.PRODUCTs
                         select new
                         {
                             product.PROD_NAME,
                             product.PROD_NO,
                             product.STOCK_TOTAL,
                             product.PRICE,
                             product.BERAT_PRODUK,
                             product.PROD_ID,
                             ACTIVE = 
                             (
                                product.IS_ACTIVE == "1"? "Aktif" : "Non Aktif"
                             )
                             
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataProduct(string prodname, string prodno)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from product in context.PRODUCTs
                         where product.PROD_NAME.Contains(prodname) & product.PROD_NO.Contains(prodno)
                         select new
                         {
                             product.PROD_NAME,
                             product.PROD_NO,
                             product.STOCK_TOTAL,
                             product.PRICE,
                             product.BERAT_PRODUK,
                             product.PROD_ID,
                             ACTIVE =
                             (
                                product.IS_ACTIVE == "1" ? "Aktif" : "Non Aktif"
                             )
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
            Session["id"] = userid;
            Response.Redirect("AddProduct.aspx");
        }

        protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 prodid;

            lblerrordelete1.Visible = false;
            lblerrordelete2.Visible = false;
            uperror.Update();

            if (e.CommandName == "ACT")
            {
                prodid = Int64.Parse(((Label)gvproduct.Rows[index].FindControl("lbl_prod_id")).Text);
                string prodid2 = Convert.ToString(prodid);

                PRODUCT prod = GetProdById(prodid);

                EditProd(prod);
                DataTable dtproduct = GetDataProducts();

                gvproduct.DataSource = dtproduct;
                gvproduct.DataBind();

            }

            if (e.CommandName == "DTL")
            {
                prodid = Int64.Parse(((Label)gvproduct.Rows[index].FindControl("lbl_prod_id")).Text);
                string prodid2 = Convert.ToString(prodid);

                PRODUCT prod = GetProdById(prodid);
            
                Session["id"] = userid;
                Session["prodid"] = prodid2;
                Response.Redirect("DetailProduct.aspx");
            }

            if (e.CommandName == "ED")
            {
                prodid = Int64.Parse(((Label)gvproduct.Rows[index].FindControl("lbl_prod_id")).Text);
                string prodid2 = Convert.ToString(prodid);

                PRODUCT prod = GetProdById(prodid);
                Session["id"] = userid;
                Session["prodid"] = prodid2;
                Response.Redirect("EditProduct.aspx");

            }

            if (e.CommandName == "DEL")
            {
                prodid = Int64.Parse(((Label)gvproduct.Rows[index].FindControl("lbl_prod_id")).Text);
                string prodid2 = Convert.ToString(prodid);

                PRODUCT prod = GetProdById(prodid);

                List<TRANS_PENJUALAN> penjualan = GetDataPenjualanById(Convert.ToInt64(prod.PROD_ID));

                List<TRANS_STOCK_BARANG> stok = GetDataStokById(Convert.ToInt64(prod.PROD_ID));

                List<CART> cart = GetDataCartById(Convert.ToInt64(prod.PROD_ID));

                if (penjualan.Count > 0)
                {
                    lblerrordelete1.Visible = true;
                    uperror.Update();
                }

                else if (stok.Count > 0)
                {
                    lblerrordelete2.Visible = true;
                    uperror.Update();
                }

                else
                {
                    foreach (CART carts in cart)
                    {
                        CART cart1 = new CART();
                        cart1.CART_ID = carts.CART_ID;
                        context.Entry(cart1).State = EntityState.Deleted;
                    }

                    PRODUCT prod1 = new PRODUCT();
                    prod1.PROD_ID = prod.PROD_ID;

                    context.Entry(prod1).State = EntityState.Deleted;
                    context.SaveChanges();

                    DataTable dtproduct = GetDataProduct(txtboxproductname.Text, txtboxproductno.Text);

                    gvproduct.DataSource = dtproduct;
                    gvproduct.DataBind();
                }
            }
        }

        public virtual void EditProd(PRODUCT prod)
        {
            using (var context1 = new LAISONEntities())
            {
                var prod1 = context1.PRODUCTs.Where(u => u.PROD_ID == prod.PROD_ID).FirstOrDefault();

                if(prod1.IS_ACTIVE == "1")
                {
                    prod1.IS_ACTIVE = "0";
                }

                else
                {
                    prod1.IS_ACTIVE = "1";
                }

                context1.SaveChanges();

            }
        }

        public virtual PRODUCT GetProdById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT a = (from j in context.PRODUCTs
                         where j.PROD_ID == id
                         select j).FirstOrDefault();
            return a;
        }

        public virtual List<TRANS_PENJUALAN> GetDataPenjualanById(Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            List<TRANS_PENJUALAN> penjualan = (from checkout in context.CHECKOUTs
                                               join transpenjualan in context.TRANS_PENJUALAN
                                                   on checkout.TRANS_PENJUALAN_ID equals transpenjualan.TRANS_PENJUALAN_ID
                                               where checkout.PROD_ID == prodid
                                               select transpenjualan).ToList();

            return penjualan;
        }

        public virtual List<TRANS_STOCK_BARANG> GetDataStokById(Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            List<TRANS_STOCK_BARANG> stokbarang = (from stok in context.TRANS_STOCK_BARANG
                                                  where stok.PROD_ID == prodid
                                                  select stok).ToList();

            return stokbarang;
        }

        public virtual List<CART> GetDataCartById(Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            List<CART> carts = (from cart in context.CARTs
                                                   where cart.PROD_ID == prodid
                                                   select cart).ToList();

            return carts;
        }
    }
}