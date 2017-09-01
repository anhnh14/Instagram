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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbNotification = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.btnPost = new System.Windows.Forms.Button();
            this.pbRecentAvatar = new System.Windows.Forms.PictureBox();
            this.pbRecentImage = new System.Windows.Forms.PictureBox();
            this.btnShowRecentImageofUser = new System.Windows.Forms.Button();
            this.lbComment = new System.Windows.Forms.ListBox();
            this.lbPictureId = new System.Windows.Forms.Label();
            this.btnLike = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbRecentAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRecentImage)).BeginInit();
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
            this.button1.Click += new System.EventHandler(this.btnDownloaRecentPicture_Click);
            // 
            // tbSearchUser
            // 
            this.tbSearchUser.Location = new System.Drawing.Point(491, 25);
            this.tbSearchUser.Name = "tbSearchUser";
            this.tbSearchUser.Size = new System.Drawing.Size(100, 22);
            this.tbSearchUser.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(398, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search User";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(615, 25);
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
            this.lbSearchUser.Location = new System.Drawing.Point(401, 70);
            this.lbSearchUser.Name = "lbSearchUser";
            this.lbSearchUser.Size = new System.Drawing.Size(289, 116);
            this.lbSearchUser.TabIndex = 7;
            // 
            // btDownloadReccentImageofUser
            // 
            this.btDownloadReccentImageofUser.Location = new System.Drawing.Point(491, 267);
            this.btDownloadReccentImageofUser.Name = "btDownloadReccentImageofUser";
            this.btDownloadReccentImageofUser.Size = new System.Drawing.Size(105, 39);
            this.btDownloadReccentImageofUser.TabIndex = 8;
            this.btDownloadReccentImageofUser.Text = "Download ";
            this.btDownloadReccentImageofUser.UseVisualStyleBackColor = true;
            this.btDownloadReccentImageofUser.Click += new System.EventHandler(this.btDownloadReccentImageofUser_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(41, 336);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(175, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // lbNotification
            // 
            this.lbNotification.AutoSize = true;
            this.lbNotification.Location = new System.Drawing.Point(38, 267);
            this.lbNotification.Name = "lbNotification";
            this.lbNotification.Size = new System.Drawing.Size(65, 17);
            this.lbNotification.TabIndex = 10;
            this.lbNotification.Text = "Progress";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(388, 472);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Comment";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(391, 505);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(289, 22);
            this.tbComment.TabIndex = 12;
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(491, 545);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(84, 36);
            this.btnPost.TabIndex = 13;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // pbRecentAvatar
            // 
            this.pbRecentAvatar.Location = new System.Drawing.Point(753, 25);
            this.pbRecentAvatar.Name = "pbRecentAvatar";
            this.pbRecentAvatar.Size = new System.Drawing.Size(209, 161);
            this.pbRecentAvatar.TabIndex = 14;
            this.pbRecentAvatar.TabStop = false;
            // 
            // pbRecentImage
            // 
            this.pbRecentImage.Location = new System.Drawing.Point(390, 323);
            this.pbRecentImage.Name = "pbRecentImage";
            this.pbRecentImage.Size = new System.Drawing.Size(289, 131);
            this.pbRecentImage.TabIndex = 15;
            this.pbRecentImage.TabStop = false;
            // 
            // btnShowRecentImageofUser
            // 
            this.btnShowRecentImageofUser.Location = new System.Drawing.Point(391, 267);
            this.btnShowRecentImageofUser.Name = "btnShowRecentImageofUser";
            this.btnShowRecentImageofUser.Size = new System.Drawing.Size(94, 39);
            this.btnShowRecentImageofUser.TabIndex = 17;
            this.btnShowRecentImageofUser.Text = "Show recent publish image";
            this.btnShowRecentImageofUser.UseVisualStyleBackColor = true;
            this.btnShowRecentImageofUser.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // lbComment
            // 
            this.lbComment.FormattingEnabled = true;
            this.lbComment.ItemHeight = 16;
            this.lbComment.Location = new System.Drawing.Point(753, 323);
            this.lbComment.Name = "lbComment";
            this.lbComment.Size = new System.Drawing.Size(228, 132);
            this.lbComment.TabIndex = 18;
            // 
            // lbPictureId
            // 
            this.lbPictureId.AutoSize = true;
            this.lbPictureId.Location = new System.Drawing.Point(482, 471);
            this.lbPictureId.Name = "lbPictureId";
            this.lbPictureId.Size = new System.Drawing.Size(46, 17);
            this.lbPictureId.TabIndex = 19;
            this.lbPictureId.Text = "label4";
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(605, 267);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(75, 39);
            this.btnLike.TabIndex = 20;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // Information
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 602);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.lbPictureId);
            this.Controls.Add(this.lbComment);
            this.Controls.Add(this.btnShowRecentImageofUser);
            this.Controls.Add(this.pbRecentImage);
            this.Controls.Add(this.pbRecentAvatar);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbNotification);
            this.Controls.Add(this.progressBar1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbRecentAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRecentImage)).EndInit();
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
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbNotification;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.PictureBox pbRecentAvatar;
        private System.Windows.Forms.PictureBox pbRecentImage;
        private System.Windows.Forms.Button btnShowRecentImageofUser;
        private System.Windows.Forms.ListBox lbComment;
        private System.Windows.Forms.Label lbPictureId;
        private System.Windows.Forms.Button btnLike;
    }
}