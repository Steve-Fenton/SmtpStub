using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;

namespace Fenton.SmtpService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [ExcludeFromCodeCoverage]
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] 
            { 
                new SmtpStub(new ConfigurationProvider()) 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
