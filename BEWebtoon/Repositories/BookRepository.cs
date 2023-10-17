using AutoMapper;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests.BookRequest;
using BEWebtoon.WebtoonDBContext;
using IOC.ApplicationLayer.Utilities;
using IOCBEWebtoon.Utilities;
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
        public async Task CreateBook(CreateBookDto createBookDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.AdminAuthor))
            {
                var data = _mapper.Map<Book>(createBookDto);

                var userId = _sessionManager.GetSessionValueInt("UserId");
                var userProfile = await _dBContext.UserProfiles.Where(x => x.Id == userId).Include(x => x.Authors).FirstOrDefaultAsync();

                if (userProfile != null)
                {
                    var author = await _dBContext.Authors.FindAsync(userProfile.AuthorId);

                    if (author != null)
                    {
                        var bookFollow = new BookFollow
                        {
                            AuthorId = userProfile.AuthorId,
                            Authors = author,
                            Books = data
                        };

                        if (data.BookFollows == null)
                        {
                            data.BookFollows = new List<BookFollow>();
                        }

                        data.BookFollows.Add(bookFollow);
                    }
                }

                await ProcessBookData(data, createBookDto);

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
                var books = await _dBContext.Books
                    .Include(x=>x.BookFollows)
                    .ThenInclude(x=>x.Authors)
                    .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Categories)
                    .ToListAsync();
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
            var query = await _dBContext.Books
                    .Include(x => x.BookFollows)
                    .ThenInclude(x => x.Authors)
                    .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Categories).ToListAsync();
            if (!string.IsNullOrEmpty(request.keyword.TrimAndLower()))
                query = query.Where(x => x.Title.ToLower().Contains(request.keyword.ToLower())
                                        || SearchHelper.ConvertToUnSign(x.Title).ToLower().Contains(request.keyword.ToLower())).ToList();
            if (!string.IsNullOrEmpty(request.AuthorName.TrimAndLower()))
            {
                query = query.Where(b => b.BookFollows.Any(bf => bf.Authors.AuthorName.TrimAndLower().Contains(request.AuthorName.TrimAndLower())
                                                           || SearchHelper.ConvertToUnSign(bf.Authors.AuthorName).TrimAndLower().Contains(request.AuthorName.TrimAndLower()))).ToList();
            }
            if (!string.IsNullOrEmpty(request.CategoryName.TrimAndLower()))
            {
                query = query.Where(b => b.CategoryBooks.Any(bf => bf.Categories.CategoryName.TrimAndLower().Contains(request.CategoryName.TrimAndLower())
                                                           || SearchHelper.ConvertToUnSign(bf.Categories.CategoryName).TrimAndLower().Contains(request.CategoryName.TrimAndLower()))).ToList();
            }
            var items = _mapper.Map<IEnumerable<BookDto>>(query);
            return PagedResult<BookDto>.ToPagedList(items, request.PageIndex, request.PageSize);
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
                var book = await _dBContext.Books.Where(x => x.Id == updateBookDto.Id).Include(a=>a.CategoryBooks).Include(w=>w.BookFollows).FirstOrDefaultAsync();
                if(book!= null)
                {
                    await ProcessBookData(book, updateBookDto);
                    await _dBContext.SaveChangesAsync();
                }
            }
        }
        private async Task ProcessBookData(Book book, CreateOrUpdateBookDto bookDto)
        {
            book.CategoryBooks = bookDto.CategoryId.Select(categoryId => new CategoryBook { CategoryId = categoryId }).ToList();
            book.BookFollows = bookDto.AuthorId.Select(authorId => new BookFollow { AuthorId = authorId }).ToList();

            if (bookDto.File != null && bookDto.File.Length > 0)
            {
                if (bookDto.ImagePath != null)
                {
                    if (File.Exists(Path.Combine(bookDto.ImagePath)))
                        File.Delete(Path.Combine(bookDto.ImagePath));
                }
                book.ImagePath = await FileHelper.SaveFile(bookDto.File, "BookImage");
            }
        }

    }
}
