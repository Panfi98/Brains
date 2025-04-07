using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ContactMapper : Profile
{
    public ContactMapper()
    {
        CreateMap<Contact,GetContactDTO>().ReverseMap();
    }
}