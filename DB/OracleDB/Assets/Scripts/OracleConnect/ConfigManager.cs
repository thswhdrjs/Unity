using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OracleConnect
{
    class ConfigManager
    {
        private static string configFileName = @"C:\Users\user\Desktop\BUSAN_OracleConnect_20220125\OracleConnect\bin\Debug\Config.xml";
        public static string GetValue(params string[] args)
        {
            string result = string.Empty;
            try
            {
                XDocument xDoc = XDocument.Load(configFileName);
                result = GetNodeValue(xDoc.FirstNode as XElement, 0, args);
            }
            catch (Exception ex)
            {

                result = ex.Message;
                Console.WriteLine(result);
                result = string.Empty;
            }

            return result;
        }

        private static string GetNodeValue(XElement node, int idx, params string[] args)
        {
            string result = string.Empty;
            if (args.Length > idx + 1)
                result = GetNodeValue(node.Element(args[idx]), ++idx, args);
            else
                result = node.Element(args[idx]).Value.ToString();
            return result;
        }
    }
}
