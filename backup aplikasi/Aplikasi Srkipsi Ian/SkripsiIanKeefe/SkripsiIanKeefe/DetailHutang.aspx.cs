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
    public partial class DetailHutang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];              
                LAISONEntities context = new LAISONEntities();

                TRANS_HUTANG hutang = GetDataHutangById(Convert.ToInt64(hutangid));
                CLIENT client = GetDataClientById(Convert.ToInt64(hutang.CLIENT_ID));

                lbltransno.Text = hutang.TRANS_NO;
                lblnamaclient.Text = client.CLIENT_NAME;
                DateTime tanggal = hutang.TANGGAL;

                lbltanggaltransaksi.Text = tanggal.ToString("MM/dd/yyyy");

                if (hutang.IS_ACTIVE== "EXP")
                {
                    lblstatus.Text = "Lunas";
                }

                else if (hutang.IS_ACTIVE == "ACT")
                {
                    lblstatus.Text = "Active";
                }
                lbltotalhutang.Text = String.Format("{0:Rp 0,0.00}", hutang.HUTANG_AMT);
                lblsisa.Text = String.Format("{0:Rp 0,0.00}", hutang.SISA); 
            }
        }

        public virtual TRANS_HUTANG GetDataHutangById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_HUTANG hutang = (from j in context.TRANS_HUTANG where j.TRANS_HUTANG_ID == hutangid select j).FirstOrDefault();

            return hutang;
        }

        public virtual CLIENT GetDataClientById(Int64 hutangid)
        {
            LAISONEntities context = new LAISONEntities();

            CLIENT hutang = (from j in context.CLIENTs where j.CLIENT_ID == hutangid select j).FirstOrDefault();

            return hutang;
        }
    }
}