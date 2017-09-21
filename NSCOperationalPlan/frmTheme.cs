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
    public partial class frmTheme : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        bool newflag = true; 

        public frmTheme()
        {
            
            InitializeComponent();
            ArrangeDataGridView();
            FillGrid();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = true;
            cd.SolidColorOnly = true;
            if (cd.ShowDialog() == DialogResult.OK) 
            {
                txtThemeColor.BackColor = cd.Color;
                txtThemeColor.Text = string.Format("#{0:X2}{1:X2}{2:X2}", cd.Color.R,cd.Color.G, cd.Color.B);
            }
            else
            {
                return;
            }

        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            //Application.OpenForms["frmdashboard"].Visible = true;
            
            this.Dispose();
        }

        private void FillGrid()
        {
            string strsql = "SELECT theme.* FROM theme ORDER BY id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            dgv1.Rows.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {

                    dgv1.Rows.Add(new String[] { row["id"].ToString(),
                              row["theme_short"].ToString(),
                              row["theme_description"].ToString() + Environment.NewLine,
                              row["theme_color"].ToString()
                    });
                }
                MyDLLs.MyGridUtils.ColorDataGrid(dgv1, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmTheme_Load(object sender, EventArgs e)
        {

            this.ControlBox = false;
            dgv1.CurrentCell = null;
            //Application.OpenForms["frmdashboard"].Visible = false;
        }

        private bool InsertTheme(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            bool result = false;
            //Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //strdct.Add("ThemeID", txtThemeCode.Text.ToString());
            //strdct.Add("ThemeShort", txtThemeShort.Text.ToString());
            //strdct.Add("ThemeDescription", txtTheme.Text.ToString());
            //strdct.Add("ThemeColor", txtThemeColor.Text.ToString());

            string query = @"INSERT INTO theme (id, theme_short, theme_description, theme_color)"
                + " VALUES (@ThemeID , @ThemeShort, @ThemeDescription, @ThemeColor)";
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
        private bool UpdateTheme(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            bool result = false;
            //Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //strdct.Add("ThemeID", txtThemeCode.Text.ToString());
            //strdct.Add("ThemeShort", txtThemeShort.Text.ToString());
            //strdct.Add("ThemeDescription", txtTheme.Text.ToString());
            //strdct.Add("ThemeColor", txtThemeColor.Text.ToString());

            string query = @"UPDATE theme SET theme_short = @ThemeShort, theme_description = @ThemeDescription,"
                + " theme_color = @ThemeColor WHERE id = @ThemeID";
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
                Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

                strdct.Add("ThemeID", txtThemeCode.Text.ToString());
                strdct.Add("ThemeShort", txtThemeShort.Text.ToString());
                strdct.Add("ThemeDescription", txtTheme.Text.ToString());
                strdct.Add("ThemeColor", txtThemeColor.Text.ToString());

                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

                string strsql = "SELECT theme.* FROM theme WHERE id = '" + txtThemeCode.Text + "';";
                DataTable tb = db.GetDataTable(conn, strsql);   // .GetDataTable(strsql);
                if (tb.Rows.Count > 0) { newflag = false; }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (newflag) {InsertTheme(conn, trans, strdct); } else { UpdateTheme(conn, trans, strdct); }
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
            txtThemeCode.Text = "";
            txtThemeShort.Text = "";
            txtTheme.Text = "";

            txtThemeColor.BackColor = Color.White;
            txtThemeColor.Text = "";
            txtThemeCode.Focus();
            dgv1.CurrentCell = null;

        }

        private void PopulateDataFromGrid()
        {
            try
            {
                txtThemeCode.Text = dgv1.CurrentRow.Cells[0].Value.ToString();


                txtThemeShort.Text = dgv1.CurrentRow.Cells[1].Value.ToString();
                txtTheme.Text = dgv1.CurrentRow.Cells[2].Value.ToString().TrimEnd();
                txtThemeColor.Text = dgv1.CurrentRow.Cells[3].Value.ToString();
                txtThemeColor.BackColor = ColorTranslator.FromHtml(txtThemeColor.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            newflag = false;
            PopulateDataFromGrid();
            txtThemeCode.Enabled = false;
            tsbEdit.Enabled = false;
            tsbNew.Enabled = true;
            txtThemeShort.Focus();
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            newflag = true;

            txtThemeCode.Enabled = true;
            tsbEdit.Enabled = true;
            tsbNew.Enabled = false;
            ClearText();
            txtThemeCode.Focus();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintTheme();
            //frmPrint frmprint = new frmPrint();

            //Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            //DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //string strsql = "SELECT theme.* FROM theme ORDER BY id;";
            //DataTable tb = db.GetDataTable(conn, strsql);

            //frmprint.dataTable = tb;
            //frmprint.reportName = @"rptTheme.rdlc";

            //frmprint.Show();
        }

        private void txtThemeColor_Enter(object sender, EventArgs e)
        {
            btnColor_Click(sender, e);
            txtThemeShort.Focus();
        }

        private void ArrangeDataGridView()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("ID", 90);
            dct.Add("Theme Short", 250);
            dct.Add("Theme", 570);
            dct.Add("ThemeColor", 0);

            int[] hiddenRows = {3};

            MyDLLs.MyGridUtils.ArrangeDataGrid(dgv1, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            dgv1.ReadOnly = true;
        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }
    }
}
