using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkripsiIanKeefe.Entities;

namespace SkripsiIanKeefe
{
    public partial class Main : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            var id = (string)Session["id"];
            //string id = Request.QueryString["id"];
            Int64 userid = Convert.ToInt64(id);
            LAISONEntities context = new LAISONEntities();
            REF_USER user = (from u in context.REF_USER where u.REF_USER_ID == userid select u).FirstOrDefault();

            if(user==null)
            {
                Response.Redirect("Login.aspx");
            }

            lblnameuser.Text = "Welcome " + user.FIRST_NAME + " " + user.LAST_NAME;
        }

        protected void lbprediksistokbarang_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PrediksiStokBarang.aspx");
        }

        protected void lblaisontrading_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("Main.aspx");
        }

        protected void lbpenjualan_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PenjualanPagingAdmin.aspx");
        }

        protected void lbbiaya_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("BiayaPagingAdmin.aspx");
        }

        protected void lbhutang_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("HutangPagingAdmin.aspx");
        }

        protected void lbstok_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PembelianStockBarangPagingAdmin.aspx");

        }

        protected void lbpembayaranhutang_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PembayaranHutangPagingAdmin.aspx");
        }

        protected void lbjurnal_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("JurnalPaging.aspx");
        }

        protected void lbcustomer_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("CustomerPagingAdmin.aspx");
        }

        protected void lbproduct_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("ProductPagingAdmin.aspx");
        }

        protected void lbsupplier_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("SupplierPagingAdmin.aspx");
        }

        protected void lbclient_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("ClientPagingAdmin.aspx");
        }

        protected void lblogout_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("Login.aspx");
        }

        protected void lbapprovepembayaran_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PagingApprovePembayaranAdmin.aspx");
        }

        protected void lbpengiriman_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PagingPengirimanAdmin.aspx");
        }

        protected void lbcanceltransaksi_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PagingCancelTransaksi.aspx");
        }

        protected void lbapprovecancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PagingApproveCancelTransaksi.aspx");
        }

        protected void lbapproverefund_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = userid;
            Response.Redirect("PagingApproveRefundTransaksi.aspx");
        }
    }
}