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
using System.Collections;


namespace Exams
{
    public partial class ExamPage : Form
    {
        int mins = 19;
        int sec = 60;
        static int currentQnt = 0;
        int[] finalPaper = new int[20];
        DataTable dtQuestionPaper = new DataTable();
        int quid;
        //int dcount = 1;
        int uid;
        int examId;
        string uname;
        string pass;
        char ans;
        int j = 0;
        HashSet<int> qp;

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
        
       

        public ExamPage()
        {
            InitializeComponent();
        }

        private void ExamPage_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            lblUname.Text = uname;
            panel1.Visible = false;
            SqlDataAdapter daSubjects = new SqlDataAdapter("Select * from subjects", con);
            DataTable dtSubjec = new DataTable();
            daSubjects.Fill(dtSubjec);
            DataRow r = dtSubjec.NewRow();
            r[0] = -1;
            r[1] = "Choose a subject";
            dtSubjec.Rows.InsertAt(r, 0);
            cmbSubjects.DataSource = dtSubjec;
            cmbSubjects.DisplayMember = "subjectname";
            cmbSubjects.ValueMember = "subid";


            examid();

            
        }
     

        public void Eid()
        {
            SqlCommand cons = new SqlCommand("insert into Exams(ExamID,StudentId,subjectId) values (" + examId + "," + uid + "," + Convert.ToInt32(cmbSubjects.SelectedValue) + ")", con);

            if (cons.ExecuteNonQuery() > 0)
            {
                lblExamId.Text = examId.ToString();
            }
        }
        public int examid()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand sb = new SqlCommand("select isnull(max(ExamId),1) from Exams", con);
          
            
                int num = (int)sb.ExecuteScalar();


            if (num != null)
            {
               

                SqlCommand cmd = new SqlCommand("select max(ExamId)+1 from Exams", con);
                examId = (int)cmd.ExecuteScalar();
            }
            return examId;
            
        }
      

      
        
       
        void DisplayNextQtn(int qnid)
        {
            try
            {
                DataRow cuQn = dtQuestionPaper.Rows[qnid];
                lblQuestion.Text = cuQn[3].ToString();
                lblOptionA.Text = cuQn[4].ToString();
                lblOptionB.Text = cuQn[5].ToString();
                lblOptionC.Text = cuQn[6].ToString();
                lblOptionD.Text = cuQn[7].ToString();
                quid = Convert.ToInt32( cuQn[0].ToString());
            }
            catch (Exception e)
            {
                //if (q1.BackColor == Color.Blue | q2.BackColor == Color.Blue | q3.BackColor == Color.Blue | q4.BackColor == Color.Blue
                //    | q5.BackColor == Color.Blue)
                //{
                //    lblMsg.Text = "Review the marked questions";
                //}
                //else
                //{
                    
                //}
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

            if (cmbSubjects.SelectedIndex == 0)
            {
                MessageBox.Show("Choose a Subject");
                return;
            }
            else
            {
                Eid();
                panel1.Visible = true;
                timer1.Enabled = true;
                btnStart.Enabled = false;
                lblSubName.Text = cmbSubjects.Text;
                

                // select 20 random questions
                SqlDataAdapter daQuestions = new SqlDataAdapter("Select QuestionId from QuestionBank where subjectId =" + Convert.ToInt32(cmbSubjects.SelectedValue), con);
                DataTable dtQns = new DataTable();
                daQuestions.Fill(dtQns);
                List<int> lstQids = new List<int>();
                foreach (DataRow row in dtQns.Rows)
                {
                    lstQids.Add(Convert.ToInt32(row[0]));
                }
                qp = new HashSet<int>();
                //Random r = new Random();
                while (qp.Count <20)
                {
                    Random r = new Random();
                    int qno = lstQids[r.Next(lstQids.Count - 1)];
                    qp.Add(qno);
                }
                string id = "";
                foreach (int q in qp)
                {
                    id = id + q + ",";
                }
                qp.CopyTo(finalPaper);
                SqlDataAdapter daQp = new SqlDataAdapter("Select * from QuestionBank where QuestionId in (" + id.Remove(id.LastIndexOf(",")) + ")", con);
                daQp.Fill(dtQuestionPaper);
                
                    DisplayNextQtn(currentQnt);
               
                if (lblQno.Text == "") ;
                {
                    lblQno.Text = "1";


                }

                

            }

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            sec--;
            lblSec.Text = sec.ToString();
            lblMin.Text = mins.ToString();
            if (sec == 0)
            {
                sec = 60;
                mins--;
                lblSec.Text = sec.ToString();
                lblMin.Text = mins.ToString();
            }
            if (mins == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Its Time\nTest has ended!!!");
                SubmitExam();
            }
        }
        void SubmitExam()
        {
            MessageBox.Show("Thank You. Your Answers has been recorded.");

            Answer lp = new Answer();
            lp.Uname = uname;
            lp.Uid = uid;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select e.ExamID as [Examid],e.QuestionID,e.Answer from Answers e inner join QuestionBank q on q.Answer=e.Answer where e.ExamId=" + examId + " and q.QuestionId=e.QuestionID";
            cmd.Connection = con;
          
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            int numRows = dt.Rows.Count;
            if (numRows > 20)
            {
                numRows = 20;
            }
           
          
                if (numRows <= 10)
                {
                    lp.NumR = numRows;
                    lp.Pass = "Fail";

                }
                else if (numRows > 10)
                {
                    lp.NumR = numRows;
                    lp.Pass = "Pass";
                }


            //    int rowsAffected = reader.RecordsAffected;

            //if(rowsAffected<=3)
            //{
            //    lp.Pass = "Fail";

            //}
            //else if(rowsAffected>3)
            //{
            //    lp.Pass = "Pass";
            //}
           
                lp.Show();
            this.Close();
        }


        public void clearBtn()
        {
            if (rdOptionA.Checked == true || rdOptionB.Checked == true || rdOptionC.Checked == true || rdOptionD.Checked == true)
            {
                rdOptionA.Checked = false; rdOptionB.Checked = false; rdOptionC.Checked = false; rdOptionD.Checked = false;
                
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            //lblQno.Text = Convert.ToString(dcount);


            currentQnt++;
            int i = currentQnt;
            int j = 0;
            if (i <= 20)
            {
                j = i;
                //if (i < 20)
                //{
                //    lblQno.Text = Convert.ToString(++i);
                //}
                if (con.State == ConnectionState.Closed)
                    con.Open();
                //if (rdOptionA.Checked == false | rdOptionB.Checked == false | rdOptionC.Checked == false | rdOptionD.Checked == false)
                //{
                //    MessageBox.Show("Select the options for an answer");

                //}
                //else 
                //{
                    chkAns();
                if (rdOptionA.Checked == true || rdOptionB.Checked == true || rdOptionC.Checked == true || rdOptionD.Checked == true)
                // if(ans=='A'|| ans == 'B'|| ans == 'C'|| ans == 'D')
                {
                    if (i < 20)
                    {
                        lblQno.Text = Convert.ToString(++i);
                    }
                    clearBtn();
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        cmd.CommandText = "insert into Answers values (" + Convert.ToInt32(examId) + "," + quid + ",'" + Convert.ToChar(ans) + "')";
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { }
                    //catch
                    //{
                    //    MessageBox.Show("Select the Option");
                    //}
                    DisplayNextQtn(currentQnt);

                    //   }

                    if (j <= 20)
                    {
                        // ++j;

                        if (j == 1)
                        {
                            // q1.Enabled = false;
                            if (q1.Enabled == true)
                            {
                                q1.BackColor = Color.Green;
                                q1.Enabled = false;
                            }

                        }
                        else if (j == 2)
                        {
                            q2.BackColor = Color.Green;
                            q2.Enabled = false;
                        }
                        else if (j == 3)
                        {
                            q3.BackColor = Color.Green;
                            q3.Enabled = false;
                        }
                        else if (j == 4)
                        {
                            q4.BackColor = Color.Green;
                            q4.Enabled = false;
                        }

                        else if (j == 5)
                        {
                            q5.BackColor = Color.Green;
                            q5.Enabled = false;
                        }
                        else if (j == 6)
                        {
                            q6.BackColor = Color.Green;
                            q6.Enabled = false;
                        }
                        else if (j == 7)
                        {
                            q7.BackColor = Color.Green;
                            q7.Enabled = false;
                        }
                        else if (j == 8)
                        {
                            q8.BackColor = Color.Green;
                            q8.Enabled = false;
                        }
                        else if (j == 9)
                        {
                            q9.BackColor = Color.Green;
                            q9.Enabled = false;
                        }
                        else if (j == 10)
                        {
                            q10.BackColor = Color.Green;
                            q10.Enabled = false;
                        }
                        else if (j == 11)
                        {
                            q11.BackColor = Color.Green;
                            q11.Enabled = false;
                        }
                        else if (j == 12)
                        {
                            q12.BackColor = Color.Green;
                            q12.Enabled = false;
                        }
                        else if (j == 13)
                        {
                            q13.BackColor = Color.Green;
                            q13.Enabled = false;
                        }
                        else if (j == 14)
                        {
                            q14.BackColor = Color.Green;
                            q14.Enabled = false;

                        }
                        else if (j == 15)
                        {
                            q15.BackColor = Color.Green;
                            q15.Enabled = false;
                        }
                        else if (j == 16)
                        {
                            q16.BackColor = Color.Green;
                            q16.Enabled = false;
                        }
                        else if (j == 17)
                        {
                            q17.BackColor = Color.Green;
                            q17.Enabled = false;
                        }
                        else if (j == 18)
                        {
                            q18.BackColor = Color.Green;
                            q18.Enabled = false;
                        }
                        else if (j == 19)
                        {
                            q19.BackColor = Color.Green;
                            q19.Enabled = false;
                        }
                        else
                        if (j == 20)
                        {
                            q20.BackColor = Color.Green;
                            q20.Enabled = false;
                            if (q1.BackColor == Color.Blue | q2.BackColor == Color.Blue | q3.BackColor == Color.Blue | q4.BackColor == Color.Blue |
                                q5.BackColor == Color.Blue | q6.BackColor == Color.Blue | q7.BackColor == Color.Blue | q8.BackColor == Color.Blue | q9.BackColor == Color.Blue | q10.BackColor == Color.Blue |
                                q11.BackColor == Color.Blue | q12.BackColor == Color.Blue | q13.BackColor == Color.Blue | q14.BackColor == Color.Blue | q15.BackColor == Color.Blue | q16.BackColor == Color.Blue | q17.BackColor == Color.Blue | q18.BackColor == Color.Blue | q19.BackColor == Color.Blue | q20.BackColor == Color.Blue
                               )
                            {
                                MessageBox.Show("Check the question marked for review");
                            }
                            else
                            {
                                MessageBox.Show("Click On End Test to End the exam.");
                            }

                        }
                    }

                    }
                   else
               {
                    DisplayNextQtn(--currentQnt);
                    MessageBox.Show("Choose the opt");
                }
                }
                // con.Close();
            }   //dcount++;

        

        
        

        public void chkAns()
        {
            if (rdOptionA.Checked == true || rdOptionB.Checked == true || rdOptionC.Checked == true || rdOptionD.Checked == true)
            {
                if (rdOptionA.Checked == true)
                {
                    ans = 'A';
                    // rdOptionA.Checked = false;

                }
                else if (rdOptionB.Checked == true)
                {
                    ans = 'B';
                    // rdOptionB.Checked = false;

                }
                else if (rdOptionC.Checked == true)
                {
                    ans = 'C';
                    // rdOptionC.Checked = false;

                }
                else if (rdOptionD.Checked == true)
                {
                    ans = 'D';
                    // rdOptionD.Checked = false;

                }
               // return ans;
            }
            else
            {
               // return -1;
                MessageBox.Show("Select optionss");
              //  currentQnt--;
            }

        }

        private void q1_Click(object sender, EventArgs e)
        {
            
            
                lblQno.Text = Convert.ToString(1);
            
            DisplayNextQtn(Convert.ToInt32(q1.Text) - 1);
            currentQnt = Convert.ToInt32(q1.Text) - 1;
            q1.BackColor = Color.Green;
        }

        private void q2_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(2);
            DisplayNextQtn(Convert.ToInt32(q2.Text) - 1);
            currentQnt = Convert.ToInt32(q2.Text) - 1;
            q2.BackColor = Color.Green;
        }

        private void q3_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(3);
            DisplayNextQtn(Convert.ToInt32(q3.Text) - 1);
            currentQnt = Convert.ToInt32(q3.Text) - 1;
            q3.BackColor = Color.Green;
        }

        private void q4_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(4);
            DisplayNextQtn(Convert.ToInt32(q4.Text) - 1);
            currentQnt = Convert.ToInt32(q4.Text) - 1;
            q4.BackColor = Color.Green;
        }

        private void q5_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(5);
            DisplayNextQtn(Convert.ToInt32(q5.Text) - 1);
            currentQnt = Convert.ToInt32(q5.Text) - 1;
            q5.BackColor = Color.Green;
        }

        private void q6_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(6);
            DisplayNextQtn(Convert.ToInt32(q6.Text) - 1);
            currentQnt = Convert.ToInt32(q6.Text) - 1;
            q6.BackColor = Color.Green;
        }

        private void q7_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(7);
            DisplayNextQtn(Convert.ToInt32(q7.Text) - 1);
            currentQnt = Convert.ToInt32(q7.Text) - 1;
            q7.BackColor = Color.Green;
        }

        private void q8_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(8);
            DisplayNextQtn(Convert.ToInt32(q8.Text) - 1);
            currentQnt = Convert.ToInt32(q8.Text) - 1;
            q8.BackColor = Color.Green;
        }

        private void q9_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(9);
            DisplayNextQtn(Convert.ToInt32(q9.Text) - 1);
            currentQnt = Convert.ToInt32(q9.Text) - 1;
            q9.BackColor = Color.Green;
        }

        private void q10_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(10);
            DisplayNextQtn(Convert.ToInt32(q10.Text) - 1);
            currentQnt = Convert.ToInt32(q10.Text) - 1;
            q10.BackColor = Color.Green;
        }

        private void q11_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(11);
            DisplayNextQtn(Convert.ToInt32(q11.Text) - 1);
            currentQnt = Convert.ToInt32(q11.Text) - 1;
            q11.BackColor = Color.Green;
        }

        private void q12_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(12);
            DisplayNextQtn(Convert.ToInt32(q12.Text) - 1);
            currentQnt = Convert.ToInt32(q12.Text) - 1;
            q12.BackColor = Color.Green;
        }

        private void q13_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(13);
            DisplayNextQtn(Convert.ToInt32(q13.Text) - 1);
            currentQnt = Convert.ToInt32(q13.Text) - 1;
            q13.BackColor = Color.Green;
        }

        private void q14_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(14);
            DisplayNextQtn(Convert.ToInt32(q14.Text) - 1);
            currentQnt = Convert.ToInt32(q14.Text) - 1;
            q14.BackColor = Color.Green;
        }

        private void q15_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(15);
            DisplayNextQtn(Convert.ToInt32(q15.Text) - 1);
            currentQnt = Convert.ToInt32(q15.Text) - 1;
            q15.BackColor = Color.Green;
        }

        private void q16_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(16);
            DisplayNextQtn(Convert.ToInt32(q16.Text) - 1);
            currentQnt = Convert.ToInt32(q16.Text) - 1;
            q16.BackColor = Color.Green;
        }

        private void q17_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(17);
            DisplayNextQtn(Convert.ToInt32(q17.Text) - 1);
            currentQnt = Convert.ToInt32(q17.Text) - 1;
            q17.BackColor = Color.Green;
        }

        private void q18_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(18);
            DisplayNextQtn(Convert.ToInt32(q18.Text) - 1);
            currentQnt = Convert.ToInt32(q18.Text) - 1;
            q18.BackColor = Color.Green;
        }

        private void q19_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(19);
            DisplayNextQtn(Convert.ToInt32(q19.Text) - 1);
            currentQnt = Convert.ToInt32(q19.Text) - 1;
            q19.BackColor = Color.Green;
        }

        private void q20_Click(object sender, EventArgs e)
        {
            lblQno.Text = Convert.ToString(20);
            DisplayNextQtn(Convert.ToInt32(q20.Text) - 1);
            currentQnt = Convert.ToInt32(q20.Text) - 1;
            q20.BackColor = Color.Green;
        }

        private void panel1_Enter(object sender, EventArgs e)
        {

        }

        private void btnReview_Click(object sender, EventArgs e)
        {

           // currentQnt++;

            //for(int i=currentQnt;i<20;i++)
            //{
            //    q1.c
            //}
            int i = currentQnt;
            if (i <= 20)
            {
                if (i < 20)
                {
                    lblQno.Text = Convert.ToString(++i);
                }

                if (i == 1)
                {
                    q1.BackColor = Color.Blue;
                   // currentQnt++;
                }
                else if (i == 2)
                {
                    q2.BackColor = Color.Blue;
                }
                else if (i == 3)
                {
                    q3.BackColor = Color.Blue;

                }
                else if (i == 4)
                {
                    q4.BackColor = Color.Blue;
                }

                else if (i == 5)
                {
                    q5.BackColor = Color.Blue;
                }
                else if (i == 6)
                {
                    q6.BackColor = Color.Blue;
                }
                else if (i == 7)
                {
                    q7.BackColor = Color.Blue;
                }
                else if (i == 8)
                {
                    q8.BackColor = Color.Blue;
                }
                else if (i == 9)
                {
                    q9.BackColor = Color.Blue;
                }
                else if (i == 10)
                {
                    q10.BackColor = Color.Blue;
                }
                else if (i == 11)
                {
                    q11.BackColor = Color.Blue;
                }
                else if (i == 12)
                {
                    q12.BackColor = Color.Blue;
                }
                else if (i == 13)
                {
                    q13.BackColor = Color.Blue;
                }
                else if (i == 14)
                {
                    q14.BackColor = Color.Blue;

                }
                else if (i == 15)
                {
                    q15.BackColor = Color.Blue;
                }
                else if (i == 16)
                {
                    q16.BackColor = Color.Blue;
                }
                else if (i == 17)
                {
                    q17.BackColor = Color.Blue;
                }
                else if (i == 18)
                {
                    q18.BackColor = Color.Blue;
                }
                else if (i == 19)
                {
                    q19.BackColor = Color.Blue;
                }
                else if (i == 20)
                {
                    q20.BackColor = Color.Blue;
                }
                currentQnt++;
                DisplayNextQtn(currentQnt);
            }

            }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitExam();
            
        }

        private void lblQuestion_Click(object sender, EventArgs e)
        {

        }

        //private void rdOptionA_CheckedChanged(object sender, EventArgs e)
        //{
        //    rdOptionA.Checked = true;
        //}

        //private void rdOptionB_CheckedChanged(object sender, EventArgs e)
        //{
        //    rdOptionB.Checked = true;
        //}

        //private void rdOptionC_CheckedChanged(object sender, EventArgs e)
        //{
        //    rdOptionC.Checked = true;
        //}

        //private void rdOptionD_CheckedChanged(object sender, EventArgs e)
        //{
        //    rdOptionD.Checked = true;
        //}
    }
}
