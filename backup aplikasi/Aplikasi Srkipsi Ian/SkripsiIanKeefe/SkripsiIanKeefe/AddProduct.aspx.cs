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
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            PRODUCT prod = new PRODUCT();

            prod.PROD_NAME = txtboxnamaproduct.Text;
            prod.STOCK_TOTAL = Convert.ToInt32(txtboxstokproduct.Text);
            prod.PRICE = Convert.ToDecimal(txtboxharga.Text);
            prod.BERAT_PRODUK = Convert.ToInt32(txtboxberat.Text);

            PRODUCT PROD1 = GetDataProdLatest();

            prod.PROD_NO = Convert.ToString("PRD") +  PROD1.PROD_ID.ToString("0000");
            prod.PRICE = Convert.ToDecimal(txtboxharga.Text);
            string strname = flpdgambar.FileName.ToString();
            flpdgambar.PostedFile.SaveAs(Server.MapPath("~/Upload/") + strname);
            prod.IS_ACTIVE =  Convert.ToString(1);
            prod.FOTO = "~/Upload/" + strname.ToString();
            prod.DESCR = txtboxdescr.Text;

            context.PRODUCTs.Add(prod);
            context.SaveChanges();
            Session["id"] = id;
            Response.Redirect("ProductPagingAdmin.aspx");
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("ProductPagingAdmin.aspx");
        }

        public virtual PRODUCT GetDataProdLatest()
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT query = (from j in context.PRODUCTs
                             orderby j.PROD_ID descending
                             select j).FirstOrDefault();

            return query;
        }

        protected void txtboxharga_TextChanged(object sender, EventArgs e)
        {
            double ammount = Convert.ToDouble(txtboxharga.Text);
            txtboxharga.Text = ammount.ToString("#,##0");
        }
    }
}