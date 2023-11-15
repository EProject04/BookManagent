using BEWebtoon.DataTransferObject.CategoriesDto;
using BEWebtoon.DataTransferObject.FollowingsDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface IFollowingRepository
    {
        Task<List<FollowingDto>> GetAll();
        Task<FollowingDto> GetById(int id);
        Task UpdateFollowing(UpdateFollowingDto updateFollowingDto);
        Task<PagedResult<FollowingDto>> GetFollowingPagination(SeacrhPagingRequest request);
    }
}
