using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

using ProgressBarState = disk_usage.ProgressBarState;
using disk_usage;

namespace disk_usage_ui
{
    public partial class DiskTile : UserControl
    {

        public event EventHandler<EventArgs> RemoveRequested;

        public event EventHandler<EventArgs> AddNewPath;

        public event EventHandler<EventArgs> PropertiesChanged;

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

        //bool _interactive = true;

        public bool Interactive { get; set; } = true;


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
            usageBar.Value = 100;
            Console.WriteLine($"Setting progress bar not found, value {usageBar.Value}");
            usageBar.SetState(ProgressBarState.Error);

            PathLocation location = _recordReference?.Location() ?? PathLocation.Unknown;

            switch (location)
            {
                case PathLocation.Local:
                case PathLocation.OS:
                    detailLabel.Text = "Path not found";
                    break;
                case PathLocation.Remote:
                    detailLabel.Text = "Path not available";
                    break;
                default:
                    detailLabel.Text = "";
                    break;
            }
        }

        public void UpdateUserInterface(PathRecord pathRecord)
        {
            _recordReference = pathRecord;
            UpdateUserInterface();
        }

        public void UpdateUserInterface() //disk_usage.PathRecord pathRecord)
        {
            PathRecord pathRecord = _recordReference;

            

            nameLabel.Text = $"{pathRecord.FriendlyName}";

            path = pathRecord.Path;

            pictureBox.Visible = true;

            switch (pathRecord.Location())
            {
                case PathLocation.Local:
                    pictureBox.Image = Program.Theme.LocalDiskImage;
                    break;
                case PathLocation.OS:
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

            detailLabel.Text = $"{pathRecord.FreeSpace.ExplorerLabel()} free of {pathRecord.Capacity.ExplorerLabel()}";

            if (pathRecord.Capacity.Bytes < 1) //edge case where path has not been found
            {
                SetAsNotFound();
            }
            else
            {
                usageBar.SetState(pathRecord.HasLowDiskSpace ? ProgressBarState.Error : ProgressBarState.Normal);
            }    
        }

        PathRecord _recordReference;

        public DiskTile(PathRecord pr) : this()
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
            if (Interactive)
            {
                OpenFolder();
            }
            
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
            Tools.OpenDirectory(path);
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
                //disk_usage.Windows.ShowFileProperties(path);
                var propertiesForm = new Forms.PropertiesForm();
                propertiesForm.ProvideData(_recordReference);

                string existingFriendlyName = _recordReference.FriendlyName;
                
                var dr = propertiesForm.ShowDialog();

                
                switch (dr)
                {
                    case DialogResult.OK:
                        _recordReference.FriendlyName = propertiesForm.DiskLabel;
                        PropertiesChanged?.Invoke(this, new EventArgs());
                        break;
                    default:
                        _recordReference.FriendlyName = existingFriendlyName; //restore existing name
                        break;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        void shortcutButton_Click(object sender, EventArgs e)
        {
            Shortcuts.TryCreate(_recordReference);
        }

        void tileContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Interactive) e.Cancel = true;
        }
    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            pBar.Invalidate();
        }


        public static void SetState(this ProgressBar pBar, ProgressBarState state)
        {
            pBar.SetState((int)state);
        }
    }
}
