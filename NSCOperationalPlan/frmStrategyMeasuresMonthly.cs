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
            LoadMeasuresFromDB();
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {

        }

       
            
        private void ArrangeGrid()
        {
            Dictionary<string, int> coloumnDict = new Dictionary<string, int>();
            coloumnDict.Add("Strategy ID", 80);                   
            coloumnDict.Add("Description", 700);         
            coloumnDict.Add("Measure Code", 60);                              
            coloumnDict.Add("Source", 120);   
            coloumnDict.Add("Remark", 500);   
            coloumnDict.Add("RemarkOriginal", 240);
            int[] hiddenRows = {5};
            int[] readonlyrows = {0,1,2,3,5};
            MyDLLs.MyGridUtils.ArrangeDataGrid(dgvStraMeasures, coloumnDict, hiddenRows);
        }

        private void LoadMeasuresFromDB() {
        }
       
    }
}
