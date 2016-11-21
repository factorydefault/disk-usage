﻿using System.Drawing;
using disk_usage;

namespace disk_usage_ui
{
    public interface Theme
    {
        Color ChartUsedSpace { get; }
        Color ChartUsedSpaceSecondary { get; }

        Image NetworkDiskImage { get; }
        Image LocalDiskImage { get; }

        Image OSDiskImage { get; }
        Image NotFoundImage { get; }
    }

    public static class Theming
    {
        public static Theme ForCurrentOS()
        {
            return Windows.CurrentOSName().GetTheme();
        }

        public static Theme GetTheme(this OSName version)
        {
            switch (version)
            {
                case OSName.Windows10:
                    return new Windows10Theme();
                case OSName.Windows8:
                    return new Windows10Theme(); //lazy, not making an additional theme for win 8 at the moment
                default:
                    return new Windows7Theme();
            }
        }
    }


    public class Windows7Theme : Theme
    {
        public Color ChartUsedSpace => Color.FromArgb(5, 214, 42);
        public Color ChartUsedSpaceSecondary => Color.FromArgb(192, 255, 192);

        public Image NetworkDiskImage => Properties.Resources.networkdrive7;

        public Image LocalDiskImage => Properties.Resources.localdrive7;

        public Image OSDiskImage => Properties.Resources.osdisk7;

        public Image NotFoundImage => Properties.Resources.notfound7;


    }

    public class Windows10Theme: Theme
    {
        public Color ChartUsedSpace => Color.FromArgb(36, 158, 215);
        public Color ChartUsedSpaceSecondary => Color.FromArgb(86, 181, 226);
        public Image NetworkDiskImage => Properties.Resources.networkdrive10;

        public Image LocalDiskImage => Properties.Resources.localdrive10;

        public Image OSDiskImage => Properties.Resources.osdrive10;

        public Image NotFoundImage => Properties.Resources.notfound10;
    }

    public class Windows8Theme : Theme
    {
        public Color ChartUsedSpace => Color.Blue;
        public Color ChartUsedSpaceSecondary => Color.Blue;
        public Image NetworkDiskImage => Properties.Resources.networkdrive7;

        public Image LocalDiskImage => NetworkDiskImage;

        public Image OSDiskImage => NetworkDiskImage;

        public Image NotFoundImage => Properties.Resources.notfound7;
    }

}
