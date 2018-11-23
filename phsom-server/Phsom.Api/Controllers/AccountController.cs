#region Copyright
// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------
#endregion

using Microsoft.AspNetCore.Mvc;

namespace Phsom.Api.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Phsom.Api.Data;
    using Phsom.Api.Models;

    public class AccountController : ControllerBase
    {
        private readonly UserManager<PhsomUser> userManager;

        public AccountController(UserManager<PhsomUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Ok();
        }
    }
}