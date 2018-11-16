using System.IO;
using System.Drawing;
using System.Windows.Forms;
using ReplacePrograms.Utils;

namespace ReplacePrograms.UI
{
    public partial class MainUI : Form
    {
        private Size ProgramSize = new Size(381, 151);
        private string SourcePath = "";
        private string DestinationPath = "";

        public MainUI()
        {
            InitializeComponent();

            // UI Settings
            this.Text = "Replace Programs";
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.MinimumSize = ProgramSize;
            this.MaximumSize = ProgramSize;
            this.Size = ProgramSize;
            this.MaximizeBox = false;
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
            if (this.SourcePath.Length > 0 && this.DestinationPath.Length > 0)
            {
                MigrationUtils.ProceedMigration(this.SourcePath, this.DestinationPath);
            }
            else
            {
                // Display Error Message to user.
                MessageBox.Show("Sie haben entweder Source/Destination Ordner nicht ausgewählt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}