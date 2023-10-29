using BEWebtoon.DataTransferObject.CommentsDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetAll();
        Task<CommentDto> GetById(int id);
        Task CreateComment(CreateCommentDto createCommentDto);
        Task UpdateComment(UpdateCommentDto updateCommentDto);
        Task DeleteComment(int id);
    }
}
