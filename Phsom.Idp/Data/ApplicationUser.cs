// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="">
//   
// </copyright>
// <summary>
//   The application user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Phsom.Idp.Data
{
    #region includes

    using Microsoft.AspNetCore.Identity;

    #endregion

    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser<long>
    {
    }
}