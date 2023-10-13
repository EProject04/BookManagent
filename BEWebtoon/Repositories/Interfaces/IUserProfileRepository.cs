using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;
using BEWebtoon.Requests.UserProfileRequest;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfileDto>> GetAll();
        Task<UserProfileDto> GetById(int id);
        Task UpdateUserProfile(UpdateUserProfileDto UserProfile);
        Task DeleteUserProfile(int id);
        Task<PagedResult<UserProfileDto>> GetUserProfilePagination(UserProfileRequest request);
    }
}
