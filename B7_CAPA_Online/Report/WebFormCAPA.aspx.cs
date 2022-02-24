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
                DataTable DT1 = new DataTable();
                DataTable DT2 = new DataTable();
                DataTable DT3 = new DataTable();
                DataTable DT4 = new DataTable();
                DataTable DT5 = new DataTable();
                DataTable DT6 = new DataTable();
                DataTable DT7 = new DataTable();
                DataTable DT8 = new DataTable();
                DataTable DT9 = new DataTable();
                DataTable DT10 = new DataTable();
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

                        dataAdapt.Fill(DT1);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Root", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;


                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT2);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_TPerbaikan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;


                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT3);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_TPencegahan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT4);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Pelaksana_Pencegahan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT5);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Verifikasi", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT6);
                    }
                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_KajianResiko", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT7);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Evaluator", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT8);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_Pelaksana_Perbaikan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT9);
                    }

                    using (SqlCommand command = new SqlCommand("SP_LOAD_REPORT_KajianResiko_Perubahan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@NoCAPA", System.Data.SqlDbType.VarChar);
                        command.Parameters["@NoCAPA"].Value = NoCAPA;



                        SqlDataAdapter dataAdapt = new SqlDataAdapter();
                        dataAdapt.SelectCommand = command;

                        dataAdapt.Fill(DT10);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                        throw ex;
                }

                ReportDataSource DataSource = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetHeader1.Designer.cs", DT);
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetHeader1", DT));

                ReportDataSource DataSource1 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetAnalisa.Designer.cs", DT1);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource1);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetAnalisa", DT1));

                ReportDataSource DataSource2 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetRoot.Designer.cs", DT2);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource2);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetRoot", DT2));

                ReportDataSource DataSource3 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetPerbaikan.Designer.cs", DT3);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource3);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPerbaikan", DT3));

                ReportDataSource DataSource4 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetPencegahan.Designer.cs", DT4);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource4);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPencegahan", DT4));

                ReportDataSource DataSource5 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetPelaksanaPencegahan.Designer.cs", DT5);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource5);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPelaksanaPencegahan", DT5));

                ReportDataSource DataSource6 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetVerifikasi", DT6);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource6);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetVerifikasi", DT6));

                ReportDataSource DataSource7 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetKajian.Designer.cs", DT7);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource7);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetKajian", DT7));

                ReportDataSource DataSource8 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetEvaluator.Designer.cs", DT8);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource8);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetEvaluator", DT8));

                ReportDataSource DataSource9 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetPelaksanaPerbaikan.Designer.cs", DT9);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource9);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetPelaksanaPerbaikan", DT9));

                ReportDataSource DataSource10 = new ReportDataSource("~/DataSource/B7_CAPA_ONLINEDataSetKajianPerubahan.Designer.cs", DT10);
                this.ReportViewer1.LocalReport.DataSources.Add(DataSource10);
                ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSetKajianPerubahan", DT10));
            }
        }
    }
}