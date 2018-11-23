#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="PhsomContext.cs" company="James Consulting, LLC">
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
    using JetBrains.Annotations;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     The PHSOM context.
    /// </summary>
    public class PhsomContext : IdentityUserContext<PhsomUser, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhsomContext"/> class.
        /// </summary>
        public PhsomContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhsomContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public PhsomContext([NotNull] DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        ///     Gets or sets the courses.
        /// </summary>
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Gets or sets the people
        /// </summary>
        public DbSet<Person> People {get; set; }

        /// <summary>
        ///     Gets or sets the students.
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Gets or sets the addresses
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PhsomUser>().OwnsOne(x => x.Name, sa =>
            {
                sa.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired();
                sa.Property(x => x.LastName).HasColumnName("LastName").IsRequired();
                sa.Property(x => x.Title).HasColumnName("Title");
            }).OwnsOne(x => x.UserProfile);

            base.OnModelCreating(builder);
        }
    }
}