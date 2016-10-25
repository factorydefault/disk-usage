using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace disk_usage_ui.UserControls
{
    public partial class DiskChart : UserControl
    {
        public DiskChart()
        {
            InitializeComponent();

            if (Program.Theme != null)
            {
                Chart.Series["UsedSpace"].Color = Program.Theme.ChartUsedSpace;
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

            foreach(var pc in data)
            {
                DataPoint usedPoint = new DataPoint();

                usedPoint.SetValueXY(index, pc.TotalSpace - pc.FreeSpace);
                //usedPoint.SetValueY(pc.TotalSpace - pc.FreeSpace);
                usedPoint.AxisLabel = pc.FriendlyName;

                usedSeries.Points.Add(usedPoint);
                
                DataPoint freePoint = new DataPoint();

                freePoint.SetValueXY(index, pc.FreeSpace);
                //freePoint.SetValueY(pc.FreeSpace);
                freePoint.AxisLabel = pc.FriendlyName;

                freeSeries.Points.Add(freePoint);
                index--;
            }
        }

    }
}
