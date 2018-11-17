using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using ReplacePrograms.Utils;
using ReplacePrograms.Models;

namespace ReplacePrograms.UI
{
    public partial class MainUI : Form
    {
        private string SourcePath = "";
        private string DestinationPath = "";

        public MainUI()
        {
            InitializeComponent();

            // For testing:
            this.button2.Visible = this.button2.Enabled = false;
            this.button3.Visible = this.button3.Enabled = false;

            // UI Settings
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.CenterToScreen();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.SourcePath = Path.GetDirectoryName(folderBrowser.FileName);
            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.DestinationPath = Path.GetDirectoryName(folderBrowser.FileName);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //Console.WriteLine((ProgramUtils.HasAdministratorRights()) ? "True" : "False");

            if (this.listboxSource.Items.Count == this.listBoxDestination.Items.Count && (this.listboxSource.Items.Count > 0 && this.listBoxDestination.Items.Count > 0))
            {
                // Create a new Task to run the complete migration behind and to dont freeze the UI.
                Task.Run(() => MigrationUtils.Proceed(ConvertData()));

                // When we are done we can empty the listboxes
                this.listboxSource.Items.Clear();
                this.listBoxDestination.Items.Clear();
            }
            else
            {
                // Display Error Message to user.
                MessageBox.Show("Sie haben entweder Source/Destination Ordner nicht ausgewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Migration> ConvertData()
        {
            List<Migration> data = new List<Migration>();

            if(this.listboxSource.Items.Count == this.listBoxDestination.Items.Count)
            {
                for(int i = 0; i < this.listboxSource.Items.Count; i++)
                {
                    Migration current = new Migration(this.listboxSource.Items[i].ToString(), this.listBoxDestination.Items[i].ToString(), true);
                    data.Add(current);
                }
            }

            return data;
        }

        private void AddSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.listboxSource.Items.Add(Path.GetDirectoryName(folderBrowser.FileName));
            }
        }

        private void AddDestination_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.listBoxDestination.Items.Add(Path.GetDirectoryName(folderBrowser.FileName));
            }
        }
    }
}