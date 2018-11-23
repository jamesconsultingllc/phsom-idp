namespace Phsom.Api
{
    using System;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    using Sentry;

    using SharpRaven;
    using SharpRaven.Data;

    using SentryEvent = SharpRaven.Data.SentryEvent;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSentry()
                .Build();
    }
}
