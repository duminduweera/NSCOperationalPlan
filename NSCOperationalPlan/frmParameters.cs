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

namespace NSCOperationalPlan
{
    public partial class frmParameters : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        bool newflag = true;

        public frmParameters()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmParameters_Load(object sender, EventArgs e)
        {
            cboCurrentMonth.DataSource = Enum.GetNames(typeof(Months));
            ArrangeGridParam();
            tsbNew_Click(sender, e);
        }

        private void ArrangeGridParam()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 50);
            dct.Add("Year", 100);
            dct.Add("Month", 100);

            int[] hiddenRows = { };

            MyGridUtils.ArrangeDataGrid(grd1, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            grd1.ReadOnly = true;

        }
        private void FillGridParam()
        {
            string strsql = "SELECT * FROM tbl_params order by id;";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            grd1.Rows.Clear();
            int id = 0;
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    id = int.Parse(row["id"].ToString());
                    grd1.Rows.Add(new String[] { row["id"].ToString(),
                              row["current_year"].ToString(),
                              Enum.GetName(typeof(Months),row["current_month"])
                    });
                }
                MyGridUtils.ColorDataGrid(grd1, 0, 11);
                grd1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            id++;
            txtID.Text = id.ToString();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {

            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("ID", txtID.Text);
            strdct.Add("ProgramYear", txtCurrentYear.Text);
            strdct.Add("ProgramMonth", (int)Enum.Parse(typeof(Months), cboCurrentMonth.SelectedValue.ToString(), true));
            strdct.Add("User", OPGlobals.CurrentUser.LoginName);

            try
            {
                newflag = true;
                string strsql = @"SELECT COUNT(*) as noofrecs FROM tbl_params"
                    + " WHERE (current_year) ='" + strdct["ProgramYear"] + "' AND current_month=" + strdct["ProgramMonth"] + ";";
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = db.GetDataTable(conn, strsql);
                if (int.Parse(tb.Rows[0][0].ToString()) > 0)
                {
                    MessageBox.Show("Delivery Year and Month Already Exist, Please try with different Year/Month", "OP PLAN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        InsertParams(conn, trans, strdct);
                        trans.Commit();
                        conn.Close();
                        string msg = "Data has been saved successfully, Please Close application to make that effect" + Environment.NewLine + " Do you want to close this Now?";
                        //if (MessageBox.Show(msg, "OP MESSAGE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                            Program.CloseApplication(msg,true);
                        //}
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                conn.Close();
                tsbNew_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private bool InsertParams(DbConnection con, DbTransaction trans, Dictionary<string, dynamic> strdct)
        {
            bool result = false;
            string query = @"INSERT INTO tbl_params (id, current_year, current_month, user_name)"
                + " VALUES (@ID , @ProgramYear, @ProgramMonth, @User)";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            FillGridParam();
        }
    }
}
