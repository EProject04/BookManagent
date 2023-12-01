using AutoMapper;
using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.CommentsDto;
using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using Microsoft.EntityFrameworkCore;

namespace BEWebtoon.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        public CommentRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }
        public async Task CreateComment(CreateCommentDto createCommentDto)
        {
            if (_sessionManager.CheckLogin())
            {
                var book = await _dBContext.Books.FindAsync(createCommentDto.BookId);

                if (book == null)
                {
                    throw new CustomException("Book not found");
                }

                var userId = _sessionManager.GetSessionValueInt("UserId");

                var comment = new Comment
                {
                    CommentText = createCommentDto.CommentText,
                    Rate = createCommentDto.Rate,
                    BookId = createCommentDto.BookId,
                    UserId = userId
                };

                try
                {
                    _dBContext.Comments.Add(comment);
                    await _dBContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new CustomException("Failed to create a comment: " + ex.Message);
                }
            }
            else
            {
                throw new UnauthorizedAccessException("User is not logged in");
            }
        }

        public async Task DeleteComment(int id)
        {
            if (_sessionManager.CheckLogin())
            {
                var comment = await _dBContext.Comments.FindAsync(id);

                if (comment != null)
                {
                    _dBContext.Comments.Remove(comment);
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay comment voi id" + id);
                }
            }
        }

        public async Task<List<CommentDto>> GetAll()
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                List<CommentDto> commnetsDto = new List<CommentDto>();
                var comments = await _dBContext.Comments.ToListAsync();
                if (comments != null)
                {
                    commnetsDto = _mapper.Map<List<Comment>, List<CommentDto>>(comments);
                }
                return commnetsDto;
            }
            return null;
        }

        public async Task<CommentDto> GetById(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var comment = await _dBContext.Comments.FindAsync(id);
                if (comment != null)
                {

                    CommentDto commentDto = _mapper.Map<Comment, CommentDto>(comment);
                    return commentDto;

                }
                else
                {
                    throw new Exception("Khong tim thay comment");
                }
            }
            return null;
        }

        public async Task UpdateComment(UpdateCommentDto updateCommentDto)
        {
            if (_sessionManager.CheckLogin())
            {
                using var transaction = await _dBContext.Database.BeginTransactionAsync();

                var comment = await _dBContext.Comments.FindAsync(updateCommentDto.Id);

                if (comment == null)
                {
                    throw new CustomException("Comment not found");
                }

                var userId = _sessionManager.GetSessionValueInt("UserId");

                if (comment.UserId != userId)
                {
                    throw new CustomException("You don't have permission to update this comment");
                }

                var book = await _dBContext.Books.FindAsync(updateCommentDto.BookId);
                var user = await _dBContext.Users.FindAsync(updateCommentDto.UserId);

                if (book == null)
                {
                    throw new CustomException("Book not found");
                }

                if (user == null)
                {
                    throw new CustomException("User not found");
                }

                try
                {
                    _dBContext.Entry(comment).CurrentValues.SetValues(updateCommentDto);
                    await _dBContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new CustomException("Failed to update the comment: " + ex.Message);
                }
            }
            else
            {
                throw new CustomException("User is not logged in");
            }
        }

    }
}
