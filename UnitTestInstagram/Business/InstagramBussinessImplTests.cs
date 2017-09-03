using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoInstagram.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DemoInstagram.Business.Interface;
using DemoInstagram.APIsHelper;

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

            IBusiness business = new InstagramBussinessImpl();
           // List <Profile> listProfile = business.ProcessListProfile(json);

            //Assert.IsNotNull();
        }
    }
}