using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDLLs;
using System.Data.Common;
using System.Data;

namespace NSCOperationalPlan
{
    class clsReports
    {
        public static void PrintTheme()
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "SELECT theme.* FROM theme ORDER BY id;";
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptTheme.rdlc";

            frmprint.Show();

        }
        public static void PrintStrategyObjective()
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "Select Distinct strategy_view.theme_id, strategy_view.theme_color, strategy_view.strategy_objective, strategy_view.strategy_objective_id"
                + " From strategy_view Order By strategy_view.theme_id, strategy_view.strategy_objective_rank; ";

            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptStrategyObjective.rdlc";

            frmprint.Show();

        }
        public static void PrintStrategy()
        {
            frmPrint frmprint = new frmPrint();
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            string strsql = "Select Distinct strategy_view.theme_id, strategy_view.theme_color,"
                + " concat(strategy_view.strategy_objective_id, ' - ', strategy_view.strategy_objective) as strategy_objective,"
                + " strategy_view.strategy_objective_id, strategy_view.strategy_id, strategy_view.strategy_rank,"
                + " concat(strategy_view.strategy_id, ' - ', strategy_view.strategy) as strategy"
                + " From strategy_view Order By strategy_view.theme_id, strategy_view.strategy_objective_rank, strategy_view.strategy_rank; ";
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptStrategy.rdlc";

            frmprint.Show();

        }
        public static void PrintStatus()
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            string strsql = "SELECT status.* FROM status ORDER BY id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptStatus.rdlc";

            frmprint.Show();

        }

        public static void PrintAction()
        {
            string strsql = "SELECT DISTINCT `view_strategy`.`theme_id` AS `theme_id`, `view_strategy`.`theme_color` AS `theme_color`,"
                + " `view_strategy`.`strategy_objective_id` AS `strategy_objective_id`, `view_strategy`.`strategy_objective` AS `strategy_objective`,"
                + " `view_strategy`.`strategy_id` AS `strategy_id`, `view_strategy`.`strategy` AS `strategy`, `action`.`id` AS `action_id`,"
                + " CONCAT_WS(' - ', `action`.`id`, `action`.`action_description`) AS `action`, `action`.`action_partner_org` AS `action_partner_org`,"
                + " `manager`.`manager_description` AS `manager_name`, `delivery_program`.`delivery_program_year` AS `delivery_program_year` FROM"
                + " (((`view_strategy` LEFT JOIN `action` ON((`view_strategy`.`strategy_id` = `action`.`strategy_id`))) JOIN `manager` ON((`action`.`manager_id` = `manager`.`id`)))"
                + " JOIN `delivery_program` ON((`delivery_program`.`action_id` = `action`.`id`)))"
                + " ORDER BY `view_strategy`.`theme_id` , `view_strategy`.`strategy_rank` , `action`.`action_rank`;";

            PrintAction(strsql);
        }

        public static void PrintCouncilSourcePlan(string query)
        {
            frmPrint frmprint = new frmPrint();
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, query);           
            tb.DefaultView.Sort = "action_rank" + " " + "asc";
            tb = tb.DefaultView.ToTable();

            frmprint.dataTable = tb;
                        frmprint.reportName = @"rptCouncilSourcePlan.rdlc";
            frmprint.Show();

        }
      
        public static void PrintAction(string query)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, query);


            DataTable temp = AlterActionTable(tb);

            frmprint.dataTable = temp;
            frmprint.reportName = @"rptAction.rdlc";

            frmprint.Show();

        }

        private static DataTable AlterActionTable(DataTable tb)
        {

            DataTable temp = tb.DefaultView.ToTable(true);
            temp = temp.DefaultView.ToTable(true, "action_id");

            String expression;
            foreach (DataRow row in temp.Rows)
            {

                DataRow[] result = tb.Select("action_id = '" + row["action_id"] + "'");

                expression = "";
                for (int i = 0; i < result.Length; i++)
                {
                    expression += result[i]["delivery_program_year"] + Environment.NewLine;
                }
                for (int i = 0; i < result.Length; i++)
                {
                    result[i]["delivery_program_year"] = expression;
                }
                Console.WriteLine(expression);

            }

            temp = tb.DefaultView.ToTable(true);

            return temp;
        }
        public static void PrintPerformanceIndicatior()
        {
            //frmPrint frmprint = new frmPrint();

            //Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            //string strsql = "SELECT `view_strategy`.`theme_id` AS `theme_id`, `view_strategy`.`theme_color` AS `theme_color`,"
            //    + " `view_strategy`.`strategy_objective_id` AS `strategy_objective_id`, `view_strategy`.`strategy_objective` AS `strategy_objective`,"
            //    + " `view_strategy`.`strategy_id` AS `strategy_id`, `view_strategy`.`strategy` AS `strategy`, `action`.`id` AS `id`,"
            //    + " CONCAT_WS(' - ', `action`.`id`, `action`.`action_description`) AS `action`, `action`.`action_partner_org` AS `action_partner_org`,"
            //    + " `manager`.`manager_name` AS `manager_name`, `delivery_program`.`delivery_program_year` AS `delivery_program_year` FROM"
            //    + " (((`view_strategy` LEFT JOIN `action` ON((`view_strategy`.`strategy_id` = `action`.`strategy_id`))) JOIN `manager` ON((`action`.`manager_id` = `manager`.`id`)))"
            //    + " JOIN `delivery_program` ON((`delivery_program`.`action_id` = `action`.`id`)))"
            //    + " WHERE (`delivery_program`.`delivery_program_year` = '" + OPGlobals.currentYear + "')"
            //    + " ORDER BY `view_strategy`.`theme_id` , `view_strategy`.`strategy_rank` , `action`.`action_rank`;";

            //DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //DataTable tb = db.GetDataTable(conn, strsql);

            //frmprint.dataTable = tb;
            //frmprint.reportName = @"rptAction.rdlc";

            //frmprint.Show();

        }
        public static void PrintMonthlyProgress(string query)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, query);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptMonthlyProgress.rdlc";

            frmprint.Show();

        }

        public static void PrintMonthlyProgressForCouncil(string query)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, query);

            frmprint.dataTable = tb;
            //frmprint.reportName = @"rptMonthlyProgress_forCouncil.rdlc";
            frmprint.reportName = @"rptMonthlyProgress_forCouncil.rdlc";
            frmprint.Show();

        }

        public static void PrintActionByThemeGraph()
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = new DataTable();
            string strsql = null;

            //=========================== REPORT 1 ===================

            strsql = "SELECT * FROM view_dashboard_top WHERE delivery_program_year = '" + OPGlobals.currentYear + "'"
                + " AND progress_month=" + OPGlobals.currentMonth + " ORDER BY theme_id, status_id";
            tb = db.GetDataTable(conn, strsql);
            if (tb.Rows.Count == 0)
            {
                strsql = "SELECT DISTINCT theme_id, theme_short,theme_color, theme_color,action_by_theme,"
                    + " null as status_id, null as status_short, null as status_color, null as completed_action, null as progress_month"
                    + " FROM view_dashboard_top WHERE delivery_program_year = '" + OPGlobals.prevoiusYear + "' ORDER BY theme_id";
                tb = db.GetDataTable(conn, strsql);
            }

            frmprint.dataTable = tb;
            frmprint.reportName = @"gphActionByTheme.rdlc";

            frmprint.Show();
        }
        public static void PrintActionByThemeGraph(string strsql)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = new DataTable();

            tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"gphActionByTheme.rdlc";

            frmprint.Show();
        }
        public static void PrintActionByStatusGraph(string strsql)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = new DataTable();
            //string strsql = null;

            ////=========================== REPORT 4 ===================

            //strsql = "Select status.id, status.status_short, status.status_color, A.action_completed, A.progress_year, A.progress_month"
            //    + " From status Left Join"
            //    + " (Select progress.progress_year, progress.progress_month, Count(progress.id) As action_completed, progress.status_id"
            //    + " From progress Where progress_year = '" + OPGlobals.currentYear + "' and progress_month = " + OPGlobals.currentMonth
            //    + " Group By progress.progress_year, progress.progress_month, progress.status_id) A On status.id = A.status_id";

            tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"gphActionByStatus.rdlc";

            frmprint.Show();
        }

        public static void PrintStrategyDashboard(string strsql)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = new DataTable();
            tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptCompletedActionsByStrategy.rdlc";

            frmprint.Show();

        }

        public static void PrintCapitalWorks(string cpwYear)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "SELECT * FROM view_cpw where cpw_year='" + cpwYear + "';";
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptcpw.rdlc";

            frmprint.Show();
        }
        public static void PrintCapitalWorksMonthlyProgress(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();

            frmprint.dataTable = CapitalWork.GetTableCapitalWorksMonthlyProgress(cpw_year, cpw_month);  
            frmprint.reportName = @"rptcpw_progress.rdlc";

            frmprint.Show();

        }
        public static void PrintCapitalWorksMonthlyProgressByServicePlan(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();

            frmprint.dataTable = CapitalWork.GetTableCapitalWorksMonthlyProgress(cpw_year, cpw_month);
            frmprint.reportName = @"rptcpw_progress_by_service_plan.rdlc";

            frmprint.Show();

        }
        public static void PrintCapitalWorksSummary(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();
            frmprint.dataTable = CapitalWork.GetTableCapitalWorksDepartmentSummary(cpw_year, cpw_month);
            frmprint.reportName = @"gphBudgetSummary.rdlc";

            frmprint.Show();
        }
        public static void PrintCapitalWorksServiceSummary(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();
            frmprint.dataTable = CapitalWork.GetTableCapitalWorksServiceSummary(cpw_year, cpw_month);
            frmprint.reportName = @"rptCPWServiceSummary.rdlc";

            frmprint.Show();
        }
        public static void PrintCapitalWorksServiceDetails(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();
            frmprint.dataTable = CapitalWork.GetTableCapitalWorksServiceDetails(cpw_year, cpw_month);
            frmprint.reportName = @"rptCPWServiceWise.rdlc";

            frmprint.Show();
        }
        public static void PrintCapitalWorksServiceDetails(string cpw_year, int cpw_month, string servicePlanID)
        {
            frmPrint frmprint = new frmPrint();
            frmprint.dataTable = CapitalWork.GetTableCapitalWorksServiceDetails(cpw_year, cpw_month, servicePlanID);
            frmprint.reportName = @"rptCPWServiceWise.rdlc";

            frmprint.Show();
        }

        public static void PrintCapitalWorksDepartmentSummary(string cpw_year, int cpw_month, string department)
        {
            frmPrint frmprint = new frmPrint();
            frmprint.dataTable = CapitalWork.GetTableCapitalWorksDepartmentSummary(cpw_year, cpw_month, department);
            frmprint.reportName = @"gphBudgetSummaryDepartment.rdlc";

            frmprint.Show();
        }
        public static void PrintCapitalWorksQBR(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();

            frmprint.dataTable = CapitalWork.GetTableCapitalWorksMonthlyProgress(cpw_year, cpw_month);
            frmprint.reportName = @"rptcpwqbr.rdlc";

            frmprint.Show();

        }

        public static void PrintCapitalWorksCounil(string cpw_year, int cpw_month)
        {
            frmPrint frmprint = new frmPrint();

            frmprint.dataTable = CapitalWork.GetTableCapitalWorksMonthlyProgress(cpw_year, cpw_month);
            frmprint.reportName = @"rptcpw_progressCouncil.rdlc";


            frmprint.Show();

        }
        public static void PrintCapitalWorksCounil(string cpw_year, int cpw_month, string serviceID)
        {
            frmPrint frmprint = new frmPrint();

            frmprint.dataTable = CapitalWork.GetTableCapitalWorksMonthlyProgress(cpw_year, cpw_month, serviceID);
            frmprint.reportName = @"rptcpw_progressCouncil.rdlc";

            frmprint.Show();

        }
        public static void PrintCapitalWorksCounil(string cpw_year, int cpw_month, string directorId, string managerId)
        {
            frmPrint frmprint = new frmPrint();

            frmprint.dataTable = CapitalWork.GetTableCapitalWorksMonthlyProgress(cpw_year, cpw_month, directorId, managerId);
            frmprint.reportName = @"rptcpw_progressCouncil.rdlc";

            

            frmprint.Show();

        }

        internal static void PrintSubReport(string servicePlanSql, string actionSql, string kpmSql, string cwpSql)
        {
            frmPrint frmprint = new frmPrint();
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable dtServicePlan = db.GetDataTable(conn, servicePlanSql);
            DataTable dtAction = db.GetDataTable(conn, actionSql);
            DataTable dtKPM = db.GetDataTable(conn, kpmSql);
            DataTable dtCWP = db.GetDataTable(conn, cwpSql);

            frmprint.dataTable = dtServicePlan;
            frmprint.dataTable2 = dtAction;
            frmprint.dataTable3 = dtKPM;
            frmprint.dataTable4 = dtCWP;



            frmprint.reportName = @"rptCouncilReport.rdlc";
            //frmprint.reportName = @"Report1.rdlc";
            frmprint.Show();
        }

        public static void PrintKPIMonthlyProgress(string query)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, query);

            frmprint.dataTable = tb;
            frmprint.reportName = @"Report3.rdlc";

            frmprint.Show();

        }

        private static void PrintKPIProgressReport(string strsql)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptKPIProgress.rdlc";

            frmprint.Show();

        }

        private static void PrintKPIProgressReportCouncil(string strsql)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptKPIProgress_Council.rdlc";

            frmprint.Show();

        }

        private static void PrintKPIProgressReportFull(string strsql)
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            frmprint.reportName = @"rptKPMProgressFull.rdlc";

            frmprint.Show();

        }

        public static void PrintKPIProgress(string opYear, int opMonth)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth);
            PrintKPIProgressReport(strsql);
        }
        public static void PrintKPIProgress(string opYear, int opMonth, string directorID)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID);
            PrintKPIProgressReport(strsql);
        }
        public static void PrintKPIProgress(string opYear, int opMonth, string directorID, string managerId)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID, managerId);
            PrintKPIProgressReport(strsql);
        }

        public static void PrintKPIProgressCouncil(string opYear, int opMonth)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth);
            PrintKPIProgressReportCouncil(strsql);
        }
        public static void PrintKPIProgressCouncil(string opYear, int opMonth, string directorID)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID);
            PrintKPIProgressReportCouncil(strsql);
        }
        public static void PrintKPIProgressCouncil(string serviceID, string opYear, int opMonth)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(serviceID, opYear, opMonth);
            PrintKPIProgressReportCouncil(strsql);
        }
        public static void PrintKPIProgressCouncil(string opYear, int opMonth, string directorID, string managerId)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID, managerId);
            PrintKPIProgressReportCouncil(strsql);
        }

        public static void PrintKPIProgressFull(string opYear, int opMonth)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth);
            PrintKPIProgressReportFull(strsql);
        }
        public static void PrintKPIProgressFull(string opYear, int opMonth, string directorID)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID);
            PrintKPIProgressReportFull(strsql);
        }
        public static void PrintKPIProgressFull(string opYear, int opMonth, string directorID, string managerId)
        {
            string strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID, managerId);
            PrintKPIProgressReportFull(strsql);
        }

        public static void TestSubReport(string opYear, int opMonth, string directorID, string managerID)
        {
            frmPrint frmprint = new frmPrint();

            List<DataTable> tbList = new List<DataTable>();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            string strsql = "";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            //strsql = ReportQueries.QMonthlyProgress(opYear, opMonth, false, directorID, managerID);
            strsql = MonthlyProgress.GetQueryMonthlyProgress(opYear, opMonth, directorID, managerID);
            DataTable tb = db.GetDataTable(conn, strsql);
            frmprint.dataTable = tb;

            tb = CapitalWork.GetTableCapitalWorksMonthlyProgress(opYear, opMonth, directorID, managerID);
            tbList.Add(tb);

            strsql = KeyPerformanceIndex.GetMonthlyKPIProgressQuery(opYear, opMonth, directorID, managerID);
            tb = db.GetDataTable(conn, strsql);
            tbList.Add(tb);

            frmprint.subDataTable = tbList;

            frmprint.dataTable = tb;
            frmprint.reportName = @"rpt1.rdlc";

            frmprint.Show();

        }

        public static void PrintDeliveryProgram()
        {
            frmPrint frmprint = new frmPrint();

            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //string strsql = "SELECT view_delivery_program.* FROM view_delivery_program;";
            string strsql = "SELECT * FROM view_delivery_program_17_21;";
            DataTable tb = db.GetDataTable(conn, strsql);

            frmprint.dataTable = tb;
            //frmprint.reportName = @"rptStrategyMeasureMonthly.rdlc";
            frmprint.reportName = @"Report2.rdlc";

            frmprint.Show();

        }
        private static void PrepareDataForDeliveryProgramReport()
        {
            string strsql = "SELECT DISTINCT view_delivery_program.* FROM view_delivery_program;";
        }

    }
}
