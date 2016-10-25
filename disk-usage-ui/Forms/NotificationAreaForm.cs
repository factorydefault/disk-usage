using System;
using System.Drawing;
using System.Windows.Forms;
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
        const int BALLOON_TIMEOUT_DEFAULT = 4000;
        const int FORM_WIDTH = 275;
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
                debugprint();
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

            int diskCount = (sortedCollection.Count < MAX_ITEM_HEIGHT) ? sortedCollection.Count : MAX_ITEM_HEIGHT;

            int height = 64 * (diskCount +1); //extra 64px for the combo box area and padding etc

            SetFixedFormSize(FORM_WIDTH, height);

            PositionForm();

            foreach (var pc in sortedCollection)
            {
                DiskTile newTile = new DiskTile(pc);

                newTile.Padding = new Padding(0);

                //subscribe to the events
                newTile.RemoveRequested += RemovePathUsingTileObject;
                newTile.AddNewPath += AddNewPath;

                diskStack.Controls.Add(newTile);
            }

            ResumeLayout(true);
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
                taskbarIcon.ShowBalloonTip(BALLOON_TIMEOUT_DEFAULT);
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
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                core.AddPathToList(dialog.NewComputer);
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
    }
}
