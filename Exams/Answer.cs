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
    public partial class Answer : Form
    {

        int uid;
        int examId;
        string uname;
        string pass;
        int numR;
        char ans;
        public int NumR
        {
            set { numR = value; }
            get { return numR; }
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
            set { pass = value; }
            get { return pass; }
        }

        SqlConnection con = new SqlConnection(@"Data Source=HANNAN\SQLEXPRESS01; Initial catalog=exams; integrated Security=true");


        public Answer()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            lblUname.Text = uname;
            lblRes.Text = pass;
            lblMarks.Text = numR.ToString()+"/20";
        }
    }
}
