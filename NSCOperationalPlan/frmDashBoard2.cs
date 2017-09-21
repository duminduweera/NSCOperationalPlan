using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;
using MyDLLs;

namespace NSCOperationalPlan
{
    public partial class frmDashBoard2 : Form
    {
        public frmDashBoard2()
        {
            InitializeComponent();
        }

        private void frmDashBoard2_Load(object sender, EventArgs e)
        {

            Color bkg = Color.FromArgb(172, 172, 172);

            reportViewer1.ShowToolBar = false;
            reportViewer1.BackColor = bkg;

            //reportViewer2.Dock = DockStyle.Fill;
            reportViewer2.ShowToolBar = false;
            reportViewer2.BackColor = bkg;

            reportViewer3.ShowToolBar = false;
            reportViewer3.BackColor = bkg;

            reportViewer4.ShowToolBar = false;
            reportViewer4.BackColor = bkg;

            frmPrint frmprint = new frmPrint();
            DrawCharts();
            RefreshCharts();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DrawCharts();
            RefreshCharts();
        }

        private void RefreshCharts()
        {
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
            this.reportViewer4.RefreshReport();

        }
        private void DrawCharts()
        {
            try
            {
                Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = new DataTable();
                string strsql = null;

                //=========================== REPORT 1 ===================

                strsql = "SELECT * FROM view_dashboard_top WHERE delivery_program_year = '" + OPGlobals.prevoiusYear + "'"
                    + " AND progress_month=" + OPGlobals.previousMonth + " ORDER BY theme_id, status_id";
                tb = db.GetDataTable(conn, strsql);
                if (tb.Rows.Count == 0)
                {
                    strsql = "SELECT DISTINCT theme_id, theme_short,theme_color, theme_color,action_by_theme,"
                        + " null as status_id, null as status_short, null as status_color, null as completed_action"
                        + " FROM view_dashboard_top WHERE delivery_program_year = '" + OPGlobals.prevoiusYear + "' ORDER BY theme_id";
                    tb = db.GetDataTable(conn, strsql);
                }

                reportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = tb;

                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                this.reportViewer1.LocalReport.ReportPath = OPGlobals.reportParth + @"rptDashBoard001.rdlc"; // reportPath; // @"rptDashBoard.rdlc";

                //================Report 2====================

                strsql = "Select status.id, status.status_short, status.status_color, A.action_completed, A.progress_year, A.progress_month"
                    + " From status Left Join"
                    + " (Select progress.progress_year, progress.progress_month, Count(progress.id) As action_completed, progress.status_id"
                    + " From progress Where progress_year = '" + OPGlobals.prevoiusYear + "' and progress_month = " + OPGlobals.previousMonth
                    + " Group By progress.progress_year, progress.progress_month, progress.status_id) A On status.id = A.status_id";

                tb = db.GetDataTable(conn, strsql);

                reportViewer2.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "DataSet1";
                reportDataSource2.Value = tb;

                reportViewer2.LocalReport.DataSources.Add(reportDataSource2);

                this.reportViewer2.LocalReport.ReportPath = OPGlobals.reportParth + @"rptDashBoard002.rdlc"; // reportPath; 

                //===========================REPORT 3 MODYFIED -  CHECKED===================

                strsql = "SELECT * FROM view_dashboard_top WHERE delivery_program_year = '" + OPGlobals.currentYear + "'"
                    + " AND (progress_month = " + OPGlobals.currentMonth
                    + " OR  view_dashboard_top.progress_month Is Null)"
                    + " ORDER BY theme_id, status_id";
                tb = db.GetDataTable(conn, strsql);
                if(tb.Rows.Count == 0)
                {
                    strsql = "SELECT DISTINCT theme_id, theme_short,theme_color, theme_color,action_by_theme,"
                        + " null as status_id, null as status_short, null as status_color, null as completed_action"
                        + " FROM view_dashboard_top WHERE delivery_program_year = '" + OPGlobals.currentYear + "' ORDER BY theme_id";
                    tb = db.GetDataTable(conn, strsql);
                }

                reportViewer3.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource3 = new ReportDataSource();

                reportDataSource3.Name = "DataSet1";
                reportDataSource3.Value = tb;

                reportViewer3.LocalReport.DataSources.Add(reportDataSource3);

                this.reportViewer3.LocalReport.ReportPath = OPGlobals.reportParth + @"rptDashBoard003.rdlc"; // reportPath; // @"rptDashBoard.rdlc";

                //================Report 4====================

                strsql = "Select status.id, status.status_short, status.status_color, A.action_completed, A.progress_year, A.progress_month"
                    + " From status Left Join"
                    + " (Select progress.progress_year, progress.progress_month, Count(progress.id) As action_completed, progress.status_id"
                    + " From progress Where progress_year = '" + OPGlobals.currentYear + "' and progress_month = " + OPGlobals.currentMonth
                    + " Group By progress.progress_year, progress.progress_month, progress.status_id) A On status.id = A.status_id";

                tb = db.GetDataTable(conn, strsql);

                reportViewer4.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource4 = new ReportDataSource();

                reportDataSource4.Name = "DataSet1";
                reportDataSource4.Value = tb;

                reportViewer4.LocalReport.DataSources.Add(reportDataSource4);

                this.reportViewer4.LocalReport.ReportPath = OPGlobals.reportParth + @"rptDashBoard004.rdlc"; // reportPath; // @"rptDashBoard.rdlc";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
