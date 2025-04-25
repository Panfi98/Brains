using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ResumeTemplateMapper : Profile
{
    public ResumeTemplateMapper()
    {
        CreateMap<ResumeTemplate, GetResumeTemplateDTO>()
            .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.deletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.SoftDeleted, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<ResumeTemplate, PostResumeTemplateDTO>().ReverseMap();
    }
}