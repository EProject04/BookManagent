using Azure.Core;
using BEWebtoon.DataTransferObject.BooksDto;
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

        public async Task<List<BookDto>> GetFollowingBooks(int userprofileId)
        {
            return await _repository.GetFollowingBooks(userprofileId);
        }

        public async Task Following(int userprofileId, int bookId)
        {
            await _repository.Following(userprofileId, bookId);
        }

        public async Task UnFollowing(int userprofileId, int bookId)
        {
            await _repository.UnFollowing(userprofileId, bookId);
        }
    }
}
