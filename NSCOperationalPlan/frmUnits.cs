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
    public partial class frmUnits : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        Units mUnits;

        bool newflag = true; 

        public frmUnits()
        {
            
            InitializeComponent();
            ArrangeDataGridView();
            FillGrid();
        }

        private void ArrangeDataGridView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("Code", 50);
            dct.Add("Short", 100);
            dct.Add("Description", 230);

            int[] hiddenRows = { };

            MyDLLs.MyGridUtils.ArrangeDataGrid(dgv1, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            dgv1.ReadOnly = true;
        }

        private void FillGrid()
        {
            string strsql = "SELECT * FROM kpi_units ORDER BY CAST(kpi_unit_id AS UNSIGNED);";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv1.Rows.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {

                    dgv1.Rows.Add(new String[] { row["kpi_unit_id"].ToString(),
                              row["kpi_unit_short"].ToString(),
                              row["kpi_unit"].ToString()
                    });
                }
                MyDLLs.MyGridUtils.ColorDataGrid(dgv1, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PopulateDataFromGrid()
        {
            try
            {
                txtCode.Text = mUnits.Code;
                txtShortDescription.Text = mUnits.ShortDescription;
                txtDescription.Text = mUnits.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            //Application.OpenForms["frmdashboard"].Visible = true;
            
            this.Dispose();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            newflag = false;
            mUnits.ShortDescription = txtShortDescription.Text;
            mUnits.Description = txtDescription.Text;
            if (!mUnits.IsExist(mUnits.Code))
            {
                newflag = true;
            }

            try
            {
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (newflag) { mUnits.InsertUnit(db, conn, trans); } else { mUnits.UpdateUnit(db, conn, trans); }
                        trans.Commit();
                        MessageBox.Show("Unit has been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                conn.Close();
                FillGrid();
                ClearText();
            } catch (Exception ex1)
            {
                MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex1.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearText()
        {
            txtCode.Text = "";
            txtShortDescription.Text = "";
            txtDescription.Text = "";

            txtCode.Focus();

            dgv1.CurrentCell = null;

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                newflag = false;
                mUnits = new Units(dgv1.CurrentRow.Cells[0].Value.ToString(),
                    dgv1.CurrentRow.Cells[2].Value.ToString().TrimEnd(),
                    dgv1.CurrentRow.Cells[1].Value.ToString());

                PopulateDataFromGrid();
                tsbEdit.Enabled = false;
                tsbNew.Enabled = true;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            newflag = true;
            ClearText();

            mUnits = new Units();
            mUnits.Code = mUnits.GetNextUnitCode().ToString();

            txtCode.Text = mUnits.Code;

            tsbEdit.Enabled = true;
            tsbNew.Enabled = false;

            txtShortDescription.Focus();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            //clsReports.PrintTheme();
        }

        

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }
    }
}
