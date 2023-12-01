using AutoMapper;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.CommentsDto;
using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests.UserProfileRequest;
using BEWebtoon.WebtoonDBContext;
using IOC.ApplicationLayer.Utilities;
using IOCBEWebtoon.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BEWebtoon.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        private readonly IWebHostEnvironment _env;
        public UserProfileRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager, IWebHostEnvironment env)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
            _env = env;
        }

        public async Task DeleteUserProfile(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
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
        }

        public async Task<List<UserProfileDto>> GetAll()
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                List<UserProfileDto> usersDto = new List<UserProfileDto>();
                var users = await _dBContext.UserProfiles.Include(x=>x.Users).ThenInclude(x=>x.Roles).ToListAsync();
                if (users != null)
                {
                    usersDto = _mapper.Map<List<UserProfile>, List<UserProfileDto>>(users);
                }
                return usersDto;
            }
            return null;
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
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
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
            return null;
        }

        public async Task UpdateUserProfile(UpdateUserProfileDto updateUserProfileDto)
        {
            if (_sessionManager.CheckLogin())
            {
                using var transaction = await _dBContext.Database.BeginTransactionAsync();

                var userProfile = await _dBContext.UserProfiles.Include(x => x.Users).Where(x => x.Id == updateUserProfileDto.Id).FirstOrDefaultAsync();
                var data = _mapper.Map<UserProfile>(userProfile);

                if (updateUserProfileDto.File != null && updateUserProfileDto.File.Length > 0)
                {
                    string oldImageName = ImageHelper.UserAvatarName(updateUserProfileDto.Id);
                    string oldImagePath = Path.Combine(_env.ContentRootPath, "wwwroot/resource/userprofile/images", oldImageName);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                    string newImageName = ImageHelper.UserAvatarName(updateUserProfileDto.Id);
                    string newImagePath = Path.Combine(_env.ContentRootPath, "wwwroot/resource/userprofile/images", newImageName);
                    using (var fileStream = new FileStream(newImagePath, FileMode.Create, FileAccess.Write))
                    {
                        await updateUserProfileDto.File.CopyToAsync(fileStream);
                    }
                    data.ImagePath = ImageHelper.UserprofileImageUri(newImageName);
                }
                if (updateUserProfileDto.LastName != null || updateUserProfileDto.FirstName != null)
                {
                    data.FistName = updateUserProfileDto.FirstName ?? data.FistName;

                    data.LastName = updateUserProfileDto.LastName ?? data.LastName;

                    data.FullName = $"{data.FistName} {data.LastName}";
                }
                if (userProfile.Users != null)
                {
                    userProfile.Users.Email = updateUserProfileDto.Email ?? userProfile.Users.Email;
                }
                await _dBContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
               
        }
    }
}
