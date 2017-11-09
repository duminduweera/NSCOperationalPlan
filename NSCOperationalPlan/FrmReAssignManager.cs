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
        public FrmReAssignManager()
        {
            InitializeComponent();
        }

        private void FrmReAssignManager_Load(object sender, EventArgs e)
        {
            ArrangeActionGrid();
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
        }


        #region --DATA GRID ARRANGE--
        private void ArrangeActionGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 100);                         //0
            dct.Add("Action", 780);                     //1
            //---------INSERT CHECKBOX COULMN--         //
            dct.Add("Check", 100);                      //2

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
            dgv01.SelectionMode = DataGridViewSelectionMode.CellSelect;
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

            var column = new DataColumn("Chk", typeof(bool));
            column.DefaultValue = false;
            tb.Columns.Add(column);

            dgv01.Rows.Clear();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    //DataGridBoolColumn col = new DataGridBoolColumn();
                    dgv01.Rows.Add(new String[] {
                            row["action_id"].ToString(),
                            row["action_description"].ToString(),
                            row["Chk"].ToString()
                    });

                }
               
                //dgv01.Columns.Add(c);

                dgv01.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
