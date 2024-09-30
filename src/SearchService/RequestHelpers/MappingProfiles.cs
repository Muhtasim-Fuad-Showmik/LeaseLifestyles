using System;
using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers
{
    /// <summary>
    /// Class for mapping between <see cref="RentCreated"/> and <see cref="Item"/>.
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfiles"/> class.
        /// </summary>
        public MappingProfiles()
        {
            CreateMap<RentCreated, Item>();
        }
    }
}

