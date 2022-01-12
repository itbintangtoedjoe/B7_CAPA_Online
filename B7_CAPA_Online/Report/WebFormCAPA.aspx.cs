using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace B7_CAPA_Online.Report
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                ReportViewer1.Reset();
                ReportViewer1.LocalReport.EnableExternalImages = true;

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/ReportCAPA.rdlc");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["CAPAONLINE"];
                string conString = mySetting.ConnectionString;
                SqlConnection conn = new SqlConnection(conString);
                DataTable DT = new DataTable();
                string a = Request.QueryString.Get("resi");

                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("sp_load_report", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@resi", System.Data.SqlDbType.VarChar);
                        command.Parameters["@resi"].Value = a;

                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT);
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in DT.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in DT.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                ReportDataSource DataSource = new ReportDataSource("EkspedisiOnlineDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource);

                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("EODataset", DT));

            }
        }
    }
}