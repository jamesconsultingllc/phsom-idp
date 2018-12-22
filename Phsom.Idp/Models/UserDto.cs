#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDto.cs" company="James Consulting, LLC">
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
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    /// <summary>
    /// The user dto.
    /// </summary>
    public class UserDto : UserBase
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
    }
}