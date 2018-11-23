// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersController.cs" company="">
//   
// </copyright>
// <summary>
//   The users controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersController.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

// ReSharper disable StyleCop.SA1650
namespace Phsom.Idp.Controllers
{
    #region includes

    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Phsom.Idp.Data;
    using Phsom.Idp.Models;

    #endregion

    /// <summary>
    ///     The users controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        ///     The context.
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        ///     The user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="context">
        /// </param>
        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        /// <summary>
        /// The get by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StatusCodeResult), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK, Type = typeof(UserDto))]
        public async Task<ActionResult<UserDto>> GetByUserName([Required] string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest();
            }

            var user = await userManager.FindByNameAsync(userName).ConfigureAwait(false);

            var claims = await userManager.GetClaimsAsync(user).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = Mapper.Map<UserDto>(user);
            if (claims != null)
            {
                Mapper.Map(claims.ToArray(), userDto);
            }

            return Ok(userDto);
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="userCreationDto">
        /// The user creation object.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        [Produces(typeof(UserDto))]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreatedAtActionResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(StatusCodeResult), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserCreationDto userCreationDto)
        {
            if (userCreationDto == null)
            {
                return BadRequest();
            }

            var userName = $"{userCreationDto.FirstName[0]}{userCreationDto.LastName}";

            return await CreateUser(userCreationDto, userName).ConfigureAwait(false);
        }

        /// <summary>
        /// The unlock user.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost("unlock/{userName}")]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StatusCodeResult), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UnlockUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest();
            }

            var user = await userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.SetLockoutEnabledAsync(user, false).ConfigureAwait(false);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="userCreationDto">
        /// The user creation dto.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<ActionResult<UserDto>> CreateUser(UserCreationDto userCreationDto, string userName)
        {
            var user = await userManager.FindByNameAsync(userName).ConfigureAwait(false);
            userCreationDto.UserName = userName;

            if (user == null)
            {
                var newUser = Mapper.Map<ApplicationUser>(userCreationDto);
                newUser.UserName = userName;
                var claims = Mapper.Map<Claim[]>(userCreationDto);

                using (var transaction = await context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var result = await userManager.CreateAsync(newUser, userCreationDto.Password).ConfigureAwait(false);

                    if (result.Succeeded)
                    {
                        result = await userManager.AddClaimsAsync(newUser, claims).ConfigureAwait(false);

                        if (result.Succeeded)
                        {
                            transaction.Commit();
                            var userDto = Mapper.Map<UserDto>(newUser);
                            Mapper.Map(claims, userDto);
                            return Created($"{Request.Scheme}://{Request.Host}{Request.PathBase}/api/user/{newUser.UserName}", userDto);
                        }

                        transaction.Rollback();
                    }
                }
            }
            else
            {
                var lastCharIdx = -1;

                for (lastCharIdx = userName.Length - 1; lastCharIdx >= 0; lastCharIdx--)
                {
                    if (char.IsDigit(userName[lastCharIdx]))
                    {
                        continue;
                    }

                    break;
                }

                if (lastCharIdx == userName.Length - 1)
                {
                    return await CreateUser(userCreationDto, $"{userName}1").ConfigureAwait(false);
                }

                return await CreateUser(userCreationDto, $"{userName.Substring(0, lastCharIdx + 1)}{int.Parse(userName.Substring(lastCharIdx + 1)) + 1}").ConfigureAwait(false);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, "There was an error encountered creating the user");
        }
    }
}