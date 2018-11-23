#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Config.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Idp
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    using IdentityModel;

    using IdentityServer4;
    using IdentityServer4.Models;
    using IdentityServer4.Test;

    /// <summary>
    /// The config.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// The get clients.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
               new Client
               {
                   ClientName = "phsom-web-client",
                   ClientId = "phsom-web-client",
                   AllowedGrantTypes = GrantTypes.Implicit,
                   RequireConsent = false,
                   AccessTokenLifetime = 900,
                   AllowAccessTokensViaBrowser = true,
                   RedirectUris =
                       new List<string> { "https://localhost:4200/signin-oidc" },
                   AllowedScopes =
                   {
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile,
                       IdentityServerConstants.StandardScopes.Address,
                       "roles",
                       "phsom-api"
                   },
                   PostLogoutRedirectUris = new[] { "https://localhost:4200" }
               }
            };
        }

        /// <summary>
        /// The get identity resources.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
                       {
                           new IdentityResources.OpenId(),
                           new IdentityResources.Profile(),
                           new IdentityResources.Address(),
                           new IdentityResource("roles", "Your role(s)", new[] { "role" })
                       };
        }

        /// <summary>
        /// The get users.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.List{T}"/>.
        /// </returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
                       {
                           new TestUser
                               {
                                   SubjectId = "2D86D4D9-4A07-4745-8B51-CF5A930B891E",
                                   Username = "rudy",
                                   Password = "password",
                                   Claims = new List<Claim>
                                                {
                                                    new Claim(
                                                        JwtClaimTypes.GivenName,
                                                        "Rudy"),
                                                    new Claim(
                                                        JwtClaimTypes.FamilyName,
                                                        "James")
                                                }
                               }
                       };
        }

        /// <summary>
        /// The get API resources.
        /// </summary>
        /// <returns>
        /// The <see cref="System.Collections.Generic.IEnumerable{T}"/>.
        /// </returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { new ApiResource("phsom-api", "Phsom Web API") };
        }
    }
}