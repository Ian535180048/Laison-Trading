using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkripsiIanKeefe.Entities;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;

namespace SkripsiIanKeefe
{
    public partial class CustomerPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var id = (string)Session["id"];
                Int64 halo = new Int64();

                DataTable dtcust = GetDataCustomers();

                if (gvcust.Rows.Count == 0)
                {
                    gvcust.DataSource = dtcust;
                    gvcust.DataBind();
                }
            }
            
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dtcust = GetDataCustomer(txtboxcustname.Text, txtboxphone.Text, txtboxemail.Text);

            gvcust.DataSource = dtcust;
            gvcust.DataBind();
        }

        public virtual DataTable GetDataCustomers()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from refuser in context.REF_USER
                         where refuser.ROLE == "C"
                         select new
                         {
                             refuser.FIRST_NAME,
                             refuser.LAST_NAME,
                             refuser.PHONE_NUMBER,
                             refuser.EMAIL,
                             refuser.REF_USER_ID
                             //TRANSAKSI,
                         });
            DataTable dt = LINQResultToDataTable(query);
            return dt;
        }

        public virtual DataTable GetDataCustomer(string custname, string phone, string email)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from refuser in context.REF_USER
                         //join transpenjualan in context.TRANS_PENJUALAN on refuser.REF_USER_ID equals transpenjualan.REF_USER_ID
                         //let TRANSAKSI = refuser.TRANS_PENJUALAN.Count
                         where (refuser.FIRST_NAME+refuser.LAST_NAME).Contains(custname) & 
                         refuser.PHONE_NUMBER.Contains(phone) & refuser.EMAIL.Contains(email) & refuser.ROLE=="C"
                         select new
                         {
                             refuser.FIRST_NAME,
                             refuser.LAST_NAME,
                             refuser.PHONE_NUMBER,
                             refuser.EMAIL,
                             refuser.REF_USER_ID
                             //TRANSAKSI,
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

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 refuserid;

            if (e.CommandName == "RST")
            {
                refuserid = Int64.Parse(((Label)gvcust.Rows[index].FindControl("lbl_user_id")).Text);
                string refuserid2 = Convert.ToString(refuserid);

                REF_USER user1 = GetDataUser(refuserid);

                EditUser(user1);
                DataTable dtcust = GetDataCustomers();

                gvcust.DataSource = dtcust;
                gvcust.DataBind();

            }
        }

        public virtual REF_USER GetDataUser(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            REF_USER user = (from j in context.REF_USER where j.REF_USER_ID==id select j).FirstOrDefault();

            return user;
        }

        public virtual void EditUser(REF_USER user)
        {
            using (var context1 = new LAISONEntities())
            {
                var user1 = context1.REF_USER.Where(u => u.REF_USER_ID == user.REF_USER_ID).FirstOrDefault();

                user1.PASSWORD = HashPassword("password");
                context1.SaveChanges();

            }
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            System.Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            System.Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}