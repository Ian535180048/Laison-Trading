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
    public partial class AddSupplier : System.Web.UI.Page
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

            SUPPLIER supp = new SUPPLIER();

            supp.SUPPLIER_NAME = txtboxnamasupplier.Text;

            SUPPLIER supp1 = GetDataSuppLatest();
            supp.SUPPLIER_NO = Convert.ToString("SUP") + supp1.SUPPLIER_ID.ToString("0000");

            context.SUPPLIERs.Add(supp);
            context.SaveChanges();

            Session["id"] = id;
            Response.Redirect("SupplierPagingAdmin.aspx");

        }

        public virtual SUPPLIER GetDataSuppLatest()
        {
            LAISONEntities context = new LAISONEntities();

            SUPPLIER query = (from j in context.SUPPLIERs
                             orderby j.SUPPLIER_ID descending
                             select j).FirstOrDefault();

            return query;
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];

            Session["id"] = id;
            Response.Redirect("SupplierPagingAdmin.aspx");
        }
    }
}