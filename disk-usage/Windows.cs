using IWshRuntimeLibrary;
using System;
using System.Runtime.InteropServices;

namespace disk_usage
{
    public static class Windows
    {
        public static OsName CurrentOsName()
        {
            var os = Environment.OSVersion;
            var ver = os.Version;
            return ver.Name();
        }

        public static OsName Name(this Version version)
        {
            switch ($"{version.Major}.{version.Minor}")
            {
                case "10.1":
                    return OsName.Windows10;
                case "6.2":
                case "6.3":
                    return OsName.Windows8; //it's not important to distinguish 8.0/8.1 here.
                case "6.1":
                    return OsName.Windows7;
                default:
                    return OsName.Other;
            }
        }

        public static string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string InstallDirectory => System.IO.Path.GetPathRoot(Environment.SystemDirectory);


        public static bool ShowFileProperties(string filename)
        {
            var info = new NativeMethods.ShellExecuteInfo();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = 5;
            info.fMask = 12;
            return NativeMethods.ShellExecuteEx(ref info);
        }

        //http://stackoverflow.com/questions/13019189/creating-a-shortcut-to-a-folder-in-c-sharp

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="sourceFile">A file you want to make shortcut to</param>
        /// <param name="shortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        public static void CreateShortcut(string sourceFile, string shortcutFile)
        {
            CreateShortcut(sourceFile, shortcutFile, null, null, null, null);
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="targetPath">A file you want to make shortcut to</param>
        /// <param name="shortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="description">Shortcut description</param>
        /// <param name="arguments">Command line arguments</param>
        /// <param name="hotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="workingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string targetPath, string shortcutFile, string description,
           string arguments, string hotKey, string workingDirectory)
        {
            if (string.IsNullOrEmpty(targetPath))
                throw new ArgumentNullException(nameof(targetPath));
            if (string.IsNullOrEmpty(shortcutFile))
                throw new ArgumentNullException(nameof(shortcutFile));

            var wshShell = new WshShell();

            var shorcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFile);

            shorcut.TargetPath = targetPath;
            shorcut.Description = description;
            if (!string.IsNullOrEmpty(arguments))
                shorcut.Arguments = arguments;
            if (!string.IsNullOrEmpty(hotKey))
                shorcut.Hotkey = hotKey;
            if (!string.IsNullOrEmpty(workingDirectory))
                shorcut.WorkingDirectory = workingDirectory;

            shorcut.Save();
        }


    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
        out ulong lpFreeBytesAvailable,
        out ulong lpTotalNumberOfBytes,
        out ulong lpTotalNumberOfFreeBytes);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ShellExecuteInfo
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }
    }

}
