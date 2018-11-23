#region Copyright

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="MappingProfile.cs" company="James Consulting, LLC">
// //   Copyright (c) 2018 All Rights Reserved
// // </copyright>
// // <author>rudyj</author>
// // <summary>
// // 
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#endregion

namespace Phsom.Api
{
    using AutoMapper;
    using Phsom.Api.Data;
    using Phsom.Api.Models;

    /// <inheritdoc />
    /// <summary>
    /// The mapping profile.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<StudentCreation, Student>()
                .ForMember(x => x.UpdateDate, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());
        }
    }
}