using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using disk_usage;
using System.Collections.Generic;
using UISettings = disk_usage_ui.Properties.Settings;

namespace disk_usage_ui
{
    public partial class NotificationAreaForm : Form
    {
        const int PADDING_BOTTOM = 10;
        const int PADDING_RIGHT = 23;

        const int BALLOON_TIMEOUT_DEFAULT = 5000; //five seconds
        const int NOTIFY_DURATION = 15000;

        const int FORM_WIDTH = 303;
        const int MAX_ITEM_LIST_COUNT = 7;

        DiskUsage core;

        public NotificationAreaForm()
        {
            InitializeComponent();
            PositionForm();
            core = new DiskUsage();

            HideForm(core.SettingsFileWasGenerated);

            orderByCombo.AddEnumDescriptionItems(new SortingOption(), ComboIndex);

            //do an update now
            core.RequestUpdateFromAll();
            NotificationTime = DateTime.Now.AddSeconds(10);
            notificationTimer.Enabled = true;
        }


        bool allowshowdisplay;

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowshowdisplay ? value : allowshowdisplay);
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

        bool HasOneOrMorePath => core.Paths.Count > 0;

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
            
            allowshowdisplay = true;
            Visible = true;
          
            setAvailabilityOfChartMenuItems();
            core.RequestUpdateFromAll();

        }
        
        void ClearStackControls()
        {
            if (diskStack.Controls.Count > 0)
            {
                for (int index = diskStack.Controls.Count - 1; index >= 0; index--)
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

        int ComboIndex
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

        bool HideInaccessablePaths
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

            var sortedCollection = core.Sorted(SelectedSorting);

            int collectionCount = (HideInaccessablePaths) ? sortedCollection.Count(pr => pr.Capacity.Bytes > 0) : sortedCollection.Count();

            int diskCount = (collectionCount < MAX_ITEM_LIST_COUNT) ? collectionCount : MAX_ITEM_LIST_COUNT;

            int rowOneHeight = tableLayout.GetRowHeights()[1] + tableLayout.GetRowHeights()[2];

            int BorderWidth = (Width - ClientSize.Width) / 2;
            int TitlebarHeight = Height - ClientSize.Height - (2 * BorderWidth);

            int height = (DiskTile.HeightPixels * diskCount) + rowOneHeight + (2* BorderWidth) + TitlebarHeight + diskStack.Padding.Top + diskStack.Padding.Bottom;

            SetFixedFormSize(FORM_WIDTH + (2* BorderWidth), height);

            PositionForm();

            foreach (var pc in sortedCollection)
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
            saveChanges();
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
            allowshowdisplay = false;

            if (notify)
            {
                taskbarIcon.ShowBalloonTip(BALLOON_TIMEOUT_DEFAULT,"Disk Usage", "Click the taskbar icon to see disk usage information", ToolTipIcon.None);
            }
        }

        public void PositionForm()
        {
            var workingArea = Screen.GetWorkingArea(this);

            Location = new Point(workingArea.Right - Size.Width - PADDING_RIGHT,
                workingArea.Bottom - Size.Height - PADDING_BOTTOM);

        }

        void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void FormDeactivate(object sender, EventArgs e)
        {
            allowshowdisplay = false;
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
            var dialog = new AddPathDialog();
            dialog.InitialPath = Windows.InstallDirectory;
            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var addPathOperation = core.AddPathToList(dialog.NewComputer);

                if (!addPathOperation.Success)
                {
                    MessageBox.Show(addPathOperation.Message, "Unable to add path");
                }
                RebuildUserInterface();
                saveChanges();
            }
        }

        void RemovePathUsingTileObject(object sender, EventArgs e)
        {
            try
            {
                var tile = (DiskTile)sender;

                var dr = MessageBox.Show($"Are you sure you would like to remove \"{tile.path}\"?","Remove Path",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                Debug.Print(tile.path);

                if (dr == DialogResult.Yes)
                {
                    core.RemovePathFromList(tile.path);

                    tile.Visible = false;
                    saveChanges();
                    RebuildUserInterface();
                }
 
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
           
        }


        void saveChanges()
        {
            var saveOperatation = core.SaveSettingsFile();

            if (!saveOperatation.Success)
            {
                MessageBox.Show(saveOperatation.Message, "Could not save file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        void editJsonButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(core.SettingsFileLocation);
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
            spawnChart();
        }

        void spawnChart()
        {
            var chartDialog = new Forms.ChartDialogForm(core, SelectedSorting); //(core.Sorted(SelectedSorting));
            chartDialog.Show();
        }
        
        void setAvailabilityOfChartMenuItems()
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
            setAvailabilityOfChartMenuItems();

            if (UISettings.Default.Notifications)
            {
                notificationsMI.Image = Properties.Resources.ic_notifications_black_18dp;
            }
            else
            {
                notificationsMI.Image = Properties.Resources.ic_notifications_off_black_18dp;
            }

            hideInaccessableItem.Checked = HideInaccessablePaths;
        }

        void aboutButton_Click(object sender, EventArgs e)
        {
            var about = new Forms.AboutForm();
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

            Console.WriteLine($"Next notification after {result.ToShortTimeString()}");

            return result;
        }

        void ProcessNotifications()
        {
            if (DateTime.Compare(DateTime.Now,NotificationTime) >= 0) //is it the right time?
            {
                Console.WriteLine("Can notify if required!");

                int count = 0;

                var notificationTargets = new List<PathRecord>();

                foreach (var path in core.Paths)
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
            taskbarIcon.ShowBalloonTip(NOTIFY_DURATION, "Low Disk Space", $"You are running out of disk space on {pathListDescription(notificationTargets)}. Click here to see details.", ToolTipIcon.Info);
        }

        string pathListDescription(List<PathRecord> paths)
        {
            const int MAX_NAMED_PATHS = 3;

            if (paths == null || paths.Count == 0)
            {
                return "null";
            }

            var result = new System.Text.StringBuilder();
            
            if (paths.Count > MAX_NAMED_PATHS)
            {
                int others = paths.Count - MAX_NAMED_PATHS;
                string kwd = (others > 1) ? "disks" : "disk";

                for (int i = 0; i < MAX_NAMED_PATHS; i++)
                {
                    result.Append(paths[i].FriendlyName);
                    result.Append(i < MAX_NAMED_PATHS - 1 ? ", " : "");
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

        void setNotificationFrequency(int min)
        {
            UISettings.Default.Notifications = (min > 0);
            UISettings.Default.Frequency = min;
            UISettings.Default.Save();
            
            NotificationTime = DateTime.Now; //do a notification now
            if (UISettings.Default.Notifications) ProcessNotifications();
        }

        void offMI_Click(object sender, EventArgs e)
        {
            setNotificationFrequency(0);
        }

        void fiveminMI_Click(object sender, EventArgs e)
        {
            setNotificationFrequency(5);
        }

        void thirtyMI_Click(object sender, EventArgs e)
        {
            setNotificationFrequency(30);
        }

        void onehourMI_Click(object sender, EventArgs e)
        {
            setNotificationFrequency(60);
        }

        void fourhourMI_Click(object sender, EventArgs e)
        {
            setNotificationFrequency(60 * 4);
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
