using System.Configuration;

namespace Fenton.SmtpService
{
    public class ConfigurationProvider
    {
        public string LoggingDirectory
        {
            get
            {
                return GetConfigurationValue("LoggingDirectory");
            }
        }

        public int ListeningPort
        {
            get
            {
                return GetConfigurationInt32("ListeningPort");
            }
        }

        public bool SaveMessages
        {
            get
            {
                return GetConfigurationBool("SaveMessages");
            }
        }

        private static int GetConfigurationInt32(string key)
        {
            int value;
            if (int.TryParse(GetConfigurationValue(key), out value))
            {
                return value;
            }
            return 0;
        }

        private static bool GetConfigurationBool(string key)
        {
            bool value;
            if (bool.TryParse(GetConfigurationValue(key), out value))
            {
                return value;
            }
            return false;
        }

        private static string GetConfigurationValue(string key)
        {
            return (ConfigurationManager.AppSettings[key] != null)
                ? ConfigurationManager.AppSettings[key]
                : string.Empty;
        }

    }
}
