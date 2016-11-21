using IWshRuntimeLibrary;
using System;
using System.Runtime.InteropServices;

namespace disk_usage
{
    public static class Windows
    {
        public static OSName CurrentOSName()
        {
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;
            return ver.Name();
        }

        public static OSName Name(this Version version)
        {
            switch ($"{version.Major}.{version.Minor}")
            {
                case "10.1":
                    return OSName.Windows10;
                case "6.2":
                case "6.3":
                    return OSName.Windows8; //it's not important to distinguish 8.0/8.1 here.
                case "6.1":
                    return OSName.Windows7;
                default:
                    return OSName.Other;
            }
        }

        public static string Desktop => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string InstallDirectory => System.IO.Path.GetPathRoot(Environment.SystemDirectory);


        public static bool ShowFileProperties(string Filename)
        {
            var info = new NativeMethods.SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = 5;
            info.fMask = 12;
            return NativeMethods.ShellExecuteEx(ref info);
        }

        //http://stackoverflow.com/questions/13019189/creating-a-shortcut-to-a-folder-in-c-sharp

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="SourceFile">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        public static void CreateShortcut(string SourceFile, string ShortcutFile)
        {
            CreateShortcut(SourceFile, ShortcutFile, null, null, null, null);
        }

        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="TargetPath">A file you want to make shortcut to</param>
        /// <param name="ShortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="Description">Shortcut description</param>
        /// <param name="Arguments">Command line arguments</param>
        /// <param name="HotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="WorkingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string TargetPath, string ShortcutFile, string Description,
           string Arguments, string HotKey, string WorkingDirectory)
        {
            if (string.IsNullOrEmpty(TargetPath))
                throw new ArgumentNullException(nameof(TargetPath));
            if (string.IsNullOrEmpty(ShortcutFile))
                throw new ArgumentNullException(nameof(ShortcutFile));

            var wshShell = new WshShell();

            var shorcut = (IWshShortcut)wshShell.CreateShortcut(ShortcutFile);

            shorcut.TargetPath = TargetPath;
            shorcut.Description = Description;
            if (!string.IsNullOrEmpty(Arguments))
                shorcut.Arguments = Arguments;
            if (!string.IsNullOrEmpty(HotKey))
                shorcut.Hotkey = HotKey;
            if (!string.IsNullOrEmpty(WorkingDirectory))
                shorcut.WorkingDirectory = WorkingDirectory;

            shorcut.Save();
        }


    }

    static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
        out ulong lpFreeBytesAvailable,
        out ulong lpTotalNumberOfBytes,
        out ulong lpTotalNumberOfFreeBytes);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO
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
