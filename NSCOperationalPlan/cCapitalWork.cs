using MyDLLs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NSCOperationalPlan
{
    public class CapitalWork
    {
        private int     _cpwID;
        private string _cpwJobCostNumber;
        private string  _cpwYear;
        private int     _cpwMonth;
        private string  _cpwManagerID;
        private string  _cpwServicePlanID;
        private string  _cpwThemeID;
        private string  _cpwStrategyObjID;
        private string  _cpwDescription;
        private double  _cpwOrgBudget;
        private double  _cpwReviseBudget;
        private double  _cpwytd;

        private double  _cpwPrjBudget;
        private double  _cpwCompletedPercentage;
        private string  _cpwComment;

        public int CapitalWorkID
        {
            get { return _cpwID;}
            set { _cpwID = value;}
        }
        public string CapitalWorkJobCostNumber
        {
            get { return _cpwJobCostNumber; }
            set { _cpwJobCostNumber = value; }
        }
        public string CapitalWorkYear
        {
            get { return _cpwYear; }
            set { _cpwYear = value; }
        }
        public int CapitalWorkMonth
        {
            get { return _cpwMonth; }
            set { _cpwMonth = value; }
        }
        public string ManagerID
        {
            get { return _cpwManagerID; }
            set { _cpwManagerID = value; }
        }
        public string ServicePlanID
        {
            get { return _cpwServicePlanID; }
            set { _cpwServicePlanID = value; }
        }
        public string ThemeID
        {
            get { return _cpwThemeID; }
            set { _cpwThemeID = value; }
        }
        public string StrategyObjectiveID
        {
            get { return _cpwStrategyObjID; }
            set { _cpwStrategyObjID = value; }
        }
        public string Description
        {
            get { return _cpwDescription; }
            set { _cpwDescription = value; }
        }
        public double OriginalBudget
        {
            get { return _cpwOrgBudget; }
            set { _cpwOrgBudget = value; }
        }
        public double ProjectedBudget
        {
            get { return _cpwPrjBudget; }
            set { _cpwPrjBudget = value; }
        }
        public double YearToDate
        {
            get { return _cpwytd; }
            set {
                double ytd;
                bool isDouble = Double.TryParse(value.ToString(), out ytd);
                if (!isDouble)
                {
                    throw new Exception("YTD is NOT Valid value");
                }
                _cpwytd = value; }
        }

        public double RevisedBudget
        {
            get { return _cpwReviseBudget; }
            set { _cpwReviseBudget = value; }
        }
        public double PercentageCompled
        {
            get { return _cpwCompletedPercentage; }
            set { _cpwCompletedPercentage = value; }
        }
        public string MonthlyComment
        {
            get { return _cpwComment; }
            set { _cpwComment = value; }
        }

        public CapitalWork() { }
        public CapitalWork(int id) { this._cpwID = id; }

        public bool InsertCWP(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //Check is empty, if yes then get next cpw id
            if(this._cpwID == 0) { this._cpwID = getNextCPWIndex(); }

            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("capital_works_jobno", this._cpwJobCostNumber);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("ManagerID", this._cpwManagerID);
            strdct.Add("ServicePlanID", this._cpwServicePlanID);
            //strdct.Add("ThemeID", this._cpwThemeID);
            strdct.Add("StrategyObjID", this._cpwStrategyObjID);
            strdct.Add("Description", this._cpwDescription);
            strdct.Add("Budget", this._cpwOrgBudget);
            //strdct.Add("LastRevisedBudget", this._cpwReviseBudget);

            if (string.IsNullOrEmpty(this._cpwYear)) {
                throw new Exception("Capital Work Year cannot be Empty");
            }
            if (string.IsNullOrEmpty(this._cpwManagerID)) {
                throw new Exception("Responsible Manager cannot be Empty");
            }
            if (string.IsNullOrEmpty(this._cpwServicePlanID)) {
                throw new Exception("Service Plan cannot be Empty");
            }
            if (string.IsNullOrEmpty(this._cpwDescription)) {
                throw new Exception("Capital Work Program Description cannot be Empty");
            }

            string query = @"INSERT INTO capital_works (capital_works_id, capital_works_jobno, capital_works_year, capital_works_manager_id,"
                + " capital_works_service_plann_id, capital_works_stg_obj_id, capital_works_description,"
                + " capital_works_original_budget) VALUES (@CPW_ID, @capital_works_jobno, @CPW_Year, @ManagerID, @ServicePlanID,"
                + " @StrategyObjID, @Description, @Budget);";
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
        public bool UpdateCWP(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("ManagerID", this._cpwManagerID);
            strdct.Add("ServicePlanID", this._cpwServicePlanID);
            strdct.Add("StrategyObjID", this._cpwStrategyObjID);
            strdct.Add("Description", this._cpwDescription);
            strdct.Add("Budget", this._cpwOrgBudget);
            strdct.Add("RevisedBudget", this._cpwReviseBudget);

            if (string.IsNullOrEmpty(this._cpwYear))
            {
                throw new Exception("Capital Work Year cannot be Empty");
            }
            if (string.IsNullOrEmpty(this._cpwManagerID))
            {
                throw new Exception("Responsible Manager cannot be Empty");
            }
            if (string.IsNullOrEmpty(this._cpwServicePlanID))
            {
                throw new Exception("Service Plan cannot be Empty");
            }
            if (string.IsNullOrEmpty(this._cpwDescription))
            {
                throw new Exception("Capital Work Program Description cannot be Empty");
            }

            string query = @"UPDATE capital_works SET capital_works_manager_id = @ManagerID, capital_works_service_plann_id = @ServicePlanID,"
                + " capital_works_stg_obj_id = @StrategyObjID, capital_works_description = @Description,"
                + " capital_works_original_budget = @Budget"
                + " WHERE capital_works_id = " + this._cpwID + ";";
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
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("CPW_Month", this._cpwMonth);
            strdct.Add("CPW_Projected", this._cpwPrjBudget);
            strdct.Add("CPW_Percentage", this._cpwCompletedPercentage);
            strdct.Add("CPW_Remark", this._cpwComment);

            DataTable tb;
            string strsql = " Select COUNT(*) As noofrecs From capital_works_monthly_progress Where"
                            + " capital_works_id = " + this._cpwID + " And capital_works_year = '" + this._cpwYear + "' And capital_works_month = " + this._cpwMonth;
            tb = db.GetDataTable(con, strsql);

            if (int.Parse(tb.Rows[0]["noofrecs"].ToString()) > 0)
            { UpdateCWPMonthly(db, con, trans, strdct); }
            else { InsertCWPMonthly(db, con, trans, strdct); }


            return result;
        }
        private bool InsertCWPMonthly(Database db, DbConnection con, DbTransaction trans, Dictionary<string, dynamic> dict)
        {
            bool result = false;
            //Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //strdct.Add("CPW_ID", this._cpwID);
            //strdct.Add("CPW_Year", this._cpwYear);
            //strdct.Add("CPW_Month", this._cpwMonth);
            //strdct.Add("CPW_Projected", this._cpwPrjBudget);
            //strdct.Add("CPW_Percentage", this._cpwCompletedPercentage);
            //strdct.Add("CPW_Remark", this._cpwComment);

            string query = @"INSERT INTO capital_works_monthly_progress (capital_works_id, capital_works_year,"
                + " capital_works_month, capital_works_projected, capital_works_percentage, capital_works_remark) VALUES"
                + " (@CPW_ID, @CPW_Year, @CPW_Month, @CPW_Projected, @CPW_Percentage, @CPW_Remark);";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, dict);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }
        private bool UpdateCWPMonthly(Database db, DbConnection con, DbTransaction trans, Dictionary<string, dynamic> dict)
        {
            bool result = false;
            //Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            //strdct.Add("CPW_ID", this._cpwID);
            //strdct.Add("CPW_Year", this._cpwYear);
            //strdct.Add("CPW_Month", this._cpwMonth);
            //strdct.Add("CPW_Projected", this._cpwPrjBudget);
            //strdct.Add("CPW_Percentage", this._cpwCompletedPercentage);
            //strdct.Add("CPW_Remark", this._cpwComment);

            string query = @"UPDATE capital_works_monthly_progress SET capital_works_projected=@CPW_Projected,"
                + " capital_works_percentage=@CPW_Percentage, capital_works_remark=@CPW_Remark"
                + " WHERE capital_works_month=@CPW_Month and capital_works_id=@CPW_ID and capital_works_year=@CPW_Year;";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, dict);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }

        public bool InsertCWPYTD(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("CPW_Month", this._cpwMonth);
            strdct.Add("CPW_ytd", this._cpwytd);

            string query = @"INSERT INTO capital_works_ytd (capital_works_id, capital_works_year,"
                + " capital_works_month, capital_works_ytd) VALUES"
                + " (@CPW_ID, @CPW_Year, @CPW_Month, @CPW_ytd);";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved to YTD Table, Please Contact IT");
            }

            return result;

        }
        public bool UpdateCWPYTD(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("CPW_Month", this._cpwMonth);
            strdct.Add("CPW_ytd", this._cpwytd);

            string query = @"UPDATE capital_works_ytd SET capital_works_ytd = @CPW_ytd"
                + " WHERE capital_works_month = @CPW_Month and capital_works_id = @CPW_ID and capital_works_year = @CPW_Year;";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Update Error YTD Table");
            }

            return result;
        }

        public bool InsertCWPQBR(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("CPW_Quarter", OPGlobals.GetQuarter(this._cpwMonth));
            strdct.Add("CPW_Revised", this._cpwReviseBudget);

            string query = @"INSERT INTO capital_works_qbr (capital_works_id, capital_works_year,"
                + " capital_works_quarter, capital_works_revised_budget) VALUES"
                + " (@CPW_ID, @CPW_Year, @CPW_Quarter, @CPW_Revised);";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved to QBR Table, Please Contact IT");
            }

            return result;

        }
        public bool UpdateCWPQBR(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("CPW_ID", this._cpwID);
            strdct.Add("CPW_Year", this._cpwYear);
            strdct.Add("CPW_Quarter", OPGlobals.GetQuarter(this.CapitalWorkMonth));
            strdct.Add("CPW_Revised", this._cpwReviseBudget);

            string query = @"UPDATE capital_works_qbr SET capital_works_revised_budget = @CPW_Revised"
                + " WHERE capital_works_id = @CPW_ID AND capital_works_year = @CPW_Year AND capital_works_quarter = @CPW_Quarter;";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Update Error QBR Table");
            }

            return result;
        }

        public bool DeleteCWP(Database db, DbConnection con, DbTransaction trans, string cpwID)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();
            string query = "";
            strdct.Add("CPW_ID", cpwID);
            try
            {
                //--- Delete From Mail Table
                query = @"DELETE FROM capital_works WHERE capital_works_id = @CPW_ID;";
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);

                //--- Delete From Monthly Progress Table
                query = @"DELETE FROM capital_works_monthly_progress WHERE capital_works_id = @CPW_ID;";
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);

                //--- Delete From Monthly QBR Table
                query = @"DELETE FROM capital_works_qbr WHERE capital_works_id = @CPW_ID;";
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);

                //--- Delete From YTD Table
                query = @"DELETE FROM capital_works_ytd WHERE capital_works_id = @CPW_ID;";
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }

        public int getNextCPWIndex()
        {
            Database db1 = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            using ( DbConnection conn = db1.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString))
            {
                string strsql = "SELECT capital_works_id FROM capital_works ORDER BY capital_works_id DESC LIMIT 1;";
                DataTable tb = db1.GetDataTable(conn, strsql);
                if (tb.Rows.Count > 0) { return int.Parse(tb.Rows[0][0].ToString()) + 1; } else { return 1; }
            }
        }
        public static int getNextCPWIndexStatic()
        {
            Database db1 = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            using (DbConnection conn = db1.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString))
            {
                string strsql = "SELECT capital_works_id FROM capital_works ORDER BY capital_works_id DESC LIMIT 1;";
                DataTable tb = db1.GetDataTable(conn, strsql);
                if (tb.Rows.Count > 0) { return int.Parse(tb.Rows[0][0].ToString()) + 1; } else { return 1; }
            }
        }
        public static HashSet<string> getCPWNotinOP(string filePathtoExcelwithJobNos) {
            HashSet<string> cpwJobNos = new HashSet<string>();
            HashSet<String> CPWDatabase = getCPWJobNumbersFromDatabase();
            HashSet<String> CPWExcel = getCPWJobNumbersfromExcel(filePathtoExcelwithJobNos);
            return new HashSet<String>(CPWExcel.Except(CPWDatabase));
        }
        private static HashSet<string> getCPWJobNumbersfromExcel(string filePath)
        {
            HashSet<string> temp = new HashSet<string>();
            HashSet<string> duplicates = new HashSet<string>();
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = xlWorkbook.Sheets[1]; // assume it is the first sheet
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange; // get the entire used range
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!         

            for (int i = 2; i <= rowCount; i++)
            {
                try
                {
                    if (!temp.Add(xlRange.Cells[i, 1].Value2.ToString().Trim()))
                    {
                        duplicates.Add(xlRange.Cells[i, 1].Value2.ToString().Trim());
                    }

                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return temp;
        }
        private static HashSet<string> getCPWJobNumbersFromDatabase()
        {
            HashSet<string> temp = new HashSet<string>();
            DbConnection conn = OPGlobals.db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = OPGlobals.db.GetDataTable(conn, "SELECT capital_works_jobno FROM nsc_operation_plan_17_to_21.capital_works;");
            foreach (DataRow row in tb.Rows)
            {
                try
                {
                    temp.Add(row["capital_works_jobno"].ToString());
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return temp;
        }
    
        public static DataTable GetTableCapitalWorksDepartmentSummary(string cpw_year, int cpw_month)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            //string strsql = "Select A.manager_dept, A.manager_subdept as manager_description, A.cpw_year,"
            //    + cpw_month + " As capital_works_month,"
            //    + " Sum(A.cpw_original_budget) As sum_cpw_org, Sum(B.capital_works_ytd) As sum_cpw_ytod From"
            //    + " (Select capital_works_ytd.* From capital_works_ytd Where"
            //    + " capital_works_ytd.capital_works_month = " + cpw_month + " And"
            //    + " capital_works_ytd.capital_works_year = '" + cpw_year + "') B Right Join"
            //    + " view_cpw A On A.cpw_id = B.capital_works_id And A.cpw_year = B.capital_works_year"
            //    + " Group By A.manager_dept, A.manager_subdept, A.cpw_year;";

            string strsql = "Select A.manager_dept, A.manager_subdept As manager_description, A.cpw_year,"
                + cpw_month + " As cpw_month, Sum(A.cpw_original_budget)As sum_cpw_org,"
                + " Sum(B.capital_works_ytd) As sum_cpw_ytod, Sum(C.capital_works_revised_budget) as sum_cpw_revised From"
                + " (Select capital_works_ytd.* From capital_works_ytd Where"
                + " capital_works_month = " + cpw_month + " And capital_works_year = '" + cpw_year + " ') B Right Join"
                + " view_cpw A On A.cpw_id = B.capital_works_id And A.cpw_year = B.capital_works_year Left Join"
                + " (Select capital_works_id, capital_works_year, capital_works_quarter, capital_works_revised_budget From capital_works_qbr"
                + " Where capital_works_qbr.capital_works_quarter = " + OPGlobals.GetQuarter(cpw_month) + ") C"
                + " On A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year Group By"
                + " A.manager_dept, A.manager_subdept, A.cpw_year";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);
            return tb;
        }
        public static DataTable GetTableCapitalWorksDepartmentSummary(string cpw_year, int cpw_month, string directorate)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            //string strsql = "Select A.manager_dept, A.manager_subdept as manager_description, A.cpw_year,"
            //                + cpw_month + " As capital_works_month,"
            //                + " Sum(A.cpw_original_budget) As sum_cpw_org, Sum(B.capital_works_ytd)As sum_cpw_ytod From"
            //                + " (Select capital_works_ytd.* From capital_works_ytd Where"
            //                + " capital_works_ytd.capital_works_month = " + cpw_month + " And"
            //                + " capital_works_ytd.capital_works_year = '" + cpw_year + "') B Right Join"
            //                + " view_cpw A On A.cpw_id = B.capital_works_id And A.cpw_year = B.capital_works_year"
            //                 + " WHERE A.director_id ='" + directorate + "'"
            //                + " Group By A.manager_dept, A.manager_subdept, A.cpw_year;";

            string strsql = "Select A.manager_dept, A.manager_subdept, A.cpw_year," + cpw_month + " As cpw_month,"
                + " Count(A.manager_subdept)As noofrecs, Sum(A.cpw_original_budget) As sum_cpw_org, Sum(C.capital_works_revised_budget) As sum_cpw_revised,"
                + " Sum(B.capital_works_ytd) As cpw_ytod, Sum(D.capital_works_percentage) As sum_cpw_percentage From view_cpw A Left Join"
                + " (Select capital_works_ytd.* From capital_works_ytd Where"
                + " capital_works_month = " + cpw_month + " And capital_works_year = '" + cpw_year + "') B"
                + " On A.cpw_id = B.capital_works_id And A.cpw_year = B.capital_works_year Left Join"
                + " (Select capital_works_id, capital_works_year, capital_works_quarter, capital_works_revised_budget From capital_works_qbr"
                + " Where capital_works_qbr.capital_works_quarter = " + OPGlobals.GetQuarter(cpw_month) + ") C"
                + " On A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year Left Join"
                + " (Select capital_works_monthly_progress.* From capital_works_monthly_progress Where"
                + " capital_works_month = " + cpw_month + " AND capital_works_year='" + cpw_year + "') D"
                + " On A.cpw_id = D.capital_works_id And A.cpw_year = D.capital_works_year"
                + " WHERE A.director_id ='" + directorate + "'"
                + " Group By A.manager_dept, A.manager_subdept, A.cpw_year";


            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);
            return tb;
        }
        public static DataTable GetTableCapitalWorksServiceSummary(string cpw_year, int cpw_month)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            //string strsql = "SELECT A.service_plan, A.cpw_year, " + cpw_month + " As cpw_month,"
            //    + " COUNT(A.service_plan) AS noofrecs,"
            //    + " Sum(A.cpw_original_budget) As sum_cpw_org, Sum(C.capital_works_revised_budget) As sum_cpw_revised,"
            //    + " sum(B.capital_works_ytd) as sum_cpw_ytod From view_cpw A Left Join"
            //    + " (Select capital_works_ytd.* From capital_works_ytd Where"
            //    + " capital_works_ytd.capital_works_month = " + cpw_month + " And"
            //    + " capital_works_ytd.capital_works_year = '" + cpw_year + "') B On A.cpw_id = B.capital_works_id And A.cpw_year = B.capital_works_year Left Join"
            //    + " (Select capital_works_id, capital_works_year, capital_works_quarter, capital_works_revised_budget"
            //    + " From capital_works_qbr Where capital_works_qbr.capital_works_quarter = " + OPGlobals.GetQuarter(cpw_month) + ") C"
            //    + " On A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year"
            //    + " Group By A.service_plan, A.cpw_year, cpw_month;";

            string strsql = "Select A.service_plan, A.cpw_year, " + cpw_month + " As cpw_month, Count(A.manager_subdept)As noofrecs,"
                + " Sum(A.cpw_original_budget) As sum_cpw_org, Sum(C.capital_works_revised_budget) As sum_cpw_revised,"
                + " Sum(B.capital_works_ytd) As cpw_ytod, Sum(D.capital_works_percentage) As sum_cpw_percentage"
                + " From view_cpw A Left Join (Select capital_works_ytd.* From capital_works_ytd Where"
                + " capital_works_month = " + cpw_month + " And capital_works_year = '" + cpw_year + " ') B"
                + " On A.cpw_id = B.capital_works_id And A.cpw_year = B.capital_works_year Left Join"
                + " (Select capital_works_id, capital_works_year, capital_works_quarter, capital_works_revised_budget From capital_works_qbr"
                + " Where capital_works_qbr.capital_works_quarter = " + OPGlobals.GetQuarter(cpw_month) + " ) C"
                + " On A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year Left Join"
                + " (Select capital_works_monthly_progress.* From capital_works_monthly_progress Where"
                + " capital_works_month = " + cpw_month + " AND capital_works_year='" + cpw_year + "') D On"
                + " A.cpw_id = D.capital_works_id And A.cpw_year = D.capital_works_year Group By A.service_plan, A.cpw_year";

            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);

            return tb;
        }
        public static DataTable GetTableCapitalWorksServiceDetails(string cpw_year, int cpw_month)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            string strsql = GetSQLCapitalWorksMonthlyProgress(cpw_year, cpw_month);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);

            return tb;
        }
        public static DataTable GetTableCapitalWorksServiceDetails(string cpw_year, int cpw_month, string servicePlanID )
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);

            string strsql = GetSQLCapitalWorksMonthlyProgress(cpw_year, cpw_month) + " where cpw_service_plann_id = '"+ servicePlanID + "'";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            DataTable tb = db.GetDataTable(conn, strsql);

            return tb;
        }

        public static string GetSQLCapitalWorksYTD(string cpw_year, int cpw_month)
        {
            string strsql = "";

            //strsql = "SELECT A.*, " + OPGlobals.GetQuarter(cpw_month) + " As cpw_quarter, C.capital_works_month As cpw_month,"
            //    + " C.capital_works_ytd As cpw_ytd From view_cpw A Left Join"
            //    + " (Select capital_works_ytd.* From capital_works_ytd"
            //    + " Where capital_works_ytd.capital_works_month = " + cpw_month
            //    + " And capital_works_ytd.capital_works_year = '" + cpw_year + "') C On"
            //    + " A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year";

            strsql = "SELECT A.*, C.capital_works_month As cpw_month,"
                + " C.capital_works_ytd As cpw_ytd From view_cpw_qbr A Left Join"
                + " (Select capital_works_ytd.* From capital_works_ytd"
                + " Where capital_works_ytd.capital_works_month = " + cpw_month
                + " And capital_works_ytd.capital_works_year = '" + cpw_year + "') C On"
                + " A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year";
            return strsql;
        }
        public static DataTable GetTableCapitalWorksYTD(string cpw_year, int cpw_month)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            string strsql = GetSQLCapitalWorksYTD(cpw_year, cpw_month)
                + " ORDER BY A.director_id, A.cpw_manager_id, A.cpw_id;";
            DataTable tb = db.GetDataTable(conn, strsql);
            return tb;
        }

        public static string GetSQLCapitalWorksMonthlyProgress(string cpw_year, int cpw_month)
        {
            string strsql = "";
            int qtr = OPGlobals.GetQuarter(cpw_month);

            strsql = "Select A.*, " + cpw_month + " As cpw_month, C.capital_works_ytd As cpw_ytod, D.capital_works_projected As cpw_projected,"
                + " D.capital_works_percentage As cpw_percentage, D.capital_works_remark As cpw_remark"
                + " From view_cpw_qbr A Left Join (Select capital_works_ytd.* From capital_works_ytd"
                + " Where capital_works_ytd.capital_works_month = " + cpw_month
                + " And capital_works_ytd.capital_works_year = '" + cpw_year + " ') C On A.cpw_id = C.capital_works_id And A.cpw_year = C.capital_works_year Left Join"
                + " (Select capital_works_monthly_progress.* From capital_works_monthly_progress"
                + " Where capital_works_monthly_progress.capital_works_month = " + cpw_month
                + " And capital_works_monthly_progress.capital_works_year = '" + cpw_year + "') D"
                + " On A.cpw_id = D.capital_works_id And A.cpw_year = D.capital_works_year";

            return strsql;
        }
        public static string GetSQLCapitalWorksMonthlyProgress(string servicePlanID, string cpw_year, int cpw_month)
        {
            string strsql = GetSQLCapitalWorksMonthlyProgress(cpw_year, cpw_month);
            strsql = strsql + " WHERE cpw_service_plann_id != '000'";

            if (!string.IsNullOrEmpty(servicePlanID) && servicePlanID != "000")
            {
                strsql = strsql + " AND cpw_service_plann_id = '" + servicePlanID + "'";
            }
            return strsql;
        }

        public static DataTable GetTableCapitalWorksMonthlyProgress(string cpw_year, int cpw_month)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            string strsql = GetSQLCapitalWorksMonthlyProgress(cpw_year, cpw_month) 
                + " ORDER BY A.director_id, A.cpw_manager_id, A.cpw_id;";
            DataTable tb = db.GetDataTable(conn, strsql);
            return tb;
        }
        public static DataTable GetTableCapitalWorksMonthlyProgress(string cpw_year, int cpw_month, string serviceID)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            string strsql = GetSQLCapitalWorksMonthlyProgress(cpw_year, cpw_month);

            if (!string.IsNullOrEmpty(serviceID) && serviceID != "000")
            {
                strsql = strsql + " WHERE A.cpw_service_plann_id = '" + serviceID + "'";
            }
            strsql = strsql + " ORDER BY A.cpw_service_plann_id;";
            DataTable tb = db.GetDataTable(conn, strsql);
            return tb;
        }
        public static DataTable GetTableCapitalWorksMonthlyProgress(string cpw_year, int cpw_month, string directorId, string managerId)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            string strsql = GetSQLCapitalWorksMonthlyProgress(cpw_year, cpw_month);

            if (string.IsNullOrEmpty(managerId) || managerId == "-0-")
            {
                strsql = strsql + " WHERE director_id = '" + directorId + "'";
            }
            else
            {
                strsql = strsql + " WHERE director_id = '" + directorId + "' AND cpw_manager_id = '" + managerId + "'";
            }

            DataTable tb = db.GetDataTable(conn, strsql);
            return tb;
        }
    }
}
