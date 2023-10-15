using AutoMapper;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Requests.BookRequest;
using BEWebtoon.WebtoonDBContext;
using IOC.ApplicationLayer.Utilities;
using IOCBEWebtoon.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEWebtoon.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        public BookRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }
        [HttpGet]
        public async Task CreateBook(CreateBookDto createBookDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.AdminAuthor))
            {
                var data = _mapper.Map<Book>(createBookDto);
                try
                {
                    await _dBContext.Books.AddAsync(data);
                    await _dBContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new CustomException($"Danh muc da ton tai" + ex);
                }
            }
        }
        public async Task DeleteBook(int id)
        {
            var book = await _dBContext.Books.FindAsync(id);
            if (book != null)
            {
                _dBContext.Books.Remove(book);
                await _dBContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Khong tim thay sach voi id" + id);
            }
        }

        public async Task<List<BookDto>> GetAll()
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.AdminAuthor))
            {
                List<BookDto> booksDto = new List<BookDto>();
                var books = await _dBContext.Books.ToListAsync();
                if (books != null)
                {
                    booksDto = _mapper.Map<List<Book>, List<BookDto>>(books);
                }
                return booksDto;
            }
            return null;
        }

        public async Task<PagedResult<BookDto>> GetBookPagination(BookRequest request)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.AdminAuthor))
            {
                var query = await _dBContext.Books.ToListAsync();
                if (!string.IsNullOrEmpty(request.keyword))
                    query = query.Where(x => x.Title.ToLower().Contains(request.keyword.ToLower())
                                            || SearchHelper.ConvertToUnSign(x.Title).ToLower().Contains(request.keyword.ToLower())).ToList();
                if (request.CategoryName != null)
                {
                    query = query.Where(x => x.CategoryBooks.Any(cb => cb.CategoryBookName.TrimAndLower().Contains(request.CategoryName.TrimAndLower()))).ToList();
                }
                var items = _mapper.Map<IEnumerable<BookDto>>(query);
                return PagedResult<BookDto>.ToPagedList(items, request.PageIndex, request.PageSize);
            }
            return null;
        }

        public async Task<BookDto> GetById(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.AdminAuthor))
            {
                var book = await _dBContext.Books.FindAsync(id);
                if (book != null)
                {

                    BookDto bookDto = _mapper.Map<Book, BookDto>(book);
                    return bookDto;

                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
            return null;
        }

        public async Task UpdateBook(UpdateBookDto updateBookDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.AdminAuthor))
            {
                var book = await _dBContext.Books.Where(x => x.Id == updateBookDto.Id).FirstOrDefaultAsync();
                var data = _mapper.Map<Book>(book);
                if (updateBookDto.File != null && updateBookDto.File.Length > 0)
                {

                    if (updateBookDto.ImagePath != null)
                    {
                        if (File.Exists(Path.Combine(updateBookDto.ImagePath)))
                            File.Delete(Path.Combine(updateBookDto.ImagePath));
                    }
                    data.ImagePath = await FileHelper.SaveFile(updateBookDto.File, "BookImage");
                }
                await _dBContext.SaveChangesAsync();
            }
        }
    }
}
