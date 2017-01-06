using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubnautiClean
{
    public partial class Form1 : Form
    {
        private string rootPath;
        private string configPath;
        private string subnauticaPath;
		private ReaperXml reaperXml;

		public Form1()
        {
            InitializeComponent();
        }

        public void UserConfig()
        {
            rootPath = AppDomain.CurrentDomain.BaseDirectory;
            configPath = System.IO.Path.Combine(rootPath, "config.txt");
            // create config file if none exists
            if(!System.IO.File.Exists(configPath)) {
                System.IO.FileStream fs = System.IO.File.Create(configPath);
            } else {
                Console.WriteLine("Config file already exists");
            }
        }

        public void WriteUserPath()
        {
            // Verify the file path actually exists
            subnauticaPath = textBox1.Text;
            if(System.IO.Path.GetDirectoryName(subnauticaPath) == String.Empty) {
                // TODO: Open an error message window
                label1.Text = "Invalid File Path!";
            } else {
                // Set up Stream Writer and pass it the config path
                System.IO.StreamWriter objWriter;
                objWriter = new System.IO.StreamWriter(configPath);

                // TODO: Check for file access before writing (otherwise writing is blocked if file is open)
                Console.WriteLine("StreamWriter" + objWriter);
                objWriter.Write(subnauticaPath);
                objWriter.Close();
                label1.Text = "Saved your text!";
            }

            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string userText = textBox1.Text;
			//label1.Text = "You clicked Enter!\n" + "You entered: " + userText;
			//WriteUserPath();

			//reaperXml.CreateSubXml();
			reaperXml.ReadSubXml();
			label1.Text = "Read XML file!";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UserConfig();
			reaperXml = new ReaperXml();
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            //bool firstFocus = true;
            //if (firstFocus)
            //{
            //    textBox1.Text = "";
            //    firstFocus = false;
            //}
        }

    }
}
