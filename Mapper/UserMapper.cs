using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,GetUserDTO>() .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.createdAt, opt => opt.Ignore())
            .ForMember(dest => dest.updatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.deletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.SoftDeleted, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<PostUserDTO, User>();
    }
}