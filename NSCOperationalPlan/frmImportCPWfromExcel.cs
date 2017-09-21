using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyDLLs;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Globalization;

namespace NSCOperationalPlan
{
    public partial class frmImportCPWfromExcel : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmImportCPWfromExcel()
        {
            InitializeComponent();
        }

        private void frmImportCPWfromExcel_Load(object sender, EventArgs e)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, "SELECT capital_works_id,capital_works_jobno,capital_works_original_budget FROM nsc_operation_plan_17_to_21.capital_works order by capital_works_id;");
            Dictionary<String, double> CapitalWorks = new Dictionary<string, double>();

            foreach (DataRow row in tb.Rows)
            {
              
                CapitalWorks.Add(row.ItemArray[1].ToString(),double.Parse(row.ItemArray[2].ToString()));
                //Console.WriteLine(row.ItemArray[0].ToString()+"     "+ row.ItemArray[1].ToString());
             
            }
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("JCNo", 80);                //1
            dct.Add("Description", 490);        //2
            dct.Add("Org.Budget", 90);         //3
            dct.Add("Rev.Budget", 90);         //4
            dct.Add("Service", 150);                //5
            dct.Add("Directore", 150);            //6
            dct.Add("Manager", 150);               //7

            dct.Add("ServiceID", 50);               //7
            dct.Add("DirectorID", 50);               //7
            dct.Add("ManagerID", 50);               //7


            int[] hiddenRows = { };
            int[] readonlyrows = {  };

            MyGridUtils.ArrangeDataGrid(dg1, dct, hiddenRows);
        }

        private void ReadFromExcel()
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open("C:\\Users\\perersu\\Desktop\\SAM4.xlsx");
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            Excel.Range xlRange = xlWorksheet.UsedRange; // get the entire used range


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            txtRows.Text = rowCount.ToString() ;
            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!

            pb1.Value = 0;
            pb1.Maximum = rowCount;
            pb1.Visible = true;

            string val;
            try
            {
                for (int i = 2; i <= rowCount; i++)
                {
                    int original = int.Parse(xlRange.Cells[i, 3].Value2.ToString());
                    int revised = int.Parse(xlRange.Cells[i, 4].Value2.ToString());
                    pb1.Value += 1;

                    if (original !=0 && original != revised) {
                        Dictionary<string, string> userDetails = new Dictionary<string, string>();
                        val = xlRange.Cells[i, 1].Value2.ToString();
                        userDetails = GetManagerID(xlRange.Cells[i, 6].Value2.ToString());
                        dg1.Rows.Add(new String[] {
                            val.Substring(0,9),
                            xlRange.Cells[i, 2].Value2.ToString(),
                            xlRange.Cells[i, 3].Value2.ToString(),
                            xlRange.Cells[i, 4].Value2.ToString(),
                            xlRange.Cells[i, 5].Value2.ToString(),
                            "",
                            xlRange.Cells[i, 6].Value2.ToString(),
                            GetServiceID(xlRange.Cells[i, 5].Value2.ToString()),
                            userDetails["DirectorID"],
                            userDetails["ManagerID"]
                        });
                        
                    }
                    else {
                        continue;
                    }                                     
                }
            }
            catch (Exception ex) {
                int x=5;
            }
             pb1.Visible = false;
            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private string GetServiceID(string service)
        {

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "Select id, service_plan From service_plan Where service_plan = '" + service + "'";
            DataTable tb = db.GetDataTable(conn, strsql);

            if(tb.Rows.Count == 0)
            {
                return "-0-";
            } else
            {
                return tb.Rows[0]["id"].ToString();
            }

        }

        private Dictionary<string,string> GetManagerID(string manager)
        {

            Dictionary<string, string> userDetails = new Dictionary<string, string>();
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "Select id, manager_id From manager Where manager_description = '" + manager + "'";
            DataTable tb = db.GetDataTable(conn, strsql);

            if (tb.Rows.Count == 0)
            {
                userDetails.Add("DirectorID", "-0-");
                userDetails.Add("ManagerID", "-0-");
            }
            else
            {
                userDetails.Add("DirectorID", tb.Rows[0]["manager_id"].ToString());
                userDetails.Add("ManagerID", tb.Rows[0]["id"].ToString());
            }
            return userDetails;
        }

       

        private void tsbNew_Click(object sender, EventArgs e)
        {
            //ArrangeGrid();
            //ReadFromExcel();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            for(int dr=0; dr < dg1.RowCount; dr++)
            {
                CapitalWork cpw = new CapitalWork(dr+170);
                cpw.CapitalWorkJobCostNumber = dg1.Rows[dr].Cells["JCNo"].Value.ToString();
                cpw.Description = dg1.Rows[dr].Cells["Description"].Value.ToString();

                cpw.OriginalBudget = Double.Parse(string.IsNullOrEmpty(dg1.Rows[dr].Cells["Org.Budget"].Value.ToString()) ? "0" : dg1.Rows[dr].Cells["Org.Budget"].Value.ToString(),
                            NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);

                cpw.RevisedBudget = Double.Parse(string.IsNullOrEmpty(dg1.Rows[dr].Cells["Rev.Budget"].Value.ToString()) ? "0" : dg1.Rows[dr].Cells["Rev.Budget"].Value.ToString(),
                            NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);

                cpw.ServicePlanID = dg1.Rows[dr].Cells["ServiceID"].Value.ToString();
                cpw.ManagerID = dg1.Rows[dr].Cells["ManagerID"].Value.ToString();

                cpw.CapitalWorkYear = OPGlobals.currentYear;
                cpw.CapitalWorkMonth = OPGlobals.currentMonth;


                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (cpw.OriginalBudget == 0)
                        {
                            bool saveresult = cpw.InsertCWP(db, conn, trans);
                            if (saveresult) { cpw.InsertCWPQBR(db, conn, trans); } else { throw new Exception("Something went worng!!!"); }
                        }
                       


                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            MessageBox.Show("Capital Work Project has been imported successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

     
    }
}
