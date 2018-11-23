#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api
{
    #region includes

    using System.Linq;

    using AutoMapper;

    using JetBrains.Annotations;

    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Phsom.Api.Configuration;
    using Phsom.Api.Data;

    #endregion

    /// <summary>
    ///     The startup.
    /// </summary>
    [UsedImplicitly]
    public class Startup
    {
        /// <summary>
        ///     The configuration.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">
        ///     The configuration.
        /// </param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">
        ///     The app.
        /// </param>
        /// <param name="env">
        ///     The env.
        /// </param>
        /// <param name="modelBuilder">
        ///     The model builder
        /// </param>
        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PhsomModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routeBuilder => { routeBuilder.MapODataServiceRoute("ODataRoutes", "odata", modelBuilder.GetEdmModel(app.ApplicationServices)); });
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">
        ///     The services.
        /// </param>
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PhsomContext>(options => options.UseInMemoryDatabase(databaseName:"test"));
            services.AddIdentity<PhsomUser, IdentityRole>().AddEntityFrameworkStores<PhsomContext>().AddDefaultTokenProviders();
            
            services.AddMvcCore(
                setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;

                    var jsonOutputFormatter = setupAction.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();

                    if (jsonOutputFormatter != null)
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.phsom.student+json");
                        jsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.phsom.gridstudent+json");
                    }
                });

            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            services.AddOData();
            services.AddTransient<PhsomModelBuilder>();
        }
    }
}