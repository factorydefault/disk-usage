using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using ByteSizeLib;
using System.Linq;
using System.Threading.Tasks;

namespace disk_usage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PathRecord
    {
        // As per https://blogs.msdn.microsoft.com/oldnewthing/20101117-00/?p=12263/
        public const int LowDiskSpacePercentage = 90;

        readonly Disk _disk;

        public PathRecord()
        {
            FriendlyName = "";
            _disk = new Disk();
            _disk.DiskInfoUpdated += Disk_DiskInfoUpdated;
        }

        public event EventHandler<EventArgs> DiskInfoUpdated;

        void Disk_DiskInfoUpdated(object sender, EventArgs e)
        {
            DiskInfoUpdated?.Invoke(this, new EventArgs()); //pass up event
        }

        public DiskAttributes DiskAttributes
        {
            get { return _disk.Attributes; }
            set { _disk.Attributes = value; }
        }

        public Task RequestInfoTask => _disk.RequestDiskInfoAsync();

        //public void RequestDiskInfoAsync()
        //{
        //    RequestInfoTask.FireAndForget();
        //}

        public void RequestDiskInfo()
        {
            _disk.RequestDiskInfo();
        }

        public bool ShowOnChart { get; set; } = true;

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
                var filename = FriendlyName.Replace('.', '_').Replace("\\", " ");

                char[] toTrim = { ' ', '_' };

                return System.IO.Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c.ToString(), string.Empty)).Trim(toTrim);     
            }
        }

        [JsonProperty]
        public string Path
        {
            get
            {
                return _disk.Path;
            }
            set
            {
                _disk.Path = value;
            }
        }

        public ByteSize FreeSpace => ByteSize.FromBytes(_disk.Attributes.FreeBytes);

        public ByteSize UsedSpace => ByteSize.FromBytes(BytesUsed);

        public ByteSize Capacity => ByteSize.FromBytes(_disk.Attributes.TotalBytes);

        public bool HasZeroCapacity => Capacity.Bytes < 1;

        ulong BytesUsed => _disk.Attributes.TotalBytes - _disk.Attributes.FreeBytes;

        public int FillLevel => (int)Math.Round(_disk.Attributes.PercentageFilled, 0);

        public double FillPercentageDbl => _disk.Attributes.PercentageFilled;

        public bool HasLowDiskSpace => FillLevel > LowDiskSpacePercentage;
        
        public static PathRecord Create(string path, string name = "")
        {
            return new PathRecord { Path = path, FriendlyName = name };
        }

        public static Regex LocalRegex => new Regex(@"^([A-Z]):\\(?:([^\\\n]+\\)*)$");

        public static Regex LocalRootRegex => new Regex(@"^([a-zA-Z]):\\");

        public static Regex UncNamedRegex => new Regex(@"^\\\\([^\\]+\\)(?:([^\\\n]+\\)+)$");


        public PathLocation Location()
        {
            var match = LocalRegex.Match(Path);

            if (match.Success || LocalRootRegex.IsMatch(Path))
            {
                string drive = $"{match.Groups[1].Value}:\\";
                return (drive == Windows.InstallDirectory) ? PathLocation.Os : PathLocation.Local;
            }
            return PathLocation.Remote;
        }

        public bool ShouldNotify => Notifications && Capacity.Bytes > 0;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Notifications { get; set; } = false;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue(false)]
        public bool Highlight { get; set; } = false;


    }
}
