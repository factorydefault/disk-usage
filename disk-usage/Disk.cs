using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace disk_usage
{
    public struct DiskAttributes
    {
        public ulong AvailableBytes { get; private set; }
        public ulong TotalBytes { get; private set; }
        public ulong FreeBytes { get; private set; }

        public DiskAttributes(ulong available, ulong totalBytes, ulong totalFreeBytes)
        {
            AvailableBytes = available;
            TotalBytes = totalBytes;
            FreeBytes = totalFreeBytes;
        }

        public void WriteToConsole()
        {
            Console.WriteLine("Bytes Available to User: {0,15:D}", AvailableBytes);
            Console.WriteLine("Total Bytes: {0,15:D}", TotalBytes);
            Console.WriteLine("Free Bytes: {0,15:D}", FreeBytes);
        }

        public double PercentageFree
        {
            get
            {
                return (TotalBytes > 0) ? (FreeBytes / (double)TotalBytes * 100.0) : 0;
            }
        }

        public double PercentageFilled => (100.0 - PercentageFree);

    }

    class Disk
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
      out ulong lpFreeBytesAvailable,
      out ulong lpTotalNumberOfBytes,
      out ulong lpTotalNumberOfFreeBytes);

        public event EventHandler<EventArgs> DiskInfoUpdated;

        public string Path { get; set; }

        public DiskAttributes Attributes { get; private set; } = new DiskAttributes(0, 0, 0);

        public async Task RequestDiskInfo()
        {
            await GetInfoAsync();
            //Console.WriteLine($"Async done, {Path} has {Attributes.FreeBytes} bytes free.");
            DiskInfoUpdated?.Invoke(this, new EventArgs());
        }

        public Task GetInfoAsync()
        {
            return Task.Run( () =>
            {
                ulong ulAvailableBytes;
                ulong ulBytes;
                ulong ulFreeBytes;

                bool success = GetDiskFreeSpaceEx(Path,
                                      out ulAvailableBytes,
                                      out ulBytes,
                                      out ulFreeBytes);
                if (success)
                {
                    Attributes = new DiskAttributes(ulAvailableBytes, ulBytes, ulFreeBytes);
                }
                else
                {
                    Attributes = new DiskAttributes(0, 0, 0);
                }
            });

        }

    }

}
