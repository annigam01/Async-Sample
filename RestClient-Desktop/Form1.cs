using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace RestClient_Desktop
{
    public partial class Form1 : Form
    {
        ArrayList AllDir = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "working...";
            label1.Text = GetDirectoryCount(@"C:\Users\annigam\");
            
        }
        
        //Step1 - write the method that does the actual work
        private string GetDirectoryCount(string CurrentDir)
        {
            //listBox1.Items.Add(CurrentDir);
            AllDir.Add(CurrentDir);
            
            try
            {
                
                string[] AllDirs = System.IO.Directory.GetDirectories(CurrentDir);

                foreach (string Dir in AllDirs)
                {
                    AllDir.Add(Dir);
                    GetDirectoryCount(Dir);
                }


            }
            catch (Exception)
            {
                
            }

            return AllDir.Count.ToString();
            
        }

        //Step2- make a task wrapper around the actual method
        private Task<string> GetDirectoryCountAsync(string CurDir)
        {
            return Task.Factory.StartNew(() => GetDirectoryCount(CurDir));

        }

        //Step 3 - mark the calling method as async and use await keyword when calling the wrapper method
        private async void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "working";
            var result = await GetDirectoryCountAsync(@"C:\Users\annigam\");
            label1.Text = result;

        }
    }
}
