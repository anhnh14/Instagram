using DemoInstagram.APIsHelper;
using DemoInstagram.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace DemoInstagram
{
    public partial class Information : Form
    {
        Profile profile = new Profile();
        static HttpClient client = new HttpClient();
        public Information(Profile profile)
        {
            InitializeComponent();
            this.profile = profile;
            lbUsername.Text = profile.full_name;

        }

        private void Information_Load(object sender, EventArgs e)
        {

        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                string name = profile.profile_picture.Split('/').LastOrDefault();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                client.DownloadFileAsync(new Uri(profile.profile_picture), Configuaration.FOLDER_NAME + name);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            try
            {
                Picture recentImage = await endpoint.getRecentImage();
                using (WebClient client = new WebClient())
                {
                    if (recentImage.url != null)
                    {
                        string name = recentImage.url.Split('/').LastOrDefault();
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                        client.DownloadFileAsync(new Uri(recentImage.url), Configuaration.FOLDER_NAME + name);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            Endpoint endPoint = new Endpoint();

            string search = tbSearchUser.Text;
            try
            {
                var profile = await endPoint.searchUser(search);

                lbSearchUser.DataSource = profile;
                lbSearchUser.DisplayMember = "full_name";
                lbSearchUser.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void btDownloadReccentImageofUser_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            try
            {
                Profile profile = (Profile)lbSearchUser.SelectedItem;
                if (profile != null)
                {
                    string userId = profile.id;
                    Picture image = await endpoint.getImageRecentPublishByUser(userId);
                    using (WebClient client = new WebClient())
                    {
                        if (image.url != null)
                        {
                            string name = image.url.Split('/').LastOrDefault();
                            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                            client.DownloadFileAsync(new Uri(image.url), Configuaration.FOLDER_NAME + name);
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show(Configuaration.ERROR_MESSAGE);
            }

        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lbNotification.Text = Configuaration.DOWNLOAD_SUCCESS + Configuaration.FOLDER_NAME;

        }

    }
}
