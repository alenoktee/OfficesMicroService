using AutoMapper;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OfficeCreateDto, Office>();

        CreateMap<Office, OfficeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        CreateMap<OfficeUpdateDto, Office>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            {
                if (srcMember is null)
                {
                    return false;
                }

                if (srcMember is string s && string.IsNullOrEmpty(s))
                {
                    return false;
                }

                return true;
            }));
    }
}
