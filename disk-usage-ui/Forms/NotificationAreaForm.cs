using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using disk_usage;
using System.Collections.Generic;
using System.Linq;
using UISettings = disk_usage_ui.Properties.Settings;

namespace disk_usage_ui
{
    public partial class NotificationAreaForm : Form
    {
        const int PADDING_BOTTOM = 10;
        const int PADDING_RIGHT = 23;
        const int BALLOON_TIMEOUT_DEFAULT = 4000;

        DiskUsage core;


        public NotificationAreaForm()
        {
            InitializeComponent();
            PositionForm();
            core = new DiskUsage();

            HideForm(core.SettingsFileWasGenerated);

            try
            {
                int index = UISettings.Default.ComboIndex;

                orderByCombo.SelectedIndex = index;
            }
            catch (Exception)
            {
                orderByCombo.SelectedIndex = 0;
            }

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
            if (core.Collection.PCs.Count == 0)
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

        }

        void debugprint()
        {
            foreach(var pc in core.Collection.PCs)
            {
                Debug.Print($"{pc.FriendlyName}: {pc.PercentageFilled}");
            }
        }


        List<Computer> SortCollection(List<Computer> input)
        {
            switch (orderByCombo.SelectedIndex)
            {
                case 0:
                    return input.OrderBy(o => o.FriendlyName).ToList();
                case 1:
                    return input.OrderByDescending(o => o.FriendlyName).ToList();
                case 2:
                    return input.OrderBy(o => o.FreeSpace).ToList();
                case 3:
                    return input.OrderByDescending(o => o.FreeSpace).ToList();
                case 4:
                    return input.OrderBy(o => o.FillLevel).ToList();
                case 5:
                    return input.OrderByDescending(o => o.FillLevel).ToList();
                case 6:
                    return input.OrderBy(o => o.TotalSpace).ToList();
                case 7:
                    return input.OrderByDescending(o => o.TotalSpace).ToList();
                default:
                    Debug.Print("ComboBox index not recognised");
                    return input; // input.OrderByDescending(o => o.FillLevel).ToList();
            }

        }

        void RebuildUserInterface()
        {
            diskStack.Controls.Clear();

            List<Computer> sortedCollection = SortCollection(core.Collection.PCs);

            SuspendLayout();            

            foreach (var pc in sortedCollection)
            {
                DiskTile newTile = new DiskTile(pc);

                //subscribe to the events
                newTile.RemoveRequested += RemovePathUsingTileObject;
                newTile.AddNewPath += AddNewPath;

                diskStack.Controls.Add(newTile);
            }

            ResumeLayout(true);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        void AddNewPath(object sender, EventArgs e)
        {
            AddPathDialog dialog = new AddPathDialog();
            dialog.InitialPath = core.WindowsInstallDirectory;
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                core.Collection.AddComputer(dialog.NewComputer);
                RebuildUserInterface();
                saveChanges();
            }


        }

        private void RemovePathUsingTileObject(object sender, EventArgs e)
        {
            try
            {
                var tile = (DiskTile)sender;

                var dr = MessageBox.Show($"Are you sure you would like to remove \"{tile.path}\"?","Remove Path",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                Debug.Print(tile.path);

                if (dr == DialogResult.Yes)
                {
                    core.Collection.RemoveComputerWithPath(tile.path);

                    tile.Visible = false;
                    saveChanges();
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

        private void viewChartButton_Click(object sender, EventArgs e)
        {
            Forms.ChartDialogForm chartDialog = new Forms.ChartDialogForm(SortCollection(core.Collection.PCs));
            chartDialog.Show();
        }

        private void taskbarContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                viewChartButton.Enabled = core.Collection.PCs.Count > 0;
            }
            catch (Exception)
            {
                viewChartButton.Enabled = false;
            }
            
        }
    }
}
