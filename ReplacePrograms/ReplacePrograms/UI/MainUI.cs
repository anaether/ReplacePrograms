using System;
using System.Windows.Forms;

namespace ReplacePrograms.UI
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();

            // UI Settings
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
        }
    }
}