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
    public partial class HomeCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();
            DataTable prod = GetDataProd();

            if (dlproduk.Items.Count == 0)
            {
                dlproduk.DataSource = prod;
                dlproduk.DataBind();
            }
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];

            DataTable prod = GetDataProdSearch(txtboxnamaproduk.Text, txtboxharga_mulai.Text, txtboxhargaakhir.Text);


            dlproduk.DataSource = prod;
            dlproduk.DataBind();
        }


        public virtual DataTable GetDataProd()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from prod in context.PRODUCTs
                         join chk in context.CHECKOUTs on prod.PROD_ID equals chk.PROD_ID
                         where prod.IS_ACTIVE == "1"
                         select new
                         {
                             prod.PROD_ID,
                             prod.PROD_NAME,
                             prod.PROD_NO,
                             prod.PRICE,
                             prod.FOTO,
                             DESCR =
                             (
                                prod.STOCK_TOTAL >= 10 ? "Stok Product Lebih Dari 10" :
                                prod.STOCK_TOTAL < 10 ? "Stok Product Kurang Dari 10" :
                                prod.STOCK_TOTAL == 0 ? "Stok Product Habis" : "Error"
                             ),
                             chk.JUMLAH_BARANG
                         } into s
                         group s by new { s.PROD_ID, s.PROD_NAME, s.PROD_NO, s.PRICE, s.FOTO} into g
                         orderby g.Sum(t=> t.JUMLAH_BARANG) descending
                         select new { TOTAL = g.Sum(t => t.JUMLAH_BARANG), g.Key.PROD_ID, g.Key.PROD_NAME, g.Key.PROD_NO, g.Key.PRICE, g.Key.FOTO });


            DataTable dt = LINQResultToDataTable(query);
            return dt;

        }

        public virtual DataTable GetDataProdSearch(string name, string pricea, string pricee)
        {
            LAISONEntities context = new LAISONEntities();

            Decimal priceawal = new Decimal();
            Decimal priceend = new Decimal();

            if (pricea == "" && pricee == "")
            {
                priceawal = 0;
                priceend = 10000000000000000;
            }

            else if (pricee == "")
            {
                priceend = 10000000000000000;
                priceawal = Convert.ToDecimal(pricea);
            }

            else if (pricea == "")
            {
                priceawal = 0;
                priceend = Convert.ToDecimal(pricee);
            }

            else
            {
                priceend = Convert.ToDecimal(pricee);
                priceawal = Convert.ToDecimal(pricea);
            }
            var query = (from prod in context.PRODUCTs
                         where prod.PROD_NAME.Contains(name) && prod.PRICE >= priceawal && prod.PRICE <= priceend && prod.IS_ACTIVE == "1"
                         select new
                         {
                             prod.PROD_ID,
                             prod.PROD_NAME,
                             prod.PROD_NO,
                             prod.PRICE,
                             prod.FOTO,
                             DESCR =
                             (
                                prod.STOCK_TOTAL >= 10 ? "Stok Product Lebih Dari 10" :
                                prod.STOCK_TOTAL < 10 ? "Stok Product Kurang Dari 10" :
                                prod.STOCK_TOTAL == 0 ? "Stok Product Habis" : "Error"
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

        protected void dlproduk_ItemCommand(object source, DataListCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            string prodid = e.CommandArgument.ToString();
            Int32 prodid1 = Convert.ToInt32(prodid);

            if (e.CommandName == "VIEW")
            {

                //Label prodid = dlproduk.Items[x1].FindControl("lbl_prod_id") as Label;
                //Label prodid = (Label)li.FindControl("lbl_prod_id");
                PRODUCT prod = GetDataProdbyid(prodid1);
                string prodid2 = Convert.ToString(prodid1);

                //if (prod.STOCK_TOTAL == 0)
                //{
                //    lblerror.Visible = true;
                //}
                //else
                //{
                    Session["id"] = id;
                    Session["prodid"] = prodid2;
                    Response.Redirect("DetailProdCust.aspx");
                

            }
        }

        public virtual PRODUCT GetDataProdbyid(Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT a = (from j in context.PRODUCTs
                         where j.PROD_ID == prodid
                         select j).FirstOrDefault();
            return a;
        }

        protected void dlproduk_ItemCommand1(object sender, ListViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            string prodid = e.CommandArgument.ToString();
            Int32 prodid1 = Convert.ToInt32(prodid);

            if (e.CommandName == "VIEW")
            {

                //Label prodid = dlproduk.Items[x1].FindControl("lbl_prod_id") as Label;
                //Label prodid = (Label)li.FindControl("lbl_prod_id");
                PRODUCT prod = GetDataProdbyid(prodid1);
                string prodid2 = Convert.ToString(prodid1);

                //if (prod.STOCK_TOTAL == 0)
                //{
                //    lblerror.Visible = true;
                //}
                //else
                //{
                Session["id"] = id;
                Session["prodid"] = prodid2;
                Response.Redirect("DetailProdCust.aspx");


            }
        }
    }
}