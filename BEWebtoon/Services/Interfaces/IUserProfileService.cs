using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests.UserProfileRequest;

namespace BEWebtoon.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<List<UserProfileDto>> GetAll();
        Task<UserProfileDto> GetById(int id);
        Task UpdateUserProfile(UpdateUserProfileDto UserProfile);
        Task DeleteUserProfile(int id);
        Task<PagedResult<UserProfileDto>> GetUserProfilePagination(UserProfileRequest request);
    }
}
