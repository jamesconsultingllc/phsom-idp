#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UserClaimsConverter.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Idp.Helpers
{
    #region includes

    using System.Linq;
    using System.Security.Claims;

    using AutoMapper;

    using IdentityModel;

    using IdentityServer4;

    using JetBrains.Annotations;

    using Newtonsoft.Json.Linq;

    using Phsom.Idp.Models;

    #endregion

    /// <summary>
    ///     The user creation to claims converter.
    /// </summary>
    [UsedImplicitly]
    public class UserClaimsConverter : ITypeConverter<UserCreationDto, Claim[]>, ITypeConverter<Claim[], UserDto>
    {
        /// <inheritdoc />
        public Claim[] Convert(UserCreationDto source, Claim[] destination, ResolutionContext context)
        {
            return new[] { new Claim(JwtClaimTypes.GivenName, source.FirstName), new Claim(JwtClaimTypes.FamilyName, source.LastName), new Claim(JwtClaimTypes.Email, source.Email), new Claim(JwtClaimTypes.PhoneNumber, source.PhoneNumber), new Claim(JwtClaimTypes.PreferredUserName, source.UserName), new Claim(JwtClaimTypes.Address, $@"{{ 'street_address' : '{source.StreetAddress}', 'locality' : '{source.City}', 'region' : '{source.State}', 'postal_code' : '{source.PostalCode}' }}", IdentityServerConstants.ClaimValueTypes.Json) };
        }

        /// <inheritdoc />
        public UserDto Convert(Claim[] source, UserDto destination, ResolutionContext context)
        {
            destination.UserName = source.Single(x => x.Type == JwtClaimTypes.PreferredUserName).Value;
            destination.Email = source.Single(x => x.Type == JwtClaimTypes.Email).Value;
            destination.FirstName = source.Single(x => x.Type == JwtClaimTypes.GivenName).Value;
            destination.LastName = source.Single(x => x.Type == JwtClaimTypes.FamilyName).Value;
            destination.PhoneNumber = source.Single(x => x.Type == JwtClaimTypes.PhoneNumber).Value;

            var addressString = source.Single(x => x.Type == JwtClaimTypes.Address).Value;

            if (!string.IsNullOrWhiteSpace(addressString))
            {
                dynamic address = JObject.Parse(addressString);
                destination.StreetAddress = address.street_address;
                destination.City = address.locality;
                destination.State = address.region;
                destination.PostalCode = address.postal_code;
            }

            return destination;
        }
    }
}