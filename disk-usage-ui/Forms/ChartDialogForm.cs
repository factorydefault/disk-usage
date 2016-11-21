using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageFormat = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat;
using disk_usage;

namespace disk_usage_ui.Forms
{
    public partial class ChartDialogForm : Form
    {
        public ChartDialogForm()
        {
            InitializeComponent();
        }

        [Obsolete("Use other constructor instead", true)]
        public ChartDialogForm(IEnumerable<disk_usage.PathRecord> collection)
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

                try
                {
                    sortingCombo.Items.Clear();

                    foreach (SortingOption option in Enum.GetValues(typeof(SortingOption)))
                    {
                        sortingCombo.Items.Add(option.GetDescription());
                    }

                    sortingCombo.SelectedIndex = (int)initialSort;
                }
                catch (Exception)
                {
                    sortingCombo.SelectedIndex = 0;
                }

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
                MessageBox.Show($"Saved {saveFileDialog.FileName}");
            }
        }

        void updateChart()
        {
            UserControls.ChartDisplayMode m = (UserControls.ChartDisplayMode)displayModeCombo.SelectedIndex;
            SortingOption sorting = (SortingOption)sortingCombo.SelectedIndex;

            diskChart.SetChartOptions(m, sorting);
        }


        private void displayModeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateChart();
        }

        private void sortingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateChart();
        }

        ToolStripMenuItem showAllItem()
        {
            var showAllMI = new ToolStripMenuItem
            {
                Name = "showAll",
                Text = "Show All"
            };
            showAllMI.Click += showAllClicked;
            return showAllMI;
        }

        ToolStripMenuItem hideAllItem()
        {
            var hideAllMI = new ToolStripMenuItem
            {
                Name = "hideAll",
                Text = "Hide All"
            };
            hideAllMI.Click += hideAllClicked;
            return hideAllMI;
        }

        ToolStripMenuItem filterMenu()
        {
            var filterMI = new ToolStripMenuItem
            {
                Name = "filterMenu",
                Text = "Filters",
                Image = Properties.Resources.filter_16xLG
            };

            var lowDiskSpace = new ToolStripMenuItem("With low disk space (>90% fill)");
            lowDiskSpace.Click += LowDiskSpace_Click;
            filterMI.DropDownItems.Add(lowDiskSpace);

            var lowGBFree = new ToolStripMenuItem("Less than 200 GB remaining");
            lowGBFree.Click += LowGBFree_Click;
            filterMI.DropDownItems.Add(lowGBFree);

            var localdisks = new ToolStripMenuItem("Local / Mapped disks");
            localdisks.Click += Localdisks_Click;
            filterMI.DropDownItems.Add(localdisks);

            var networkdisks = new ToolStripMenuItem("Network Shares");
            networkdisks.Click += Networkdisks_Click;
            filterMI.DropDownItems.Add(networkdisks);

            return filterMI;
        }

        void Networkdisks_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                data.ShowOnChart = data.Location() == PathLocation.Remote;
            }
            updateChart();
        }

        void Localdisks_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                var location = data.Location();
                data.ShowOnChart = (location == PathLocation.Local || location == PathLocation.OS);
            }
            updateChart();
        }

        void LowGBFree_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                data.ShowOnChart = data.FreeSpace.GigaBytes < 200.0; 
            }
            updateChart();
        }

        void LowDiskSpace_Click(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (!data.ShowOnChart) continue;
                data.ShowOnChart = data.HasLowDiskSpace;
            }
            updateChart();
        }

        void seriesMI_DropDownOpening(object sender, EventArgs e)
        {
            seriesMI.DropDownItems.Clear();
            bool hideEmpty = Properties.Settings.Default.HideInaccessablePaths;

            var dropdown = seriesMI.DropDownItems;

            dropdown.Add(showAllItem());
            dropdown.Add(hideAllItem());
            dropdown.Add(filterMenu());
            var sep = new ToolStripSeparator();


            seriesMI.DropDownItems.Add(sep);
            
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if (hideEmpty && data.Capacity.Bytes < 1) continue;

                var item = new ToolStripMenuItem
                {
                    Name = data.ShortcutName,
                    Checked = data.ShowOnChart,
                    Tag = data.Path,
                    Text = data.FriendlyName,
                };
                item.Click += seriesMIOptionClicked;
                dropdown.Add(item);
            }
        }

        void showAllClicked(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            { 
                data.ShowOnChart = true;
            }
            updateChart();
        }

        void hideAllClicked(object sender, EventArgs e)
        {
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                    data.ShowOnChart = false;
            }
            updateChart();
        }

        void seriesMIOptionClicked(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            foreach (var data in diskChart.PathsInSortedOrder)
            {
                if ((string) clickedItem.Tag == data.Path)
                {
                    data.ShowOnChart = !data.ShowOnChart;
                    break;
                }
            }

            updateChart();

        }
    }
}
