using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests.BookRequest;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task CreateBook(CreateBookDto createBookDto)
        {
           await _bookRepository.CreateBook(createBookDto);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }

        public async Task<List<BookDto>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public Task<PagedResult<BookDto>> GetBookPagination(BookRequest request)
        {
            return _bookRepository.GetBookPagination(request);  
        }

        public Task<BookDto> GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public async Task UpdateBook(UpdateBookDto updateBookDto)
        {
           await _bookRepository.UpdateBook(updateBookDto);
        }
    }
}
