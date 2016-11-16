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

        [Obsolete("Use other constructor instead",true)]
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

            if(dataStore != null)
            {
                diskChart.AssignData(dataStore, initialSort);

                try
                {
                    sortingCombo.Items.Clear();

                    foreach (SortingOption option in Enum.GetValues(typeof(SortingOption)))
                    {
                        sortingCombo.Items.Add(option.GetDescription());
                    }

                    sortingCombo.SelectedIndex = (int) initialSort;
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

            diskChart.SetChartOptions(m,sorting);
        }


        private void displayModeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateChart();
        }

        private void sortingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateChart();
        }
    }
}
