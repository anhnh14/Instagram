using DemoInstagram.APIsHelper;
using System;
using System.Net.Http;
using System.Windows.Forms;


namespace DemoInstagram
{
    public partial class Form1 : Form
    {
        static HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            Profile profile = new Profile();
            try
            {
                Global.TOKEN = tbToken.Text;
                profile = await endpoint.getProfile();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            System.IO.Directory.CreateDirectory(Configuaration.FOLDER_NAME);
            this.Hide();
            Information inf = new Information(profile);
            inf.Show();
        }
        
    }
}
