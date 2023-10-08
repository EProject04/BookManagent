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
            var checkRoleId = _sessionManager.GetSessionValue("RoleId");
            if (checkRoleId == "1")
            {

                var data = _mapper.Map<User>(userDto);
                try
                {
                    await _dBContext.Users.AddAsync(data);
                    await _dBContext.SaveChangesAsync();
                }catch(Exception ex) 
                {
                    throw new CustomException("Nguoi dung da ton tai");
                }
            }
            else
            {
                throw new CustomException("Ban chua duoc phan quyen");
            }
        }

        public async Task DeleteUser(int id)
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

        public async Task<List<UserDto>> GetAll()
        {
            List<UserDto> usersDto = new List<UserDto>();
            var users = await _dBContext.Users.Include(u => u.Roles).ToListAsync();
            if(users != null)
            {
                usersDto = _mapper.Map<List<User>,List<UserDto>>(users);
            }
            return usersDto;
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

        public async Task UpdateUser(UpdateUserDto userDto)
        {
            var user = await _dBContext.Users.Where(x=>x.Id == userDto.Id).FirstOrDefaultAsync();
            if(user != null)
            {
                user.Email = userDto.Email;
                user.Username = userDto.Username;
                user.Password = userDto.Password;
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Khong tim thay nguoi dung");
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
