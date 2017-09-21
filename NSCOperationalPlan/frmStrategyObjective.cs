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
    public partial class frmStrategyObjective : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        bool newflag = true;
        private ListViewItem themeClicked;

        public frmStrategyObjective()
        {
            InitializeComponent();
            ArrangeListView();
            ArrangeGridView();
            FillThemes();
        }

        private void ArrangeListView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("Theme ID", 100);
            dct.Add("Theme", 600);
            dct.Add("ThemeColor", 0);
            dct.Add("code_for_strategy", 0);

            MyDLLs.MyGridUtils.ArrangeListView(lstTheme, dct);
        }
        private void ArrangeGridView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("Theme", 80);
            dct.Add("ID", 60);
            dct.Add("Stategy Objective", 720);
            dct.Add("ThemeColor", 0);

            int[] hiddenRows = { 3 };
            MyDLLs.MyGridUtils.ArrangeDataGrid(dgvStrategyObjective, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            dgvStrategyObjective.ReadOnly = true;
        }

        private void FillDataGrid(string themeID)
        {
            if (newflag == false) { return; }

            string strsql = "Select Distinct strategy_view.theme_id, strategy_view.strategy_objective_id, strategy_view.theme_short, strategy_view.code_for_strategy,"
                + " strategy_view.theme_color, strategy_view.strategy_objective From strategy_view"
                + " Where strategy_view.theme_id = '" + themeID + "' Order By strategy_view.strategy_objective_id";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);

            dgvStrategyObjective.Rows.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgvStrategyObjective.Rows.Add(new String[] { row["theme_id"].ToString(),
                              row["strategy_objective_id"].ToString(),
                              row["strategy_objective"].ToString() + Environment.NewLine,
                              row["theme_color"].ToString()
                    });

                }
                MyDLLs.MyGridUtils.ColorDataGrid(dgvStrategyObjective, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillThemes()
        {
            string strsql = "SELECT theme.* FROM theme ORDER BY id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            lstTheme.Items.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    ListViewItem lvi = new ListViewItem(row["id"].ToString());
                    lvi.SubItems.Add(row["theme_short"].ToString());
                    lvi.SubItems.Add(row["theme_color"].ToString());
                    lvi.SubItems.Add(row["theme_code_for_strategy"].ToString());

                    lstTheme.Items.Add(lvi);
                }

                MyDLLs.MyGridUtils.ListViewAlternateRowColor(lstTheme, Color.LightGray, Color.Bisque, 0, 2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lstTheme_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            themeClicked = MyDLLs.MyGridUtils.listViewDeSelectPreviousItems(lstTheme, e, themeClicked);
            if (e.NewValue == CheckState.Checked)
            {
                FillDataGrid(lstTheme.SelectedItems[0].SubItems[0].Text.ToString());
                dgvStrategyObjective.CurrentCell = null;
                SetNextStrategyObjectiveID(lstTheme.SelectedItems[0].SubItems[0].Text.ToString(), lstTheme.SelectedItems[0].SubItems[3].Text.ToString());
            }
            else
            {
            }
        }
        private void SetNextStrategyObjectiveID(string themeid,  string strategy_objective_type)
        {
            try
            {
                string strsql = "SELECT COUNT(*) As noofrecs FROM strategy_objective"
                    + " WHERE strategy_objective.theme_id = '" + themeid + "' GROUP BY strategy_objective.theme_id";
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = db.GetDataTable(conn, strsql);

                int nextid = 1;

                if (tb.Rows.Count > 0)
                {
                    nextid = int.Parse(tb.Rows[0][0].ToString()) + 1;
                }
                txtStrategyObjectiveID.Text = strategy_objective_type.ToUpper() + nextid.ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearText()
        {
            txtStrategyObjectiveID.Text = "";
            txtStrategyObjective.Text = "";
            //cboTheme.SelectedIndex = 0;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {

                newflag = true;

                Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

                strdct.Add("ThemeID", lstTheme.SelectedItems[0].SubItems[0].Text); // lstTheme.SelectedItems[0].SubItems[0].Text);
                strdct.Add("ID", txtStrategyObjectiveID.Text.ToString());
                strdct.Add("StrategyObjective", txtStrategyObjective.Text.ToString());

                string strsql = "SELECT COUNT(*) as noofrecs FROM strategy_objective"
                    + " WHERE (id)='" + txtStrategyObjectiveID.Text.ToString() + "'";

                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = db.GetDataTable(conn, strsql);

                //if (tb.Rows[0][0] > 0) { newflag = false; }
                if (int.Parse(tb.Rows[0][0].ToString()) > 0) { newflag = false; }

                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (DbTransaction trans = conn.BeginTransaction())
                {

                    try
                    {
                        if (newflag) { InsertStrategyObjective(conn, trans, strdct); } else { UpdateStrategyObjective(conn, trans, strdct); }
                        trans.Commit();
                        MessageBox.Show("Theme has been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                conn.Close();
                tsbNew_Click(sender, e);
                FillDataGrid(lstTheme.SelectedItems[0].SubItems[0].Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private bool InsertStrategyObjective(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            bool result = false;

            string query = @"INSERT INTO strategy_objective (id, strategy_objective, theme_id)"
                + " VALUES (@ID , @StrategyObjective, @ThemeID)";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }
        private bool UpdateStrategyObjective(DbConnection con, DbTransaction trans, Dictionary<string, dynamic> strdct)
        {
            bool result = false;
            //Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //strdct.Add("ThemeID", lstTheme.SelectedItems[0].SubItems[0].Text); // lstTheme.SelectedItems[0].SubItems[0].Text);
            //strdct.Add("ID", txtStrategyObjectiveID.Text.ToString());
            //strdct.Add("StrategyObjective", txtStrategyObjective.Text.ToString());

            string query = @"UPDATE strategy_objective SET strategy_objective = @StrategyObjective, theme_id = @ThemeID WHERE id = @ID";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }
        private void PopulateDataFromDataGrid()
        {
            try
            {
                
                //cboTheme.Text = lstStrategy.SelectedItems[0].Text;
                txtStrategyObjectiveID.Text = dgvStrategyObjective.CurrentRow.Cells[1].Value.ToString(); //lstStrategy.SelectedItems[0].SubItems[1].Text;
                txtStrategyObjective.Text = dgvStrategyObjective.CurrentRow.Cells[2].Value.ToString().TrimEnd();  //lstStrategy.SelectedItems[0].SubItems[2].Text;

                lstTheme.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void tsbNew_Click(object sender, EventArgs e)
        {
            newflag = true;
            tsbNew.Enabled = false;
            tsbEdit.Enabled = true;
            lstTheme.Enabled = true;
            ClearText();
            if (themeClicked.Selected)
            {
                SetNextStrategyObjectiveID(lstTheme.SelectedItems[0].SubItems[0].Text.ToString(), lstTheme.SelectedItems[0].SubItems[3].Text.ToString());
            }
        }

        private void dgvStrategyObjective_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            newflag = false;
            tsbNew.Enabled = true;
            tsbEdit.Enabled = false;
            PopulateDataFromDataGrid();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintStrategyObjective();
        }
    }
}
