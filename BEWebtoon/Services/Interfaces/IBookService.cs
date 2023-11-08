using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests.BookRequest;

namespace BEWebtoon.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAll();
        Task<BookDto> GetById(int id);
        Task CreateBook(CreateBookDto createBookDto);
        Task UpdateBook(UpdateBookDto updateBookDto);
        Task DeleteBook(int id);
        Task<PagedResult<BookDto>> GetBookPagination(BookRequest request);
    }
}
