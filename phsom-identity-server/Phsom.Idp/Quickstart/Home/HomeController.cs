// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityServer4.Quickstart.UI
{
    using System.Threading.Tasks;

    using IdentityServer4.Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IHostingEnvironment environment;

        public HomeController(IIdentityServerInteractionService interaction, IHostingEnvironment environment)
        {
            this.interaction = interaction;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            if (environment.IsDevelopment())
            {
                // only show in development
                return View();
            }

            return NotFound();
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await interaction.GetErrorContextAsync(errorId).ConfigureAwait(false);
            if (message != null)
            {
                vm.Error = message;

                if (!environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }
    }
}