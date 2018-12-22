#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCreationDto.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCreationDto.cs" company="James Consulting, LLC">
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
    ///     The user creation model.
    /// </summary>
    public class UserCreationDto : UserBase
    {
        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        internal string UserName { get; set; }
    }
}