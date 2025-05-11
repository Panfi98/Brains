using BrainsToDo.DTOModels;
using BrainsToDo.Models;

namespace BrainsToDo.Mapper;

using AutoMapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserLogInDTO>().ReverseMap(); 
        CreateMap<User,UserSignUpDTO>().ReverseMap();
    }
}