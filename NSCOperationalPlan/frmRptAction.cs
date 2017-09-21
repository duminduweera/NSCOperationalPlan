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
    public partial class frmRptAction : Form
    {
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        public frmRptAction()
        {
            InitializeComponent();
        }

        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");
            //tb = db.GetDataTable(@"SELECT * FROM director_view;");
            DataRow dr = tb.NewRow();
            dr["director_id"] = "-0-";
            dr["director_description"] = "=== ALL ===";
            tb.Rows.InsertAt(dr,0);
            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
            LoadManagers(cboDirector.SelectedValue.ToString());
        }

        private void LoadManagers(string directorcode)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM manager_view WHERE director_id = '" + directorcode + "';");

            DataRow dr = tb.NewRow();
            dr["manager_id"] = "-0-";
            dr["manager_description"] = "=== ALL ===";
            tb.Rows.InsertAt(dr, 0);

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";

        }

        private void ListDeliveryYear()
        {
            Dictionary<string, int> dct = new Dictionary<string, int>();
            dct.Add("Year", 200);

            MyDLLs.MyGridUtils.ArrangeListView(lstRptDelivery, dct);
            this.lstRptDelivery.View = View.Details;

            string strsql = @"SELECT DISTINCT program_year FROM program_years order by id;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            try
            {
                foreach (DataRow row in tb.Rows)
                {
                    ListViewItem lvi = new ListViewItem(row["program_year"].ToString());
                    lstRptDelivery.Items.Add(lvi);
                }
                MyDLLs.MyGridUtils.ListViewAlternateRowColor(lstRptDelivery, Color.LightGray, Color.Bisque);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRptAction_Load(object sender, EventArgs e)
        {
            ListDeliveryYear();
            LoadDirectors();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            string strsql = "";
            if (cboDirector.SelectedValue.ToString() !="-0-") { strsql = " AND manager.manager_id='" + cboDirector.SelectedValue.ToString() + "'"; }
            if (cboManager.SelectedValue.ToString() != "-0-") { strsql += " AND manager.id='" + cboManager.SelectedValue.ToString() + "'"; }

            //List<string> mDeliveryYearList = lstRptDelivery.Items.Cast<ListViewItem>()
                                 //.Select(x => x.ToString()).ToList();

            List<string> mDeliveryYearList = lstRptDelivery.Items.Cast<ListViewItem>()
                                 .Select(x => x.Text).ToList();

            string yrs = "";
            //string yrs1 = "";
            foreach (ListViewItem item in lstRptDelivery.Items)
            {
                if (item.Checked)
                {
                    if (string.IsNullOrEmpty(yrs))
                    {
                        yrs = " (delivery_program_year = '" + item.Text + "'";
                        //yrs1 = item.Text;
                    }
                    else
                    {
                        yrs += " OR " + "delivery_program_year = '" + item.Text + "'";
                        //yrs1 += "," + item.Text;
                    }
                }
            }

            if (string.IsNullOrEmpty(yrs))
            {
                yrs = " WHERE (`delivery_program`.`delivery_program_year` = '" + OPGlobals.currentYear + "')";
                //yrs1 = OPGlobals.currentYear;
            }
            else
            {
                yrs = " WHERE " + yrs + ")";
            }

            strsql = yrs + strsql;

            strsql = "SELECT DISTINCT view_strategy.theme_id As theme_id, view_strategy.theme_color As theme_color,"
                + " view_strategy.strategy_objective_id As strategy_objective_id, view_strategy.strategy_objective As strategy_objective,"
                + " view_strategy.strategy_id As strategy_id, view_strategy.strategy As strategy, action.id As action_id,"
                + " Concat_Ws(' - ', action.id, action.action_description) As action, action.action_partner_org As action_partner_org,"
                + " manager.manager_description As manager_name, delivery_program_year As delivery_program_year,"
                + " manager.id As manager_id, manager.manager_id As director_id"
                + " FROM ((view_strategy Left Join action On view_strategy.strategy_id = action.strategy_id) JOIN"
                + " manager On action.manager_id = manager.id) Join delivery_program On delivery_program.action_id = action.id"
                + strsql
                + " ORDER BY theme_id, view_strategy.strategy_rank, action.action_rank;";


            clsReports.PrintAction(strsql);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboDirector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDirector.SelectedValue.GetType().Name == "String" && cboDirector.SelectedValue.ToString() != "-0-")
                {
                    LoadManagers(cboDirector.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
