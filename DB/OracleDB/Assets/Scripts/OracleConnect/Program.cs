using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OracleConnect;
using System.Net;
using System.Data;
using static OracleConnect.Data;
using System.Net.Sockets;

namespace OracleConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            DBManager dbManager = new DBManager();

            WebSocketServerManager webSoketMgr = new WebSocketServerManager();
            webSoketMgr.StartServer();

            Console.ReadKey();

            webSoketMgr.StopServer();
        }
    }
}
