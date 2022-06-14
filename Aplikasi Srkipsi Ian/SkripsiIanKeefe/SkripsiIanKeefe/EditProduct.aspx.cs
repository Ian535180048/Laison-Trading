using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using SkripsiIanKeefe.Entities;

namespace SkripsiIanKeefe
{
    public partial class EditProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var prodid = (string)Session["prodid"];
                LAISONEntities context = new LAISONEntities();

                Int64 prodid1 = Convert.ToInt64(prodid);

                PRODUCT prod = GetProdById(prodid1);

                lblprodno.Text = prod.PROD_NO;
                txtboxammount.Text = Convert.ToString(prod.STOCK_TOTAL);
                txtboxberat.Text = Convert.ToString(prod.BERAT_PRODUK);
                txtboxharga.Text = Convert.ToString(prod.PRICE);
                txtboxproductname.Text = Convert.ToString(prod.PROD_NAME);
                txtboxdeskripsi.Text = prod.DESCR;
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var prodid = (string)Session["prodid"];
            LAISONEntities context = new LAISONEntities();

            Int64 prodid1 = Convert.ToInt64(prodid);

            using (var context1 = new LAISONEntities())
            {
                var prod = context1.PRODUCTs.Where(u => u.PROD_ID == prodid1).FirstOrDefault();

                if (txtboxberat.Text != null)
                {
                    prod.BERAT_PRODUK = Convert.ToInt32(txtboxberat.Text);
                }

                if (txtboxharga.Text != null)
                {
                    prod.PRICE = Convert.ToDecimal(txtboxharga.Text);
                }

                if (txtboxproductname.Text != null)
                {
                    prod.PROD_NAME = Convert.ToString(txtboxproductname.Text);
                }

                if (txtboxammount.Text != null)
                {
                    prod.STOCK_TOTAL = Convert.ToInt32(txtboxammount.Text);
                }

                if (flpdgambar.HasFile)
                {
                    string strname = flpdgambar.FileName.ToString();
                    flpdgambar.PostedFile.SaveAs(Server.MapPath("~/Upload/") + strname);

                    prod.FOTO = "~/Upload/" + strname.ToString();
                }

                if(txtboxdeskripsi.Text != null)
                {
                    prod.DESCR = txtboxdeskripsi.Text;
                }

                context1.SaveChanges();
                Session["id"] = id;
                Response.Redirect("ProductPagingAdmin.aspx");

            }
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var prodid = (string)Session["prodid"];
            Session["id"] = id;
            Response.Redirect("ProductPagingAdmin.aspx");
        }

        public virtual PRODUCT GetProdById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT a = (from j in context.PRODUCTs
                         where j.PROD_ID == id
                         select j).FirstOrDefault();
            return a;
        }
    }
}