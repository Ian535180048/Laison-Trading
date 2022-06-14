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
    public partial class PrediksiStokBarang : System.Web.UI.Page
    {
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = (string)Session["id"];

                DataTable prod = GetDataProduct();

                dlproduk.DataSource = prod;
                dlproduk.DataTextField = "PROD_NAME";
                dlproduk.DataValueField = "PROD_ID";
                dlproduk.DataBind();

                updatatahunds.Visible = true;
                updatatahunds.Update();
            }

        }
        #endregion

        #region button
        protected void lbcalculate_Click(object sender, EventArgs e)
        {
            updatapastls.Visible = false;
            updatapastls.Update();

            uptotalls.Visible = false;
            uptotalls.Update();

            upleast.Visible = false;
            upleast.Update();

            updataaktualds.Visible = false;
            updataaktualds.Update();

            upmeanerror.Visible = false;
            upmeanerror.Update();

            updatahasilds.Visible = false;
            updatahasilds.Update();

            if (updatatahunds.Visible == false)
            {

            }

            else
            {

                if (dlmetode.SelectedValue == "LS")
                {
                    DataTable ls = GetDataAktualLS(Convert.ToInt64(dlproduk.SelectedValue), Convert.ToInt64(dlbulan.SelectedValue));
                    gvdatapastls.DataSource = ls;
                    gvdatapastls.DataBind();

                    DataTable hasils = GetDataLS(Convert.ToInt64(dlbulan.SelectedValue));
                    gvleast.DataSource = hasils;
                    gvleast.DataBind();

                    updatapastls.Visible = true;
                    upleast.Visible = true;

                    updatapastls.Update();
                    upleast.Update();
                }

                else if (dlmetode.SelectedValue == "DS")
                {
                    DataTable ds = GetDataAktualDS(Convert.ToInt64(dlproduk.SelectedValue), Convert.ToInt64(dlbulan.SelectedValue));

                    gvdataaktualds.DataSource = ds;
                    gvdataaktualds.DataBind();

                    updataaktualds.Visible = true;
                    upleast.Update();

                    DataTable hasils = GetDataDS(Convert.ToInt64(dlbulan.SelectedValue));
                    gvhasilds.DataSource = hasils;
                    gvhasilds.DataBind();

                    updatahasilds.Visible = true;
                    updatahasilds.Update();
                }
            }

        }
        #endregion

        #region index
        protected void dlmetode_SelectedIndexChanged(object sender, EventArgs e)
        {

            updatapastls.Visible = false;
            updatapastls.Update();

            uptotalls.Visible = false;
            uptotalls.Update();

            upleast.Visible = false;
            upleast.Update();

            updataaktualds.Visible = false;
            updataaktualds.Update();

            upmeanerror.Visible = false;
            upmeanerror.Update();

            updatahasilds.Visible = false;
            updatahasilds.Update();

            if (dlmetode.SelectedValue == "DS")
            {
                var id = (string)Session["id"];
                LAISONEntities context = new LAISONEntities();

                DataTable prod = GetDataProduct();

                dlproduk.DataSource = prod;
                dlproduk.DataTextField = "PROD_NAME";
                dlproduk.DataValueField = "PROD_ID";
                dlproduk.DataBind();

                updatatahunds.Visible = true;
                updatatahunds.Update();
            }

            else if (dlmetode.SelectedValue == "LS")
            {
                var id = (string)Session["id"];
                LAISONEntities context = new LAISONEntities();

                DataTable prod = GetDataProduct();

                dlproduk.DataSource = prod;
                dlproduk.DataTextField = "PROD_NAME";
                dlproduk.DataValueField = "PROD_ID";
                dlproduk.DataBind();

            }
        }
        #endregion

        #region metode

        #region double
        public virtual DataTable GetDataDS(Int64 bulan)
        {

            lblhasilds.Visible = true;
            updatahasilds.Update();

            var now = DateTime.Now;

            DataTable dt = new DataTable();

            dt.Columns.Add("month", typeof(Int32));
            dt.Columns.Add("year", typeof(Int32));

            

            dt.Columns.Add("eoq", typeof(Int64));
            dt.Columns.Add("safety", typeof(Int64));
            dt.Columns.Add("max", typeof(Int64));
            dt.Columns.Add("prediksi", typeof(Int64));

            Int32 i = 0;
            Int32 j = 1;
            Decimal akar = new Decimal();
            Int64 hitungan1 = new Int64();
            Decimal hitungan2 = new Decimal();

            while (i < bulan)
            {
                DataRow dr = dt.NewRow();
                dr["month"] = now.AddMonths(i).Month;
                dr["year"] = now.AddMonths(i).Year;
                dr["prediksi"] = Convert.ToInt64(Decimal.Parse(((Label)gvdataaktualds.Rows[Convert.ToInt32(bulan-1)].FindControl("lbl_at")).Text) + Decimal.Parse(((Label)gvdataaktualds.Rows[Convert.ToInt32(bulan-1)].FindControl("lbl_slope")).Text) * (i+1));

                hitungan1 = Convert.ToInt64(2 * Convert.ToInt32(dr["prediksi"]) * Convert.ToInt32(txtboxpengirimanprodukds.Text));

                if (hitungan1 < 0)
                {
                    dr["eoq"] = 0;
                }

                else
                {
                    hitungan2 = Convert.ToDecimal(Convert.ToInt32(txtboxpemesananprodukds.Text) * 0.02);
                    akar = Convert.ToDecimal(Math.Sqrt(Convert.ToInt64(hitungan1 / hitungan2)));

                    dr["eoq"] = akar;
                }
                Int32 day = System.DateTime.DaysInMonth(now.AddMonths(i).Year, now.AddMonths(i).Month);

                
                dr["safety"] = Convert.ToInt64((Convert.ToDecimal(dr["prediksi"]) / day) * 2);
                dr["max"] = Convert.ToInt64(dr["eoq"]) + Convert.ToInt32(dr["safety"]);

                j++;
                i++;
                dt.Rows.Add(dr);
            }
            return dt;

        }


        public virtual DataTable GetDataAktualDS(Int64 prod, Int64 bulan)
        {
            lblaktualds.Visible = true;
            updataaktualds.Update();

            var now = DateTime.Now;
            now = now.Date.AddDays(1 - now.Day).AddMonths(-1);

            Int32 bulan1 = Convert.ToInt32(bulan);

            var months = Enumerable.Range(-1 * bulan1 + 1, bulan1)
            .Select(x => new {
                year = now.AddMonths(x).Year,
                month = now.AddMonths(x).Month
            });

            LAISONEntities context = new LAISONEntities();


            var query = (from product in context.PRODUCTs
                         join checkout in context.CHECKOUTs on product.PROD_ID equals checkout.PROD_ID
                         join trans in context.TRANS_PENJUALAN on checkout.TRANS_PENJUALAN_ID equals trans.TRANS_PENJUALAN_ID
                         where product.PROD_ID == prod && trans.STATUS != "CAN" && trans.STATUS != "CAN_REQ" && trans.STATUS != "PD_REQ" 
                         && trans.STATUS != "PD_APV_REQ" && trans.STATUS !="PAID"
                         select new
                         {
                             trans.TANGGAL_BAYAR,
                             checkout.JUMLAH_BARANG
                         }).GroupBy(a => new { a.TANGGAL_BAYAR.Value.Month, a.TANGGAL_BAYAR.Value.Year }, (key, group) => new
                         {
                             yr = key.Year,
                             mnth = key.Month,
                             Amount = group.Sum(z => z.JUMLAH_BARANG)
                         }).ToList();


            var changesPerYearAndMonth =
            months.GroupJoin(query,
                m => new { month = m.month, year = m.year },
                revision => new {
                    month = revision.mnth,
                    year = revision.yr
                },
                (p, g) => new {
                    month = p.month,
                    year = p.year,
                    count = g.Sum(a => a.Amount)
                });

            DataTable dt = LINQResultToDataTable(changesPerYearAndMonth);


            dt.Columns.Add("tunggal", typeof(Decimal));
            dt.Columns.Add("ganda", typeof(Decimal));
            dt.Columns.Add("at", typeof(Decimal));
            dt.Columns.Add("bt", typeof(Decimal));
            dt.Columns.Add("ft", typeof(Int64));
            dt.Columns.Add("p", typeof(Decimal));

            Int32 i = 0;
            Decimal pe = new Decimal();
            Decimal horst = Convert.ToDecimal(0.1);
            Decimal min = Convert.ToDecimal(100);
            Decimal horstasli = Convert.ToDecimal(0);

            while (horst <= Convert.ToDecimal(0.9))
            {
                i = 0;
                pe = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (i == 0)
                    {
                        row["tunggal"] = row["count"];
                        row["ganda"] = row["count"];
                        row["at"] = 2 * Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ganda"].ToString());
                        row["bt"] = (Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ganda"].ToString())) * (Convert.ToDecimal(horst) / (1 - Convert.ToDecimal(horst)));
                        row["ft"] = Convert.ToDecimal(dt.Rows[i]["at"].ToString()) + Convert.ToDecimal(dt.Rows[i]["bt"].ToString()) * i;
                        row["p"] = 0;

                        pe = pe + Convert.ToDecimal(row["p"]);
                    }

                    else if (i > 0)
                    {
                        Decimal rw = Convert.ToDecimal(dt.Rows[i - 1]["tunggal"].ToString());
                        Decimal decim = Convert.ToDecimal(horst);

                        row["tunggal"] = Convert.ToDecimal(Convert.ToDecimal(horst) * Convert.ToInt64(dt.Rows[i]["count"].ToString()) + (1 - Convert.ToDecimal(horst)) * Convert.ToDecimal(dt.Rows[i - 1]["tunggal"].ToString()));
                        row["ganda"] = Convert.ToDecimal(horst) * Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) + (1 - Convert.ToDecimal(horst)) * Convert.ToDecimal(dt.Rows[i - 1]["ganda"].ToString());

                        Decimal k = Convert.ToDecimal(Convert.ToDecimal(horst) * Convert.ToInt64(row["count"]) + (1 - Convert.ToDecimal(horst)) * Convert.ToDecimal(dt.Rows[i - 1]["tunggal"].ToString()));
                        Decimal v = Convert.ToDecimal(horst) * Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) + (1 - Convert.ToDecimal(horst)) * Convert.ToDecimal(dt.Rows[i - 1]["ganda"].ToString());

                        Decimal at = 2 * Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ganda"].ToString());

                        row["at"] = 2 * Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ganda"].ToString());
                        row["bt"] = (Convert.ToDecimal(dt.Rows[i]["tunggal"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ganda"].ToString()) * (Convert.ToDecimal(horst) / (1 - Convert.ToDecimal(horst))));
                        row["ft"] = Convert.ToInt64(Convert.ToDecimal(dt.Rows[i]["at"].ToString()) + Convert.ToDecimal(dt.Rows[i]["bt"].ToString()));

                        if (Convert.ToInt64(dt.Rows[i]["count"].ToString()) == Convert.ToInt64(0))
                        {
                            row["p"] = 0;
                            pe = pe + Convert.ToDecimal(row["p"]);
                        }

                        else
                        {
                            row["p"] = Convert.ToDecimal(Math.Abs((Convert.ToDecimal(dt.Rows[i]["count"].ToString()) - Convert.ToDecimal(dt.Rows[i]["ft"].ToString())) / Convert.ToDecimal(dt.Rows[i]["count"].ToString())));
                            pe = pe + Convert.ToDecimal(dt.Rows[i]["p"].ToString());
                        }

                    }
                    i++;                
                }
                pe = (100 / bulan) * pe;
                    horst = Convert.ToDecimal(horst) + Convert.ToDecimal(0.1);

                    if (min > pe)
                    {
                        min = pe;
                        horstasli = horst;
                    }
            }

            DataTable dr = LINQResultToDataTable(changesPerYearAndMonth);

            dr.Columns.Add("tunggal", typeof(Decimal));
            dr.Columns.Add("ganda", typeof(Decimal));
            dr.Columns.Add("at", typeof(Decimal));
            dr.Columns.Add("bt", typeof(Decimal));
            dr.Columns.Add("ft", typeof(Int64));
            dr.Columns.Add("p", typeof(Decimal));

            Int32 j = 0;
            Decimal error = Convert.ToDecimal(0);
            foreach (DataRow rows in dr.Rows)
            {
                if (j == 0)
                {
                    rows["tunggal"] = rows["count"];
                    rows["ganda"] = rows["count"];
                    rows["at"] = 2 * Convert.ToDecimal(dr.Rows[j]["tunggal"].ToString()) - Convert.ToDecimal(dr.Rows[j]["ganda"].ToString());
                    rows["bt"] = (Convert.ToDecimal(dr.Rows[j]["tunggal"].ToString()) - Convert.ToDecimal(dr.Rows[j]["ganda"].ToString())) * (Convert.ToDecimal(horstasli) / (1 - Convert.ToDecimal(horstasli)));
                    rows["ft"] = Convert.ToDecimal(dr.Rows[j]["at"].ToString()) + Convert.ToDecimal(dr.Rows[j]["bt"].ToString()) * i;
                    rows["p"] = 0;

                    error = error + Convert.ToDecimal(dr.Rows[j]["p"].ToString());
                }


                else if (j > 0)
                {
                    Decimal rw = Convert.ToDecimal(dt.Rows[j - 1]["tunggal"].ToString());
                    Decimal decim = Convert.ToDecimal(horstasli);

                    rows["tunggal"] = Convert.ToDecimal(Convert.ToDecimal(horstasli) * Convert.ToInt64(dr.Rows[j]["count"].ToString()) + (1 - Convert.ToDecimal(horstasli)) * Convert.ToDecimal(dr.Rows[j - 1]["tunggal"].ToString()));
                    rows["ganda"] = Convert.ToDecimal(horstasli) * Convert.ToDecimal(dt.Rows[j]["tunggal"].ToString()) + (1 - Convert.ToDecimal(horstasli)) * Convert.ToDecimal(dr.Rows[j - 1]["ganda"].ToString());

                    Decimal k = Convert.ToDecimal(Convert.ToDecimal(horstasli) * Convert.ToInt64(rows["count"]) + (1 - Convert.ToDecimal(horstasli)) * Convert.ToDecimal(dr.Rows[j - 1]["tunggal"].ToString()));
                    Decimal v = Convert.ToDecimal(horstasli) * Convert.ToDecimal(dr.Rows[j]["tunggal"].ToString()) + (1 - Convert.ToDecimal(horstasli)) * Convert.ToDecimal(dr.Rows[j - 1]["ganda"].ToString());

                    Decimal at = 2 * Convert.ToDecimal(dr.Rows[j]["tunggal"].ToString()) - Convert.ToDecimal(dr.Rows[j]["ganda"].ToString());

                    rows["at"] = 2 * Convert.ToDecimal(dr.Rows[j]["tunggal"].ToString()) - Convert.ToDecimal(dr.Rows[j]["ganda"].ToString());
                    rows["bt"] = (Convert.ToDecimal(dr.Rows[j]["tunggal"].ToString()) - Convert.ToDecimal(dr.Rows[j]["ganda"].ToString())) * (Convert.ToDecimal(horstasli) / (1 - Convert.ToDecimal(horstasli)));
                    
                    Decimal a = Convert.ToDecimal(dr.Rows[j]["at"].ToString());
                    Decimal bt = Convert.ToDecimal(dr.Rows[j]["bt"].ToString());
                    Decimal ft = Convert.ToDecimal(dr.Rows[j]["at"].ToString()) + Convert.ToDecimal(dr.Rows[j]["bt"].ToString());

                    rows["ft"] = Convert.ToInt64(ft);

                    if (Convert.ToInt64(dr.Rows[j]["count"].ToString()) == Convert.ToInt64(0))
                    {
                        rows["p"] = 0;
                        error = error + Convert.ToDecimal(rows["p"]);
                    }

                    else
                    {
                        rows["p"] = Convert.ToDecimal(Math.Abs((Convert.ToDecimal(dr.Rows[j]["count"].ToString()) - Convert.ToDecimal(dr.Rows[j]["ft"].ToString())) / Convert.ToDecimal(dr.Rows[j]["count"].ToString())));
                        error = error + Convert.ToDecimal(rows["p"]);
                    }
                }
                j++;
            }
            error = (100 / bulan) * error;

            lblmeanabsolute.Text = string.Format("{0:0.00}", error) + " %";

            upmeanerror.Visible = true;
            upmeanerror.Update();
            return dr;
        }

        #endregion

        #region least square
        public virtual DataTable GetDataLS(Int64 bulan)
        {
            lblhasills.Visible = true;
            upleast.Update();

            lbldatals.Visible = true;
            updatapastls.Update();

            var now = DateTime.Now;

            DataTable dt = new DataTable();

            dt.Columns.Add("bulan", typeof(Int32));
            dt.Columns.Add("tahun", typeof(Int32));

            dt.Columns.Add("x", typeof(Int32));
            dt.Columns.Add("prediksi", typeof(Int32));

            dt.Columns.Add("eoq", typeof(Int64));
            dt.Columns.Add("safety", typeof(Int64));
            dt.Columns.Add("max", typeof(Int64));

            Int32 i = 0;
            Int32 j = 1;
            Int64 akar = new Int64();
            Int64 hitungan1 = new Int64();
            Decimal hitungan2 = new Decimal();

            while (i<bulan)
            {
                DataRow dr = dt.NewRow();
                dr["bulan"] = now.AddMonths(i).Month;
                dr["tahun"] = now.AddMonths(i).Year;

                if(bulan%2==1)
                {
                    dr["x"] = Int64.Parse(((Label)gvdatapastls.Rows[Convert.ToInt32(bulan - 1)].FindControl("lbl_x")).Text) + j;
                }

                else if (bulan%2==0)
                {
                    dr["x"] = Int64.Parse(((Label)gvdatapastls.Rows[Convert.ToInt32(bulan - 1)].FindControl("lbl_x")).Text) + j*2;
                }
                
                dr["prediksi"] = Convert.ToDecimal(lblnilaitrend.Text) + Convert.ToDecimal(lblnilairataratatrend.Text) * Convert.ToInt32(dr["x"]);


                hitungan1 = Convert.ToInt64(2 * Convert.ToInt32(dr["prediksi"]) * Convert.ToInt32(txtboxpengirimanprodukds.Text));
                if (hitungan1 < 0)
                {
                    dr["eoq"] = 0;
                }

                else
                {
                    hitungan2 = Convert.ToDecimal(Convert.ToInt32(txtboxpemesananprodukds.Text) * 0.02);
                    akar = Convert.ToInt64(Math.Sqrt(Convert.ToInt64(hitungan1 / hitungan2)));

                    dr["eoq"] = akar;
                }

                Int32 day = System.DateTime.DaysInMonth(now.AddMonths(i).Year,now.AddMonths(i).Month);
                
                dr["safety"] = Convert.ToInt64((Convert.ToDecimal(dr["prediksi"]) / day) * 2);
                dr["max"] = Convert.ToInt64(dr["eoq"]) + Convert.ToDecimal(dr["safety"]);

                j++;
                i++;
                dt.Rows.Add(dr);
            }
            return dt;

        }

        public virtual DataTable GetDataAktualLS(Int64 prod, Int64 bulan)
        {

            var now = DateTime.Now;
            now = now.Date.AddDays(1 - now.Day).AddMonths(-1);

            Int32 bulan1 = Convert.ToInt32(bulan);

            var months = Enumerable.Range(-1 * bulan1 + 1, bulan1 )
            .Select(x => new {
                year = now.AddMonths(x).Year,
                month = now.AddMonths(x).Month
            });

            LAISONEntities context = new LAISONEntities();


            var query = (from product in context.PRODUCTs
                         join checkout in context.CHECKOUTs on product.PROD_ID equals checkout.PROD_ID
                         join trans in context.TRANS_PENJUALAN on checkout.TRANS_PENJUALAN_ID equals trans.TRANS_PENJUALAN_ID
                         where product.PROD_ID == prod && trans.STATUS != "CAN" && trans.STATUS != "CAN_REQ" && trans.STATUS != "PD_REQ"
                         && trans.STATUS != "PD_APV_REQ" && trans.STATUS != "PAID"
                         select new
                         {
                             trans.TANGGAL_BAYAR,
                             checkout.JUMLAH_BARANG
                         }).GroupBy(a => new { a.TANGGAL_BAYAR.Value.Month, a.TANGGAL_BAYAR.Value.Year }, (key, group) => new
                         {
                             yr = key.Year,
                             mnth = key.Month,
                             Amount = group.Sum(z => z.JUMLAH_BARANG)
                         }).ToList();


            var changesPerYearAndMonth =
            months.GroupJoin(query,
                m => new { month = m.month, year = m.year },
                revision => new {
                    month = revision.mnth,
                    year = revision.yr
                },
                (p, g) => new {
                    month = p.month,
                    year = p.year,
                    count = g.Sum(a => a.Amount)
                });

            DataTable dt = LINQResultToDataTable(changesPerYearAndMonth);

            dt.Columns.Add("x", typeof(Int32));
            dt.Columns.Add("xpangkat", typeof(Int32));
            dt.Columns.Add("xy", typeof(Int32));

            if (dt.Rows.Count % 2 == 0)
            {
                Int32 titiktengah = dt.Rows.Count / 2;
                Int32 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    if (i <= titiktengah)
                    {
                        row["x"] = -1 - 2 * (dt.Rows.Count / 2 - i);
                    }

                    else if (i == titiktengah + 1)
                    {
                        row["x"] = 1;
                    }

                    else if (i > titiktengah + 1)
                    {
                        row["x"] = 1 - 2 * (dt.Rows.Count / 2 - i) - 2;
                    }

                    row["xpangkat"] = Convert.ToInt32(row["x"]) * Convert.ToInt32(row["x"]);
                    row["xy"] = Convert.ToInt32(row["x"]) * Convert.ToInt32(row["count"]);
                    i++;
                }
            }

            else if (dt.Rows.Count == 1)
            {
                Int32 i = 1;
                foreach (DataRow row in dt.Rows)
                {
                    if (i  == 1)
                    {
                        row["x"] = 1;
                    }

                    row["xpangkat"] = Convert.ToInt32(row["x"]) * Convert.ToInt32(row["x"]);
                    row["xy"] = Convert.ToInt32(row["x"]) * Convert.ToInt32(row["count"]);
                    i++;
                }
            }

            else if (dt.Rows.Count % 2 == 1)
            {
                
                Int32 titiktengah = dt.Rows.Count / 2;
                Int32 i = 0;
                Int32 y = 0;
                foreach (DataRow row in dt.Rows)
                {

                        row["x"] = 0 - titiktengah + i;
                        y = 0 - titiktengah + i;
                    

                    //else if (i )
                    //{
                    //    row["x"] = -1;
                    //}

                    //else if (i == titiktengah)
                    //{
                    //    row["x"] = 0;
                    //}

                    //else if (i == titiktengah + 1)
                    //{
                    //    row["x"] = 0+
                    //}

                    //else if (i > titiktengah + 1)
                    //{
                    //    row["x"] = 3 - 2 * ((dt.Rows.Count-1) / 2 - i) - 6;
                    //}
                    row["xpangkat"] = Convert.ToInt32(row["x"]) * Convert.ToInt32(row["x"]);
                    row["xy"] = Convert.ToInt32(row["x"]) * Convert.ToInt32(row["count"]);
                    i++;
                }
            }

            Decimal TotalPenjualan = Convert.ToDecimal(dt.Compute("SUM(count)", string.Empty));
            Int64 Totalx = Convert.ToInt64(dt.Compute("SUM(x)", string.Empty));
            Decimal Totalxkuadrat = Convert.ToDecimal(dt.Compute("SUM(xpangkat)", string.Empty));
            Decimal Totalxy = Convert.ToDecimal(dt.Compute("SUM(xy)", string.Empty));

            Decimal Totaltrend = Convert.ToDecimal(TotalPenjualan / bulan);
            Decimal Totalrata = Convert.ToDecimal(Totalxy / Totalxkuadrat);

            lbltotalpenjualan.Text = Convert.ToString(TotalPenjualan);
            lbltotalx.Text = Convert.ToString(Totalx);
            lbltotalxkuadrat.Text = Convert.ToString(Totalxkuadrat);
            lbltotalxy.Text = Convert.ToString(Totalxy);

            lblnilaitrend.Text = Totaltrend.ToString("0.00");
            lblnilairataratatrend.Text = Totalrata.ToString("0.00");



            uptotalls.Visible = true;
            uptotalls.Update();

            return dt;

        }
        #endregion

        public virtual DataTable GetDataProduct()
        {
            LAISONEntities context = new LAISONEntities();

            var query = (from trans in context.PRODUCTs
                         where trans.IS_ACTIVE=="1"
                         select trans);

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
        #endregion


    }
}