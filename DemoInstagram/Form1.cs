using DemoInstagram.APIsHelper;
using System;
using System.Net.Http;
using System.Threading.Tasks;
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Endpoint endpoint = new Endpoint();
            Profile profile = new Profile();
            try
            {
                Global.TOKEN = tbToken.Text;
                Task.Run(async () =>
                {
                    profile = await endpoint.getProfile();
                }).GetAwaiter().GetResult();
                
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
            Information inf = new Information(profile);
            inf.Show();
            inf.FormClosed += new FormClosedEventHandler(frm2_FormClosed);

        }

        private void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Unhide Form1
        }

    }
}
