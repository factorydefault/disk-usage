using Newtonsoft.Json;
using System.Collections.Generic;

namespace disk_usage
{
    public class ComputerCollection
    {
        public ComputerCollection()
        {
            computers = new List<Computer>();
        }

        List<Computer> computers;

        public void AddComputer(Computer computer)
        {
            computers.Add(computer);
        }

        public void AddComputer(string path, string friendlyName = "")
        {
            AddComputer(Computer.Create(path, friendlyName));
        }

        [JsonProperty("Computers")]
        public List<Computer> PCs
        {
            get
            {
                return computers;
            }
        }

        public void RemoveComputerWithPath(string path)
        {
            foreach(var comp in computers)
            {
                if(comp.Path == path)
                {
                    computers.Remove(comp);
                    break;
                }
            }
        }
    }
}
