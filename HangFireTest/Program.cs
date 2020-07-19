using Hangfire;
using Hangfire.SqlServer;
using System;

namespace HangFireTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseColouredConsoleLogProvider()
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(@"Data Source=.\sqlexpress;Initial Catalog=HangFireTestDb;Integrated Security=True", new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   UsePageLocksOnDequeue = true,
                   DisableGlobalLocks = true
               });

            BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));
            BackgroundJob.Schedule(() =>  Console.WriteLine("Delayed") ,TimeSpan.FromSeconds(10));

            using (var server = new BackgroundJobServer())
            {
                Console.ReadLine();
            }
        }
    }
}
