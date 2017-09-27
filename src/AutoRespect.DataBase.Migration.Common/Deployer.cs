using DbUp;
using System;
using System.Reflection;

namespace AutoRespect.DataBase.Migration.Common
{
    public abstract class Deployer
    {
        protected readonly string connectionString;
        protected readonly Assembly assemblyWithScripts;

        public Deployer (string connectionString, Assembly assemblyWithScripts)
        {
            this.connectionString = connectionString;
            this.assemblyWithScripts = assemblyWithScripts;

        }

        public void Run()
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
