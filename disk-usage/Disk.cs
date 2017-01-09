using System;
using System.Text;
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

        public string AsStringMessage()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bytes Available to User: {AvailableBytes,15:D}");
            sb.AppendLine($"Total Bytes: {TotalBytes,15:D}");
            sb.AppendLine($"Free Bytes: {FreeBytes,15:D}");
            return sb.ToString();
        }

        public double PercentageFree => (TotalBytes > 0) ? (FreeBytes / (double)TotalBytes) * 100.0 : 0;

        public double PercentageFilled => (100.0 - PercentageFree);

    }


    internal class Disk
    {
        public event EventHandler<EventArgs> DiskInfoUpdated;

        public string Path { get; set; }

        public DiskAttributes Attributes { get; set; } = new DiskAttributes(0, 0, 0);

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
