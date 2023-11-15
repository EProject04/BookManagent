using AutoMapper;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.FollowingsDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using IOCBEWebtoon.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BEWebtoon.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        public FollowingRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }

        public async Task<List<FollowingDto>> GetAll()
        {
            if (_sessionManager.CheckLogin())
            {
                List<FollowingDto> followingsDto = new List<FollowingDto>();
                var userId = _sessionManager.GetSessionValueInt("UserId");
                var followings = await _dBContext.Followings.Where(x => x.UserId == userId)
                                .Include(x => x.Books).ToListAsync();
                    followingsDto = _mapper.Map<List<Following>, List<FollowingDto>>(followings);
                return followingsDto;
            }
            else
            {
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");
            }
        }

        public async Task<FollowingDto> GetById(int id)
        {
            if (_sessionManager.CheckLogin())
            {
                var following = await _dBContext.Followings
                            .Include(x => x.Books).FirstOrDefaultAsync(b => b.Id == id);
                if (following != null)
                {
                    FollowingDto followingDto = _mapper.Map<Following, FollowingDto>(following);
                    return followingDto;
                }
                else
                {
                    throw new Exception("Khong tim thay sach");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");
            }
            
        }

        public async Task<PagedResult<FollowingDto>> GetFollowingPagination(SeacrhPagingRequest request)
        {
            if (_sessionManager.CheckLogin())
            {
                var userId = _sessionManager.GetSessionValueInt("UserId");
                var query = await _dBContext.Followings.Where(x=>x.UserId == userId)
                            .Include(x => x.Books).ToListAsync();
                var items = _mapper.Map<IEnumerable<FollowingDto>>(query);
                foreach (var item in items)
                {
                    if (item.Books != null)
                    {
                        if (!string.IsNullOrEmpty(request.keyword.TrimAndLower()))
                            item.Books = item.Books.Where(x => x.Title.ToLower().Contains(request.keyword.ToLower())
                                                    || SearchHelper.ConvertToUnSign(x.Title).ToLower().Contains(request.keyword.ToLower())).ToList();

                        item.Books = item.Books.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
                    }
                }
                return PagedResult<FollowingDto>.ToPagedList(items, request.PageIndex, request.PageSize);
            }
            else
            {
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");
            }
        }

        public async Task UpdateFollowing(UpdateFollowingDto updateFollowingDto)
        {
            if (_sessionManager.CheckLogin())
            {
                var userId = _sessionManager.GetSessionValueInt("UserId");
                var following = await _dBContext.Followings
               .Include(x => x.Books)
               .FirstOrDefaultAsync(b => b.UserId == userId && b.Id == updateFollowingDto.Id);


                if (following != null)
                {
                    var booksToFollow = await _dBContext.Books
                        .Where(book => updateFollowingDto.BookIds.Contains(book.Id))
                        .ToListAsync();

                    foreach (var book in following.Books.ToList())
                    {
                        if (!booksToFollow.Contains(book))
                        {
                            following.Books.Remove(book);
                        }
                    }

                    foreach (var book in booksToFollow)
                    {
                        if (!following.Books.Contains(book))
                        {
                            following.Books.Add(book);
                        }
                    }

                    await _dBContext.SaveChangesAsync();
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập");
            }
           
        }


    }
}
