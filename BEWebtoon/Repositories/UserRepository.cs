using AutoMapper;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using IOCBEWebtoon.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace BEWebtoon.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        private readonly IConfiguration _configuration;


        public UserRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager, IConfiguration configuration)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
            _configuration = configuration;
        }
        public async Task CreateUser(CreateUserDto userDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var userInfo = await _dBContext.Users.Where(x => x.Username == userDto.Username).FirstOrDefaultAsync();
                if (userInfo != null)
                {
                    throw new CustomException("Nguoi dung da ton tai");
                }
                else
                {
                    var role = await _dBContext.Roles.Where(x => x.Id == userDto.RoleId).FirstOrDefaultAsync();

                    if (role != null)
                    {
                        var data = _mapper.Map<User>(userDto);
                        try
                        {
                            await _dBContext.Users.AddAsync(data);
                            await _dBContext.SaveChangesAsync();
                            var userProfile = new UserProfile
                            {
                                Id = data.Id,
                                Email = data.Email,

                            };
                           
                            if (userDto.RoleId == 2)
                            {
                                var author = new Author
                                {
                                    AuthorName = data.Username,
                                };
                                userProfile.Authors = author;
                            }
                            var following = new Following
                            {
                                UserId = userProfile.Id,
                            };
                            userProfile.Followings = following;
                            userProfile.ImagePath = "https://aptechlearningproject.site/uploads/userprofiles/male_default.jpg";

                            await _dBContext.UserProfiles.AddAsync(userProfile);
                            await _dBContext.SaveChangesAsync();
                          
                        }
                        catch (Exception ex)
                        {
                            throw new CustomException(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        throw new CustomException("Không tìm thấy quyền người dùng");
                    }
                }


                
               
            }
        }

        public async Task DeleteUser(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var user = await _dBContext.Users.FindAsync(id);
                if (user != null)
                {
                    _dBContext.Users.Remove(user);
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
        }

        public async Task<List<UserDto>> GetAll()
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                List<UserDto> usersDto = new List<UserDto>();
                var users = await _dBContext.Users.Include(u => u.Roles).ToListAsync();
                if (users != null)
                {
                    usersDto = _mapper.Map<List<User>, List<UserDto>>(users);
                }
                return usersDto;
            }
            return null;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _dBContext.Users.FindAsync(id);
            if (user != null)
            {

                UserDto userDto = _mapper.Map<User, UserDto>(user);
                return userDto;

            }
            else
            {
                throw new Exception("Khong tim thay nguoi dung");
            }
        }
       

        public async Task<PagedResult<UserDto>> GetUserPagination(SeacrhPagingRequest request)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var query = await _dBContext.Users.ToListAsync();
                if (!string.IsNullOrEmpty(request.keyword))
                    query = query.Where(x => x.Username.ToLower().Contains(request.keyword.ToLower())
                                            || SearchHelper.ConvertToUnSign(x.Username).ToLower().Contains(request.keyword.ToLower())).ToList();
                var items = _mapper.Map<IEnumerable<UserDto>>(query);
                return PagedResult<UserDto>.ToPagedList(items, request.PageIndex, request.PageSize);
            }
            return null;
           
        }

        public async Task RegisterUser(RegisterUserDto userDto)
        {
            var userInfo = await _dBContext.Users.Where(x=>x.Username== userDto.Username).FirstOrDefaultAsync();
            if (userInfo != null)
            {
                  throw new CustomException("Nguoi dung da ton tai");
            }
            else
            {
                
                var data = _mapper.Map<User>(userDto);
                try
                {
                    await _dBContext.Users.AddAsync(data);
                    await _dBContext.SaveChangesAsync();

                    var userProfile = new UserProfile
                    {
                        Id = data.Id,
                        Email = data.Email,
                    };
                    if (userDto.RoleId == 2)
                    {
                        var author = new Author
                        {
                            AuthorName = data.Username,
                        };
                        userProfile.Authors = author;
                    }
                    var following = new Following
                    {
                        UserId = userProfile.Id,
                    };
                    userProfile.Followings = following;
                    await _dBContext.UserProfiles.AddAsync(userProfile);
                    await _dBContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new CustomException($"Loi roi" + ex);

                }
            }
           
        }
        public async Task UpdateUser(UpdateUserDto userDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                await _dBContext.Database.BeginTransactionAsync();
                var user = await _dBContext.Users.Where(x => x.Id == userDto.Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    _dBContext.Entry(user).CurrentValues.SetValues(userDto);
                    await _dBContext.SaveChangesAsync();
                    await _dBContext.Database.CommitTransactionAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
        }

        public async Task LoginUser(LoginUserDto userDto)
        {
            var userInfo = await _dBContext.Users.Where(x=>x.Username == userDto.Username && x.Password == userDto.Password).FirstOrDefaultAsync();
            if(userInfo != null)
            {
                if (userInfo != null)
                {
                    _sessionManager.SetSessionValue("RoleId", userInfo.RoleId.ToString());
                    _sessionManager.SetSessionValueInt("UserId", userInfo.Id);
                }
                
            }
            else
            {
                throw new CustomException("Thong tin dang nhap khong dung");
            }

        }

        public async Task Logout()
        {
            if (_sessionManager.CheckLogin())
            {
                _sessionManager.Logout();
                return;
            }
           
        }
        public async Task ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var userInfo = await _dBContext.Users.Where(x => x.Email == forgotPasswordDto.Email).FirstOrDefaultAsync();
            if (userInfo == null)
            {
                throw new CustomException("Không tìm thấy người dùng với email:" + forgotPasswordDto.Email);
            }
            userInfo.Password =forgotPasswordDto.NewPassword;
            await _dBContext.SaveChangesAsync();

           
        }

    }
}
