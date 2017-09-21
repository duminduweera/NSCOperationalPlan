using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    public class clsUser
    {
        string _userid;
        string _username;
        string _loginName;
        UserRights _permission;
        List<string> _disableMenuOptions;
        string _managerid;
        string _directorid;
        string mDepartment;
        string mDivision;

        public string UserID
        {
            get { return _userid;}
            set { _userid = value; }
        }
        public string DirectorID
        {
            get { return _directorid; }
            set { _directorid = value; }
        }
        public string ManagerID
        {
            get { return _managerid; }
            set { _managerid = value; }
        }
        public List<string> DisableMenuOptions
        {
            get { return _disableMenuOptions; }
            set { _disableMenuOptions = value; }
        }

        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        public UserRights Permission
        {
            get { return _permission; }
            set { _permission = value; }
        }

        public string Department
        {
            get { return mDepartment; }
            set { mDepartment = value; }
        }

        public string Division
        {
            get { return mDivision; }
            set { mDivision = value; }
        }

        public clsUser() { }
        public clsUser(string userid, string username, UserRights userrights, string loginName, string directorid)
        {
            this._userid = userid;
            this._username = username;
            this._permission = userrights;
            this._loginName = loginName;
            this._directorid = directorid;
        }
    }
}
