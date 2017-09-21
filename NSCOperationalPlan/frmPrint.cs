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
        private DataTable dtb;
        private List<DataTable> dtbsub;
        private string reportname;

        public DataTable dataTable
        {
            set { dtb = value; }
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
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = this.dtb;
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

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

            this.reportViewer1.RefreshReport();

        }

        private void frmPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
