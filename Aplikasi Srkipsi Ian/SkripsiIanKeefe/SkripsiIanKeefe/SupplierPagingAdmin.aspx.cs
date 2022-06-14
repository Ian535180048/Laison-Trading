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
    public partial class SupplierPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();
            }
            
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dtproduct = GetDataSupplier(txtboxsuppliername.Text, txtboxsupplierno.Text);

            gvsupplier.DataSource = dtproduct;
            gvsupplier.DataBind();
        }


        public virtual DataTable GetDataSupplier(string suppliername, string supplierno)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from supplier in context.SUPPLIERs
                         where supplier.SUPPLIER_NAME.Contains(suppliername) & supplier.SUPPLIER_NO.Contains(supplierno)
                         select new
                         {
                             supplier.SUPPLIER_NAME,
                             supplier.SUPPLIER_NO,
                             supplier.SUPPLIER_ID
                         });
            DataTable dt = LINQResultToDataTable(query);
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

        protected void ltladd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);

            Session["id"] = id;
            Response.Redirect("AddSupplier.aspx");
        }

        protected void gvsupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 supplierid;

            lblerrordelete1.Visible = false;
            uperror.Update();

            if (e.CommandName == "ED")
            {
                supplierid = Int64.Parse(((Label)gvsupplier.Rows[index].FindControl("lbl_supplier_id")).Text);
                string supplierid2 = Convert.ToString(supplierid);

                Session["id"] = id;
                Session["supplierid"] = supplierid2;
                Response.Redirect("EditSupplier.aspx");
            }

            if (e.CommandName == "DEL")
            {
                supplierid = Int64.Parse(((Label)gvsupplier.Rows[index].FindControl("lbl_supplier_id")).Text);
                string supplierid2 = Convert.ToString(supplierid);

                SUPPLIER supp = GetSuppById(supplierid);

                List<TRANS_STOCK_BARANG> stok = GetDataStokById(Convert.ToInt64(supp.SUPPLIER_ID));

                if (stok.Count > 0)
                {
                    lblerrordelete1.Visible = true;
                    uperror.Update();
                }

                else
                {
                    SUPPLIER supp1 = new SUPPLIER();
                    supp1.SUPPLIER_ID = supp.SUPPLIER_ID;

                    context.Entry(supp1).State = EntityState.Deleted;
                    context.SaveChanges();

                    DataTable dtproduct = GetDataSupplier(txtboxsuppliername.Text, txtboxsupplierno.Text);

                    gvsupplier.DataSource = dtproduct;
                    gvsupplier.DataBind();
                }
            }
        }

        public virtual SUPPLIER GetSuppById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            SUPPLIER a = (from j in context.SUPPLIERs
                         where j.SUPPLIER_ID == id
                         select j).FirstOrDefault();
            return a;
        }

        public virtual List<TRANS_STOCK_BARANG> GetDataStokById(Int64 suppid)
        {
            LAISONEntities context = new LAISONEntities();

            List<TRANS_STOCK_BARANG> stokbarang = (from stok in context.TRANS_STOCK_BARANG
                                                   where stok.SUPPLIER_ID == suppid
                                                   select stok).ToList();

            return stokbarang;
        }
    }
}