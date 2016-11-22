using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using disk_usage;

namespace disk_usage_ui.UserControls
{
    public enum ChartDisplayMode
    {
        Capacity,
        PercentageFill
    }

    public enum ChartOrientation
    {
        Horizontal,
        Vertical,
    }

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

        ChartDisplayMode Mode { get; set; } = ChartDisplayMode.Capacity;
                
        public void SaveImage(string imageFileName,ChartImageFormat format)
        {
            Chart.SaveImage(imageFileName, format);
        }

        DiskUsage dataStore { get; set; } //hold a reference to disk usage.

        public IEnumerable<PathRecord> PathsInSortedOrder
        {
            get
            {
                return dataStore.Sorted(Sorting);
            }
        }



        SortingOption Sorting { get; set; }

        public void AssignData(DiskUsage data, SortingOption initialSorting)
        {
            dataStore = data;
            Sorting = initialSorting;

            SetChartOptions(Mode, Sorting);
        }
        
        public void SetChartOptions(ChartDisplayMode mode, SortingOption sorting)
        {
            Sorting = sorting;
            Mode = mode;
            DrawChart(PathsInSortedOrder);
        }

        void SetAxisYTitle(string text)
        {
            Chart.ChartAreas[0].AxisY.Title = text;
        } 
         
        public IEnumerable<string> SeriesNames()
        {
            var usedSeries = Chart.Series["UsedSpace"];

            foreach( var point in usedSeries.Points)
            {
                yield return point.AxisLabel;
            }

        }

        public ChartOrientation ChartOrientation { get; set; } = ChartOrientation.Horizontal;

        SeriesChartType normalSeries
        {
            get
            {
                return (ChartOrientation == ChartOrientation.Horizontal) ? SeriesChartType.StackedBar : SeriesChartType.StackedColumn;
            }
        }

        SeriesChartType percentageSeries
        {
            get
            {
                return (ChartOrientation == ChartOrientation.Horizontal) ? SeriesChartType.StackedBar100 : SeriesChartType.StackedColumn100;
            }
        }


        void DrawChart(IEnumerable<PathRecord> data)
        {
            var usedSeries = Chart.Series["UsedSpace"];
            var freeSeries = Chart.Series["FreeSpace"];

            bool isUsingCapacity = (Mode == ChartDisplayMode.Capacity);
            bool isHorizontal = (ChartOrientation == ChartOrientation.Horizontal);

            usedSeries.ChartType = (isUsingCapacity) ? normalSeries : percentageSeries;
            freeSeries.ChartType = (isUsingCapacity) ? normalSeries : percentageSeries;

            usedSeries.BackGradientStyle = isHorizontal ? GradientStyle.HorizontalCenter : GradientStyle.VerticalCenter;

            Chart.ChartAreas[0].AxisX.LabelStyle.Angle = isHorizontal ? 0 : -60;

            SetAxisYTitle((isUsingCapacity) ? "Capacity (GB)" : "Percentage Fill");

            usedSeries.Points.Clear();
            freeSeries.Points.Clear();

            var index = isHorizontal? data.Count(): 0; //for correct ordering
            bool hideEmpty = Properties.Settings.Default.HideInaccessablePaths;

            foreach (var pc in data)
            {
                if (hideEmpty && pc.HasZeroCapacity) continue;

                if (!pc.ShowOnChart) continue;

                var usedPoint = new DataPoint();

                usedPoint.SetValueXY(index, pc.UsedSpace.GigaBytes);
                usedPoint.AxisLabel = pc.FriendlyName;

                if (pc.HasLowDiskSpace)
                {
                    usedPoint.Color = lowDiskSpaceColor;
                    usedPoint.BackSecondaryColor = lowDiskSpaceColorSecondary;
                }

                usedSeries.Points.Add(usedPoint);

                var freePoint = new DataPoint();

                freePoint.SetValueXY(index, pc.FreeSpace.GigaBytes);
                freePoint.AxisLabel = pc.FriendlyName;

                freeSeries.Points.Add(freePoint);

                index = (isHorizontal) ? index - 1 : index + 1;
            }
        }

    }
}
