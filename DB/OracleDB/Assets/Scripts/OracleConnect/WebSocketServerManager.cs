using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnect
{
    public class WebSocketServerManager
    {
        public static WebSocketServerManager instance = null;

        string ip = "127.0.0.1";
        int port = 57580;

        WebSocketServer wss;

        public WebSocketSessionManager webSocketSessionManager;
        public List<WebSession> webSessions;

        public WebSocketServerManager()
        {
            if (instance == null)
                instance = this;

            webSessions = new List<WebSession>();
        }

        public void StartServer()
        {
            wss = new WebSocketServer(IPAddress.Parse(ip), port);
            wss.AddWebSocketService<WebSession>("/WebServer");

            webSocketSessionManager = wss.WebSocketServices["/WebServer"].Sessions;

            try
            {
                wss.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.HResult);
                Console.WriteLine(e.Message);

                throw new InvalidOperationException(e.Message, e);
            }

            Console.WriteLine("Web Socket Server Start!!");
        }

        public void StopServer()
        {
            Console.WriteLine("Web Socket Server Close!!");
            webSocketSessionManager = null;
            webSessions.Clear();
            wss.Stop();
        }

        public void Add_WebSession(WebSession _web)
        {
            if (!webSessions.Contains(_web))
                webSessions.Add(_web);

            Console.WriteLine($"Add_WebServer : {webSessions.Count}");
        }

        public void Remove_WebSession(WebSession _web)
        {
            if (webSessions.Contains(_web))
                webSessions.Remove(_web);

            Console.WriteLine($"Remove_Webserver : {webSessions.Count}");
        }

        public void BroadCast_Packet(string _data)
        {
            if (webSocketSessionManager != null)
                webSocketSessionManager.Broadcast(_data);
        }

        public string PacketProcessing(string _data)
        {
            string retval = string.Empty;

            JObject jObject = JObject.Parse(_data);

            switch (jObject["protocol"].ToString())
            {
                case "CS_Get_Weather":
                    retval = ProcessCS_Get_Weather(JsonConvert.DeserializeObject<CS_Get_Weather>(_data));
                    break;
                case "CS_GetContainerInfoByID":
                    retval = ProcessCS_GetContainerInfoByID(JsonConvert.DeserializeObject<CS_GetContainerInfoByID>(_data));
                    break;
                case "CS_Get_YardTruckInfoByID":
                    retval = ProcessCS_Get_YardTruckInfoByID(JsonConvert.DeserializeObject<CS_Get_YardTruckInfoByID>(_data));
                    break;
                case "CS_Get_TruckInfoByDate":
                    retval = ProcessCS_Get_TruckInfoByDate(JsonConvert.DeserializeObject<CS_Get_TruckInfoByDate>(_data));
                    break;
                case "CS_Get_TruckInfoByCAR_CD":
                    retval = ProcessCS_Get_TruckInfoByCAR_CD(JsonConvert.DeserializeObject<CS_Get_TruckInfoByCAR_CD>(_data));
                    break;
                case "CS_Get_TruckDetailInfoByCAR_CD":
                    retval = ProcessCS_Get_TruckDetailInfoByCAR_CD(JsonConvert.DeserializeObject<CS_Get_TruckDetailInfoByCAR_CD>(_data));
                    break;
            }

            return retval;
        }

        public string ProcessCS_Get_Weather(CS_Get_Weather _packet)
        {
            Console.WriteLine("ProcessCS_Get_Weather");

            OracleDataReader dataReader = DBManager.instance.Get_WeatherInfo();

            SC_Get_Weather packet = new SC_Get_Weather();

            while (dataReader.Read())
            {
                packet.FCST_DT1 = dataReader["FCST_DT1"].ToString();
                packet.FCST_DT = dataReader["FCST_DT"].ToString();
                packet.FCST_TIME = dataReader["FCST_TIME"].ToString();
                packet.FCST_DT_MAIN = dataReader["FCST_DT_MAIN"].ToString();
                packet.F_SKY = dataReader["F_SKY"].ToString();
                packet.F_SKY_NAME = dataReader["F_SKY_NAME"].ToString();
                packet.F_TMP = dataReader["F_TMP"].ToString();
                packet.F_POP = dataReader["F_POP"].ToString();
                packet.F_PTY = dataReader["F_PTY"].ToString();
                packet.F_PTY_NAME = dataReader["F_PTY_NAME"].ToString();
                packet.F_PCP = dataReader["F_PCP"].ToString();
                packet.F_VEC = dataReader["F_VEC"].ToString();
                packet.F_WSD = dataReader["F_WSD"].ToString();
                packet.F_WAV = dataReader["F_WAV"].ToString();
                packet.AREA_NM = dataReader["AREA_NM"].ToString();

                //Console.WriteLine(packet.FCST_DT_MAIN);
            }

            return JsonConvert.SerializeObject(packet);
        }

        public string ProcessCS_GetContainerInfoByID(CS_GetContainerInfoByID _packet)
        {
            Console.WriteLine("ProcessCS_GetContainerInfoByID");

            OracleDataReader dataReader = DBManager.instance.Get_ContainerInfoByID(_packet.containerID);

            SC_GetContainerInfoByID packet = new SC_GetContainerInfoByID();

            if (dataReader.Read())
            {
                packet.CNTR_NO = dataReader["CNTR_NO"].ToString();
                packet.VSL_CD = dataReader["VSL_CD"].ToString();
                packet.VOYAGE = dataReader["VOYAGE"].ToString();
                packet.FE = dataReader["FE"].ToString();
                packet.WEIGHT = dataReader["WEIGHT"].ToString();
                packet.CARGO_TYPE = dataReader["CARGO_TYPE"].ToString();
                packet.IN_DATE = dataReader["IN_DATE"].ToString();
                packet.OUT_DATE = dataReader["OUT_DATE"].ToString();
            }

            return JsonConvert.SerializeObject(packet);
        }

        public string ProcessCS_Get_YardTruckInfoByID(CS_Get_YardTruckInfoByID _packet)
        {
            Console.WriteLine("ProcessCS_GetYardTruckInfoByID");

            OracleDataReader dataReader = DBManager.instance.Get_YardTruckInfoByID(_packet.yardTruckID);

            SC_Get_YardTruckInfoByID packet = new SC_Get_YardTruckInfoByID();

            if (dataReader.Read())
            {
                packet.EQU_NO = dataReader["EQU_NO"].ToString();
                packet.LATITUDE = dataReader["LATITUDE"].ToString();
                packet.LONGITUDE = dataReader["LONGITUDE"].ToString();
                packet.ALTITUDE = dataReader["ALTITUDE"].ToString();
                packet.GPS_TIME = dataReader["GPS_TIME"].ToString();
                packet.FAULTCD = dataReader["FAULTCD"].ToString();
                packet.USER_ID = dataReader["USER_ID"].ToString();
                packet.UPDATE_TIME = dataReader["UPDATE_TIME"].ToString();
            }

            return JsonConvert.SerializeObject(packet);
        }

        public string ProcessCS_Get_TruckInfoByDate(CS_Get_TruckInfoByDate _packet)
        {
            Console.WriteLine("ProcessCS_Get_TruckInfoByDate");

            OracleDataReader dataReader = DBManager.instance.Get_TruckInfoByDate(_packet.date);

            SC_Get_TruckInfoByDate packet = new SC_Get_TruckInfoByDate();

            DT3TruckInfo info;

            while (dataReader.Read())
            {
                info = new DT3TruckInfo();

                info.CAR_CD = dataReader["CAR_CD"].ToString();
                info.DISTANCE = dataReader["DISTANCE"].ToString();
                info.ARRIVE_DATE = dataReader["ARRIVE_DATE"].ToString();

                packet.Add_Info(info);
            }

            return JsonConvert.SerializeObject(packet);
        }

        public string ProcessCS_Get_TruckInfoByCAR_CD(CS_Get_TruckInfoByCAR_CD _packet)
        {
            Console.WriteLine("ProcessCS_Get_TruckInfoByCAR_CD");

            OracleDataReader dataReader = DBManager.instance.Get_TruckInfoByCAR_CB(_packet.CAR_CD);

            SC_Get_TruckInfoByCAR_CD packet = new SC_Get_TruckInfoByCAR_CD();

            if (dataReader.Read())
            {
                packet.CAR_CD = dataReader["CAR_CD"].ToString();
                packet.DISTANCE = dataReader["DISTANCE"].ToString();
                packet.ARRIVE_DATE = dataReader["ARRIVE_DATE"].ToString();
            }

            return JsonConvert.SerializeObject(packet);
        }

        public string ProcessCS_Get_TruckDetailInfoByCAR_CD(CS_Get_TruckDetailInfoByCAR_CD _packet)
        {
            Console.WriteLine("ProcessCS_Get_TruckDetailInfoByCAR_CD");

            OracleDataReader dataReader = DBManager.instance.Get_TruckDetailInfoByCAR_CB(_packet.CAR_CD);

            SC_Get_TruckDetailInfoByCAR_CD packet = new SC_Get_TruckDetailInfoByCAR_CD();

            if (dataReader.Read())
            {
                packet.CAR_CD = dataReader["CAR_CD"].ToString();
                packet.VSL = dataReader["VSL"].ToString();
                packet.VOY = dataReader["VOY"].ToString();
                packet.CNTR_NO = dataReader["CNTR_NO"].ToString();
                packet.TYPE_SIZE = dataReader["TYPE_SIZE"].ToString();
            }

            return JsonConvert.SerializeObject(packet);
        }
    }
}
