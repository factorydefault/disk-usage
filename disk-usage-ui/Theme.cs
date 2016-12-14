using System.Drawing;
using disk_usage;

namespace disk_usage_ui
{
    public interface ITheme
    {
        Color ChartUsedSpace { get; }
        Color ChartUsedSpaceSecondary { get; }

        Image NetworkDiskImage { get; }
        Image LocalDiskImage { get; }

        Image OsDiskImage { get; }
        Image NotFoundImage { get; }
    }

    public static class Theming
    {
        public static ITheme ForCurrentOs()
        {
            return Windows.CurrentOsName().GetTheme();
        }

        public static ITheme GetTheme(this OsName version)
        {
            switch (version)
            {
                case OsName.Windows10:
                    return new Windows10Theme();
                case OsName.Windows8:
                    return new Windows10Theme(); //lazy, not making an additional theme for win 8 at the moment
                default:
                    return new Windows7Theme();
            }
        }
    }


    public class Windows7Theme : ITheme
    {
        public Color ChartUsedSpace => Color.FromArgb(5, 214, 42);
        public Color ChartUsedSpaceSecondary => Color.FromArgb(192, 255, 192);

        public Image NetworkDiskImage => Properties.Resources.networkdrive7;

        public Image LocalDiskImage => Properties.Resources.localdrive7;

        public Image OsDiskImage => Properties.Resources.osdisk7;

        public Image NotFoundImage => Properties.Resources.notfound7;


    }

    public class Windows10Theme: ITheme
    {
        public Color ChartUsedSpace => Color.FromArgb(36, 158, 215);
        public Color ChartUsedSpaceSecondary => Color.FromArgb(86, 181, 226);
        public Image NetworkDiskImage => Properties.Resources.networkdrive10;

        public Image LocalDiskImage => Properties.Resources.localdrive10;

        public Image OsDiskImage => Properties.Resources.osdrive10;

        public Image NotFoundImage => Properties.Resources.notfound10;
    }

    public class Windows8Theme : ITheme
    {
        public Color ChartUsedSpace => Color.Blue;
        public Color ChartUsedSpaceSecondary => Color.Blue;
        public Image NetworkDiskImage => Properties.Resources.networkdrive7;

        public Image LocalDiskImage => NetworkDiskImage;

        public Image OsDiskImage => NetworkDiskImage;

        public Image NotFoundImage => Properties.Resources.notfound7;
    }

}
