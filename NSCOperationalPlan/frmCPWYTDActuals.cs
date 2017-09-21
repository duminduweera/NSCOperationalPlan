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
using System.Data.Odbc;
using System.Globalization;

namespace NSCOperationalPlan
{
    public partial class frmCPWYTDActual : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmCPWYTDActual()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmCPWYTDActual_Load(object sender, EventArgs e)
        {
            ArrangeGrid();
            LoadTableFromDatabase();
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 35);                   //0
            dct.Add("cpwID", 0);                //1
            dct.Add("Service", 250);            //2
            dct.Add("Description", 490);        //3
            dct.Add("Org.Budget", 150);         //4
            dct.Add("Rev.Budget", 150);         //5
            dct.Add("YTD", 150);                //6
            dct.Add("YtoD copy", 0);            //7
            dct.Add("NEFlag", 0);               //8    == 0-NEW, 1-EDIT
            dct.Add("jobno", 0);                //9


            int[] hiddenRows = { 1,7,8,9 };
            int[] readonlyrows = {0,1,2,3,4,5,7,8,9 };

            MyGridUtils.ArrangeDataGrid(dgv01, dct, hiddenRows);

            dgv01.RowTemplate.MinimumHeight = 28;
            dgv01.DefaultCellStyle.BackColor = Color.Beige;

            this.dgv01.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv01.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach(int i in readonlyrows)
            {
                dgv01.Columns[i].ReadOnly = true;
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

            try
            {
                string strsql = CapitalWork.GetSQLCapitalWorksYTD(OPGlobals.currentYear, OPGlobals.currentMonth) 
                    + " ORDER BY A.director_id, A.cpw_manager_id, A.cpw_id;";

                DataTable tb = db.GetDataTable(conn, strsql);
                foreach (DataRow row in tb.Rows)
                {
                    dgv01.Rows.Add(new String[] {(dgv01.RowCount+1).ToString(),
                        row["cpw_id"].ToString(),
                        row["service_plan"].ToString(),
                        row["cpw_description"].ToString(),
                        string.Format("{0:$0,0.00}", row["cpw_original_budget"]),
                        string.Format("{0:$0,0.00}", row["cpw_revised_budget"]),
                        string.Format("{0:$0,0.00}", row["cpw_ytd"]),
                        row["cpw_ytd"].ToString(),
                        (string.IsNullOrEmpty(row["cpw_ytd"].ToString()) ? "0" : "1"),
                        row["cpw_jobno"].ToString()

                    });
                    //string.Format("{0:$0,0.00}", row["cpw_revised_budget"]),

                }
                dgv01.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            CapitalWork cpw;

            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            string msg = "";
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    for(int i=0; i<dgv01.Rows.Count; i++)
                    {
                        double number1, number2;
                        
                        try
                        { number1 = Double.Parse(dgv01.Rows[i].Cells[6].Value.ToString(), NumberStyles.Currency); }
                        catch (Exception ex) {number1 = 0;}

                        try
                        { number2 = Double.Parse(dgv01.Rows[i].Cells[7].Value.ToString(), NumberStyles.Currency); }
                        catch (Exception ex) { number2 = 0; }

                        if (number1 != number2)
                        {
                            cpw = new CapitalWork(int.Parse(dgv01.Rows[i].Cells[1].Value.ToString()));
                            cpw.CapitalWorkYear = OPGlobals.currentYear;
                            cpw.CapitalWorkMonth = OPGlobals.currentMonth;

                            cpw.YearToDate = number1;
                            if (dgv01.Rows[i].Cells[8].Value.ToString() == "0")
                            {
                                cpw.InsertCWPYTD(db, conn, trans);
                            }
                            else
                            {
                                cpw.UpdateCWPYTD(db, conn, trans);
                            }
                        }
                    }
                    trans.Commit();
                    msg = "Capital Work YTD has been saved/updated successfully";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    msg = "Data NOT Saved ..." + Environment.NewLine + ex.Message;
                }
            }
            conn.Close();
            //msg += Environment.NewLine + "<<<<<<< OPERATION SUMMARY >>>>>>>";
            MessageBox.Show( msg, "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintCapitalWorksSummary(OPGlobals.currentYear, OPGlobals.currentMonth);
            //clsReports.PrintCapitalWorksDepartmentSummary(OPGlobals.currentYear, OPGlobals.currentMonth, "002");
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            pb01.Value = 0;
            pb01.Maximum = dgv01.Rows.Count;
            pb01.Visible = true;

            using (OdbcConnection odbccon = new OdbcConnection("DSN=PCS"))
            {
                if (odbccon.State == ConnectionState.Closed) { odbccon.Open(); }
                foreach (DataGridViewRow row  in dgv01.Rows)
                {
                    System.Data.DataTable tbl = new System.Data.DataTable();
                    string strquery = "SELECT JCACCOUNT, YTDTOT, PTOT FROM JCMST"
                        + " WHERE (JCMST.JCACCOUNT) = '" + row.Cells["jobno"].Value.ToString() + "-0000" + "';";
                    OdbcDataAdapter da = new OdbcDataAdapter(strquery, odbccon);
                    da.Fill(tbl);

                    if (tbl.Rows.Count > 0)
                    {
                        double tval = Double.Parse(tbl.Rows[0][1].ToString()) + Double.Parse(tbl.Rows[0][2].ToString());
                        row.Cells["YTD"].Value = string.Format("{0:$0,0.00}", tval);
                    }
                    pb01.Value++;
                }
            }
            pb01.Visible = false;
            MessageBox.Show("Successfully import data from Practical");
        }
    }
}
