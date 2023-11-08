using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests.UserProfileRequest;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repository;
        public UserProfileService(IUserProfileRepository repository)
        {
            _repository= repository;
        }
        public async Task DeleteUserProfile(int id)
        {
            await _repository.DeleteUserProfile(id);
        }

        public async Task<List<UserProfileDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public Task<UserProfileDto> GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Task<PagedResult<UserProfileDto>> GetUserProfilePagination(UserProfileRequest request)
        {
            return _repository.GetUserProfilePagination(request);
        }

        public async Task UpdateUserProfile(UpdateUserProfileDto UserProfile)
        {
            await _repository.UpdateUserProfile(UserProfile);
        }
    }
}
