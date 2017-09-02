using DemoInstagram.APIsHelper;
using DemoInstagram.APIsHelper.APIsInterface;
using DemoInstagram.Business.Interface;
using DemoInstagram.Support;
using Microsoft.Practices.Unity;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DemoInstagram
{
    public partial class Form1 : Form
    {
        static HttpClient client = new HttpClient();

        private readonly IEndpoint _endPoint;
        public Form1(IEndpoint endPoint)
        {
            InitializeComponent();
            _endPoint = endPoint;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            
            Profile profile = new Profile();
            
            //Call container for Dependency injection 
            var container = UnityContainerSuppor.BuildUnityContainer();
            var instanceEndpoint = container.Resolve<IEndpoint>();

            try
            {
                Global.TOKEN = tbToken.Text;

                //Get profile from token
                Task<Profile> getProfile = _endPoint.GetProfile();
                profile = await getProfile;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string path = System.IO.Directory.GetCurrentDirectory();
            Global.DIRECTORY = path + Configuaration.FOLDER_NAME;
            System.IO.Directory.CreateDirectory(Global.DIRECTORY);
            this.Hide();
          
            Information inf = new Information(profile, instanceEndpoint);
            inf.Show();
            inf.FormClosed += new FormClosedEventHandler(frm2_FormClosed);

        }

        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Unhide Form1
        }
        

    }
}
