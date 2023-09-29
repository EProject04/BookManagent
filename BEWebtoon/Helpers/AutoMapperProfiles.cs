using AutoMapper;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;

namespace BEWebtoon.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Users, UserDto>();
            CreateMap<CreateUserDto, Users>();
            CreateMap<UpdateUserDto, Users>();
        }
    }
}
