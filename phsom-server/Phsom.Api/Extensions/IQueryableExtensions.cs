#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="IQueryableExtensions.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Extensions
{
    using System.Linq;

    /// <summary>
    /// The IQueryable extensions.
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// The add paging.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="batchSize">
        /// The batch size.
        /// </param>
        /// <typeparam name="{T}">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable{T}"/>.
        /// </returns>
        public static IQueryable<T> AddPaging<T>(this IQueryable<T> query, int page, int batchSize)
        {
            return query.Skip(page * batchSize).Take(batchSize);
        }
    }
}