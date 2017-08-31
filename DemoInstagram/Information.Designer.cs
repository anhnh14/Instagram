namespace DemoInstagram
{
    partial class Information
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.btn_Download = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbSearchUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lbSearchUser = new System.Windows.Forms.ListBox();
            this.btDownloadReccentImageofUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(148, 24);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(73, 17);
            this.lbUsername.TabIndex = 1;
            this.lbUsername.Text = "Username";
            // 
            // btn_Download
            // 
            this.btn_Download.Location = new System.Drawing.Point(41, 91);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(175, 45);
            this.btn_Download.TabIndex = 2;
            this.btn_Download.Text = "Download Profile Image";
            this.btn_Download.UseVisualStyleBackColor = true;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 45);
            this.button1.TabIndex = 3;
            this.button1.Text = "Download Recent Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbSearchUser
            // 
            this.tbSearchUser.Location = new System.Drawing.Point(529, 25);
            this.tbSearchUser.Name = "tbSearchUser";
            this.tbSearchUser.Size = new System.Drawing.Size(100, 22);
            this.tbSearchUser.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(436, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search User";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(648, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lbSearchUser
            // 
            this.lbSearchUser.FormattingEnabled = true;
            this.lbSearchUser.ItemHeight = 16;
            this.lbSearchUser.Location = new System.Drawing.Point(439, 91);
            this.lbSearchUser.Name = "lbSearchUser";
            this.lbSearchUser.Size = new System.Drawing.Size(270, 116);
            this.lbSearchUser.TabIndex = 7;
            // 
            // btDownloadReccentImageofUser
            // 
            this.btDownloadReccentImageofUser.Location = new System.Drawing.Point(439, 239);
            this.btDownloadReccentImageofUser.Name = "btDownloadReccentImageofUser";
            this.btDownloadReccentImageofUser.Size = new System.Drawing.Size(270, 45);
            this.btDownloadReccentImageofUser.TabIndex = 8;
            this.btDownloadReccentImageofUser.Text = "Download Recent Image of User";
            this.btDownloadReccentImageofUser.UseVisualStyleBackColor = true;
            this.btDownloadReccentImageofUser.Click += new System.EventHandler(this.btDownloadReccentImageofUser_Click);
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 405);
            this.Controls.Add(this.btDownloadReccentImageofUser);
            this.Controls.Add(this.lbSearchUser);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSearchUser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Download);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.label1);
            this.Name = "Information";
            this.Text = "Information";
            this.Load += new System.EventHandler(this.Information_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbSearchUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lbSearchUser;
        private System.Windows.Forms.Button btDownloadReccentImageofUser;
    }
}