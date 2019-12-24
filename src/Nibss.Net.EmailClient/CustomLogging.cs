

using log4net;
using System.IO;

namespace Nibss.Net.EmailClient
{
    public static class CustomLogging
    {

        private static ILog _log = null;
        private static string _logFile = null;

        public static void Initialize(string ApplicationPath)
        {
            _logFile = Path.Combine(ApplicationPath, "App_Data", "Api.log");

            GlobalContext.Properties["LogFileName"] = _logFile;

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(ApplicationPath, "Log4Net.config")));

            _log = LogManager.GetLogger("Nibss.EmailClientWeb");
        }

        public static string LogFile
        {
            get { return _logFile; }
        }

        public enum TracingLevel
        {
            ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF
        }

        public static void LogMessage(TracingLevel Level, string Message)
        {
            switch (Level)
            {
                case TracingLevel.DEBUG:
                    _log.Debug(Message);
                    break;

                case TracingLevel.INFO:
                    _log.Info(Message);
                    break;

                case TracingLevel.WARN:
                    _log.Warn(Message);
                    break;

                case TracingLevel.ERROR:
                    _log.Error(Message);
                    break;

                case TracingLevel.FATAL:
                    _log.Fatal(Message);
                    break;
            }
        }
    }


    public static class Logger
    {

        public static ILog Log { get; set; }

        static Logger()
        {
            Log = LogManager.GetLogger(typeof(Logger));
        }
        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, System.Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void Info(object msg)
        {
            Log.Info(msg);
        }
    }
}
