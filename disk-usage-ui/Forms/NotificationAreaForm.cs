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

        const int FORM_WIDTH = 260;
        const int MAX_ITEM_HEIGHT = 7;

        DiskUsage core;

        public NotificationAreaForm()
        {
            InitializeComponent();
            PositionForm();
            core = new DiskUsage();

            HideForm(core.SettingsFileWasGenerated);

            try
            {
                orderByCombo.Items.Clear();
                                
                foreach (SortingOption option in Enum.GetValues(typeof(SortingOption)))
                {
                    orderByCombo.Items.Add(option.GetDescription());
                }

                int index = UISettings.Default.ComboIndex;

                orderByCombo.SelectedIndex = index;
            }
            catch (Exception)
            {
                orderByCombo.SelectedIndex = 0;
            }

            //do an update now
            core.RequestUpdateFromAll();
            NotificationTime = DateTime.Now.AddSeconds(10);
            notificationTimer.Enabled = true;
        }


        bool allowshowdisplay = false;

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

        void ShowForm()
        {
            if (core.Paths.Count == 0)
            {
                AddNewPath(this, new EventArgs());
            }
            else
            {
                //debugprint();
                RebuildUserInterface();
                allowshowdisplay = true;
                Visible = true;
            }
            setAvailabilityOfChartMenuItems();
            core.RequestUpdateFromAll();

        }

        void debugprint()
        {
            foreach(var path in core.Paths)
            {
                Debug.Print($"{path.FriendlyName}: {path.PercentageFilled}");
            }
        }


        void ClearStackControls()
        {
            if (diskStack.Controls.Count > 0)
            {
                for (int index = diskStack.Controls.Count - 1; index >= 0; index--)
                {
                    DiskTile c = (DiskTile)diskStack.Controls[index];
                    c.UnsubscribeToEvents();
                    c.RemoveRequested -= RemovePathUsingTileObject;
                    c.AddNewPath -= AddNewPath;
                    c.PropertiesChanged -= TilePropertiesChanged;
                    diskStack.Controls.Remove(c);
                    c.Dispose();
                }
            }
                        

        }


        void RebuildUserInterface()
        {
            SuspendLayout();

            ClearStackControls();

            List<PathRecord> sortedCollection = core.SortedList(SelectedSorting);

            int collectionCount = (UISettings.Default.HideInaccessablePaths) ? sortedCollection.Count(pr => pr.Capacity.Bytes > 0) : sortedCollection.Count;

            int diskCount = (collectionCount < MAX_ITEM_HEIGHT) ? collectionCount : MAX_ITEM_HEIGHT;

            int rowOneHeight = tableLayout.GetRowHeights()[1];

            int BorderWidth = (Width - ClientSize.Width) / 2;
            int TitlebarHeight = Height - ClientSize.Height - (2 * BorderWidth);

            int height = (DiskTile.HeightPixels * diskCount) + rowOneHeight + (2* BorderWidth) + TitlebarHeight + diskStack.Padding.Top + diskStack.Padding.Bottom;

            SetFixedFormSize(FORM_WIDTH + (2* BorderWidth), height);

            PositionForm();

            foreach (var pc in sortedCollection)
            {
                DiskTile newTile = new DiskTile(pc);

                newTile.Padding = new Padding(0);

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
            Rectangle workingArea = Screen.GetWorkingArea(this);

            Location = new Point(workingArea.Right - Size.Width - PADDING_RIGHT,
                workingArea.Bottom - Size.Height - PADDING_BOTTOM);

        }

        void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void openButton_Click(object sender, EventArgs e)
        {
            ShowForm();
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
                MouseEventArgs mouseEvent = (MouseEventArgs)e;

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

        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        void AddNewPath(object sender, EventArgs e)
        {
            AddPathDialog dialog = new AddPathDialog();
            dialog.InitialPath = Windows.InstallDirectory;
            var result = dialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var or = core.AddPathToList(dialog.NewComputer);

                if (!or.Result)
                {
                    MessageBox.Show(or.Message, "Unable to add path");
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
            core.SaveSettingsFile();
        }


        private void editJsonButton_Click(object sender, EventArgs e)
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
                UISettings.Default.ComboIndex = orderByCombo.SelectedIndex;
                UISettings.Default.Save();
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
            Forms.ChartDialogForm chartDialog = new Forms.ChartDialogForm(core.SortedList(SelectedSorting));
            chartDialog.Show();
        }
        
        void setAvailabilityOfChartMenuItems()
        {
            try
            {
                bool availability = core.Paths.Count > 0;
                viewChartButton.Enabled = availability;
                chartButton.Enabled = availability;
            }
            catch (Exception)
            {
                viewChartButton.Enabled = false;
                chartButton.Enabled = false;
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


            hideInaccessableItem.Checked = UISettings.Default.HideInaccessablePaths;
        }

        void aboutButton_Click(object sender, EventArgs e)
        {
            Forms.AboutForm about = new Forms.AboutForm();
            about.ShowDialog();
        }

        void chartButton_Click(object sender, EventArgs e)
        {
            spawnChart();
        }

        void hideInaccessableItem_Click(object sender, EventArgs e)
        {
            UISettings.Default.HideInaccessablePaths = !UISettings.Default.HideInaccessablePaths; //toggle
            hideInaccessableItem.Checked = UISettings.Default.HideInaccessablePaths;
            UISettings.Default.Save();

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
            //is it the right time?

            if (DateTime.Compare(DateTime.Now,NotificationTime) >= 0)
            {
                Console.WriteLine("Can notify if required!");


                int count = 0;

                List<PathRecord> notificationTargets = new List<PathRecord>();

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

            System.Text.StringBuilder result = new System.Text.StringBuilder();
            

            if (paths.Count > MAX_NAMED_PATHS)
            {
                int others = paths.Count - MAX_NAMED_PATHS;
                string kwd = (others > 1) ? "disks" : "disk";

                for (int i = 0; i < MAX_NAMED_PATHS; i++)
                {
                    result.Append(paths[i].FriendlyName);
                    result.Append(i < MAX_NAMED_PATHS - 1 ? ", " : "");
                }
                result.Append($" and { others} other {kwd}");

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


    }
}
