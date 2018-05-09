using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using MyDLLs;
using NSCUtils;
using System.IO;
using System.DirectoryServices;
using System.Diagnostics;

namespace NSCOperationalPlan
{
    enum Months
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    //public enum UserRights
    //{
    //    Administrator = 0,
    //    Director = 1,
    //    Manager = 2,
    //    Finance = 11,
    //    AdminOfficer = 12,
    //    Editor = 13,
    //    User = 99
    //}
    public enum UserRights
    {
        Administrator = 0,
        GM = 9,
        Director = 10,
        Editor = 15,
        Finance = 20,
        Manager = 25,
        UserInheritFromManager = 26,
        //AdminOfficer = 50,
        User = 99
        
    }
    static class Program
    {
        public static readonly bool AD_INTERGRATE = true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                //----------Check if another instance of this application is already running---
                //if (Process.GetProcessesByName("NSCOperationalPlan").Length > 0)
                //{
                //    throw new Exception("Another Instance of this Program is already running");
                //}

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting;

                OPGlobals.dbProvider = "MySql.Data.MySqlClient";
                OPGlobals.db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
                //OPGlobals.connString = "SERVER = nscutil; DATABASE = nsc_operation_plan_17_to_21; UID = sudinthap; PASSWORD = NSCv90cisc0;";
                OPGlobals.connString = "SERVER = nscutil; DATABASE = nsc_operation_plan_17_to_21; UID = opuser; PASSWORD = NSC@2390;";
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                OPGlobals.reportParth = Path.Combine(exeFolder, @"Reports\");

                //=========================================
                if (AD_INTERGRATE)
                {
                    //NSCUtils.ADUser u = new NSCUtils.ADUser("masonli");
                    //NSCUtils.ADUser u = new NSCUtils.ADUser("rossni");
                    //mccleti
                    NSCUtils.ADUser u = new ADUser();
                    if (string.IsNullOrEmpty(u.UserName))
                    {
                        throw new Exception("Sorry, You don't have enough permission to run this Program");
                    }
                    if (!LoadUser(u, "C"))
                    {
                        throw new Exception("Sorry, You don't have enough permission to run this Program");
                    }
                } else
                {

                }

                //Application.Run(new frmCPWMonthly());
                //Application.Run(new frmCPWQBR());

                Database db = null;
                string strsql = "";
                DbConnection conn = null;
                DataTable tb = null;

                db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
                //DBConnect db = new DBConnect();
                strsql = "SELECT * FROM tbl_params ORDER BY ID DESC;";
                conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
                tb = db.GetDataTable(conn, strsql);

                OPGlobals.currentYear = tb.Rows[0][1].ToString();
                OPGlobals.currentMonth = int.Parse(tb.Rows[0][2].ToString());

                //--- TODO ---
                //---  Remove this in production---
                //OPGlobals.currentMonth = OPGlobals.GetPreviousMonth(OPGlobals.currentMonth, OPGlobals.currentYear);
                //=================================



                OPGlobals.FinancialYearStarts = new DateTime(int.Parse(OPGlobals.currentYear.Substring(0, 2)), 7, 1);
                OPGlobals.CurrentQuarter = OPGlobals.GetQuarter(OPGlobals.currentMonth);

                if (tb.Rows.Count > 1)
                {
                    OPGlobals.prevoiusYear = tb.Rows[1][1].ToString();
                    OPGlobals.previousMonth = int.Parse(tb.Rows[1][2].ToString());
                }
                else
                {
                    OPGlobals.previousMonth = OPGlobals.GetPreviousMonth(OPGlobals.currentMonth, OPGlobals.currentYear);
                    OPGlobals.prevoiusYear = OPGlobals.GetPreviousYear(OPGlobals.currentMonth, OPGlobals.currentYear);
                }

                //string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                
                OPGlobals.reportParth = Path.Combine(exeFolder, @"Reports\");                
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //frmOperationPlan frmMDI = new frmOperationPlan();
                frmOperationPlan frmMDI = frmOperationPlan.getInstance();
                Application.Run(frmMDI);
            }
            catch (Exception ex)
            {
                CloseApplication(ex.Message, false);
            }
        }

        private static void disableMenuItems(clsUser aduser)
        {
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);
            DataTable tb = db.GetDataTable(conn, "SELECT * FROM program_settings;");
            if (aduser.LoginName.Equals("mccleti", StringComparison.OrdinalIgnoreCase)
                || aduser.LoginName.Equals("ratlesa", StringComparison.OrdinalIgnoreCase)
                || aduser.LoginName.Equals("perersu", StringComparison.OrdinalIgnoreCase)
                || aduser.LoginName.Equals("toddste", StringComparison.OrdinalIgnoreCase))
            //if (aduser.UserName.Equals("mccleti", StringComparison.InvariantCultureIgnoreCase)
            //    || aduser.UserName.Equals("ratlesa", StringComparison.InvariantCultureIgnoreCase)
            //    || aduser.Permission == UserRights.Administrator)
            {
                OPGlobals.CapitalWorksEnabled = true;
                OPGlobals.DeliveryProgramEnabled = true;
            }
            else
            {
                OPGlobals.CapitalWorksEnabled = Convert.ToBoolean(Convert.ToInt32(tb.Rows[0][0].ToString()));
                OPGlobals.DeliveryProgramEnabled = Convert.ToBoolean(Convert.ToInt32(tb.Rows[0][1].ToString()));
            }
        }


        public static bool OPLogin(string username, string password)
        {
            bool result = false;
            try
            {
                DirectoryEntry Ldap = new DirectoryEntry("LDAP://narrabri.nsw.gov.au", username, password);
                result = true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return result;
        }

        public static bool LoadUser(NSCUtils.ADUser aduser, string usertype)
        {
            bool result = false;

            Cursor.Current = Cursors.WaitCursor;
            clsUser user = new clsUser();

            user.UserName = aduser.DisplayName;
            user.LoginName = aduser.UserName;

            user.Department = aduser.Department;
            user.Division = aduser.Division;

            user.Permission = UserRights.User;

            user.UserID = aduser.UserName;

            try
            {

                if (SetManagerAndDirector(user, aduser.Division) == false)
                {
                    throw new Exception("User could Not be found or Wrong Supervisor. Please call IT Support to fix this..");
                }

                SetMenus(user);
                disableMenuItems(user);
                

#if DEBUG
                OPGlobals.CapitalWorksEnabled = true;
                OPGlobals.DeliveryProgramEnabled = true;

#endif

                //if (usertype == "P")
                //{
                //    OPGlobals.PreviousUser = OPGlobals.CurrentUser;
                //}

                OPGlobals.CurrentUser = user;
                result = true;
                Cursor.Current = Cursors.AppStarting;
            } catch (Exception ex)
            {
                Cursor.Current = Cursors.AppStarting;
                MessageBox.Show("ERROR IN LOGIN " + Environment.NewLine + ex.Message, "Operation Plan", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            Console.WriteLine(aduser.ToString());
            return result;

        }

        private static bool SetManagerAndDirector(clsUser user,  String division)
        {
            bool mRetVal = false;
            Database db = MyDLLs.MyDBFactory.GetDatabase(OPGlobals.dbProvider);
            DbConnection conn = db.CreateDbConnection(Database.ConnectionType.ConnectionString, OPGlobals.connString);

            string strsql = "SELECT * FROM manager WHERE manager_subdept = '" + division + "';";
            DataTable tb = db.GetDataTable(conn, strsql);

            if (tb.Rows.Count > 0)
            {

                user.DirectorID = tb.Rows[0]["manager_id"].ToString();
                user.ManagerID = tb.Rows[0]["id"].ToString();

                string mUserPermission = tb.Rows[0]["manager_permission"].ToString();

                user.Permission = (UserRights) int.Parse(mUserPermission);
                if(user.LoginName == tb.Rows[0]["manager_login_name"].ToString())
                {
                    user.UserID = tb.Rows[0]["manager_login_name"].ToString();
                } 

                mRetVal = true;
            }

            return mRetVal;
        }

        private static void SetMenus(clsUser user)
        {

            List<string> default_usermenu = new List<string> { "D01", "D02", "D03", "D06", "D07" };  //Disable Options
            List<string> temp = new List<string> { }; 
            switch (user.Permission)
            {
                case UserRights.Administrator:
                    //user.DisableMenuOptions = new List<string> { };
                    break;
                case UserRights.GM:
                    temp = new List<string> { "A07", "A08", "A07_1" };
                    break;
                case UserRights.Editor:
                    temp = new List<string> { "A07" };
                    //user.DisableMenuOptions = default_usermenu.Concat(temp).ToList();
                    break;
                case UserRights.Director:
                    temp = new List<string> { "A01", "A02", "A03", "A04", "A05", "A06", "A07", "A07_1", "A08" };
                    //user.DisableMenuOptions = default_usermenu.Concat(temp).ToList();
                    break;
                case UserRights.Manager:
                    temp = new List<string> { "A01", "A02", "A03", "A04", "A05", "A06", "A07", "A07_1", "A08" };
                    //user.DisableMenuOptions = default_usermenu.Concat(temp).ToList();
                    break;
                case UserRights.Finance:
                    temp = new List<string> { "A01", "A02", "A03", "A04", "A05","A06", "A08" };
                    //user.DisableMenuOptions = default_usermenu.Concat(temp).ToList();
                    break;
                default:
                    temp = new List<string> { "A01", "A02", "A03", "A04", "A05", "A06", "A07", "A07_1", "A08" };
                    //user.DisableMenuOptions = default_usermenu.Concat(temp).ToList();
                    break;
            }

            if (temp.Count > 0)
            {
                user.DisableMenuOptions = default_usermenu.Concat(temp).ToList();
            }

        }

        public static void CloseApplication(string msg, bool userclosed)
        {
            if (userclosed)
            {
                if (MessageBox.Show(msg, "OP PLAN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }
            } else {
                MessageBox.Show(msg, "OP PLAN", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            System.Windows.Forms.Application.Exit();
        }
    }
    
}
