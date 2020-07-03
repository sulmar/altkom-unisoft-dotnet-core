using System;
using System.IO;
using Topshelf;

namespace Altkom.UniSoft.WindowsService
{

    // dotnet add package Topshelf
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(s =>
            {
                s.Service<LoggingService>();
                s.SetServiceName("Altkom Service");
                s.StartAutomatically();                
            });
        }
    }

    public class LoggingService : ServiceControl
    {
        private const string filename = @"c:\temp\log.txt";

        private void Log(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            File.AppendAllText(filename, $"{message} {DateTime.UtcNow} \n");
        }

        public bool Start(HostControl hostControl)
        {
            Log("Started!");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log("Stopped.");
            return true;
        }
    }
}
