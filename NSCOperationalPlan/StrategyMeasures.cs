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
        Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

        private String mMeasureCode, mDescription, mSource, mHowMeasured, mManager_id, mStrategy_id, mComment, mYear;
        private int mMonth;
        private double mCurrentProgress;

        #region --- Members ---
        public string MeasureCode
        {
            get { return mMeasureCode; }  
            set { mMeasureCode = value; }
        }
        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        public string Source
        {
            get { return mSource; }
            set { mSource = value; }
        }
        public string MeasuringCriteria
        {
            get { return mHowMeasured; }
            set { mHowMeasured = value; }
        }
        public string ManagerID
        {
            get { return mManager_id; }
            set { mManager_id = value; }
        }
        public string StrategyID
        {
            get { return mStrategy_id; }
            set { mStrategy_id = value; }
        }
        public string Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }
        public string Year
        {
            get { return mYear; }
            set { mYear = value; }
        }
        public int Month
        {
            get { return mMonth; }
            set { mMonth = value; }
        }
        public double CurrentProgress
        {
            get { return mCurrentProgress; }
            set { mCurrentProgress = value; }
        }
        #endregion

        #region --- Constrctors ---
        public StrategyMeasures() { }
        public StrategyMeasures(string measureCode, string strategyCode, string year, int month ) : this()
        {
            this.mMeasureCode = measureCode;
            this.mStrategy_id = strategyCode;
            this.mYear = year;
            this.mMonth = month;

            //LoadMembersIfExist();
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
            string strsql = GetQueryStrategyMeasuresProgress(cYear, cMonth) + " AND director_id='" + cDirectorID + "'";
            return strsql;

        }
        public static string GetQueryStrategyMeasuresProgress(string cYear, int cMonth, string cDirectorID, string cManagerID)
        {
            string strsql = GetQueryStrategyMeasuresProgress(cYear, cMonth) + " AND director_id='" + cDirectorID + "' AND manager_id='" + cManagerID + "'";
            return strsql;
        }

        #endregion

        #region --- Database operations ---
        private Dictionary<string, dynamic> FillDictionary()
        {
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("MeasureCode", this.mMeasureCode);
            strdct.Add("Description", this.mDescription);
            strdct.Add("Source", this.mSource);
            strdct.Add("HowMeasured", this.mHowMeasured);
            strdct.Add("ManagerID", this.mManager_id);
            strdct.Add("StrategyID", this.mStrategy_id);
            strdct.Add("Comment", this.mComment);
            strdct.Add("Month", this.mMonth);
            strdct.Add("Year", this.mYear); 
            strdct.Add("CurrentProgress", this.mCurrentProgress);

            return strdct;
        }
        private void LoadMembersIfExist()
        {
            string strsql = @"SELECT * FROM strategy_measure_monthly"
                + " WHERE strategy_measure_code = '" + this.mMeasureCode + "'"
                + " AND strategy_id='" + this.mStrategy_id + "'"
                + " AND year='" + this.mYear + "' AND month = " + this.mMonth;


        }
        public bool IsExist()
        {
            bool result = false;
            string strsql = @"SELECT * FROM strategy_measure_monthly"
                + " WHERE strategy_measure_code = '" + this.mMeasureCode + "'"
                + " AND strategy_id='" + this.mStrategy_id + "'"
                + " AND year='" + this.mYear + "' AND month = " + this.mMonth;

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, strsql);

            if (tb.Rows.Count > 0) { result = true; }

            return result;

        }
        public bool InsertMonthlyStrategyMeasures(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = FillDictionary();

            string query = @"INSERT INTO strategy_measure_monthly"
                + " (strategy_measure_code, strategy_id, year, month, current_result, comment) VALUES"
                + " (@MeasureCode, @StrategyID, @Year, @Month, @CurrentProgress, @Comment)";
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
        public bool UpdateMonthlyStrategyMeasures(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;

            Dictionary<string, dynamic> strdct = FillDictionary();

            string query = @"UPDATE strategy_measure_monthly SET current_result = @CurrentProgress, comment = @Comment"
                + " WHERE strategy_measure_code= @MeasureCode and strategy_id = @StrategyID"
                + " AND year = @Year AND month = @Month";
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
        #endregion
    }
}
