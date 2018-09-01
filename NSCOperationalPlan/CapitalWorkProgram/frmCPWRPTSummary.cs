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
    public partial class frmCPWRPTSummary : Form
    {
        public string ReportType = "";
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        Dictionary<string, string> serviceOptions = new Dictionary<string, string>()
        {
            { "-0-", "NONE" },
            { "1", "Service Plan - Detail Report" },
            { "2", "Service Plan - Summary Report" },
        };

        public frmCPWRPTSummary()
        {
            InitializeComponent();
        }
        private void tsbClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void LoadServicePlan()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM service_plan where id !='000' ORDER BY service_plan;");

            DataRow dr = tb.NewRow();
            dr["id"] = "-0-";
            dr["service_plan"] = "=== ALL ===";
            tb.Rows.InsertAt(dr, 0);

            cboServicePlan.DataSource = tb;
            cboServicePlan.DisplayMember = "service_plan";
            cboServicePlan.ValueMember = "id";
        }
        private void LoadDirectors()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM director_view;");

            DataRow dr = tb.NewRow();
            dr["director_id"] = "-0-";
            dr["director_description"] = "=== ALL ===";
            tb.Rows.InsertAt(dr, 0);
            cboDirector.DataSource = tb;
            cboDirector.DisplayMember = "director_description";
            cboDirector.ValueMember = "director_id";
            LoadManagers(cboDirector.SelectedValue.ToString());
        }
        private void LoadManagers(string directorcode)
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT * FROM view_directors_plus_managers WHERE director_id = '" + directorcode + "';");

            DataRow dr = tb.NewRow();
            dr["manager_id"] = "-0-";
            dr["manager_description"] = "=== ALL ===";
            tb.Rows.InsertAt(dr, 0);

            cboManager.DataSource = tb;
            cboManager.DisplayMember = "manager_description";
            cboManager.ValueMember = "manager_id";

        }
        private void LoadYears()
        {
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, @"SELECT DISTINCT progress_year FROM progress ORDER BY progress_year;");

            //DataRow dr = tb.NewRow();
            //dr["progress_year"] = OPGlobals.currentYear;
            //tb.Rows.InsertAt(dr, 0);

            cboOPYear.DataSource = tb;
            cboOPYear.DisplayMember = "progress_year";
            cboOPYear.ValueMember = "progress_year";

        }
        private void LoadMonths()
        {
            //yourComboBox.DataSource = Enum.GetValues(typeof(YourEnum))
            cboOPMonth.DataSource = Enum.GetValues(typeof(Months));
            cboOPMonth.Text = Enum.GetName(typeof(Months), OPGlobals.currentMonth);

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

        private void frmCPWRPTSummary_Load(object sender, EventArgs e)
        {
            LoadServicePlan();
            LoadDirectors();
            LoadYears();
            LoadMonths();

            //cboServicePlan.DataSource = new BindingSource(serviceOptions, null);
            //cboServicePlan.DisplayMember = "Value";
            //cboServicePlan.ValueMember = "key";
        }


        private void tsbPrint_Click(object sender, EventArgs e)
        {
            Months m = (Months)Enum.Parse(typeof(Months), cboOPMonth.SelectedValue.ToString());
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                if (chk1.Checked == true)
                {
                    clsReports.PrintCapitalWorksServiceSummary(cboOPYear.Text, (int)m);
                } else
                {
                    if (cboServicePlan.SelectedValue.ToString() != "-0-")
                    {
                        //clsReports.PrintCapitalWorksServiceDetails(cboOPYear.Text, (int)m);
                        clsReports.PrintCapitalWorksServiceDetails(cboOPYear.Text, (int)m, cboServicePlan.SelectedValue.ToString());
                    }
                    else
                    {
                        clsReports.PrintCapitalWorksServiceDetails(cboOPYear.Text, (int)m);
                        //clsReports.PrintCapitalWorksServiceDetails(cboOPYear.Text, (int)m, cboServicePlan.SelectedValue.ToString());
                    }
                }

            } else
            {
                if (cboDirector.SelectedValue.ToString() != "-0-")
                {
                    clsReports.PrintCapitalWorksDepartmentSummary(cboOPYear.Text, (int)m, cboDirector.SelectedValue.ToString());
                }
                else
                {
                    clsReports.PrintCapitalWorksSummary(cboOPYear.Text, (int)m);
                }
            }
        }

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                cboServicePlan.Enabled = false;
            } else
            {
                cboServicePlan.Enabled = true;
            }
        }
    }
}
