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
        int mStartegyMEasureMonth = OPGlobals.GetStrategyMeasureMonth();
        int[] readonlyrows = { 0, 1, 2, 3, 6, 7 };

        public frmStratMeasureProgress()
        {
            InitializeComponent();
        }
        private void frmStratMeasureProgress_Load(object sender, EventArgs e)
        {
            //--- Update Information Box ---
            lblMonth.Text = "Strategy Measure Progress to date upto "
                + Enum.GetName(typeof(Months), mStartegyMEasureMonth) + " 20" + OPGlobals.currentYear.Substring(3, 2);
            //=============================
            ArrangeScreen();
            ArrangeDataGridView();
            LoadDirectors();
        }

        private void ArrangeScreen()
        {
            this.Width = Screen.FromControl(this).Bounds.Width;
            this.Height = Screen.FromControl(this).Bounds.Height - 200;

            opt0.Checked = true;
            opt1.Text = "Show all Strategy measures in " + OPGlobals.CurrentUser.Department;
            groupBox1.Enabled = false;

            if (OPGlobals.CurrentUser.Permission == UserRights.Administrator || OPGlobals.CurrentUser.Permission == UserRights.GM || OPGlobals.CurrentUser.Permission == UserRights.Editor)
            {
                groupBox1.Enabled = true;
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
            coloumnDict.Add("Measure", 500);                    //1
            coloumnDict.Add("Measured Against", 250);           //2
            coloumnDict.Add("Target June-2021", 120);           //3
            coloumnDict.Add("Progress", 100);                   //4
            coloumnDict.Add("Comments", 500);                   //5

            coloumnDict.Add("strategy_id", 0);                  //6
            coloumnDict.Add("Exist", 100);                      //7

            int[] hiddenRows = { 0, 6, 7 };
            //int[] readonlyrows = { 0, 1, 2, 3, 6 };

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
        //private void ColorDataGridDataentry(Color clr)
        //{
        //    foreach (int i in readonlyrows)
        //    {
        //        dgv.Columns[i].ReadOnly = true;
        //        dgv.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
        //        dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
        //    }

        //}
        private void AddDatatoGrid(string cYear, int cMonth)
        {
            string strsql = GetQuery(cYear, cMonth) + " ORDER BY strategy_measure_code";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv.Rows.Clear();
            dgv.Refresh();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv.Rows.Add(row["strategy_measure_code"].ToString(),
                            row["strategy_measure_code"].ToString() + " | "+ row["description"].ToString() + Environment.NewLine,
                            row["how_measured"].ToString(),
                            row["target"].ToString(),
                            row["current_result"].ToString(),
                            row["comment"].ToString(),
                            row["strategy_id"].ToString(),
                            String.IsNullOrEmpty(row["comment"].ToString()) ? "0" : "1"
                    );
                }

                //MyDLLs.MyGridUtils.ColorDataGrid(dgv, 0, 11);
                //MyDLLs.MyGridUtils.ColorDataGrid(dgv, 8, 12);
                dgv.CurrentCell = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dgv.CurrentCell = null;
        }
        private void AddDatatoGridFromPrevious(string cYear, int cMonth)
        {
            string strsql = GetQuery(cYear, cMonth);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            for (int i = 0; i < dgv.RowCount; i++)
            {
                string tempstr = strsql + " AND B.strategy_measure_code = '" + dgv.Rows[i].Cells["Code"].Value.ToString() + "'";

                DataTable tb = db.GetDataTable(conn, tempstr);
                if (tb.Rows.Count > 0 && string.IsNullOrEmpty(dgv.Rows[i].Cells["Progress"].Value.ToString()))
                {
                    dgv.Rows[i].Cells["Progress"].Value = tb.Rows[0]["current_result"];
                    dgv.Rows[i].Cells["Comments"].Value = tb.Rows[0]["comment"];
                }
            }
            dgv.CurrentCell = null;
        }

        private string GetQuery(string cYear, int cMonth)
        {
            string strsql;

            DataRowView dr1 = (DataRowView)cboDirector.SelectedItem;
            DataRowView dr2 = (DataRowView)cboManager.SelectedItem;
            if (dr2 != null)
            {
                strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(cYear, cMonth, dr1["director_id"].ToString(), dr2["manager_id"].ToString());
            }
            else
            {
                if (opt2.Checked)
                {
                    strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(cYear, cMonth);
                }
                else if (opt1.Checked)
                {
                    strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(cYear, cMonth, OPGlobals.CurrentUser.DirectorID);
                }
                else
                {
                    strsql = StrategyMeasures.GetQueryStrategyMeasuresProgress(cYear, cMonth, OPGlobals.CurrentUser.DirectorID, OPGlobals.CurrentUser.ManagerID);
                }
            }
            return strsql;
        }

        #region --- Select the Manager (ONLY FOR ADMINISTRATOR FUNCTION)--- 
        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");

            //----- Insert a new Row in 0 possition ---
            DataRow dr = tb.NewRow();
            dr["director_id"] = "-0-";
            dr["director_description"] = "-NONE-";
            tb.Rows.InsertAt(dr, 0);

            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
        }
        private void LoadManagers()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataRowView row = (DataRowView)cboDirector.SelectedItem;
            string strsql = "SELECT * FROM view_directors_plus_managers WHERE director_id = '" + row["director_id"].ToString() + "';";
            DataTable tb = db.GetDataTable(conn, @strsql);

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";
        }
        private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadManagers();
        }
        private void cboManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboManager.SelectedIndex > 0) { AddDatatoGrid(OPGlobals.currentYear, OPGlobals.currentMonth); }
        }
        #endregion

        #region --- Toolbar Buttons ---
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void tsbPreviousMonth_Click(object sender, EventArgs e)
        {
            string msg = "This will fill all empty cells with previous values, Do you want to Continue?";
            if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                int pMonth = OPGlobals.GetStrategyMeasurePrevouosMonth(mStartegyMEasureMonth);
                AddDatatoGridFromPrevious(OPGlobals.GetStrategyMeasurePrevouosYear(OPGlobals.currentYear, pMonth), pMonth);
            }
        }
        #endregion

        private void opt_Click(object sender, EventArgs e)
        {
            cboDirector.SelectedIndex = 0;
            AddDatatoGrid(OPGlobals.currentYear, OPGlobals.currentMonth);
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveStrategyMeasureMonthlyProgress();
        }
        private bool SaveStrategyMeasureMonthlyProgress()
        {
            bool mRetVal = false;

            DataTable tb;
            string msg;
            dgv.EndEdit();

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dgv.Rows[i].Cells["Progress"].Value.ToString()) && string.IsNullOrEmpty(dgv.Rows[i].Cells["Comments"].Value.ToString())) { continue; }

                        StrategyMeasures sm = new StrategyMeasures();
                        sm.MeasureCode = dgv.Rows[i].Cells["Code"].Value.ToString();
                        sm.StrategyID = dgv.Rows[i].Cells["strategy_id"].Value.ToString();
                        sm.Year = OPGlobals.currentYear;
                        sm.Month = OPGlobals.GetStrategyMeasureMonth();

                        double mProgress;
                        Double.TryParse(dgv.Rows[i].Cells["Progress"].Value.ToString(), out mProgress);
                        sm.CurrentProgress = mProgress;

                        sm.Comment = dgv.Rows[i].Cells["Comments"].Value.ToString();

                        if (dgv.Rows[i].Cells["Exist"].Value.ToString() == "0")
                        {
                            //==== NEW ====
                            sm.InsertMonthlyStrategyMeasures(db, conn, trans);
                        } else
                        {
                            // --- EDIT AN EXISTING RECORD ---
                            sm.UpdateMonthlyStrategyMeasures(db, conn, trans);
                        }
                    }
                    trans.Commit();
                    mRetVal = true;
                    //RefreshGridAfterSaveData();
                    msg = "Strategy Measures Monthly Progress has been saved/updated successfully";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    msg = "Data NOT Saved ..." + Environment.NewLine + ex.Message;
                }
            }

            conn.Close();
            MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return mRetVal;
        }

        //private void tsbClear_Click(object sender, EventArgs e)
        //{
        //    for (int x = 0; x < dgv.Count; x++)
        //    {
        //        for (int i = 0; i < dgv[x].RowCount; i++)
        //        {
        //            dgv[x].Rows[i].Cells["Progress"].Value = "";
        //            dgv[x].Rows[i].Cells["Remarks"].Value = "";
        //        }
        //    }
        //}
    }
}
