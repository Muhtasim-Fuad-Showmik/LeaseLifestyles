using System;
using AutoMapper;
using Contracts;
using RentService.DTOs;
using RentService.Entities;

namespace RentService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Mapping for Rent to Rent DTO with Item included
        CreateMap<Rent, RentDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, RentDto>();

        // Mapping for CreateRentDto to Rent and Item with
        // d: destination, o: option, s: source
        CreateMap<CreateRentDto, Rent>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<CreateRentDto, Item>();

        // Mapping for RentDTO to RentCreated entity for the Event Bus
        CreateMap<RentDto, RentCreated>();

        // Mapping for Rent to RentUpdated entity for the Event Bus
        CreateMap<Rent, RentUpdated>().IncludeMembers(a => a.Item);
        CreateMap<Item, RentUpdated>();
    }
}
