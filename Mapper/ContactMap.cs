using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class ContactMap : Profile
{
    public ContactMap()
    {
        CreateMap<Contact,GetContactDTO>().ReverseMap();
    }
}