#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileController.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Controllers.Admin
{
    #region includes

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Phsom.Api.Data;

    #endregion

    /// <summary>
    /// The profile controller.
    /// </summary>
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly PhsomContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ProfileController(PhsomContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid ID");
            }

            var profile = await context.Students.FindAsync(id).ConfigureAwait(false);

            if (profile == default(Student))
            {
                return NotFound();
            }

            return Ok();
        }
    }
}