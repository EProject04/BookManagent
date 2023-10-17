using AutoMapper;
using BEWebtoon.DataTransferObject.AuthorDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using IOCBEWebtoon.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BEWebtoon.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        public AuthorRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }
        public async Task DeleteAuthor(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var author = await _dBContext.Authors.FindAsync(id);
                if (author != null)
                {
                    _dBContext.Authors.Remove(author);
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
        }

        public async Task<List<AuthorDto>> GetAll()
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                List<AuthorDto> authorsDto = new List<AuthorDto>();
                var authors = await _dBContext.Authors.ToListAsync();
                if (authors != null)
                {
                    authorsDto = _mapper.Map<List<Author>, List<AuthorDto>>(authors);
                }
                return authorsDto;
            }
            return null;
        }

        public async Task<PagedResult<AuthorDto>> GetAuthorPagination(SeacrhPagingRequest request)
        {
                var query = await _dBContext.Authors.ToListAsync();
                if (!string.IsNullOrEmpty(request.keyword))
                    query = query.Where(x => x.AuthorName.ToLower().Contains(request.keyword.ToLower())
                                            || SearchHelper.ConvertToUnSign(x.AuthorName).ToLower().Contains(request.keyword.ToLower())).ToList();
                var items = _mapper.Map<IEnumerable<AuthorDto>>(query);
                return PagedResult<AuthorDto>.ToPagedList(items, request.PageIndex, request.PageSize);
        }

        public async Task<AuthorDto> GetById(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var author = await _dBContext.Authors.FindAsync(id);
                if (author != null)
                {

                    AuthorDto authorDto = _mapper.Map<Author, AuthorDto>(author);
                    return authorDto;

                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
            return null;
        }

        public async Task UpdateAuthor(UpdateAuthorDto updateAuthorDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var author = await _dBContext.Authors.Where(x => x.Id == updateAuthorDto.Id).FirstOrDefaultAsync();
                if (author != null)
                {
                    author.AuthorName = updateAuthorDto.AuthorName;
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
        }
    }
}
