using AutoMapper;
using BEWebtoon.DataTransferObject.AuthorDto;
using BEWebtoon.DataTransferObject.BookFollowsDto;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.CategoriesBookDto;
using BEWebtoon.DataTransferObject.CategoriesDto;
using BEWebtoon.DataTransferObject.CommentsDto;
using BEWebtoon.DataTransferObject.FollowingsDto;
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
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Users.Roles.RoleName));
            CreateMap<CreateUserProfileDto, UserProfile>();
            CreateMap<UpdateUserProfileDto, UserProfile>();
            #endregion

            #region Book
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.BookFollows, otp => otp.MapFrom(src => src.BookFollows))
                .ForMember(dest => dest.CategoriesBook, otp => otp.MapFrom(src => src.CategoryBooks));
            CreateMap<Book, FollowingBookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            #endregion

            #region Author
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            #endregion

            #region Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            #endregion

            #region BookFollow
            CreateMap<BookFollow, BookFollowDto>()
                .ForMember(dest => dest.AuthorId, otp => otp.MapFrom(src => src.Authors.Id))
                .ForMember(dest => dest.BookId, otp => otp.MapFrom(src => src.Books.Id))
                .ForMember(dest => dest.AuthorName, otp => otp.MapFrom(src => src.Authors.AuthorName));
            #endregion

            #region CategoryBook
            CreateMap<CategoryBook, CategoryBookDto>()
                .ForMember(dest => dest.CategoryId, otp => otp.MapFrom(src => src.Categories.Id))
                .ForMember(dest => dest.BookId, otp => otp.MapFrom(src => src.Books.Id))
                .ForMember(dest => dest.CategoryName, otp => otp.MapFrom(src => src.Categories.CategoryName));
            #endregion

            #region Comment
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserProfiles.FullName));
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
            #endregion


            #region Following
            CreateMap<Following, FollowingDto>()
                .ForMember(dest => dest.Books, otp => otp.MapFrom(src => src.Books));
            CreateMap<UpdateFollowingDto, Following>();
            #endregion
        }
    }
}
