using System;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using ByteSizeLib;

namespace disk_usage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PathRecord
    {
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


        Disk disk;

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
                string filename = FriendlyName;

                foreach (var c in System.IO.Path.GetInvalidFileNameChars())
                {
                    //filename = filename.Replace(c, '-');
                    filename = filename.Replace(c.ToString(), "");
                }

                return filename;
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

        public double FreeSpaceRounded
        {
            get
            {
                return Math.Round(disk.DSI.FreeSpaceInGB, 2);
                //return Math.Round(disk.Info().FreeSpaceInGB, 2);
            }
        }
                
        public double TotalSpaceRounded
        {
            get
            {
                return Math.Round(disk.DSI.TotalSpaceInGB, 2);
                //return Math.Round(disk.Info().TotalSpaceInGB, 2);
            }
        }

        public string PercentageFilled => $"{Math.Round(disk.DSI.PercentageFilled, 2)} %";

        public ByteSize FreeSpace => ByteSize.FromBytes(disk.DSI.FreeBytesAvailable);

        public ByteSize UsedSpace => ByteSize.FromBytes(bytesUsed);

        public ByteSize Capacity => ByteSize.FromBytes(disk.DSI.TotalNumberOfBytes);

        ulong bytesUsed => disk.DSI.TotalNumberOfBytes - disk.DSI.FreeBytesAvailable;

        public int FillLevel => (int)Math.Round(disk.DSI.PercentageFilled, 0);

        /// <summary>
        /// As per https://blogs.msdn.microsoft.com/oldnewthing/20101117-00/?p=12263/
        /// </summary>
        public bool HasLowDiskSpace => FillLevel > 90;
        
        public static PathRecord Create(string path, string name = "")
        {
            return new PathRecord { Path = path, FriendlyName = name };
        }


        public static Regex LocalRegex => new Regex(@"^([a-zA-Z]):\\(?:([^\\\n]+\\)*)$");

        public static Regex LocalRootRegex => new Regex(@"^([a-zA-Z]):\\");

        public static Regex UNCNamedRegex => new Regex(@"^\\\\([^\\]+\\)(?:([^\\\n]+\\)+)$");

        //public Regex UNCIPRegex => new Regex(@"");


        public PathLocation Location()
        {

            var match = LocalRegex.Match(Path);

            if (match.Success || LocalRootRegex.IsMatch(Path))
            {
                string drive = $"{match.Groups[1].Value}:\\";
                //Console.WriteLine(drive);
                return (drive == Windows.InstallDirectory) ? PathLocation.OS : PathLocation.Local;
            }
            return PathLocation.Remote;


        }

    }
}
