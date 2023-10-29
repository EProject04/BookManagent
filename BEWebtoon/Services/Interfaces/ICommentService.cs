using BEWebtoon.DataTransferObject.CommentsDto;

namespace BEWebtoon.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAll();
        Task<CommentDto> GetById(int id);
        Task CreateComment(CreateCommentDto createCommentDto);
        Task UpdateComment(UpdateCommentDto updateCommentDto);
        Task DeleteComment(int id);
    }
}
