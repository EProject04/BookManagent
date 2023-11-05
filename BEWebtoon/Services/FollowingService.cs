using Azure.Core;
using BEWebtoon.DataTransferObject.FollowingsDto;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class FollowingService : IFollowingService
    {
        private readonly IFollowingRepository _repository;
        public FollowingService(IFollowingRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<FollowingDto>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<FollowingDto> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<PagedResult<FollowingDto>> GetFollowingPagination(SeacrhPagingRequest request)
        {
            return await _repository.GetFollowingPagination(request);
        }

        public async Task UpdateFollowing(UpdateFollowingDto updateFollowingDto)
        {
            await _repository.UpdateFollowing(updateFollowingDto);
        }
    }
}
