using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Support.Utils
{
    public static class LoggerUtils
    {
        private const string LOGGER_CONFIG_FILENAME = "log4net.config";
        public static void LoadLoggerConfig()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(LOGGER_CONFIG_FILENAME));
        }
    }
}