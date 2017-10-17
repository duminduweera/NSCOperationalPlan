using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using MyDLLs;

namespace NSCOperationalPlan
{

    public partial class frmMonthlyProgress : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmMonthlyProgress()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            dgv01.EndEdit();
            if (MyGridUtils.IsColumnDataChanged(dgv01, new List<string>() { "Status", "Progress", "Remarks" }, new List<string>() { "BStatus", "BProgress", "BRemarks" }))
            {
                if (MessageBox.Show("Data has been changed, Do you want to Save Before exit?", "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SaveOPMonthlyProgress();
                }
            }
            this.Dispose();
        }

        private void ArrangeDataGridView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 60);                           //0
            dct.Add("Theme", 0);                        //1
            dct.Add("Action_id", 0);                    //2
            dct.Add("Action", 750);                     //3
            dct.Add("Target Date", 100);                //4
            dct.Add("manager_id", 0);                   //5
            dct.Add("Responsible Manager", 200);        //6
            //---------INSERT COMBOBOXCOLOUMN--         //7
            dct.Add("", 10);                            //8
            dct.Add("Progress", 60);                    //9
            dct.Add("Remarks", 500);                    //10

            dct.Add("theme_color", 0);                  //10
            dct.Add("Status_color", 0);                 //11

            dct.Add("NEW_Flag", 0);                     //12

            dct.Add("BStatus", 0);                      //13
            dct.Add("BProgress", 0);                    //14
            dct.Add("BRemarks", 0);                     //15

            int[] hiddenRows = { 1, 2, 5, 10, 11, 12, 13, 14, 15 };
            int[] readonlyrows = { 0, 1, 2, 3, 4, 5, 6, 10, 11, 12, 13, 14, 15};

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows);
            dgv01.RowTemplate.MinimumHeight = 28;
            dgv01.DefaultCellStyle.BackColor = Color.Beige;

            foreach (int i in readonlyrows)
            {
                dgv01.Columns[i].ReadOnly = true;
                dgv01.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgv01.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }


            //dgv01.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv01.SelectionMode = DataGridViewSelectionMode.CellSelect;

        }

        private void frmMonthlyProgress_Load(object sender, EventArgs e)
        {
            ArrangeScreen();

            ArrangeDataGridView();
            ClearData();


            BindDropDownToDataGrid();
            opt0.Checked = true;
            dgv01.CurrentCell = null;
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

        private void FillGrid()
        {

            string strsql;
            if (opt2.Checked)
            {
                strsql = MonthlyProgress.GetQueryMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
            } else if (opt1.Checked)
            {
                strsql = MonthlyProgress.GetQueryMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth, OPGlobals.CurrentUser.DirectorID);
            } else
            {
                strsql = MonthlyProgress.GetQueryMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth, OPGlobals.CurrentUser.DirectorID, OPGlobals.CurrentUser.ManagerID);
            }
            
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv01.Rows.Clear();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv01.Rows.Add(new String[] {(dgv01.RowCount+1).ToString(),
                            "  ",
                            row["action_id"].ToString(),
                            row["action_id"].ToString() + " " + row["action_description"].ToString(),
                            String.Format("{0:dd-MMM-yyyy}",   row["delivery_program_targetdate"]),
                            row["manager_id"].ToString(),
                            row["manager_description"].ToString(),
                            row["status_id"].ToString(),
                            "",
                            row["progress_pecentage"].ToString(),
                            row["progress_description"].ToString(),
                            row["theme_color"].ToString(),
                            row["status_color"].ToString(),
                            String.IsNullOrEmpty(row["status_id"].ToString()) ? "1" : "0",
                            row["status_id"].ToString(),
                            row["progress_pecentage"].ToString(),
                            row["progress_description"].ToString()
                    });
                    //dgv01.Rows[8].Cells[8].Style.BackColor = Color.Beige;
                }

                MyDLLs.MyGridUtils.ColorDataGrid(dgv01, 0, 11);
                MyDLLs.MyGridUtils.ColorDataGrid(dgv01, 8, 12);
                dgv01.CurrentCell = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //================== NEW =============
        private void BindDropDownToDataGrid()
        {
            var cmbStatus = new DataGridViewComboBoxColumn();
            

            string strsql = "Select status.* From status Order By status.id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            cmbStatus.HeaderText = "Select Status";
            cmbStatus.Name = "Status";


            cmbStatus.DataSource = tb;
            cmbStatus.ValueMember = "id";
            cmbStatus.DisplayMember = "status_short";

            dgv01.Columns.Insert(7,cmbStatus);

            cmbStatus.FlatStyle = FlatStyle.Flat;

            cmbStatus.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            
        }
        private void LoadFromPreviousMonth()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql;
            int noofrecs = 0;
            //double revised;
            try
            {
                for (int i = 0; i < dgv01.RowCount; i++)
                {

                    strsql = "Select progress.status_id, progress.progress_pecentage, progress.progress_description, status.status_color From progress Inner Join"
                        + " status On progress.status_id = status.id Where"
                        + " progress.action_id = '" + dgv01.Rows[i].Cells["Action_id"].Value.ToString() + "' And"
                        + " progress.progress_year = '" + OPGlobals.prevoiusYear + "' And"
                        + " progress.progress_month = " + OPGlobals.previousMonth + ";";
                    DataTable tb = db.GetDataTable(conn, strsql);
                    if (tb.Rows.Count > 0)
                    {
                        noofrecs++;
                        (dgv01.Rows[i].Cells["Status"] as DataGridViewComboBoxCell).Value = tb.Rows[0]["status_id"];
                        dgv01.Rows[i].Cells["Progress"].Value = tb.Rows[0]["progress_pecentage"];
                        dgv01.Rows[i].Cells["Remarks"].Value = tb.Rows[0]["progress_description"];
                        dgv01.Rows[i].Cells[8].Style.BackColor = ColorTranslator.FromHtml(tb.Rows[0]["status_color"].ToString());
                        //(dgv01.Rows[i].Cells["Status"] as DataGridViewComboBoxCell).Value = "On Target";
                        //(dgv01.Rows[i].Cells["Status"] as DataGridViewComboBoxCell).Value = 1;
                    }
                }

                if (noofrecs == 0)
                {
                    MessageBox.Show("No data available for Previous Month", "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ClearData()
        {
            for(int i=0; i<dgv01.Rows.Count; i++)
            {
                //int test = 0;
                //(dgv01.Rows[i].Cells["Status"] as DataGridViewComboBoxCell).Value = (dgv01.Rows[i].Cells["Status"] as DataGridViewComboBoxCell).Items[0];
                (dgv01.Rows[i].Cells["Status"] as DataGridViewComboBoxCell).Value = "0";
                dgv01.Rows[i].Cells["Progress"].Value = "";
                dgv01.Rows[i].Cells["Remarks"].Value = "";
                dgv01.Rows[i].Cells[8].Style.BackColor = Color.Beige;

                dgv01.Rows[i].Cells["BStatus"].Value = "";
                dgv01.Rows[i].Cells["BProgress"].Value = "";
                dgv01.Rows[i].Cells["BRemarks"].Value = "";

            }

        }

        private bool IsRowDataChanged(DataGridViewRow dgvRow)
        {
            bool result = false;
            if (MyGridUtils.IsColumnDataChanged(dgvRow, new List<string>() { "Status", "Progress", "Remarks" }, new List<string>() { "BStatus", "BProgress", "BRemarks" })) { result = true; }

            return result;
        }
        private bool IsRowDataChanged(DataGridView dgv)
        {
            bool result = false;
            if (MyGridUtils.IsColumnDataChanged(dgv, new List<string>() { "Status", "Progress", "Remarks" }, new List<string>() { "BStatus", "BProgress", "BRemarks" })) { result = true; }
            return result;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveOPMonthlyProgress();
            
        } 
        private void SaveOPMonthlyProgress()
        {
            DataTable tb;

            Double percentageComplete;
            MonthlyProgress progress;
            String mActionID, msg;
            int mActionStatus;


            dgv01.EndEdit();

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i <= dgv01.Rows.Count - 1; i++)
                    {
                        if (!IsRowDataChanged(dgv01.Rows[i])) { continue; }

                        mActionID = dgv01.Rows[i].Cells["Action_id"].Value.ToString();

                        progress = new MonthlyProgress(mActionID, OPGlobals.currentYear, OPGlobals.currentMonth);

                        try
                        { mActionStatus = int.Parse(dgv01.Rows[i].Cells["Status"].Value.ToString()); }
                        catch (Exception eex) { mActionStatus = 0; }

                        try
                        { percentageComplete = Double.Parse(dgv01.Rows[i].Cells["Progress"].Value.ToString(), NumberStyles.Currency); }
                        catch (Exception eex) { percentageComplete = 0; }

                        if (percentageComplete > 100) { percentageComplete = 100; }
                        if (percentageComplete < 0) { percentageComplete = 0; }

                        progress.PercentageCompleted = percentageComplete;
                        progress.Description = dgv01.Rows[i].Cells["Remarks"].Value.ToString();
                        progress.ActionStatus = mActionStatus;

                        progress.InsertUpdateMonthlyProgress(db, conn, trans);

                    }

                    trans.Commit();
                    UpdateDataGrid();

                    msg = "Monthly Progress has been saved/updated successfully";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    msg = "Data NOT Saved ..." + Environment.NewLine + ex.Message;
                }
            }

            conn.Close();
            MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void UpdateDataGrid()
        {
            for (int i = 0; i <= dgv01.Rows.Count - 1; i++)
            {
                dgv01.Rows[i].Cells["BStatus"].Value = dgv01.Rows[i].Cells["Status"].Value;
                dgv01.Rows[i].Cells["BProgress"].Value = dgv01.Rows[i].Cells["Progress"].Value;
                dgv01.Rows[i].Cells["BRemarks"].Value = dgv01.Rows[i].Cells["Remarks"].Value;
            }

        }

        private void txtPercentage_Validating(object sender, CancelEventArgs e)
        {
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            //string strsql = ReportQueries.QMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth, true);
            string strsql = MonthlyProgress.GetQueryMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth);
            clsReports.PrintMonthlyProgress(strsql);
        }

        private void dgv01_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
            {
                datagridview.BeginEdit(true);

                ((ComboBox)datagridview.EditingControl).DroppedDown = true;
            }
        }

        private void dgv01_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv01.CurrentCell.ColumnIndex == 7)
            {
                // Check box column
                ComboBox comboBox = e.Control as ComboBox;
                //comboBox.SelectedIndexChanged -= new EventHandler(Status_SelectedIndexChanged);
                comboBox.SelectedIndexChanged += new EventHandler(Status_SelectedIndexChanged);
            }
        }

        private void Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            DataRowView row = (DataRowView)cmb.SelectedItem;
            try
            {
                
                //ChangeCellColorInDataGridView(dgv01.CurrentCell, row["status_color"].ToString());

                dgv01.Rows[dgv01.CurrentRow.Index].Cells[8].Style.BackColor = ColorTranslator.FromHtml(row["status_color"].ToString());
                //dgv01.Rows[dgv01.CurrentRow.Index].Cells[8].Value = row["status_short"].ToString();
            } catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }



            //MessageBox.Show("sdkjf hskdjf");
        }

        private void ChangeCellColorInDataGridView(DataGridViewCell cell, string color)
        {
            try
            {
                //cell.Style.BackColor = ColorTranslator.FromHtml(color);
                //dgv01.SelectedRows[cell.RowIndex].Cells[cell.ColumnIndex].Style.BackColor = ColorTranslator.FromHtml(color);

                dgv01.Rows[dgv01.CurrentRow.Index].Cells[8].Style.BackColor = ColorTranslator.FromHtml(color);
                //dgv01.Rows[dgv01.CurrentRow.Index].Cells[8].Value = 
            } catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void tsbPreviousMonth_Click(object sender, EventArgs e)
        {
            LoadFromPreviousMonth();
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {

        }

        private void tsbClear_Click_1(object sender, EventArgs e)
        {
            ClearData();
        }

        private void dgv01_Leave(object sender, EventArgs e)
        {
            //if (!MyGridUtils.IsColumnDataChanged(dgv01, new List<string>() { "Status", "Progress", "Remarks" }, new List<string>() { "BStatus", "BProgress", "BRemarks" }))
            //{
            //    if (MessageBox.Show("Data has been changed, Do you want to Save Before exit?" , "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //    {
            //        SaveData();
            //    }
            //}
        }

        private void opt0_CheckedChanged(object sender, EventArgs e)
        {
            if (opt0.Checked) { FillGrid(); }
        }
        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked) { OptionCheckedChanged(sender, e); }
        }
        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked) { OptionCheckedChanged(sender, e); }
        }
        private void OptionCheckedChanged(object sender, EventArgs e)
        {
            string msg = "If you have changed any of the row values, this action will clear all, Do you want to Continue?";
            if (MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                FillGrid();
            }
        }
    }
}
