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
    public partial class DetailProduct : System.Web.UI.Page
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
                lblprodname.Text = prod.PROD_NAME;
                lblberat.Text = Convert.ToString(prod.BERAT_PRODUK);
                lblharga.Text = String.Format("{0:Rp 0,0.00}", prod.PRICE);
                lblamt.Text = Convert.ToString(prod.STOCK_TOTAL);
                img.ImageUrl = Convert.ToString(prod.FOTO);
                
                if(prod.DESCR == null)
                {
                    lbldescr.Text = "Tidak ada deskripsi";
                }

                else
                {
                    lbldescr.Text = prod.DESCR;
                }
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
    }
}