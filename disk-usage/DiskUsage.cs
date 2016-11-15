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
        const Newtonsoft.Json.Formatting SETTINGS_FORMAT = Newtonsoft.Json.Formatting.Indented;

        public bool SettingsFileWasGenerated { get; private set; } = false;

        public DiskUsage()
        {
            Paths = new List<PathRecord>();
            ReadOrCreateSettingsFile(); 
        }

        public string SettingsFileLocation
        {
            get
            {
                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return $"{localAppData}\\{DATA_FOLDER}\\{PATHS_FILE}";
            }
        }

        string SettingsDirectory => Path.GetDirectoryName(SettingsFileLocation);

        bool SettingsFileExists => File.Exists(SettingsFileLocation);

        void ReadOrCreateSettingsFile()
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
            var result = AddPathToList(Windows.InstallDirectory, $"OSDisk ({Windows.InstallDirectory})");

            Directory.CreateDirectory(SettingsDirectory);

            SaveSettingsFile();

            SettingsFileWasGenerated = true;
        }

        public OperationResult SaveSettingsFile()
        {
            try
            {
                Debug.Print("trying to save");

                if (string.IsNullOrEmpty(SettingsFileLocation)) throw new NullReferenceException();

                using (StreamWriter file = File.CreateText(SettingsFileLocation))
                {
                    var serializer = new JsonSerializer { Formatting = SETTINGS_FORMAT };
                    serializer.Serialize(file, Paths);
                }
                Debug.Print("saved!");
                return OperationResult.Response(true,"Settings file saved");
            }
            catch (Exception ex)
            {
                Debug.Print($"could not save: {ex.Message}");
                return OperationResult.Response(false, $"could not save: {ex.Message}");
            }
            
        }

        public void RequestUpdateFromAll()
        {
            foreach(var path in Paths)
            {
                path.RequestDiskInfoAsync();
            }
        }

        void ReadSettingsFile()
        {
            try
            {
                if (string.IsNullOrEmpty(SettingsFileLocation)) throw new NullReferenceException();
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
            public bool Success { get; private set; }
            public string Message { get; private set; }


            OperationResult(bool result, string message = "")
            {
                Success = result;
                Message = message;
            }

            public static OperationResult Response(bool result, string message = "")
            {
                return new OperationResult { Success = result, Message = message };
            }

            public override string ToString()
            {
                string resultdesc = Success ? "Success" : "Failure";
                string message = !string.IsNullOrWhiteSpace(Message) ? $": {Message}" : string.Empty;
                return $"{resultdesc}{message}";
            }
        }

        public OperationResult AddPathToList(PathRecord computer)
        {
            foreach(var existing in _pathList)
            {
                if (existing.Path == computer.Path)
                {
                    Debug.Print($"Path {computer.Path} cannot be added as it already exists with the label {existing.FriendlyName}.");
                    return OperationResult.Response(false, $"Path {computer.Path} cannot be added as it already exists with the label {existing.FriendlyName}.");
                }
            }

            _pathList.Add(computer);
            return OperationResult.Response(true);
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
        
        public IEnumerable<PathRecord> Sorted(SortingOption sorting)
        {
            switch (sorting)
            {
                case SortingOption.Alphabetical:
                    return Paths.OrderBy(o => o.FriendlyName.Replace("\\",""));
                case SortingOption.AlphabeticalDescending:
                    return Paths.OrderByDescending(o => o.FriendlyName.Replace("\\", ""));
                case SortingOption.FreeSpace:
                    return Paths.OrderBy(o => o.FreeSpace.Bytes);
                case SortingOption.FreeSpaceDescending:
                    return Paths.OrderByDescending(o => o.FreeSpace.Bytes);
                case SortingOption.FillPercentage:
                    return Paths.OrderBy(o => o.FillLevel);
                case SortingOption.FillPercentageDescending:
                    return Paths.OrderByDescending(o => o.FillLevel);
                case SortingOption.Capacity:
                    return Paths.OrderBy(o => o.Capacity.Bytes);
                case SortingOption.CapacityDescending:
                    return Paths.OrderByDescending(o => o.Capacity.Bytes);
                case SortingOption.UsedSpace:
                    return Paths.OrderBy(o => o.UsedSpace.Bytes);
                case SortingOption.UsedSpaceDescending:
                    return Paths.OrderByDescending(o => o.UsedSpace.Bytes);
                default:
                    Debug.Print("sorting not recognised");
                    return Paths.AsEnumerable();
            }
        }

        public void RemovePathFromList(string path)
        {
            bool recurring = false;

            foreach (var paths in _pathList)
            {
                if (paths.Path == path)
                {
                    _pathList.Remove(paths);
                    recurring = true;
                    break;
                }
            }
            if (recurring) RemovePathFromList(path);
        }

    }
    
    static class TaskExtensions
    {
#pragma warning disable RECS0154 // Parameter is never used
                                /// <summary>
                                /// Consumes a task and doesn't do anything with it. Useful for fire-and-forget calls to asynchronous methods within asynchronous methods.
                                /// https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.threading.tplextensions.forget.aspx
                                /// </summary>
                                /// <param name="task"></param>
        public static void Forget(this System.Threading.Tasks.Task task)
        {
        }
#pragma warning restore RECS0154 // Parameter is never used
    }

}
