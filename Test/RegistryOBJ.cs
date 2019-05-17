using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class RegistryOBJ
    {
        public int EventType { get; set; }
        public bool Status { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public static string Registry(int ProcessID,string ProcessFileLocation,string ProcessName,string EventType,bool Status,string KeyName,ulong ValueName,string IP,int Port)
        {
            return(ProcessID+","+ProcessFileLocation+","+ProcessName+","+EventType+","+Status+","+KeyName+","+ValueName+","+IP+","+Port);
        }
    }
}
