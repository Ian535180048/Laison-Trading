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
    public partial class AddClient : System.Web.UI.Page
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

            CLIENT client = new CLIENT();

            client.CLIENT_NAME = txtboxnamaclient.Text;

            CLIENT client1 = GetDataClientLatest();
            client.CLIENT_NO = Convert.ToString("CLN") + client1.CLIENT_ID.ToString("0000");

            context.CLIENTs.Add(client);
            context.SaveChanges();
            Session["id"] = id;
            Response.Redirect("ClientPagingAdmin.aspx");
        }

        protected void lbcancel_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            Session["id"] = id;
            Response.Redirect("ClientPagingAdmin.aspx");
        }

        public virtual CLIENT GetDataClientLatest()
        {
            LAISONEntities context = new LAISONEntities();

            CLIENT query = (from j in context.CLIENTs
                                 orderby j.CLIENT_ID descending
                              select j).FirstOrDefault();

            return query;
        }
    }
}