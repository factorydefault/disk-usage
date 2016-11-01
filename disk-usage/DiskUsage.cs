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
        Newtonsoft.Json.Formatting SETTINGS_FORMAT = Newtonsoft.Json.Formatting.Indented;

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
            var or = AddPathToList(Windows.InstallDirectory, $"OSDisk ({Windows.InstallDirectory})");

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

        public void RequestUpdateFromAll()
        {
            foreach(var pr in Paths)
            {
                pr.RequestDiskInfo();
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

        public struct OperationResult
        {
            public bool Result { get; private set; }
            public string Message { get; private set; }

            public OperationResult(bool result, string message = "")
            {
                Result = result;
                Message = message;
            }
        }

        public OperationResult AddPathToList(PathRecord computer)
        {
            foreach(var existing in _pathList)
            {
                if (existing.Path == computer.Path)
                {
                    Debug.Print($"Path {computer.Path} cannot be added as it already exists with the label {existing.FriendlyName}.");
                    return new OperationResult(false, $"Path {computer.Path} cannot be added as it already exists with the label {existing.FriendlyName}.");
                }
            }

            _pathList.Add(computer);
            return new OperationResult(true);
        }

        public OperationResult AddPathToList(string path, string friendlyName = "")
        {
            return AddPathToList(PathRecord.Create(path, friendlyName));
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
                    return Paths.OrderBy(o => o.FriendlyName.Replace("\\","")).ToList();
                case SortingOption.AlphabeticalDescending:
                    return Paths.OrderByDescending(o => o.FriendlyName.Replace("\\", "")).ToList();
                case SortingOption.FreeSpace:
                    return Paths.OrderBy(o => o.FreeSpace.Bytes).ToList();
                case SortingOption.FreeSpaceDescending:
                    return Paths.OrderByDescending(o => o.FreeSpace.Bytes).ToList();
                case SortingOption.FillPercentage:
                    return Paths.OrderBy(o => o.FillLevel).ToList();
                case SortingOption.FillPercentageDescending:
                    return Paths.OrderByDescending(o => o.FillLevel).ToList();
                case SortingOption.Capacity:
                    return Paths.OrderBy(o => o.Capacity.Bytes).ToList();
                case SortingOption.CapacityDescending:
                    return Paths.OrderByDescending(o => o.Capacity.Bytes).ToList();
                case SortingOption.UsedSpace:
                    return Paths.OrderBy(o => o.UsedSpace.Bytes).ToList();
                case SortingOption.UsedSpaceDescending:
                    return Paths.OrderByDescending(o => o.UsedSpace.Bytes).ToList();
                default:
                    Debug.Print("sorting not recognised");
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
    
    static class TaskExtensions
    {
        /// <summary>
        /// Consumes a task and doesn't do anything with it. Useful for fire-and-forget calls to asynchronous methods within asynchronous methods.
        /// https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.threading.tplextensions.forget.aspx
        /// </summary>
        /// <param name="task"></param>
        public static void Forget(this System.Threading.Tasks.Task task)
        {
        }
    }

}
