using AutoMapper;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;

namespace BEWebtoon.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Roles.RoleName));
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
