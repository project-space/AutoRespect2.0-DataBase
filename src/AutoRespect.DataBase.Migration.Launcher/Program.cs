using AutoRespect.DataBase.Migration.Common;
using AutoRespect.DataBase.Migration.IdentityServer;
using AutoRespect.DataBase.Migration.ResourceServer;

namespace AutoRespect.DataBase.Migration.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var deployers = new Deployer [] {
                new IdentityServerDeployer(),
                new ResourceServerDeployer()
            };

            foreach (var deployer in deployers)
            {
                deployer.Run();
            }
        }
    }
}
