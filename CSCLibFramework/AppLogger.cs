using Newtonsoft.Json;
using Serilog;
using System;

namespace CSCLibFramework
{
    public class AppLogger
    {
        private readonly ILogger logger;

        public AppLogger(ILogger logger)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.logger = logger;
        }

        public void Log(object value) => logger.Debug("{@value}", value);

        public void LogAsJson(object value)
        {
            var json = JsonConvert.SerializeObject(value);

            logger.Debug("{json}", json);
        }
    }

    /*
     new LoggerConfiguration()
        .WriteTo
            .Debug(LogEventLevel.Debug)
        .CreateLogger();
     */
}
