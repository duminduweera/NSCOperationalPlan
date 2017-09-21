using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSCOperationalPlan
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            btnLogin.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnCancel.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

            if (Program.OPLogin(txtUserName.Text.ToString(), txtPassword.Text.ToString()) == true)
            {
                NSCUtils.ADUser aduser = new NSCUtils.ADUser(txtUserName.Text.ToString());
                Program.LoadUser(aduser, "P");

                Cursor.Current = Cursors.AppStarting;
                this.Dispose();
            }
            else
            {
                Cursor.Current = Cursors.AppStarting;
                MessageBox.Show("You are not Authorise to run this program", "Operational Plan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            btnLogin.Enabled = true;
            btnCancel.Enabled = true;
        }
    }
}
