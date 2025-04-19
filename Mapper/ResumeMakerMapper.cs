using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ResumeMakerMapper: Profile
{
    public ResumeMakerMapper()
    {
        CreateMap<Resume, GetResumeDTOForResumeMaker>().ReverseMap()
            .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.deletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.SoftDeleted, opt => opt.Ignore());
            
        
        CreateMap<Resume, PostResumeForResumeMaker>().ReverseMap();
        
        CreateMap<Education, GetEducationDTOForResumeMaker>().ReverseMap()
            .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.deletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.SoftDeleted, opt => opt.Ignore());
        
        CreateMap<Education, PostEducationForResumeMaker>().ReverseMap();
        
        
    }
   
}