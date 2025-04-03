using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class JobMap : Profile
{
    public JobMap()
    {
        CreateMap<Job,GetJobDTO>().ReverseMap();
    }
}