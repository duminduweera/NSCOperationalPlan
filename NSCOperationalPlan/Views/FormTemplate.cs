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
    public partial class FormTemplate : Form
    {
        bool mIsNew = true;
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public FormTemplate()
        {
            InitializeComponent();
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 50);                   //0
            dct.Add("Theme", 80);              //1
            dct.Add("Strategy", 190);           //2
            dct.Add("Description", 270);        //3
            dct.Add("Org.Budget", 120);         //4
            dct.Add("Rev.Budget", 120);         //5
            dct.Add("Service Plan", 210);       //6
            dct.Add("Manager", 210);            //7

            dct.Add("cpwID", 0);                //8
            dct.Add("ThemeID", 0);              //9
            dct.Add("Strat_Obj_id", 0);         //10
            dct.Add("DirectorID", 0);           //11
            dct.Add("ServicePlanID", 0);        //12
            dct.Add("ManagerID", 0);            //13
            dct.Add("ThemeColor", 0);           //14
            dct.Add("JobNo", 100);                //15
            dct.Add("CarryOver", 0);                //16

            int[] hiddenRows = { 8, 9, 10, 11, 12, 13, 14, 16 };
            //int[] hiddenRows = { };

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);

            this.dgv01.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv01.RowTemplate.MinimumHeight = 28;

            dgv01.ReadOnly = true;
        }
        private void ClearData()
        {
        }

        #region ===== TO BE IMPLEMENTED =====
        //private void frmCPW_Load(object sender, EventArgs e)
        //{
        //    ArrangeGrid();

        //    LoadDirectors();
        //    LoadThemes();
        //    LoadServicePlan();

        //    LoadTableFromDatabase();

        //    tsbNew_Click(sender, e);
        //    if (OPGlobals.CurrentUser.Permission == UserRights.Administrator) { tsbDelete.Enabled = true; }

        //}
        //private void LoadTableFromDatabase()
        //{
        //    DataRowView row1 = (DataRowView)cboDirector.SelectedItem;
        //    DataRowView row2 = (DataRowView)cboManager.SelectedItem;
        //    string strsql;
        //    strsql = "SELECT * FROM view_cpw_qbr"
        //        + " where cpw_year='" + OPGlobals.currentYear
        //        + "' AND director_id='" + row1[0] + "' AND cpw_manager_id = '" + row2[0] + "'"
        //        + " Order by director_id, cpw_manager_id, cpw_id;";

        //    LoadTableFromDatabase(strsql);
        //}
        //private void LoadTableFromDatabase(string strsql)
        //{
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    dgv01.Rows.Clear();
        //    dgv01.Refresh();
        //    cboServicePlan.SelectedIndex = 0;
        //    //DataRowView row1 = (DataRowView)cboDirector.SelectedItem;
        //    //DataRowView row2 = (DataRowView)cboManager.SelectedItem;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(cboDirector.SelectedValue.ToString())) { return; }
        //        //string strsql;
        //        //strsql = "SELECT * FROM view_cpw_qbr"
        //        //    + " where cpw_year='" + OPGlobals.currentYear
        //        //    + "' AND director_id='" + row1[0] + "' AND cpw_manager_id = '" + row2[0] + "'"
        //        //    + " Order by director_id, cpw_manager_id, cpw_id;";

        //        DataTable tb = db.GetDataTable(conn, strsql);
        //        foreach (DataRow row in tb.Rows)
        //        {
        //            dgv01.Rows.Add(new String[] {(dgv01.RowCount+1).ToString(),
        //                row["theme_id"].ToString(),
        //                row["strategy_objective"].ToString(),
        //                row["cpw_description"].ToString(),
        //                string.Format("{0:$0,0.00}", row["cpw_original_budget"]),
        //                string.Format("{0:$0,0.00}", row["cpw_revised_budget"]),
        //                row["service_plan"].ToString(),
        //                row["manager_description"].ToString(),
        //                row["cpw_id"].ToString(),
        //                row["theme_id"].ToString(),
        //                row["cpw_stg_obj_id"].ToString(),
        //                row["director_id"].ToString(),
        //                row["cpw_service_plann_id"].ToString(),
        //                row["cpw_manager_id"].ToString(),
        //                row["theme_color"].ToString(),
        //                row["cpw_jobno"].ToString(),
        //                string.Format("{0:$0,0.00}", row["cpw_carry_over"])
        //            });
        //        }
        //        dgv01.CurrentCell = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        //private void LoadDataFromGrid()
        //{
        //    txtcpwid.Text = dgv01.CurrentRow.Cells["cpwID"].Value.ToString();
        //    txtJobCostNumber.Text = dgv01.CurrentRow.Cells["jobno"].Value.ToString();
        //    if (!string.IsNullOrEmpty(dgv01.CurrentRow.Cells["ThemeID"].Value.ToString()))
        //    {
        //        cboTheme.SelectedValue = dgv01.CurrentRow.Cells["ThemeID"].Value.ToString();
        //    }
        //    if (!string.IsNullOrEmpty(dgv01.CurrentRow.Cells["Strat_Obj_id"].Value.ToString()))
        //    {
        //        cboStrategyObj.SelectedValue = dgv01.CurrentRow.Cells["Strat_Obj_id"].Value.ToString();
        //    }
        //    txtCPWDescription.Text = dgv01.CurrentRow.Cells["Description"].Value.ToString();
        //    if (!string.IsNullOrEmpty(dgv01.CurrentRow.Cells["Org.Budget"].Value.ToString()))
        //    {
        //        txtCPWBudget.Text = dgv01.CurrentRow.Cells["Org.Budget"].Value.ToString().Remove(0, 1);
        //    }
        //    if (!string.IsNullOrEmpty(dgv01.CurrentRow.Cells["Rev.Budget"].Value.ToString()))
        //    {
        //        txtRevisedBudget.Text = dgv01.CurrentRow.Cells["Rev.Budget"].Value.ToString().Remove(0, 1);
        //    }

        //    if (!string.IsNullOrEmpty(dgv01.CurrentRow.Cells["CarryOver"].Value.ToString()))
        //    {
        //        txtCarryOver.Text = dgv01.CurrentRow.Cells["CarryOver"].Value.ToString().Remove(0, 1);
        //    }

        //    //cboDirector.SelectedValue = dgv01.CurrentRow.Cells["DirectorID"].Value.ToString();
        //    //cboManager.SelectedValue = dgv01.CurrentRow.Cells["ManagerID"].Value.ToString();
        //    cboServicePlan.Text = dgv01.CurrentRow.Cells["Service Plan"].Value.ToString();
        //}
        //private void UpdateDataGrid(int cpwid)
        //{
        //    if (string.IsNullOrEmpty(txtcpwid.Text)) {
        //        dgv01.Rows.Add(new String[] { (dgv01.RowCount + 1).ToString() });
        //        dgv01.CurrentCell = dgv01.Rows[dgv01.RowCount-1].Cells[0];
        //    }

        //    dgv01.CurrentRow.Cells["cpwID"].Value = cpwid.ToString();

        //    dgv01.CurrentRow.Cells["jobno"].Value = txtJobCostNumber.Text;

        //    if (cboTheme.SelectedValue.ToString() != "-0-")
        //    {
        //        DataRowView row = (DataRowView)cboTheme.SelectedItem;
        //        dgv01.CurrentRow.Cells["ThemeColor"].Value = row["theme_color"].ToString();
        //        dgv01.CurrentRow.Cells["Theme"].Value = row["id"].ToString();
        //        dgv01.CurrentRow.Cells["ThemeID"].Value = row["id"].ToString();
        //    }
        //    if (!string.IsNullOrEmpty(cboStrategyObj.Text))
        //    {
        //        dgv01.CurrentRow.Cells["Strategy"].Value = cboStrategyObj.Text;
        //        dgv01.CurrentRow.Cells["Strat_Obj_id"].Value = cboStrategyObj.SelectedValue.ToString();
        //    }
        //    dgv01.CurrentRow.Cells["Description"].Value = txtCPWDescription.Text;
        //    dgv01.CurrentRow.Cells["Org.Budget"].Value = string.Format("{0:$0,0.00}", double.Parse(txtCPWBudget.Text));
        //    dgv01.CurrentRow.Cells["Rev.Budget"].Value = string.Format("{0:$0,0.00}", double.Parse(txtRevisedBudget.Text));
        //    dgv01.CurrentRow.Cells["CarryOver"].Value = string.Format("{0:$0,0.00}", double.Parse(txtCarryOver.Text));
        //    if (!string.IsNullOrEmpty(cboServicePlan.Text))
        //    {
        //        dgv01.CurrentRow.Cells["ServicePlanID"].Value = cboServicePlan.SelectedValue.ToString();
        //        dgv01.CurrentRow.Cells["Service Plan"].Value = cboServicePlan.Text;
        //    }
        //    dgv01.CurrentRow.Cells["DirectorID"].Value = cboDirector.SelectedValue.ToString();
        //    dgv01.CurrentRow.Cells["Manager"].Value = cboManager.Text;
        //    dgv01.CurrentRow.Cells["ManagerID"].Value = cboManager.SelectedValue.ToString();


        //    dgv01.CurrentCell.Selected = false;
        //    EventArgs e = new EventArgs();
        //    tsbNew_Click(this, e);
        //}
        //private void LoadDirectors()
        //{
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");
        //    cboDirector.DataSource = tb;
        //    cboDirector.DisplayMember = "director_description";
        //    cboDirector.ValueMember = "director_id";
        //}
        //private void LoadManagers()
        //{
        //    DataRowView row = (DataRowView)cboDirector.SelectedItem;

        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + row["director_id"].ToString() + "';");

        //    cboManager.DataSource = tb;
        //    cboManager.DisplayMember = "manager_description";
        //    cboManager.ValueMember = "manager_id";
        //    //LoadServicePlan();

        //}
        //private void LoadServicePlan()
        //{
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    string strsql = @"SELECT * FROM service_plan;";
        //    //string strsql = @"SELECT * FROM service_plan WHERE id <> '000'";
        //    //+ " AND service_plan_manager_id = '" + row["manager_id"].ToString() + "';";
        //    DataTable tb = db.GetDataTable(conn, strsql);
        //    DataRow dr = tb.NewRow();

        //    cboServicePlan.DataSource = tb;
        //    cboServicePlan.DisplayMember = "service_plan";
        //    cboServicePlan.ValueMember = "id";
        //}
        //private void LoadThemes()
        //{
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, @"SELECT id, theme_short FROM theme;");

        //    DataRow dr = tb.NewRow();

        //    dr["id"] = "-0-";
        //    dr["theme_short"] = "=== NONE ===";
        //    tb.Rows.InsertAt(dr, 0);

        //    cboTheme.DataSource = tb;
        //    cboTheme.DisplayMember = "theme_short";
        //    cboTheme.ValueMember = "id";
        //}
        //private void LoadStrategyObjective()
        //{
        //    DataRowView row = (DataRowView)cboTheme.SelectedItem;

        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, @"SELECT * FROM strategy_objective WHERE theme_id='" + row["id"] + "' order by rank;");
        //    cboStrategyObj.DataSource = tb;
        //    cboStrategyObj.DisplayMember = "strategy_objective";
        //    cboStrategyObj.ValueMember = "id";
        //}
        //private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadManagers();
        //    //LoadTableFromDatabase();
        //}
        //private void cboTheme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadStrategyObjective();
        //}
        //private void cboManager_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataRowView row = (DataRowView)cboManager.SelectedItem;
        //    if (mIsNew) { LoadTableFromDatabase(); }
        //}
        #endregion

        private void dgv01_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(this, e);
        }

        #region -- Form Buttons --
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            mIsNew = false;
            //LoadDataFromGrid();
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            mIsNew = true;
            ClearData();
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorks(OPGlobals.currentYear);
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            //bool saveresult = false;
            ////======== NEW CPW===========
            //CapitalWork cpw = new CapitalWork((string.IsNullOrEmpty(txtcpwid.Text) ? 0 : int.Parse(txtcpwid.Text)));

            //cpw.CapitalWorkJobCostNumber = txtJobCostNumber.Text;

            //DataRowView row;
            //row = (DataRowView)cboManager.SelectedItem;
            //if (row != null) { cpw.ManagerID = row["manager_id"].ToString(); }

            //cpw.CapitalWorkYear = txtFinYear.Text;
            //cpw.CapitalWorkMonth = OPGlobals.currentMonth;

            //row = (DataRowView)cboServicePlan.SelectedItem;
            //if (row != null) { cpw.ServicePlanID = row["id"].ToString(); }

            //row = (DataRowView)cboStrategyObj.SelectedItem;
            //if (row != null) { cpw.StrategyObjectiveID = row["id"].ToString(); }

            //cpw.Description = txtCPWDescription.Text;

            //double number;
            //if (Double.TryParse(txtCPWBudget.Text, out number)) { cpw.OriginalBudget = number; }
            //if (Double.TryParse(txtRevisedBudget.Text, out number)) { cpw.RevisedBudget = number; }
            //if (Double.TryParse(txtCarryOver.Text, out number)) { cpw.CarryOverBudget = number; }

            //DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //if (conn.State == ConnectionState.Closed) { conn.Open(); }
            //using (DbTransaction trans = conn.BeginTransaction())
            //{
            //    try
            //    {
            //        if (cpw.CapitalWorkID == 0)
            //        {
            //            saveresult = cpw.InsertCWP(db, conn, trans);
            //            if (saveresult) { cpw.InsertCWPQBR(db, conn, trans); }
            //        }
            //        else
            //        {
            //            saveresult = cpw.UpdateCWP(db, conn, trans);
            //            if (saveresult) { cpw.UpdateCWPQBR(db, conn, trans); }
            //        }

            //        trans.Commit();
            //        MessageBox.Show("Capital Work Project has been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        trans.Rollback();
            //        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //conn.Close();
            //if (saveresult == true)
            //{
            //    UpdateDataGrid(cpw.CapitalWorkID);
            //}
        }
        #endregion

    }
}
