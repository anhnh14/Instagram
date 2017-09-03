using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoInstagram.APIsHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoInstagram.Model;
using DemoInstagram.Support;
using DemoInstagram.APIsHelper.APIsInterface;
using DemoInstagram.Business.Interface;
using DemoInstagram.Business;
using Microsoft.Practices.Unity;

namespace DemoInstagram.APIsHelper.Tests
{
    [TestClass()]
    public class EndpointTests
    {
        /// <summary>
        /// Test case success
        /// </summary>
        [TestMethod()]
        public void GetProfileTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Profile profile = null;
            Task.Run(async () =>
            {
                profile = await endpoint.GetProfile();
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(profile);
            Assert.AreEqual(profile.full_name, "Anh Nguyen");

        }

        /// <summary>
        /// Test case empty token
        /// </summary>
        [TestMethod]
        public void GetProfileTest1()
        {
            Global.TOKEN = "";
            Exception exception = null;
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Profile profile = null;
            Task.Run(async () =>
            {
                try
                {
                    profile = await endpoint.GetProfile();
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
        public void SearchUserTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            List<Profile> listProfile = null;
            Task.Run(async () =>
            {
                listProfile = await endpoint.SearchUser("anh");
            }).GetAwaiter().GetResult();

            Assert.IsTrue(listProfile.Count > 0);
        }

        /// <summary>
        /// Test search User with empty token
        /// </summary>
        [TestMethod]
        public void SearchUserTest1()
        {
            Global.TOKEN = "";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Exception exception = null;
            List<Profile> listProfile = null;
            Task.Run(async () =>
            {
                try
                {
                    listProfile = await endpoint.SearchUser("anh");
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
        public void SearchUserTest2()
        {
            Global.TOKEN = "5964438851.abb738d.8d2695e0f6624fc9ab2c37dd02f841cd";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Exception exception = null;
            List<Profile> listProfile = null;
            Task.Run(async () =>
            {
                try
                {
                    listProfile = await endpoint.SearchUser("anh");
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
        public void GetRecentImageTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Picture picture = null;
            Task.Run(async () =>
            {
                picture = await endpoint.GetRecentImage();
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(picture);
            Assert.IsNotNull(picture.url);
        }

        /// <summary>
        /// Test case get recent image with empty token
        /// </summary>
        [TestMethod()]
        public void GetRecentImageTest1()
        {
            Global.TOKEN = "";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Picture picture = null;
            Exception exception = null;
            Task.Run(async () =>
            {
                try
                {
                    picture = await endpoint.GetRecentImage();
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
        public void GetImageRecentPublishByUserTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Picture picture = null;
            Task.Run(async () =>
            {

                picture = await endpoint.GetImageRecentPublishByUser("5964438851");


            }).GetAwaiter().GetResult();
            Assert.IsNotNull(picture);
        }

        /// <summary>
        /// Test case get recent image publish by user with empty token
        /// </summary>
        [TestMethod()]
        public void GetImageRecentPublishByUserTest1()
        {
            Global.TOKEN = "";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            Picture picture = null;
            Exception exception = null;
            Task.Run(async () =>
            {
                try
                {
                    picture = await endpoint.GetImageRecentPublishByUser("5964438851");
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
        public void PostCommentTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            bool test = false;
            Task.Run(async () =>
            {
                test = await endpoint.PostComment("nice", "1593619934200949826_5964438851");
            }).Wait();
            Assert.IsTrue(test);
        }

        /// <summary>
        /// Test case post commentwith more than 4 hashtag
        /// </summary>
        [TestMethod()]
        public void PostCommentTest1()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            bool test = false;
            Task.Run(async () =>
            {
                test = await endpoint.PostComment("#a #b #c #d #e", "1593619934200949826_5964438851");
            }).Wait();
            Assert.IsFalse(test);
        }

        /// <summary>
        /// Test case load comment success
        /// </summary>
        [TestMethod()]
        public void LoadCommentsTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            List<Comment> listComment = new List<Comment>();
            Task.Run(async () =>
            {
                listComment = await endpoint.LoadComments("1593619934200949826_5964438851");
            }).Wait();


            Assert.IsTrue(listComment.Count > 0);
        }

        /// <summary>
        /// Test case load comment fail
        /// </summary>
        [TestMethod()]
        public void LoadCommentsTest1()
        {
            Global.TOKEN = "5964438851.abb738d.8d2695e0f6624fc9ab2c37dd02f841cd";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            List<Comment> listComment = new List<Comment>();
            Exception exception = null;
            Task.Run(async () =>
            {
                try
                {
                    listComment = await endpoint.LoadComments("");
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
        public void LikeImageTest()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            bool testSuccess = false;
            Task.Run(async () =>
            {
                testSuccess = await endpoint.LikeImage("1593619934200949826_5964438851");
            }).Wait();

            Assert.IsTrue(testSuccess);
        }

        /// <summary>
        /// Test like image fail
        /// </summary>
        [TestMethod()]
        public void LikeImageTest1()
        {
            Global.TOKEN = "39217616.abb738d.964d271718624e29a213d5b8d602ccf7";
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instance = container.Resolve<InstagramBussinessImpl>();
            EndpointImpl endpoint = new EndpointImpl(instance);
            bool testSuccess = false;
            Task.Run(async () =>
            {
                testSuccess = await endpoint.LikeImage("");
            }).Wait();

            Assert.IsFalse(testSuccess);
        }
    }
}