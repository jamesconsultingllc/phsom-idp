#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="Student.cs" company="James Consulting, LLC">
//   Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>rudyj</author>
// <summary>
// 
// </summary>
// ----------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Data
{
    #region includes

    using System.Collections.Generic;

    using Phsom.Api.Models;

    #endregion

    /// <summary>
    /// The student.
    /// </summary>
    public class Student : Auditable
    {
        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///     Gets or sets the address id.
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        public virtual AddressModel AddressModel { get; set; }

        /// <summary>
        ///     Gets or sets the course.
        /// </summary>
        public virtual List<Course> Courses { get; set; }
    }
}