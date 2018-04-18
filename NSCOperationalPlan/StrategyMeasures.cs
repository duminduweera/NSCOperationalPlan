using MyDLLs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    class StrategyMeasures
    {
        private String _measureCode, _description,_source,_howMeasured,_year,manager_id,strategy_id,_comment,_month;
        private static Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        private static DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        public StrategyMeasures()
        {
           
        }
        #region --- Class Members---
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public string Description
        {get {return _description;}
            set{_description = value;}
        }
        public string HowMeasured
        {
            get
            {
                return _howMeasured;
            }

            set
            {
                _howMeasured = value;
            }
        }

        public string Manager_id
        {
            get
            {
                return manager_id;
            }

            set
            {
                manager_id = value;
            }
        }

        public string MeasureCode
        {
            get
            {
                return _measureCode;
            }

            set
            {
                _measureCode = value;
            }
        }

        public string Source
        {
            get
            {
                return _source;
            }

            set
            {
                _source = value;
            }
        }

        public string Strategy_id
        {
            get
            {
                return strategy_id;
            }

            set
            {
                strategy_id = value;
            }
        }

        public string Year
        {
            get
            {
                return _year;
            }

            set
            {
                _year = value;
            }
        }

        public string Month
        {
            get
            {
                return _month;
            }

            set
            {
                _month = value;
            }
        }
        #endregion

        #region --- Get Queries ---

        public static string GetQueryStrategyMeasures(string cYear)
        {
            string strsql = "SELECT * FROM view_strategy_measure WHERE view_strategy_measure.year = '" + cYear + "'";
            return strsql;
        }

        public static string GetQueryStrategyMeasuresProgress(string cYear, int cMonth)
        {
            int mnth = OPGlobals.GetStrategyMeasureMonth(cMonth);
            string strsql = "SELECT A.*, B.month, B.current_result, B.comment FROM view_strategy_measure A"
                + " LEFT JOIN (SELECT * FROM strategy_measure_monthly WHERE"
                    + " strategy_measure_monthly.year = '" + cYear + "' AND"
                    + " strategy_measure_monthly.month = " + mnth + ") B"
                + " ON B.strategy_measure_code = A.strategy_measure_code AND B.year = A.year AND B.strategy_id = A.strategy_id"
                + " WHERE A.year = '" + cYear + "'";
            return strsql;

        }
        public static string GetQueryStrategyMeasuresProgress(string cYear, int cMonth, string cDirectorID)
        {
            string strsql = GetQueryStrategyMeasuresProgress(cYear, cMonth) + " AND director_id='" +cDirectorID + "'";
            return strsql;

        }
        public static string GetQueryStrategyMeasuresProgress(string cYear, int cMonth, string cDirectorID, string cManagerID)
        {
            string strsql = GetQueryStrategyMeasuresProgress(cYear, cMonth) + " AND director_id='" + cDirectorID + "' AND manager_id='" + cManagerID + "'";
            return strsql;
        }

        #endregion

        #region --- Save Monthly/anualy progress

        //strategy_measure_code , strategy_id, year, month, current_result, comment

        #endregion


        internal static DataTable getMeasuresforManagers(String managerID,String year)
        {
            String strsql = "SELECT * FROM view_strategy_measure WHERE manager_id='" + managerID + "' and year='" + year + "' ORDER BY theme_id,strategy_objective_id,rank;";    
            return db.GetDataTable(conn, strsql);
        }
        internal static DataTable getMeasuresforDirectors(String director_ID, String year)
        {
            String strsql = "SELECT * FROM view_strategy_measure WHERE director_id='" + director_ID + "' and year='" + year + "' ORDER BY theme_id,strategy_objective_id,rank;";
            return db.GetDataTable(conn, strsql);
        }

        internal static DataTable getAllMeasures(String year)
        {
            String strsql = "SELECT * FROM view_strategy_measure WHERE year='" + year + "' ORDER BY theme_id,strategy_objective_id,rank;";           
            return db.GetDataTable(conn, strsql);
        }

        internal static DataTable getAllManagers()
        {
            string strsql = "SELECT * FROM manager order by manager_description;";
            return db.GetDataTable(conn, strsql);
        }

        private bool InsertMonthlyProgress() {
            bool result = false;
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>(); 
                
            dict.Add("strategy_measure_code", this._measureCode);
            dict.Add("strategy_id", this.strategy_id);
            dict.Add("year", this._year);
            dict.Add("month", this.Month);
            dict.Add("remark", this._comment);
            foreach (KeyValuePair<String, dynamic> entry in dict) {
                if (entry.Value == null) {throw new Exception("Cannot save startegy measure,  null values in "+entry.Value);}
            }

            string query = @"INSERT INTO strategy_measure_monthly"
                + " (strategy_measure_code, strategy_id, year, month, remark)"
                + " VALUES (@strategy_measure_code, @strategy_id, @year, @month, @remark)";

            using (DbTransaction trans = conn.BeginTransaction()) {
                try
                {
                    db.InsertUpdateDeleteRecord(conn, trans, query, dict);
                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");                    
                }
            }
            return result;
        }
    }
}
