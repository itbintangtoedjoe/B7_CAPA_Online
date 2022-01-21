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
    public partial class WebFormCAPA : System.Web.UI.Page
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
                string NoCAPA = Request.QueryString.Get("NoCAPA");

                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;

                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Analisa", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;

                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Root", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;

                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_TPencegahan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;

                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_TPerbaikan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;

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

                ReportDataSource DataSource = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DatasetHeader1", DT));

                ReportDataSource DataSource1 = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource1);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DatasetHeader1", DT));

                ReportDataSource DataSource2 = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource2);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DatasetHeader1", DT));

                ReportDataSource DataSource3 = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource3);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetAnalisa", DT));

                ReportDataSource DataSource4 = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource4);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetRootcause", DT));

                ReportDataSource DataSource5 = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource5);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPerbaikan", DT));

                ReportDataSource DataSource6 = new ReportDataSource("B7_CAPA_ONLINEDataSource", DT);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource6);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPencegahan", DT));
            }
        }
    }
}