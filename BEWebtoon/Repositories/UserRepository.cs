using AutoMapper;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using IOCBEWebtoon.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BEWebtoon.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;


        public UserRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }
        public async Task CreateUser(CreateUserDto userDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var role = await _dBContext.Roles.Where(x=>x.Id == userDto.RoleId).FirstOrDefaultAsync();   
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
                        };
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
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
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
            return null;
        }

        public async Task UpdateUser(UpdateUserDto userDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            { 
                await _dBContext.Database.BeginTransactionAsync();
                var user = await _dBContext.Users.Where(x => x.Id == userDto.Id).FirstOrDefaultAsync();
                var checkRole = await _dBContext.Roles.Where(x => x.Id == userDto.RoleId).FirstOrDefaultAsync();
                if (user != null)
                {
                    if (checkRole != null)
                    {
                        user.RoleId = userDto.RoleId;
                    }
                    else
                    {
                        throw new CustomException("Không tìm thấy quyền người dùng");
                    }
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
       

        public async Task<PagedResult<UserDto>> GetUserPagination(SeacrhPagingRequest request)
        {
            var query = await _dBContext.Users.ToListAsync();
            if (!string.IsNullOrEmpty(request.keyword))
                query = query.Where(x => x.Username.ToLower().Contains(request.keyword.ToLower())
                                        || SearchHelper.ConvertToUnSign(x.Username).ToLower().Contains(request.keyword.ToLower())).ToList();
            var items = _mapper.Map<IEnumerable<UserDto>>(query);
            return PagedResult<UserDto>.ToPagedList(items, request.PageIndex, request.PageSize);
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
                data.RoleId = 3;
                try
                {
                    await _dBContext.Users.AddAsync(data);
                    await _dBContext.SaveChangesAsync();
                    var userProfile = new UserProfile
                    {
                        Id = data.Id,
                    };
                    await _dBContext.UserProfiles.AddAsync(userProfile);
                    await _dBContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new CustomException($"Loi roi" + ex);

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
                }
            }
            else
            {
                throw new CustomException("Thong tin dang nhap khong dung");
            }
            
        }

        public  async Task Logout()
        {
            _sessionManager.Logout();
            return;
        }
    }
}
