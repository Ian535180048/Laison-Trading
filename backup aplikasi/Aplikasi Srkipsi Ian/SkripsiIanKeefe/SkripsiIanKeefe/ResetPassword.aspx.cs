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
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
        }

        protected void lbback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];

            if (txtboxpassword.Text != txtboxpasswordconfirm.Text)
            {
                lblpasscon.Visible = true;
            }

            else
            {
                Editpassword(Convert.ToInt64(id), txtboxpassword.Text);
            }

        }


        public virtual void Editpassword(Int64 id, string pass)
        {
            LAISONEntities context = new LAISONEntities();

            using (var context1 = new LAISONEntities())
            {
                var user = context1.REF_USER.Where(u => u.REF_USER_ID == id).FirstOrDefault();

                string password = HashPassword(pass);

                user.PASSWORD = password;

                context1.SaveChanges();
                Response.Redirect("Login.aspx");
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

        public virtual REF_USER GetDataUser(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            REF_USER user = (from j in context.REF_USER where j.REF_USER_ID == id select j).FirstOrDefault();

            return user;
        }
    }
}