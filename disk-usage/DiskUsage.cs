using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace disk_usage
{
    public class DiskUsage
    {
        const string SETTINGS_FILE = "\\disk_usage_data\\computers.txt";
        Formatting SETTINGS_FORMAT = Formatting.Indented;

        public bool SettingsFileWasGenerated { get; private set; } = false;


        public ComputerCollection Collection { get; private set; }

        public DiskUsage()
        {
            Collection = new ComputerCollection();
            Refresh();
        }


        public string SettingsFileLocation
        {
            get
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return $"{localAppData}{SETTINGS_FILE}";
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

        public string WindowsInstallDirectory => Path.GetPathRoot(Environment.SystemDirectory);

        void CreateNewSettingsFile()
        {
            Collection.PCs.Clear();
            Debug.Print("Creating json settings file with defaults");
            Collection.AddComputer(WindowsInstallDirectory, $"OSDisk ({WindowsInstallDirectory})");

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
                    serializer.Serialize(file, Collection);
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
                Collection = JsonConvert.DeserializeObject<ComputerCollection>(File.ReadAllText(SettingsFileLocation));
            }
            catch (JsonReaderException ex)
            {
                Debug.Print($"Unable to read settings file: {ex.Message}");
                Collection = new ComputerCollection();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }

}
