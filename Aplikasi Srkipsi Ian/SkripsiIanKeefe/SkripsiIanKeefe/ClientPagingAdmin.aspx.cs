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
    public partial class ClientPagingAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            Int64 halo = new Int64();
        }

        protected void ltlsearch_Click(object sender, EventArgs e)
        {
            var id = (string)Session["id"];
            LAISONEntities context = new LAISONEntities();

            DataTable dtproduct = GetDataClient(txtboxclientname.Text, txtboxclientno.Text);

            gvclient.DataSource = dtproduct;
            gvclient.DataBind();
        }

        public virtual DataTable GetDataClient(string clientname, string clientno)
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from client in context.CLIENTs
                         where client.CLIENT_NAME.Contains(clientname) & client.CLIENT_NO.Contains(clientno)
                         select new
                         {
                             client.CLIENT_NAME,
                             client.CLIENT_NO,
                             client.CLIENT_ID
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
            Response.Redirect("AddClient.aspx");
        }

        protected void gvclient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LAISONEntities context = new LAISONEntities();
            var id = (string)Session["id"];
            string userid = Convert.ToString(id);
            string check = e.CommandName;
            int index = ((GridViewRow)(((Control)e.CommandSource).Parent.Parent)).RowIndex;
            Int64 clientid;

            lblerrordelete1.Visible = false;
            uperror.Update();

            if (e.CommandName == "ED")
            {
                clientid = Int64.Parse(((Label)gvclient.Rows[index].FindControl("lbl_client_id")).Text);
                string clientid2 = Convert.ToString(clientid);

                Session["id"] = id;
                Session["clientid"] = clientid2;
                Response.Redirect("EditClient.aspx");
            }

            if (e.CommandName == "DEL")
            {
                clientid = Int64.Parse(((Label)gvclient.Rows[index].FindControl("lbl_client_id")).Text);
                string clientid2 = Convert.ToString(clientid);

                CLIENT client = GetClientById(clientid);

                List<TRANS_HUTANG> transhutang = GetDataHutangById(Convert.ToInt64(client.CLIENT_ID));

                if (transhutang.Count > 0)
                {
                    lblerrordelete1.Visible = true;
                    uperror.Update();
                }

                else
                {
                    CLIENT client1 = new CLIENT();
                    client1.CLIENT_ID = client.CLIENT_ID;

                    context.Entry(client1).State = EntityState.Deleted;
                    context.SaveChanges();

                    DataTable dtproduct = GetDataClient(txtboxclientname.Text, txtboxclientno.Text);

                    gvclient.DataSource = dtproduct;
                    gvclient.DataBind();
                }
            }
        }

        public virtual CLIENT GetClientById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            CLIENT a = (from j in context.CLIENTs
                          where j.CLIENT_ID == id
                          select j).FirstOrDefault();
            return a;
        }

        public virtual List<TRANS_HUTANG> GetDataHutangById(Int64 id)
        {
            LAISONEntities context = new LAISONEntities();

            List<TRANS_HUTANG> hutang = (from hutang1 in context.TRANS_HUTANG
                                         where hutang1.CLIENT_ID == id
                                                   select hutang1).ToList();

            return hutang;
        }
    }
}