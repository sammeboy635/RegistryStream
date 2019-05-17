using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class ProcessOBJ 
    {
        //all the registry types

        public string ProcessFileLocation { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public ProcessOBJ(string processFileLocation, int processId, string processName)
        {
            ProcessFileLocation = processFileLocation;
            ProcessId = processId;
            ProcessName = processName;
        }
    }
}
