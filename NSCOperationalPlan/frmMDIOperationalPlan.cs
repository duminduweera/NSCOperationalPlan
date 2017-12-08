using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using MyDLLs;


namespace NSCOperationalPlan
{
    public partial class frmOperationPlan : Form
    {
        private static frmOperationPlan Instance;
        frmDashBoard2 frmdashboard = new frmDashBoard2();
        int frmtop = 0;
        int frmleft = 0;

        public static frmOperationPlan getInstance()
        {
            if(Instance == null)
            {
                Instance = new frmOperationPlan();
            }
            return Instance;
        }

        private frmOperationPlan()
        {
            Instance = this;
            InitializeComponent();
            
            frmtop = this.menuStrip1.Bottom + this.menuStrip1.Height+ toolStrip1.Height;
           

            //OpenFrmMonthlyProgressNEW(new object(),new EventArgs());
        }
        private void frmOperationPlan_Load(object sender, EventArgs e)
        {
            ArrangeMenu();

            //RefreshForm();

            frmdashboard.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmdashboard.MdiParent = this;

            frmdashboard.Show();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            tsCPW.Enabled = OPGlobals.CapitalWorksEnabled;
            capitalWorkMonthlyProgressToolStripMenuItem.Enabled = OPGlobals.CapitalWorksEnabled;

        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Program.CloseApplication("Do you want to Exit This Application?",true);
        }

        private void OpenFrmMonthlyProgressNEW(object sender, EventArgs e)
        {
           //frmMonthlyProgressNew frmMonthlyProgressNew = new frmMonthlyProgressNew();
           // ArrangeForm(frmMonthlyProgressNew);
           // frmMonthlyProgressNew.ShowDialog();        //.Show();
           
        }
        private void mnuMonthlyProgress_Click(object sender, EventArgs e)
        {
            frmMonthlyProgress frmmonthlyprogress = new frmMonthlyProgress();
            ArrangeForm(frmmonthlyprogress);
            frmmonthlyprogress.ShowDialog();        //.Show();
        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            mnuExit_Click(sender, e);
        }

        private void mnuThemes_Click(object sender, EventArgs e)
        {
            frmTheme frmtheme = new frmTheme();
            ArrangeForm(frmtheme);
            frmtheme.ShowDialog();  // .Show();
        }

        private void mnuStrategies_Click(object sender, EventArgs e)
        {
            frmStrategy frmstrategy = new frmStrategy();
            ArrangeForm(frmstrategy);
            frmstrategy.ShowDialog();       //.Show();
        }

        private void mnuStatus_Click(object sender, EventArgs e)
        {
            frmStatus frmstatus = new frmStatus();
            ArrangeForm(frmstatus);
            frmstatus.ShowDialog();     // .Show();
        }

        private void mnuPI_Click(object sender, EventArgs e)
        {
            //------- Before add new KPI---
            //frmPIndicator frmpindicator = new frmPIndicator();
            //ArrangeForm(frmpindicator);
            //frmpindicator.ShowDialog();     // .Show();
            //-----------END

            frmKPI frmpindicator = new frmKPI();
            ArrangeForm(frmpindicator);
            frmpindicator.ShowDialog();     // .Show();
        }

        private void mnuActions_Click(object sender, EventArgs e)
        {
            frmAction frmaction = new frmAction();
            ArrangeForm(frmaction);
            frmaction.ShowDialog();     // .Show();
        }
        private void mnuBackup_Click(object sender, EventArgs e)
        {
            //Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            //DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //try
            //{
            //    db.BackupDatabase(conn, "nsc_operation_plan_17_to_21", "c:\\nsc_operation_plan_17_to_21");
            //    //db.BackupDatabase(conn);
            //    MessageBox.Show("Success");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void mnuRestore_Click(object sender, EventArgs e)
        {
            //Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            //DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //try
            //{
            //    db.RestorDatabase(conn, "nsc_test_Import", "C:\\nsc_operation_plan_17_to_21.sql");
            //    MessageBox.Show("Success");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void mnuStrategyObjective_Click(object sender, EventArgs e)
        {
            frmStrategyObjective frmstrategyobjective = new frmStrategyObjective();
            ArrangeForm(frmstrategyobjective);
            frmstrategyobjective.ShowDialog();       // .Show();
        }

        public void ArrangeMenu()
        {
            DisableAllMenuOptions();
            RefreshForm();
        }
        private void DisableAllMenuOptions()
        {
            EnableAllMenuOptions();
            try
            {
                if(OPGlobals.CurrentUser.DisableMenuOptions != null)
                {
                    foreach (string tag in OPGlobals.CurrentUser.DisableMenuOptions)
                    {
                        //==============Menu Items=================
                        foreach (ToolStripMenuItem mnuitem in menuStrip1.Items)
                        {
                            try
                            {
                                var items = mnuitem.DropDownItems;
                                var item = items.Cast<ToolStripItem>().FirstOrDefault(control => String.Equals(control.Tag, tag));
                                if (item != null)
                                {
                                    item.Enabled = false;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        //==============Toolbar======================
                        var items1 = toolStrip1.Items;
                        var item1 = items1.Cast<ToolStripItem>().FirstOrDefault(control => String.Equals(control.Tag, tag));
                        if (item1 != null)
                        {
                            item1.Enabled = false;
                            //break;
                        }
                    }
                }

            } catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EnableAllMenuOptions()
        {
            foreach(ToolStripMenuItem mnuitem in menuStrip1.Items)
            {
                foreach (ToolStripItem item in mnuitem.DropDownItems)
                {
                    if (item is ToolStripSeparator)
                        continue;
                    item.Enabled = true;
                }
            }
            foreach(ToolStripItem btn in toolStrip1.Items)
            {
                if(btn is ToolStripSeparator) { continue; }
                btn.Enabled = true;

            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmOperationPlan_Activated(object sender, EventArgs e)
        {
            frmdashboard.Visible = true;
            
            //RefreshForm();
            //MessageBox.Show("test");
           
        }

        private void frmOperationPlan_Deactivate(object sender, EventArgs e)
        {
            frmdashboard.Visible = false;
        }

        private void ArrangeForm(Form frmName)
        {
            frmName.StartPosition = FormStartPosition.Manual;
            frmName.ShowIcon = false;
            frmName.MaximizeBox = false;
            frmName.MinimizeBox = false;
            frmName.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmName.ControlBox = true;
            frmName.Top = frmtop;
            frmName.Left = frmleft;
        }
        private void ArrangeForm(Form frmName, Boolean centre)
        {
            if (centre == true) { frmName.StartPosition = FormStartPosition.CenterParent; }
            
            frmName.ShowIcon = false;
            frmName.MaximizeBox = false;
            frmName.MinimizeBox = false;
            frmName.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmName.ControlBox = true;
            frmName.Top = frmtop;
            frmName.Left = frmleft;
        }

        private void tsExit_Click_1(object sender, EventArgs e)
        {
            mnuExit_Click(sender, e);
        }

        private void tsMonthlyProgress_Click(object sender, EventArgs e)
        {
            mnuMonthlyProgress_Click(sender, e);
        }

        private void mnuRptTheme_Click(object sender, EventArgs e)
        {
            clsReports.PrintTheme();
        }

        private void mnuRptStrategyObjective_Click(object sender, EventArgs e)
        {
            clsReports.PrintStrategyObjective();
        }

        private void mnuRptStrategy_Click(object sender, EventArgs e)
        {
            clsReports.PrintStrategy();
        }

        private void mnuRptStatus_Click(object sender, EventArgs e)
        {
            clsReports.PrintStatus();
        }

        private void mnuRptAction_Click(object sender, EventArgs e)
        {
            frmRptAction frmrptaction = new frmRptAction();
            ArrangeForm(frmrptaction);
            frmrptaction.ShowDialog(); 
        }

        private void loginAsADifferentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frmlogin = new frmLogin();
            ArrangeForm(frmlogin,true);
            frmlogin.ShowDialog();

        }

        private void RefreshForm()
        {
            statUser.Text =  OPGlobals.CurrentUser.ManagerID + " - " + OPGlobals.CurrentUser.UserName ;
            statYear.Text = "Financial Year - " + OPGlobals.currentYear ;
            statMonth.Text = "Current Month - " + Enum.GetName(typeof(Months), (int)OPGlobals.currentMonth) ;  //OPGlobals.currentMonth;
            statDepartment.Text = OPGlobals.CurrentUser.Department;
            statDivision.Text = OPGlobals.CurrentUser.Division;
        }

        private void AddDepartmentToStatus()
        {
            //Label mDepartment = new Label();
            //mDepartment.Text = OPGlobals.CurrentUser.Department;

            //Label mDivision = new Label();
            //mDivision.Text = OPGlobals.CurrentUser.Division;

            //statusStrip1.Items.Add(new ToolStrip)

            //StatusStrip1.Items.Add(New ToolStripControlHost(NewButton))
        }

        private void logBackToPreviousUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OPGlobals.PreviousUser == null)
            {
                MessageBox.Show("Sorry, There is no Previous user", "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string msg = "This will revert current user back to the previous User, Do you want to Continue?";
                if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    OPGlobals.CurrentUser = OPGlobals.PreviousUser;
                    OPGlobals.PreviousUser = null;
                    ArrangeMenu();
                    MessageBox.Show("Welcome Back " + OPGlobals.CurrentUser.UserName, "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //private void mnuRptProgress_Click(object sender, EventArgs e)
        //{
        //    string strquery = null;
        //    clsReports.PrintMonthlyProgress(strquery);
        //}

        private void setCurrentMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParameters frmparams = new frmParameters();
            ArrangeForm(frmparams, true);
            frmparams.ShowDialog();

        }

        private void mnuMonthlyKPIProgress_Click(object sender, EventArgs e)
        {
            frmKPIProgress frmkpiprogress = new frmKPIProgress();
            ArrangeForm(frmkpiprogress);
            frmkpiprogress.ShowDialog();       // .Show();

        }

        private void originalBudgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCPW frmcpw = new frmCPW();
            ArrangeForm(frmcpw);
            frmcpw.ShowDialog();       // .Show();
        }

        private void capitalWorkMonthlyProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCPWMonthly frmcpwmonthly = new frmCPWMonthly();
            ArrangeForm(frmcpwmonthly);
            frmcpwmonthly.ShowDialog();       // .Show();
        }

        private void capitalWorkProgramToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorksMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
        }

        private void monthlyProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorks(OPGlobals.currentYear);
        }

        private void tsCPW_Click(object sender, EventArgs e)
        {
            capitalWorkMonthlyProgressToolStripMenuItem_Click(sender, e);
        }
        private void MonthlyYTDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCPWYTDActual frmytd = new frmCPWYTDActual();
            ArrangeForm(frmytd);
            frmytd.ShowDialog();       // .Show();
        }
        private void QBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCPWQuartelyUpdate frmqbr = new frmCPWQuartelyUpdate();
            ArrangeForm(frmqbr);
            frmqbr.ShowDialog();
        }

        private void mnuRptCPWSummary_Click(object sender, EventArgs e)
        {
            frmCPWRPTSummary frmcpwrpt = new frmCPWRPTSummary();
            //frmcpwrpt.ReportType = "MonthlyProgress";
            ArrangeForm(frmcpwrpt);
            frmcpwrpt.ShowDialog();

        }

        private void mnuRptProgress_Click(object sender, EventArgs e)
        {
            frmRptMonthlyProgress frmrptmonthlyprogress = new frmRptMonthlyProgress();
            ArrangeForm(frmrptmonthlyprogress);
            frmrptmonthlyprogress.ShowDialog();
        }

        private void councilReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCouncilReport frmcouncilreport = new frmCouncilReport();
            ArrangeForm(frmcouncilreport);
            frmcouncilreport.ShowDialog();

        }

        private void mnuRptPI_Click(object sender, EventArgs e)
        {
            clsReports.PrintKPIProgressFull(OPGlobals.currentYear, OPGlobals.currentMonth);
        }

        private void tsMonthlyCPWProgress_Click(object sender, EventArgs e)
        {
            mnuMonthlyKPIProgress_Click(sender, e);
        }

        //private void testSubReportToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmCouncilReport frmcouncilreport = new frmCouncilReport();
        //    ArrangeForm(frmcouncilreport);
        //    frmcouncilreport.ShowDialog();
        //}

        private void importCPWFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportCPWfromExcel frmImportCPW = new frmImportCPWfromExcel();
            ArrangeForm(frmImportCPW);
            frmImportCPW.ShowDialog();
        }

        private void mnuRptCouncilSource_Click(object sender, EventArgs e)
        {
            frmRptSourcePlan frmSP = new frmRptSourcePlan();
            ArrangeForm(frmSP);
            frmSP.ShowDialog();
        }

        private void tsStrategyMeasure_Click(object sender, EventArgs e)
        {
            frmStrategyMeasuresMonthly frmStraMeasureMonthly = new frmStrategyMeasuresMonthly();
            ArrangeForm(frmStraMeasureMonthly);
            frmStraMeasureMonthly.ShowDialog();
        }

        private void importSMeasuresFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImportStartegyMeasuresfromExcel frmImportSMeasures = new frmImportStartegyMeasuresfromExcel();
            ArrangeForm(frmImportSMeasures);
            frmImportSMeasures.ShowDialog();
        }

        private void reAssignManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReAssignManager frm = new FrmReAssignManager();
            ArrangeForm(frm);
            frm.ShowDialog();
        }

        //private void subReportingTestingToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    string month = OPGlobals.currentMonth.ToString(); ;
        //    string year = OPGlobals.currentYear.ToString();
        //    string servicePlanSql = "Select A.id As service_plan_id, A.service_plan, B.actions As action,C.kpm As kpm, D.cwp As cwp From service_plan A Left Join(Select action.service_plan_id, COUNT(*) As actions From action Group By action.service_plan_id) B On B.service_plan_id = A.id Left Join (Select kpi.service_plan_id, COUNT(*) As kpm From kpi Group By kpi.service_plan_id) C On C.service_plan_id = A.id Left Join (Select capital_works.capital_works_service_plann_id As service_plan_id, COUNT(*) As cwp From capital_works Group By capital_works.capital_works_service_plann_id) D On D.service_plan_id = A.id where service_plan != '-NONE-';";     
        //    string actionSql = "Select A.theme_id, A.theme_short, A.theme_color, A.strategy_objective_id, concat(A.strategy_objective_id,' ', A.strategy_objective) as strategy_objective, A.strategy_objective_rank, A.strategy_id, A.strategy_rank, concat(A.strategy_id, ' ', A.strategy) as strategy, A.action_id, A.action_rank, A.action_description as action_description, A.action_partner_org, A.manager_id, A.manager_name, A.manager_description, A.sub_department, A.director_id, A.director_name, A.director_description, A.department, A.delivery_program_id, A.delivery_program_year, A.delivery_program_TargetDate, A.service_plan_id, A.service_plan, B.progress_id, B.status_id, B.progress_description, B.progress_pecentage, B.status_short, B.status_color, B.progress_year, B.progress_month, 'September' as delivery_program_month from (Select * From view_action_by_year Where view_action_by_year.delivery_program_year = '17/18') A Left Join (Select progress.id As progress_id, progress.progress_description, progress.progress_pecentage, status.id As status_id, status.status_short, status.status_color, progress.action_id, progress.progress_year, progress.progress_month From progress Inner Join status On progress.status_id = status.id Where progress.progress_year = '17/18' And progress.progress_month = 9) B On A.action_id = B.action_id";
        //    string kpmSql = "Select A.kpi_id, A.kpm_id, A.kpm_description, A.manager_id, A.manager_name, A.manager_description, A.director_description, A.director_name, A.director_id, A.department, A.sub_department, A.efficiency_description, A.kpi_prefix_id, A.kpi_estimate_year, A.kpi_prefix, A.kpi_prefix_short, A.kpi_estimate, A.unit_id, A.kpi_unit, A.kpi_unit_short, A.service_plan_id, A.service_plan, B.kpi_year, B.kpi_month, B.kpi_progress, B.kpi_remark From view_kpi A Left Join (Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark From kpi_progress Where kpi_progress.kpi_year = '17/18' And kpi_progress.kpi_month = 9) B On A.kpi_id = B.kpi_id;";

        //    string cwpSql = "Select A.*, 9 As cpw_month, C.capital_works_ytd As cpw_ytod, D.capital_works_projected As cpw_projected, D.capital_works_percentage As cpw_percentage, D.capital_works_remark As cpw_remark From view_cpw_qbr A Left Join (Select capital_works_ytd.* From capital_works_ytd Where capital_works_ytd.capital_works_month = 9 And capital_works_ytd.capital_works_year = '17/18 ') C On A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year Left Join (Select capital_works_monthly_progress.* From capital_works_monthly_progress Where capital_works_monthly_progress.capital_works_month = 9 And capital_works_monthly_progress.capital_works_year = '17/18') D On A.cpw_id = D.capital_works_id And A.cpw_year = D.capital_works_year ORDER BY A.director_id, A.cpw_manager_id, A.cpw_id;";
        //    clsReports.PrintSubReport(servicePlanSql, actionSql, kpmSql, cwpSql);

        //}
    }

}
