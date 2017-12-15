using MyDLLs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    class MonthlyProgress
    {
        private string _actionID;
        private string _opYear;
        private int _opMonth;
        private string _description;
        private double _percentageCompleted;
        private int _actionStatus;

        public string  ActionID
        {
            get { return _actionID; }
            set { _actionID = value; }
        }
        public string OPYear
        {
            get { return _opYear; }
            set { _opYear = value; }
        }
        public int OPMonth
        {
            get { return _opMonth; }
            set { _opMonth = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public double PercentageCompleted
        {
            get { return _percentageCompleted; }
            set { _percentageCompleted = value; }
        }
        public int ActionStatus
        {
            get { return _actionStatus; }
            set { _actionStatus = value; }
        }

        public MonthlyProgress() { }
        public MonthlyProgress(string actionID, string opYear, int opMonth)
        {
            this._actionID = actionID;
            this._opYear = opYear;
            this._opMonth = opMonth;
        }

        public bool InsertUpdateMonthlyProgress(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("progress_year", this._opYear);
            strdct.Add("progress_description", this._description);
            strdct.Add("status_id", this._actionStatus.ToString());
            strdct.Add("action_id", this._actionID);
            strdct.Add("progress_month", this._opMonth);
            strdct.Add("progress_pecentage", this._percentageCompleted);

            string strsql = "SELECT COUNT(*) as noofrecs FROM progress"
                + " WHERE action_id = '" + this._actionID + "' AND progress_year = '"
                + this._opYear + "' AND progress_month = " + this._opMonth + ";";
            DataTable tb = db.GetDataTable(con, strsql);

            if (int.Parse(tb.Rows[0][0].ToString()) == 0) {
                strsql = @"INSERT INTO progress (progress_year, progress_month, progress_pecentage, progress_description, status_id, action_id)"
                                   + " VALUES (@progress_year, @progress_month, @progress_pecentage, @progress_description, @status_id, @action_id)";
            } else {
                strsql = @"UPDATE progress SET progress_description = @progress_description, progress_pecentage = @progress_pecentage,"
                               + " status_id = @status_id"
                               + " WHERE action_id = @action_id AND progress_year=@progress_year AND progress_month=@progress_month;";
            }
            db.InsertUpdateDeleteRecord(con, trans, strsql, strdct);

            return result;
        }

        //public bool UpdateMonthlyProgress(Database db)
        //{
        //    bool result = false;
        //    Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

        //    strdct.Add("progress_year", this._opYear);
        //    strdct.Add("progress_description", this._description);
        //    strdct.Add("status_id", this._actionStatus.ToString());
        //    strdct.Add("action_id", this._actionID);
        //    strdct.Add("progress_month", this._opMonth);
        //    strdct.Add("progress_pecentage", this._percentageCompleted);

        //    string strsql = "SELECT COUNT(*) as noofrecs FROM progress"
        //        + " WHERE action_id = '" + this._actionID + "' AND progress_year = '"
        //        + this._opYear + "' AND progress_month = " + this._opMonth + ";";
        //    DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        //    DataTable tb = db.GetDataTable(conn, strsql);

        //    bool newflag = (int.Parse(tb.Rows[0][0].ToString()) > 0 ? false : true);

        //    try
        //    {
        //        if (conn.State == ConnectionState.Closed) { conn.Open(); }
        //        using (DbTransaction trans = conn.BeginTransaction())
        //        {
        //            try
        //            {
        //                if (newflag)
        //                {
        //                    strsql = @"INSERT INTO progress (progress_year, progress_month, progress_pecentage, progress_description, status_id, action_id)"
        //                        + " VALUES (@progress_year, @progress_month, @progress_pecentage, @progress_description, @status_id, @action_id)";
        //                }
        //                else
        //                {
        //                    strsql = @"UPDATE progress SET progress_description = @progress_description, progress_pecentage = @progress_pecentage,"
        //                        + " status_id = @status_id"
        //                        + " WHERE action_id = @action_id AND progress_year=@progress_year AND progress_month=@progress_month;";
        //                }
        //                db.InsertUpdateDeleteRecord(conn, trans, strsql, strdct);
        //                trans.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                trans.Rollback();
        //                throw ex;
        //            }
        //        }
        //        result = true;
        //    } catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    if (conn.State == ConnectionState.Open) { conn.Close(); }
        //    return result;
        //}

        public bool Equals(MonthlyProgress other)
        {
            if (this._actionID != other.ActionID) { return false; }
            if (this._opYear != other.OPYear) { return false; }
            if (this._opMonth != other.OPMonth) { return false; }
            if (this._description != other.Description) { return false; }
            if (this._actionStatus != other.ActionStatus) { return false; }
            if (this._percentageCompleted != other.PercentageCompleted) { return false; }

            return true;
        }

        //===============================================
        public static string GetQueryMonthlyProgress(string opYear, int opMonth)
        {
            string strsql = "";
            string opmonth = Enum.GetName(typeof(Months), opMonth);

            strsql = "Select A.theme_id, A.theme_short, A.theme_color, A.strategy_objective_id,"
                + " concat(A.strategy_objective_id,' ', A.strategy_objective) as strategy_objective,"
                + " A.strategy_objective_rank, A.strategy_id, A.strategy_rank, concat(A.strategy_id, ' ', A.strategy) as strategy,"
                + " A.action_id, A.action_rank, A.action_description as action_description, A.action_partner_org,"
                + " A.manager_id, A.manager_name, A.manager_description, A.sub_department, A.director_id, A.director_name,"
                + " A.director_description, A.department, A.delivery_program_id, A.delivery_program_year, A.delivery_program_TargetDate,"
                + " A.service_plan_id, A.service_plan,"
                + " B.progress_id, B.status_id, B.progress_description, B.progress_pecentage, B.status_short, B.status_color,"
                + " '" + opYear + "' as progress_year, " + opMonth + " as progress_month, '" + opmonth + "' as delivery_program_month"
                + " from (Select * From view_action_by_year Where view_action_by_year.delivery_program_year = '" + opYear + "') A Left Join"
                + " (Select progress.id As progress_id, progress.progress_description, progress.progress_pecentage, status.id As status_id,"
                + " status.status_short, status.status_color, progress.action_id, progress.progress_year, progress.progress_month"
                + " From progress Inner Join status On progress.status_id = status.id Where"
                + " progress.progress_year = '" + opYear + "' And progress.progress_month = " + opMonth + ") B On A.action_id = B.action_id";
            return strsql;

        }
        public static string GetQueryMonthlyProgress(string opYear, int opMonth, string opDirector)
        {
            string strsql = GetQueryMonthlyProgress(opYear, opMonth);
            if (opDirector != "-0-")
            {
                strsql += " WHERE director_id='" + opDirector + "'";
            }
            return strsql;
        }
        public static string GetQueryMonthlyProgress(string serviceID, string opYear, int opMonth)
        {
            string strsql = GetQueryMonthlyProgress(opYear, opMonth);
            if (serviceID != "000")
            {
                strsql += " WHERE service_plan_id='" + serviceID + "'";
            }
            return strsql;
        }
        public static string GetQueryMonthlyProgress(string opYear, int opMonth, string opDirector, string opManager)
        {
            string strsql = GetQueryMonthlyProgress(opYear, opMonth);
            if (opDirector != "-0-" && opManager != "-0-")
            {
                strsql += " WHERE director_id='" + opDirector + "' AND manager_id='" + opManager + "'";
            } else if (opDirector != "-0-")
            {
                strsql += " WHERE director_id='" + opDirector + "'";
            } else if (opManager != "-0-") {
                strsql += " WHERE manager_id='" + opManager + "'";
            }

            return strsql;
        }

    }
}
