using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exams
{
    public partial class AdminHome : Form
    {
        int uid;
        string uname;
        string pass;
        
        public int Uid
        {
            set { uid = value; }
            get { return uid; }
        }

        public string Uname
        {
            set { uname = value; }
            get { return uname; }
        }

        public string Pass
        {
            set { pass = value; }
            get { return pass; }
        }

        SqlConnection con = new SqlConnection(@"Data Source=ES-SYS-568\SQL2017; Initial catalog=exams; integrated Security=true");

        

        public AdminHome()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ChangePwd chPwd = new ChangePwd();
            chPwd.Uname = uname;
            chPwd.Uid = uid;
            chPwd.Pass = pass;
            chPwd.PAction = 1;
            chPwd.Show();
            this.Close();
            
        }

      
        

        private void linkLblLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginPage frm1 = new LoginPage();
            frm1.Show();
            this.Close();
        }

        private void btnStaffMgmt_Click(object sender, EventArgs e)
        {
            StaffMgmt stfMgt = new StaffMgmt();
            stfMgt.Uname = uname;
            stfMgt.Uid = uid;
            stfMgt.Pass = pass;
            stfMgt.Show();
            this.Close();
        }

        private void btnAllocateSub_Click(object sender, EventArgs e)
        {
            SubjectMgmt sub = new SubjectMgmt();
            sub.Uname = uname;
            sub.Uid = uid;
            sub.Show();
            this.Close();
        }

        private void btnSubMgmt_Click(object sender, EventArgs e)
        {
            SubjectMgmt sub = new SubjectMgmt();
            sub.Uname = uname;
            sub.Uid = uid;
            sub.Show();
            this.Close();
        }

        private void grpBox_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AdminHome_Load(object sender, EventArgs e)
        {
            lblUname.Text = uname;
        }
    }
}
