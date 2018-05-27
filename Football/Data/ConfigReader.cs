using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Football.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigReader
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> configurationFile = new Dictionary<string, string>();

        private ConfigReader()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configFile">string</param>
        public static void Init(string configFile)
        {
            StreamReader fs = new StreamReader(configFile);
            while (fs.Peek() != -1)
            {
               
                string [] configLine = fs.ReadLine().Split('=');
                configurationFile.Add(configLine[0],configLine[1]);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">string</param>
        /// <returns>string</returns>
        public static string GetConfigurationValue(string key)
        {
            if(configurationFile.ContainsKey(key))
                return configurationFile[key];
            return string.Empty;
        }
    }
}
