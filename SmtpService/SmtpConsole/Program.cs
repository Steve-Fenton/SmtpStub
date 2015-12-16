using Fenton.SmtpService;
using System;

namespace Fenton.SmtpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configurationProvider = new ConfigurationProvider();
                var service = new SmtpStub(configurationProvider);
                service.Init();
                Console.WriteLine("SMTP Stub listening on port " + configurationProvider.ListeningPort);
                Console.WriteLine("A (.) will print for each message received.");
                Console.WriteLine("Message dumps are placed in " + configurationProvider.LoggingDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
