using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class CompanyMap : Profile
{
    public CompanyMap()
    {
        CreateMap<Company,GetCompanyDTO>().ReverseMap();
        CreateMap<Company,PostCompanyDTO>().ReverseMap();
    }
}