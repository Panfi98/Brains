using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ResumeMakerMapper: Profile
{
    public ResumeMakerMapper()
    {
        CreateMap<Resume, PostResumeForResumeMaker>()
            .ForMember(dest => dest.FirstName, opt => opt.Condition(src => !string.IsNullOrEmpty(src.FirstName)))
            .ForMember(dest => dest.LastName, opt => opt.Condition(src => !string.IsNullOrEmpty(src.LastName)))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Email)))
            .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => !string.IsNullOrEmpty(src.PhoneNumber)))
            .ForMember(dest => dest.Address, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Address)))
            .ForMember(dest => dest.PictureURL, opt => opt.Condition(src => !string.IsNullOrEmpty(src.PictureURL)))
            .ReverseMap();
        
        CreateMap<Education, PostEducationForResumeMaker>().ReverseMap();
        
        
    }
   
}