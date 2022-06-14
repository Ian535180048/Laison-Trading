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
    public partial class AddBiaya : System.Web.UI.Page
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

            TRANS_BIAYA transbiaya = new TRANS_BIAYA();
            transbiaya.AMT = Convert.ToDecimal(txtboxammount.Text);
            transbiaya.DESCR = ddlbebantipe.SelectedValue;
            transbiaya.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);

            TRANS_BIAYA transbiayaid = GetDataTransBiayabyIdLatest();
            transbiaya.TRANS_NO = Convert.ToString(DateTime.Today.Year) + Convert.ToString("BYA") + transbiayaid.TRANS_BIAYA_ID.ToString("0000");


            for (int i = 0; i < 2; i++)
            {
                JURNAL jurnal = new JURNAL();

                if (i == 0)
                {
                    if (ddlbebantipe.SelectedValue == Convert.ToString("BL"))
                    {
                        jurnal.COA_ID = GetCOA("5001");
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Beban Listrik";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }
                    else if (ddlbebantipe.SelectedValue == Convert.ToString("BA"))
                    {
                        jurnal.COA_ID = GetCOA("5002");
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Beban Air";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }
                    else if (ddlbebantipe.SelectedValue == Convert.ToString("BI"))
                    {
                        jurnal.COA_ID = GetCOA("5003");
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Beban Internet";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }

                    else if (ddlbebantipe.SelectedValue == Convert.ToString("BP"))
                    {
                        jurnal.COA_ID = GetCOA("5004");
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Beban Pajak";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }

                    else if (ddlbebantipe.SelectedValue == Convert.ToString("BDLL"))
                    {
                        jurnal.COA_ID = GetCOA("5006");
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Beban Lain-lain";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }

                    else if (ddlbebantipe.SelectedValue == Convert.ToString("BAD"))
                    {
                        jurnal.COA_ID = GetCOA("5005");
                        jurnal.IN_AMT = Convert.ToDecimal(txtboxammount.Text);
                        jurnal.DESCR = "Beban Administrasi";
                        jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                    }
                }

                else if (i == 1)
                {
                    jurnal.COA_ID = GetCOA("1001");
                    jurnal.OUT_AMT = Convert.ToDecimal(txtboxammount.Text);
                    jurnal.DESCR = "Kas";
                    jurnal.TANGGAL = Convert.ToDateTime(txtboxtanggal.Text);
                }

                transbiaya.JURNALs.Add(jurnal);
            }

            context.TRANS_BIAYA.Add(transbiaya);
            context.SaveChanges();
            Session["id"] = id;
            Response.Redirect("BiayaPagingAdmin.aspx");
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];

            Session["id"] = id;
            Response.Redirect("BiayaPagingAdmin.aspx");
        }

        public virtual TRANS_BIAYA GetDataTransBiayabyIdLatest()
        {
            LAISONEntities context = new LAISONEntities();

            TRANS_BIAYA a = (from j in context.TRANS_BIAYA
                             orderby j.TRANS_BIAYA_ID descending
                             select j).FirstOrDefault();

            return a;
        }

        public virtual Int64 GetCOA(string value)
        {
            LAISONEntities context = new LAISONEntities();

            Int64 a = (from j in context.COAs
                     where j.COA_NO == value
                     select  j.COA_ID).FirstOrDefault();

            return a;
        }


    }
}