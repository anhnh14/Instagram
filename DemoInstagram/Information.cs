﻿using DemoInstagram.APIsHelper;
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
            Picture picture = new Picture();
            picture.url = profile.profile_picture;
            Download(picture);

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Endpoint endPoint = new Endpoint();
            pbRecentImage.Image = null;

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

        private void btnPost_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();

            //Get User & picture
            Profile profile = (Profile)lbSearchUser.SelectedItem;
            Picture picture = new Picture();
            if (profile != null)
            {
                string userId = profile.id;
               
                Task.Run(async () =>
                {
                    picture = await endpoint.getImageRecentPublishByUser(userId);
                }).Wait();
            }

            //Post comment
            string comment = tbComment.Text;
            try
            {
                endpoint.postComment(comment, picture.id);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            System.Diagnostics.Process.Start(Global.DIRECTORY);
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

        private void button2_Click(object sender, EventArgs e)
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
                }

            }
            catch
            {
                MessageBox.Show(Configuaration.ERROR_MESSAGE);
            }
        }
    }
}
