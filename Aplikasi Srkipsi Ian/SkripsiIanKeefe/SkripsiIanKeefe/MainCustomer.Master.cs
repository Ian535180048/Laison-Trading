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
    public partial class MainCustomer : System.Web.UI.MasterPage
    {
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 userid = Convert.ToInt64(id);
            LAISONEntities context = new LAISONEntities();
            REF_USER user = (from u in context.REF_USER where u.REF_USER_ID == userid select u).FirstOrDefault();
            lblnameuser.Text =  user.FIRST_NAME + " " + user.LAST_NAME;
        }
        #endregion

        #region button
        protected void lblaisontrading_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("HomeCustomer.aspx");
            }
            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("HomeCustomer.aspx");
            }

        }

        protected void lbcart_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("Cart.aspx?id="+id);
            }
            else
            {
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
        }

        protected void lbtransaksi_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("HistoryTransaksi.aspx");
            }
            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("HistoryTransaksi.aspx");
            }

        }

        protected void lblogout_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("Login.aspx");
            }
        
            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Response.Redirect("Login.aspx");
            }
        }
    

        protected void lblsearch_Click(object sender, EventArgs e)
        {

        }

        protected void lbsettings_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("SettingCust.aspx");
            }

            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("SettingCust.aspx");
            }
        }

        protected void lbtransaksionprogress_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("PenjualanOnProgressAdmin.aspx");
            }

            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("PenjualanOnProgressAdmin.aspx");
            }
        }

        protected void lbpembayaran_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("PagingPembayaranProduk.aspx");
            }

            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("PagingPembayaranProduk.aspx");
            }
        }

        protected void lbpenerimaan_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("PagingPenerimaanProduk.aspx");
            }

            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("PagingPenerimaanProduk.aspx");
            }
        }

        protected void lbcanceltrans_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("PagingRequestCancelTransaksi.aspx");
            }

            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("PagingRequestCancelTransaksi.aspx");
            }
        }

        protected void lbrefund_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            List<PEMBAYARAN> pmbyrn = GetDataPembayaranCart(Convert.ToInt64(id));
            if (pmbyrn == null)
            {
                Session["id"] = id;
                Response.Redirect("RefundCustomer.aspx");
            }

            else
            {
                foreach (PEMBAYARAN pm in pmbyrn)
                {
                    PEMBAYARAN pmb = new PEMBAYARAN();
                    pmb.pembayaran_id = pm.pembayaran_id;
                    context.Entry(pmb).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                Session["id"] = id;
                Response.Redirect("RefundCustomer.aspx");
            }
        }

        #endregion
        #region method

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


        #endregion


    }
}