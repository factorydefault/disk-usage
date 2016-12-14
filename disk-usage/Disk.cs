using System;
using System.Threading.Tasks;

namespace disk_usage
{
    public struct DiskAttributes
    {
        public ulong AvailableBytes { get; }
        public ulong TotalBytes { get;}
        public ulong FreeBytes { get; }

        public DiskAttributes(ulong available, ulong totalBytes, ulong totalFreeBytes)
        {
            AvailableBytes = available;
            TotalBytes = totalBytes;
            FreeBytes = totalFreeBytes;
        }

        [Obsolete]
        public void WriteToConsole()
        {
            Console.WriteLine("Bytes Available to User: {0,15:D}", AvailableBytes);
            Console.WriteLine("Total Bytes: {0,15:D}", TotalBytes);
            Console.WriteLine("Free Bytes: {0,15:D}", FreeBytes);
        }

        public double PercentageFree => (TotalBytes > 0) ? (FreeBytes / (double)TotalBytes) * 100.0 : 0;

        public double PercentageFilled => (100.0 - PercentageFree);

    }


    internal class Disk
    {
        public event EventHandler<EventArgs> DiskInfoUpdated;

        public string Path { get; set; }

        public DiskAttributes Attributes { get; private set; } = new DiskAttributes(0, 0, 0);

        public async Task RequestDiskInfoAsync()
        {
            await RequestDiskInfoTask();
            DiskInfoUpdated?.Invoke(this, new EventArgs());
        }

        Task RequestDiskInfoTask() => Task.Run( () => RequestDiskInfo() );
        
        /// <summary>
        /// Blocks thread when called directly.. use RequestDiskInfoAync instead.
        /// </summary>
        public void RequestDiskInfo()
        {
            ulong ulAvailableBytes;
            ulong ulBytes;
            ulong ulFreeBytes;

            var success = NativeMethods.GetDiskFreeSpaceEx(Path,
                                  out ulAvailableBytes,
                                  out ulBytes,
                                  out ulFreeBytes);

            Attributes = success ? new DiskAttributes(ulAvailableBytes, ulBytes, ulFreeBytes) : new DiskAttributes(0, 0, 0);
        }
    }

}
