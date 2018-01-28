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
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace NSCOperationalPlan
{
    public partial class frmCPWQuartelyUpdate : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmCPWQuartelyUpdate()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 35);                   //0
            dct.Add("cpwID", 0);                //1
            dct.Add("Service", 200);            //2
            dct.Add("Description", 530);        //3
            dct.Add("Org.Budget", 120);         //4
            dct.Add("Rev.Budget", 120);         //5
            dct.Add("Projected", 120);          //6

            int[] hiddenRows = { 1 };
            int[] readonlyrows = { 0, 1, 2, 3, 4, 5 };

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows);
            dgv01.DefaultCellStyle.BackColor = Color.Beige;

            this.dgv01.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv01.RowTemplate.MinimumHeight = 28;

            foreach (int i in readonlyrows)
            {
                this.dgv01.Columns[i].ReadOnly = true;
                this.dgv01.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                this.dgv01.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
            }


            dgv01.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        private void LoadTableFromDatabase()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            dgv01.Rows.Clear();
            dgv01.Refresh();

            string strsql;
            strsql = CapitalWork.GetSQLCapitalWorksMonthlyProgress(OPGlobals.currentYear, OPGlobals.currentMonth) 
                + " Order by A.director_id, A.cpw_manager_id, A.cpw_id; ";

            DataTable tb = db.GetDataTable(conn, strsql);
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    dgv01.Rows.Add(new String[] {(dgv01.RowCount+1).ToString(),
                    row["cpw_id"].ToString(),
                    row["service_plan"].ToString(),
                    row["cpw_description"].ToString(),
                    //string.Format("{0:$0,0.00}", row["cpw_original_budget"]),
                    string.Format("{0:C}", row["cpw_original_budget"]),
                    string.Format("{0:C}", row["cpw_revised_budget"]),
                    string.Format("{0:C}", row["cpw_projected"])
                });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dgv01.CurrentCell = null;
        }

        private void frmCPWQuartelyUpdate_Load(object sender, EventArgs e)
        {
            ArrangeGrid();
            LoadTableFromDatabase();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            var exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
            bool opstatus = true;
            string msg = "<<<<<<< OPERATION SUMMARY >>>>>>>" + Environment.NewLine;
            pb1.Value = 0;
            pb1.Maximum = dgv01.RowCount+1 ;
            pb1.Visible = true;
            double projected;
            double revised;
            CapitalWork cw;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }

            using (DbTransaction trans = conn.BeginTransaction())
            {               
                    for (int i = 0; i < dgv01.RowCount; i++)
                    {
                        dgv01.CurrentCell = dgv01.Rows[i].Cells[0];
                        pb1.Value++;
                    try
                    {
                        projected = Double.Parse(string.IsNullOrEmpty(dgv01.Rows[i].Cells["Projected"].Value.ToString()) ? "0" : dgv01.Rows[i].Cells["Projected"].Value.ToString(),
                           NumberStyles.AllowCurrencySymbol | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint);
                        revised = Double.Parse(string.IsNullOrEmpty(dgv01.Rows[i].Cells["Rev.Budget"].Value.ToString()) ? "0" : dgv01.Rows[i].Cells["Rev.Budget"].Value.ToString(),
                            NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
                        if (projected <= 0)
                        {                           
                            continue;
                        }
                        cw = new CapitalWork(int.Parse(dgv01.Rows[i].Cells["cpwID"].Value.ToString()));
                        cw.CapitalWorkYear = OPGlobals.currentYear;
                        cw.CapitalWorkMonth = OPGlobals.currentMonth;
                        cw.RevisedBudget = projected;
                        if (!cw.InsertCWPQBR(db, conn, trans)) {
                            cw.UpdateCWPQBR(db, conn, trans);
                        }                       
                    }
                    catch (NullReferenceException ex1)
                    {
                         continue;                         
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        msg = ex.Message;
                        break;
                    }                    
                    }
                    trans.Commit();
                    msg += "Data saved successfully";
                    LoadTableFromDatabase();           
            }
            conn.Close();
            MessageBox.Show(msg, "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pb1.Visible = false;     
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorksQBR(OPGlobals.currentYear, OPGlobals.currentMonth);
        }
    }
}
