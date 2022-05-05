
namespace Exams
{
    partial class AdminHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBox = new System.Windows.Forms.GroupBox();
            this.lblUname = new System.Windows.Forms.Label();
            this.linkLblLogout = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChgPwd = new System.Windows.Forms.Button();
            this.btnStaffMgmt = new System.Windows.Forms.Button();
            this.btnSubMgmt = new System.Windows.Forms.Button();
            this.btnAllocateSub = new System.Windows.Forms.Button();
            this.grpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBox
            // 
            this.grpBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.grpBox.Controls.Add(this.lblUname);
            this.grpBox.Controls.Add(this.linkLblLogout);
            this.grpBox.Controls.Add(this.label1);
            this.grpBox.Location = new System.Drawing.Point(45, 21);
            this.grpBox.Name = "grpBox";
            this.grpBox.Size = new System.Drawing.Size(406, 74);
            this.grpBox.TabIndex = 0;
            this.grpBox.TabStop = false;
            this.grpBox.Enter += new System.EventHandler(this.grpBox_Enter);
            // 
            // lblUname
            // 
            this.lblUname.AutoSize = true;
            this.lblUname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUname.Location = new System.Drawing.Point(84, 30);
            this.lblUname.Name = "lblUname";
            this.lblUname.Size = new System.Drawing.Size(0, 15);
            this.lblUname.TabIndex = 5;
            this.lblUname.Click += new System.EventHandler(this.label2_Click);
            // 
            // linkLblLogout
            // 
            this.linkLblLogout.AutoSize = true;
            this.linkLblLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblLogout.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLblLogout.Location = new System.Drawing.Point(306, 30);
            this.linkLblLogout.Name = "linkLblLogout";
            this.linkLblLogout.Size = new System.Drawing.Size(59, 20);
            this.linkLblLogout.TabIndex = 2;
            this.linkLblLogout.TabStop = true;
            this.linkLblLogout.Text = "Logout";
            this.linkLblLogout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblLogout_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome:";
            // 
            // btnChgPwd
            // 
            this.btnChgPwd.Location = new System.Drawing.Point(87, 141);
            this.btnChgPwd.Name = "btnChgPwd";
            this.btnChgPwd.Size = new System.Drawing.Size(196, 23);
            this.btnChgPwd.TabIndex = 1;
            this.btnChgPwd.Text = "Change Password";
            this.btnChgPwd.UseVisualStyleBackColor = true;
            this.btnChgPwd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStaffMgmt
            // 
            this.btnStaffMgmt.Location = new System.Drawing.Point(87, 188);
            this.btnStaffMgmt.Name = "btnStaffMgmt";
            this.btnStaffMgmt.Size = new System.Drawing.Size(196, 23);
            this.btnStaffMgmt.TabIndex = 2;
            this.btnStaffMgmt.Text = "Staff Management";
            this.btnStaffMgmt.UseVisualStyleBackColor = true;
            this.btnStaffMgmt.Click += new System.EventHandler(this.btnStaffMgmt_Click);
            // 
            // btnSubMgmt
            // 
            this.btnSubMgmt.Location = new System.Drawing.Point(87, 236);
            this.btnSubMgmt.Name = "btnSubMgmt";
            this.btnSubMgmt.Size = new System.Drawing.Size(196, 23);
            this.btnSubMgmt.TabIndex = 3;
            this.btnSubMgmt.Text = "Subject Management";
            this.btnSubMgmt.UseVisualStyleBackColor = true;
            this.btnSubMgmt.Click += new System.EventHandler(this.btnSubMgmt_Click);
            // 
            // btnAllocateSub
            // 
            this.btnAllocateSub.Location = new System.Drawing.Point(87, 281);
            this.btnAllocateSub.Name = "btnAllocateSub";
            this.btnAllocateSub.Size = new System.Drawing.Size(196, 23);
            this.btnAllocateSub.TabIndex = 4;
            this.btnAllocateSub.Text = "Allocate Subject";
            this.btnAllocateSub.UseVisualStyleBackColor = true;
            this.btnAllocateSub.Click += new System.EventHandler(this.btnAllocateSub_Click);
            // 
            // AdminHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(518, 450);
            this.Controls.Add(this.btnAllocateSub);
            this.Controls.Add(this.btnSubMgmt);
            this.Controls.Add(this.btnStaffMgmt);
            this.Controls.Add(this.btnChgPwd);
            this.Controls.Add(this.grpBox);
            this.Name = "AdminHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Homepage";
            this.Load += new System.EventHandler(this.AdminHome_Load);
            this.grpBox.ResumeLayout(false);
            this.grpBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBox;
        private System.Windows.Forms.Button btnChgPwd;
        private System.Windows.Forms.Button btnStaffMgmt;
        private System.Windows.Forms.Button btnSubMgmt;
        private System.Windows.Forms.Button btnAllocateSub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLblLogout;
        private System.Windows.Forms.Label lblUname;
    }
}