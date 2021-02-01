using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;   //This namespace is used to work with WMI classes. For using this namespace add reference of System.Management.dll .
using Microsoft.Win32;     //This namespace is used to work with Registry editor.
using System.Diagnostics;
using System.Web;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace SignalRbus
{
    public class SystemInfo
    {
        /***********************Method *****************/
        public InfoSystem GetOperatingSystemInfo()
        {
            //List<DataPair> ListeObject = new List<DataPair>();

            InfoSystem sys = new InfoSystem();

            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            sys.ComputerName = GetComputerName();

            foreach (ManagementObject managementObject in mos.Get())
            {
                
                if (managementObject["Caption"] != null)
                {
                     sys.OsSystem = managementObject["Caption"].ToString();
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    sys.OS_Architecture = managementObject["OSArchitecture"].ToString();
                }
                if (managementObject["Version"] != null)
                {
                    sys.OS_Version= managementObject["Version"].ToString();
                }
              
                sys.CPU_Physical_Core= ((Environment.ProcessorCount) / 2).ToString();
                sys.CPU_Logical_Core=(Environment.ProcessorCount.ToString());
            }
            return sys;
         }

        public  Performance GetPerformance()
        {
            Performance per = new Performance();
            //Dictionary<string, object> dcSystemInfoStatic = new Dictionary<string, object>();
            PerformanceCounter perfCPU = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            PerformanceCounter perfMemory = new PerformanceCounter("Memory", "Available MBytes");
            PerformanceCounter perfSystem = new PerformanceCounter("System", "System Up Time");
            per.ComputerName = GetComputerName();
            for (int i = 0; i <= 100; i++)
            {
                per.CPU = perfCPU.NextValue();
                per.RAM = perfMemory.NextValue();
                per.System_Up_Time = perfSystem.NextValue();
                //Environment.MachineName;
                System.Threading.Thread.Sleep(10);
            }
            //ManagementObjectSearcher mos = new ManagementObjectSearcher("Win32_Process'");

            ////Process currentProcess = Process.GetCurrentProcess();
            ////// Get all processes running on the local computer.
            //////Process[] localAll = Process.GetProcesses();
            ////Process[] processes = Process.GetProcesses();
            //var list = new List<Performance>();
           
            //    Process[] processCollection = Process.GetProcesses();
            //    foreach (Process p in processCollection)
            //    for (i)
            //    {
            //          per.ProcName = p.ProcessName;

            //     }
            //// Get whatever attribute for process.
            ////list.Add(new Performance { ProcName = process.ProcessName });


            return per;
        }
     
       
        public InfoNetwork GetNetworkSystemInfo()
        {
            InfoNetwork infoNet = new InfoNetwork();

            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_NetworkAdapterConfiguration WHERE  DHCPEnabled = 'true'");
            infoNet.ComputerName = GetComputerName();
            foreach (ManagementObject managementObject in mos.Get())
            {

                if (managementObject["MACAddress"] != null)
                {
                    infoNet.MAC_Adress = managementObject["MACAddress"].ToString();
                }

                if (managementObject["IPAddress"] != null)
                {

                    string[] val = (string[])managementObject["IPAddress"]; //.Cast<string>().ToArray();
                    infoNet.IP_Adress = val[0].ToString();
                }
            }
            return infoNet;
        }

        public string GetComputerName()
            {
                return Environment.MachineName;
            }

        public Pc GetNetworkSystemInfo2()
        {
            Pc pcinfo = new Pc();

            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_NetworkAdapterConfiguration WHERE  DHCPEnabled = 'TRUE'");
            pcinfo.Name = GetComputerName();
            foreach (ManagementObject managementObject in mos.Get())
            {

                if (managementObject["MACAddress"] != null)
                {
                    pcinfo.Adress_Mac = managementObject["MACAddress"].ToString();
                }

                if (managementObject["IPAddress"] != null)
                {

                    string[] val = (string[])managementObject["IPAddress"]; //.Cast<string>().ToArray();
                    pcinfo.IP = val[0].ToString();
                }
            }
            return pcinfo;

        }
    }
     
    /********************** Tables *******************/

        public class Performance
            {
                public int IdPerf { get; set; }

                public string ComputerName { get; set; }
   
                public float CPU { get; set; }

                public float RAM { get; set; }

                public float System_Up_Time { get; set; }
                
                public string ProcName { get; set; }

                public DateTime DateCreation { get; set; }

                public int Pc { get; set; }

            }
        public class InfoNetwork
        {
            public int IdInfoNet { get; set; }

            public string ComputerName { get; set; }

            public string IP_Adress { get; set; }

            public string MAC_Adress { get; set; }

            public int  Pc { get; set; }
        }
        public class InfoSystem
        {
            public int IdSystem { get; set; }
            public string ComputerName { get; set; }
            public string OsSystem { get; set; }
            public string OS_Architecture { get; set; }
            public string OS_Version { get; set; }
            public string CPU_Physical_Core { get; set; }
            public string CPU_Logical_Core { get; set; }
            public int  Pc { get; set; }
        }
        public class Pc
        {
        
            public int Id { get; set; }

       
            public string IP { get; set; }

      
            public string Adress_Mac { get; set; }

       
            public string Name { get; set; }

       
            public int Parc { get; set; }

       
            public string AD { get; set; }


            public int AppsPCs { get; set; }

         }

        public class InfoManager
        {
            public InfoManager()
            {

            }

            #region ChainedeConnection
            private string GetConnexionStringFromFile()
            {

                string _StrConnexionString = string.Empty;
                try
                {
                    string _Source = string.Empty;
                    string _Catalog = string.Empty;
                    string _Security = string.Empty;
                    string _User = string.Empty;
                    string _Password = string.Empty;
                    XmlDocument doc = new XmlDocument();
                    string _file = "";
                    try
                    {
                        _file = System.Environment.CurrentDirectory + "\\ParamConfig.xml";
                    }
                    catch { }
                    if (!File.Exists(_file))
                    {
                        return string.Empty;
                    }
                    doc.Load(_file);

                    XmlNodeList elemList = doc.GetElementsByTagName("ParamConnexion");
                    string SendMail = "";
                    XmlNodeList eltList = elemList[0].ChildNodes;
                    for (int j = 0; j < eltList.Count; j++)
                    {
                        SendMail = eltList[j].Name;
                        switch (SendMail)
                        {
                            case "Source":
                                _Source = eltList[j].InnerText;
                                break;
                            case "Catalog":
                                _Catalog = eltList[j].InnerText;
                                break;
                            case "Security":
                                _Security = eltList[j].InnerText;
                                break;
                            case "User":
                                _User = eltList[j].InnerText;
                                break;
                            case "Password":
                                _Password = eltList[j].InnerText;
                                break;
                            default:
                                string autres = eltList[j].InnerText;
                                break;
                        }
                    }
                    if (_Security == "WIN")
                    {
                        //workstation id=tsi-laptop;packet size=4096;integrated security=sspi;data source= "tsi-laptop\mssqlserver2012";persist security info=false;initial catalog=farmasi;pooling = true;connect timeout=600
                        _StrConnexionString = String.Format("data Source={0};initial catalog={1};integrated security=sspi;", _Source, _Catalog);
                    }
                    else if (_Security == "SQL")
                    {
                        //_StrConnexionString = String.Format("Data Source={0};Persist Security Info=True;Initial Catalog={1};User Id={2};Password={3}", _Source, _Catalog, _User, _Password);
                        _StrConnexionString = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};Integrated Security=false", _Source, _Catalog, _User, _Password);
                    }
                }
                catch (Exception exc)
                {
                }

                return _StrConnexionString;
            }
        #endregion

            private const string CONNECTION_STRING = "Server=DESKTOP-DDGJARM;Database=Parc_informatique; User Id=tsi;password=Pfe@2020;Integrated Security=false";

           public void SendData()
            {
            //Performance Perf = SystemInfo.GetPerformance();
            
            //Instance
            SystemInfo system = new SystemInfo();
            Performance perf = system.GetPerformance();
            InfoSystem infosys = system.GetOperatingSystemInfo();
            InfoNetwork infoNet = system.GetNetworkSystemInfo();


            Pc pcinfo = system.GetNetworkSystemInfo2();

            //string CONNECTION_STRING = GetConnexionStringFromFile();

            /******************Table InfoNetwork*****************************/

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {

                    //string Query2 = "Insert into InfoSystems (ComputerName,OS_Architecture,OsSystem,OS_Version,CPU_Physical_Core,CPU_Logical_Core) VALUES (@ComputerName,@OS_Architecture,@OsSystem,@OS_Version,@CPU_Physical_Core,@CPU_Logical_Core)";
                    string Query3 = "IF EXISTS (SELECT * FROM InfoNetworks WHERE ComputerName like @ComputerName) BEGIN Update InfoNetworks SET " +
                        "IP_Adress = @IP_Adress, MAC_Adress = @MAC_Adress WHERE ComputerName like @ComputerName END " +
                        "ELSE BEGIN Insert into InfoNetworks (ComputerName,IP_Adress,MAC_Adress)" +
                        " VALUES (@ComputerName,@IP_Adress,@MAC_Adress) END";
                    using (SqlCommand command = new SqlCommand(Query3, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ComputerName", infoNet.ComputerName);
                        command.Parameters.AddWithValue("@IP_Adress", infoNet.IP_Adress);
                        command.Parameters.AddWithValue("@MAC_Adress", infoNet.MAC_Adress);


                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
            }

            /************************table pc*********************************/

            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {

                    //string Query2 = "Insert into InfoSystems (ComputerName,OS_Architecture,OsSystem,OS_Version,CPU_Physical_Core,CPU_Logical_Core) VALUES (@ComputerName,@OS_Architecture,@OsSystem,@OS_Version,@CPU_Physical_Core,@CPU_Logical_Core)";
                    string Query4 = "IF EXISTS (SELECT * FROM Pcs WHERE Name like @Name) BEGIN Update Pcs SET " +
                        "IP = @IP, Adress_Mac = @Adress_Mac WHERE Name like @Name END " +
                        "ELSE BEGIN Insert into Pcs (IP,Adress_Mac,Name)" +
                        " VALUES (@IP,@Adress_Mac,@Name) END";
                    using (SqlCommand command = new SqlCommand(Query4, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@IP", pcinfo.IP);
                        command.Parameters.AddWithValue("@Adress_Mac", pcinfo.Adress_Mac);
                        command.Parameters.AddWithValue("@Name", pcinfo.Name);


                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
            }

            /***************Tables Performances****************************/
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING)) {

                try{
                    //string Query1 = "Insert into Performances (ComputerName,CPU,RAM,System_Up_Time) VALUES (@ComputerName,@CPU,@RAM,@System_Up_Time)";
                    string Query1 = "IF EXISTS (SELECT * FROM Performances WHERE ComputerName like @ComputerName) BEGIN Update Performances SET " +
                        "CPU = @CPU, RAM = @RAM, System_Up_Time=@System_Up_Time, ProcName=@ProcName ,DateCreation = GetDate() WHERE ComputerName like @ComputerName END " +
                        "ELSE BEGIN Insert into Performances  (ComputerName,CPU,RAM,System_Up_Time, ProcName,DateCreation)" +
                        " VALUES (@ComputerName,@CPU,@RAM,@System_Up_Time, @ProcName , GetDate()) END";
                    using (SqlCommand command = new SqlCommand(Query1, conn))
                        {
                            conn.Open();
                            command.Parameters.AddWithValue("@ComputerName", perf.ComputerName);
                            command.Parameters.AddWithValue("@CPU", perf.CPU);
                            command.Parameters.AddWithValue("@RAM", perf.RAM);
                            command.Parameters.AddWithValue("@System_Up_Time", perf.System_Up_Time);
                            command.Parameters.AddWithValue("@ProcName", perf.ProcName);

                        //command.Parameters.AddWithValue("@DateCreation", perf.DateCreation.Date);

                        command.ExecuteNonQuery();
                        }
                }
                catch (SqlException ex)
                {

                }
                    finally
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }

                    }
                }


            //string CONNECTION_STRING = GetConnexionStringFromFile();

            /******************Table InfoSystems*****************************/
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {

                    //string Query2 = "Insert into InfoSystems (ComputerName,OS_Architecture,OsSystem,OS_Version,CPU_Physical_Core,CPU_Logical_Core) VALUES (@ComputerName,@OS_Architecture,@OsSystem,@OS_Version,@CPU_Physical_Core,@CPU_Logical_Core)";
                    string Query2 = "IF EXISTS (SELECT * FROM InfoSystems WHERE ComputerName like @ComputerName) BEGIN Update InfoSystems SET " +
                        "OS_Architecture = @OS_Architecture, OsSystem = @OsSystem, OS_Version = @OS_Version,CPU_Physical_Core = @CPU_Physical_Core,CPU_Logical_Core = @CPU_Logical_Core WHERE ComputerName like @ComputerName END " +
                        "ELSE BEGIN Insert into InfoSystems (ComputerName,OS_Architecture,OsSystem,OS_Version,CPU_Physical_Core,CPU_Logical_Core)" +
                        " VALUES (@ComputerName,@OS_Architecture,@OsSystem,@OS_Version,@CPU_Physical_Core,@CPU_Logical_Core) END";
                    using (SqlCommand command = new SqlCommand(Query2, conn))
                    {
                        conn.Open();
                        command.Parameters.AddWithValue("@ComputerName", infosys.ComputerName);
                        command.Parameters.AddWithValue("@OS_Architecture", infosys.OS_Architecture);
                        command.Parameters.AddWithValue("@OsSystem", infosys.OsSystem);
                        command.Parameters.AddWithValue("@OS_Version", infosys.OS_Version);
                        command.Parameters.AddWithValue("@CPU_Physical_Core", infosys.CPU_Physical_Core);
                        command.Parameters.AddWithValue("@CPU_Logical_Core", infosys.CPU_Logical_Core);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {

                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
            }

            
        }

        }

}
