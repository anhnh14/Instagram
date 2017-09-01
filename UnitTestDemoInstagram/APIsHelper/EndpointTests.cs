using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoInstagram.APIsHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInstagram.Model;

namespace DemoInstagram.APIsHelper.Tests
{
    [TestClass()]
    public class EndpointTests
    {
        /// <summary>
        /// Test case success
        /// </summary>
        [TestMethod()]
        public void getProfileTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            Profile profile = null;
            Task.Run(async () =>
            {
                profile = await endpoint.getProfile();
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(profile);
            Assert.AreEqual(profile.full_name, "Anh Nguyen");

        }

        /// <summary>
        /// Test case empty token
        /// </summary>
        [TestMethod]
        public void getProfileTest1()
        {
            Global.TOKEN = "";
            Exception exception = null;
            Endpoint endpoint = new Endpoint();
            Profile profile = null;
            Task.Run(async () =>
            {
                try
                {
                    profile = await endpoint.getProfile();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }).GetAwaiter().GetResult();

            Assert.IsNull(profile);
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Missing client_id or access_token URL parameter.");
        }

        /// <summary>
        /// Test case search user with valid token
        /// </summary>
        [TestMethod()]
        public void searchUserTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            List<Profile> listProfile = null;
            Task.Run(async () =>
            {
                listProfile = await endpoint.searchUser("anh");
            }).GetAwaiter().GetResult();

            Assert.IsTrue(listProfile.Count > 0);
        }

        /// <summary>
        /// Test search User with empty token
        /// </summary>
        [TestMethod]
        public void searchUserTest1()
        {
            Global.TOKEN = "";
            Endpoint endpoint = new Endpoint();
            Exception exception = null;
            List<Profile> listProfile = null;
            Task.Run(async () =>
            {
                try
                {
                    listProfile = await endpoint.searchUser("anh");
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }).GetAwaiter().GetResult();

            Assert.IsNull(listProfile);
            Assert.IsNotNull(exception);
        }

        /// <summary>
        /// Test search User with token not authorize for this function
        /// </summary>
        [TestMethod]
        public void searchUserTest2()
        {
            Global.TOKEN = "5964438851.abb738d.8d2695e0f6624fc9ab2c37dd02f841cd";
            Endpoint endpoint = new Endpoint();
            Exception exception = null;
            List<Profile> listProfile = null;
            Task.Run(async () =>
            {
                try
                {
                    listProfile = await endpoint.searchUser("anh");
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }).GetAwaiter().GetResult();

            Assert.IsNull(listProfile);
            Assert.IsNotNull(exception);
        }

        /// <summary>
        /// Test case get recent images publish by onwer token
        /// </summary>
        [TestMethod()]
        public void getRecentImageTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            Picture picture = null;
            Task.Run(async () =>
            {
                picture = await endpoint.getRecentImage();
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(picture);
            Assert.IsNotNull(picture.url);
        }

        /// <summary>
        /// Test case get recent image with empty token
        /// </summary>
        [TestMethod()]
        public void getRecentImageTest1()
        {
            Global.TOKEN = "";
            Endpoint endpoint = new Endpoint();
            Picture picture = null;
            Exception exception = null;
            Task.Run(async () =>
            {
                try
                {
                    picture = await endpoint.getRecentImage();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

            }).GetAwaiter().GetResult();

            Assert.IsNull(picture);
            Assert.IsNotNull(exception);
        }


        /// <summary>
        /// Test case get recent image publish by user with valid token
        /// </summary>
        [TestMethod()]
        public void getImageRecentPublishByUserTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            Picture picture = null;
            Task.Run(async () =>
            {

                picture = await endpoint.getImageRecentPublishByUser("5964438851");


            }).GetAwaiter().GetResult();
            Assert.IsNotNull(picture);
        }

        /// <summary>
        /// Test case get recent image publish by user with empty token
        /// </summary>
        [TestMethod()]
        public void getImageRecentPublishByUserTest1()
        {
            Global.TOKEN = "";
            Endpoint endpoint = new Endpoint();
            Picture picture = null;
            Exception exception = null;
            Task.Run(async () =>
            {
                try
                {
                    picture = await endpoint.getImageRecentPublishByUser("5964438851");
                }
                catch (Exception ex)
                {
                    exception = ex;
                }


            }).GetAwaiter().GetResult();
            Assert.IsNull(picture);
            Assert.IsNotNull(exception);
        }

        /// <summary>
        /// Test case post comment success
        /// </summary>
        [TestMethod()]
        public void postCommentTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            bool test = false;
            Task.Run(async () =>
            {
                test = await endpoint.postComment("nice", "1593619934200949826_5964438851");
            }).Wait();
            Assert.IsTrue(test);
        }

        /// <summary>
        /// Test case post commentwith more than 4 hashtag
        /// </summary>
        [TestMethod()]
        public void postCommentTest1()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            bool test = false;
            Task.Run(async () =>
            {
                test = await endpoint.postComment("#a #b #c #d #e", "1593619934200949826_5964438851");
            }).Wait();
            Assert.IsFalse(test);
        }



        /// <summary>
        /// Test case load comment success
        /// </summary>
        [TestMethod()]
        public void loadCommentsTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            List<Comment> listComment = new List<Comment>();
            Task.Run(async () =>
            {
                listComment = await endpoint.loadComments("1593619934200949826_5964438851");
            }).Wait();


            Assert.IsTrue(listComment.Count > 0);
        }

        /// <summary>
        /// Test case load comment fail
        /// </summary>
        [TestMethod()]
        public void loadCommentsTest1()
        {
            Global.TOKEN = "5964438851.abb738d.8d2695e0f6624fc9ab2c37dd02f841cd";
            Endpoint endpoint = new Endpoint();
            List<Comment> listComment = new List<Comment>();
            Exception exception = null;
            Task.Run(async () =>
            {
                try
                {
                    listComment = await endpoint.loadComments("");
                }catch(Exception ex)
                {
                    exception = ex;
                }
                
            }).Wait();


            Assert.IsNotNull(exception);
        }


        /// <summary>
        /// Test like image success
        /// </summary>
        [TestMethod()]
        public void likeImageTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            bool testSuccess = false;
            Task.Run(async () =>
            {
                testSuccess = await endpoint.likeImage("1593619934200949826_5964438851");
            }).Wait();

            Assert.IsTrue(testSuccess);
        }

        /// <summary>
        /// Test like image fail
        /// </summary>
        [TestMethod()]
        public void likeImageTest1()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            Endpoint endpoint = new Endpoint();
            bool testSuccess = false;
            Task.Run(async () =>
            {
                testSuccess = await endpoint.likeImage("");
            }).Wait();

            Assert.IsFalse(testSuccess);
        }
    }
}