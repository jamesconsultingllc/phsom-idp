#region Copyright

// ----------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonName.cs" company="James Consulting, LLC">
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

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     Represents the person's name
    /// </summary>
    [ComplexType]
    public class PersonName : IEquatable<PersonName>, IComparable<PersonName>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonName" /> class.
        /// </summary>
        public PersonName()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonName" /> class.
        /// </summary>
        /// <param name="lastName">
        ///     The last name.
        /// </param>
        /// <param name="firstName">
        ///     The first name.
        /// </param>
        public PersonName([Required] string lastName, [Required] string firstName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        ///     Gets or sets person's First Name
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets person's Last Name
        /// </summary>
        [StringLength(100)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets persons' Title
        /// </summary>
        public Title? Title { get; set; }

        /// <summary>
        ///     The ==.
        /// </summary>
        /// <param name="left">
        ///     The left.
        /// </param>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        /// Returns true if equal, otherwise false
        /// </returns>
        public static bool operator ==(PersonName left, PersonName right)
        {
            if (ReferenceEquals(left, null))
            {
                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// Return true if not equal, otherwise false
        /// </returns>
        public static bool operator !=(PersonName left, PersonName right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     The compare to.
        /// </summary>
        /// <param name="other">
        ///     The other.
        /// </param>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public int CompareTo(PersonName other)
        {
            return other == null ? 1 : string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        ///     Compares 2 person name objects for equality
        /// </summary>
        /// <param name="obj">The person's name to compare to the current name</param>
        /// <returns>Returns true if equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PersonName person))
            {
                return false;
            }

            return Equals(person);
        }

        /// <summary>
        ///     The equals.
        /// </summary>
        /// <param name="other">
        ///     The other.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        public bool Equals(PersonName other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && Equals(Title, other.Title);
        }

        /// <summary>
        ///     Generates a hash code for the person's name
        /// </summary>
        /// <returns>An integer representing the hash code of the given name</returns>
        public override int GetHashCode()
        {
            return (GetType() + ToString()).GetHashCode();
        }

        /// <summary>
        ///     Returns a string representation of the person's name
        /// </summary>
        /// <returns>a string representation of the person's name</returns>
        public override string ToString()
        {
            var name = string.Empty;

            if (Title.HasValue)
            {
                name = Title.ToString();
            }

            name += " " + FirstName;

            name += " " + LastName;

            return name;
        }

        /// <summary>
        ///     The to string last name first.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public string ToStringLastNameFirst()
        {
            var name = LastName;
            name += ", " + FirstName;
            return name;
        }
    }
}