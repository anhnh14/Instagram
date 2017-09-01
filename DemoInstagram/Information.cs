using DemoInstagram.APIsHelper;
using DemoInstagram.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoInstagram
{
    public partial class Information : Form
    {
        Profile profile = new Profile();
       // static HttpClient client = new HttpClient();

        private System.Windows.Forms.Timer timer1;

        #region Set auto run function loadComment
        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }
        public void StopTimer()
        {
            if (timer1 != null)
            {
                timer1.Stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            loadComments();
        }
        #endregion

        public Information(Profile profile)
        {
            InitializeComponent();
            this.profile = profile;
            lbUsername.Text = profile.full_name;
            lbPictureId.Hide();
        }

        private void Information_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Download image of owner token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Download_Click(object sender, EventArgs e)
        {
            Picture picture = new Picture();
            picture.url = profile.profile_picture;
            Download(picture);

        }

        /// <summary>
        /// Download recent image published by owner token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownloaRecentPicture_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            try
            {

                Picture recentPicture = new Picture();
                Task.Run(async () =>
                {
                    recentPicture = await endpoint.getRecentImage();
                }).Wait();
                Download(recentPicture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Search User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Endpoint endPoint = new Endpoint();
            pbRecentImage.Image = null;
            lbPictureId.Text = null;
            StopTimer();
            lbComment.DataSource = null;

            string search = tbSearchUser.Text;
            try
            {
                List<Profile> profile = new List<Profile>();
                Task.Run(async () =>
                {
                    profile = await endPoint.searchUser(search);
                }).Wait();
                lbSearchUser.DataSource = profile;
                lbSearchUser.DisplayMember = "full_name";
                lbSearchUser.ValueMember = "id";
                if(profile.Count > 0)
                {
                    pbRecentAvatar.Load(profile[0].profile_picture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Down oad Recent image published by user from search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDownloadReccentImageofUser_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            Picture picture = new Picture();
            picture.url = pbRecentImage.ImageLocation;
            try
            {
                Download(picture);

            }
            catch
            {
                MessageBox.Show(Configuaration.ERROR_MESSAGE);
            }

        }

        /// <summary>
        /// Post comment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();

            //Get User 
            Profile profile = (Profile)lbSearchUser.SelectedItem;
            if (profile != null)
            {
               
                string comment = tbComment.Text;
                try
                {
                    endpoint.postComment(comment, lbPictureId.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            tbComment.Text = "";
        }

        /// <summary>
        /// Show recent image published by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            try
            {
                Profile profile = (Profile)lbSearchUser.SelectedItem;

                if (profile != null)
                {
                    string userId = profile.id;
                    Picture picture = new Picture();
                    Task.Run(async () =>
                    {
                        picture = await endpoint.getImageRecentPublishByUser(userId);

                    }).Wait();
                    pbRecentImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbRecentImage.Load(picture.url);
                    lbPictureId.Text = picture.id;
                    loadComments();
                    InitTimer();
                }

            }
            catch
            {
                MessageBox.Show(Configuaration.ERROR_MESSAGE);
            }
        }

        /// <summary>
        /// Calculate progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
        }

        /// <summary>
        /// Show progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show(Configuaration.DOWNLOAD_SUCCESS + Global.DIRECTORY);
            System.Diagnostics.Process.Start(Global.DIRECTORY);
        }
        
        private void loadComments()
        {
            Endpoint endpoint = new Endpoint();
            List<Comment> listComment = new List<Comment>();
            Task.Run(async () =>
            {
                listComment = await endpoint.loadComments(lbPictureId.Text);
            }).Wait();
            List<DataListBox> listData = new List<DataListBox>();
           
            foreach(var item in listComment)
            {
                DataListBox data = new DataListBox();
                data.id = item.id;
                data.content = item.from.username + ": " + item.text;
                listData.Add(data);
            }

            lbComment.DataSource = listData;
            lbComment.DisplayMember = "content";
            lbComment.ValueMember = "id";
            lbComment.SelectedIndex = lbComment.Items.Count - 1;
        }

        void Download(Picture picture)
        {
            using (WebClient client = new WebClient())
            {
                if (picture.url != null)
                {
                    string name = picture.url.Split('/').LastOrDefault();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(picture.url), Global.DIRECTORY + name);
                }
            }
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();

            if (!string.IsNullOrEmpty(lbPictureId.Text))
            {
                try
                {
                    endpoint.likeImage(lbPictureId.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
