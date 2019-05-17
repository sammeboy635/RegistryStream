using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Test;

namespace RegistryStream
{
    class Program
    {
        public List<Process> SystemProcessesList;
        public static void Main()
        {
            if (TraceEventSession.IsElevated() != true)
            {
                Console.WriteLine("Must be elevated (Admin) to run this program.");
                Console.ReadKey();
                //Debugger.Break();
                return;
            }

            string FileLocation = @"C:\Users\Sam\Desktop\Events.csv";
            StreamWriter sw = File.AppendText(FileLocation);
            using (TraceEventSession session = new TraceEventSession(KernelTraceEventParser.KernelSessionName))
            {
                session.EnableKernelProvider(
                    KernelTraceEventParser.Keywords.Registry |
                    KernelTraceEventParser.Keywords.ImageLoad |
                    KernelTraceEventParser.Keywords.FileIOInit |
                    KernelTraceEventParser.Keywords.NetworkTCPIP |
                    KernelTraceEventParser.Keywords.Process);
                //  Subscribe to RegistryOpen
                //session.Source.Kernel.AddCallbackForEvents<RegistryTraceData>(processEvent);
                session.Source.Kernel.RegistrySetValue += delegate (RegistryTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(RegistryOBJ.Registry(data.ProcessID,null,data.ProcessName,data.EventName,getStatus(data.Status),data.KeyName,data.KeyHandle,null,0));
                    //sw.WriteLine(data.ProcessID+","+null+"," +data.ProcessName+","+ data.EventName+"," +getStatus(data.Status)+","+ data.KeyName+","+ data.KeyHandle+"," +null+","+ 0);
                };
                session.Source.Kernel.RegistryOpen += delegate (RegistryTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(data.ProcessID + "," + null + "," + data.ProcessName + "," + data.EventName + "," + getStatus(data.Status) + "," + data.KeyName + "," + data.KeyHandle + "," + null + "," + 0);

                };
                session.Source.Kernel.FileIOFileDelete += delegate (FileIONameTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                };
                session.Source.Kernel.RegistryDelete += delegate (RegistryTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(data.ProcessID + "," + null + "," + data.ProcessName + "," + data.EventName + "," + getStatus(data.Status) + "," + data.KeyName + "," + data.KeyHandle + "," + null + "," + 0);
                };
                session.Source.Kernel.RegistryKCBDelete += delegate (RegistryTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(data.ProcessID + "," + null + "," + data.ProcessName + "," + data.EventName + "," + getStatus(data.Status) + "," + data.KeyName + "," + data.KeyHandle + "," + null + "," + 0);
                };
                session.Source.Kernel.RegistryKCBCreate += delegate (RegistryTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(data.ProcessID + "," + null + "," + data.ProcessName + "," + data.EventName + "," + getStatus(data.Status) + "," + data.KeyName + "," + data.KeyHandle + "," + null + "," + 0);
                };

                session.Source.Kernel.FileIOCreate += delegate (FileIOCreateTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                };
                session.Source.Kernel.ImageLoad += delegate (ImageLoadTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                };

                session.Source.Kernel.FileIORename += delegate (FileIOInfoTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                };
                session.Source.Kernel.ProcessStart += delegate (ProcessTraceData data)
                {
                    Console.WriteLine(data);
                    //sw.WriteLine(data);

                };

                session.Source.Kernel.ProcessStop += delegate (ProcessTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);

                };
                session.Source.Kernel.TcpIpConnect += delegate (TcpIpConnectTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(data.ProcessID + "," + null + "," + data.ProcessName + "," + data.EventName + "," + 0 + "," + 0 + "," + 0 + "," + data.daddr + "," + data.dport);
                };
                session.Source.Kernel.UdpIpSend += delegate (UdpIpTraceData data)
                {
                    //Console.WriteLine(data);
                    //sw.WriteLine(data);
                    //sw.WriteLine(data.ProcessName +","+ data.dport);
                };


                session.Source.Process();
            }
        }

        private static void processEvent(RegistryTraceData evt)
        {
            Console.WriteLine(evt);
            /*String[] output = new String[] {
            evt.TimeStamp.ToLongTimeString(),
            evt.EventName.Substring(9, evt.EventName.Length - 9),
            getPath(evt.ProcessID),
            evt.PayloadByName("KeyName").ToString(),
            evt.ProcessID.ToString(),
            //getStatus(int.Parse(evt.PayloadByName("Status").ToString())),
            //getMemUsage(evt.ProcessID).ToString()
            };

            ReplaceAll(output, "", "null");

            String line = String.Join(",", output);
            Console.Out.WriteLine(line);*/
        }
        public void ProcessList()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process a in processlist)
                SystemProcessesList.Add(a);
        }
        public static void ReplaceAll(string[] items, string oldValue, string newValue)
        {
            for (int index = 0; index < items.Length; index++)
                if (items[index] == oldValue)
                    items[index] = newValue;
        }

        public static long? getMemUsage(int pid)
        {
            try{
                Process target = Process.GetProcessById(pid);
                String temp = target.MainModule.FileName;
                return target.WorkingSet64/1024;
            }
            catch{
                return null;
            }         
        }

        public static String getPath(int pid)
        {
            try
            {
                Process target = Process.GetProcessById(pid);
                return target.MainModule.FileName;
            }
            catch
            {
                return "";
            }
        }

        public static bool getStatus(int status)
        {
            if (status== 0)
                return true;
            else
                return false;
        }
    }
}