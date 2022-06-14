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
    public partial class EditSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var supplierid = (string)Session["supplierid"];
                LAISONEntities context = new LAISONEntities();

                Int64 supplierid1 = Convert.ToInt64(supplierid);

                SUPPLIER suppl = GetSuppById(supplierid1);
                lblsupplierno.Text = suppl.SUPPLIER_NO;
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var supplierid = (string)Session["supplierid"];
            LAISONEntities context = new LAISONEntities();

            Int64 supplierid1 = Convert.ToInt64(supplierid);

            using (var context1 = new LAISONEntities())
            {
                var supp = context1.SUPPLIERs.Where(u => u.SUPPLIER_ID == supplierid1).FirstOrDefault();

                if (txtboxsuppliername.Text != null)
                {
                    supp.SUPPLIER_NAME = txtboxsuppliername.Text;
                }

                context1.SaveChanges();
                Session["id"] = id;
                Response.Redirect("SupplierPagingAdmin.aspx");
            }
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var supplierid = (string)Session["supplierid"];

            Session["id"] = id;
            Response.Redirect("SupplierPagingAdmin.aspx");
        }

        public virtual SUPPLIER GetSuppById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            SUPPLIER a = (from j in context.SUPPLIERs
                          where j.SUPPLIER_ID == id
                          select j).FirstOrDefault();
            return a;
        }
    }
}