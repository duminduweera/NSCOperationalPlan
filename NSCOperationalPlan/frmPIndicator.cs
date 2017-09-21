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
    public partial class frmPIndicator : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        bool newflag = true; 

        public frmPIndicator()
        {
            InitializeComponent();

            cboYear.Items.Clear();
            cboYear.Items.Add(OPGlobals.currentYear);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("PI", 30);
            dct.Add("Performance Indicator", 620);
            dct.Add("Year", 80);

            MyDLLs.MyGridUtils.ArrangeDataGrid(grdPI,dct,Color.LightGray, Color.LightSteelBlue);
            grdPI.ReadOnly = true;
        }

        private void FillGrid()
        {
            string strsql = "SELECT performance_indicator.* FROM performance_indicator ORDER BY description;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            grdPI.Rows.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    grdPI.Rows.Add(new String[] { row["id"].ToString(),
                              row["description"].ToString() + Environment.NewLine,
                              row["pi_year"].ToString() == "00/00" ? "" : row["pi_year"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool InsertIndicator(DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("Description", txtPI.Text.ToString());
            strdct.Add("PIYear", cboYear.SelectedIndex >= 0 ? cboYear.SelectedItem.ToString() : "00/00");

            string query = @"INSERT INTO performance_indicator (description, pi_year)"
                + " VALUES (@Description, @PIYear)";
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
        private bool UpdateIndicator(DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();


            strdct.Add("ID", int.Parse(txtPIID.Text.ToString()));
            strdct.Add("Description", txtPI.Text.ToString());
            strdct.Add("PIYear", cboYear.SelectedIndex >= 0 ? cboYear.SelectedItem.ToString() : "00/00");


            string query = @"UPDATE performance_indicator SET description = @Description, pi_year = @PIYear"
                + " WHERE id = @ID";
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
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                if (string.IsNullOrEmpty(txtPIID.Text))
                {
                    newflag = true;
                }
                else
                {
                    string strsql = "SELECT performance_indicator.* FROM performance_indicator WHERE id = " + int.Parse(txtPIID.Text.ToString()) + ";";
                    DataTable tb = db.GetDataTable(conn, strsql);
                    if (tb.Rows.Count > 0) { newflag = false; }
                }
                
                if(conn.State == ConnectionState.Closed) { conn.Open(); }

                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (newflag) { InsertIndicator(conn, trans); } else { UpdateIndicator(conn, trans); }
                        trans.Commit();
                        MessageBox.Show("Theme has been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void ClearText()
        {
            txtPIID.Text = "";
            txtPI.Text = "";
            grdPI.CurrentCell = null;

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            newflag = false;
            PopulateDataFromList();
            tsbEdit.Enabled = false;
            tsbNew.Enabled = true;
            txtPI.Focus();
        }

        private void PopulateDataFromList()
        {
            try
            {
                txtPIID.Text = grdPI.CurrentRow.Cells[0].Value.ToString();  // lstPI.SelectedItems[0].Text;
                txtPI.Text = grdPI.CurrentRow.Cells[1].Value.ToString().TrimEnd();  // lstPI.SelectedItems[0].SubItems[1].Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            newflag = true;
            tsbEdit.Enabled = true;
            tsbNew.Enabled = false;
            ClearText();
            txtPI.Focus();
        }

        private void lstPI_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void frmPIndicator_Load(object sender, EventArgs e)
        {
            ArrangeGrid();
            FillGrid();
            grdPI.CurrentCell = null;
        }

        private void grdPI_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
