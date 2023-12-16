using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;


namespace PhotoViewer
{
    public partial class Form1 : Form
    {
        ArrayList alist = new ArrayList();
        int i = 0;
        int filelength = 0;
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(560, 380);

            timer.Interval = 7000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            nextImage();


        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string localizacao = fbd.SelectedPath;
                DirectoryInfo inputDir = new DirectoryInfo(localizacao);

                try
                {
                    if ((inputDir.Exists))
                    {
                        FileInfo file = null;
                        foreach (FileInfo eachfile in inputDir.GetFiles())
                        {
                            file = eachfile;
                            if (file.Extension == ".jpg")
                            {
                                alist.Add(file.FullName);
                                filelength = filelength + 1;
                            }
                        }

                        pictureBox1.Image = Image.FromFile(alist[0].ToString());
                        i = 0;
                        timer.Enabled = true;
                    }
                }
                catch (Exception ex)
                    {
                        MessageBox.Show("Please check if the folder you have selected contains images, and make sure they are in the correct format for the application (.JPG only)" ,"Read the message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

        private void previousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousImage();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextImage();
        }

        void nextImage()
        {
            if (i + 1 < filelength)
            {
                pictureBox1.Image = Image.FromFile(alist[i + 1].ToString());
                i = i + 1;
            }
            else
            {
                i = -1;
                GC.Collect();
            }
        }

        void previousImage()
        {
            if (i - 1 >= 0)
            {
                pictureBox1.Image = Image.FromFile(alist[i - 1].ToString());
                i = i - 1;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 fabout = new AboutBox1();
            fabout.ShowDialog();
        }
    }
}
