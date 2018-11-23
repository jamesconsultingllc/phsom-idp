#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Person.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Data
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The person.
    /// </summary>
    public class Person : Auditable
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets the address ID
        /// </summary>
        public int AddressId { get; set; }
        
        /// <summary>
        /// Gets or sets the person's address
        /// </summary>
        public virtual Address Address { get; set; }
    }
}