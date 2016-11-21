using System;
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

        [Obsolete]
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
                return (TotalBytes > 0) ? (FreeBytes / (double)TotalBytes) * 100.0 : 0;
            }
        }

        public double PercentageFilled => (100.0 - PercentageFree);

    }



    class Disk
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
            if (success)
            {
                Attributes = new DiskAttributes(ulAvailableBytes, ulBytes, ulFreeBytes);
            }
            else
            {
                Attributes = new DiskAttributes(0, 0, 0);
            }
        }
    }

}
