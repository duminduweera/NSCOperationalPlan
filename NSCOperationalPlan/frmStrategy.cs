using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using MyDLLs;

namespace NSCOperationalPlan
{
    public partial class frmStrategy : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        bool newflag = true;
        private ListViewItem themeClicked;

        public frmStrategy()
        {
            InitializeComponent();
            ArrangeListView();
            ArrangeGridView();
        }

        private void frmStrategy_Load(object sender, EventArgs e)
        {
            FillThemes();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ArrangeListView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("Theme ID", 100);
            dct.Add("Theme", 600);
            dct.Add("ThemeColor", 0);

            MyDLLs.MyGridUtils.ArrangeListView(lstTheme, dct);
        }
        private void ArrangeGridView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("Theme", 80);
            dct.Add("ID", 60);
            dct.Add("Stategy", 720);
            dct.Add("ThemeColor", 0);
            dct.Add("StrategyRank", 0);

            int[] hiddenRows = {3,4};
            MyDLLs.MyGridUtils.ArrangeDataGrid(dgvStrategy, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            dgvStrategy.ReadOnly = true;
        }

        private void FillDataGrid(string themeID, string strategyObjID)
        {
            if (newflag == false) { return; }

            string strsql = "Select * from strategy_view Where theme_id = '" + themeID + "' AND strategy_objective_id = '" + strategyObjID + "' ORDER BY strategy_rank;";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);

            dgvStrategy.Rows.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgvStrategy.Rows.Add(new String[] { row["theme_id"].ToString(),
                              row["strategy_id"].ToString(),
                              row["strategy"].ToString() + Environment.NewLine,
                              row["theme_color"].ToString(),
                              row["strategy_rank"].ToString()
                    });

                }
                MyDLLs.MyGridUtils.ColorDataGrid(dgvStrategy, 0, 3);
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
            DataTable tb = db.GetDataTable(conn,strsql);

            lstTheme.Items.Clear();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    ListViewItem lvi = new ListViewItem(row["id"].ToString());
                    lvi.SubItems.Add(row["theme_short"].ToString());
                    lvi.SubItems.Add(row["theme_color"].ToString());

                    lstTheme.Items.Add(lvi);
                }

                MyDLLs.MyGridUtils.ListViewAlternateRowColor(lstTheme, Color.LightGray, Color.Bisque, 0, 2);

                cboTheme.DataSource = tb;
                cboTheme.DisplayMember = "theme_short";
                cboTheme.ValueMember = "id";
                cboTheme.Tag = "theme_color";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool InsertStrategy(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            bool result = false;

            string query = @"INSERT INTO strategy (id, rank, strategy, strategy_objective_id, theme_id)"
                + " VALUES (@StrategyID, @Rank, @Strategy, @StrategyObjectiveID, @ThemeID)";
            try
            {
                db.InsertUpdateDeleteRecord(con,trans, query, strdct);
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }
        private bool UpdateStrategy(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            bool result = false;

            //Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //strdct.Add("ThemeID", dgvStrategy.CurrentRow.Cells[0].Value.ToString()); // lstTheme.SelectedItems[0].SubItems[0].Text);
            //strdct.Add("StrategyObjectiveID", txtStrategyObjectiveID.Text.ToString());
            //strdct.Add("StrategyID", txtStrategyID.Text.ToString());
            //strdct.Add("Strategy", txtStrategy.Text.ToString());

            string query = @"UPDATE strategy SET strategy = @Strategy WHERE id = @StrategyID";
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
        private void tsbSave_Click(object sender, EventArgs e)
        {
            try
            {
                newflag = true;

                Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

                strdct.Add("ThemeID", lstTheme.SelectedItems[0].SubItems[0].Text.ToString()); // lstTheme.SelectedItems[0].SubItems[0].Text);
                strdct.Add("StrategyObjectiveID", txtStrategyObjectiveID.Text.ToString());
                strdct.Add("StrategyID", txtStrategyID.Text.ToString());
                strdct.Add("Strategy", txtStrategy.Text.ToString());
                strdct.Add("Rank", txtStrategyRank.Text);

                string strsql = "SELECT COUNT(*) as noofrecs FROM strategy"
                    + " WHERE (strategy.id) = '" + txtStrategyID.Text.ToString() + "';";  //AND (strategy.theme_id)='" + dgvStrategy.CurrentRow.Cells[0].Value.ToString() + "';"; // lstTheme.SelectedItems[0].SubItems[0].Text + "';";
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = db.GetDataTable(conn, strsql);
                if (int.Parse(tb.Rows[0][0].ToString()) > 0) { newflag = false; }

                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (newflag) { InsertStrategy(conn, trans, strdct); } else { UpdateStrategy(conn, trans, strdct); }
                        trans.Commit();
                        MessageBox.Show("Strategy has been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                conn.Close();
                tsbNew_Click(sender, e);
                FillDataGrid(lstTheme.SelectedItems[0].SubItems[0].Text, txtStrategyObjectiveID.Text);
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
            //txtStrategyID.Enabled = true;
            ClearText();

            List<string> nextid = new List<string>();
            nextid = GetNextStrategyID(lstTheme.SelectedItems[0].SubItems[0].Text.ToString(), txtStrategyObjectiveID.Text);
            txtStrategyID.Text = nextid[0];
            txtStrategyRank.Text = nextid[1];

            //txtStrategyID.Text= GetNextStrategyID(lstTheme.SelectedItems[0].SubItems[0].Text.ToString(), txtStrategyObjectiveID.Text);
            //txtStrategyID.Focus();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            newflag = false;
            tsbNew.Enabled = true;
            tsbEdit.Enabled = false;
            //txtStrategyID.Enabled = false;
            txtStrategy.Focus();
            PopulateDataFromList();

        }

        private void PopulateDataFromList()
        {
            try
            {
                //cboTheme.Text = lstStrategy.SelectedItems[0].Text;
                txtStrategyID.Text = dgvStrategy.CurrentRow.Cells[1].Value.ToString(); //lstStrategy.SelectedItems[0].SubItems[1].Text;
                txtStrategyRank.Text = dgvStrategy.CurrentRow.Cells[4].Value.ToString(); //lstStrategy.SelectedItems[0].SubItems[1].Text;
                txtStrategy.Text = dgvStrategy.CurrentRow.Cells[2].Value.ToString().TrimEnd();  //lstStrategy.SelectedItems[0].SubItems[2].Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void lstStrategy_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void ClearText()
        {
            txtStrategyID.Text = "";
            txtStrategy.Text = "";
            //cboTheme.SelectedIndex = 0;
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintStrategy();
        }

        private void lstTheme_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            themeClicked = MyDLLs.MyGridUtils.listViewDeSelectPreviousItems(lstTheme, e, themeClicked);
            if (e.NewValue == CheckState.Checked)
            {
                FillStrategyObjective(lstTheme.SelectedItems[0].SubItems[0].Text.ToString());
                dgvStrategy.CurrentCell = null;
            }
            else
            {
            }
        }

        private void FillStrategyObjective(string themeid)
        {
            string strsql = "SELECT * FROM strategy_objective WHERE strategy_objective.theme_id = '" + themeid + "' ORDER BY id;";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);

            //cboStrategyObjective.Items.Clear();
            cboStrategyObjective.DataSource = tb;
            cboStrategyObjective.ValueMember = "id";
            cboStrategyObjective.DisplayMember = "strategy_objective";
        }
        private void dgvStrategy_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private List<string> GetNextStrategyID(string themeid, string strategyobjid)
        {
            List<string> nextstrategyid = new List<string>();

            string strsql = "SELECT Count(strategy.id)As noofrecs FROM strategy WHERE strategy.strategy_objective_id = '" + strategyobjid + "' AND strategy.theme_id = '" + themeid + "'";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            int nextid = 1;

            if (tb.Rows.Count > 0)
            {
                nextid = int.Parse(tb.Rows[0][0].ToString()) + 1;
            }

            //nextstrategyid = strategyobjid + "." + nextid.ToString().Trim();

            nextstrategyid.Clear();

            nextstrategyid.Add(strategyobjid + "." + nextid.ToString().Trim());
            nextstrategyid.Add(nextid.ToString().Trim());

            return nextstrategyid;
        }

        private void cboStrategyObjective_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStrategyObjective.SelectedIndex < 0) { return; }

            DataRowView row = (DataRowView)cboStrategyObjective.SelectedItem;
            txtStrategyObjectiveID.Text = row[0].ToString();

            List<string> nextid = new List<string>();
            nextid = GetNextStrategyID(row[3].ToString(), row[0].ToString());
            txtStrategyID.Text = nextid[0];
            txtStrategyRank.Text = nextid[1];

            FillDataGrid(lstTheme.SelectedItems[0].SubItems[0].Text.ToString(), txtStrategyObjectiveID.Text);
        }

        private void cboStrategyObjective_Click(object sender, EventArgs e)
        {
            //string nextif = GetNextStrategyID();
        }

        //private void cboTest_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //e.DrawBackground();

        //// Get the item text    
        //string text = ((ComboBox)sender).Items[e.Index].ToString();

        //// Determine the forecolor based on whether or not the item is selected  

        ////string hex = "000000";

        ////Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
        //Brush brush;
        //brush = Brushes.Black;
        ////brush = new SolidBrush(_color);
        //switch (((ComboBox)sender).SelectedValue.ToString())
        //{
        //    case "Theame1":
        //        brush = Brushes.Red;
        //        //hex = "FF0000";
        //        break;
        //    case "Theame2":
        //        brush = Brushes.Green;
        //        //hex = "FF00FF";
        //        break;
        //    case "Theame3":
        //        brush = Brushes.Blue;
        //        //hex = "FFFF00";
        //        break;
        //    case "Theame4":
        //        brush = Brushes.Brown;
        //        //hex = "00FF00";
        //        break;
        //}

        ////if (YourListOfDates[e.Index] < DateTime.Now)// compare  date with your list.  
        ////{
        ////    brush = Brushes.Red;
        ////}
        ////else
        ////{
        ////    brush = Brushes.Green;
        ////}

        //// Draw the text    
        //e.Graphics.DrawString(text, ((Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
        //}
    }
}
