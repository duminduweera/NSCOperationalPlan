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
        private String _measureCode, _description,_source,_howMeasured,_year,manager_id,strategy_id,_comment;

        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                _comment = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        internal static DataTable getMeasures(String managerID,String year)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            String strsql = "SELECT * FROM view_strategy_measure WHERE manager_id='" + managerID + "' and year='" + year + "' ORDER BY theme_id,strategy_objective_id,rank;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            return db.GetDataTable(conn, strsql);
        }

        internal static DataTable getAllMeasures(String year)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            String strsql = "SELECT * FROM view_strategy_measure WHERE year='" + year + "' ORDER BY theme_id,strategy_objective_id,rank;";
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            return db.GetDataTable(conn, strsql);
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


    }
}
