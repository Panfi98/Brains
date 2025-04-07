using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class JobMapper : Profile
{
    public JobMapper()
    {
        CreateMap<Job, GetJobDTO>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.deletedAt, opt => opt.Ignore());

        CreateMap<Job, PostJobDTO>().ReverseMap()
            .ForMember(dest => dest.Name, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Name)))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)))
            .ForMember(dest => dest.Place, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Place)))
            .ForMember(dest => dest.Position, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Position)));
    }
}