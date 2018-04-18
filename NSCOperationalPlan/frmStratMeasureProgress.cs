using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using MyDLLs;


namespace NSCOperationalPlan
{
    public partial class frmStratMeasureProgress : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmStratMeasureProgress()
        {
            InitializeComponent();
        }
        private void frmStratMeasureProgress_Load(object sender, EventArgs e)
        {
            ArrangeScreen();
            ArrangeDataGridView();
            AddDatatoGrid();
        }

        private void ArrangeScreen()
        {
            this.Width = Screen.FromControl(this).Bounds.Width;
            this.Height = Screen.FromControl(this).Bounds.Height - 200;

            opt1.Text = "Show all Strategy measures in " + OPGlobals.CurrentUser.Department;

            if (OPGlobals.CurrentUser.Permission == UserRights.Administrator || OPGlobals.CurrentUser.Permission == UserRights.GM || OPGlobals.CurrentUser.Permission == UserRights.Editor)
            {
                opt1.Enabled = true;
                opt2.Enabled = true;
            }
            else if (OPGlobals.CurrentUser.Permission == UserRights.Director)
            {
                opt1.Enabled = true;
                opt2.Enabled = false;

            }
            else
            {
                opt1.Enabled = false;
                opt2.Enabled = false;
            }
        }

        private void ArrangeDataGridView()
        {
            Dictionary<string, int> coloumnDict = new Dictionary<string, int>();

            coloumnDict.Add("Code", 0);                         //0
            coloumnDict.Add("Measure", 300);                    //1
            coloumnDict.Add("Measured Against", 200);           //2
            coloumnDict.Add("Target June-2021", 140);           //3
            coloumnDict.Add("Progress", 240);                   //4
            coloumnDict.Add("Comments", 500);                   //5

            coloumnDict.Add("strategy_id", 0);                  //6

            int[] hiddenRows = { 0, 6 };
            int[] readonlyrows = { 0, 1, 2, 3, 6 };

            MyGridUtils.ArrangeDataGrid(dgv, coloumnDict, hiddenRows);
            dgv.RowTemplate.MinimumHeight = 28;
            dgv.DefaultCellStyle.BackColor = Color.Beige;

            foreach (int i in readonlyrows)
            {
                dgv.Columns[i].ReadOnly = true;
                dgv.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }

            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

        }
        private void AddDatatoGrid()
        {
            string strsql;

            DataRowView dr1 = (DataRowView)cboDirector.SelectedItem;
            DataRowView dr2 = (DataRowView)cboManager.SelectedItem;
            #region --- Select Query---
            if (dr2 != null)
            {
                strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(OPGlobals.currentYear, OPGlobals.currentMonth, dr1["director_id"].ToString(), dr2["manager_id"].ToString());
            }
            else
            {
                if (opt2.Checked)
                {
                    strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
                }
                else if (opt1.Checked)
                {
                    strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(OPGlobals.currentYear, OPGlobals.currentMonth, OPGlobals.CurrentUser.DirectorID);
                }
                else
                {
                    strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(OPGlobals.currentYear, OPGlobals.currentMonth, OPGlobals.CurrentUser.DirectorID, OPGlobals.CurrentUser.ManagerID);
                }
            }
            #endregion
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv.Rows.Clear();
            dgv.Refresh();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv.Rows.Add(new String[] {row["strategy_measure_code"].ToString(),
                            row["strategy_measure_code"].ToString() + " - "+ row["description"].ToString(),
                            row["how_measured"].ToString(),
                            row["target"].ToString(),
                            row["current_result"].ToString(),
                            row["comment"].ToString()
                    });
                    //dgv01.Rows[8].Cells[8].Style.BackColor = Color.Beige;
                }

                MyDLLs.MyGridUtils.ColorDataGrid(dgv, 0, 11);
                MyDLLs.MyGridUtils.ColorDataGrid(dgv, 8, 12);
                dgv.CurrentCell = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            dgv.CurrentCell = null;
        }

        #region --- Select the Manager (ONLY FOR ADMINISTRATOR FUNCTION)--- 
        //private void LoadDirectors()
        //{
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");

        //    //----- Insert a new Row in 0 possition ---
        //    DataRow dr = tb.NewRow();
        //    dr["director_id"] = "-0-";
        //    dr["director_description"] = "-NONE-";
        //    tb.Rows.InsertAt(dr, 0);

        //    cboDirector.DataSource = tb;
        //    cboDirector.DisplayMember = "director_description";
        //    cboDirector.ValueMember = "director_id";
        //}
        //private void LoadManagers()
        //{
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

        //    DataRowView row = (DataRowView)cboDirector.SelectedItem;
        //    string strsql = "SELECT * FROM view_directors_plus_managers WHERE director_id = '" + row["director_id"].ToString() + "';";
        //    DataTable tb = db.GetDataTable(conn, @strsql);

        //    cboManager.DataSource = tb;
        //    cboManager.DisplayMember = "manager_description";
        //    cboManager.ValueMember = "manager_id";
        //}
        //private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadManagers();
        //}
        //private void cboManager_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillGrid();
        //}
        #endregion

        #region --- Toolbar Buttons ---
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

    }
}
