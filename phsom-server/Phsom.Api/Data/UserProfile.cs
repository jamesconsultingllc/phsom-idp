#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="James Consulting, LLC">
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
    /// <summary>
    ///     The user profile.
    /// </summary>
    public class UserProfile : Auditable
    {
        public int UserProfileId { get; set; }
        /// <summary>
        ///     Gets or sets the address id.
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        ///     Gets or sets the bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether has photo.
        /// </summary>
        public bool HasPhoto { get; set; }

        /// <summary>
        ///     Gets or sets the mailing address.
        /// </summary>
        public virtual Address MailingAddress { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        public virtual PhsomUser User { get; set; }
    }
}