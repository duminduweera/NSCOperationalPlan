using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using MyDLLs;


namespace NSCOperationalPlan
{
    public partial class frmKPI : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        static string SELECTED_TAB;
        static int LAST_KPI_ID = 0;
        List<DataGridView> dgvList;

        public frmKPI()
        {
            InitializeComponent();
            dgvList = new List<DataGridView>() { dgv01, dgv02, dgv03 };

        }

        private void ArrangeGrids()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("#", 50);               //0                
            dct.Add("ID", 0);               //1
            dct.Add("Description", 500);    //2
            dct.Add("PrefixID", 0);         //3
            dct.Add("Prefix", 130);          //4
            dct.Add("Value", 55);           //5
            dct.Add("UnitID", 0);           //6
            dct.Add("Unit", 45);            //7
            dct.Add("ManagerID", 0);        //8
            dct.Add("Manager", 230);        //9
            dct.Add("Est.Year", 60);        //10
            dct.Add("SP_ID", 0);            //11
            dct.Add("Service Plan", 230);   //12

            dct.Add("BDescription", 0);    //2
            dct.Add("BPrefixID", 0);         //3
            dct.Add("BValue", 0);           //5
            dct.Add("BUnitID", 0);           //6
            dct.Add("BManagerID", 0);        //8
            dct.Add("BSP_ID", 0);            //11


            int[] hiddenRows = { 1, 3, 6, 8, 11, 13, 14, 15, 16, 17, 18 };
            //int[] hiddenRows = { };

            for (int i = 0; i < dgvList.Count; i++)
            {
                MyGridUtils.ArrangeDataGrid(dgvList[i], dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
                dgvList[i].RowTemplate.MinimumHeight = 28;
                dgvList[i].ReadOnly = true;
            }

        }

        private void AddToGrid(DataGridView dgv)
        {
            if (String.IsNullOrEmpty(txtKPIID.Text))
            {
                try
                {
                    {
                        dgv.Rows.Add(new String[] {(dgv.RowCount+1).ToString(), "", txtEffiDes.Text,
                        cboEffiPrefix.SelectedValue.ToString(),
                        cboEffiPrefix.Text,
                        txtEffiValue.Text,
                        cboEffiUnits.SelectedValue.ToString(),
                        cboEffiUnits.Text,
                        cboManager.SelectedValue.ToString(),
                        cboManager.Text,
                        txtEffiYear.Text,
                        cboServicePlan.SelectedValue.ToString(),
                        cboServicePlan.Text
                    });
                    }
                    MyGridUtils.ColorDataGrid(dgv);
                    dgv.CurrentCell = null;
                    ClearScreenData("NEW");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                dgv.SelectedRows[0].Cells["Description"].Value = txtEffiDes.Text;
                dgv.SelectedRows[0].Cells["PrefixID"].Value = cboEffiPrefix.SelectedValue.ToString();
                dgv.SelectedRows[0].Cells["Prefix"].Value = cboEffiPrefix.Text;
                dgv.SelectedRows[0].Cells["Value"].Value = txtEffiValue.Text;

                dgv.SelectedRows[0].Cells["UnitID"].Value = cboEffiUnits.SelectedValue.ToString();
                dgv.SelectedRows[0].Cells["Unit"].Value = cboEffiUnits.Text;
                dgv.SelectedRows[0].Cells["SP_ID"].Value = cboServicePlan.SelectedValue.ToString();
                dgv.SelectedRows[0].Cells["Service Plan"].Value = cboServicePlan.Text;

                txtKPIID.Text = "";
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");
            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
            LoadManagers(cboDirector.SelectedValue.ToString());
        }
        private void LoadManagers(string directorcode)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "';");

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";

        }
        private void LoadEffiPrefix()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM kpi_prefix;");
            cboEffiPrefix.DataSource = tb;
            cboEffiPrefix.DisplayMember = "kpi_prefix";
            cboEffiPrefix.ValueMember = "kpi_prefix_id";
        }
        private void LoadEffiUnits()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT* FROM kpi_units;");
            cboEffiUnits.DataSource = tb;
            cboEffiUnits.DisplayMember = "kpi_unit";
            cboEffiUnits.ValueMember = "kpi_unit_id";
        }
        private void LoadServicePlan()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM service_plan;");

            //DataRow dr = tb.NewRow();
            //dr["id"] = "000";
            //dr["service_plan"] = "-NONE-";
            //tb.Rows.InsertAt(dr, 0);

            cboServicePlan.DataSource = tb;
            cboServicePlan.DisplayMember = "service_plan";
            cboServicePlan.ValueMember = "id";
        }

        private void frmKPI_Load(object sender, EventArgs e)
        {

            LoadDirectors();
            LoadEffiPrefix();
            LoadEffiUnits();

            LoadServicePlan();

            txtEffiYear.Text = OPGlobals.currentYear;

            ArrangeGrids();

            for(int i =0; i<dgvList.Count; i++) { dgvList[i].CurrentCell = null; }

            tabkpi.SelectedTab = tab02;
            tabkpi.SelectedTab = tab01;

            ClearScreenData("NEW");

            //mSelectedKPIForEdit.Clear();

        }

        private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDirector.SelectedValue.GetType().Name == "String")
                {
                    LoadManagers(cboDirector.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            //======== NEW KPI===========
            //KeyPerformanceIndex kpi = new KeyPerformanceIndex();
            //int kpiid = kpi.getNextKPIIndex();
            //LAST_KPI_ID = kpi.getNextKPIIndex();

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            using (DbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    if (SaveDataToDatabase("001", dgv01, conn, trans) == false) { throw new Exception("ERROR Saving Efficiency Measures"); }
                    if (SaveDataToDatabase("002", dgv02, conn, trans) == false) { throw new Exception("ERROR Saving Effectiveness Measures"); }
                    if (SaveDataToDatabase("003", dgv03, conn, trans) == false) { throw new Exception("ERROR Saving Effectiveness Measures"); }

                    trans.Commit();
                    MessageBox.Show("Key Performance Measures have been saved/updated successfully", "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKPIFromDatabase();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Data NOT Saved ..." + Environment.NewLine + ex.Message.ToString(), "OP ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            conn.Close();
            RefreshGridView();
        }

        private void ClearScreenData(string flag)
        {
            txtKPIID.Text = "";
            cboServicePlan.SelectedIndex = 0;
            txtEffiDes.Text = "";
            cboEffiPrefix.SelectedIndex = 0;
            txtEffiValue.Text = "";
            cboEffiUnits.SelectedIndex = 0;

            if (flag == "NEW")
            {
                tsbEdit.Enabled = false;
                tsbNew.Enabled = true;

                if (OPGlobals.CurrentUser.Permission == UserRights.Administrator)
                {
                    tsbDelete.Enabled = false;
                }
            } else
            {
                tsbEdit.Enabled = true;
                tsbNew.Enabled = false;

                if (OPGlobals.CurrentUser.Permission == UserRights.Administrator)
                {
                    tsbDelete.Enabled = true;
                }

            }
        }

        private void RefreshGridView()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int i = 0; i < dgvList[x].RowCount; i++)
                {
                    if (!MyGridUtils.IsColumnDataChanged(dgvList[x].Rows[i],
                        new List<String>() { "Description", "PrefixID", "Value", "UnitID", "ManagerID", "SP_ID" },
                        new List<String>() { "BDescription", "BPrefixID", "BValue", "BUnitID", "BManagerID", "BSP_ID" }))
                    {
                        continue;
                    }
                    dgvList[x].Rows[i].Cells["BDescription"].Value = dgvList[x].Rows[i].Cells["Description"].Value;
                    dgvList[x].Rows[i].Cells["BPrefixID"].Value = dgvList[x].Rows[i].Cells["PrefixID"].Value;
                    dgvList[x].Rows[i].Cells["BValue"].Value = dgvList[x].Rows[i].Cells["Value"].Value;
                    dgvList[x].Rows[i].Cells["BUnitID"].Value = dgvList[x].Rows[i].Cells["UnitID"].Value;
                    dgvList[x].Rows[i].Cells["BManagerID"].Value = dgvList[x].Rows[i].Cells["ManagerID"].Value;
                    dgvList[x].Rows[i].Cells["BSP_ID"].Value = dgvList[x].Rows[i].Cells["SP_ID"].Value;
                }
            }
        }

        private bool SaveDataToDatabase(string kpm_id, DataGridView dgv, DbConnection con, DbTransaction trans)
        {
            bool retval = true;
            KeyPerformanceIndex kpi;
            LAST_KPI_ID = KeyPerformanceIndex.getNextKPIIndex();
            int mKPIId;
            bool mNewFlag = false;
            try
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (!MyGridUtils.IsColumnDataChanged(dgv.Rows[i], 
                        new List<String>() { "Description", "PrefixID", "Value", "UnitID", "ManagerID", "SP_ID" }, 
                        new List<String>() { "BDescription", "BPrefixID", "BValue", "BUnitID", "BManagerID", "BSP_ID" }))
                    {
                        continue;
                    }
                    //---- Check for KPI Index in the Grid--
                    //--------- If Empty or Null New KPI otherwise Edit
                    if (String.IsNullOrEmpty(dgv.Rows[i].Cells["ID"].Value.ToString()))
                    {
                        mKPIId = LAST_KPI_ID;
                        LAST_KPI_ID++;
                        mNewFlag = true;
                    } else
                    {
                        mKPIId = int.Parse(dgv.Rows[i].Cells["ID"].Value.ToString());
                        mNewFlag = false;
                    }

                    kpi = new KeyPerformanceIndex();
                    kpi.KPIID = mKPIId;
                    kpi.KPMID = kpm_id;
                    kpi.ManagerID = dgv.Rows[i].Cells["ManagerID"].Value.ToString();
                    kpi.Description = dgv.Rows[i].Cells["Description"].Value.ToString();
                    kpi.Prefix = dgv.Rows[i].Cells["PrefixID"].Value.ToString();
                    kpi.EstimateYear = dgv.Rows[i].Cells["Est.Year"].Value.ToString();
                    kpi.EstimateValue = Convert.ToDouble(dgv.Rows[i].Cells["Value"].Value);
                    kpi.Unit = dgv.Rows[i].Cells["UnitID"].Value.ToString();
                    kpi.ServicePlanID = dgv.Rows[i].Cells["SP_ID"].Value == null ? "000" : dgv.Rows[i].Cells["SP_ID"].Value.ToString();
                    if (mNewFlag)
                    {
                        kpi.InsertKPI(db, con, trans);
                        kpi.InsertKPIEstimate(db, con, trans);
                    }
                    else
                    {
                        kpi.UpdateKPI(db, con, trans);
                        kpi.UpdateKPIEstimate(db, con, trans);
                    }
                }
            } catch (Exception ex)
            {
                retval = false;
            }
            return retval;
        }
        private bool InsertEffectiveness(KeyPerformanceIndex kpi, DbConnection con, DbTransaction trans)
        {
            bool retval = true;
            try
            {
                for (int i = 0; i < dgv02.RowCount; i++)
                {
                    if (String.IsNullOrEmpty(dgv02.Rows[i].Cells[1].Value.ToString()))
                    {
                        kpi.KPIID = LAST_KPI_ID;
                        kpi.KPMID = "002";
                        kpi.ManagerID = dgv02.Rows[i].Cells[8].Value.ToString();
                        kpi.Description = dgv02.Rows[i].Cells[2].Value.ToString();
                        kpi.Prefix = dgv02.Rows[i].Cells[3].Value.ToString();
                        kpi.EstimateYear = dgv02.Rows[i].Cells[10].Value.ToString();
                        //double x = Convert.ToDouble(dgv01.Rows[i].Cells[5].Value);

                        kpi.EstimateValue = Convert.ToDouble(dgv02.Rows[i].Cells[5].Value);
                        kpi.Unit = dgv02.Rows[i].Cells[6].Value.ToString();

                        kpi.ServicePlanID = dgv02.Rows[i].Cells[11].Value.ToString();

                        kpi.InsertKPI(db, con, trans);
                        kpi.InsertKPIEstimate(db, con, trans);

                        LAST_KPI_ID++;
                    }
                }
            }
            catch (Exception ex)
            {
                retval = false;
            }
            return retval;
        }
        private bool InsertWorkload(KeyPerformanceIndex kpi, DbConnection con, DbTransaction trans)
        {
            bool retval = true;
            try
            {
                for (int i = 0; i < dgv03.RowCount; i++)
                {
                    if (String.IsNullOrEmpty(dgv03.Rows[i].Cells[1].Value.ToString()))
                    {
                        kpi.KPIID = LAST_KPI_ID;
                        kpi.KPMID = "003";
                        kpi.ManagerID = dgv03.Rows[i].Cells[8].Value.ToString();
                        kpi.Description = dgv03.Rows[i].Cells[2].Value.ToString();
                        kpi.Prefix = dgv03.Rows[i].Cells[3].Value.ToString();
                        kpi.EstimateYear = dgv03.Rows[i].Cells[10].Value.ToString();
                        //double x = Convert.ToDouble(dgv01.Rows[i].Cells[5].Value);

                        kpi.EstimateValue = Convert.ToDouble(dgv03.Rows[i].Cells[5].Value);
                        kpi.Unit = dgv03.Rows[i].Cells[6].Value.ToString();

                        kpi.ServicePlanID = dgv01.Rows[i].Cells[11].Value.ToString();

                        kpi.InsertKPI(db, con, trans);
                        kpi.InsertKPIEstimate(db, con, trans);

                        LAST_KPI_ID++;
                    }
                }
            }
            catch (Exception ex)
            {
                retval = false;
            }
            return retval;
        }

        private void cboManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadKPIFromDatabase();
        }

        private void LoadKPIFromDatabase()
        {
            LoadTableFromDatabase(dgv01, "001");
            LoadTableFromDatabase(dgv02, "002");
            LoadTableFromDatabase(dgv03, "003");
        }
        private void LoadTableFromDatabase(DataGridView dgv, string kpitype)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            dgv.Rows.Clear();
            dgv.Refresh();
            try
            {
                DataTable tb = KeyPerformanceIndex.GetKPITable(db, conn, cboManager.SelectedValue.ToString(), OPGlobals.currentYear, kpitype);
                foreach (DataRow row in tb.Rows)
                {
                    dgv.Rows.Add(new String[] {(dgv.RowCount+1).ToString(),
                        row["kpi_id"].ToString(),
                        row["efficiency_description"].ToString(),
                        row["kpi_prefix_id"].ToString(),
                        row["kpi_prefix"].ToString(),
                        row["kpi_estimate"].ToString(),
                        row["unit_id"].ToString(),
                        row["kpi_unit_short"].ToString(),
                        row["manager_id"].ToString(),
                        row["manager_description"].ToString(),
                        row["kpi_estimate_year"].ToString(),
                        row["service_plan_id"].ToString(),
                        row["service_plan"].ToString(),
                        row["efficiency_description"].ToString(),
                        row["kpi_prefix_id"].ToString(),
                        row["kpi_estimate"].ToString(),
                        row["unit_id"].ToString(),
                        row["manager_id"].ToString(),
                        row["service_plan_id"].ToString()
                });
                }
                dgv.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadDataFromTable(DataGridViewRow dgvRow, string kpitype)
        {
            txtKPIID.Text = dgvRow.Cells["ID"].Value.ToString();

            txtEffiDes.Text = dgvRow.Cells["Description"].Value.ToString();
            txtEffiValue.Text = dgvRow.Cells["Value"].Value.ToString();
            try { cboEffiPrefix.SelectedValue = dgvRow.Cells["PrefixID"].Value.ToString(); } catch { }
            try { cboEffiUnits.SelectedValue = dgvRow.Cells["UnitID"].Value.ToString(); } catch { }
            try { cboServicePlan.SelectedValue = dgvRow.Cells["SP_ID"].Value.ToString(); } catch { }

        }

        private void tabkpi_Selected(object sender, TabControlEventArgs e)
        {
            //Bitmap b;
            SELECTED_TAB = tabkpi.SelectedTab.Name;
            if (tabkpi.SelectedTab == tabkpi.TabPages["tab01"])//your specific tabname
            {
                //b = new Bitmap(@"C:\myBitmap.jpg");
                btnEffiAdd.Image = Properties.Resources.Efficiency;
                lblkpi.Image = Properties.Resources.Efficiency;
                btnEffiAdd.Text = "Add to Efficiency Table";
                lblkpi.Text = "Efficiency Measurement";
            }
            else if (tabkpi.SelectedTab == tabkpi.TabPages["tab02"])
            {
                //b = new Bitmap(@"C:\myBitmap.jpg");
                btnEffiAdd.Image = Properties.Resources.effectiveness;
                btnEffiAdd.Text = "Add to Effectiveness Table";
                lblkpi.Image = Properties.Resources.effectiveness;
                lblkpi.Text = "Effectiveness Measurement";
            }
            else
            {
                //b = new Bitmap(@"C:\myBitmap.jpg");
                btnEffiAdd.Image = Properties.Resources.workload;
                btnEffiAdd.Text = "Add to Workload Table";
                lblkpi.Image = Properties.Resources.workload;
                lblkpi.Text = "Workload Measurement";
            }
        }

        private void btnEffiAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEffiDes.Text))
            {
                MessageBox.Show("Description Cannot be Empty", "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEffiDes.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtEffiValue.Text))
            {
                MessageBox.Show("Value Cannot be Empty", "OPERATION PLAN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEffiValue.Focus();
                return;
            }

            switch (SELECTED_TAB)
            {
                case "tab01":
                    AddToGrid(dgv01);
                    //AddToGrd01();
                    break;
                case "tab02":
                    AddToGrid(dgv02);
                    //AddToGrd02();
                    break;
                case "tab03":
                    AddToGrid(dgv03);
                    //AddToGrd03();
                    break;
            }
            ClearScreenData("NEW");
        }

        private void tabkpi_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font f;
            Brush backBrush;
            Brush foreBrush;
            if(e.Index == tabkpi.SelectedIndex)
            {
                f = new Font(e.Font, FontStyle.Bold);
                backBrush = new System.Drawing.SolidBrush(Color.Green);
                foreBrush = Brushes.White;
            }
            else
            {
                f = e.Font;
                backBrush = new SolidBrush(e.BackColor);
                foreBrush = new SolidBrush(e.ForeColor);
            }

            string tabName = this.tabkpi.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

        }

        private void dgv01_DoubleClick(object sender, EventArgs e)
        {
            ClearScreenData("EDIT");
            LoadDataFromTable(dgv01.SelectedRows[0], "001");
        }
        private void dgv02_DoubleClick(object sender, EventArgs e)
        {
            ClearScreenData("EDIT");
            LoadDataFromTable(dgv02.SelectedRows[0], "002");
        }
        private void dgv03_DoubleClick(object sender, EventArgs e)
        {
            ClearScreenData("EDIT");
            LoadDataFromTable(dgv03.SelectedRows[0], "003");
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {

        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string msg;
            try
            {
                if (MessageBox.Show("THIS WILL DLETE KPI ID =" + txtKPIID.Text + Environment.NewLine + " DO YOU WANT TO CONTINUE", "OPERATION PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    KeyPerformanceIndex kpm = new KeyPerformanceIndex();
                    kpm.DeleteKPM(txtKPIID.Text);
                    msg = "KPM has been DELETED successfully";
                    btnEffiAdd_Click(this, e);
                } else
                {
                    msg = "Operation has beed cancelled by the user";
                }
            }
            catch (Exception ex)
            {
                msg = "Data NOT DELETED ..." + Environment.NewLine + ex.Message.ToString();
            }
            MessageBox.Show(msg, "OP MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
    
}
