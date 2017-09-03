using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Practices.Unity;
using DemoInstagram.APIsHelper.APIsInterface;
using DemoInstagram.APIsHelper;
using DemoInstagram.SupportDI;

namespace DemoInstagram
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            //Call container support for Dependency injection
            var container = UnityContainerSuppor.BuildUnityContainer();
            var endPoint = container.Resolve<IEndpoint>();
            Application.Run(new Form1(endPoint));
        }
        

    }
}
