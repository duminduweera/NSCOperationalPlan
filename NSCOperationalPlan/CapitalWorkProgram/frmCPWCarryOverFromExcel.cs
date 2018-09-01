using MyDLLs;
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
using System.IO;

namespace NSCOperationalPlan
{
    public partial class frmCPWCarryOverFromExcel : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        public frmCPWCarryOverFromExcel()
        {
            InitializeComponent();
            ArrangeGrid();
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("JCNo", 80);                //1
            dct.Add("CarryOver", 100);                 //2

            int[] hiddenRows = { };
            int[] readonlyrows = { };

            MyGridUtils.ArrangeDataGrid(dg1, dct, hiddenRows);
        }

        private void ReadFromExcel(string filePath)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            Excel.Range xlRange = xlWorksheet.UsedRange; // get the entire used range


            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

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
                    dg1.Rows.Add(new String[] {
                            xlRange.Cells[i, 1].Value2.ToString(),
                            xlRange.Cells[i, 2].Value2.ToString()
                        });
                }
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

        private void tsbSave_Click(object sender, EventArgs e)
        {
            string path = @"\log_carry_over.txt";
            Dictionary<string, dynamic> dc = new Dictionary<string, dynamic>();
            double CarryOver = 0.0;
            for (int dr = 0; dr < dg1.RowCount; dr++)
            {
                dc.Clear();
                dc.Add("jcno", dg1.Rows[dr].Cells["JCNo"].Value.ToString());

                CarryOver = Double.Parse(string.IsNullOrEmpty(dg1.Rows[dr].Cells["CarryOver"].Value.ToString()) ? "0" : dg1.Rows[dr].Cells["CarryOver"].Value.ToString(),
                                NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands);
                dc.Add("carryover", CarryOver);
                dc.Add("cwpyear", OPGlobals.currentYear);
                try
                {
                    DataTable tb = db.GetResultTableByStoredProcedures("sp_update_cwp_carryover", dc);
                    File.AppendAllText(path, tb.Rows[0][0].ToString() + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    File.AppendAllText(path, "Error in " + dg1.Rows[dr].Cells["JCNo"].Value.ToString() + " - " + CarryOver.ToString() + ex.Message + Environment.NewLine);
                }

            }

            MessageBox.Show("Capital Work Project has been imported successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
