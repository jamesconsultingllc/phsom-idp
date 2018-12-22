#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="SeedData.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Idp.Data
{
    #region includes

    using System;
    using System.Linq;
    using System.Security.Claims;

    using IdentityModel;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    /// The seed data.
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// The ensure seed data.
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var logger = scope.ServiceProvider.GetService<ILogger>();

                context.Database.Migrate();

                if (!context.Roles.Any(x => x.Name.Equals("Admin")))
                {
                    context.Roles.Add(new IdentityRole<long>("Admin"));
                }

                if (!context.Roles.Any(x => x.Name.Equals("Teacher")))
                {
                    context.Roles.Add(new IdentityRole<long>("Teacher"));
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

                    logger.LogInformation("Pastor Moon created");
                }
                else
                {
                    logger.LogInformation("Pastor Moon already exists");
                }
            }
        }
    }
}