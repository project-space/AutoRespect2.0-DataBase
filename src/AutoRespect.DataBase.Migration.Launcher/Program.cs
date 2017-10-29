using AutoRespect.Infrastructure.DI;
using Microsoft.Extensions.DependencyInjection;

namespace AutoRespect.DataBase.Migration.Launcher
{
    class Program
    {
        static int Main(string[] args)
        {
            var serviceProvider = DIAttributeInstaller.Install(new ServiceCollection());
            var application = serviceProvider.GetService<IApplication>();

            return application.Run().Result;
        }
    }
}
