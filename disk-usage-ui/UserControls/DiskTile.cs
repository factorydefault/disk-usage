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

            if (Program.Theme != null)
            {
                pictureBox.Image = Program.Theme.NetworkDiskImage;
            }
        }

        public void SetAsNotFound(string label = "")
        {
            if(!string.IsNullOrWhiteSpace(label))
            {
                nameLabel.Text = label;
            }

            pictureBox.Image = Program.Theme.NotFoundImage;
                        
            usageBar.Minimum = 0;
            usageBar.Maximum = 100;
            usageBar.Value = 0;
            detailLabel.Text = "Path not found";
        }

        public void VariablesFromComputer(disk_usage.PathRecord pathRecord)
        {
            nameLabel.Text = $"{pathRecord.FriendlyName}";

            path = pathRecord.Path;

            pictureBox.Visible = true;

            switch (pathRecord.Location())
            {
                case disk_usage.PathLocation.Local:
                    pictureBox.Image = Program.Theme.LocalDiskImage;
                    break;
                case disk_usage.PathLocation.OS:
                    pictureBox.Image = Program.Theme.OSDiskImage;
                    break;
                default:
                    pictureBox.Image = Program.Theme.NetworkDiskImage;
                    break;
            }


            usageBar.Minimum = 0;
            usageBar.Maximum = 100;
            usageBar.Value = pathRecord.FillLevel;

            usageBar.SetState((usageBar.Value > 80) ? 2 : 1);

            detailLabel.Text = $"{pathRecord.FreeSpace} GB free of {Math.Round(pathRecord.TotalSpace,0,MidpointRounding.AwayFromZero)} GB";

            if (pathRecord.TotalSpace < 0.0001) //edge case where path has not been found
            {
                SetAsNotFound();
            }

        }

        disk_usage.PathRecord _recordReference;

        public DiskTile(disk_usage.PathRecord pr) : this()
        {
            _recordReference = pr;
            _recordReference.DiskInfoUpdated += Pr_DiskInfoUpdated;

            VariablesFromComputer(_recordReference);
        }

        private void Pr_DiskInfoUpdated(object sender, EventArgs e)
        {
            VariablesFromComputer(_recordReference);
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

        private void clipboardButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(path);
            }
            catch (Exception ex)
            { 

                Console.Write(ex.Message);
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
