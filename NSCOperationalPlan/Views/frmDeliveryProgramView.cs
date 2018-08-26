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
    public partial class frmDeliveryProgramView : Form
    {
        private static bool mIsNew = true;
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        DeliveryProgramModel mDeliveryProgram = new DeliveryProgramModel();

        public frmDeliveryProgramView()
        {
            InitializeComponent();
            this.txtID.DataBindings.Add("Text", mDeliveryProgram, "ID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txtFinYear.DataBindings.Add("Text", mDeliveryProgram, "Year", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txtDescription.DataBindings.Add("Text", mDeliveryProgram, "Description", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboSource.DataBindings.Add("Text", mDeliveryProgram, "Source", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboPrefix.DataBindings.Add("SelectedValue", mDeliveryProgram, "Prefix", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboStrategy.DataBindings.Add("SelectedValue", mDeliveryProgram, "StrategyID", true, DataSourceUpdateMode.OnPropertyChanged);
            //this.txtTarget.DataBindings.Add("Text", mDeliveryProgram, "TargetValue", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboUnits.DataBindings.Add("SelectedValue", mDeliveryProgram, "Unit", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboFreequency.DataBindings.Add("Text", mDeliveryProgram, "ReportFrequency", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboHowMeasured.DataBindings.Add("Text", mDeliveryProgram, "HowMeasured", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboDirector.DataBindings.Add("SelectedValue", mDeliveryProgram, "DirectorID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cboManager.DataBindings.Add("SelectedValue", mDeliveryProgram, "ManagerID", true, DataSourceUpdateMode.OnPropertyChanged);
            this.txtTargetValue.DataBindings.Add("Text", mDeliveryProgram, "TargetValue", true, DataSourceUpdateMode.OnPropertyChanged);

            tsbNew_Click(this, new EventArgs());
        }

        private void frmDeliveryProgramView_Load(object sender, EventArgs e)
        {
            this.Text = "Delivery Program";
            txtFinYear.Text = OPGlobals.currentYear;
            ArrangeGrid();
            LoadDirectors();
            LoadSource();
            LoadReportFreequency();
            LoadStrategy();
            LoadHowMeasured();
            LoadPrefix();
            LoadUnits();
            LoadTableFromDatabase();
            tsbNew_Click(this, e);
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 50);                   //0
            dct.Add("Description", 400);        //1
            dct.Add("Source", 100);             //2
            dct.Add("Prefix", 130);             //3
            dct.Add("Target", 100);             //4
            dct.Add("Unit", 130);               //5
            dct.Add("ReportFrequency", 150);    //6
            dct.Add("Department", 200);          //7

            dct.Add("HowMeasured", 0);          //8
            dct.Add("Year", 0);                 //9
            dct.Add("StrategyID", 0);           //10
            dct.Add("ManagerID", 0);            //11
            dct.Add("DirectorID", 0);           //12
            dct.Add("PrefixID", 0);             //13
            dct.Add("UnitID", 0);               //14

            int[] hiddenRows = { 8, 9, 10, 11, 12, 13, 14};

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);

            this.dgv01.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv01.RowTemplate.MinimumHeight = 28;

            dgv01.ReadOnly = true;
        }
        private void LoadTableFromDatabase()
        {
            string strsql = "SELECT * FROM view_delivery_program where period_rank=2;";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            dgv01.Rows.Clear();
            dgv01.Refresh();
            try
            {
                DataTable tb = db.GetDataTable(conn, strsql);
                foreach (DataRow row in tb.Rows)
                {
                    dgv01.Rows.Add(new String[] {
                        row["strategy_measure_code"].ToString(),
                        row["strategy_measure_description"].ToString(),
                        row["strategy_measure_source"].ToString(),
                        row["prefix_short"].ToString(),
                        row["strategy_measure_target"].ToString(),
                        row["unit_full"].ToString(),
                        row["report_frequency"].ToString(),
                        row["manager_description"].ToString(),
                        row["how_measured"].ToString(),
                        row["dp_year"].ToString(),
                        row["strategy_id"].ToString(),
                        row["manager_id"].ToString(),
                        row["director_id"].ToString(),
                        row["prefix_id"].ToString(),
                        row["unit_id"].ToString()
                    });
                }
                dgv01.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LoadDataFromGrid()
        {
            try
            {
                txtID.Text = dgv01.CurrentRow.Cells["ID"].Value.ToString();
                txtFinYear.Text = dgv01.CurrentRow.Cells["Year"].Value.ToString();
                cboSource.SelectedValue = dgv01.CurrentRow.Cells["Source"].Value.ToString();
                cboFreequency.SelectedValue = dgv01.CurrentRow.Cells["ReportFrequency"].Value.ToString();
                cboStrategy.SelectedValue = dgv01.CurrentRow.Cells["StrategyID"].Value.ToString();
                cboHowMeasured.SelectedValue = dgv01.CurrentRow.Cells["HowMeasured"].Value.ToString();
                cboDirector.SelectedValue = dgv01.CurrentRow.Cells["DirectorID"].Value.ToString();
                cboManager.SelectedValue = dgv01.CurrentRow.Cells["ManagerID"].Value.ToString();
                txtDescription.Text = dgv01.CurrentRow.Cells["Description"].Value.ToString();
                cboPrefix.SelectedValue = dgv01.CurrentRow.Cells["PrefixID"].Value.ToString();
                txtTargetValue.Text = dgv01.CurrentRow.Cells["Target"].Value.ToString();
                cboUnits.SelectedValue = dgv01.CurrentRow.Cells["UnitID"].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + Environment.NewLine + ex.Message, "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearData()
        {
            txtID.Text = "";
            txtFinYear.Text = OPGlobals.currentYear;
            txtDescription.Text = "";
            mDeliveryProgram.TargetValue = 0;
            cboSource.SelectedIndex = -1;
            cboPrefix.SelectedIndex = -1;
            cboStrategy.SelectedIndex = -1;
            cboUnits.SelectedIndex = -1;
            cboFreequency.SelectedIndex = -1;
            cboHowMeasured.SelectedIndex = -1;
            cboManager.SelectedIndex = -1;
            cboDirector.SelectedIndex = -1;
            dgv01.CurrentCell = null;
        }

        #region -- Load All Combo Boxes ---

        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");
            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
        }
        private void LoadManagers()
        {
            if((DataRowView)cboDirector.SelectedItem==null) { return; }
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + row["director_id"].ToString() + "';");

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";
            //LoadServicePlan();
        }
        private void LoadSource()
        {
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT source as source FROM strategy_measure ORDER BY source");

            cboSource.DataSource = tb;
            cboSource.DisplayMember = "source";
            cboSource.ValueMember = "source";
        }
        private void LoadReportFreequency()
        {
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT report_frequency as freequency FROM strategy_measure ORDER BY report_frequency");

            cboFreequency.DataSource = tb;
            cboFreequency.DisplayMember = "freequency";
            cboFreequency.ValueMember = "freequency";

        }
        private void LoadStrategy()
        {
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT distinct id, strategy FROM strategy");

            cboStrategy.DataSource = tb;
            cboStrategy.DisplayMember = "strategy";
            cboStrategy.ValueMember = "id";

        }
        private void LoadHowMeasured()
        {
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT how_measured FROM strategy_measure ORDER BY how_measured");

            cboHowMeasured.DataSource = tb;
            cboHowMeasured.DisplayMember = "how_measured";
            cboHowMeasured.ValueMember = "how_measured";

        }
        private void LoadPrefix()
        {
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT kpi_prefix_id, kpi_prefix FROM kpi_prefix");

            cboPrefix.DataSource = tb;
            cboPrefix.DisplayMember = "kpi_prefix";
            cboPrefix.ValueMember = "kpi_prefix_id";

        }
        private void LoadUnits()
        {
            DataRowView row = (DataRowView)cboDirector.SelectedItem;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT kpi_unit_id, kpi_unit FROM kpi_units");

            cboUnits.DataSource = tb;
            cboUnits.DisplayMember = "kpi_unit";
            cboUnits.ValueMember = "kpi_unit_id";

        }

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
            if(dgv01.CurrentRow==null) { return; }
            mIsNew = false;
            txtID.Enabled = false;
            //tsbNew.Enabled = true;
            //tsbEdit.Enabled = false;
            tsbCancel.Enabled = true;
            LoadDataFromGrid();
            dgv01.Enabled = false;
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            mIsNew = true;
            txtID.Enabled = true;
            //tsbNew.Enabled = false;
            //tsbEdit.Enabled = true;
            tsbCancel.Enabled = true;
            dgv01.Enabled = true;
            dgv01.Refresh();
            ClearData();
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorks(OPGlobals.currentYear);
        }
        private void tsbCancel_Click(object sender, EventArgs e)
        {
            tsbNew_Click(this, e);
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
        }
        private void btnFill_Click(object sender, EventArgs e)
        {
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    bool canSave = mDeliveryProgram.DataValidate();
                    if (canSave && mIsNew)
                    {
                        mDeliveryProgram.InsertData(db, conn, trans);
                    }
                    else if (canSave && mIsNew == false)
                    {
                        mDeliveryProgram.UpdateData(db, conn, trans);
                    }
                    trans.Commit();
                    LoadSource();
                    LoadReportFreequency();
                    LoadHowMeasured();
                    MessageBox.Show("Delivery program details have been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Data Error" + Environment.NewLine + ex.Message, "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            conn.Close();
            LoadTableFromDatabase();
            tsbCancel_Click(this, new EventArgs());

        }
        private void tsbSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtID.Text.ToUpper();

            dgv01.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgv01.Rows)
                {
                    if (row.Cells[0].Value.ToString().ToUpper().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        #endregion

        private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadManagers();
        }

        private void txtID_Validating(object sender, CancelEventArgs e)
        {
            tsbSearch_Click(this, e);
        }
    }
}
