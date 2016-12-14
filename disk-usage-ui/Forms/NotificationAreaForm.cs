using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using disk_usage;
using disk_usage_ui.UserControls;
using UISettings = disk_usage_ui.Properties.Settings;

namespace disk_usage_ui.Forms
{
    public partial class NotificationAreaForm : Form
    {
        const int PaddingBottom = 10;
        const int PaddingRight = 23;

        const int BalloonTimeoutDefault = 5000; //five seconds
        const int NotifyDuration = 15000;

        const int FormWidth = 303;
        const int MaxItemListCount = 7;

        readonly DiskUsage _core;

        public NotificationAreaForm()
        {
            InitializeComponent();
            PositionForm();
            _core = new DiskUsage();

            HideForm(_core.SettingsFileWasGenerated);

            orderByCombo.AddEnumDescriptionItems(new SortingOption(), ComboIndex);

            //do an update now
            _core.RequestUpdateFromAll();
            NotificationTime = DateTime.Now.AddSeconds(10);
            notificationTimer.Enabled = true;
        }


        bool _allowshowdisplay;

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(_allowshowdisplay ? value : _allowshowdisplay);
        }
        void ToggleForm()
        {
            if (Visible)
            {
                HideForm();
            }
            else
            {
                ShowForm();
            }
        }

        bool HasOneOrMorePath => _core.Paths.Count > 0;

        void ShowForm()
        {
            if (!HasOneOrMorePath)
            {
                AddNewPath(this, new EventArgs());
            }
            else
            {
                RebuildUserInterface();
            }
            
            _allowshowdisplay = true;
            Visible = true;
          
            SetAvailabilityOfChartMenuItems();
            _core.RequestUpdateFromAll();

        }
        
        void ClearStackControls()
        {
            if (diskStack.Controls.Count > 0)
            {
                for (var index = diskStack.Controls.Count - 1; index >= 0; index--)
                {
                    var c = (DiskTile)diskStack.Controls[index];
                    c.UnsubscribeToEvents();
                    c.RemoveRequested -= RemovePathUsingTileObject;
                    c.AddNewPath -= AddNewPath;
                    c.PropertiesChanged -= TilePropertiesChanged;
                    diskStack.Controls.Remove(c);
                    c.Dispose();
                }
            }
                        

        }

        static int ComboIndex
        {
            get
            {
                return UISettings.Default.ComboIndex;
            }
            set
            {
                UISettings.Default.ComboIndex = value;
                UISettings.Default.Save();
            }
        }

        static bool HideInaccessablePaths
        {
            get
            {
                return UISettings.Default.HideInaccessablePaths;
            }
            set
            {
                UISettings.Default.HideInaccessablePaths = value;
                UISettings.Default.Save();
            }
        }


        void RebuildUserInterface()
        {
            SuspendLayout();

            ClearStackControls();

            var sortedCollection = _core.Sorted(SelectedSorting);

            var pathRecords = sortedCollection as IList<PathRecord> ?? sortedCollection.ToList();

            var collectionCount = (HideInaccessablePaths) 
                ? pathRecords.Count(pr => pr.Capacity.Bytes > 0) 
                : pathRecords.Count;

            int diskCount = (collectionCount < MaxItemListCount) ? collectionCount : MaxItemListCount;

            int rowOneHeight = tableLayout.GetRowHeights()[1] + tableLayout.GetRowHeights()[2];

            int borderWidth = (Width - ClientSize.Width) / 2;
            int titlebarHeight = Height - ClientSize.Height - (2 * borderWidth);

            int height = (DiskTile.HeightPixels * diskCount) + rowOneHeight + (2* borderWidth) + titlebarHeight + diskStack.Padding.Top + diskStack.Padding.Bottom;

            SetFixedFormSize(FormWidth + (2* borderWidth), height);

            PositionForm();

            foreach (var pc in pathRecords)
            {
                var newTile = new DiskTile(pc) { Padding = new Padding(0) };

                //subscribe to the events
                newTile.RemoveRequested += RemovePathUsingTileObject;
                newTile.AddNewPath += AddNewPath;
                newTile.PropertiesChanged += TilePropertiesChanged;

                diskStack.Controls.Add(newTile);
            }

            ResumeLayout(true);
        }

        void TilePropertiesChanged(object sender, EventArgs e)
        {
            SaveChanges();
        }

        public void SetFixedFormSize(int width, int height)
        {
            var newSize = new Size(width, height);
            MinimumSize = newSize;
            MaximumSize = newSize;
            Size = newSize;
        }

        public void HideForm(bool notify = false)
        {
            Hide();
            Visible = false;
            _allowshowdisplay = false;

            if (notify)
            {
                taskbarIcon.ShowBalloonTip(BalloonTimeoutDefault,"Disk Usage", "Click the taskbar icon to see disk usage information", ToolTipIcon.None);
            }
        }

        public void PositionForm()
        {
            var workingArea = Screen.GetWorkingArea(this);

            Location = new Point(workingArea.Right - Size.Width - PaddingRight,
                workingArea.Bottom - Size.Height - PaddingBottom);

        }

        void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void FormDeactivate(object sender, EventArgs e)
        {
            _allowshowdisplay = false;
            Visible = false;
        }

        void taskbarIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var mouseEvent = (MouseEventArgs)e;

                if (mouseEvent.Button == MouseButtons.Left)
                {
                    ToggleForm();
                }
            }
            catch
            {
                throw;
            }
        }

        void NotificationAreaForm_Resize(object sender, EventArgs e)
        {
            PositionForm();
        }

        void AddNewPath(object sender, EventArgs e)
        {
            var dialog = new AddPathDialog {InitialPath = Windows.InstallDirectory};
            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var addPathOperation = _core.AddPathToList(dialog.NewComputer);

                if (!addPathOperation.Success)
                {
                    MessageBox.Show(addPathOperation.Message, "Unable to add path");
                }
                RebuildUserInterface();
                SaveChanges();
            }
        }

        void RemovePathUsingTileObject(object sender, EventArgs e)
        {
            try
            {
                var tile = (DiskTile)sender;

                var dr = MessageBox.Show($"Are you sure you would like to remove \"{tile.Path}\"?","Remove Path",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                Debug.Print(tile.Path);

                if (dr == DialogResult.Yes)
                {
                    _core.RemovePathFromList(tile.Path);

                    tile.Visible = false;
                    SaveChanges();
                    RebuildUserInterface();
                }
 
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
           
        }


        void SaveChanges()
        {
            var saveOperatation = _core.SaveSettingsFile();

            if (!saveOperatation.Success)
            {
                MessageBox.Show(saveOperatation.Message, @"Could not save file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        void editJsonButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(_core.SettingsFileLocation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        void orderByCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboIndex = orderByCombo.SelectedIndex;
            }
            catch (Exception)
            {

                throw;
            }
            RebuildUserInterface();
        }

        public SortingOption SelectedSorting
        {
            get
            {
                try
                {
                    return (SortingOption)orderByCombo.SelectedIndex;
                }
                catch
                {
                    return 0;
                }
                
            }
        }

        void viewChartButton_Click(object sender, EventArgs e)
        {
            SpawnChart();
        }

        void SpawnChart()
        {
            var chartDialog = new ChartDialogForm(_core, SelectedSorting); //(core.Sorted(SelectedSorting));
            chartDialog.Show();
        }
        
        void SetAvailabilityOfChartMenuItems()
        {
            try
            {
                viewChartButton.Enabled = HasOneOrMorePath;
                settingsButton.Enabled = HasOneOrMorePath;
            }
            catch (Exception)
            {
                viewChartButton.Enabled = false;
                settingsButton.Enabled = false;
            }
        }

        void taskbarContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetAvailabilityOfChartMenuItems();

            notificationsMI.Image = UISettings.Default.Notifications 
                ? Properties.Resources.ic_notifications_black_18dp 
                : Properties.Resources.ic_notifications_off_black_18dp;

            hideInaccessableItem.Checked = HideInaccessablePaths;
        }

        void aboutButton_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        void settingsMainButton_Click(object sender, EventArgs e)
        {
            mainContextMenu.Show(settingsButton, Cursor.Position);
        }

        void hideInaccessableItem_Click(object sender, EventArgs e)
        {
            HideInaccessablePaths = !HideInaccessablePaths; //toggle
            hideInaccessableItem.Checked = HideInaccessablePaths;
            RebuildUserInterface();

        }

        void notificationTimer_Tick(object sender, EventArgs e)
        {
            if (UISettings.Default.Notifications) ProcessNotifications();
        }

        
        DateTime NotificationTime { get; set; }

        public DateTime GetNextNotification()
        {
            var result = DateTime.Now.AddMinutes(UISettings.Default.Frequency);

            Console.WriteLine($@"Next notification after {result.ToShortTimeString()}");

            return result;
        }

        void ProcessNotifications()
        {
            if (DateTime.Compare(DateTime.Now,NotificationTime) >= 0) //is it the right time?
            {
                Console.WriteLine(@"Can notify if required!");

                int count = 0;

                var notificationTargets = new List<PathRecord>();

                foreach (var path in _core.Paths)
                {
                    if (path.HasLowDiskSpace && path.ShouldNotify)
                    {
                        count++;
                        notificationTargets.Add(path);
                    }
                }

                if (count > 0)
                {
                    DoNotify(notificationTargets);
                }
            }
            else
            {
                //Console.WriteLine( $"Blocked, wating for {NotificationTime.ToShortTimeString()}.");
            }
        }

        void DoNotify(List<PathRecord> notificationTargets)
        {
            NotificationTime = GetNextNotification();
            taskbarIcon.ShowBalloonTip(NotifyDuration, "Low Disk Space", $"You are running out of disk space on {PathListDescription(notificationTargets)}. Click here to see details.", ToolTipIcon.Info);
        }

        static string PathListDescription(IReadOnlyList<PathRecord> paths)
        {
            const int maxNamedPaths = 3;
            
            if (paths == null || paths.Count == 0)
            {
                return "null";
            }

            var result = new System.Text.StringBuilder();
            
            if (paths.Count > maxNamedPaths)
            {
                int others = paths.Count - maxNamedPaths;
                string kwd = (others > 1) ? "disks" : "disk";

                for (int i = 0; i < maxNamedPaths; i++)
                {
                    result.Append(paths[i].FriendlyName);
                    result.Append(i < maxNamedPaths - 1 ? ", " : "");
                }
                result.Append($" and {others} other {kwd}");

                return result.ToString();
            }

            int joins = paths.Count - 1;

            for (int i = 0; i < paths.Count; i++)
            {
                result.Append(paths[i].FriendlyName);
                if (i == joins) break;
                result.Append(i < joins - 1 ? ", " : " and ");
            }            
            return result.ToString();
        }


        void taskbarIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        void SetNotificationFrequency(int min)
        {
            UISettings.Default.Notifications = (min > 0);
            UISettings.Default.Frequency = min;
            UISettings.Default.Save();
            
            NotificationTime = DateTime.Now; //do a notification now
            if (UISettings.Default.Notifications) ProcessNotifications();
        }

        void offMI_Click(object sender, EventArgs e)
        {
            SetNotificationFrequency(0);
        }

        void fiveminMI_Click(object sender, EventArgs e)
        {
            SetNotificationFrequency(5);
        }

        void thirtyMI_Click(object sender, EventArgs e)
        {
            SetNotificationFrequency(30);
        }

        void onehourMI_Click(object sender, EventArgs e)
        {
            SetNotificationFrequency(60);
        }

        void fourhourMI_Click(object sender, EventArgs e)
        {
            SetNotificationFrequency(60 * 4);
        }

        void notificationsMI_DropDownOpened(object sender, EventArgs e)
        {
            int freq = UISettings.Default.Frequency;

            offMI.Checked = (freq == 0);
            fiveminMI.Checked = (freq == 5);
            thirtyMI.Checked = (freq == 30);
            onehourMI.Checked = (freq == 60);
            fourhourMI.Checked = (freq == (60 * 4));
        }

        void openTSMI_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        void exitTBMI_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


}
