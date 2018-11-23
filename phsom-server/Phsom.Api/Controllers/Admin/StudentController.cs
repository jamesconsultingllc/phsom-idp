#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="StudentController.cs" company="James Consulting, LLC">
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

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    using AutoMapper;

    using JetBrains.Annotations;

    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Phsom.Api.Data;
    using Phsom.Api.Extensions;
    using Phsom.Api.Models;

    #endregion

    /// <summary>
    ///     The student controller.
    /// </summary>
    // ReSharper disable once HollowTypeName
    public class StudentController : ODataController, IDisposable
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<StudentController> logger;

        /// <summary>
        ///     The PHSOM context.
        /// </summary>
        private readonly PhsomContext phsomContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StudentController" /> class.
        /// </summary>
        /// <param name="phsomContext">
        ///     The PHSOM context.
        /// </param>
        /// <param name="logger">
        ///     The logger.
        /// </param>
        public StudentController(PhsomContext phsomContext, ILogger<StudentController> logger)
        {
            this.phsomContext = phsomContext;
            this.logger = logger;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            phsomContext?.Dispose();
        }

        /// <summary>
        ///     The get students.
        /// </summary>
        /// <returns>
        ///     The <see cref="IQueryable{T}" />.
        /// </returns>
        [EnableQuery]
        public IQueryable<StudentCreation> Get()
        {
            return phsomContext.Students.Select(x => Mapper.Map<StudentCreation>(x)).AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [EnableQuery]
        public SingleResult<StudentCreation> Get([FromODataUri] int id)
        {
            var student = phsomContext.Students.Where(x => x.StudentId == id).Select(x => Mapper.Map<StudentCreation>(x)).AsQueryable();
            return SingleResult.Create(student);
        }

        /// <summary>
        ///     The post.
        /// </summary>
        /// <param name="request">
        ///     The request.
        /// </param>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task<IActionResult> Post(StudentCreation request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await phsomContext.Students.AddAsync(Mapper.Map<Student>(request)).ConfigureAwait(false);
                await phsomContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "An error occurred saving the student");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred saving the student");
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "An error occurred saving the student");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred saving the student");
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "An error occurred saving the student");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred saving the student");
            }

            return CreatedAtRoute(nameof(this.Get), new { id = request.StudentId }, request);
        }

        /// <summary>
        ///     The search students.
        /// </summary>
        /// <param name="searchTerm">
        ///     The search term.
        /// </param>
        /// <param name="page">
        ///     The page.
        /// </param>
        /// <param name="batchSize">
        ///     The batch size.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable{T}" />.
        /// </returns>
        public IEnumerable<Student> SearchStudents(string searchTerm, int page, int batchSize = 10)
        {
            // ReSharper disable once FormatStringProblem
            return phsomContext.Students.FromSql("SELECT * FROM [dbo].[Students] (@p0)", searchTerm).AddPaging(page, batchSize);
        }
    }
}