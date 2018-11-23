#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="Address.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The address.
    /// </summary>
    public class AddressModel : IEquatable<AddressModel>, IComparable<AddressModel>
    {
        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        [Display(Name = "Address")]
        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        [StringLength(100)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        [Required]
        [StringLength(14)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// True if equal otherwise false
        /// </returns>
        public static bool operator ==(AddressModel left, AddressModel right)
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
        /// Return true if not equal otherwise false
        /// </returns>
        public static bool operator !=(AddressModel left, AddressModel right)
        {
            return !(left == right);
        }

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(AddressModel other)
        {
            return other == null ? 1 : string.Compare(this.ToString(), other.ToString(), StringComparison.Ordinal);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AddressModel))
            {
                return false;
            }

            return this.Equals((AddressModel)obj);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(AddressModel other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Address1 ?? string.Empty, other.Address1 ?? string.Empty)
                   && string.Equals(this.Address2 ?? string.Empty, other.Address2 ?? string.Empty)
                   && string.Equals(this.City ?? string.Empty, other.City ?? string.Empty)
                   && string.Equals(this.State ?? string.Empty, other.State ?? string.Empty) && string.Equals(
                       this.PostalCode ?? string.Empty,
                       other.PostalCode ?? string.Empty);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            const int prime = 397;
            var result = this.Address1 != null ? this.Address1.GetHashCode() : 0;
            result = (result * prime) ^ (this.Address2 != null ? this.Address2.GetHashCode() : 0);
            result = (result * prime) ^ (this.City != null ? this.City.GetHashCode() : 0);
            result = (result * prime) ^ (this.State != null ? this.State.GetHashCode() : 0);
            result = (result * prime) ^ (this.PostalCode != null ? this.PostalCode.GetHashCode() : 0);
            return result;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var address = this.Address1;

            if (!string.IsNullOrWhiteSpace(this.Address2))
            {
                address += Environment.NewLine + this.Address2;
            }

            address += $"{Environment.NewLine}{this.City}, {this.State} {this.PostalCode}";

            return address;
        }
    }
}