using MyDLLs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCOperationalPlan
{
    public partial class FrmReAssignManager : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        private static bool iniFlag = false;
        static string SELECTED_TAB;
        List<DataGridView> dgvList;

        public FrmReAssignManager()
        {
            InitializeComponent();
        }

        private void FrmReAssignManager_Load(object sender, EventArgs e)
        {
            dgvList = new List<DataGridView>() { dgv02_1, dgv02_2, dgv02_3 };

            ArrangeActionGrid();
            ArrangeKPMGrid();
            ArrangeCWPGrid();

            LoadDirectors();
            iniFlag = true;
        }

        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");

            cboDirectorFrom.DataSource = tb;
            cboDirectorFrom.DisplayMember = "director_description";
            cboDirectorFrom.ValueMember = "director_id";

            DataTable tb2 = tb.Copy();
            cboDirectorTo.DataSource = tb2;
            cboDirectorTo.DisplayMember = "director_description";
            cboDirectorTo.ValueMember = "director_id";
        }
        private void LoadManagers(string directorcode, ComboBox cmbManager)
        {
            //if(cmbManager.SelectedItem==null) { return; }
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb;
            string strsql;
            DataRowView row = (DataRowView)cboManagerFrom.SelectedItem;

            if (cmbManager.Name == "cboManagerTo" && row != null)
            {
                strsql = "SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "' AND manager_id <> '" + row["manager_id"].ToString() + "';";
                tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "' AND manager_id <> '" + row["manager_id"].ToString() + "';");
            }
            else
            {
                strsql = "SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "';";
                tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "';");
            }

            cmbManager.DataSource = tb;
            cmbManager.DisplayMember = "manager_description";
            cmbManager.ValueMember = "manager_id";

        }

        private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            DataRowView row = (DataRowView)cmb.SelectedItem;
            try
            {
                if (cmb.SelectedValue.GetType().Name == "String" || iniFlag == false)
                {
                    if (cmb.Name.ToString() == "cboDirectorFrom")
                    {
                        LoadManagers(row["director_id"].ToString(), cboManagerFrom);
                    }
                    else
                    {
                        LoadManagers(row["director_id"].ToString(), cboManagerTo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboDirectorFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDirector_SelectedIndexChanged(sender, e);
        }
        private void cboDirectorTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDirector_SelectedIndexChanged(sender, e);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboManagerFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)cboDirectorFrom.SelectedItem;
            LoadManagers(row["director_id"].ToString(), cboManagerTo);
            FillActionGrid();
            FillKPMGrid();
            FillCWPGrid();
        }

        #region --DATA GRID ARRANGE--
        private void ArrangeActionGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 100);                         //0
            dct.Add("Action", 800);                     //1
            //---------INSERT CHECKBOX COULMN--         //
            //dct.Add("Check", 100);                      //2

            int[] hiddenRows = { };
            int[] readonlyrows = { 0, 1};

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows);
            dgv01.RowTemplate.MinimumHeight = 28;
            dgv01.DefaultCellStyle.BackColor = Color.Beige;

            foreach (int i in readonlyrows)
            {
                dgv01.Columns[i].ReadOnly = true;
                dgv01.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgv01.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }

            //----ADD Check box column---
            AddCheckBoxColumn(dgv01, "dgv01X");

            dgv01.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        private void ArrangeKPMGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 100);               //1
            dct.Add("Description", 800);    //2

            //---------INSERT CHECKBOX COULMN--         //

            int[] hiddenRows = { };
            int[] readonlyrows = { 0, 1 };


            for (int i = 0; i < dgvList.Count; i++)
            {
                MyGridUtils.ArrangeDataGrid(dgvList[i], dct, hiddenRows);
                dgvList[i].RowTemplate.MinimumHeight = 28;
                dgvList[i].DefaultCellStyle.BackColor = Color.Beige;

                foreach (int j in readonlyrows)
                {
                    dgvList[i].Columns[j].ReadOnly = true;
                    dgvList[i].Columns[j].DefaultCellStyle.BackColor = Color.LightGray;
                    dgvList[i].Columns[j].DefaultCellStyle.ForeColor = Color.Black;
                }

                //----ADD Check box column---
                AddCheckBoxColumn(dgvList[i], dgvList[i].Name.ToString() + "X");

                dgvList[i].SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
        }
        private void ArrangeCWPGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 100);                         //0
            dct.Add("Description", 800);                     //1
            //---------INSERT CHECKBOX COULMN--         //
            //dct.Add("Check", 100);                      //2

            int[] hiddenRows = { };
            int[] readonlyrows = { 0, 1 };

            MyGridUtils.ArrangeDataGrid(dgv03, dct, hiddenRows);
            dgv03.RowTemplate.MinimumHeight = 28;
            dgv03.DefaultCellStyle.BackColor = Color.Beige;

            foreach (int i in readonlyrows)
            {
                dgv03.Columns[i].ReadOnly = true;
                dgv03.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgv03.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }

            //----ADD Check box column---
            AddCheckBoxColumn(dgv03, "dgv03X");

            dgv03.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        private void AddCheckBoxColumn(DataGridView dgv, string colName)
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = colName;
            checkColumn.HeaderText = "X";
            checkColumn.Width = 50;
            //checkColumn.ReadOnly = false;
            checkColumn.TrueValue = true;
            checkColumn.FalseValue = false;
            checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            //checkColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns.Add(checkColumn);
        }
        #endregion

        #region -- LOAD DATA FROM DATABASE --
        private void FillActionGrid()
        {
            DataRowView dr = (DataRowView)cboManagerFrom.SelectedItem;
            if(string.IsNullOrEmpty(dr["director_id"].ToString()) || string.IsNullOrEmpty(dr["manager_id"].ToString())) { return; }
            string strsql = MonthlyProgress.GetQueryMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth, dr["director_id"].ToString(), dr["manager_id"].ToString());

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv01.Rows.Clear();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv01.Rows.Add(row["action_id"].ToString(),
                            row["action_description"].ToString(),
                            false
                    );
                }
                dgv01.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillKPMGrid()
        {
            LoadKPMFromDatabase(dgv02_1, "001");
            LoadKPMFromDatabase(dgv02_2, "002");
            LoadKPMFromDatabase(dgv02_3, "003");
        }
        private void LoadKPMFromDatabase(DataGridView dgv, string kpitype)
        {
            DataRowView dr = (DataRowView)cboManagerFrom.SelectedItem;
            if (string.IsNullOrEmpty(dr["director_id"].ToString()) || string.IsNullOrEmpty(dr["manager_id"].ToString())) { return; }

            string strsql = KeyPerformanceIndex.GetQueryKPIwithProgress(OPGlobals.currentYear, OPGlobals.currentMonth, kpitype, dr["director_id"].ToString(), dr["manager_id"].ToString());

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);
            dgv.Rows.Clear();
            dgv.Refresh();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv.Rows.Add(
                        row["kpi_id"].ToString(),
                        row["efficiency_description"].ToString(),
                        false
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillCWPGrid()
        {
            DataRowView dr = (DataRowView)cboManagerFrom.SelectedItem;
            if (string.IsNullOrEmpty(dr["director_id"].ToString()) || string.IsNullOrEmpty(dr["manager_id"].ToString())) { return; }

            string strsql = CapitalWork.GetSQLCapitalWorksMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
            strsql += " WHERE director_id ='" + dr["director_id"].ToString() + "' AND cpw_manager_id ='" + dr["manager_id"].ToString() + "'";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv03.Rows.Clear();
            dgv03.Refresh();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv03.Rows.Add(new String[] {
                            row["cpw_id"].ToString(),
                            row["cpw_description"].ToString()
                    });

                }
                dgv03.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region -- Save Data back to Database --
        private void ChangeDelegation()
        {
            DataRowView row1 = (DataRowView)cboManagerFrom.SelectedItem;
            DataRowView row2 = (DataRowView)cboManagerTo.SelectedItem;

            string msg = "";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    #region  ------------- ACTIONS -----------
                    dgv01.ClearSelection();
                    for (int i = 0; i < dgv01.RowCount; i++)
                    {
                        bool chk = Convert.ToBoolean(dgv01.Rows[i].Cells["dgv01X"].Value);

                        if (chk == true) {
                            clsAction action = new clsAction();
                            action.ActionID = dgv01.Rows[i].Cells["ID"].Value.ToString();
                            action.ManagerID = row1["manager_id"].ToString();
                            action.ChangeManagerAction(db, conn, trans, row2["manager_id"].ToString());
                        }
                    }
                    #endregion
                    #region ------------ KPM ---------------
                    for (int i = 0; i < dgvList.Count; i++)
                    {
                        dgvList[i].ClearSelection();
                        string dgvname = dgvList[i].Name.ToString() + "X";

                        for (int j = 0; j < dgvList[i].RowCount; j++)
                        {
                            bool chk = Convert.ToBoolean(dgvList[i].Rows[j].Cells[dgvname].Value);
                            if (chk == true)
                            {
                                KeyPerformanceIndex kpi = new KeyPerformanceIndex();
                                kpi.KPIID = int.Parse(dgvList[i].Rows[j].Cells["ID"].Value.ToString());
                                kpi.ManagerID = row1["manager_id"].ToString();
                                kpi.ChangeManagerKPI(db, conn, trans, row2["manager_id"].ToString());
                            }
                        }
                    }
                    #endregion
                    #region  ------------- Capital Works -----------
                    dgv03.ClearSelection();
                    for (int i = 0; i < dgv03.RowCount; i++)
                    {
                        bool chk = Convert.ToBoolean(dgv03.Rows[i].Cells["dgv03X"].Value);

                        if (chk == true)
                        {
                            CapitalWork cwp = new CapitalWork();
                            cwp.CapitalWorkID = int.Parse(dgv03.Rows[i].Cells["ID"].Value.ToString());
                            cwp.ManagerID = row1["manager_id"].ToString();
                            cwp.ChangeManagerCWP(db, conn, trans, row2["manager_id"].ToString());
                        }
                    }
                    #endregion


                    trans.Commit();
                    msg = "Responsible Manager has been changed successfully";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    msg = "E R R O R !!! ..." + Environment.NewLine + ex.Message;
                }
            }
            conn.Close();
            MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FillActionGrid();
            FillKPMGrid();
            FillCWPGrid();

        }

        #endregion

        private void tsbSave_Click(object sender, EventArgs e)
        {
            ChangeDelegation();
        }

        #region -- Cell Click on CheckBox ---
        private void dgv01_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_CellContentClick(sender, e);
        }
        private void dgv02_1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_CellContentClick(sender, e);
        }
        private void dgv02_2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_CellContentClick(sender, e);
        }
        private void dgv02_3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_CellContentClick(sender, e);
        }
        private void dgv03_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView_CellContentClick(sender, e);
        }
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            senderGrid.EndEdit();
        }
        #endregion

    }
}
