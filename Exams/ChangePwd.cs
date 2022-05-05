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
    public partial class ChangePwd : Form
    {
        int paction;
        string uname;
        int uid;
        string pass;

        public int PAction
        {
            set { paction = value; }
            get { return paction; }
        }

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
            set
            {
                pass = value;
            }
            get { return pass; }
        }

        SqlConnection con = new SqlConnection(@"Data Source=ES-SYS-568\SQL2017; Initial catalog=exams; integrated Security=true");


        public ChangePwd()
        {
            InitializeComponent();
        }


       

        private void linkLblLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginPage frm1 = new LoginPage();
            frm1.Show();
            this.Close();
                
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            if (paction == 1)
            {
                AdminHome frm2 = new AdminHome();
                frm2.Uid = uid;
                frm2.Uname = uname;
                frm2.Show();
                this.Close();
            }
            else if(paction==2)
            {
                staffHome stfHm = new staffHome();
                stfHm.Uid = uid;
                stfHm.Uname = uname;
                stfHm.Show();
                this.Close();
            }
            else if (paction == 3)
            {
                studentHome stuHm = new studentHome();
                stuHm.Uid = uid;
                stuHm.Uname = uname;
                stuHm.Show();
                this.Close();
            }
        }

        private void btnSavePwd_Click_1(object sender, EventArgs e)
        {
            //int res = 0;
            SqlConnection con = new SqlConnection(@"Data Source=ES-SYS-568\SQL2017; Initial catalog=exams; integrated Security=true");
            if (con.State == ConnectionState.Closed)
                con.Open();
            ////string oldPwd = txtOldPwd.Text.Trim() ;
            ////string newPwd = txtNewPwd.Text.Trim();
            ////string cnfPwd = txtCnfPwd.Text.Trim();
            if (txtOldPwd.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtOldPwd, "Enter The current Password");
            }

            else
            {
                if (txtOldPwd.Text.Trim().Equals(pass))
                {
                    if (txtNewPwd.Text.Trim().Length == 0 | txtCnfPwd.Text.Trim().Length == 0)
                    {
                        errorProvider2.SetError(txtNewPwd, "Enter the new Password");
                        errorProvider3.SetError(txtCnfPwd, "Enter the Confirm password");
                    }
                    else
                    {
                        if (txtNewPwd.Text.Trim().Equals(txtCnfPwd.Text.Trim()))
                        {
                            SqlCommand cmds = new SqlCommand("update users set password='" + txtCnfPwd.Text.Trim() + "' where userid=" + uid, con);
                            if (cmds.ExecuteNonQuery() > 0)
                            {
                                MessageBox.Show("Password updated");
                                SqlCommand passCmd = new SqlCommand("select password from users where userid=" + uid, con);
                                pass = (string)passCmd.ExecuteScalar();
                                txtOldPwd.Clear();
                                txtNewPwd.Clear();
                                txtCnfPwd.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Unable to update password");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password Doesn't match");
                            txtCnfPwd.Focus();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Old password entered is wrong");
                }
            }
        }

        private void ChangePwd_Load(object sender, EventArgs e)
        {
            lblUname.Text = uname;
        }

      
    }
}
