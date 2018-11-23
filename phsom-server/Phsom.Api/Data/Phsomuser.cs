#region Copyright
// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="Phsomuser.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------
#endregion

namespace Phsom.Api.Data
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public class PhsomUser : IdentityUser<int>
    {
        public PersonName Name { get; set; }

       // public virtual ICollection<Enrollment> Enrollments { get; set; }
        /// <summary>
        /// Gets or sets the user profile id.
        /// </summary>
        public int UserProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public PhsomUser()
        {
            Name = new PersonName();
        }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<PhsomUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }
}