using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NSCOperationalPlan
{
    public partial class frmImportStartegyMeasuresfromExcel : Form
    {
        public frmImportStartegyMeasuresfromExcel()
        {
            InitializeComponent();
        }

        private void tsLoad_Click(object sender, EventArgs e)
        {
            
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filePath = openFileDialog1.FileName;
                ArrangeGrid();
                ReadExcelFile(filePath);              
            }
           
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("Strategy ID", 60);                //1
            dct.Add("Director ID", 60);        //2
            dct.Add("Strategy Measure", 700);         //3
            dct.Add("Measure Code", 60);//4
            dct.Add("Year", 60);   //5
            int[] hiddenRows = { };
            int[] readonlyrows = { };

            MyDLLs.MyGridUtils.ArrangeDataGrid(dg1, dct, hiddenRows);
        }

        private void ReadExcelFile(string filePath)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange; // get the entire used range
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!

            pb1.Value = 0;
            pb1.Maximum = rowCount;
            pb1.Visible = true;

            string col1="",col3="",col4="";
            int col2=0;
           
                for (int i = 2; i <= rowCount; i++)
                {
                    try
                    {
                        pb1.Value += 1;

                    col3 = xlRange.Cells[i, 3].Value2.ToString().Trim();
                    col4 = xlRange.Cells[i, 4].Value2.ToString().Trim();
                    col1 = xlRange.Cells[i, 1].Value2.ToString().Trim();                      
                    col2 = int.Parse(xlRange.Cells[i, 2].Value2.ToString().Trim());
                       
                            
                        dg1.Rows.Add(new String[] {
                            col1.Split(' ')[0].ToString(),
                            col2.ToString("D3"),col3,col4,"17/18"
                        });

                    }
                    catch (Exception ex){
                    dg1.Rows.Add(new String[] {
                            col1.Split(' ')[0].ToString(),
                            col2.ToString("D3"),col3,col4,"17/18"
                        });
                    //Console.WriteLine(ex);
                    continue;
                    }      
                        
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

        private void tsSave_Click(object sender, EventArgs e)
        {

        }
    }
}
