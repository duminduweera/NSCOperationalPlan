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
    public partial class frmStatus : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        bool newflag = true; 

        public frmStatus()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            newflag = true;
            ClearText();
            txtStatusCode.Enabled = true;
            tsbEdit.Enabled = true;
            tsbNew.Enabled = false;
            //ChangeLabelColor(true);
            txtStatusCode.Focus();

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            newflag = false;
            PopulateDataFromList();
            txtStatusCode.Enabled = false;
            tsbEdit.Enabled = false;
            tsbNew.Enabled = true;
            txtStatusShort.Focus();

        }

        private void ClearText()
        {
            txtStatusCode.Text = "";
            txtStatusShort.Text = "";
            txtStatus.Text = "";
            txtColor.Text = "";
            grdStatus.CurrentCell = null;
        }
        private void FillGrid()
        {
            string strsql = "SELECT status.* FROM status ORDER BY id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            grdStatus.Rows.Clear();

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    grdStatus.Rows.Add(new String[] { row["id"].ToString(),
                              row["status_short"].ToString(),
                              row["status_description"].ToString() + Environment.NewLine,
                              row["status_color"].ToString()
                    });
                    MyDLLs.MyGridUtils.ColorDataGrid(grdStatus, 1, 3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PopulateDataFromList()
        {
            try
            {
                txtStatusCode.Text = grdStatus.CurrentRow.Cells[0].Value.ToString(); //  lstStatus.SelectedItems[0].Text;
                txtStatusShort.Text = grdStatus.CurrentRow.Cells[1].Value.ToString();  //lstStatus.SelectedItems[0].SubItems[1].Text;
                txtStatus.Text = grdStatus.CurrentRow.Cells[2].Value.ToString();  //lstStatus.SelectedItems[0].SubItems[2].Text;
                txtColor.Text = grdStatus.CurrentRow.Cells[3].Value.ToString();
                txtColor.BackColor = ColorTranslator.FromHtml(txtColor.Text);
                
                //txtR.Text = lstStatus.SelectedItems[0].SubItems[3].Text;
                //txtG.Text = lstStatus.SelectedItems[0].SubItems[4].Text;
                //txtB.Text = lstStatus.SelectedItems[0].SubItems[5].Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private bool InsertStatus(DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            Dictionary<string, int> intdct = new Dictionary<string, int>();

            strdct.Add("StatusID", txtStatusCode.Text.ToString());
            strdct.Add("StatusShort", txtStatusShort.Text.ToString());
            strdct.Add("StatusDescription", txtStatus.Text.ToString());
            strdct.Add("StatusColor", txtColor.Text.ToString());  //"#" + int.Parse(txtR.Text.ToString()).ToString("X2") + int.Parse(txtG.Text.ToString()).ToString("X2") + int.Parse(txtB.Text.ToString()).ToString("X2"));
            //intdct.Add("StatusColor_R", int.Parse(txtR.Text.ToString()));
            //intdct.Add("StatusColor_G", int.Parse(txtG.Text.ToString()));
            //intdct.Add("StatusColor_B", int.Parse(txtB.Text.ToString()));


            string query = @"INSERT INTO status (id, status_short, status_description, status_color)"
                + " VALUES (@StatusID , @StatusShort, @StatusDescription, @StatusColor)";

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
        private bool UpdateStatus(DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("StatusID", txtStatusCode.Text.ToString());
            strdct.Add("StatusShort", txtStatusShort.Text.ToString());
            strdct.Add("StatusDescription", txtStatus.Text.ToString());
            strdct.Add("StatusColor", txtColor.Text.ToString());  // "#" + int.Parse(txtR.Text.ToString()).ToString("X2") + int.Parse(txtG.Text.ToString()).ToString("X2") + int.Parse(txtB.Text.ToString()).ToString("X2"));
            //intdct.Add("StatusColor_R", int.Parse(txtR.Text.ToString()));
            //intdct.Add("StatusColor_G", int.Parse(txtG.Text.ToString()));
            //intdct.Add("StatusColor_B", int.Parse(txtB.Text.ToString()));

            //string query = "UPDATE isr SET isr_status = '" + status + "' WHERE isr_id = '" + id + "'";

            string query = @"UPDATE status SET status_short = @StatusShort, status_description = @StatusDescription, status_color = @StatusColor"
                + " WHERE id = @StatusID";

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

                string strsql = "SELECT status.* FROM status WHERE id = '" + txtStatusCode.Text + "';";
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = db.GetDataTable(conn, strsql);
                if (tb.Rows.Count > 0) { newflag = false; }

                if(conn.State == ConnectionState.Closed) { conn.Open(); }

                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (newflag) { InsertStatus(conn, trans); } else { UpdateStatus(conn, trans); }
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

        private void btnColor_Click(object sender, EventArgs e)
        {

            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.SolidColorOnly = true;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                txtColor.BackColor = cd.Color;
                txtColor.Text = string.Format("#{0:X2}{1:X2}{2:X2}", cd.Color.R, cd.Color.G, cd.Color.B);
            }
            else
            {
                return;
            }
        }


        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintStatus();
        }

        private void txtColor_Enter(object sender, EventArgs e)
        {
            btnColor_Click(sender, e);
            txtStatusShort.Focus();
        }
        private void ArrangeGrid()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("ID", 0);
            dct.Add("Status Short", 120);
            dct.Add("Status", 625);
            dct.Add("Color", 0);

            int[] hiddenRows = { 0,3 };

            MyDLLs.MyGridUtils.ArrangeDataGrid(grdStatus, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            grdStatus.ReadOnly = true;

        }

        private void txtColor_TextChanged(object sender, EventArgs e)
        {
            txtColor.BackColor = ColorTranslator.FromHtml(txtColor.Text);
        }

        private void grdStatus_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            ArrangeGrid();
            FillGrid();
            grdStatus.CurrentCell = null;
        }

    }
}
