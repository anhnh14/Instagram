using DemoInstagram.APIsHelper;
using DemoInstagram.APIsHelper.APIsInterface;
using DemoInstagram.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoInstagram
{
    public partial class Information : Form
    {
        Profile profile = new Profile();
        private readonly IEndpoint _endPoint;
         
        public Information(Profile profile, IEndpoint endPoint)
        {
            InitializeComponent();
            this.profile = profile;
            lbUsername.Text = profile.full_name;
            _endPoint = endPoint;
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
        private async void btnDownloaRecentPicture_Click(object sender, EventArgs e)
        {
            try
            {
                Task<Picture> getPicture = _endPoint.GetRecentImage();
                Picture recentPicture = await getPicture;
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
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            pbRecentImage.Image = null;
            lbPictureId.Text = null;
            lbComment.DataSource = null;

            string search = tbSearchUser.Text;
            try
            {
                List<Profile> listProfile = new List<Profile>();

                listProfile = await _endPoint.SearchUser(search);
                Task<List<Profile>> getListProfile = _endPoint.SearchUser(search);

                listProfile = await getListProfile;
                lbSearchUser.DataSource = listProfile;
                lbSearchUser.DisplayMember = Configuaration.KEY_API_FULL_NAME;
                lbSearchUser.ValueMember = Configuaration.KEY_API_ID;
                if (listProfile.Count > 0)
                {
                    pbRecentAvatar.Load(listProfile.FirstOrDefault().profile_picture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// Down oad Recent image published by user from search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDownloadReccentImageofUser_Click(object sender, EventArgs e)
        {
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
        private async void btnPost_Click(object sender, EventArgs e)
        {
            

            //Get User 
            Profile profile = (Profile)lbSearchUser.SelectedItem;
            if (profile != null)
            {
                string comment = tbComment.Text;
                try
                {
                    Task<bool> post = _endPoint.PostComment(comment, lbPictureId.Text);
                    await post;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
            tbComment.Text = "";
            loadComments();
        }

        /// <summary>
        /// Show recent image published by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnShow_Click(object sender, EventArgs e)
        {
           
            try
            {
                Profile profile = (Profile)lbSearchUser.SelectedItem;

                if (profile != null)
                {
                    string userId = profile.id;

                    Task<Picture> getPicture = _endPoint.GetImageRecentPublishByUser(userId);
                    Picture picture = await getPicture;
                    pbRecentImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbRecentImage.Load(picture.url);
                    lbPictureId.Text = picture.id;
                    loadComments();
                }

            }
            catch
            {
                MessageBox.Show(Configuaration.ERROR_MESSAGE);
            }
        }

        /// <summary>
        /// Like image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLike_Click(object sender, EventArgs e)
        {
           
            bool checkSuccess = false;
            if (!string.IsNullOrEmpty(lbPictureId.Text))
            {
                try
                {
                    Task<bool> like = _endPoint.LikeImage(lbPictureId.Text);
                    await like;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (checkSuccess)
                {
                    MessageBox.Show(Configuaration.SUCCESS);
                }
            }
        }

        /// <summary>
        /// Load comment from image
        /// </summary>
        private async void loadComments()
        {
            List<Comment> listComment = new List<Comment>();

            try
            {
                Task<List<Comment>> getListComment = _endPoint.LoadComments(lbPictureId.Text);
                listComment = await getListComment;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            List<DataListBox> listData = new List<DataListBox>();

            foreach (var item in listComment)
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

        /// <summary>
        /// Download picture
        /// </summary>
        /// <param name="picture"></param>
        private void Download(Picture picture)
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

        /// <summary>
        /// Calculate progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
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
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show(Configuaration.DOWNLOAD_SUCCESS + Global.DIRECTORY);
            System.Diagnostics.Process.Start(Global.DIRECTORY);
        }
    }
}
