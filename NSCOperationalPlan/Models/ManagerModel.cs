namespace NSCOperationalPlan
{
    public class ManagerModel : BaseViewModel
    {
        private string mID;
        private string mADUserName;
        private string mDescription;
        private string mName;
        private string mDirectorID;
        private string mDepartment;
        private string mSubDepartment;

        public string ID
        {
            get { return mID; }
            set { mID = value; }
        }
        public string ActiveDirectoryUserName
        {
            get { return mADUserName; }
            set { mADUserName = value; }
        }
        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public string DirectorID
        {
            get { return mDirectorID; }
            set { mDirectorID = value; }
        }
        public string Department
        {
            get { return mDepartment; }
            set { mDepartment = value; }
        }
        public string SubDepartment
        {
            get { return mSubDepartment; }
            set { mSubDepartment = value; }
        }
        
        public ManagerModel() { }

    }
}
