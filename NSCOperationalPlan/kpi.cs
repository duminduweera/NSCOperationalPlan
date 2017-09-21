using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using MyDLLs;
using System.Data;

namespace NSCOperationalPlan
{
    public class KeyPerformanceIndex : IEquatable<KeyPerformanceIndex>
    {
        private int     _kpiID;
        private string  _kpmID;
        private string  _managerID;
        private string  _kpiDescription;
        private string  _kpiPrefix;
        private string  _kpiUnit;
        private string  _kpiEstimateYear;
        private double  _kpiEstimateValue;
        private string  _kpiCurrentYear;
        private double  _kpiCurrentValue;
        private int     _kpiMonth;
        private string  _monthlyRemark;
        private string  _service_plan_id;

        public int KPIID
        {
            get { return _kpiID; }
            set { _kpiID = value; }
        }
        public string KPMID
        {
            get { return _kpmID; }
            set { _kpmID = value; }
        }
        public string ManagerID
        {
            get { return _managerID;}
            set { _managerID = value;}
        }
        public string Description
        {
            get { return _kpiDescription; }
            set { _kpiDescription = value; }
        }
        public string Prefix
        {
            get { return _kpiPrefix; }
            set { _kpiPrefix = value; }
        }
        public string Unit
        {
            get { return _kpiUnit; }
            set { _kpiUnit = value; }
        }
        public string EstimateYear
        {
            get { return _kpiEstimateYear; }
            set { _kpiEstimateYear = value; }
        }
        public int KPIMonth
        {
            get { return _kpiMonth; }
            set { _kpiMonth = value; }
        }
        public double EstimateValue
        {
            get { return _kpiEstimateValue; }
            set { _kpiEstimateValue = value; }
        }
        public string CurrentYear
        {
            get { return _kpiCurrentYear; }
            set { _kpiCurrentYear = value; }
        }
        public string ServicePlanID
        {
            get { return _service_plan_id; }
            set { _service_plan_id = value; }
        }
        public double CurrentValue
        {
            get { return _kpiCurrentValue; }
            set { _kpiCurrentValue = value; }
        }

        public string Remark
        {
            get { return _monthlyRemark; }
            set { _monthlyRemark = value; }
        }

        public KeyPerformanceIndex() { }

        public bool InsertKPI(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("KPI_ID", this._kpiID);
            strdct.Add("KPM_ID", this._kpmID);
            strdct.Add("ManagerID", this._managerID);
            strdct.Add("Description", this._kpiDescription);
            strdct.Add("Prefix", this._kpiPrefix);
            strdct.Add("Units", this._kpiUnit);
            strdct.Add("ServicePlan", this._service_plan_id);

            string query = @"INSERT INTO kpi"
                + " (id, kpm_id, manager_id, efficiency_description, prefix, unit, service_plan_id)"
                + " VALUES (@KPI_ID, @KPM_ID, @ManagerID, @Description, @Prefix, @Units, @ServicePlan)";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }
        public bool UpdateKPI(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;

            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("KPI_ID", this._kpiID);
            strdct.Add("KPM_ID", this._kpmID);
            strdct.Add("ManagerID", this._managerID);
            strdct.Add("Description", this._kpiDescription);
            strdct.Add("Prefix", this._kpiPrefix);
            strdct.Add("Units", this._kpiUnit);
            strdct.Add("ServicePlan", this._service_plan_id);


            string query = "UPDATE kpi SET manager_id = @ManagerID,"
                + " efficiency_description = @Description , prefix = @Prefix, unit = @Units, service_plan_id = @ServicePlan WHERE id = @KPI_ID;";

            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }

        public bool InsertKPIEstimate(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("KPI_ID", this._kpiID);
            strdct.Add("KPI_Year", this._kpiEstimateYear);
            strdct.Add("KPI_Estimate", this._kpiEstimateValue);

            string query = @"INSERT INTO kpi_estimate"
                + " (kpi_id, kpi_year, kpi_estimate)"
                + " VALUES (@KPI_ID , @KPI_Year, @KPI_Estimate)";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }
            return result;
        }
        public bool UpdateKPIEstimate(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("KPI_ID", this._kpiID);
            strdct.Add("KPI_Year", this._kpiEstimateYear);
            strdct.Add("KPI_Estimate", this._kpiEstimateValue);

            string query = @"UPDATE kpi_estimate SET kpi_estimate = @KPI_Estimate"
                + " WHERE kpi_id = @KPI_ID AND kpi_year = @KPI_Year;";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }
            return result;
        }

        public bool InsertKPIMonthlyProgress(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("KPI_ID", this._kpiID);
            strdct.Add("KPI_Year", this._kpiCurrentYear);
            strdct.Add("KPI_Month", this._kpiMonth);
            strdct.Add("Progress", this._kpiCurrentValue);
            strdct.Add("Remark", this._monthlyRemark);

            string query = @"INSERT INTO kpi_progress"
                + " (kpi_id, kpi_year, kpi_month, kpi_progress, kpi_remark)"
                + " VALUES (@KPI_ID, @KPI_Year, @KPI_Month, @Progress, @Remark)";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }
        public bool UpdateKPIMonthlyProgress(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("KPI_ID", this._kpiID);
            strdct.Add("KPM_Year", this._kpiCurrentYear);
            strdct.Add("KPI_Month", this._kpiMonth);
            strdct.Add("Progress", this._kpiCurrentValue);
            strdct.Add("Remark", this._monthlyRemark);

            string query = @"UPDATE kpi_progress SET"
                + " kpi_progress = @Progress, kpi_remark = @Remark"
                + " WHERE kpi_id = @KPI_ID AND kpi_year = @KPM_Year AND kpi_month = @KPI_Month;";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }

        public bool InsertUpdateMonthlyProgress(Database db, DbConnection con, DbTransaction trans)
        {
            if(IsUpdated(db, con, this._kpiID, this._kpiCurrentYear, this._kpiMonth))
            {
                UpdateKPIMonthlyProgress(db, con, trans);
            } else
            {
                InsertKPIMonthlyProgress(db, con, trans);
            }

            return true;
        }

        public static DataTable GetKPITable(Database db, DbConnection con, string manID, string kpiyear)
        {
            string strsql = "SELECT * FROM view_kpi"
                + " WHERE manager_id='" + manID + "' and kpi_year='" + kpiyear + "' ORDER BY kpm_id,kpi_id;";
            return db.GetDataTable(con, strsql);
        }
        public static DataTable GetKPITable(Database db, DbConnection con, string manID, string kpiyear, string kpm)
        {
            string strsql = "SELECT * FROM view_kpi"
                + " WHERE manager_id='" + manID + "' and kpi_estimate_year='" + kpiyear + "' and kpm_id='" + kpm + "' ORDER BY kpm_id,kpi_id;";
            return db.GetDataTable(con, strsql);
        }
        public static DataTable GetKPIwithProgressTable(Database db, DbConnection con, string manID, string kpiyear, int kpimonth, string kpm)
        {
            string strsql = "SELECT A.kpi_id, A.kpm_id, A.kpm_description, A.manager_id, A.director_id, A.manager_name, A.manager_description,"
                + " A.director_name, A.director_description, A.efficiency_description, A.kpi_prefix_id, A.kpi_prefix, A.kpi_prefix_short,"
                + " A.kpi_estimate, A.unit_id, A.kpi_unit, A.kpi_unit_short, A.kpi_estimate_year, B.kpi_progress, B.kpi_remark, B.kpi_month, B.kpi_year"
                + " FROM view_kpi A Left Join"
                + "(Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark"
                + " FROM kpi_progress WHERE kpi_progress.kpi_year = '" + kpiyear + "' AND kpi_progress.kpi_month =" + kpimonth + ") B "
                + " On A.kpi_id = B.kpi_id"
                + " WHERE (A.manager_id = '" + manID + "' or A.director_id ='" + manID + "') and A.kpi_estimate_year = '" + kpiyear + "' and A.kpm_id = '" + kpm + "' ORDER BY kpm_id,kpi_id; ";

            return db.GetDataTable(con, strsql);
        }

        public static string GetMonthlyKPIProgressQuery(string opYear, int opMonth)
        {
            string strsql = "";
            string opmonth = Enum.GetName(typeof(Months), opMonth);

            strsql = "Select A.kpi_id, A.kpm_id, A.kpm_description, A.manager_id, A.manager_name, A.manager_description, A.director_description,"
            + " A.director_name, A.director_id, A.department, A.sub_department, A.efficiency_description, A.kpi_prefix_id, A.kpi_estimate_year,"
            + " A.kpi_prefix, A.kpi_prefix_short, A.kpi_estimate, A.unit_id, A.kpi_unit, A.kpi_unit_short, A.service_plan_id, A.service_plan,"
            + " B.kpi_year, B.kpi_month, B.kpi_progress, B.kpi_remark From view_kpi A Left Join"
            + " (Select kpi_progress.kpi_id, kpi_progress.kpi_year, kpi_progress.kpi_month, kpi_progress.kpi_progress, kpi_progress.kpi_remark"
            + " From kpi_progress Where kpi_progress.kpi_year = '" + opYear + "' And kpi_progress.kpi_month = " + opMonth + ") B On A.kpi_id = B.kpi_id" ;
            return strsql;
        }

        public static string GetMonthlyKPIProgressQuery(string opYear, int opMonth, string opManager)
        {
            string strsql = "";
            strsql = GetMonthlyKPIProgressQuery(opYear, opMonth);
            if (!string.IsNullOrEmpty(opManager)  && opManager != "-0-")
            {
                strsql += " WHERE manager_id='" + opManager + "'";
            }
            return strsql;
        }

        public static string GetMonthlyKPIProgressQuery(string opYear, int opMonth, string opDirector, string opManager)
        {
            string strsql = "";
            strsql = GetMonthlyKPIProgressQuery(opYear, opMonth);

            //string opmonth = Enum.GetName(typeof(Months), opMonth);

            if (string.IsNullOrEmpty(opManager) || opManager == "-0-")
            {
                strsql += " WHERE director_id='" + opDirector + "'";
            }
            else
            {
                strsql += " WHERE director_id='" + opDirector + "' AND manager_id='" + opManager + "'";
            }

            return strsql;
        }

        public static KeyPerformanceIndex GetProgress(Database db, DbConnection con, int id, string yr, int mnt)
        {
            KeyPerformanceIndex k = new KeyPerformanceIndex();
            string strsql = "SELECT kpi_progress.kpi_progress, kpi_progress.kpi_remark FROM kpi_progress"
                + " WHERE kpi_progress.kpi_id = " + id + " AND kpi_progress.kpi_year = '" + yr + "' AND kpi_progress.kpi_month = " + mnt;
            try
            {
                DataTable tb = db.GetDataTable(con, strsql);
                if (tb.Rows.Count > 0)
                {
                    k.KPIID = id;
                    k.CurrentYear = yr;
                    k.KPIMonth = mnt;
                    k.CurrentValue = Convert.ToDouble(tb.Rows[0]["kpi_progress"].ToString());
                    k.Remark = tb.Rows[0]["kpi_remark"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("ERROR UPDATING PROGRESS" + Environment.NewLine + ex);
            }

            return k;
        }

        public static int getNextKPIIndex()
        {
            Database db1 = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db1.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            string strsql = "SELECT kpi.id FROM kpi ORDER BY id DESC LIMIT 1;";
            DataTable tb = db1.GetDataTable(conn, strsql);

            if (tb.Rows.Count > 0) { return int.Parse(tb.Rows[0][0].ToString()) + 1; } else { return 1; }
        }

        public bool Equals(KeyPerformanceIndex other)
        {
            if(this._kpiID != other.KPIID) { return false; }
            if(this._kpiCurrentYear != other.CurrentYear) { return false; }
            if(this._kpiMonth != other.KPIMonth) { return false; }
            if (this._kpiCurrentValue != other.CurrentValue) { return false; }
            if (this._monthlyRemark != other.Remark) { return false; }

            return true;
        }

        private bool IsUpdated(Database db, DbConnection con, int kniid, string year, int month)
        {
            string strsql = "Select Count(*) as noofrecs From kpi_progress Where kpi_id = " + kniid + " And kpi_year = '" + year + "' And kpi_month = " + month + ";";
            DataTable tb = db.GetDataTable(con, strsql);

            int count = int.Parse(tb.Rows[0]["noofrecs"].ToString());

            if (count > 0) { return true; } else { return false; }
        }
    }
}
