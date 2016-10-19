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

        public ChartDialogForm(List<disk_usage.Computer> collection)
        {
            InitializeComponent();

            diskChart.SetData(collection);
        }

        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dr = saveFileDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                diskChart.SaveImage(saveFileDialog.FileName, ImageFormat.Png);
                MessageBox.Show($"Saved {saveFileDialog.FileName}");
            }
        }
    }
}
