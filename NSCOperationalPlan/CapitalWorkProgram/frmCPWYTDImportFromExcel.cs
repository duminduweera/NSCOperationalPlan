using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Data.Common;
using MyDLLs;
using System.IO;

namespace NSCOperationalPlan
{
    public partial class frmCPWYTDImportFromExcel : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmCPWYTDImportFromExcel()
        {
            InitializeComponent();
            ArrangeGrid();
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("JCNo", 80);                //1
            dct.Add("YTD", 100);                 //2
            dct.Add("R_Bdgt", 100);            //3

            int[] hiddenRows = { };
            int[] readonlyrows = { };

            MyGridUtils.ArrangeDataGrid(dg1, dct, hiddenRows);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filePath = openFileDialog1.FileName;
                //HashSet<string> CPWNotinDataBase = CapitalWork.getCPWNotinOP("D:\\Downloads\\CPW.xlsx");
                ArrangeGrid();
                ReadFromExcel(filePath);
            }
        }

        private void ReadFromExcel(string filePath)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            Excel.Range xlRange = xlWorksheet.UsedRange; // get the entire used range


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            double totYTD = 0.0;
            double totRB = 0.0;
            double temp = 0.0;

            txtRows.Text = rowCount.ToString();
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
                    if(xlRange.Cells[i, 2].Value2 != null)
                    {
                        dg1.Rows.Add(new String[] {
                            xlRange.Cells[i, 1].Value2.ToString(),
                            xlRange.Cells[i, 2].Value2.ToString(),
                            xlRange.Cells[i, 3].Value2.ToString()
                        });

                        temp = Double.Parse(string.IsNullOrEmpty(xlRange.Cells[i, 2].Value2.ToString()) ? "0" : xlRange.Cells[i, 2].Value2.ToString(),
                                NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);

                        totYTD += temp;

                        temp = Double.Parse(string.IsNullOrEmpty(xlRange.Cells[i, 3].Value2.ToString()) ? "0" : xlRange.Cells[i, 3].Value2.ToString(),
                                NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
                        totRB += temp;
                    }
                }
                txt1.Text = string.Format("{0:0.#0}", totYTD.ToString());
                txt2.Text = string.Format("{0:0.#0}", totRB.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void tsbSave_Click(object sender, EventArgs e)
        {
            string path = @"\log.txt";
            Dictionary<string, dynamic> dc = new Dictionary<string, dynamic>();
            Dictionary<string, dynamic> dc2 = new Dictionary<string, dynamic>();

            double cwpYTD = 0.0;
            double cwpRB = 0.0;


            for (int dr = 0; dr < dg1.RowCount; dr++)
            {
                dc.Clear();
                //fill Directory 1
                dc.Add("jcno", dg1.Rows[dr].Cells["JCNo"].Value.ToString());
                cwpYTD = Double.Parse(string.IsNullOrEmpty(dg1.Rows[dr].Cells["YTD"].Value.ToString()) ? "0" : dg1.Rows[dr].Cells["YTD"].Value.ToString(),
                                NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
                dc.Add("ytd", cwpYTD);
                dc.Add("cwpyear", OPGlobals.currentYear);
                dc.Add("cwpmonth", OPGlobals.currentMonth);

                //Fill Directory 2
                dc2.Clear();
                dc2.Add("jcno", dg1.Rows[dr].Cells["JCNo"].Value.ToString());
                dc2.Add("cwpyear", OPGlobals.currentYear);
                dc2.Add("cwpquarter", OPGlobals.CurrentQuarter);

                cwpRB = Double.Parse(string.IsNullOrEmpty(dg1.Rows[dr].Cells["R_Bdgt"].Value.ToString()) ? "0" : dg1.Rows[dr].Cells["R_Bdgt"].Value.ToString(),
                                NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
                dc2.Add("revised_budget", cwpRB);

                try
                {
                    DataTable tb = db.GetResultTableByStoredProcedures("sp_update_cwp_ytd_with_jcno", dc);
                    File.AppendAllText(path, tb.Rows[0][0].ToString() + Environment.NewLine);

                    DataTable tb2 = db.GetResultTableByStoredProcedures("sp_update_cwp_revised_budget_by_jcno", dc2);
                    File.AppendAllText(path, tb2.Rows[0][0].ToString() + Environment.NewLine);

                } catch (Exception ex)
                {
                    File.AppendAllText(path, "Error in " + dg1.Rows[dr].Cells["JCNo"].Value.ToString() + ex.Message + Environment.NewLine);
                }

            }

            MessageBox.Show("Capital Work Project has been imported successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
