using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoRespect.Infrastructure.DI.Design;
using AutoRespect.Infrastructure.DI.Design.Attributes;
using AutoRespect.Infrastructure.ServiceDiscovery.Design.Db;
using DbUp;

namespace AutoRespect.DataBase.Migration.Launcher
{
    public interface IApplication
    {
        Task<int> Run();
    }

    [DI(LifeCycle.Singleton)]
    public class Application : IApplication
    {
        private readonly IDbConnectionStringGetter connectionStringGetter;

        public Application(IDbConnectionStringGetter connectionStringGetter)
        {
            this.connectionStringGetter = connectionStringGetter;
        }

        public async Task<int> Run()
        {
            var identityServerConnectionString = await connectionStringGetter.Get(DbType.IdentityServer);
            var resourceServerConnectionString = await connectionStringGetter.Get(DbType.ResourceServer);

            var identityServerAssembly = Assembly.Load("AutoRespect.DataBase.Migration.IdentityServer");
            var resourceServerAssembly = Assembly.Load("AutoRespect.DataBase.Migration.ResourceServer");

            RunMigration(identityServerAssembly, identityServerConnectionString);
            RunMigration(resourceServerAssembly, resourceServerConnectionString);

            return 0;
        }

        private void RunMigration(Assembly assemblyWithScripts, string connectionString)
        {
            EnsureDatabase.For.SqlDatabase(connectionString);

            var migrator =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(assemblyWithScripts)
                    .LogToConsole()
                    .Build();

            if (!migrator.TryConnect(out string error))
            {
                Console.WriteLine($"Establishment connection failed by reason: {error}");
#if DEBUG
                Console.ReadLine();
#endif
            }
            else
            {
                var migrationResult = migrator.PerformUpgrade();
                if (migrationResult.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Migration rolling successful");
                    Console.ResetColor();
#if DEBUG
                    Console.ReadLine();
#endif
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(migrationResult.Error);
                    Console.ResetColor();

#if DEBUG
                    Console.ReadLine();
#endif
                }
            }
        }
    }
}
