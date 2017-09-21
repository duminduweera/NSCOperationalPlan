using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using MyDLLs;


namespace NSCOperationalPlan
{
    //Teest comment to check github commit sff
    public partial class frmCouncilReport : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmCouncilReport()
        {
            InitializeComponent();
        }



        private void LoadReportType()
        {
            Dictionary<string, string> t = new Dictionary<string, string>()
            {
                {"1", "Monthly Progress" },
                {"2", "Capital Work" },
                {"3", "Performance Indeces" }
            };

            cboReportType.DataSource = new BindingSource(t, null);
            cboReportType.DisplayMember = "Value";
            cboReportType.ValueMember = "Key";
        }
        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");

            DataRow dr = tb.NewRow();
            dr["director_id"] = "-0-";
            dr["director_description"] = "=== ALL ===";
            tb.Rows.InsertAt(dr, 0);
            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
            LoadManagers(cboDirector.SelectedValue.ToString());
        }
        private void LoadManagers(string directorcode)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "';");

            DataRow dr = tb.NewRow();
            dr["manager_id"] = "-0-";
            dr["manager_description"] = "=== ALL ===";
            tb.Rows.InsertAt(dr, 0);

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";

        }
        private void LoadYears()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT progress_year FROM progress ORDER BY progress_year;");

            cboOPYear.DataSource = tb;
            cboOPYear.DisplayMember = "progress_year";
            cboOPYear.ValueMember = "progress_year";

        }
        private void LoadMonths()
        {
            cboOPMonth.DataSource = Enum.GetValues(typeof(Months));
            cboOPMonth.Text = Enum.GetName(typeof(Months), OPGlobals.currentMonth);

        }

        private void frmCouncilReport_Load(object sender, EventArgs e)
        {
            LoadReportType();
            LoadDirectors();
            LoadYears();
            LoadMonths();
        }

        private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDirector.SelectedValue.GetType().Name == "String" && cboDirector.SelectedValue.ToString() != "-0-")
                {
                    LoadManagers(cboDirector.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            string strsql = "";
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());
          

            if(chk1.Checked == true)
            {
                clsReports.TestSubReport(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
            }
            else
            {
                switch (cboReportType.SelectedValue.ToString())
                {
                    //case "MonthlyProgress":
                    case "1":
                        if (cboDirector.SelectedValue.ToString() != "-0-")  // && cboManager.SelectedValue.ToString() != "-0-")
                        {
                            //strsql = ReportQueries.QMonthlyProgress(cboOPYear.Text, (int)m, false, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                            strsql = MonthlyProgress.GetQueryMonthlyProgress(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                        }
                        else
                        {
                            //strsql = ReportQueries.QMonthlyProgress(cboOPYear.Text, (int)m, true);
                            strsql = MonthlyProgress.GetQueryMonthlyProgress(cboOPYear.Text, (int)m);
                        }
                        clsReports.PrintMonthlyProgressForCouncil(strsql);
                        break;
                    //case "Capital Work":
                    case "2":
                        if (cboDirector.SelectedValue.ToString() != "-0-")  // && cboManager.SelectedValue.ToString() != "-0-")
                        {
                            clsReports.PrintCapitalWorksCounil(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                        }
                        else
                        {
                            clsReports.PrintCapitalWorksCounil(cboOPYear.Text, (int)m);
                        }

                        break;
                    //case "KPI":
                    case "3":
                        if (cboDirector.SelectedValue.ToString() != "-0-")  // && cboManager.SelectedValue.ToString() != "-0-")
                        {
                            clsReports.PrintKPIProgress(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                        }
                        else
                        {
                            clsReports.PrintKPIProgress(cboOPYear.Text, (int)m);
                        }
                        break;
                }

            }
        }
    }
}
