#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Models
{
    /// <summary>
    /// The login model.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}