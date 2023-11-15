using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.FollowingsDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Services.Interfaces
{
    public interface IFollowingService
    {
        Task<List<BookDto>> GetFollowingBooks(int userprofileId);
        Task Following(int userprofileId, int bookId);
        Task UnFollowing(int userprofileId, int bookId);
    }
}
