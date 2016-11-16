using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ImageFormat = System.Windows.Forms.DataVisualization.Charting.ChartImageFormat;

namespace disk_usage_ui.Forms
{
    public partial class ChartDialogForm : Form
    {
        public ChartDialogForm()
        {
            InitializeComponent();
        }

        public ChartDialogForm(IEnumerable<disk_usage.PathRecord> collection)
        {
            InitializeComponent();

            if (collection != null)
            {
                diskChart.SetData(collection);
            }    
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
    }
}
