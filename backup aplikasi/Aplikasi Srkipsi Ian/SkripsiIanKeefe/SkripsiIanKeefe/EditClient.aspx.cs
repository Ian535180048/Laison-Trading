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
    public partial class EditClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];
                var clientid = (string)Session["clientid"];
                LAISONEntities context = new LAISONEntities();

                Int64 clientid1 = Convert.ToInt64(clientid);

                CLIENT client = GetClientById(clientid1);
                lblclientno.Text = client.CLIENT_NO;
                txtboxclientname.Text = client.CLIENT_NAME;
            }
        }

        protected void lbadd_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var clientid = (string)Session["clientid"];
            LAISONEntities context = new LAISONEntities();

            Int64 clientid1 = Convert.ToInt64(clientid);

            using (var context1 = new LAISONEntities())
            {
                var client = context1.CLIENTs.Where(u => u.CLIENT_ID == clientid1).FirstOrDefault();

                if (txtboxclientname.Text != null)
                {
                    client.CLIENT_NAME = txtboxclientname.Text;
                }

                context1.SaveChanges();
                Session["id"] = id;
                Response.Redirect("ClientPagingAdmin.aspx");
            }
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            var clientid = (string)Session["clientid"];
            string userid = Convert.ToString(id);
            Session["id"] = id;
            Response.Redirect("ClientPagingAdmin.aspx");
        }

        public virtual CLIENT GetClientById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            CLIENT a = (from j in context.CLIENTs
                        where j.CLIENT_ID == id
                        select j).FirstOrDefault();
            return a;
        }
    }
}