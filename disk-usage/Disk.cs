using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace disk_usage
{
    struct DiskSpaceInformation
    {
        public ulong FreeBytesAvailable;
        public ulong TotalNumberOfBytes;
        public ulong TotalNumberOfFreeBytes;

        public DiskSpaceInformation(ulong free, ulong totalBytes, ulong totalFreeBytes)
        {
            FreeBytesAvailable = free;
            TotalNumberOfBytes = totalBytes;
            TotalNumberOfFreeBytes = totalFreeBytes;
        }

        public void WriteToConsole()
        {
            Console.WriteLine("Free Bytes Available:      {0,15:D}", FreeBytesAvailable);
            Console.WriteLine("Total Number Of Bytes:     {0,15:D}", TotalNumberOfBytes);
            Console.WriteLine("Total Number Of FreeBytes: {0,15:D}", TotalNumberOfFreeBytes);
        }

        public double FreeSpaceInGB => FreeBytesAvailable / Disk.GBConversion;

        public double TotalSpaceInGB => TotalNumberOfBytes / Disk.GBConversion;

        public double PercentageFree => (FreeBytesAvailable / (double)TotalNumberOfBytes * 100.0);

        public double PercentageFilled => (100.0 - PercentageFree);

    }

    internal class Disk
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
      out ulong lpFreeBytesAvailable,
      out ulong lpTotalNumberOfBytes,
      out ulong lpTotalNumberOfFreeBytes);

        public const double GBConversion = 1073741824.0;


        public string Path { get; set; }

        public DiskSpaceInformation Info()
        {
            if (string.IsNullOrWhiteSpace(Path)) throw new ArgumentException("Set path first");

            return GetInfoForPath(Path);
        }


        public static DiskSpaceInformation GetInfoForPath(string path = @"\\mycomputer\myfolder")
        {
            DiskSpaceInformation result;

            bool success = GetDiskFreeSpaceEx(path,
                                  out result.FreeBytesAvailable,
                                  out result.TotalNumberOfBytes,
                                  out result.TotalNumberOfFreeBytes);
            if (!success)
                throw new System.ComponentModel.Win32Exception();

            //result.WriteToConsole();

            return result;
        }


    }

    internal static class DiskExtensions
    {
        public static DiskSpaceInformation GetDiskInformation(this System.IO.DirectoryInfo dir)
        {
            return Disk.GetInfoForPath(dir.FullName);
        }
    }
}
