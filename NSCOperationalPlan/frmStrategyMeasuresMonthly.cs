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
    public partial class frmStrategyMeasuresMonthly : Form
    {
        private int[] hiddenRows;
        private int[] readonlyrows;
        public frmStrategyMeasuresMonthly()
        {
            InitializeComponent();
        }
        private void frmStrategyMeasuresMonthly_Load(object sender, EventArgs e)
        {
            setUserRights();
            ArrangeGrid();
            BindDropDownToDataGrid();


            AddDatatoGrid(StrategyMeasures.getMeasuresforManagers(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear));

        }
        private void setUserRights()
        {
#if DEBUG
            //chk1.Visible = true;
            //chk2.Visible = true;
            OPGlobals.CurrentUser.Permission = UserRights.Director;

#endif

            hiddenRows = new int[] { 6, 7, 8 };

            readonlyrows = new int[] { 0, 1, 2, 3, 4, 5 };
            if (OPGlobals.CurrentUser.Permission == UserRights.Director)
            {
                chk1.Visible = true;
                readonlyrows = new int[] { 0, 1, 2, 3, 4 };
            }
            if (OPGlobals.CurrentUser.Permission == UserRights.GM)
            {
                chk2.Visible = true;
            }

        }
        private void ArrangeGrid()
        {
            Dictionary<string, int> coloumnDict = new Dictionary<string, int>();
            coloumnDict.Add("#", 35);//0
            coloumnDict.Add("Strategy", 350);//1
            coloumnDict.Add("Measure", 500);//2
            coloumnDict.Add("Source", 100);//3
            coloumnDict.Add("Accountable Director", 150);//4
            coloumnDict.Add("Progress", 500);//5
            coloumnDict.Add("ProgressOriginal", 240);//6
            coloumnDict.Add("themeColour", 240);//7
            coloumnDict.Add("ResponsibleManagerOriginal", 240);//8
            MyDLLs.MyGridUtils.ArrangeDataGrid(dgv, coloumnDict, hiddenRows);

            dgv.RowTemplate.MinimumHeight = 28;
            dgv.DefaultCellStyle.BackColor = Color.Beige;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private void BindDropDownToDataGrid()
        {
            var cmbManager = new DataGridViewComboBoxColumn();
            DataTable tb = StrategyMeasures.getAllManagers();
            cmbManager.HeaderText = "Responsible Manager";
            cmbManager.Name = "Responsible Manager";
            cmbManager.DataSource = tb;
            cmbManager.ValueMember = "id";
            cmbManager.DisplayMember = "manager_description";
            dgv.Columns.Insert(5, cmbManager);
            dgv.Columns[5].MinimumWidth = 250;
            cmbManager.FlatStyle = FlatStyle.Flat;
            cmbManager.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            foreach (int i in readonlyrows)
            {
                dgv.Columns[i].ReadOnly = true;
                dgv.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                AddDatatoGrid(StrategyMeasures.getMeasuresforDirectors(OPGlobals.CurrentUser.DirectorID, OPGlobals.currentYear));
            }
            else
            {
                AddDatatoGrid(StrategyMeasures.getMeasuresforManagers(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear));
            }

        }
        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked) { AddDatatoGrid(StrategyMeasures.getAllMeasures(OPGlobals.currentYear)); }
            else { AddDatatoGrid(StrategyMeasures.getMeasuresforManagers(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear)); }
        }
        private void AddDatatoGrid(DataTable tb)
        {
            dgv.Rows.Clear();
            dgv.Refresh();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv.Rows.Add(new String[] {(
                        dgv.RowCount+1).ToString(),
                        row["strategy_id"].ToString()+" "+row["strategy"].ToString(),
                        row["strategy_measure_code"].ToString()+" "+row["description"].ToString(),
                        row["source"].ToString(),
                        row["director_description"].ToString(),
                        row["manager_id"].ToString(),
                        "",
                        "",
                        row["theme_color"].ToString(),
                        row["manager_id"].ToString()
                    });
                }
                MyDLLs.MyGridUtils.ColorDataGrid(dgv, 0, 8);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dgv.CurrentCell = null;
        }

        private void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick && datagridview.Columns[e.ColumnIndex].ReadOnly == false)
            {
                datagridview.BeginEdit(true);
                ((ComboBox)datagridview.EditingControl).DroppedDown = true;
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 0) {
                DbConnection conn = OPGlobals.db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //if (SaveDataToDatabase(dgv, conn, trans) == false) {  }
                        trans.Commit();
                        MessageBox.Show("Strategy Measures have been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AddDatatoGrid(StrategyMeasures.getMeasuresforManagers(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear));
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                conn.Close();
            }
            
            //RefreshGridView();
        }

        private void SaveDataToDatabase(DataGridView dgv, DbConnection conn, DbTransaction trans)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (MyGridUtils.IsColumnDataChanged(dgv.Rows[i],
                       new List<String>() { "Responsible Manager", "Progress" },
                       new List<String>() { "ResponsibleManagerOriginal", "ProgressOriginal" }))
                {
                    StrategyMeasures tempMeasure = new StrategyMeasures();
                    tempMeasure.MeasureCode = dgv.Rows[i].Cells["Measure"].Value.ToString().Split(' ')[0];
                    tempMeasure.Strategy_id = dgv.Rows[i].Cells["Strategy"].Value.ToString().Split(' ')[0];
                    tempMeasure.Year = OPGlobals.currentYear;

                }
                else
                {

                    continue;
                }
            }

          
        }
    }
}
