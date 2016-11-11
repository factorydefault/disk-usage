using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using ByteSizeLib;
using System.Linq;

namespace disk_usage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PathRecord
    {
        public const int LOW_DISK_SPACE_PERCENTAGE = 90;

        Disk disk;

        public PathRecord()
        {
            FriendlyName = "";
            disk = new Disk();
            disk.DiskInfoUpdated += Disk_DiskInfoUpdated;
        }

        public event EventHandler<EventArgs> DiskInfoUpdated;

        void Disk_DiskInfoUpdated(object sender, EventArgs e)
        {
            DiskInfoUpdated?.Invoke(this, new EventArgs()); //pass on event
        }

        public void RequestDiskInfo()
        {
            disk.RequestDiskInfo().Forget();
        }

        [JsonProperty]
        public string FriendlyName
        {
            get
            {
                return (string.IsNullOrWhiteSpace(_friendlyName)) ? Path : _friendlyName;
            }
            set
            {
                _friendlyName = value;
            }
        }
        string _friendlyName;

        public string ShortcutName
        {
            get
            {
                string filename = FriendlyName.Replace('.', '_');

                filename = filename.Replace("\\", " ");

                char[] toTrim = { ' ', '_' };

                return System.IO.Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c.ToString(), string.Empty)).Trim(toTrim);     
            }
        }

        [JsonProperty]
        public string Path
        {
            get
            {
                return disk.Path;
            }
            set
            {
                disk.Path = value;
            }
        }

        public string PercentageFilled => $"{Math.Round(disk.Attributes.PercentageFilled, 2)} %";

        public ByteSize FreeSpace => ByteSize.FromBytes(disk.Attributes.FreeBytes);

        public ByteSize UsedSpace => ByteSize.FromBytes(bytesUsed);

        public ByteSize Capacity => ByteSize.FromBytes(disk.Attributes.TotalBytes);

        ulong bytesUsed => disk.Attributes.TotalBytes - disk.Attributes.FreeBytes;

        public int FillLevel => (int)Math.Round(disk.Attributes.PercentageFilled, 0);

        /// <summary>
        /// As per https://blogs.msdn.microsoft.com/oldnewthing/20101117-00/?p=12263/
        /// </summary>
        public bool HasLowDiskSpace => FillLevel > LOW_DISK_SPACE_PERCENTAGE;
        
        public static PathRecord Create(string path, string name = "")
        {
            return new PathRecord { Path = path, FriendlyName = name };
        }

        public static Regex LocalRegex => new Regex(@"^([A-Z]):\\(?:([^\\\n]+\\)*)$");

        public static Regex LocalRootRegex => new Regex(@"^([a-zA-Z]):\\");

        public static Regex UNCNamedRegex => new Regex(@"^\\\\([^\\]+\\)(?:([^\\\n]+\\)+)$");


        public PathLocation Location()
        {
            var match = LocalRegex.Match(Path);

            if (match.Success || LocalRootRegex.IsMatch(Path))
            {
                string drive = $"{match.Groups[1].Value}:\\";
                return (drive == Windows.InstallDirectory) ? PathLocation.OS : PathLocation.Local;
            }
            return PathLocation.Remote;
        }

        public bool ShouldNotify
        {
            get
            {
                return Notifications && Capacity.Bytes > 0;
            }
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Notifications { get; set; } = true;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Highlight { get; set; } = false;


    }
}
