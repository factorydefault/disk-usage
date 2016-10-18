using System;
using Newtonsoft.Json;

namespace disk_usage
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Computer
    {
        public Computer()
        {
            FriendlyName = "";
            disk = new Disk();
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

        public double FreeSpace
        {
            get
            {
                return Math.Round(disk.Info().FreeSpaceInGB, 2);
            }
        }

        public double TotalSpace
        {
            get
            {
                return Math.Round(disk.Info().TotalSpaceInGB, 2);
            }
        }

        public string PercentageFilled
        {
            get
            {
                return $"{Math.Round(disk.Info().PercentageFilled, 2)} %";
            }
        }

        public int FillLevel => (int) Math.Round(disk.Info().PercentageFilled,0);

        public static Computer Create(string path, string name = "")
        {
            return new Computer { Path = path, FriendlyName = name };
        }

    }
}
