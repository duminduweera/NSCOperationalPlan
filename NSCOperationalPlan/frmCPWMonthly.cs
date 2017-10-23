using MyDLLs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCOperationalPlan
{
    public partial class frmCPWMonthly : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        private Dictionary<CapitalWork, bool> cpwitems = new Dictionary<CapitalWork, bool>();
       

        public frmCPWMonthly()
        {
            InitializeComponent();
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            dgv01.EndEdit();
            if (IsRowDataChanged(dgv01))
            {
                if (MessageBox.Show("Data has been changed, Do you want to Save Before exit?", "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SaveCapitalWorkMonthlyProgress();
                }
            }
            this.Dispose();
        }
        private void frmCPWMonthly_Load(object sender, EventArgs e)
        {
            ArrangeScreen();
            ArrangeGrid();

            opt0.Checked = true;
            
        }

        /// <summary>
        /// Arrange Screen with User Permision
        /// </summary>
        private void ArrangeScreen()
        {

            this.Width = Screen.FromControl(this).Bounds.Width;
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
        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 30);                   //0
            dct.Add("cpwID", 0);                //1
            dct.Add("Service", 150);            //2
            dct.Add("Description", 400);        //3
            dct.Add("Revised-Council", 120);            //4
            dct.Add("YTD", 120);                //5
            dct.Add("Projected", 120);            //6
            dct.Add("% Comp.", 120);            //7
            dct.Add("Comment", 300);            //8

            dct.Add("BProjected", 100);            //9
            dct.Add("B% Comp.", 100);              //10
            dct.Add("BComment", 100);              //11

            dct.Add("NEFlag", 50);               //12      //0-NEW 1-EDIT

            int[] hiddenRows = { 1, 9, 10, 11, 12 };
            //int[] hiddenRows = {  };
            int[] readonlyrows = { 0, 1, 2, 3, 4, 5, 9, 10, 11, 12};

            //int[] hiddenRows = { 1 };

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows);
            dgv01.RowTemplate.MinimumHeight = 28;
            dgv01.DefaultCellStyle.BackColor = Color.Beige;

            foreach (int i in readonlyrows)
            {
                dgv01.Columns[i].ReadOnly = true;
                this.dgv01.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                this.dgv01.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }

            this.dgv01.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv01.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        private void LoadTableFromDatabase()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            dgv01.Rows.Clear();
            cpwitems.Clear();                //Clear Capital Work Object Array
            dgv01.Refresh();
            CapitalWork cp;
            bool flagEdit;

            //number1 = Double.Parse(dgv01.Rows[i].Cells[6].Value.ToString(), NumberStyles.Currency);
            try
            {
                string strsql;
                strsql = CapitalWork.GetSQLCapitalWorksMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
                //strsql += " WHERE A.cpw_quarter = " + OPGlobals.GetQuarter(OPGlobals.currentMonth);

                if (opt0.Checked)
                {
                    strsql += " WHERE cpw_manager_id ='" + OPGlobals.CurrentUser.ManagerID + "'";
                }
                else if (opt1.Checked)
                {
                    strsql += " WHERE director_id ='" + OPGlobals.CurrentUser.DirectorID + "'";
                }

                strsql += " Order by A.director_id, A.cpw_manager_id, A.cpw_id;";

                double projected = 0;
                double revised = 0;
                

                DataTable tb = db.GetDataTable(conn, strsql);
                foreach (DataRow row in tb.Rows)
                {
                    //if (string.IsNullOrEmpty(row["cpw_revised_budget"].ToString()))
                    //{
                        //throw new CustomException("001");
                    //}
                    revised = string.IsNullOrEmpty(row["cpw_revised_budget"].ToString()) ? 0 : Double.Parse(row["cpw_revised_budget"].ToString(), NumberStyles.Currency);
                    projected = string.IsNullOrEmpty(row["cpw_projected"].ToString()) ? 0 : Double.Parse(row["cpw_projected"].ToString(), NumberStyles.Currency);

                    //projected = revised;

                    if (revised == 0)
                    {
                        revised = Double.Parse(row["cpw_original_budget"].ToString(), NumberStyles.Currency);
                    }

                    dgv01.Rows.Add(new String[] {(dgv01.RowCount+1).ToString(),
                        row["cpw_id"].ToString(),
                        row["service_plan"].ToString(),
                        row["cpw_description"].ToString(),
                        string.Format("{0:$0,0.00}", revised),     //row["cpw_revised_budget"]),
                        string.Format("{0:$0,0.00}", string.IsNullOrEmpty(row["cpw_ytod"].ToString()) ? 0 : Double.Parse(row["cpw_ytod"].ToString(), NumberStyles.Currency)),
                        string.Format("{0:$0,0.00}", (projected==0) ? revised : projected),
                        string.IsNullOrEmpty(row["cpw_percentage"].ToString()) ? "" : row["cpw_percentage"].ToString(),
                        row["cpw_remark"].ToString(),
                        string.Format("{0:$0,0.00}", projected),
                        string.IsNullOrEmpty(row["cpw_percentage"].ToString()) ? "" : row["cpw_percentage"].ToString(),
                        row["cpw_remark"].ToString(),
                        string.IsNullOrEmpty(row["cpw_projected"].ToString()) ? "0" : "1"
                    });
                }
                dgv01.CurrentCell = null;
            }
            catch (CustomException ex)
            {
                MessageBox.Show("ERROR IN CPW" + Environment.NewLine + "Finance has not updated YTD details", "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tsbClose_Click(this, new EventArgs());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadTableFromDatabasePreviousMonth()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql;
            //double revised;
            for (int i=0; i < dgv01.RowCount; i++)
            {
                strsql = "Select capital_works_monthly_progress.* From capital_works_monthly_progress Where"
                    + " capital_works_monthly_progress.capital_works_year = '" + OPGlobals.prevoiusYear + "' And"
                    + " capital_works_monthly_progress.capital_works_month = " + OPGlobals.previousMonth + " And"
                    + " capital_works_monthly_progress.capital_works_id = " + dgv01.Rows[i].Cells["cpwID"].Value;
                DataTable tb = db.GetDataTable(conn, strsql);
                if (tb.Rows.Count > 0)
                {
                    //revised = Double.Parse(tb.Rows[0]["capital_works_projected"].ToString(), NumberStyles.Currency);

                    //dgv01.Rows[i].Cells["Revised"].Value = revised;
                    dgv01.Rows[i].Cells["% Comp."].Value = tb.Rows[0]["capital_works_percentage"];
                    dgv01.Rows[i].Cells["Comment"].Value = tb.Rows[0]["capital_works_remark"];
                }
            }
        }

        private void dgv01_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                try
                {
                    double revised = Double.Parse(this.dgv01.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), NumberStyles.Currency);
                    this.dgv01.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = revised;
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void dgv01_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                try
                {
                    this.dgv01.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Format("{0:$0,0.00}", this.dgv01.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private bool IsRowDataChanged(DataGridViewRow dgvRow)
        {
            bool retVal = false;
            if (MyGridUtils.IsColumnDataChanged(dgvRow, new List<string>() { "Projected", "% Comp.", "Comment" }, new List<string>() { "BProjected", "B% Comp.", "BComment" })) { retVal = true; }
            return retVal;
        }
        private bool IsRowDataChanged(DataGridView dgv)
        {
            bool retVal = false;
            if (MyGridUtils.IsColumnDataChanged(dgv, new List<string>() { "Projected", "% Comp.", "Comment" }, new List<string>() { "BProjected", "B% Comp.", "BComment" })) { retVal = true; }
            return retVal;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveCapitalWorkMonthlyProgress();
        }
        private bool SaveCapitalWorkMonthlyProgress()
        {
            bool mRetVal = false;
            CapitalWork c;
            double projected, percentage_completed;
            string comments, msg;
            DataTable tb;

            dgv01.EndEdit();

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < dgv01.Rows.Count; i++)
                    {
                        if (!IsRowDataChanged(dgv01.Rows[i])) { continue; }
                        try
                        { projected = Double.Parse(dgv01.Rows[i].Cells["Projected"].Value.ToString(), NumberStyles.Currency); }
                        catch (Exception eex) { projected = 0; }

                        try
                        { percentage_completed = Double.Parse(dgv01.Rows[i].Cells["% Comp."].Value.ToString(), NumberStyles.Currency); }
                        catch (Exception eex) { percentage_completed = 0; }
                        if (percentage_completed > 100 || percentage_completed < 0) { percentage_completed = 0; }

                        try { comments = dgv01.Rows[i].Cells["Comment"].Value.ToString(); } catch (Exception eex) { comments = ""; }

                        c = new CapitalWork(int.Parse(dgv01.Rows[i].Cells["cpwID"].Value.ToString()));
                        c.CapitalWorkMonth = OPGlobals.currentMonth;
                        c.CapitalWorkYear = OPGlobals.currentYear;
                        c.ProjectedBudget = projected;
                        c.PercentageCompled = percentage_completed;
                        c.MonthlyComment = comments;

                        c.InsertUpdateMonthlyProgress(db, conn, trans);
                    }
                    trans.Commit();
                    mRetVal = true;
                    RefreshGridAfterSaveData();
                    msg = "Capital Work Monthly Progress has been saved/updated successfully";
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
        private void RefreshGridAfterSaveData()
        {
            for (int i = 0; i <= dgv01.Rows.Count - 1; i++)
            {
                if (!IsRowDataChanged(dgv01.Rows[i])) { continue; }
                dgv01.Rows[i].Cells["BProjected"].Value = dgv01.Rows[i].Cells["Projected"].Value;
                dgv01.Rows[i].Cells["B% Comp."].Value = dgv01.Rows[i].Cells["% Comp."].Value;
                dgv01.Rows[i].Cells["BComment"].Value = dgv01.Rows[i].Cells["Comment"].Value;
            }

        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorksMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
        }

        private void dgv01_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            string msg = "If you have changed any of the row values, this action will clear all, Do you want to Continue?";
            if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information)== DialogResult.Yes)
            {
                //ChangeSaveButtonStatus();
                LoadTableFromDatabase();
            }
        }
        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            string msg = "If you have changed any of the row values, this action will clear all, Do you want to Continue?";
            if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //ChangeSaveButtonStatus();
                LoadTableFromDatabase();
            }
        }

        private void ChangeSaveButtonStatus()
        {
            //if(chk1.Checked || chk2.Checked) { tsbSave.Enabled = false; } else { tsbSave.Enabled = true; }
        }

        private void tsbPrevious_Click(object sender, EventArgs e)
        {
            LoadTableFromDatabasePreviousMonth();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        private void ClearData()
        {
            for (int i = 0; i < dgv01.RowCount; i++)
            {
                dgv01.Rows[i].Cells["% Comp."].Value = "";
                dgv01.Rows[i].Cells["Comment"].Value = "";
            }

        }

        private void OptionCheckedChanged(object sender, EventArgs e)
        {
            string msg = "If you have changed any of the row values, this action will clear all, Do you want to Continue?";
            if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                LoadTableFromDatabase();
            }
        }
        private void opt0_CheckedChanged(object sender, EventArgs e)
        {
            if (opt0.Checked) { LoadTableFromDatabase(); }
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
