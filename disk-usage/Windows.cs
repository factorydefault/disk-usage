using System;
using System.Runtime.InteropServices;

namespace disk_usage
{
    public static class Windows
    {
        public static OSVersion Version()
        {
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;

            switch ($"{ver.Major}.{ver.Minor}")
            {
                case "10.1":
                    return OSVersion.Windows10;
                case "6.2":
                case "6.3":
                    return OSVersion.Windows8; //it's not important to distinguish 8.0/8.1 here.
                case "6.1":
                    return OSVersion.Windows7;
                default:
                    return OSVersion.Other;
            }
        }

        public static string InstallDirectory => System.IO.Path.GetPathRoot(Environment.SystemDirectory);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

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

        public static bool ShowFileProperties(string Filename)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = 5;
            info.fMask = 12;
            return ShellExecuteEx(ref info);
        }

    }
}
