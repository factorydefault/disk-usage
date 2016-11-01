using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace disk_usage_ui.UserControls
{
    public partial class DiskChart : UserControl
    {

        Color lowDiskSpaceColor => Color.Red;
        Color lowDiskSpaceColorSecondary => Color.FromArgb(255, 128, 128);

        public DiskChart()
        {
            InitializeComponent();

            if (Program.Theme != null)
            {
                Chart.Series["UsedSpace"].Color = Program.Theme.ChartUsedSpace;
                Chart.Series["UsedSpace"].BackSecondaryColor = Program.Theme.ChartUsedSpaceSecondary;
            }
        }

                
        public void SaveImage(string imageFileName,ChartImageFormat format)
        {
            Chart.SaveImage(imageFileName, format);
        }

        public void SetData(List<disk_usage.PathRecord> data)
        {
            var usedSeries = Chart.Series["UsedSpace"];
            var freeSeries = Chart.Series["FreeSpace"];

            usedSeries.Points.Clear();
            freeSeries.Points.Clear();

            //for correct ordering
            int index = data.Count;

            bool hideEmpty = Properties.Settings.Default.HideInaccessablePaths;

            foreach(var pc in data)
            {
                if (hideEmpty && pc.Capacity.Bytes < 1) continue;

                DataPoint usedPoint = new DataPoint();

                usedPoint.SetValueXY(index, pc.UsedSpace.GigaBytes);
                //usedPoint.SetValueY(pc.TotalSpace - pc.FreeSpace);
                usedPoint.AxisLabel = pc.FriendlyName;

                if (pc.HasLowDiskSpace)
                {
                    usedPoint.Color = lowDiskSpaceColor;
                    usedPoint.BackSecondaryColor = lowDiskSpaceColorSecondary;
                }

                usedSeries.Points.Add(usedPoint);

                DataPoint freePoint = new DataPoint();

                freePoint.SetValueXY(index, pc.FreeSpace.GigaBytes);
                //freePoint.SetValueY(pc.FreeSpace);
                freePoint.AxisLabel = pc.FriendlyName;

                freeSeries.Points.Add(freePoint);
                index--;
            }
        }

    }
}
