using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LoggingSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddNLog().AddEventSourceLogger().AddDebug().AddNLog();

            while (true)
            {
                ILogger logger1 = loggerFactory.CreateLogger<Program>();

                ILogger logger2 = loggerFactory.CreateLogger("Zerodo");

                logger1.LogTrace("this is trace {date}", DateTime.Now);
                logger1.LogDebug(new EventId(4004), "this is debug");
                logger1.LogInformation("this is info");
                logger2.LogWarning("this is warning");
                logger2.LogError("this is error");
                logger2.LogCritical("this critical");

                ILogger logger3 = loggerFactory.CreateLogger("groupDemo");

                using (logger3.BeginScope("this is group log"))
                {
                    int a = 123;
                    logger2.LogTrace("a={a}", a);
                    int b = 321;
                    logger2.LogTrace("b={b}", b);
                    int c = a + b;
                    logger2.LogTrace("c={c}", c);
                }

                Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
