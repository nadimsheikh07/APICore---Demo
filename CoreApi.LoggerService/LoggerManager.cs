using Serilog;

namespace CoreApi.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = Log.ForContext(typeof(LoggerManager));

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Information(message);
        }

        public void LogWarn(string message)
        {
            logger.Warning(message);
        }
    }
}
