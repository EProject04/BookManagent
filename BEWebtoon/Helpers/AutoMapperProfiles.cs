using AutoMapper;
using BEWebtoon.DataTransferObject.AuthorDto;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;
using BEWebtoon.Requests.BookRequest;

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

            #region UserProfile
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<CreateUserProfileDto, UserProfile>();
            CreateMap<UpdateUserProfileDto, UserProfile>();
            #endregion

            #region Book
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<CategoryBook, BookRequest>()
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.CategoryName));
            #endregion

            #region Author
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            #endregion
        }
    }
}
