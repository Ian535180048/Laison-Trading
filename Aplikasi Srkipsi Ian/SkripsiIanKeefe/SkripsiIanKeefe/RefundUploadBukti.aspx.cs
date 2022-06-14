﻿using System;
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
    public partial class RefundUploadBukti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var transid = (string)Session["transid"];

            Int64 halo = new Int64();

            DataTable dtproduct = GetDataProduct(Convert.ToInt64(transid));

            gvproduct.DataSource = dtproduct;
            gvproduct.DataBind();
        }

        #region button

        protected void ltladd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var transid = (string)Session["transid"];

                TRANS_PENJUALAN trans = GetDataTransPenjualan(Convert.ToInt64(transid));

                EditTrans(trans);
                TRANS_PENJUALAN penjualan = GetDataTransPenjualan(Convert.ToInt64(transid));

                Session["id"] = id;
                Response.Redirect("RefundCustomer.aspx");
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("RefundCustomer.aspx");
        }
        #endregion

        #region method
        public virtual void EditTrans(TRANS_PENJUALAN trans)
        {
            using (var context1 = new LAISONEntities())
            {
                var penjualan = context1.TRANS_PENJUALAN.Where(u => u.TRANS_PENJUALAN_ID == trans.TRANS_PENJUALAN_ID).FirstOrDefault();
                string strname = flpdbukti.FileName.ToString();

                if (flpdbukti.HasFile)
                {
                    flpdbukti.PostedFile.SaveAs(Server.MapPath("~/Upload/") + strname);
                    penjualan.BUKTI_REFUND = "~/Upload/" + strname.ToString();
                }                              
                penjualan.STATUS = "REQ_REFUND";
                penjualan.NOTES = txtboxreason.Text;
                context1.SaveChanges();

            }
        }

        public virtual TRANS_PENJUALAN GetDataTransPenjualan(Int64 transid)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];

            TRANS_PENJUALAN trans = (from j in context.TRANS_PENJUALAN
                                     where j.TRANS_PENJUALAN_ID == transid
                                     select j).FirstOrDefault();

            return trans;
        }

        public virtual DataTable GetDataProduct(Int64 transid)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.TRANS_PENJUALAN
                         join chk in context.CHECKOUTs on trans.TRANS_PENJUALAN_ID equals chk.TRANS_PENJUALAN_ID
                         join prod in context.PRODUCTs on chk.PROD_ID equals prod.PROD_ID
                         where trans.TRANS_PENJUALAN_ID == transid
                         select new
                         {
                             prod.PROD_NAME,
                             prod.PROD_ID,
                             prod.PROD_NO,
                             TOTAL_HARGA = chk.HARGA_PRODUK * chk.JUMLAH_BARANG,
                             chk.JUMLAH_BARANG
                         });

            DataTable dt = LINQResultToDataTable(query);

            var totalharga = query.Sum(a => a.TOTAL_HARGA);

            lbltotalharga.Text = String.Format("{0:Rp 0,0.00}", totalharga);

            upharga.Update();

            return dt;
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
        #endregion
    }
}