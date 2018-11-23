#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="StudentControllerTests.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Tests.Admin
{
    #region includes

    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using AutoMapper;

    using FluentAssertions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging.Abstractions;

    using Phsom.Api.Controllers.Admin;
    using Phsom.Api.Data;
    using Phsom.Api.Models;

    using Xunit;

    #endregion

    /// <summary>
    ///     The student controller tests.
    /// </summary>
    public sealed class StudentControllerTests : IDisposable
    {
        /// <summary>
        ///     The initialized.
        /// </summary>
        private static bool initialized;

        /// <summary>
        ///     The context.
        /// </summary>
        private PhsomContext context;

        /// <summary>
        ///     The disposed value.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StudentControllerTests" /> class.
        /// </summary>
        public StudentControllerTests()
        {
            var options = new DbContextOptionsBuilder<PhsomContext>().UseInMemoryDatabase("KLS").Options;
            context = new PhsomContext(options);

            if (!initialized)
            {
                Mapper.Initialize(config => { config.AddProfile<MappingProfile>(); });
                initialized = true;
            }
        }

        /// <summary>
        ///     The register student.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        [Fact]
        public async Task CreateStudent()
        {
            var request = new StudentCreation();
            var controller = new StudentController(context, new NullLogger<StudentController>());
            var result = await controller.Post(request).ConfigureAwait(false);
            result.Should().BeOfType<CreatedAtRouteResult>();
            context.Students.First().StudentId.Should().Be(1);
        }

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// The post student bad request.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task PostStudentBadRequest()
        {
            var request = new StudentCreation();
            var controller = new StudentController(context, new NullLogger<StudentController>());
            controller.ControllerContext.ModelState.AddModelError(string.Empty, "Error");
            var result = await controller.Post(request).ConfigureAwait(false);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        /// <summary>
        /// The post student throws invalid operation exception.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task PostStudentThrowsInvalidOperationExecption()
        {
            var options = new DbContextOptionsBuilder<PhsomContext>().UseInMemoryDatabase("KLS").Options;
            context = new PhsomContext(options);
            var request = new StudentCreation();
            var controller = new StudentController(context, new NullLogger<StudentController>());
            await controller.Post(new StudentCreation()).ConfigureAwait(false);
            request.StudentId = 1;
            var result = await controller.Post(request).ConfigureAwait(false);
            result.Should().BeOfType<ObjectResult>();
            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        /// <summary>
        ///     The dispose.
        /// </summary>
        /// <param name="disposing">
        ///     The disposing.
        /// </param>
        // ReSharper disable once FlagArgument
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context?.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}