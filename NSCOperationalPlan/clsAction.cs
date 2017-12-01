using System;
using System.Collections.Generic;
using MyDLLs;
using System.Data.Common;

namespace NSCOperationalPlan
{
    class clsAction
    {
        public string ActionID { get; set; }
        public string StrategyID { get; set; }
        public string Action { get; set; }
        public string ManagerID { get; set; }
        public string ServicePlanID { get; set; }
        public string CouncilPlanID { get; set; }

        public clsAction() { }
        public clsAction(string actionID) : this()
        {
            this.ActionID = actionID;
        }


        public void ChangeManagerAction(Database db, DbConnection con, DbTransaction trans, string toManagerID)
        {
            Dictionary<string, dynamic> strdct = new Dictionary<string, dynamic>();

            strdct.Add("Action_ID", this.ActionID);
            strdct.Add("ManagerIDFrom", this.ManagerID);
            strdct.Add("ManagerIDTo", toManagerID);

            string query = @"UPDATE action SET manager_id = @ManagerIDTo WHERE id = @Action_ID and manager_id = @ManagerIDFrom";
            try
            {
                db.InsertUpdateDeleteRecord(con, trans, query, strdct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + "Data NOT Saved, Please Contact IT");
            }
        }

    }
}
