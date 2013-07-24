using System.ServiceProcess;

namespace LinkMe.Framework.Host.Service.Commands
{
    public class ServiceCommand
        : RunCommand
    {
        protected override void Run(string serviceName, string applicationRootFolder, string configurationFile)
        {
            var services = new Service[1];
            services[0] = new Service(serviceName, applicationRootFolder, configurationFile);

            // Register services with SCM. The control returns only when all the services are stopped.

            ServiceBase.Run(services);
        }
    }
}
