using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class NetworkOBJ
    {
        public int EventType { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public NetworkOBJ(int eventtype, string ip, int port)
        {
            EventType = eventtype;
            IP = ip;
            Port = port;
        }
    }
}
