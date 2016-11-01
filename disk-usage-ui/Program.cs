using disk_usage;
using System;
using System.Windows.Forms;

namespace disk_usage_ui
{
    static class Program
    {
        public static Theme Theme;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            Theme = Theming.ForVersion(Windows.Version()); //determine theme

#if THEME
            // OVERRIDE
            Theme = Theming.ForVersion(OSVersion.Windows10);
#endif

            Application.Run(new NotificationAreaForm());
        }
    }
}
