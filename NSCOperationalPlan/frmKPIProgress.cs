
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
    public partial class frmKPIProgress : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        List<DataGridView> dgv;

        public frmKPIProgress()
        {
            InitializeComponent();
            dgv = new List<DataGridView>() { dgv01, dgv02, dgv03 };
        }

        private void ArrangeGrids()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 50);               //0                
            dct.Add("kpi_id", 0);               //1
            dct.Add("Description", 550);    //2
            dct.Add("Prefix", 50);         //3
            dct.Add("Est.", 60);           //4
            dct.Add("Unit", 50);            //5
            dct.Add("ManagerID", 0);        //6
            dct.Add("Progress", 80);        //7
            dct.Add("Remarks", 600);        //8
            dct.Add("kpm_id", 0);              //9
            dct.Add("BProgress", 0);        //7
            dct.Add("BRemarks", 0);        //8

            int[] hiddenRows = { 1, 6, 9, 10, 11 };
            //int[] hiddenRows = { };
            int[] readonlyrows = { 0, 1, 2, 3, 4, 5, 6, 9, 10, 11 };


            for(int x=0; x<3; x++)
            {
                MyGridUtils.ArrangeDataGrid(dgv[x], dct, hiddenRows);
                dgv[x].RowTemplate.MinimumHeight = 28;
                dgv[x].DefaultCellStyle.BackColor = Color.Beige;

                foreach (int i in readonlyrows)
                {
                    dgv[x].Columns[i].ReadOnly = true;
                    dgv[x].Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv[x].Columns[i].DefaultCellStyle.ForeColor = Color.Black;
                }

                dgv[x].Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv[x].Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv[x].Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv[x].Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgv[x].SelectionMode = DataGridViewSelectionMode.CellSelect;

            }

        }
        private void LoadKPIFromDatabase()
        {
            LoadFromDatabase(dgv01, "001");
            LoadFromDatabase(dgv02, "002");
            LoadFromDatabase(dgv03, "003");
        }

        private void LoadFromDatabase(DataGridView dgv, string kpitype)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //DataTable tb;
            string strsql;

            if (opt2.Checked)
            {
                strsql = KeyPerformanceIndex.GetQueryKPIwithProgress(OPGlobals.currentYear, OPGlobals.currentMonth, kpitype);
                //tb = KeyPerformanceIndex.GetKPIwithProgressTable(db, conn, OPGlobals.currentYear, OPGlobals.currentMonth, kpitype);
            }
            else if (opt1.Checked)
            {
                strsql = KeyPerformanceIndex.GetQueryKPIwithProgress(OPGlobals.currentYear, OPGlobals.currentMonth, kpitype, OPGlobals.CurrentUser.DirectorID);
                //tb = KeyPerformanceIndex.GetKPIwithProgressTable(db, conn, OPGlobals.CurrentUser.DirectorID, OPGlobals.currentYear, OPGlobals.currentMonth, kpitype);
            }
            else
            {
                strsql = KeyPerformanceIndex.GetQueryKPIwithProgress(OPGlobals.currentYear, OPGlobals.currentMonth, kpitype, OPGlobals.CurrentUser.DirectorID, OPGlobals.CurrentUser.ManagerID);
                //tb = KeyPerformanceIndex.GetKPIwithProgressTable(db, conn, OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear, OPGlobals.currentMonth, kpitype);
            }
            //strsql += " AND A.kpm_id = '" + kpitype + "'";

            DataTable tb = db.GetDataTable(conn, strsql);
            dgv.Rows.Clear();
            dgv.Refresh();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv.Rows.Add(new String[] {(dgv.RowCount+1).ToString(),
                        row["kpi_id"].ToString(),
                        row["efficiency_description"].ToString(),
                        row["kpi_prefix_short"].ToString(),
                        row["kpi_estimate"].ToString(),
                        row["kpi_unit_short"].ToString(),
                        row["manager_id"].ToString(),
                        row["kpi_progress"].ToString(),
                        row["kpi_remark"].ToString(),
                        row["kpm_id"].ToString(),
                        row["kpi_progress"].ToString(),
                        row["kpi_remark"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dgv.CurrentCell = null;
        }

        private void PopulateDataFromPreviousMonth()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql;
            int noofrecs = 0;
            //double revised;
            try
            {
                for (int x = 0; x < 3; x++)
                {
                    for (int i = 0; i < dgv[x].RowCount; i++)
                    {
                        strsql = "Select view_kpi_progress.kpi_progress, view_kpi_progress.kpi_remark From view_kpi_progress Where"
                            + " view_kpi_progress.kpi_year = '" + OPGlobals.prevoiusYear + "' AND"
                            + " view_kpi_progress.kpi_month = " + OPGlobals.previousMonth + " AND"
                            + " view_kpi_progress.kpm_id = '" + dgv[x].Rows[i].Cells["kpm_id"].Value.ToString() + "' AND"
                            + " view_kpi_progress.kpi_id = '" + dgv[x].Rows[i].Cells["kpi_id"].Value.ToString() + "';";
                        DataTable tb = db.GetDataTable(conn, strsql);
                        if (tb.Rows.Count > 0)
                        {
                            noofrecs++;
                            dgv[x].Rows[i].Cells["Progress"].Value = tb.Rows[0]["kpi_progress"];
                            dgv[x].Rows[i].Cells["Remarks"].Value = tb.Rows[0]["kpi_remark"];
                        }
                    }

                }
                if(noofrecs==0){
                    MessageBox.Show("No data available for Previous Month" , "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < dgv.Count; x++)
            {
                dgv[x].EndEdit();
                if (IsRowDataChanged(dgv[x]))
                {
                    if (MessageBox.Show("Data has been changed, Do you want to Save Before exit?", "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        SaveKPMMonthlyProgress();
                        break;
                    }
                }
            }

            this.Dispose();
        }

        private void frmKPIProgress_Load(object sender, EventArgs e)
        {
            ArrangeGrids();
            //LoadKPIFromDatabase();
            ArrangeScreen();
            opt0.Checked = true;
        }

        private void ArrangeScreen()
        {

            //this.Width = Screen.FromControl(this).Bounds.Width;
            this.Height = Screen.FromControl(this).Bounds.Height - 200;

            opt1.Text = "Show all Actions in " + OPGlobals.CurrentUser.Department;

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

        private void dgv01_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv01.CurrentCell.ColumnIndex < 7) { dgv01.CurrentCell = dgv01.Rows[0].Cells[7]; }
            dgv01.BeginEdit(true);
        }
        private void dgv02_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv02.CurrentCell.ColumnIndex < 7) { dgv02.CurrentCell = dgv02.Rows[0].Cells[7]; }
            dgv02.BeginEdit(true);
        }
        private void dgv03_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv03.CurrentCell.ColumnIndex < 7) { dgv03.CurrentCell = dgv03.Rows[0].Cells[7]; }
            dgv03.BeginEdit(true);
        }

        private bool IsRowDataChanged(DataGridView dgv)
        {
            bool result = false;
            if (MyGridUtils.IsColumnDataChanged(dgv, new List<string>() { "Progress", "Remarks" }, new List<string>() { "BProgress", "BRemarks" })) { result = true; }
            return result;
        }
        private bool IsRowDataChanged(DataGridViewRow dgvRow)
        {
            bool result = false;
            if (MyGridUtils.IsColumnDataChanged(dgvRow, new List<string>() { "Progress", "Remarks" }, new List<string>() { "BProgress", "BRemarks" })) { result = true; }
            return result;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveKPMMonthlyProgress();
        }

        private void SaveKPMMonthlyProgress()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    KeyPerformanceIndex kpi;
                    for (int x = 0; x < dgv.Count; x++)
                    {
                        dgv[x].EndEdit();

                        for (int i = 0; i < dgv[x].RowCount; i++)
                        {
                            if (!IsRowDataChanged(dgv[x].Rows[i])) { continue; }

                            int id = int.Parse(dgv[x].Rows[i].Cells["kpi_id"].Value.ToString());

                            kpi = new KeyPerformanceIndex();
                            kpi.KPIID = id;
                            kpi.CurrentYear = OPGlobals.currentYear;
                            kpi.KPIMonth = OPGlobals.currentMonth;

                            try
                            { kpi.CurrentValue = Double.Parse(dgv[x].Rows[i].Cells["Progress"].Value.ToString());
                            }
                            catch (Exception eex) { kpi.CurrentValue = 0; }

                            try
                            { kpi.Remark = dgv[x].Rows[i].Cells["Remarks"].Value.ToString();
                            }
                            catch (NullReferenceException ex) { kpi.Remark = ""; }

                            kpi.InsertUpdateMonthlyProgress(db, conn, trans);
                        }
                    }
                    trans.Commit();
                    RefreshGridAfterSaveData();
                    MessageBox.Show("KPM Progress has been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            conn.Close();
        }

        private void RefreshGridAfterSaveData()
        {
            for (int x = 0; x < dgv.Count; x++)
            {
                for (int i = 0; i < dgv[x].RowCount; i++)
                {
                    if (!MyGridUtils.IsColumnDataChanged(dgv[x].Rows[i], new List<string>() { "Progress", "Remarks" }, new List<string>() { "BProgress", "BRemarks" })) { continue; }
                    dgv[x].Rows[i].Cells["BProgress"].Value = dgv[x].Rows[i].Cells["Progress"].Value;
                    dgv[x].Rows[i].Cells["BRemarks"].Value = dgv[x].Rows[i].Cells["Remarks"].Value;
                }
            }

        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintKPIProgress(OPGlobals.currentYear, OPGlobals.currentMonth, OPGlobals.CurrentUser.ManagerID);
        }

        private void tsbPreviousMonth_Click(object sender, EventArgs e)
        {
            PopulateDataFromPreviousMonth();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            for (int x = 0; x < dgv.Count; x++)
            {
                for (int i = 0; i < dgv[x].RowCount; i++)
                {
                    dgv[x].Rows[i].Cells["Progress"].Value ="";
                    dgv[x].Rows[i].Cells["Remarks"].Value = "";
                }
            }
        }

        private void OptionCheckedChanged(object sender, EventArgs e)
        {
            string msg = "If you have changed any of the row values, this action will clear all, Do you want to Continue?";
            if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                LoadKPIFromDatabase();
            }
        }
        private void opt0_CheckedChanged(object sender, EventArgs e)
        {
            if (opt0.Checked) { LoadKPIFromDatabase(); }
        }
        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked) { OptionCheckedChanged(this, e); }
        }
        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked) { OptionCheckedChanged(this, e); }
        }

    }
}
