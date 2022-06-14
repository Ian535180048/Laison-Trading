using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkripsiIanKeefe.Entities;
using System.Security.Cryptography;

namespace SkripsiIanKeefe
{
    public partial class SettingCust : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var id = (string)Session["id"];

                REF_USER user = GetDataUser(Convert.ToInt64(id));

                txtboxcustfirstname.Text = user.FIRST_NAME;
                txtboxcustlastname.Text = user.LAST_NAME;

                txtboxemail.Text = user.EMAIL;
                txtboxphonenumber.Text = user.PHONE_NUMBER;
            }
        }

        protected void ltlchange_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            Int64 userid = Convert.ToInt64(id);

            using (var context1 = new LAISONEntities())
            {
                var user = context1.REF_USER.Where(u => u.REF_USER_ID == userid).FirstOrDefault();

                if (txtboxcustfirstname.Text != null)
                {
                    user.FIRST_NAME = txtboxcustfirstname.Text;
                }

                if (txtboxcustlastname.Text != null)
                {
                    user.LAST_NAME = txtboxcustlastname.Text;
                }

                if (txtboxemail.Text != null)
                {
                    user.EMAIL = Convert.ToString(txtboxemail.Text);
                }

                if (txtboxphonenumber.Text != null)
                {
                    user.PHONE_NUMBER = Convert.ToString(txtboxphonenumber.Text);
                }

                REF_USER user1 = GetDataUser(Convert.ToInt64(id));

                bool passhash = VerifyHashedPassword(user1.PASSWORD, txtboxpassowrd.Text);

                bool passhas1 = VerifyHashedPassword(user1.PASSWORD, txtboxconfirmpassword.Text);

                if (passhash == false || passhas1 == false)
                {
                    uppass.Visible = true;
                    uppass.Update();

                }

                else
                {
                    context1.SaveChanges();
                    Session["id"] = id;
                    Response.Redirect("HomeCustomer.aspx");
                }
            }
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            System.Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            System.Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        protected void ltlchangepass_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Session["id"] = id;
            Response.Redirect("ResetPassword.aspx");
        }

        public virtual REF_USER GetDataUser(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            REF_USER user = (from j in context.REF_USER where j.REF_USER_ID == id select j).FirstOrDefault();

            return user;
        }
    }
}