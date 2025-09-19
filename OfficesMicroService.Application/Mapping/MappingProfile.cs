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
    }
}
