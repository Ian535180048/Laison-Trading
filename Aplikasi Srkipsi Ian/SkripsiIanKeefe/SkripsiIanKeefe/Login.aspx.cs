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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            string username = txtboxusername.Text;
            string password2 = HashPassword(txtboxinputpassword.Text);
            REF_USER user1 = (from u in context.REF_USER where u.USERNAME == username select u).FirstOrDefault();

            if(user1 != null && user1.ROLE == "AD")
            {
                string password = txtboxinputpassword.Text;
                bool benar = VerifyHashedPassword(user1.PASSWORD, password);

                if(benar == true)
                {
                    string userid = Convert.ToString(user1.REF_USER_ID);
                    Session["id"] = userid;

                    Response.Redirect("Main.aspx");
                }

                else
                {
                    lblincorrect.Visible = true;
                }
            }

            else if (user1 != null && user1.ROLE == "C")
            {
                string password = txtboxinputpassword.Text;
                bool benar = VerifyHashedPassword(user1.PASSWORD, password);

                if (benar == true)
                {
                    string userid = Convert.ToString(user1.REF_USER_ID);
                    Session["id"] = userid;

                    Response.Redirect("HomeCustomer.aspx");
                }

                else
                {
                    lblincorrect.Visible = true;
                }
            }

            else
            {
                lblincorrect.Visible = true;
            }

            ////string password = txtboxinputpassword.Text;
            //string password2 = HashPassword(txtboxinputpassword.Text);
            //string password1 = "AAUNAz6J2sKQg97tvj5qkF4JHMsGy7HGOLBgeyIov08Pf948Zeme7n0thHzjJEasNQ==";

            ////bool benar = VerifyHashedPassword(password1, password);

            //REF_USER user = (from u in context.REF_USER where u.USERNAME == username & u.PASSWORD== password1 select u).FirstOrDefault();
            //if (user != null && user.ROLE == "AD")
            //{
            //    string userid = Convert.ToString(user.REF_USER_ID);
            //    Session["id"] = userid;

            //    Response.Redirect("Main.aspx");
            //}
            //if (user != null && user.ROLE == "C")
            //{
            //    string userid = Convert.ToString(user.REF_USER_ID);
            //    Session["id"] = userid;
            //    Response.Redirect("HomeCustomer.aspx");
            //}            
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

        protected void lb_signup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
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

        public string DecryptString(string encrstring)
        {
            byte[] b;
            string decrypted;

            try
            {
                b = Convert.FromBase64String(encrstring);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }

            catch (FormatException fe)
            {
                decrypted = "";
            }

            return decrypted;
        }

        public string EncryptString(string DecryptString)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(DecryptString);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }


        protected void btn_forgot_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx");
        }
    }
}