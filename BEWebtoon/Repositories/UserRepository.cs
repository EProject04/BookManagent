using AutoMapper;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using IOCBEWebtoon.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace BEWebtoon.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        public UserRepository(WebtoonDbContext dbContext, IMapper mapper) {
            _dBContext = dbContext;
            _mapper = mapper;
        }
        public async Task CreateUser(CreateUserDto userDto)
        {
            var data = _mapper.Map<Users>(userDto);
            try
            {
                data.FullName = userDto.FisrtName + " " +userDto.LastName;

                await _dBContext.Users.AddAsync(data);
                await _dBContext.SaveChangesAsync();
            }catch(Exception ex) 
            {
                throw new Exception(ex.Message);
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
            var users = await _dBContext.Users.ToListAsync();
            if(users != null)
            {
                usersDto = _mapper.Map<List<Users>,List<UserDto>>(users);
            }
            return usersDto;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _dBContext.Users.FindAsync(id);
            if(user!=null)
            {
                UserDto userDto = _mapper.Map<Users, UserDto>(user);
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
                user.FisrtName = userDto.FisrtName;
                user.LastName = userDto.LastName;
                user.FullName = userDto.FullName;
                user.Address = userDto.Address;
                user.Email = userDto.Email;
                user.Username = userDto.Username;
                user.Password = userDto.Password;
                user.DoB = userDto.DoB;
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Khong tim thay nguoi dung");
            }
        }
        public static string RemoveDiacritics(string text)
        {
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }

        public async Task<PagedResult<UserDto>> GetUserPagination(SeacrhPagingRequest request)
        {
            var query = await _dBContext.Users.ToListAsync();
            if (!string.IsNullOrEmpty(request.keyword))
                query = query.Where(x => x.FisrtName.ToLower().Contains(request.keyword.ToLower())
                                        || SearchHelper.ConvertToUnSign(x.FisrtName).ToLower().Contains(request.keyword.ToLower())).ToList();
            var items = _mapper.Map<IEnumerable<UserDto>>(query);
            return PagedResult<UserDto>.ToPagedList(items, request.PageIndex, request.PageSize);
        }
    }
}
