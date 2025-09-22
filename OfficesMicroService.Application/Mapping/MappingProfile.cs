using AutoMapper;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OfficeCreateDto, Office>();
        CreateMap<OfficeUpdateDto, Office>();

        CreateMap<Office, OfficeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
    }
}
