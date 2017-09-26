using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCOperationalPlan
{
    public partial class frmStrategyMeasuresMonthly : Form
    {
        public frmStrategyMeasuresMonthly()
        {
            InitializeComponent();
        }
        private void frmStrategyMeasuresMonthly_Load(object sender, EventArgs e)
        {
            ArrangeGrid();
            setUserRights();
            AddDatatoGrid(StrategyMeasures.getMeasures(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear));

    }
        private void setUserRights()
        {
            if (OPGlobals.CurrentUser.Permission == UserRights.Director) { chk1.Visible = true; }
            if (OPGlobals.CurrentUser.Permission == UserRights.GM) { chk2.Visible = true; }
            #if DEBUG
                chk1.Visible = true;
                chk2.Visible = true;
            #endif
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> coloumnDict = new Dictionary<string, int>();
            coloumnDict.Add("#", 35);
            coloumnDict.Add("Strategy ID", 80);
            coloumnDict.Add("Description", 350);
            coloumnDict.Add("Measure Code", 60);
            coloumnDict.Add("Source", 250);
            coloumnDict.Add("Comment", 500);
            coloumnDict.Add("CommentOriginal", 240);
            int[] hiddenRows = { 6 };
            int[] readonlyrows = { 0, 1, 2, 3, 4, 6 };
            MyDLLs.MyGridUtils.ArrangeDataGrid(dgv, coloumnDict, hiddenRows);

            dgv.RowTemplate.MinimumHeight = 28;
            dgv.DefaultCellStyle.BackColor = Color.Beige;
            foreach (int i in readonlyrows)
            {
                dgv.Columns[i].ReadOnly = true;
                dgv.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked) {AddDatatoGrid(StrategyMeasures.getMeasures(OPGlobals.CurrentUser.DirectorID, OPGlobals.currentYear)); }
            else {AddDatatoGrid(StrategyMeasures.getMeasures(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear)); }

        }
        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked) { AddDatatoGrid(StrategyMeasures.getAllMeasures(OPGlobals.currentYear)); }
            else { AddDatatoGrid(StrategyMeasures.getMeasures(OPGlobals.CurrentUser.ManagerID, OPGlobals.currentYear)); }
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
                        row["strategy_id"].ToString(),
                        row["description"].ToString(),
                        row["strategy_measure_code"].ToString(),
                        row["source"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dgv.CurrentCell = null;
        }

       
    }
}
