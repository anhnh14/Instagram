using DemoInstagram.APIsHelper;
using DemoInstagram.APIsHelper.APIsInterface;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInstagram.Business.Interface;
using DemoInstagram.Business;

namespace DemoInstagram.Support
{
    public class UnityContainerSuppor
    {
        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IEndpoint, EndpointImpl>();
            container.RegisterType<IBusiness, InstagramBussinessImpl>();
            return container;
        }
    }
}
