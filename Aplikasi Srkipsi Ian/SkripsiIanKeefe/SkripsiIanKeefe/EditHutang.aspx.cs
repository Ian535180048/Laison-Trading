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
    public partial class EditHutang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var hutangid = (string)Session["hutangid"];
                LAISONEntities context = new LAISONEntities();
                TRANS_HUTANG hutang = GetDataHutangById(Convert.ToInt64(hutangid));
                CLIENT client = GetDataClientById(Convert.ToInt64(hutang.CLIENT_ID));


                lbltransno.Text = hutang.TRANS_NO;
                lblclientname.Text = client.CLIENT_NAME;

                txtboxammount.Text = Convert.ToString(hutang.HUTANG_AMT);
                txtboxtanggal.Text = Convert.ToString(hutang.TANGGAL);
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var hutangid = (string)Session["hutangid"];

            Int64 hutangid2 = Convert.ToInt64(hutangid);

            using (var context1 = new LAISONEntities())
            {
                var hutang = context1.TRANS_HUTANG.Where(u => u.TRANS_HUTANG_ID == hutangid2).FirstOrDefault();

                if (txtboxammount.Text != null )
                {
                    hutang.HUTANG_AMT = Convert.ToInt64(txtboxammount.Text);
                    hutang.SISA = Convert.ToInt64(txtboxammount.Text);
                }

                if (txtboxtanggal.Text != null)
                {
                    hutang.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                }

                var jurnal = context1.JURNALs.Where(u => u.TRANS_HUTANG_ID == hutangid2).ToList();

                if (txtboxammount.Text != null)
                {
                    int i = 0;
                    foreach(JURNAL jurnals in jurnal)
                    {
                        if (i == 0)
                        {
                            jurnals.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                            i++;
                        }

                        else if (i == 1)
                        {
                            jurnals.OUT_AMT = Convert.ToDecimal(txtboxammount.Text);
                            i++;
                        }
                    }
                }

                if (txtboxtanggal.Text != null)
                {
                    int i = 0;
                    foreach (JURNAL jurnals in jurnal)
                    {
                        if (i == 0)
                        {
                            jurnals.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                            i++;
                        }

                        else if (i == 1)
                        {
                            jurnals.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                            i++;
                        }
                    }
                }

                context1.SaveChanges();
            }
            Session["id"] = id;
            Response.Redirect("HutangPagingAdmin.aspx");
        }
        

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var hutangid = (string)Session["hutangid"];

            Session["id"] = id;
            Response.Redirect("HutangPagingAdmin.aspx");
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