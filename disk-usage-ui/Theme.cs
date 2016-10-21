using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disk_usage_ui
{
    public interface Theme
    {
        Color ChartUsedSpace { get; }
    }

    public static class Theming
    {
        public static Theme ForVersion(disk_usage.OSVersion version)
        {
            switch (version)
            {
                case disk_usage.OSVersion.Windows10:
                    return new Windows10Theme();
                case disk_usage.OSVersion.Windows8:
                    return new Windows8Theme();
                default:
                    return new Windows7Theme();
            }
        }
    }


    public class Windows7Theme : Theme
    {
        public Color ChartUsedSpace => Color.FromArgb(5, 214, 42);

    }

    public class Windows10Theme: Theme
    {
        public Color ChartUsedSpace => Color.FromArgb(36, 158, 215);
    }

    public class Windows8Theme : Theme
    {
        public Color ChartUsedSpace => Color.Blue;
    }

}
