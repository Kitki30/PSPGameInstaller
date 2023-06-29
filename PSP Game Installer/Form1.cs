using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace PSP_Game_Installer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach (var drive in DriveInfo.GetDrives())
            {
                double freeSpace = drive.TotalFreeSpace;
                double totalSpace = drive.TotalSize;
                double percentFree = (freeSpace / totalSpace) * 100;
                float num = (float)percentFree;

                comboBox1.Items.Add(drive.Name);
            }
        }
        string[] game;
        string hom;
        int homebrew = 0;
        int iso = 0;
        string gameiso = "";
        int col = 0;
        int coll = 0;
        int colll = 0;
        void showFolder()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                iso = 0;
                hom = folderBrowserDialog1.SelectedPath;         
                homebrew = 1;
                listBox1.Items.Clear();
                listBox1.Items.Add("Games:");
                listBox1.Items.Add(hom);
            }

        }
        void showIso()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                iso = 1;
                homebrew = 0;
                game = openFileDialog1.FileNames;
                listBox1.Items.Clear();
                listBox1.Items.Add("Games:");
                foreach (string file in game)
                {
                    listBox1.Items.Add(file);
                }
            }

            

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, string psp)
        {
      
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Cant get dir: "
                        + sourceDirName);
                }

                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }

                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs, psp);
                    }
                }

                MessageBox.Show("Game Installed On PSP(" + psp + ")"); 
          
            
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showFolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showIso();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (listBox1.ItemHeight == 1)
            {
               
            }
            else
            {
                if (comboBox1.Text == "")
                {

                }
                else
                {
                    Random k = new Random();
                    col = k.Next(0, 255);
                    coll = k.Next(0, 255);
                    colll = k.Next(0, 255);

                    button3.ForeColor = Color.FromArgb(col, coll, colll);
                    timer1.Interval = k.Next(1, 1000);
                }
            }
           
        }
        //
        //source destination copy?Y/N
        private void button3_Click(object sender, EventArgs e)
        {
            string psp = comboBox1.Text;
            if (comboBox1.Text == "")
            {
              MessageBox.Show("Error: No PSP Selected", "PSP Game Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (listBox1.ItemHeight == 1)
            {
                MessageBox.Show("Error: No Game Selected", "PSP Game Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (homebrew == 1) {
                
                string NameFolder = new DirectoryInfo(hom).Name;
                string dest = psp+@"PSP\GAME\"+NameFolder;

                DirectoryCopy(hom, dest, true, psp); }
            if (iso == 1)
            {
                string dest = psp + @"ISO\";
                try
                {
                    foreach (string file in game)
                    {
                        File.Copy(file, dest, true);
                    }
                }
                catch
                {
                    MessageBox.Show("Error: Something went wrong", "PSP Game Installer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                double freeSpace = drive.TotalFreeSpace;
                double totalSpace = drive.TotalSize;
                double percentFree = (freeSpace / totalSpace) * 100;
                float num = (float)percentFree;

                comboBox1.Items.Add(drive.Name);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
