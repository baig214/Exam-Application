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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ES-SYS-568\SQL2017; Initial catalog=exams; integrated Security=true");

        public Form1()
        {
            InitializeComponent();
        }



        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Select name,password,role from users where email=@email", con);
            SqlParameter paramEmail = new SqlParameter("@email", SqlDbType.VarChar, 100);
            cmd.Parameters.Add(paramEmail);
            paramEmail.Value = txtEmail.Text.Trim();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[1].ToString().Equals(txtPwd.Text.Trim()))
                {
                    //lblMsg.Text = "Login is Successful";
                    //lblMsg.ForeColor = System.Drawing.Color.DarkOliveGreen;
                    //   txtPassword.Focus();
                    if (dr[2].ToString() == "1")
                    {
                        Form2 fm = new Form2();
                       // fm.User = dr[0].ToString();
                        fm.Show();
                        this.Hide();

                    }
                    else
                    {
                        lblMsg.Text = "You do not have previledges to open the Admin page";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        txtPwd.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = "Login is Unsuccessful,Try Again";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    txtPwd.Focus();
                }
            }
            else
            {
                lblMsg.Text = "Login is Unsuccessful,Try Again";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                txtEmail.Focus();
            }
            con.Close();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
