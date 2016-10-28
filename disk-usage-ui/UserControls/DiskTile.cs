using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using ProgressBarState = disk_usage.ProgressBarState;
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

            //usageBar.Visible = false;     
            usageBar.Minimum = 0;
            usageBar.Maximum = 100;
            usageBar.Value = 1;
            usageBar.SetState(ProgressBarState.Error);
            detailLabel.Text = "";
        }

        public void UpdateUserInterface(disk_usage.PathRecord pathRecord)
        {
            _recordReference = pathRecord;
            UpdateUserInterface();
        }

        public void UpdateUserInterface() //disk_usage.PathRecord pathRecord)
        {
            disk_usage.PathRecord pathRecord = _recordReference;

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

            //usageBar.Visible = true;

            usageBar.Minimum = 0;
            usageBar.Maximum = 100;

            Console.WriteLine($"Setting progress bar for {path} to {pathRecord.FillLevel}");

            usageBar.Value = pathRecord.FillLevel;

            usageBar.SetState(pathRecord.HasLowDiskSpace ? ProgressBarState.Error : ProgressBarState.Normal);

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

            UpdateUserInterface(); 
        }

        private void Pr_DiskInfoUpdated(object sender, EventArgs e)
        {
            UpdateUserInterface();
        }

        public void UnsubscribeToEvents()
        {
            _recordReference.DiskInfoUpdated -= Pr_DiskInfoUpdated;
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

        void propertiesButton_Click(object sender, EventArgs e)
        {
            try
            {
                disk_usage.Windows.ShowFileProperties(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }


        public static void SetState(this ProgressBar pBar, disk_usage.ProgressBarState state)
        {
            pBar.SetState((int)state);
        }
    }
}
