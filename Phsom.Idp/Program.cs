// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="James Consulting, LLC">
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
    using System.Linq;
    using System.Security.Claims;

    using AutoMapper;

    using IdentityModel;

    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Mappers;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Phsom.Idp.Data;
    using Phsom.Idp.Helpers;
    using Phsom.Idp.Models;

    #endregion

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The build web host.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="IWebHost"/>.
        /// </returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<StartupDevelopment>().Build();
        }

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            
            Mapper.Initialize(
                mapper =>
                {
                    
                    mapper.CreateMap<UserDto, ApplicationUser>(MemberList.Source).ReverseMap();
                    mapper.CreateMap<UserCreationDto, ApplicationUser>(MemberList.Source).ReverseMap();
                    mapper.CreateMap<UserCreationDto, Claim[]>().ConvertUsing<UserClaimsConverter>();
                    mapper.CreateMap<Claim[], UserDto>().ConvertUsing<UserClaimsConverter>();
                });
            host.Run();
        }

        /// <summary>
        /// The initialize database.
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }

                    context.SaveChanges();
                }

                var phsomContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var logger = scope.ServiceProvider.GetService<ILogger>();

                phsomContext.Database.Migrate();

                if (!phsomContext.Roles.Any(x => x.Name.Equals("Admin")))
                {
                    phsomContext.Roles.Add(new IdentityRole<long>("Admin"));
                }

                if (!phsomContext.Roles.Any(x => x.Name.Equals("Teacher")))
                {
                    phsomContext.Roles.Add(new IdentityRole<long>("Teacher"));
                }

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var bmoon = userMgr.FindByNameAsync("bmoon").Result;
                if (bmoon == null)
                {
                    bmoon = new ApplicationUser { UserName = "bmoon" };
                    var result = userMgr.CreateAsync(bmoon, "2410").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(bmoon, new[] { new Claim(JwtClaimTypes.Role, "Admin"), new Claim(JwtClaimTypes.Name, "Bonne Moon"), new Claim(JwtClaimTypes.GivenName, "Bonne"), new Claim(JwtClaimTypes.FamilyName, "Moon"), new Claim(JwtClaimTypes.Email, "rudy@thejamesfamily.life"), new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean) }).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    logger?.LogInformation("Pastor Moon created");
                }
                else
                {
                    bmoon.Email = "rudy@thejamesfamily.life";
                    var y = userMgr.UpdateAsync(bmoon).Result;
                    logger?.LogInformation("Pastor Moon already exists");
                }
            }
        }
    }
}