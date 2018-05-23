using MyDLLs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    public class Units : INotifyPropertyChanged
    {
        #region Private fields
        private string mCode;
        private string mShortDescription;
        private string mDescription;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Properties
        public string Code
        {
            get { return mCode; }
            set
            {
                mCode = value;
                OnPropertyChange("Code");
            }
        }
        public string ShortDescription
        {
            get { return mShortDescription; }
            set { mShortDescription = value; }
        }

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        #endregion

        #region Constructor
        public Units(string code, string shortDesciption, string description) :this()
        {
            this.mCode = code;
            this.mShortDescription = shortDesciption;
            this.mDescription = description;
        }
        public Units(){ }
        #endregion

        public void OnPropertyChange(string propertyName)
        {
            if(PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #region Database Operations

        private Dictionary<string, dynamic> FillDictionary()
        {
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            if (int.Parse(this.mCode) == -1) { this.mCode = GetNextUnitCode().ToString(); }

            if (string.IsNullOrEmpty(this.mShortDescription)) { throw new Exception("Short Description cannot be Empty"); }
            if (string.IsNullOrEmpty(this.mDescription)) { throw new Exception("Description cannot be Empty"); }

            strdct.Add("Code", this.mCode);
            strdct.Add("Short_Description", this.mShortDescription);
            strdct.Add("Description", this.mDescription);

            return strdct;

        }

        public bool InsertUnit(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;

            try
            {
                Dictionary<string, dynamic> strdct = FillDictionary();

                string query = @"INSERT INTO kpi_units (kpi_unit_id, kpi_unit_short, kpi_unit) VALUES (@Code, @Short_Description, @Description);";
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }
        public bool UpdateUnit(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;

            try
            {
                Dictionary<string, dynamic> strdct = FillDictionary();
                string query = @"UPDATE kpi_units SET kpi_unit_short = @Short_Description, kpi_unit = @Description"
                    + " WHERE kpi_unit_id = @Code";

                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }

            return result;
        }

        /// <summary>
        /// ========== Get Next Unit Code ========
        /// </summary>
        /// <returns></returns>
        public int GetNextUnitCode()
        {
            string strsql = "SELECT Cast(kpi_unit_id AS UNSIGNED) as id FROM kpi_units ORDER BY id DESC LIMIT 1";

            DbConnection conn = OPGlobals.db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = OPGlobals.db.GetDataTable(conn, strsql);

            if (tb.Rows.Count > 0) { return int.Parse(tb.Rows[0][0].ToString()) + 1; } else { return 1; }
        }

        public bool IsExist(string code)
        {
            string strsql = "SELECT COUNT(*) as NoofRecs FROM kpi_units WHERE kpi_unit_id ='" + code + "'";
            

            DbConnection conn = OPGlobals.db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = OPGlobals.db.GetDataTable(conn, strsql);

            return (int.Parse(tb.Rows[0][0].ToString()) > 0) ? true : false;

            //if (int.Parse(tb.Rows[0][0].ToString()) > 0) { return true; } else { return false; }
        }

        #endregion

    }
}
