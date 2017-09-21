using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace NSCOperationalPlan
{
    class ADUser
    {
        private string _username;
        private string _fullname;
        private string _distinguishedname;
        private string _userprincipalname;
        private string _department;
        private string _email;
        private string _title;
        private string _telephonenumber;
        private string _adspath;
        private string _physicaldeliveryofficename;
        private bool _isdirector;
        private bool _ismanager;
        private bool _isadmin;

        public string UserName
        {
            get { return _username; }
        }
        public string FullName
        {
            get { return _fullname; }
        }
        public string DistinguishedName
        {
            get { return _distinguishedname; }
        }
        public string UserPrincipalName
        {
            get { return _userprincipalname; }
        }
        public string Department
        {
            get { return _department; }
        }
        public string Email
        {
            get { return _email; }
        }
        public string Title
        {
            get { return _title; }
        }
        public string TelephoneNumber
        {
            get { return _telephonenumber; }
        }
        public string ADPath
        {
            get { return _adspath; }
        }
        public string PhisicalLocation
        {
            get { return _physicaldeliveryofficename; }
        }
        public bool IsDirector
        {
            get { return _isdirector; }
        }
        public bool IsManager
        {
            get { return _ismanager; }
        }
        public bool IsAdmin
        {
            get { return _isadmin; }
        }

        public ADUser()
            : this(Environment.UserName.ToString())
        {
        }
        public ADUser(string userName)
        {
            ////Dictionary<string, string> useratt = new Dictionary<string, string>();
            ////useratt = GetUsedAttributes(userName);
            _username = userName;
            GetUsedAttributes(userName);
            
            //try
            //{
            //    this._username = userName.Substring(0, 10);
            //}
            //catch
            //{

            //}

            this._isdirector = IsMemberOfADGroup(userName, "NSC_DIRECTORS");
            this._ismanager = IsMemberOfADGroup(userName, "NSC_MANAGERS");
            this._isadmin = IsMemberOfADGroup(userName, "Admins");

        }

        private void GetUsedAttributes(string objectDn)
        {
            // Get the currently connected LDAP context 
            System.DirectoryServices.DirectoryEntry entry1 = new System.DirectoryServices.DirectoryEntry("LDAP://RootDSE");
            string domainContext = entry1.Properties["defaultNamingContext"].Value as string;

            // Use the default naming context as the connected context may not work for searches
            System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + domainContext);
            System.DirectoryServices.DirectorySearcher adSearch = new System.DirectoryServices.DirectorySearcher(entry);

            adSearch.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + objectDn + "))";
            adSearch.SearchScope = SearchScope.Subtree;

            //adSearch.Filter = "(&(objectClass=user)(anr=" + objectDn + "))";
            string[] requiredProperties = new string[] { "cn", "userprincipalname", "physicaldeliveryofficename", "distinguishedname", "telephonenumber", "mail", "title", "department", "adspath" };

            foreach (String property in requiredProperties)
                adSearch.PropertiesToLoad.Add(property);

            SearchResult result = adSearch.FindOne();

            if (result != null)
            {
                foreach (String property in requiredProperties)
                {
                    if (result.GetDirectoryEntry().Properties[property].Value != null)
                    {
                        switch (property)
                        {
                            case "cn":
                                this._fullname = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "userprincipalname":
                                this._userprincipalname = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "physicaldeliveryofficename":
                                this._physicaldeliveryofficename = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "distinguishedname":
                                this._distinguishedname = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "telephonenumber":
                                this._telephonenumber = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "department":
                                this._department = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "mail":
                                this._email = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "title":
                                this._title = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            case "adspath":
                                this._adspath = result.GetDirectoryEntry().Properties[property].Value.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> GetUsedAttributesWorking(string objectDn)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // Get the currently connected LDAP context 
            System.DirectoryServices.DirectoryEntry entry1 = new System.DirectoryServices.DirectoryEntry("LDAP://RootDSE");
            string domainContext = entry1.Properties["defaultNamingContext"].Value as string;

            // Use the default naming context as the connected context may not work for searches
            System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + domainContext);
            System.DirectoryServices.DirectorySearcher adSearch = new System.DirectoryServices.DirectorySearcher(entry);

            adSearch.Filter = "(&(objectClass=user)(anr=" + objectDn + "))";
            string[] requiredProperties = new string[] { "cn", "userprincipalname", "physicaldeliveryofficename", "distinguishedname", "telephonenumber", "mail", "title", "department", "adspath" };

            foreach (String property in requiredProperties)
                adSearch.PropertiesToLoad.Add(property);

            SearchResult result = adSearch.FindOne();

            if (result != null)
            {
                foreach (String property in requiredProperties)
                    foreach (Object myCollection in result.Properties[property])
                        props.Add(property, myCollection.ToString());
            }

            return props;
        }

        private bool IsMemberOfADGroup(string userName, string groupName)
        {
            bool isMember = false;
            //List<string> grpMembers = new List<string>();

            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                //UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, sUserName);
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, IdentityType.Name, groupName);

                if (group != null)
                {
                    foreach (Principal p in group.GetMembers(true))
                    {
                        if (p.SamAccountName.ToUpper() == userName.ToUpper()) { isMember = true; break; }
                    }
                }
                group.Dispose();
            }
            return isMember;
        }

        public static List<string> GetUserGroups(string sUserName)
        {
            List<string> userGroups = new List<string>();
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, sUserName);
                foreach (var group in user.GetGroups())
                {
                    userGroups.Add(group.Name);
                }
            }
            return userGroups;
        }
    }
}