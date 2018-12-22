#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UserBase.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Idp.Models
{
    #region includes

    using System.ComponentModel.DataAnnotations;

    #endregion

    /// <summary>
    /// The user base.
    /// </summary>
    public abstract class UserBase
    {
        /// <summary>
        ///     Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets the postal code.
        /// </summary>
        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$")]
        public string PostalCode { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     Gets or sets the street address.
        /// </summary>
        public string StreetAddress { get; set; }
    }
}