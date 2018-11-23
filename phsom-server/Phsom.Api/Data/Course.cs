#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Course.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Data
{
    /// <summary>
    /// The course.
    /// </summary>
    public class Course : Auditable
    {
        /// <summary>
        /// Gets or sets the course id.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}