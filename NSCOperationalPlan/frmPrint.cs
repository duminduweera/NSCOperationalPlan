using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace NSCOperationalPlan
{
    public partial class frmPrint : Form
    {
        private DataTable dtb1, dtb2, dtb3, dtb4;
        private List<DataTable> dtbsub;
        private string reportname;

        public DataTable dataTable
        {
            set { dtb1 = value; }
        }
        public DataTable dataTable2
        {
            set { dtb2 = value; }
        }
        public DataTable dataTable3
        {
            set { dtb3 = value; }
        }
        public DataTable dataTable4
        {
            set { dtb4 = value; }
        }
        public List<DataTable> subDataTable
        {
            set { dtbsub = value; }
        }

        public string reportName
        {
            set { reportname = value; }
        }

        public frmPrint()
        {
            InitializeComponent();
        }

        private void frmPrint_Load(object sender, EventArgs e)
         {

            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.dtb1;
            //ReportDataSource reportDataSource2 = new ReportDataSource();
            //reportDataSource2.Name = "DataSet2";
            //reportDataSource2.Value = this.dtb2;
            //ReportDataSource reportDataSource3 = new ReportDataSource();
            //reportDataSource3.Name = "DataSet3";
            //reportDataSource3.Value = this.dtb3;
            //ReportDataSource reportDataSource4 = new ReportDataSource();
            //reportDataSource4.Name = "DataSet4";
            //reportDataSource4.Value = this.dtb4;

            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            //reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            //reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            //reportViewer1.LocalReport.DataSources.Add(reportDataSource4);

            //foreach (DataTable dt in dtbsub)
            //{
            //    reportDataSource = new ReportDataSource();
            //    reportDataSource.Name = "DataSet2";
            //    reportDataSource.Value = dt;
            //    reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            //}

            //reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.LocalReport.ReportPath = OPGlobals.reportParth + this.reportname;

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = ZoomMode.FullPage;
            //this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);


            this.reportViewer1.RefreshReport();

        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e) {
            ReportDataSource subReportDataSource1 = new ReportDataSource();
            subReportDataSource1.Name = "DataSet1";
            subReportDataSource1.Value = this.dtb2;
            ReportDataSource subReportDataSource2 = new ReportDataSource();
            subReportDataSource2.Name = "DataSetKPM";
            subReportDataSource2.Value = this.dtb3;
            ReportDataSource subReportDataSource3 = new ReportDataSource();
            subReportDataSource3.Name = "DataSetCWP";
            subReportDataSource3.Value = this.dtb4;
            
            e.DataSources.Add(subReportDataSource1);
            e.DataSources.Add(subReportDataSource2);
           e.DataSources.Add(subReportDataSource3);

        }

        private void frmPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
