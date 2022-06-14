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
    public partial class DetailProdCust : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var prodid = (string)Session["prodid"];


            LAISONEntities context = new LAISONEntities();

            Int64 prodid1 = Convert.ToInt64(prodid);

            PRODUCT prod = GetDataProdbyid(prodid1);

            image1.ImageUrl = prod.FOTO;
            lblproduk.Text = prod.PROD_NAME;
            lblprice.Text = String.Format("{0:Rp 0,0.00}", prod.PRICE);
            lblberat.Text = Convert.ToString(prod.BERAT_PRODUK) +" Gram";
            if(prod.STOCK_TOTAL == 0)
            {
                lblamt.Text = "Stock Habis";
                txtboxammount.Visible = false;
                lbadd.Visible = false;
            }

            else
            {
                lblamt.Text = Convert.ToString(prod.STOCK_TOTAL);
            }
                     
            if(prod.DESCR != null)
            {
                lbldescr.Text = prod.DESCR;
            }

            else
            {
                lbldescr.Text = "Tidak ada deskripsi";
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

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var prodid = (string)Session["prodid"];

            LAISONEntities context = new LAISONEntities();

            Int64 custid = Convert.ToInt64(id);
            Int64 prodid1 = Convert.ToInt64(prodid);

            CART cart1 = GetDataCartbyid(custid, prodid1);

            if (Convert.ToInt32(txtboxammount.Text) > Convert.ToInt32(lblamt.Text))
            {
                uperror.Visible = true;
                uperror.Update();
            }

            else
            {

                if (cart1 != null)
                {
                    EditCart(cart1, Convert.ToInt32(txtboxammount.Text));
                }

                else
                {
                    CART cart = new CART();

                    cart.PROD_ID = prodid1;
                    cart.REF_USER_ID = custid;
                    cart.AMMOUNT = Convert.ToInt32(txtboxammount.Text);

                    context.CARTs.Add(cart);

                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("HomeCustomer.aspx");
            }
        }

        public virtual void EditCart(CART cart, Int32 amt)
        {
            using (var context1 = new LAISONEntities())
            {
                var Cart = context1.CARTs.Where(u => u.CART_ID == cart.CART_ID).FirstOrDefault();

                Cart.AMMOUNT = Cart.AMMOUNT + amt;
                context1.SaveChanges();
            }
        }

        public virtual CART GetDataCartbyid(Int64 custid, Int64 prodid)
        {
            LAISONEntities context = new LAISONEntities();

            CART a = (from j in context.CARTs
                      join p in context.PRODUCTs on j.PROD_ID equals p.PROD_ID
                      where j.REF_USER_ID == custid
                      where j.PROD_ID == prodid
                      select j).FirstOrDefault();
            return a;
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("HomeCustomer.aspx");
        }
    }
}