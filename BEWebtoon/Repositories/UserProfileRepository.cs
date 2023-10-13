using AutoMapper;
using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Requests.UserProfileRequest;
using BEWebtoon.WebtoonDBContext;
using IOC.ApplicationLayer.Utilities;
using IOCBEWebtoon.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BEWebtoon.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        public UserProfileRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }

        public async Task DeleteUserProfile(int id)
        {
            var user = await _dBContext.UserProfiles.FindAsync(id);
            if (user != null)
            {
                _dBContext.UserProfiles.Remove(user);
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Khong tim thay nguoi dung");
            }
        }

        public async Task<List<UserProfileDto>> GetAll()
        {
            List<UserProfileDto> usersDto = new List<UserProfileDto>();
            var users = await _dBContext.UserProfiles.ToListAsync();
            if (users != null)
            {
                usersDto = _mapper.Map<List<UserProfile>, List<UserProfileDto>>(users);
            }
            return usersDto;
        }

        public async Task<UserProfileDto> GetById(int id)
        {
            var userProfile = await _dBContext.UserProfiles.FindAsync(id);
            if (userProfile != null)
            {

                UserProfileDto userProfileDto = _mapper.Map<UserProfile, UserProfileDto>(userProfile);
                return userProfileDto;

            }
            else
            {
                throw new Exception("Khong tim thay nguoi dung");
            }
        }

        public async Task<PagedResult<UserProfileDto>> GetUserProfilePagination(UserProfileRequest request)
        {
            var query = await _dBContext.UserProfiles.Include(x => x.Authors).ToListAsync();
            if (!string.IsNullOrWhiteSpace(request.keyword.TrimAndLower()))
            {
                query = query.Where(x => x.FullName.TrimAndLower().Contains(request.keyword.TrimAndLower())
                                        || SearchHelper.ConvertToUnSign(x.FullName).TrimAndLower().Contains(request.keyword.TrimAndLower())
                                        || x.Address.TrimAndLower().Contains(request.keyword.TrimAndLower())
                                        || x.PhoneNumber.TrimAndLower().Contains(request.keyword.TrimAndLower())
                                        || x.FistName.TrimAndLower().Contains(request.keyword.TrimAndLower())
                                        || x.LastName.TrimAndLower().Contains(request.keyword.TrimAndLower())
                                        || x.Email.TrimAndLower().Contains(request.keyword.TrimAndLower())).ToList();
                if (request.DateOfBirth != null)
                {
                    query = query.Where(x => x.DateOfBirth?.Year == request.DateOfBirth.Value.Year
                                          && x.DateOfBirth?.Month == request.DateOfBirth.Value.Month
                                          && x.DateOfBirth?.Day == request.DateOfBirth.Value.Day).ToList();
                }
            }
            var items = _mapper.Map<IEnumerable<UserProfileDto>>(query);
            return PagedResult<UserProfileDto>.ToPagedList(items, request.PageIndex, request.PageSize);
        }

        public async Task UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            var userProfile = await _dBContext.UserProfiles.Where(x => x.Id == updateUserProfileDto.Id).FirstOrDefaultAsync();
            var data = _mapper.Map<UserProfile>(userProfile);
            if (updateUserProfileDto.File != null && updateUserProfileDto.File.Length > 0)
            {
               
                if (updateUserProfileDto.ImagePath != null)
                {
                    if (File.Exists(Path.Combine(updateUserProfileDto.ImagePath)))
                        File.Delete(Path.Combine(updateUserProfileDto.ImagePath));
                }
                data.ImagePath = await FileHelper.SaveFile(updateUserProfileDto.File, "UserProfileImage");
            }
            await _dBContext.SaveChangesAsync();
        }
    }
}
