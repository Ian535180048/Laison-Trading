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
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                LAISONEntities context = new LAISONEntities();
                DataTable prod = GetDataProdAwal(Convert.ToInt64(id));

                if (gvcart.Rows.Count == 0)
                {
                    gvcart.DataSource = prod;
                    gvcart.DataBind();
                }


            }
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dt = GetDataProd(txtboxprodnama.Text, Convert.ToInt64(id));

            gvcart.DataSource = dt;
            gvcart.DataBind();
        }


        public virtual DataTable GetDataProdAwal(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from cart in context.CARTs
                         join prod in context.PRODUCTs on cart.PROD_ID equals prod.PROD_ID
                         where cart.REF_USER_ID == id && prod.IS_ACTIVE == "1"
                         select new
                         {
                             cart.CART_ID,
                             prod.PROD_ID,
                             prod.PROD_NAME,
                             prod.PROD_NO,
                             TOTAL = prod.PRICE * cart.AMMOUNT,
                             prod.FOTO,
                             cart.AMMOUNT,
                             prod.PRICE
                         }).ToList();


            var totalharga = query.Sum(a => a.TOTAL);
            lbltotalharga.Text = String.Format("{0:Rp 0,0.00}", totalharga);
            upcart.Update();


            DataTable dt = LINQResultToDataTable(query);
            return dt;

        }

        public virtual DataTable GetDataProd(string nama, Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from cart in context.CARTs
                         join prod in context.PRODUCTs on cart.PROD_ID equals prod.PROD_ID
                         where cart.REF_USER_ID == id && prod.PROD_NAME.Contains(nama) && prod.IS_ACTIVE == "1"
                         select new
                         {
                             cart.CART_ID,
                             prod.PROD_ID,
                             prod.PROD_NAME,
                             prod.PROD_NO,
                             TOTAL = prod.PRICE * cart.AMMOUNT,
                             prod.FOTO,
                             cart.AMMOUNT,
                             prod.PRICE,
                             prod.STOCK_TOTAL
                         }).ToList();

            if (query.Count == 0)
            {
                lbltotalharga.Text = "Rp 0.00";
            }

            else
            {
                var totalharga = query.Sum(a => a.TOTAL);
                lbltotalharga.Text = String.Format("{0:Rp 0,0.00}", totalharga);
            }
            upcart.Update();
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual CART GetDataCartbyid(Int64 cartid)
        {
            LAISONEntities context = new LAISONEntities();

            CART a = (from j in context.CARTs
                      where j.CART_ID == cartid
                      select j).FirstOrDefault();
            return a;
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

        protected void gvcart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 cartid;
            DataTable prod = new DataTable();
            if (e.CommandName == "DEL")
            {
                cartid = Int64.Parse(((Label)gvcart.Rows[index].FindControl("lblcartid")).Text);

                CART cart = GetDataCartbyid(cartid);

                CART cart1 = new CART();
                cart1.CART_ID = cart.CART_ID;

                context.Entry(cart1).State = EntityState.Deleted;
                context.SaveChanges();

                prod = GetDataProdAwal(Convert.ToInt64(id));


                gvcart.DataSource = prod;
                gvcart.DataBind();

            }

        }

        protected void lbcheckout_Click(object sender, EventArgs e)
        {
            lblerrorcart.Visible = false;
            lblerrorstok.Visible = false;
            uperror.Update();

            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            int i = 0;
            int check = 0;
            List<PEMBAYARAN> listpembayaran = new List<PEMBAYARAN>();

            foreach (GridViewRow row in gvcart.Rows)
            {

                CheckBox chkrow = (CheckBox)gvcart.Rows[i].FindControl("chckbox");
                if (chkrow.Checked)
                {
                    check++;
                    Label cartid1 = (Label)gvcart.Rows[i].FindControl("lblcartid"); /*(row.Cells[i].FindControl("lblcartid") as Label);*/

                    Int64 cartid = Convert.ToInt64(cartid1.Text);

                    PRODUCT prod = GetDataProductbycartid(cartid);

                    CART cart = GetDataCartbycartid(cartid);

                    if (prod.STOCK_TOTAL < cart.AMMOUNT)
                    {
                        lblerrorstok.Visible = true;
                        uperror.Update();
                        break;
                    }
                    else
                    {
                        PEMBAYARAN pmbyrn = new PEMBAYARAN();
                        pmbyrn.CART_ID = cartid;

                        listpembayaran.Add(pmbyrn);
                    }
                }
                if (i == gvcart.Rows.Count - 1 && check > 0)
                {
                    foreach (PEMBAYARAN pmbyrn in listpembayaran)
                    {
                        context.PEMBAYARANs.Add(pmbyrn);
                        context.SaveChanges();
                    }
                    Session["id"] = id;
                    Response.Redirect("CheckoutPembayaran.aspx");
                }

                else if (check == 0 && i == gvcart.Rows.Count)
                {
                    lblerrorcart.Visible = true;
                    uperror.Update();
                }
                i++;
            }
        }

        public virtual PRODUCT GetDataProductbycartid(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            PRODUCT query = (from cart in context.CARTs
                             join prod in context.PRODUCTs on cart.PROD_ID equals prod.PROD_ID
                             where cart.CART_ID == id
                             select prod).FirstOrDefault();

            return query;

        }


        public virtual CART GetDataCartbycartid(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            CART query = (from cart in context.CARTs
                          join prod in context.PRODUCTs on cart.PROD_ID equals prod.PROD_ID
                          where cart.CART_ID == id
                          select cart).FirstOrDefault();

            return query;

        }

        protected void chckboxall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvcart.Rows)
            {
                CheckBox cb = (CheckBox)row.FindControl("chckbox");
                CheckBox call = (CheckBox)Header.FindControl("chckboxall");
                if (cb != null)
                {
                    if(call.Checked)
                    {
                        cb.Checked = true;
                    }

                    else
                    {
                        cb.Checked = false;
                    }
                }
            }
        }
    }
}