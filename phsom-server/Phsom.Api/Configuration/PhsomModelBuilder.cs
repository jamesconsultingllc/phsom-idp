#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="PhsomModelBuilder.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Configuration
{
    using System;

    using Microsoft.AspNet.OData.Builder;
    using Microsoft.OData.Edm;

    using Phsom.Api.Data;

    /// <summary>
    /// Builds the EDM Model
    /// </summary>
    public class PhsomModelBuilder
    {
        /// <summary>
        /// Gets the EDM Model
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns>Returns the generated model</returns>
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder.EntitySet<Student>(nameof(Student)).EntityType.Filter().Count().Expand().OrderBy().Page().Select();

            return builder.GetEdmModel();
        }
    }
}