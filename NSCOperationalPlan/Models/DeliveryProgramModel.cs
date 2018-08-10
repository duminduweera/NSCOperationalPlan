using MyDLLs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    public class DeliveryProgramModel : BaseViewModel
    {
        private string mID;
        private string mDescription;
        private string mSource;
        private string mHowMeasured;
        private string mYear;
        private string mStrategyID;
        private string mDirectorID;
        private string mManagerID;
        private string mPrefix;
        private double mTargetValue;
        private string mUnit;
        private string mReportFrequency;

        public string ID
        {
            get { return mID; }
            set
            {
                if (mID == value) return;
                mID = value;
                OnPropertyChange(nameof(ID));
            }
        }
        public string Description
        {
            get { return mDescription; }
            set
            {
                if (mDescription == value) return;
                mDescription = value;
                OnPropertyChange(nameof(Description));
            }
        }
        public string Source
        {
            get { return mSource; }
            set
            {
                if (mSource == value) return;
                mSource = value;
                OnPropertyChange(nameof(Source));
            }
        }
        public string HowMeasured
        {
            get { return mHowMeasured; }
            set
            {
                if (mHowMeasured == value) return;
                mHowMeasured = value;
                OnPropertyChange(nameof(HowMeasured));
            }
        }
        public string Year
        {
            get { return mYear; }
            set
            {
                if (mYear == value) return;
                mYear = value;
                OnPropertyChange(nameof(Year));
            }
        }
        public string StrategyID
        {
            get { return mStrategyID; }
            set
            {
                if (mStrategyID == value) return;
                mStrategyID = value;
                OnPropertyChange(nameof(StrategyID));
            }
        }
        public string DirectorID
        {
            get { return mDirectorID; }
            set
            {
                if (mDirectorID == value) return;
                mDirectorID = value;
                OnPropertyChange(nameof(DirectorID));
            }
        }
        public string ManagerID
        {
            get { return mManagerID; }
            set
            {
                if (mManagerID == value) return;
                mManagerID = value;
                OnPropertyChange(nameof(ManagerID));
            }
        }
        public string Prefix
        {
            get { return mPrefix; }
            set
            {
                if (mPrefix == value) return;
                mPrefix = value;
                OnPropertyChange(nameof(Prefix));
            }
        }
        public double TargetValue
        {
            get { return mTargetValue; }
            set
            {
                if (mTargetValue != value)
                {
                    mTargetValue = value;
                    OnPropertyChange(nameof(TargetValue));
                }
            }
        }
        public string Unit  
        {
            get { return mUnit; }
            set
            {
                if (mUnit == value) return;
                mUnit = value;
                OnPropertyChange(nameof(Unit));
            }
        }
        public string ReportFrequency
        {
            get { return mReportFrequency; }
            set
            {
                if (mReportFrequency == value) return;
                mReportFrequency = value;
                OnPropertyChange(nameof(ReportFrequency));
            }
        }

        public DeliveryProgramModel() { }


        private Dictionary<string, object> FillDictionary()
        {
            Dictionary<string, object> strdct = new Dictionary<string, object>();

            strdct.Add("ID", this.mID);
            strdct.Add("Description", this.mDescription);
            strdct.Add("Source", this.mSource);
            strdct.Add("HowMeasured", this.mHowMeasured);
            strdct.Add("Year", this.mYear);
            strdct.Add("DirectorID", this.mDirectorID);
            strdct.Add("ManagerID", this.mManagerID);
            strdct.Add("StrategyID", this.mStrategyID);
            strdct.Add("Prefix", this.mPrefix);
            strdct.Add("Target", this.mTargetValue);
            strdct.Add("UnitID", this.mUnit);
            strdct.Add("ReportFrequency", this.mReportFrequency);

            return strdct;
        }

        public bool InsertData(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = FillDictionary();

            string query = @"INSERT INTO strategy_measure (strategy_measure_code, description, source, how_measured, year, director_id,"
                + " strategy_id, manager_id, prefix_id, target, unit_id, report_frequency) VALUES"
                + " (@ID, @Description, @Source, @HowMeasured, @Year, @DirectorID, @StrategyID, @ManagerID, @Prefix, @Target, @UnitID, @ReportFrequency);";
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
        public bool UpdateData(Database db, DbConnection con, DbTransaction trans)
        {
            bool result = false;
            Dictionary<string, dynamic> strdct = FillDictionary();


            string query = @"UPDATE strategy_measure SET description = @Description, source = @Source,"
                + " how_measured = @HowMeasured, director_id = @DirectorID, strategy_id = @StrategyID," 
                + " manager_id = @ManagerID, prefix_id = @Prefix, target = @Target, unit_id = @UnitID, report_frequency=@ReportFrequency"
                + " WHERE strategy_measure_code = @ID";
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
        public bool IsExist()
        {
            return true;
        }

        public bool DataValidate()
        {
            if (string.IsNullOrEmpty(this.mID)) { throw new NullReferenceException("ID Cannot be Empty"); }
            if (string.IsNullOrEmpty(this.mDescription)) { throw new NullReferenceException("Description Cannot be Empty"); }
            if (string.IsNullOrEmpty(this.mYear)) { throw new NullReferenceException("Financial year Cannot be Empty"); }
            if (string.IsNullOrEmpty(this.mStrategyID)) { throw new NullReferenceException("Strategy Cannot be Empty"); }
            if (string.IsNullOrEmpty(this.mManagerID)) { throw new NullReferenceException("Responsible Officer Cannot be Empty"); }

            return true;
        }

    }
}
