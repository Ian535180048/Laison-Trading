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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lb_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            LAISONEntities context = new LAISONEntities();

            List<REF_USER> usercheck = GetDataUser();

            bool chk = new bool();
            chk = false;
            Int64 id = new Int64();

            foreach (REF_USER users in usercheck)
            {
                if(users.EMAIL == txtboxemail.Text && users.USERNAME == txtboxusername.Text && users.PHONE_NUMBER == txtboxphonenumber.Text)
                {
                    id = users.REF_USER_ID;
                    chk = true;
                    break;
                }
            }

            if(chk==false)
            {
                uperror.Visible = true;
                uperror.Update();
            }

            else
            {
                string userid = Convert.ToString(id);

                Session["id"] = userid;
                Response.Redirect("ResetPassword.aspx");
            }           
        }

        public virtual List<REF_USER> GetDataUser()
        {
            LAISONEntities context = new LAISONEntities();

            List<REF_USER> user = (from j in context.REF_USER select j).ToList();

            return user;
        }
    }
}