using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
namespace LoggingSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ILoggerFactory loggerFactory=new LoggerFactory();
            ConsoleLoggerExtensions
            ILogger logger = loggerFactory.CreateLogger<Program>();
            logger.LogWarning(new EventId(4004),"this is debug");
            Console.WriteLine("Hello World!");
        }
    }
}
