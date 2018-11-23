#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="StartupProduction.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Idp
{
    #region includes

    using JetBrains.Annotations;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Phsom.Idp.Data;

    #endregion

    public class StartupProduction
    {
        private readonly IConfiguration configuration;

        public StartupProduction(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="env">
        /// The env.
        /// </param>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString("Phsom");
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies());
            services.AddIdentity<ApplicationUser, IdentityRole<long>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddIdentityServer().AddDeveloperSigningCredential().AddTestUsers(Config.GetUsers())
                    .AddConfigurationStore(options =>
                     {
                         options.ConfigureDbContext = builder =>
                             builder.UseSqlServer(connectionString);
                     })
                     // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(options =>
                     {
                         options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString);

                         // this enables automatic token cleanup. this is optional.
                         options.EnableTokenCleanup = true;
                         options.TokenCleanupInterval = 30;
                     });
            services.AddCors();
        }
    }
}