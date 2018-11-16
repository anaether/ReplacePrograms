using System.Drawing;
using System.Windows.Forms;
using ReplacePrograms.Utils;

namespace ReplacePrograms.UI
{
    public partial class MainUI : Form
    {
        private Size ProgramSize = new Size(381, 151);

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
    }
}