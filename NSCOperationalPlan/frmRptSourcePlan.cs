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
using System.Globalization;

namespace NSCOperationalPlan
{
    public partial class frmRptSourcePlan : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        public frmRptSourcePlan()
        {
            InitializeComponent();
        }
        private void frmRptSourcePlan_Load(object sender, EventArgs e)
        {
            LoadCouncilSourcePlans();
            LoadYears();
            LoadMonths();
        }
        private void LoadMonths()
        {
            cboOPMonth.DataSource = Enum.GetValues(typeof(Months));
            cboOPMonth.Text = Enum.GetName(typeof(Months), OPGlobals.currentMonth);
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            string month = DateTime.ParseExact(cboOPMonth.SelectedValue.ToString(), "MMMM", CultureInfo.CurrentCulture).Month.ToString("D2");
            string year = "20"+cboOPYear.SelectedValue.ToString().Split('/')[0]+"/20"+ cboOPYear.SelectedValue.ToString().Split('/')[1];           
            string strsql = "SELECT council_plan.council_plan_full, action.action_rank,action.id AS action_id,action.action_description, delivery_program.delivery_program_TargetDate,  status.status_short,Query6.progress_description,  Query6.progress_pecentage, '"+year+"' AS progress_year, '"+ month + "' AS progress_month, manager1.manager_description AS manager_name,  service_plan.service_plan,  strategy.id AS strategy_id,  strategy.strategy,  delivery_program.delivery_program_year FROM council_plan LEFT JOIN action ON council_plan.council_plan_id = action.council_plan_id LEFT JOIN (SELECT progress.action_id, progress.status_id, progress.progress_description, progress.progress_pecentage,progress.progress_year,progress.progress_month FROM progress WHERE progress.progress_year = '" + OPGlobals.currentYear+"' and progress.progress_month = '"+ month + "') Query6 ON action.id = Query6.action_id INNER JOIN manager ON action.manager_id = manager.id INNER JOIN manager manager1 ON manager.manager_id = manager1.id LEFT JOIN delivery_program ON delivery_program.action_id = action.id LEFT JOIN status ON Query6.status_id = status.id INNER JOIN service_plan ON action.service_plan_id = service_plan.id INNER JOIN strategy ON action.strategy_id = strategy.id WHERE delivery_program.delivery_program_year = '"+OPGlobals.currentYear+"' and council_plan.council_plan_id ='"+ cboSourcePlan.SelectedValue.ToString()+ "'Order By action.id asc; ";
            clsReports.PrintCouncilSourcePlan(strsql);
        }
        private void LoadYears()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT progress_year FROM progress ORDER BY progress_year;");
            cboOPYear.DataSource = tb;
            cboOPYear.DisplayMember = "progress_year";
            cboOPYear.ValueMember = "progress_year";
        }

        private void LoadCouncilSourcePlans()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM council_plan where council_plan_id != '000' Order By council_plan_full;");                     
            cboSourcePlan.DataSource = tb;
            cboSourcePlan.DisplayMember = "council_plan_short";
            cboSourcePlan.ValueMember = "council_plan_id";
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
