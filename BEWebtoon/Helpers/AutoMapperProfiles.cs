using AutoMapper;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;

namespace BEWebtoon.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
