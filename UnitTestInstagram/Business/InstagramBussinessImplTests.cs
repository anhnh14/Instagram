﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoInstagram.Business.Interface;
using DemoInstagram.APIsHelper;
using System.Net.Http;
using DemoInstagram.Model;

namespace DemoInstagram.Business.Tests
{
    [TestClass()]
    public class InstagramBussinessImplTests
    {
        /// <summary>
        /// Test Get path function
        /// </summary>
        [TestMethod()]
        public void GetPathTest()
        {
            IBusiness business = new InstagramBussinessImpl();
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            string json = business.GetPath(Configuaration.API_USER, "self/?access_token=");

            Assert.AreEqual(json, "https://api.instagram.com/v1/users/self/?access_token=39217616.abb738d.964d271718624e29a213d5b8d602ccf7");
        }

        /// <summary>
        /// Test Process list profile from JSON
        /// </summary>
        [TestMethod()]
        public void ProcessListProfileTest()
        {
            string json = "{'data': [{'id': '1247548217', 'username': 'huyle7330', 'profile_picture': 'https://scontent.cdninstagram.com/t51.2885-19/11243669_1633339570268365_1188831741_a.jpg', 'full_name': 'Huy Le', 'bio': '', 'website': '', 'is_business': false}], 'meta': {'code': 200}}";
            IBusiness bussiness = new InstagramBussinessImpl();
            List<Profile> listProfile = bussiness.ProcessListProfile(json);

            Assert.IsNotNull(listProfile);
        }

        /// <summary>
        /// Test Process list profile from string json wrong format
        /// </summary>
        [TestMethod()]
        public void ProcessListProfileWrongJsonFormatTest()
        {
            string json = "";
            Exception exception = null;
            IBusiness bussiness = new InstagramBussinessImpl();
            List<Profile> listProfile = new List<Profile>();
            try
            {
                listProfile = bussiness.ProcessListProfile(json);

            }catch(Exception ex)
            {
                exception = ex;
            }

            Assert.IsTrue(listProfile.Count == 0);
            Assert.IsNotNull(exception);
        }

        /// <summary>
        /// Test Process picture Exception
        /// </summary>
        [TestMethod()]
        public void ProcessPictureExceptionTest()
        {
            Picture picture = new Picture();
            IBusiness instagramBusinees = new InstagramBussinessImpl();
            Exception exception = null;
            string jsonString = "";
            try
            {
                picture = instagramBusinees.ProcessPicture(jsonString);

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
        }

        /// <summary>
        /// Test Process picture Success
        /// </summary>
        [TestMethod()]
        public void ProcessPictureTest()
        {
            string path = "https://api.instagram.com/v1/users/5964438851/media/recent/?access_token=39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Picture picture = new Picture();
            IBusiness instagramBusinees = new InstagramBussinessImpl();
            Exception exception = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Task.Run(async () =>
            {
                response = await client.GetAsync(path);
            }).Wait();
            string jsonString = response.Content.ReadAsStringAsync().Result;
            try
            {
                picture = instagramBusinees.ProcessPicture(jsonString);

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNull(exception);
            Assert.IsNotNull(picture);
        }

        /// <summary>
        /// Test Process picture wrong format Json
        /// </summary>
        [TestMethod()]
        public void ProcessPictureWrongJsonFormatTest()
        {
            Picture picture = null;
            IBusiness instagramBusinees = new InstagramBussinessImpl();
            Exception exception = null;
            string jsonString = "aa";
            try
            {
                picture = instagramBusinees.ProcessPicture(jsonString);

            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsNull(picture);
        }

        /// <summary>
        /// Test case load comment
        /// </summary>
        [TestMethod()]
        public void LoadCommentTest()
        {
            string path = "https://api.instagram.com/v1/media/1593619934200949826_5964438851/comments?access_token=39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            List<Comment> listComment = new List<Comment>();
            IBusiness instagramBusinees = new InstagramBussinessImpl();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Task.Run(async () =>
            {
                response = await client.GetAsync(path);
            }).Wait();
            string jsonString = response.Content.ReadAsStringAsync().Result;
            listComment = instagramBusinees.LoadComment(jsonString);

            Assert.IsTrue(listComment.Count > 0);
        }

        /// <summary>
        /// Test case load comment wrong format Json
        /// </summary>
        [TestMethod()]
        public void LoadCommentWrongFormatJsonTest()
        {
            List<Comment> listComment = new List<Comment>();
            IBusiness instagramBusinees = new InstagramBussinessImpl();
            Exception exception = new Exception();
            string jsonString = "";
            try
            {
                listComment = instagramBusinees.LoadComment(jsonString);

            }catch(Exception ex)
            {
                exception = ex;
            }

            Assert.IsTrue(listComment.Count == 0);
            Assert.IsNotNull(exception);
        }

        /// <summary>
        /// Test case process json error
        /// Invalid token
        /// </summary>
        [TestMethod()]
        public void ProcessErrorTest()
        {
            string path = "https://api.instagram.com/v1/media/1593619934200949826_5964438851/comments?access_token=39217616.abb738d.964d271718624e29a213d5b8d602ccf";
            List<Comment> listComment = new List<Comment>();
            IBusiness instagramBusinees = new InstagramBussinessImpl();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            Task.Run(async () =>
            {
                response = await client.GetAsync(path);
            }).Wait();
            string result = instagramBusinees.ProcessError(response);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "The access_token provided is invalid.");
        }
    }
}