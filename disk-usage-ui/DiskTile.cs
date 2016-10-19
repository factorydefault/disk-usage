using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace disk_usage_ui
{
    public partial class DiskTile : UserControl
    {

        public event EventHandler<EventArgs> RemoveRequested;

        public event EventHandler<EventArgs> AddNewPath;


        public new event EventHandler DoubleClick
        {
            add
            {
                base.DoubleClick += value;
                foreach (Control control in Controls)
                {
                    control.DoubleClick += value;
                }
            }
            remove
            {
                base.DoubleClick -= value;
                foreach (Control control in Controls)
                {
                    control.DoubleClick -= value;
                }
            }
        }

        public string path = "";

        public DiskTile()
        {
            InitializeComponent();
        }

        public void SetAsNotFound(string label = "")
        {
            if(!string.IsNullOrWhiteSpace(label))
            {
                nameLabel.Text = label;
            }
                        
            usageBar.Minimum = 0;
            usageBar.Maximum = 100;
            usageBar.Value = 0;
            detailLabel.Text = "Path not found";
        }

        public void VariablesFromComputer(disk_usage.Computer computer)
        {
            nameLabel.Text = $"{computer.FriendlyName}";

            path = computer.Path;

            usageBar.Minimum = 0;
            usageBar.Maximum = 100;
            usageBar.Value = computer.FillLevel;

            usageBar.SetState((usageBar.Value > 80) ? 2 : 1);

            detailLabel.Text = $"{computer.FreeSpace} GB free of {computer.TotalSpace} GB";

            if (computer.TotalSpace < 0.0001) //edge case where path has not been found
            {
                SetAsNotFound();
            }

        }

        public DiskTile(disk_usage.Computer computer) : this()
        {
            VariablesFromComputer(computer);
        }

        void DiskTile_DoubleClick(object sender, EventArgs e)
        {
            OpenFolder();
        }

        void removeItemButton_Click(object sender, EventArgs e)
        {
            RemoveRequested?.Invoke(this, new EventArgs());
        }

        void tileAddPathButton_Click(object sender, EventArgs e)
        {
            AddNewPath?.Invoke(this, new EventArgs());
        }

        void openFolderButton_Click(object sender, EventArgs e)
        {
            OpenFolder();
        }

        void OpenFolder()
        {
            if (System.IO.Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
