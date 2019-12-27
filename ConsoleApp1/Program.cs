using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paint.Data;
using Paint.Domain;
using ProjectManager;
using RandREng.Common;
using RandREng.Paging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        const string SourceDir = @"C:\Users\rober\OneDrive\Documents\Project Awards\";

        public static IConfigurationRoot Configuration { get; set; }
        public static ServiceProvider ServiceProvider { get; set; }
        static void Configure()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower() == "development";
            //Determines the working environment as IHostingEnvironment is unavailable in a console app

            var builder = new ConfigurationBuilder();
            // tell the builder to look for the appsettings.json file
            builder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //only add secrets in development
            if (isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            //Map the implementations of your classes here ready for DI
            services
                //                .Configure<SecretStuff>(Configuration.GetSection(nameof(SecretStuff)))
                .AddOptions()
                .AddLogging()
                //                .AddSingleton<ISecretRevealer, SecretRevealer>()
                .BuildServiceProvider();

            services.AddDbContext<Context>(options =>
            {
                options.ConfigureFromSettings<Context>(Configuration, "PaintDb");
            });

            ServiceProvider = services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            Configure();
            using (Context ctx = ServiceProvider.GetService<Context>())
            {
                DirectoryInfo dir = new DirectoryInfo(SourceDir);
                ProjectAward projcect = new ProjectAward(ctx);
                foreach (FileInfo fi in dir.GetFiles("*.xlsx"))
                {
                    projcect.Process(fi.FullName);
                }
            }

        }


        static void Main2(string[] args)
        {
            Configure();
            Populate();
            using (Context ctx = ServiceProvider.GetService<Context>())
            {
                int page = 1;
                int pageSize = 100;
                PagedResult<Job> jobs;

                do
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    List<int> ids = new List<int> { 1, 2 };
                    jobs = ctx.Jobs
                        .Where(j => ids.Contains(j.ClientId))
                        .OrderBy(j => j.Id)
                        .GetPaged<Job>(page, pageSize);
                    stopwatch.Stop();
                    System.Console.WriteLine($"{page} - {stopwatch.ElapsedMilliseconds}");
                    page += 1;
                } while (jobs.PageSize == jobs.Results.Count());

            }
        }

        const int JOB_COUNT = 10000;

        static void Populate()
        {
            using (Context ctx = ServiceProvider.GetService<Context>())
            {
                ctx.Database.EnsureCreated();
                //ctx.ChangeTracker.Tracked += ChangeTracker_Tracked;
                //ctx.ChangeTracker.StateChanged += ChangeTracker_StateChanged;

                //                Client client = ctx.Clients.AsTracking().FirstOrDefault(c => c.Id == 2);
                int start = ctx.Jobs.Count() + 1;
                for (int i = start; i <= JOB_COUNT; i++)
                {
                    Job job1 = new Job();
                    job1.Address = new Address();
                    job1.Address.Line1 = $"{i} MadeUp Way";
                    job1.Address.City = "Atlanta";
                    job1.Address.State = "GA";
                    job1.Address.ZipCode = "12345";
                    job1.ClientId = 2;
                    ctx.Jobs.Add(job1);
                    if (i % 1000 == 0)
                    {
                        int y = ctx.SaveChanges();
                        System.Console.WriteLine($"{i} - Created");
                    }
                }
                int x = ctx.SaveChanges();
            }
        }

        private static void ChangeTracker_StateChanged(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
        {
            System.Console.WriteLine($"StateChanged - {e.Entry.Entity.GetType().Name}");
        }

        private static void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            System.Console.WriteLine($"Tracked - {e.Entry.Entity.GetType().Name}");
        }
    }

    public class Database
    {
        public Database(Context ctx)
        {

        }
    }
}
