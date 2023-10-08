using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
            _userRepository= userRepository;
        }

        public async Task CreateUser(CreateUserDto user)
        {
           await _userRepository.CreateUser(user);
        }

        public async Task DeleteUser(int id)
        {
           await _userRepository.DeleteUser(id);
        }

        public async Task<List<UserDto>> GetAll()
        {
            return  await _userRepository.GetAll();
        }

        public async Task<UserDto> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public Task<PagedResult<UserDto>> GetUserPagination(SeacrhPagingRequest request)
        {
            return _userRepository.GetUserPagination(request);
        }

        public async Task LoginUser(LoginUserDto userDto)
        {
            await _userRepository.LoginUser(userDto);
        }

        public async Task Logout()
        {
            await _userRepository.Logout();
        }

        public async Task RegisterUser(RegisterUserDto userDto)
        {
            await _userRepository.RegisterUser(userDto);
        }

        public async Task UpdateUser(UpdateUserDto user)
        {
           await _userRepository.UpdateUser(user);
        }
    }
}
