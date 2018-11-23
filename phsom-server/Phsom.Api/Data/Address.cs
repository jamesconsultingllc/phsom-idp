#region Copyright
// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Address.cs" company="James Consulting, LLC">
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
    /// The address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public string State { get; set; }
    }
}