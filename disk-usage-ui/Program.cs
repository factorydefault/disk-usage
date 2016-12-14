using System;
using System.Windows.Forms;
using disk_usage_ui.Forms;

namespace disk_usage_ui
{
    internal static class Program
    {
        public static ITheme Theme;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Theme = Theming.ForCurrentOs();

#if THEME
            // OVERRIDE
            Theme = Theming.ForVersion(OSVersion.Windows10);
#endif

            Application.Run(new NotificationAreaForm());
        }
    }
}
