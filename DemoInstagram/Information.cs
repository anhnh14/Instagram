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

                client.DownloadFileAsync(new Uri(profile.profile_picture), Global.DIRECTORY + name);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            try
            {

                Picture recentImage = new Picture();
                Task.Run(async () =>
                {
                    recentImage = await endpoint.getRecentImage();
                }).GetAwaiter().GetResult();
                using (WebClient client = new WebClient())
                {
                    if (recentImage.url != null)
                    {
                        string name = recentImage.url.Split('/').LastOrDefault();
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                        client.DownloadFileAsync(new Uri(recentImage.url), Global.DIRECTORY + name);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Endpoint endPoint = new Endpoint();

            string search = tbSearchUser.Text;
            try
            {
                List<Profile> profile = new List<Profile>();
                Task.Run(async () =>
                {
                    profile = await endPoint.searchUser(search);
                }).GetAwaiter().GetResult();
                lbSearchUser.DataSource = profile;
                lbSearchUser.DisplayMember = "full_name";
                lbSearchUser.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btDownloadReccentImageofUser_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            try
            {
                Profile profile = (Profile)lbSearchUser.SelectedItem;
                if (profile != null)
                {
                    string userId = profile.id;
                    Picture image = new Picture();
                    Task.Run(async () =>
                    {
                        image = await endpoint.getImageRecentPublishByUser(userId);
                    }).GetAwaiter().GetResult();
                    using (WebClient client = new WebClient())
                    {
                        if (image.url != null)
                        {
                            string name = image.url.Split('/').LastOrDefault();
                            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                            client.DownloadFileAsync(new Uri(image.url), Global.DIRECTORY + name);
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
            progressBar1.Value = 0;
            progressBar1.Maximum = (int)e.TotalBytesToReceive / 100;
            progressBar1.Value = (int)e.BytesReceived / 100;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show(Configuaration.DOWNLOAD_SUCCESS + Global.DIRECTORY);

        }

    }
}
