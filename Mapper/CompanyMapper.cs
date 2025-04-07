using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class CompanyMapper : Profile
{
    public CompanyMapper()
    {
        CreateMap<Company,GetCompanyDTO>().ReverseMap();
        CreateMap<Company,PostCompanyDTO>().ReverseMap();
    }
}