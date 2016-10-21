using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace disk_usage
{
    public class DiskUsage
    {
        const string DATA_FOLDER = "disk_usage_data";
        const string PATHS_FILE = "paths.json";
        Formatting SETTINGS_FORMAT = Formatting.Indented;

        public bool SettingsFileWasGenerated { get; private set; } = false;

        public DiskUsage()
        {
            Paths = new List<PathRecord>();
            Refresh(); 
        }

        public string SettingsFileLocation
        {
            get
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return $"{localAppData}\\{DATA_FOLDER}\\{PATHS_FILE}";
            }
        }

        string SettingsDirectory => Path.GetDirectoryName(SettingsFileLocation);

        bool SettingsFileExists => File.Exists(SettingsFileLocation);

        void Refresh()
        {
            if (SettingsFileExists)
            {
                ReadSettingsFile();
            }
            else
            {
                CreateNewSettingsFile();
            }
        }


        void CreateNewSettingsFile()
        {
            Paths.Clear();
            Debug.Print("Creating json settings file with defaults");
            AddPathToList(Windows.InstallDirectory, $"OSDisk ({Windows.InstallDirectory})");

            Directory.CreateDirectory(SettingsDirectory);

            SaveSettingsFile();

            SettingsFileWasGenerated = true;
        }

        public void SaveSettingsFile()
        {
            try
            {
                Debug.Print("trying to save");
                using (StreamWriter file = File.CreateText(SettingsFileLocation))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = SETTINGS_FORMAT;
                    serializer.Serialize(file, Paths);
                }
                Debug.Print("saved!");
            }
            catch (Exception ex)
            {
                Debug.Print($"could not save: {ex.Message}");
            }

        }

        void ReadSettingsFile()
        {
            try
            {
                Paths = JsonConvert.DeserializeObject<List<PathRecord>>(File.ReadAllText(SettingsFileLocation));
            }
            catch (JsonReaderException ex)
            {
                Debug.Print($"Unable to read settings file: {ex.Message}");
                Paths = new List<PathRecord>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        List<PathRecord> _pathList;

        public void AddPathToList(PathRecord computer)
        {
            _pathList.Add(computer);
        }

        public void AddPathToList(string path, string friendlyName = "")
        {
            AddPathToList(PathRecord.Create(path, friendlyName));
        }

        public List<PathRecord> Paths
        {
            get
            {
                return _pathList;
            }
            private set
            {
                _pathList = value;
            }
        }
        
        public List<PathRecord> SortedList(SortingOption sorting)
        {
            switch (sorting)
            {
                case SortingOption.Alphabetical:
                    return Paths.OrderBy(o => o.FriendlyName).ToList();
                case SortingOption.AlphabeticalDescending:
                    return Paths.OrderByDescending(o => o.FriendlyName).ToList();
                case SortingOption.FreeSpace:
                    return Paths.OrderBy(o => o.FreeSpace).ToList();
                case SortingOption.FreeSpaceDescending:
                    return Paths.OrderByDescending(o => o.FreeSpace).ToList();
                case SortingOption.FillPercentage:
                    return Paths.OrderBy(o => o.FillLevel).ToList();
                case SortingOption.FillPercentageDescending:
                    return Paths.OrderByDescending(o => o.FillLevel).ToList();
                case SortingOption.Capacity:
                    return Paths.OrderBy(o => o.TotalSpace).ToList();
                case SortingOption.CapacityDescending:
                    return Paths.OrderByDescending(o => o.TotalSpace).ToList();
                default:
                    Debug.Print("not recognised");
                    return Paths;
            }
        }

        public void RemovePathFromList(string path)
        {
            foreach (var paths in _pathList)
            {
                if (paths.Path == path)
                {
                    _pathList.Remove(paths);
                    break;
                }
            }
        }

    }

}
