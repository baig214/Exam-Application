using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Exams
{
    public partial class LoginPage : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ES-SYS-568\SQL2017; Initial catalog=exams; integrated Security=true");

        public LoginPage()
        {
            InitializeComponent();
        }



        public void btnSubmit_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select userid,name,password,role from users where email=@email", con);
            SqlParameter paramEmail = new SqlParameter("@email", SqlDbType.VarChar, 100);
            cmd.Parameters.Add(paramEmail);

            if (txtEmail.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtEmail, "Enter your Email");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            if (txtPwd.Text.Trim().Length == 0)
            {
                errorProvider1.SetError(txtPwd, "Enter your Password");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }


            paramEmail.Value = txtEmail.Text.Trim();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[2].ToString().Equals(txtPwd.Text.Trim()))
                {
                    
                    if (dr[3].ToString() == "1")
                    {
                       AdminHome fm = new AdminHome();
                       fm.Uname = dr[1].ToString().Trim();
                        fm.Uid = Convert.ToInt32(dr[0].ToString().Trim());
                        fm.Pass = dr[2].ToString().Trim();
                       
                        fm.Show();
                        this.Hide();
                        

                    }
                    else if(dr[3].ToString()=="2")
                    {
                        staffHome stfHm = new staffHome();
                        stfHm.Uname = dr[1].ToString();
                        stfHm.Uid = Convert.ToInt32(dr[0].ToString());
                        stfHm.Pass = dr[2].ToString().Trim();
                        stfHm.Show();
                        this.Hide();
                        

                    }
                    else if (dr[3].ToString() == "3")
                    {
                        studentHome stfHm = new studentHome();
                        stfHm.Uname = dr[1].ToString();
                        stfHm.Uid = Convert.ToInt32(dr[0].ToString());
                        stfHm.Pass = dr[2].ToString().Trim();
                        stfHm.Show();
                        this.Hide();


                    }
                    else
                    {
                        lblMsg.Text = "You do not have privileges";
                        
                        txtPwd.Focus();
                    }
                    
                }
                else
                {
                    lblMsg.Text = "Login is Unsuccessful,Try Again";
                    //lblMsg.ForeColor = Color.Red;
                    txtPwd.Focus();
                }

                
            }
            else
            {
                lblMsg.Text = "Login is Unsuccessful,Try Again";
                
               txtEmail.Focus();
            }
            con.Close();


        }

       
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lnkStuReg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StudentReg strg = new StudentReg();
            strg.Show();
            this.Hide();
        }
        
    }
}
