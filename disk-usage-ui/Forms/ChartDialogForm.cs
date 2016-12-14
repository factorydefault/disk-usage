using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageFormat = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat;
using disk_usage;
using disk_usage_ui.Properties;

namespace disk_usage_ui.Forms
{
    public partial class ChartDialogForm : Form
    {
        public ChartDialogForm()
        {
            InitializeComponent();
        }

        [Obsolete("Use other constructor instead", true)]
        public ChartDialogForm(IEnumerable<PathRecord> collection)
        {
            InitializeComponent();

            if (collection != null)
            {
                //diskChart.SetData(collection);
            }
        }

        public ChartDialogForm(DiskUsage dataStore, SortingOption initialSort)
        {
            InitializeComponent();

            if (dataStore != null)
            {
                diskChart.AssignData(dataStore, initialSort);

                sortingCombo.AddEnumDescriptionItems(new SortingOption(), (int)initialSort);
            }

            displayModeCombo.SelectedIndex = 0;

        }

        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(saveFileDialog.InitialDirectory))
            {
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
            saveFileDialog.FileName = $"DiskUsage {Tools.Timestamp()}.png";

            var dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                diskChart.SaveImage(saveFileDialog.FileName, ImageFormat.Png);
                MessageBox.Show(string.Format(Resources.ChartDialogForm_SavedPathMsg, saveFileDialog.FileName));
            }
        }

        void UpdateChart()
        {
            var m = (UserControls.ChartDisplayMode)displayModeCombo.SelectedIndex;
            var sorting = (SortingOption)sortingCombo.SelectedIndex;

            diskChart.SetChartOptions(m, sorting);
        }


        private void displayModeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void sortingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }

        ToolStripMenuItem ShowAllItem()
        {
            var showAllMi = new ToolStripMenuItem
            {
                Name = "showAll",
                Text = @"Show All"
            };
            showAllMi.Click += ShowAllClicked;
            return showAllMi;
        }

        ToolStripMenuItem HideAllItem()
        {
            var hideAllMi = new ToolStripMenuItem
            {
                Name = "hideAll",
                Text = @"Hide All"
            };
            hideAllMi.Click += HideAllClicked;
            return hideAllMi;
        }

        ToolStripMenuItem FilterMenu()
        {
            var filterMi = new ToolStripMenuItem
            {
                Name = "filterMenu",
                Text = @"Filters",
                Image = Resources.filter_16xLG
            };

            var lowDiskSpace = new ToolStripMenuItem("With low disk space (>90% fill)");
            lowDiskSpace.Click += LowDiskSpace_Click;
            filterMi.DropDownItems.Add(lowDiskSpace);

            var lowGbFree = new ToolStripMenuItem("Less than 200 GB remaining");
            lowGbFree.Click += LowGBFree_Click;
            filterMi.DropDownItems.Add(lowGbFree);

            var localdisks = new ToolStripMenuItem("Local / Mapped disks");
            localdisks.Click += Localdisks_Click;
            filterMi.DropDownItems.Add(localdisks);

            var networkdisks = new ToolStripMenuItem("Network Shares");
            networkdisks.Click += Networkdisks_Click;
            filterMi.DropDownItems.Add(networkdisks);

            return filterMi;
        }

        void Networkdisks_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                data.ShowOnChart = data.Location() == PathLocation.Remote;
            }
            UpdateChart();
        }

        void Localdisks_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                var location = data.Location();
                data.ShowOnChart = (location == PathLocation.Local || location == PathLocation.Os);
            }
            UpdateChart();
        }

        void LowGBFree_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                data.ShowOnChart = data.FreeSpace.GigaBytes < 200.0; 
            }
            UpdateChart();
        }

        void LowDiskSpace_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                data.ShowOnChart = data.HasLowDiskSpace;
            }
            UpdateChart();
        }

        void seriesMI_DropDownOpening(object sender, EventArgs e)
        {
            seriesMI.DropDownItems.Clear();
            var hideEmpty = Settings.Default.HideInaccessablePaths;

            var dropdown = seriesMI.DropDownItems;

            dropdown.Add(ShowAllItem());
            dropdown.Add(HideAllItem());
            dropdown.Add(FilterMenu());
            var sep = new ToolStripSeparator();


            seriesMI.DropDownItems.Add(sep);
            
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (hideEmpty && data.HasZeroCapacity) continue;

                var item = new ToolStripMenuItem
                {
                    Name = data.ShortcutName,
                    Checked = data.ShowOnChart,
                    Tag = data.Path,
                    Text = data.FriendlyName
                };
                item.Click += SeriesMiOptionClicked;
                dropdown.Add(item);
            }
        }

        void ShowAllClicked(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            { 
                data.ShowOnChart = true;
            }
            UpdateChart();
        }

        void HideAllClicked(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                    data.ShowOnChart = false;
            }
            UpdateChart();
        }

        void SeriesMiOptionClicked(object sender, EventArgs e)
        {
            var clickedItem = (ToolStripMenuItem)sender;
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if ((string) clickedItem.Tag == data.Path)
                {
                    data.ShowOnChart = !data.ShowOnChart;
                    break;
                }
            }

            UpdateChart();

        }

        void chartTypeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            horizontalBarsToolStripMenuItem.Checked =
                (diskChart.ChartOrientation == UserControls.ChartOrientation.Horizontal);
            
            verticalBarsToolStripMenuItem.Checked =
                 (diskChart.ChartOrientation == UserControls.ChartOrientation.Vertical);

        }

        void horizontalBarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diskChart.ChartOrientation = UserControls.ChartOrientation.Horizontal;
            UpdateChart();
        }

        void verticalBarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            diskChart.ChartOrientation = UserControls.ChartOrientation.Vertical;
            UpdateChart();
        }
    }
}
