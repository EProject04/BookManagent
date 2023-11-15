using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.CategoriesDto;
using BEWebtoon.DataTransferObject.FollowingsDto;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface IFollowingRepository
    {
        Task<List<BookDto>> GetFollowingBooks(int userprofileId);
        Task Following(int userprofileId, int bookId);
        Task UnFollowing(int userprofileId, int bookId);
    }
}
