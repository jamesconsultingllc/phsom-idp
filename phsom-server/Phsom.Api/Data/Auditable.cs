#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Auditable.cs" company="James Consulting, LLC">
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
    using System;

    /// <summary>
    ///     The auditable.
    /// </summary>
    public abstract class Auditable
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        public int? UpdatedBy { get; set; }
    }
}