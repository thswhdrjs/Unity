using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net;
using System.IO;

namespace OracleConnect
{
    public sealed class OracleDBManager
    {
        public string LastExceptionString = string.Empty;
        public string ConnectionString = string.Empty;
        public string Address = string.Empty;
        public string Port = string.Empty;

        private OracleCommand LastExecutedCommand = null;
        private int RetryCnt = 0;

        public OracleConnection Connection { get; private set; }

        public bool GetConnection(int select)
        {
            try
            {
                if (this.Connection != null)
                {
                    this.Connection.Close();
                    this.Connection.Dispose();
                    this.Connection = null;
                }

                if (ConnectionString == string.Empty)
                {
                    SetConnectionString(select);
                }
                Console.WriteLine("========" + ConnectionString);
                Connection = new OracleConnection(ConnectionString);

                if (this.Address != string.Empty) //주소가 없을 경우 그냥 리턴
                {
                    Connection.Open();
                }
            }

            catch (Exception ex)
            {
                System.Reflection.MemberInfo info = System.Reflection.MethodInfo.GetCurrentMethod();

                string id = string.Format("{0}.{1}", info.ReflectedType.Name, info.Name);

                return false;
            }

            if (Connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        //public void SetDataFromDB(string sql)
        //{
        //    OracleDataAdapter adapter = new OracleDataAdapter(sql, Connection);

        //    // OracleCommand cmd = new OracleCommand();
        //    //cmd.Connetcion = Connection;

        //}

        public void GetDataFromDB(string sql, out OracleDataReader result)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection = Connection;
            cmd.CommandText = sql;
            OracleDataReader reader = cmd.ExecuteReader();
            result = reader;
        }

        public void SaveDataFromDB(string sql, string filename)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection = Connection;
            cmd.CommandText = sql;

            DataSet dataSet = new DataSet();

            using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
            {
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataSet);
            }

            var lines = dataSet.Tables[0].Rows.Cast<DataRow>().Select(p => string.Join(",", p.ItemArray));
            File.WriteAllLines("d:/" + filename + ".csv", lines);
        }

        public DataSet DataSetFromDB(string sql)
        {
            OracleCommand cmd = new OracleCommand(sql, Connection);
            cmd.Connection = Connection;
            cmd.CommandText = sql;

            DataSet dataSet = new DataSet();

            using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
            {
                dataAdapter.SelectCommand = cmd;
                dataAdapter.Fill(dataSet);
            }

            return dataSet;
        }

        #region private
        private void SetConnectionString(int select)
        {
            string user = string.Empty;
            string pwd = string.Empty; ;
            switch(select)
            {
                case 1:
                    user = ConfigManager.GetValue("DATABASE", "USER_TSB");
                    pwd = ConfigManager.GetValue("DATABASE", "PWD_TSB");
                    break;
                case 2:
                    user = ConfigManager.GetValue("DATABASE", "USER_KINET");
                    pwd = ConfigManager.GetValue("DATABASE", "PWD_KINET");
                    break;
                case 3:
                    user = ConfigManager.GetValue("DATABASE", "USER_GMT");
                    pwd = ConfigManager.GetValue("DATABASE", "PWD_GMT");
                    break;
                case 4:
                    user = ConfigManager.GetValue("DATABASE", "USER_KESTI");
                    pwd = ConfigManager.GetValue("DATABASE", "PWD_KESTI");
                    break;
                case 5:
                    user = ConfigManager.GetValue("DATABASE", "USER_SJU");
                    pwd = ConfigManager.GetValue("DATABASE", "PWD_SJU");
                    break;
            }
             
            string port = ConfigManager.GetValue("DATABASE", "PORT");
            string sid = ConfigManager.GetValue("DATABASE", "SID");
            string svr = ConfigManager.GetValue("DATABASE", "SERVICE_NAME");
            string addr01 = ConfigManager.GetValue("DATABASE", "D_ADDR01");
            string addr02 = ConfigManager.GetValue("DATABASE", "D_ADDR02");
            string address01 = string.Format("(ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))", addr01, port);
            string address02 = string.Format("(ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))", addr02, port);
            string dataSource = string.Format(@"(DESCRIPTION =(ADDRESS_LIST ={0}{1})(CONNECT_DATA =(", address01, address02);


            dataSource += svr == string.Empty ? string.Format("SID = {0})))", sid) :
            string.Format("SERVICE_NAME = {0})))", svr);

            this.Address = addr01;
            this.Port = port;
            this.ConnectionString = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + dataSource;
        }


        private bool CheckDBConnected()
        {
            string query = "SELECT 1 FROM DUAL";
            OracleDataReader result = null;
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = this.Connection;
                cmd.CommandText = query;
                result = cmd.ExecuteReader();
            }
            catch { }
            if (result != null && result.HasRows)
                return true;
            return false;
        }
        #endregion private

    }
}

