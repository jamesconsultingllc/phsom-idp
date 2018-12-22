// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartupDevelopment.cs" company="">
//   
// </copyright>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="StartupDevelopment.cs" company="James Consulting, LLC">
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

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using JetBrains.Annotations;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Net.Http.Headers;

    using Phsom.Idp.Data;

    using Swashbuckle.AspNetCore.Swagger;

    #endregion

    /// <summary>
    ///     The startup.
    /// </summary>
    [UsedImplicitly]
    public class StartupDevelopment
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupDevelopment"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public StartupDevelopment(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Identity API V1");
            });

            app.UseMvcWithDefaultRoute();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        ///     For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = GetType().GetTypeInfo().Assembly.GetName().Name;
            var connectionString = configuration.GetConnectionString("Phsom");
            services.AddMvc(
                options =>
                {
                    var jsonInputFormatter = options.InputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();

                    if (jsonInputFormatter != null)
                    {
                        jsonInputFormatter.SupportedMediaTypes.Add(MediaTypes.User);
                        jsonInputFormatter.SupportedMediaTypes.Add(MediaTypes.UserCreation);
                    }

                    var jsonOutputFormatter = options.InputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

                    jsonOutputFormatter?.SupportedMediaTypes.Add(MediaTypes.User);

                });

            services.AddCors();
            services.AddSwaggerGen(config => { config.SwaggerDoc("v1", new Info { Title = "Identity API", Version = "v1" }); });
            services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddIdentity<ApplicationUser, IdentityRole<long>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(
                options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    options.User.RequireUniqueEmail = false;
                });

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(options => { options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)); })

                     // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(
                         options =>
                         {
                             options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));

                             // this enables automatic token cleanup. this is optional.
                             options.EnableTokenCleanup = true;
                             options.TokenCleanupInterval = 30;
                         });

        }
    }
}