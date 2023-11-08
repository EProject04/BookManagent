using BEWebtoon.DataTransferObject.CommentsDto;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class CommentService : ICommentService
    { 
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {

            _commentRepository = commentRepository;

        }
        public async Task CreateComment(CreateCommentDto createCommentDto)
        {
            await _commentRepository.CreateComment(createCommentDto);
        }

        public async Task DeleteComment(int id)
        {
            await _commentRepository.DeleteComment(id);
        }

        public async Task<List<CommentDto>> GetAll()
        {
            return await _commentRepository.GetAll();
        }

        public async Task<CommentDto> GetById(int id)
        {
            return await _commentRepository.GetById(id);
        }

        public async Task UpdateComment(UpdateCommentDto updateCommentDto)
        {
           await _commentRepository.UpdateComment(updateCommentDto);
        }
    }
}
