using System;
using System.Diagnostics;

namespace disk_usage_ui
{
    public static class Tools
    {
        public static void OpenDirectory(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"Cannot open \"{path}\", it may be inaccessible or invalid.","Unable to open path");
            }
        }

        public static void OpenDirectory(disk_usage.PathRecord record)
        {
            OpenDirectory(record.Path);
        }

        public static string Timestamp()
        {
            DateTime now = DateTime.Now;
            return $"{now.Year}-{now.Month:00}-{now.Day:00} T{now.Hour:00}{now.Minute:00}";
        }

    }
}
