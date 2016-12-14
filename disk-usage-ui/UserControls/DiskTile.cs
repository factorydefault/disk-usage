using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using disk_usage;
using disk_usage_ui.Properties;
using ProgressBarState = disk_usage.ProgressBarState;

namespace disk_usage_ui.UserControls
{
    public partial class DiskTile : UserControl
    {
        public static int HeightPixels => 64;


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

        public string Path = "";

        public DiskTile()
        {
            InitializeComponent();

            if (Program.Theme != null)
            {
                pictureBox.Image = Program.Theme.NetworkDiskImage;
            }
        }

        public bool Interactive { get; set; } = true;

        public bool ShowPathOnHover { get; set; } = false;

        public void SetAsNotFound(string overrideLabel = "")
        {
            if(string.IsNullOrWhiteSpace(overrideLabel))
            {
                nameLabel.Text = !string.IsNullOrWhiteSpace(_recordReference?.FriendlyName) ? $"{_recordReference.FriendlyName}" : "Specify a Path";
            }
            else
            {
                nameLabel.Text = overrideLabel;
            }

            pictureBox.Image = Program.Theme.NotFoundImage;

            //usageBar.Visible = false;     
            usageBar.Minimum = 0;
            usageBar.Maximum = 100;
            usageBar.Value = 100;
            //Console.WriteLine($"Setting progress bar not found, value {usageBar.Value}");
            usageBar.SetState(ProgressBarState.Error);

            PathLocation location = _recordReference?.Location() ?? PathLocation.Unknown;

            notificationPicture.Visible = false;

            switch (location)
            {
                case PathLocation.Local:
                case PathLocation.Os:
                    detailLabel.Text = "Path not found";
                    break;
                case PathLocation.Remote:
                    detailLabel.Text = "Path not available";
                    break;
                default:
                    detailLabel.Text = "";
                    break;
            }

            Visible &= (!Settings.Default.HideInaccessablePaths || !Interactive);

        }

        public void UpdateUserInterface(PathRecord pathRecord)
        {
            _recordReference = pathRecord;
            UpdateUserInterface();
        }

        public void UpdateUserInterface() //disk_usage.PathRecord pathRecord)
        {
            Visible = true;

            PathRecord pathRecord = _recordReference;

            nameLabel.Text = $@"{pathRecord.FriendlyName}";

            Path = pathRecord.Path;

            pictureBox.Visible = true;
            notificationPicture.Visible = false;

            UpdateNotificationPicture(pathRecord);

            switch (pathRecord.Location())
            {
                case PathLocation.Local:
                    pictureBox.Image = Program.Theme.LocalDiskImage;
                    break;
                case PathLocation.Os:
                    pictureBox.Image = Program.Theme.OsDiskImage;
                    break;
                default:
                    pictureBox.Image = Program.Theme.NetworkDiskImage;
                    break;
            }

            usageBar.Minimum = 0;
            usageBar.Maximum = 100;

            bool highlight = pathRecord.Highlight;

            BackColor = highlight ? SystemColors.MenuHighlight : Color.White;
            nameLabel.ForeColor = highlight ? SystemColors.HighlightText : SystemColors.ControlText;
            detailLabel.ForeColor = highlight ? SystemColors.HighlightText : Color.FromArgb(117, 117, 117);

            BorderStyle = highlight ? BorderStyle.FixedSingle : BorderStyle.None;

            //Console.WriteLine($"Setting progress bar for {path} to {pathRecord.FillLevel}");

            usageBar.Value = pathRecord.FillLevel;

            detailLabel.Text = string.Format(Resources.DiskTile_UpdateUserInterface_0_free_of_1, pathRecord.FreeSpace.ExplorerLabel(), pathRecord.Capacity.ExplorerLabel());

            if (pathRecord.HasZeroCapacity) //edge case where path has not been found
            {
                SetAsNotFound();
            }
            else
            {
                usageBar.SetState(pathRecord.HasLowDiskSpace ? ProgressBarState.Error : ProgressBarState.Normal);
            }
        }

        void UpdateNotificationPicture(PathRecord pathRecord)
        {
            notificationPicture.Image = pathRecord.Notifications 
                ? Properties.Resources.ic_notifications_black_18dp 
                : Properties.Resources.ic_notifications_off_black_18dp;
        }

        PathRecord _recordReference;

        public DiskTile(PathRecord pr) : this()
        {
            _recordReference = pr;
            _recordReference.DiskInfoUpdated += Pr_DiskInfoUpdated;

            UpdateUserInterface(); 
        }

        void Pr_DiskInfoUpdated(object sender, EventArgs e)
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
            Tools.OpenDirectory(Path);
        }

        void clipboardButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(Path);
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
                        _recordReference.Notifications = propertiesForm.ShouldUseNotifications;
                        _recordReference.Highlight = propertiesForm.ShouldHighlight;

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
            e.Cancel |= !Interactive;

            openFolderButton.Text = _recordReference != null ? $"{_recordReference.Path.Ellipsis(20)} ({_recordReference.FillLevel:##0}%)" : "&Open";

            SetupNotificationContextItem();

        }

        private void SetupNotificationContextItem()
        {
            if (_recordReference.Notifications)
            {
                notifyMI.Image = Resources.ic_notifications_black_18dp;
                notifyMI.Text = @"Notifications: Enabled";
            }
            else
            {
                notifyMI.Image = Resources.ic_notifications_off_black_18dp;
                notifyMI.Text = @"Notifications: Disabled";
            }
        }

        void nameLabel_MouseEnter(object sender, EventArgs e)
        {
            if (!ShowPathOnHover) return;

            NameLabelHoverStore = nameLabel.Text;
            System.Diagnostics.Debug.Print($"saving: {NameLabelHoverStore}");

            NameLabelHoverText = $"{Path}";
            nameLabel.Text = NameLabelHoverText;
        }

        void nameLabel_MouseLeave(object sender, EventArgs e)
        {
            //if text hasn't been changed in the meantime
            if (!ShowPathOnHover || nameLabel.Text != NameLabelHoverText) return;

            System.Diagnostics.Debug.Print($"restoring: {NameLabelHoverStore}");
            nameLabel.Text = NameLabelHoverStore;
        }


        string NameLabelHoverStore { get; set; }
        string NameLabelHoverText { get; set; }

        void notificationPicture_Click(object sender, EventArgs e)
        {
            
        }

        void FlipNotifications()
        {
            _recordReference.Notifications = !_recordReference.Notifications;
            UpdateNotificationPicture(_recordReference);
            PropertiesChanged?.Invoke(this, new EventArgs());
        }

        void notificationPicture_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void notifyMI_Click(object sender, EventArgs e)
        {
            FlipNotifications();
            SetupNotificationContextItem();
        }
    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr w, IntPtr l);
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
