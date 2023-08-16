using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace OracleConnect
{
    public class DBManager
    {
        public static DBManager instance = null;

        QueryString query;

        OracleDBManager dbManager_tsb;
        OracleDBManager dbManager_klnet;
        OracleDBManager dbManager_gmt;
        OracleDBManager dbManager_kesti;
        OracleDBManager dbManager_sju;

        public DBManager()
        {
            if (instance == null)
                instance = this;

            query = new QueryString();

            dbManager_tsb = new OracleDBManager(); // 2세부
            dbManager_klnet = new OracleDBManager();
            dbManager_gmt = new OracleDBManager();
            dbManager_kesti = new OracleDBManager(); // 환경과학기술
            dbManager_sju = new OracleDBManager();

            #region 데이터베이스연결

            Console.WriteLine("TSB 데이터 베이스 연결 중...");
            if (dbManager_tsb.GetConnection(1) == false)
            {
                Console.WriteLine("TSB 데이터 베이스 접속 연결 실패!!!!!");
                return;
            }
            Console.WriteLine("TSB 데이터 베이스 접속 성공!!!");

            Console.WriteLine("kinet 데이터 베이스 연결 중...");
            if (dbManager_klnet.GetConnection(2) == false)
            {
                Console.WriteLine("kinet 데이터 베이스 접속 연결 실패!!!!!");
                return;
            }
            Console.WriteLine("kinet데이터 베이스 접속 성공!!!");

            Console.WriteLine("gmt 데이터 베이스 연결 중...");
            if (dbManager_gmt.GetConnection(3) == false)
            {
                Console.WriteLine("데이터 베이스 접속 연결 실패!!!!!");
                return;
            }
            Console.WriteLine("gmt 데이터 베이스 접속 성공!!!");

            Console.WriteLine("kesti 데이터 베이스 연결 중...");
            if (dbManager_tsb.GetConnection(4) == false)
            {
                Console.WriteLine("kesti 데이터 베이스 접속 연결 실패!!!!!");
                return;
            }
            Console.WriteLine("kesti 데이터 베이스 접속 성공!!!");

            Console.WriteLine("sju 데이터 베이스 연결 중...");
            if (dbManager_tsb.GetConnection(5) == false)
            {
                Console.WriteLine("sju 데이터 베이스 접속 연결 실패!!!!!");
                return;
            }
            Console.WriteLine("sju 데이터 베이스 접속 성공!!!");
            #endregion

            // 날씨 데이터 샘플 코드
            //dbManager_kesti.GetDataFromDB(sql_main_weather, out result);
            //dbManager_tsb.GetDataFromDB(containerInfoFromID, out result);
            //while (result.Read())
            //{
            //    Console.WriteLine(result["CNTR_NO"].ToString()
            //        + ", " + result["VSL_CD"].ToString()
            //        + ", " + result["VOYAGE"].ToString()
            //        + ", " + result["FE"].ToString()
            //        + ", " + result["WEIGHT"].ToString()
            //        + ", " + result["CARGO_TYPE"].ToString()
            //      );
            //    //Console.WriteLine(result["GAUGE_SO2"].ToString()
            //    //    + ", " + result["GAUGE_NO2"].ToString()
            //    //    + ", " + result["GAUGE_03"].ToString()
            //    //    + ", " + result["GAUGE_PM10"].ToString()
            //    //    + ", " + result["GAUGE_PM25"].ToString()
            //    //  );
            //}

            //if (result_dataset.Tables.Count > 0)
            //{
            //    DataRow r = result_dataset.Tables[0].Rows[0];
            //    Console.WriteLine(r["CNTR_NO"] + "," + r["VOYAGE"] + "," + r["CARGO_TYPE"]);

            //    //foreach (DataRow r in result_dataset.Tables[0].Rows)
            //    //{
            //    //    Console.WriteLine(r["CNTR_NO"] + "," + r["VOYAGE"] +"," + r["CARGO_TYPE"]);
            //    //}
            //}
        }

        public OracleDataReader Get_ContainerInfoByID(string _containerID)
        {
            OracleDataReader result;

            dbManager_tsb.GetDataFromDB(string.Format(query.containerInfoFromID, _containerID), out result);

            return result;
        }

        public OracleDataReader Get_YardTruckInfoByID(string _truckID)
        {
            OracleDataReader result;

            dbManager_tsb.GetDataFromDB(string.Format(query.yardTruckInfoFromID, _truckID), out result);

            return result;
        }

        public OracleDataReader Get_WeatherInfo()
        {
            OracleDataReader result;

            dbManager_gmt.GetDataFromDB(query.weatherInfo, out result);

            return result;
        }

        public OracleDataReader Get_TruckInfoByDate(string _date)
        {
            OracleDataReader result;

            dbManager_klnet.GetDataFromDB(string.Format(query.dt3TruckInfoByDate, _date), out result);

            return result;
        }

        public OracleDataReader Get_TruckInfoByCAR_CB(string _CAR_CD)
        {
            OracleDataReader result;

            dbManager_klnet.GetDataFromDB(string.Format(query.dt3TruckInfoByCAR_CD, _CAR_CD), out result);

            return result;
        }

        public OracleDataReader Get_TruckDetailInfoByCAR_CB(string _CAR_CD)
        {
            OracleDataReader result;

            dbManager_klnet.GetDataFromDB(string.Format(query.dt3TruckDetailInfoByCAR_CD, _CAR_CD), out result);

            return result;
        }


    }
}
