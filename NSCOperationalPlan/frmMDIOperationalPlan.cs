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
        frmDashBoard2 frmdashboard = new frmDashBoard2();
        int frmtop = 0;
        int frmleft = 0;

        public frmOperationPlan()
        {
         
            InitializeComponent();
            
            frmtop = this.menuStrip1.Bottom + this.menuStrip1.Height+ toolStrip1.Height;
           

            //OpenFrmMonthlyProgressNEW(new object(),new EventArgs());
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

        private void frmOperationPlan_Load(object sender, EventArgs e)
        {
            ArrangeMenu();

            RefreshForm();

            frmdashboard.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmdashboard.MdiParent = this;

            frmdashboard.Show();
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            tsCPW.Enabled = OPGlobals.CapitalWorksEnabled;
            capitalWorkMonthlyProgressToolStripMenuItem.Enabled= OPGlobals.CapitalWorksEnabled;

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

        private void ArrangeMenu()
        {
            DisableAllMenuOptions();
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

        private void tsTheme_Click(object sender, EventArgs e)
        {
            mnuThemes_Click(sender, e);
        }

        private void tsStrategyObjective_Click(object sender, EventArgs e)
        {
            mnuStrategyObjective_Click(sender, e);
        }

        private void tsStrategy_Click(object sender, EventArgs e)
        {
            mnuStrategies_Click(sender, e);
        }

        private void tsAction_Click(object sender, EventArgs e)
        {
            mnuActions_Click(sender, e);
        }

        private void tsStatus_Click(object sender, EventArgs e)
        {
            mnuStatus_Click(sender, e);
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
                OPGlobals.CurrentUser = OPGlobals.PreviousUser;
                OPGlobals.PreviousUser = null;
                frmOperationPlan_Activated(this, e);
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

        private void testSubReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCouncilReport frmcouncilreport = new frmCouncilReport();
            ArrangeForm(frmcouncilreport);
            frmcouncilreport.ShowDialog();
        }

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
    }

}
