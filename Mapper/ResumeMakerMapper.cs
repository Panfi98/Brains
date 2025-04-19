using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ResumeMakerMapper: Profile
{
    public ResumeMakerMapper()
    {
        CreateMap<Resume, PostResumeForResumeMaker>().ReverseMap();
        CreateMap<Education, PostEducationForResumeMaker>().ReverseMap();
        CreateMap<Certification, PostCertificationForResumeMaker>().ReverseMap();
        CreateMap<Experience, PostExperienceForResumeMaker>().ReverseMap();
        CreateMap<Project, PostProjectForResumeMaker>().ReverseMap();
        CreateMap<Skill, PostSkillForResumeMaker>().ReverseMap();
        CreateMap<Reference, PostReferenceForResumeMaker>().ReverseMap();
        
        
        
    }
   
}