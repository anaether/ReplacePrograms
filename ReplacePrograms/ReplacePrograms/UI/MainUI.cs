using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReplacePrograms.UI
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();

            // UI Settings
            this.Text = "ReplacePrograms";
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.MinimumSize = new Size(381, 151);
            this.MaximumSize = new Size(381, 151);
            this.MaximizeBox = false;
            this.CenterToScreen();
        }
    }
}