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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btncreateaccount_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            List<REF_USER> usercheck = GetDataUser();
            bool chk = new bool();
            chk = true;

            foreach (REF_USER users in usercheck)
            {

                if (users.EMAIL == txtboxemail.Text || users.USERNAME == txtboxusername.Text || users.PHONE_NUMBER == txtboxphonenumber.Text)
                {
                    chk = false;
                    break;
                }

            }

            if (chk == true)
            {
                REF_USER user = new REF_USER();


                user.FIRST_NAME = txtboxfirstname.Text;

                if (txtboxlastname.Text != null)
                {
                    user.LAST_NAME = txtboxlastname.Text;
                }

                user.PASSWORD = HashPassword(txtboxpassword.Text);
                user.PHONE_NUMBER = txtboxphonenumber.Text;
                user.ROLE = "C";
                user.EMAIL = txtboxemail.Text;

                user.USERNAME = txtboxusername.Text;

                REF_USER confirm = (from n in context.REF_USER where n.USERNAME == user.USERNAME select n).FirstOrDefault();

                if (confirm != null)
                {
                    lbluserada.Visible = true;
                }

                else
                {
                    if (txtboxpasswordconfirm.Text != txtboxpassword.Text)
                    {
                        lblpasscon.Visible = true;
                    }

                    else
                    {

                        user.DTM_CRT = DateTime.Today;
                        user.DTM_UPD = DateTime.Today;

                        context.REF_USER.Add(user);
                        context.SaveChanges();

                        Response.Redirect("Login.aspx");
                    }
                }
            }

            else
            {
                uperror.Visible = true;
                uperror.Update();
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

        public virtual List<REF_USER> GetDataUser()
        {
            LAISONEntities context = new LAISONEntities();

            List<REF_USER> user = (from j in context.REF_USER select j).ToList();

            return user;
        }

        public string EncryptString(string DecryptString)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(DecryptString);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }


    }
}