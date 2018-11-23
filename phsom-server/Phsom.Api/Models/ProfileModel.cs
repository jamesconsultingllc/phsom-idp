#region Copyright
// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="ProfileModel.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------
#endregion

using System.ComponentModel.DataAnnotations;
using Phsom.Api.Data;

namespace Phsom.Api.Models
{
    public class ProfileModel : Person
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }        
        public AddressModel AddressModel { get; set; }
    }
}