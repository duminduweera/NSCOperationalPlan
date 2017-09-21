using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using MyDLLs;
using System.Data;

namespace NSCOperationalPlan
{
    class batchCopyProgress
    {
        private Database _db;  // = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
        private DbConnection _conn; // = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
        private DataTable _table;

        public Database DB
        {
            set { _db = value; }
        }
        public DbConnection CONN
        {
            set { _conn = value; }
        }
        public DataTable TABLE
        {
            set { _table = value; }
        }

        public batchCopyProgress() { }

        public void StartCopy()
        {
            //INSERT INTO progress(progress_year, progress_month, progress_pecentage, progress_description, status_id, action_id)  SELECT '16/17', 09, progress_pecentage, progress_description, status_id, action_id FROM progress WHERE progress_year = '16/17' and progress_month = 8

        }

    }
}
