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
    public partial class AddHutang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();
                LAISONEntities context = new LAISONEntities();
                List<CLIENT> clientlist = GetDataClientList();

                ddlclient.DataSource = clientlist;
                ddlclient.DataTextField = "CLIENT_NAME";
                ddlclient.DataValueField = "CLIENT_ID";

                ddlclient.DataBind();
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG transhutang = new TRANS_HUTANG();

            transhutang.CLIENT_ID = Convert.ToInt64(ddlclient.SelectedValue);
            transhutang.IS_ACTIVE = "ACT";
            transhutang.HUTANG_AMT = Convert.ToDecimal(txtboxammount.Text);
            transhutang.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);

            TRANS_HUTANG transhutangid = GetDataTransHutangbyIdLatest();

            transhutang.SISA = Convert.ToDecimal(txtboxammount.Text);
            transhutang.TRANS_NO = Convert.ToString(DateTime.Today.Year) + Convert.ToString("HTG") + transhutangid.TRANS_HUTANG_ID.ToString("0000");

            for (int i = 0; i < 2; i++)
            {
                JURNAL jurnal = new JURNAL();
                if (i == 0)
                {
                    jurnal.DESCR = "Kas";
                    jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                    jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    jurnal.COA_ID = GetCOA("1001");
                }

                else if (i == 1)
                {
                    jurnal.COA_ID = GetCOA("2001");
                    jurnal.OUT_AMT = Convert.ToDecimal(txtboxammount.Text);
                    jurnal.DESCR = "Hutang Dagang";
                    jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                }
                transhutang.JURNALs.Add(jurnal);
            }

            context.TRANS_HUTANG.Add(transhutang);
            context.SaveChanges();

            Session["id"] = id;
            Response.Redirect("HutangPagingAdmin.aspx");
        }


        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("HutangPagingAdmin.aspx");
        }

        public virtual List<CLIENT> GetDataClientList()
        {
            LAISONEntities context = new LAISONEntities();

            List<CLIENT> client = (from j in context.CLIENTs select j).ToList();

            return client;

        }

        public virtual TRANS_HUTANG GetDataTransHutangbyIdLatest()
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG a = (from j in context.TRANS_HUTANG
                              orderby j.TRANS_HUTANG_ID descending
                             select j).FirstOrDefault();

            return a;
        }

        public virtual Int64 GetCOA(string value)
        {
            LAISONEntities context = new LAISONEntities();

            Int64 a = (from j in context.COAs
                       where j.COA_NO == value
                       select j.COA_ID).FirstOrDefault();

            return a;
        }
    }
}