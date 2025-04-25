using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ResumeMapper: Profile
{
    public ResumeMapper()
    {
        CreateMap<Resume, PostResumeDTO>().ReverseMap();
        CreateMap<Education, PostEducationDTO>().ReverseMap();
        CreateMap<Certification, PostCertificationDTO>().ReverseMap();
        CreateMap<Experience, PostExperienceDTO>().ReverseMap();
        CreateMap<Project, PostProjectDTO>().ReverseMap();
        CreateMap<InfoSkill, PostInfoSkillDTO>().ReverseMap();
        CreateMap<Reference, PostReferenceDTO>().ReverseMap();
        
        
        
    }
   
}