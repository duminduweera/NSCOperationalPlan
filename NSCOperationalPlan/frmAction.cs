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
using DataGridviewCalendarColumn;

namespace NSCOperationalPlan
{
    public partial class frmAction : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        bool newflag = true;
        private ListViewItem lastStrategyObjectiveItemChecked;
        private ListViewItem lastStrategyItemChecked;
        //private ListViewItem lastPerformanceIndicatorItemChecked;

        public frmAction()
        {
            InitializeComponent();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ArrangeListView()
        {

            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("ID", 70);
            dct.Add("Objective", 820);
            dct.Add("Theme", 0);
            dct.Add("ThemeColor", 0);

            foreach (KeyValuePair<string, int> pair in dct)
            {
                lstStrategyObjective.Columns.Add(pair.Key, pair.Value);
            }

            dct.Clear();
            dct.Add("ID", 70);
            dct.Add("Stategy", 820);

            foreach (KeyValuePair<string, int> pair in dct)
            {
                lstStrategy.Columns.Add(pair.Key, pair.Value);
            }

        }

        private void FillStrategyObjectiveList()
        {
            string strsql = "Select Distinct strategy_view.theme_id, strategy_view.strategy_objective_id, strategy_view.theme_short, strategy_view.code_for_strategy,"
                + " strategy_view.theme_color, strategy_view.strategy_objective From strategy_view"
                + " Order By strategy_view.theme_id, strategy_view.strategy_objective_id";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //string strsql = "Select * from strategy_view where strategy_rank>0 order by theme_id, strategy_id, strategy_rank;";

            DataTable tb = db.GetDataTable(conn, strsql);

            lstStrategyObjective.Items.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    ListViewItem lvi = new ListViewItem(row["strategy_objective_id"].ToString());

                    lvi.SubItems.Add(row["strategy_objective"].ToString());
                    lvi.SubItems.Add(row["theme_id"].ToString());
                    lvi.SubItems.Add(row["theme_color"].ToString());

                    lstStrategyObjective.Items.Add(lvi);
                }
                MyDLLs.MyGridUtils.ListViewAlternateRowColor(lstStrategyObjective, Color.LightGray, Color.Bisque, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void FillStrategyList()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "Select Distinct strategy_view.strategy_id, strategy_view.strategy_rank, strategy_view.strategy From"
                + " strategy_view Where strategy_view.theme_id = '" + lstStrategyObjective.SelectedItems[0].SubItems[2].Text.ToString() + "'"
                + " And strategy_view.strategy_objective_id = '" + lstStrategyObjective.SelectedItems[0].SubItems[0].Text.ToString() + "'"
                + " ORDER BY strategy_view.strategy_rank;";

            DataTable tb = db.GetDataTable(conn, strsql);
            lstStrategy.Items.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    ListViewItem lvi = new ListViewItem(row["strategy_id"].ToString());
                    lvi.SubItems.Add(row["strategy"].ToString());
                    lstStrategy.Items.Add(lvi);
                }
                MyDLLs.MyGridUtils.ListViewAlternateRowColor(lstStrategy, Color.Lavender, Color.LavenderBlush);
                //MyDLLs.MyGridUtils.ListViewAlternateRowColor(lstStrategy, Color.GhostWhite, Color.FloralWhite);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void ArrangeGrid()
        {
            GrdAction();
            GrdDelivery();

        }
        private void GrdAction()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            dct.Add("ID", 100);
            dct.Add("action_rank", 0);
            dct.Add("Description", 790);
            dct.Add("Performance Indicator", 0);
            dct.Add("action_partner_org", 0);
            dct.Add("strategy_id", 0);
            dct.Add("performance_indicator_id", 0);
            dct.Add("ramanager_id", 0);
            dct.Add("manager_name", 0);
            dct.Add("director_id", 0);
            dct.Add("director_name", 0);
            dct.Add("Color", 0);
            dct.Add("Service Plan", 200);
            dct.Add("spid", 0);
            dct.Add("Council Plan", 200);
            dct.Add("CouncilPlanID", 0);

            int[] hiddenRows = { 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15 };

            MyGridUtils.ArrangeDataGrid(grdAction, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);
            grdAction.ReadOnly = true;

        }
        private void GrdDelivery()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();

            //dct.Add("-", 30);
            dct.Add("Year", 100);

            int[] hiddenRows = { };

            MyGridUtils.ArrangeDataGrid(grdDelivery, dct, hiddenRows, Color.LightGray, Color.LightSteelBlue);

            DataGridViewCalendarColumn col = new DataGridViewCalendarColumn();
            col.HeaderText = "Target Date";
            col.Width = 100;
            this.grdDelivery.Columns.Add(col);

            grdDelivery.ReadOnly = false;
            grdDelivery.Columns[0].ReadOnly = true;

        }
        private void FillGrid()
        {
            string strsql = "Select * From view_action_with_service_plan Where view_action_with_service_plan.strategy_id = '" + lstStrategy.SelectedItems[0].SubItems[0].Text.ToString() + "'"
                + " ORDER BY theme_id, strategy_objective_id, strategy_rank, action_rank;";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            grdAction.Rows.Clear();
            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    grdAction.Rows.Add(new String[] { row["action_id"].ToString(),
                              row["action_rank"].ToString(),
                              row["action_description"].ToString() + Environment.NewLine,
                              "-",
                              row["action_partner_org"].ToString(),
                              row["strategy_id"].ToString(),
                              "0",
                              row["manager_id"].ToString(),
                              row["manager_description"].ToString(),
                              row["director_id"].ToString(),
                              row["director_description"].ToString(),
                              row["theme_color"].ToString(),
                              row["service_plan"].ToString(),
                              row["service_plan_id"].ToString(),
                              row["council_plan_short"].ToString(),
                              row["council_plan_id"].ToString()
                    });
                }
                MyGridUtils.ColorDataGrid(grdAction, 0, 11);
                grdAction.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void LoadDeliveryProgram()
        {
            string strsql = @"SELECT * FROM program_years order by id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            foreach (DataRow row in tb.Rows)
            {
                grdDelivery.Rows.Add(new String[] {row["program_year"].ToString() });
            }
            
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "checkBoxColumn";
            grdDelivery.Columns.Insert(0, checkBoxColumn);

            //DataGridViewD


        }

       
        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");
            //tb = db.GetDataTable(@"SELECT * FROM director_view;");

            DataRow dr = tb.NewRow();
            dr["director_id"] = "-0-";
            dr["director_description"] = "-NONE-";
            tb.Rows.InsertAt(dr, 0);

            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
            LoadManagers(cboDirector.SelectedValue.ToString());
        }
        private void LoadManagers(string directorcode)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            //DataTable tb = db.GetDataTable(conn, @"SELECT * FROM manager_view WHERE director_id = '" + directorcode + "';");
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "';");

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";
            
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
        private void LoadSourceCouncilPlan()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM council_plan Order by council_plan_short;");

            //DataRow dr = tb.NewRow();
            //dr["id"] = "000";
            //dr["service_plan"] = "-NONE-";
            //tb.Rows.InsertAt(dr, 0);

            cboSourceCouncilPlan.DataSource = tb;
            cboSourceCouncilPlan.DisplayMember = "council_plan_short";
            cboSourceCouncilPlan.ValueMember = "council_plan_id";
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

        //private void lstPI_ItemCheck(object sender, ItemCheckEventArgs e)
        //{
        //    lastPerformanceIndicatorItemChecked = MyGridUtils.listViewDeSelectPreviousItems(lstPI, e, lastPerformanceIndicatorItemChecked);
        //    if (e.NewValue == CheckState.Unchecked)
        //    {
        //        lstPI.Items[lastPerformanceIndicatorItemChecked.Index].Selected = false;
        //        txtPIID.Text = "";
        //    }
        //    else
        //    {
        //        txtPIID.Text = lstPI.Items[e.Index].SubItems[1].Text;
        //    }

        //}

        private void lstStrategy_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                //lastStrategyItemChecked = MyGridUtils.listViewDeSelectPreviousItems(lstStrategy, e, lastStrategyItemChecked);
                //======BEGIN======================================================
                if (lastStrategyItemChecked != null && lastStrategyItemChecked.Checked
               && lastStrategyItemChecked != lstStrategy.Items[e.Index])
                {
                    // uncheck the last item and store the new one
                    lastStrategyItemChecked.Checked = false;
                    //lstStrategy.Items[lastStrategyItemChecked.Index].Selected = false;
                }
                lstStrategy.Items[e.Index].Selected = true;

                //select checked item

                // store current item
                lastStrategyItemChecked = lstStrategy.Items[e.Index];
                //=========END===========================================

                if (e.NewValue == CheckState.Unchecked)
                {
                    lstStrategy.Items[lastStrategyItemChecked.Index].Selected = false;
                    txtStrategyID.Text = "";
                    txtRank.Text = "";
                }
                else
                {
                    txtStrategyID.Text = lstStrategy.Items[e.Index].Text;
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                EventArgs ev = new EventArgs();
                tsbNew_Click(sender, ev);
            }
        }

        private List<string> GetNextActionID(string strategyid)
        {
            List<string> next_action_id = new List<string>();

            string strsql = "SELECT Count(action.strategy_id)As noofrecs FROM action WHERE "
                + " action.strategy_id = '" + strategyid + "';";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            int nextid = 1;

            if (tb.Rows.Count > 0)
            {
                nextid = int.Parse(tb.Rows[0][0].ToString()) + 1;
            }

            next_action_id.Clear();

            next_action_id.Add(strategyid + "." + nextid.ToString().Trim());
            next_action_id.Add(nextid.ToString().Trim());

            return next_action_id;
        }

        //private void NextActionID()
        //{
        //    if (newflag == false) { return; }
        //    int nextid = 1;

        //    string strsql = "Select action.strategy_id as id, action.action_rank as rank, action.strategy_id From action"
        //        + " Where action.strategy_id = '" + txtStrategyID.Text + "'"
        //        + " Order By action.action_rank Desc LIMIT 1;";
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, strsql);

        //    if (tb.Rows.Count > 0)
        //    {
        //        nextid += int.Parse((tb.Rows[0][1]).ToString());
        //    }

        //    txtRank.Text = nextid.ToString();
        //}

        private void tsbSave_Click(object sender, EventArgs e)
        {
            this.ActiveControl = txtActionID; //CHANGED THE FOCUS FROM THE GRIDBOX, coz otherwise errorchecking on the gridbox doesnt work properly!!! DUMI
            DataRowView row = (DataRowView)cbopi.SelectedItem;
            txtPIID.Text = row[0].ToString();

            if (string.IsNullOrEmpty(txtStrategyID.Text))
            {
                MessageBox.Show("Please select Strategic Objective from the List and try again", "OP_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtPIID.Text))
            {
                MessageBox.Show("Please select Performance Indicator from the List and try again", "OP_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtAction.Text))
            {
                MessageBox.Show("Please enter Action for this Strategy and try again", "OP_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //string st = "";

           
            int program_years_count = 0;
            foreach (DataGridViewRow dr in grdDelivery.Rows)
            {
                bool isSelected = Convert.ToBoolean(dr.Cells["checkBoxColumn"].Value);
                if (isSelected)
                {
                    program_years_count ++;
                }
            }
            if (program_years_count == 0) {
                MessageBox.Show("Please Select Delivery Program year(s) and try again", "OP_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //for (int i = 0; i < lstDelivery.Items.Count - 1; i++)
            //{
            //    if (lstDelivery.Items[i].Checked) { st += lstDelivery.Items[i].SubItems[0].Text; }
            //}

            //if (string.IsNullOrEmpty(st))
            //{
            //    MessageBox.Show("Please Select Delivery Program year(s) and try again", "OP_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //string actionid = txtStrategyID.Text + "." + txtRank.Text;

            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("ActionID", txtActionID.Text);
            strdct.Add("ActionDesc", txtAction.Text.Trim());
            strdct.Add("ActionPartner", txtPartner.Text.Trim());
            strdct.Add("StrategyID", txtStrategyID.Text);
            strdct.Add("ManagerID", cboManager.SelectedValue.ToString());
            strdct.Add("ActionRank", int.Parse(txtRank.Text));
            strdct.Add("PIndicatorID", int.Parse(txtPIID.Text));
            strdct.Add("ServicePlanID", cboServicePlan.SelectedValue.ToString());
            strdct.Add("CouncilPlanID", cboSourceCouncilPlan.SelectedValue.ToString());

            //actionid = txtActionID.Text;

            try
            {
                newflag = true;
                string strsql = @"SELECT COUNT(*) as noofrecs FROM action WHERE (action.id) ='" + strdct["ActionID"] + "';";
                DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                DataTable tb = db.GetDataTable(conn, strsql);
                if (int.Parse(tb.Rows[0][0].ToString()) > 0) { newflag = false; }

                if(conn.State == ConnectionState.Closed) { conn.Open(); }

                using (DbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (newflag) {
                            InsertAction(conn, trans, strdct);
                            FillGrid();
                        }
                        else {
                            UpdateAction(conn, trans, strdct);
                            UpdateTableAfterSave();
                        }
                        InserDeliveryPlan(conn, trans, strdct["ActionID"]);

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
                tsbNew_Click(sender, e);

               
                //FillGrid();
                //FillActionList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void UpdateTableAfterSave()
        {
            try
            {

                grdAction.CurrentRow.Cells[2].Value = txtAction.Text.Trim() + Environment.NewLine;
                grdAction.CurrentRow.Cells[4].Value = txtPartner.Text.Trim();
                grdAction.CurrentRow.Cells[6].Value = cbopi.SelectedValue;
                grdAction.CurrentRow.Cells[7].Value = cboManager.SelectedValue;
                grdAction.CurrentRow.Cells[8].Value = cboManager.SelectedText;
                grdAction.CurrentRow.Cells[9].Value = cboDirector.SelectedValue;
                grdAction.CurrentRow.Cells[10].Value = cboDirector.SelectedText;


                grdAction.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private bool InsertAction(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            
            bool result = false;

            string query = @"INSERT INTO action (id, action_rank, action_description, action_partner_org, strategy_id, performance_indicator_id, manager_id, service_plan_id, council_plan_id)"
                + " VALUES (@ActionID , @ActionRank, @ActionDesc, @ActionPartner, @StrategyID, @PIndicatorID, @ManagerID, @ServicePlanID, @CouncilPlanID)";
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
        private bool InserDeliveryPlan(DbConnection con, DbTransaction trans, string actionID)
        {
            bool result = false;

            Dictionary<string, dynamic> strdct1 = new Dictionary<string, dynamic>();
            string query = "";

            try
            {
                strdct1.Clear();
                strdct1.Add("ActionID", actionID);

                query = @"DELETE delivery_program.* FROM delivery_program WHERE action_id ='" + actionID + "';";
                db.InsertUpdateDeleteRecord(con, trans, query, strdct1);

                foreach (DataGridViewRow dr in grdDelivery.Rows)
                {
                    bool isSelected = Convert.ToBoolean(dr.Cells["checkBoxColumn"].Value);
                    if (isSelected)
                    {
                        strdct1.Clear();
                        strdct1.Add("ActionID", actionID);
                        strdct1.Add("DeliveryProgram", dr.Cells[1].Value.ToString());
                        strdct1.Add("TargetDate", dr.Cells[2].Value);

                        query = @"INSERT INTO delivery_program (action_id, delivery_program_year,delivery_program_TargetDate)"
                            + " VALUES (@ActionID , @DeliveryProgram, @TargetDate)";
                        try
                        {
                            db.InsertUpdateDeleteRecord(con, trans, query, strdct1);
                            result = true;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }

                //for (int i = 0; i < lstDelivery.Items.Count - 1; i++)
                //{
                //    if (lstDelivery.Items[i].Checked)
                //    {
                //        strdct1.Clear();
                //        strdct1.Add("ActionID", actionID);
                //        strdct1.Add("DeliveryProgram", lstDelivery.Items[i].SubItems[0].Text);

                //        query = @"INSERT INTO delivery_program (action_id, delivery_program_year)"
                //            + " VALUES (@ActionID , @DeliveryProgram)";
                //        try
                //        {
                //            db.InsertUpdateDeleteRecord(con, trans, query, strdct1);
                //            result = true;
                //        }
                //        catch
                //        {
                //            throw;
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
            return result;
        }
        private bool UpdateAction(DbConnection con, DbTransaction trans, Dictionary<string,dynamic> strdct)
        {
            bool result = false;

            string query = @"UPDATE action SET action_description = @ActionDesc, action_partner_org = @ActionPartner, performance_indicator_id = @PIndicatorID,"
                + " manager_id = @ManagerID, service_plan_id = @ServicePlanID, council_plan_id = @CouncilPlanID WHERE id = @ActionID;";
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

        private void tsbNew_Click(object sender, EventArgs e)
        {
            newflag = true;

            //lstStrategy.Enabled = true;
            //lstAction.Enabled = true;

            tsbNew.Enabled = false;
            tsbEdit.Enabled = true;
            lstStrategy.Enabled = true;
            ClearDataEntry();
            
            //LoadDeliveryProgram();
            //LoadServicePlan();
            //LoadDirectors();
        }

        private void ClearDataEntry()
        {
            //ClearListView(lstDelivery);
            CleargrdDelivery();

            //if (lastStrategyItemChecked.Checked) { lstStrategy.Items[lastStrategyItemChecked.Index].Checked = false;}
            // (lastPerformanceIndicatorItemChecked.Checked) { lstPI.Items[lastPerformanceIndicatorItemChecked.Index].Checked = false; }

            txtAction.Text = "";
            txtPartner.Text = "";
            cboDirector.SelectedIndex = 0;
            grdAction.CurrentCell = null;
            ChangeActionID();
        }

        private void listAction_DoubleClick(object sender, EventArgs e)
        {
            //tsbEdit_Click(sender, e);
        }
        private void LoadDataFromList()
        {
            ListViewItem item = new ListViewItem();
                      
            item = lstStrategy.FindItemWithText(grdAction.CurrentRow.Cells[5].Value.ToString()); // lstAction.SelectedItems[0].SubItems[5].Text);
            if (item != null) { item.Checked = true; }

            txtAction.Text = grdAction.CurrentRow.Cells[2].Value.ToString();  // lstAction.SelectedItems[0].SubItems[2].Text;
            txtPartner.Text = grdAction.CurrentRow.Cells[4].Value.ToString();  // lstAction.SelectedItems[0].SubItems[4].Text;


            //em = lstPI.FindItemWithText(grdAction.CurrentRow.Cells[3].Value.ToString());  // lstAction.SelectedItems[0].SubItems[3].Text);
            if (item != null){ item.Checked = true;}

            cboDirector.SelectedValue = grdAction.CurrentRow.Cells[9].Value;   // lstAction.SelectedItems[0].SubItems[9].Text;
            cboManager.SelectedValue = grdAction.CurrentRow.Cells[7].Value;  // lstAction.SelectedItems[0].SubItems[7].Text;
            cboServicePlan.SelectedValue = grdAction.CurrentRow.Cells["spid"].Value;
            if (!String.IsNullOrEmpty(grdAction.CurrentRow.Cells["CouncilPlanID"].Value.ToString()))
            {
                cboSourceCouncilPlan.SelectedValue = grdAction.CurrentRow.Cells["CouncilPlanID"].Value;
            }

            txtActionID.Text = grdAction.CurrentRow.Cells[0].Value.ToString();
            txtRank.Text = grdAction.CurrentRow.Cells[1].Value.ToString();  // lstAction.SelectedItems[0].SubItems[1].Text;

            string strsql = @"SELECT delivery_program.* FROM delivery_program WHERE action_id = '" + grdAction.CurrentRow.Cells[0].Value.ToString() 
                + "' ORDER BY id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            CleargrdDelivery();

            foreach (DataRow tr in tb.Rows)
            {
                foreach (DataGridViewRow dr in grdDelivery.Rows)
                {
                    //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dr.Cells[0];
                    //chk.Value = chk.FalseValue;
                    if (tr["delivery_program_year"].ToString() == dr.Cells[1].Value.ToString())
                    {
                        //chk.Value = chk.TrueValue;
                        dr.SetValues(true);
                        dr.Cells[2].Value = tr["delivery_program_TargetDate"];
                    }
                }
            }

            //ClearListView(lstDelivery);

            //foreach (DataRow d in tb.Rows)
            //{
            //    //item = lstDelivery.FindItemWithText(d[0]);
            //    //Console.WriteLine(d[0]);
                
            //    item = lstDelivery.FindItemWithText(d[1].ToString());
            //    if (item != null) { item.Checked = true; }
            //}
        }

        //private void ClearListView(ListView lst)
        //{
        //    foreach (ListViewItem listItem in lst.Items)
        //    {
        //        listItem.Checked = false;
        //    }
        //}
        private void CleargrdDelivery()
        {
            foreach (DataGridViewRow dr in grdDelivery.Rows)
            {
                dr.SetValues(false);
                //dr.Cells[2].Value = null;
            }

        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            newflag = false;
            LoadDataFromList();

            lstStrategy.Enabled = false;
            //lstStrategy.BackColor = Color.Red;
            //lstAction.Enabled = false;
            

            tsbNew.Enabled = true;
            tsbEdit.Enabled = false;
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            clsReports.PrintAction();
        }

        private void frmAction_Load(object sender, EventArgs e)
        {
            ArrangeListView();

            //----------------------BIND COMBOBOX TO TABLE-------------------------
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "SELECT performance_indicator.* FROM performance_indicator WHERE pi_year='" + OPGlobals.currentYear + "' OR pi_year='00/00' ORDER BY description;";
            DataTable tb = db.GetDataTable(conn, strsql);
            cbopi.DataSource = tb;
            cbopi.DisplayMember = "description";
            cbopi.ValueMember = "id";
            //----------------------END BIND---------------------------------------

            FillStrategyObjectiveList();

            //FillListView();
            //FillActionList();

            ArrangeGrid();
            //FillGrid();

            //LoadDeliveryProgramOld();
            LoadDeliveryProgram();
            LoadServicePlan();
            LoadSourceCouncilPlan();
            LoadDirectors();
        }

        private void grdAction_DoubleClick(object sender, EventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void lstStrategyObjective_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //lastStrategyItemChecked = null;
            lastStrategyObjectiveItemChecked = MyGridUtils.listViewDeSelectPreviousItems(lstStrategyObjective, e, lastStrategyObjectiveItemChecked);
            if (e.NewValue == CheckState.Unchecked)
            {
                try
                {
                    lstStrategyObjective.Items[lastStrategyItemChecked.Index].Selected = false;
                    txtStrategyID.Text = "";
                    txtRank.Text = "";
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
            }
            FillStrategyList();
        }

        private void txtStrategyID_TextChanged(object sender, EventArgs e)
        {
            ChangeActionID();
        }

        private void ChangeActionID()
        {
            List<string> nextactionid = GetNextActionID(txtStrategyID.Text.ToString());
            txtActionID.Text = nextactionid[0].ToString();
            txtRank.Text = nextactionid[1].ToString();
        }

        private void grdDelivery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) { grdDelivery.CurrentCell.Value = null; }
        }

        private void grdDelivery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("stop");
        }

        private void grdDelivery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lstStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grdAction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
