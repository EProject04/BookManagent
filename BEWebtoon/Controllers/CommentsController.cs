using BEWebtoon.DataTransferObject.CommentsDto;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService CommentService)
        {
            _commentService = CommentService;
        }
        [HttpGet("get-all-comment")]
        public async Task<ActionResult<List<CommentDto>>> GetAll()
        {
            try
            {
                return await _commentService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("get-comment-by-id/{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentsById(int id)
        {
            try
            {
                return await _commentService.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("create-comment")]
        public async Task<IActionResult> CreateComments( CreateCommentDto Comments)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _commentService.CreateComment(Comments);
                return Ok(Comments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPut("update-comment/{id}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto CommentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _commentService.GetById(CommentDto.Id);
                if (data == null)
                    return NotFound();
                await _commentService.UpdateComment(CommentDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> DeleteComments(int id)
        {
            try
            {
                var data = await _commentService.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay nguoi dung");
                await _commentService.DeleteComment(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
