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

        //SELECT DISTINCT delivery_program_year as dp_year FROM delivery_program order by delivery_program_year;

        private void LoadReportType()
        {
            Dictionary<string, string> t = new Dictionary<string, string>()
            {
                {"1", "Actions by Month" },
                {"2", "Capital Work by Month" },
                {"3", "Key Performance Measures by Month" },
                {"4", "Quarterly Report Format (Actions/KPM/CWP)" }
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
            //DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT progress_year FROM progress ORDER BY progress_year;");
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT delivery_program_year as dp_year FROM delivery_program order by delivery_program_year;");

            cboOPYear.DataSource = tb;
            cboOPYear.DisplayMember = "dp_year";
            cboOPYear.ValueMember = "dp_year";
            cboOPYear.SelectedValue = OPGlobals.currentYear;


        }
        private void LoadMonths()
        {
            cboOPMonth.DataSource = Enum.GetValues(typeof(Months));
            cboOPMonth.Text = Enum.GetName(typeof(Months), OPGlobals.currentMonth);

        }
        private void LoadServicePlan()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM service_plan;");

            cboServicePlan.DataSource = tb;
            cboServicePlan.DisplayMember = "service_plan";
            cboServicePlan.ValueMember = "id";
        }

        private void frmCouncilReport_Load(object sender, EventArgs e)
        {
            LoadReportType();
            LoadDirectors();
            LoadYears();
            LoadMonths();
            LoadServicePlan();
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
            if (cboReportType.SelectedValue.ToString() == "4")
            {
                tabControl1.SelectedTab = tabPage2;
                ((Control)tabControl1.TabPages[0]).Enabled = false;
            } else
            {
                tabControl1.SelectedTab = tabPage1;
                ((Control)tabControl1.TabPages[0]).Enabled = true;
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            string strsql = "";
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());

            if (chk1.Checked == true)
            {
                clsReports.TestSubReport(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
            }
            else
            {
                switch (cboReportType.SelectedValue.ToString())
                {
                    //case "MonthlyProgress":
                    case "1":
                        PrintActionsByMonth();
                        break;
                    //case "Capital Work":
                    case "2":
                        PrintCWPByMonth();
                        break;
                    //case "KPI":
                    case "3":
                        PrintKPMByMonth();
                        break;
                    //case "SUB REPORT":
                    case "4":
                        PrintSubReportyMonth();
                        break;
                }
            }
        }

        private void PrintActionsByMonth()
        {
            string strsql = "";
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());
            if(tabControl1.SelectedIndex == 0)
            {
                if (cboDirector.SelectedValue.ToString() != "-0-")  // && cboManager.SelectedValue.ToString() != "-0-")
                {
                    strsql = MonthlyProgress.GetQueryMonthlyProgress(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                }
                else
                {
                    strsql = MonthlyProgress.GetQueryMonthlyProgress(cboOPYear.Text, (int)m);
                }
            } else
            {
                strsql = MonthlyProgress.GetQueryMonthlyProgress(cboServicePlan.SelectedValue.ToString(), cboOPYear.Text, (int)m);
            }
            clsReports.PrintMonthlyProgressForCouncil(strsql);
        }
        private void PrintCWPByMonth()
        {
            //string strsql = "";
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());
            if (tabControl1.SelectedIndex == 0)
            {
                if (cboDirector.SelectedValue.ToString() != "-0-")  // && cboManager.SelectedValue.ToString() != "-0-")
                {
                    clsReports.PrintCapitalWorksCounil(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                }
                else
                {
                    clsReports.PrintCapitalWorksCounil(cboOPYear.Text, (int)m);
                }
            } else
            {
                clsReports.PrintCapitalWorksCounil(cboOPYear.Text, (int)m, cboServicePlan.SelectedValue.ToString());
            }
        }
        private void PrintKPMByMonth()
        {
            //string strsql = "";
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());
            if (tabControl1.SelectedIndex == 0)
            {
                if (cboDirector.SelectedValue.ToString() != "-0-")  // && cboManager.SelectedValue.ToString() != "-0-")
                {
                    clsReports.PrintKPIProgressCouncil(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString(), cboManager.SelectedValue.ToString());
                }
                else
                {
                    clsReports.PrintKPIProgressCouncil(cboOPYear.Text, (int)m);
                }

            } else
            {
                clsReports.PrintKPIProgressCouncil(cboServicePlan.SelectedValue.ToString(), cboOPYear.Text, (int)m);
            }
        }

        private void PrintSubReportyMonth()
        {
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());
            //string month = OPGlobals.currentMonth.ToString();
            string year = cboOPYear.Text;

            string servicePlanSql = GetServicePlanForSubReporting(year, (int)m, cboServicePlan.SelectedValue.ToString());

                //"Select A.id As service_plan_id, A.service_plan, B.actions As action, C.kpm As kpm, D.cwp As cwp From service_plan A"
                //+ " Left Join(Select action.service_plan_id, COUNT(*) As actions From action Group By action.service_plan_id) B"
                //+ " On B.service_plan_id = A.id Left Join (Select kpi.service_plan_id, COUNT(*) As kpm From kpi Group By kpi.service_plan_id) C"
                //+ " On C.service_plan_id = A.id Left Join (Select capital_works.capital_works_service_plann_id As service_plan_id, COUNT(*) As cwp From capital_works Group By capital_works.capital_works_service_plann_id) D"
                //+ " On D.service_plan_id = A.id where service_plan != '-NONE-';";

            string actionSql = "", kpmSql="", cwpSql="";

            actionSql = MonthlyProgress.GetQueryMonthlyProgress(cboServicePlan.SelectedValue.ToString(), cboOPYear.Text, (int)m);
            cwpSql = CapitalWork.GetSQLCapitalWorksMonthlyProgress(cboServicePlan.SelectedValue.ToString(), cboOPYear.Text, (int)m);
            kpmSql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(cboServicePlan.SelectedValue.ToString(), cboOPYear.Text, (int)m);

            clsReports.PrintSubReport(servicePlanSql, actionSql, kpmSql, cwpSql);
        }

        private string GetServicePlanForSubReporting(string year, int month)
        {
            string servicePlanSql = "";

            servicePlanSql = "Select A.id As service_plan_id, A.service_plan_manager_id, A.service_plan, if(B.action>0,B.action,0) as action, if(C.kpm>0,C.kpm,0) As kpm, if(D.cwp>0,D.cwp,0) As cwp From service_plan A Left Join"
                + " (Select action.service_plan_id, Count(*) As action From action Left Join progress On progress.action_id = action.id"
                + " Where progress.progress_year = '" + year + "' And progress.progress_month = " + month + " Group By action.service_plan_id) B On B.service_plan_id = A.id Left Join"
                + " (Select kpi.service_plan_id, COUNT(*) As kpm From kpi Left Join kpi_progress On kpi.id = kpi_progress.kpi_id"
                + " Where kpi_progress.kpi_year = '" + year + "' And kpi_progress.kpi_month = " + month + " Group By kpi.service_plan_id) C On C.service_plan_id = A.id Left Join"
                + " (Select capital_works.capital_works_service_plann_id As service_plan_id, COUNT(*) As cwp From capital_works Left Join capital_works_monthly_progress On capital_works.capital_works_id = capital_works_monthly_progress.capital_works_id"
                + " Where capital_works_monthly_progress.capital_works_year = '" + year + "' And capital_works_monthly_progress.capital_works_month = " + month + " Group By capital_works.capital_works_service_plann_id) D"
                + " On D.service_plan_id = A.id Left Join manager_view E On A.service_plan_manager_id = E.manager_id"; 

            return servicePlanSql;
        }
        private string GetServicePlanForSubReporting(string year, int month, string servicePlanID )
        {
            string servicePlanSql = GetServicePlanForSubReporting(year, month);
            servicePlanSql = servicePlanSql + " WHERE id != '000'";

            if (!string.IsNullOrEmpty(servicePlanID) && servicePlanID != "000")
            {
                servicePlanSql = servicePlanSql + " AND id = '" + servicePlanID + "'";
            }
            return servicePlanSql;
        }

        
    }
}
