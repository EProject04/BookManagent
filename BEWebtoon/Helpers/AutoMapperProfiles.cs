using AutoMapper;
using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;

namespace BEWebtoon.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region User
            CreateMap<User, UserDto>()
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Roles.RoleName));
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<LoginUserDto, User>();
            #endregion

            #region Role
            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
            #endregion
        }
    }
}
