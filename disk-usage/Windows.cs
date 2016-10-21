using System;

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
    }
}
