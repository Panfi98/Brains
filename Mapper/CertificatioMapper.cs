using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class CertificationMapper : Profile
{
    public CertificationMapper()
    {
        CreateMap<Certification , GetCertificationDTO>()
            .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.deletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.SoftDeleted, opt => opt.Ignore())
            .ReverseMap();
      
        CreateMap<Certification,PostCertificationDTO>().ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Name)))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)))
            .ForMember(dest => dest.Date, opt => opt.Condition(src => src.Date >= DateTime.UtcNow.AddYears(-1)))
            .ForMember(dest => dest.Url, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Url)))
            .ForMember(dest => dest.Type, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Type)))
            .ForMember(dest => dest.ValidTo, opt => opt.Condition(src => src.ValidTo > DateTime.UtcNow));
            
    }
}

