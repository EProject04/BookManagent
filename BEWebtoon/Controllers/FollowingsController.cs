using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.FollowingsDto;
using BEWebtoon.Requests;
using BEWebtoon.Requests.BookRequest;
using BEWebtoon.Services;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("followings")]
    [ApiController]
    public class FollowingsController : ControllerBase
    {
        private readonly IFollowingService _followingService;
        public FollowingsController(IFollowingService service)
        {
            _followingService = service;
        }
        [HttpGet("get-all-following")]
        public async Task<ActionResult<List<FollowingDto>>> GetAll()
        {
            try
            {
                return await _followingService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("get-following-by-id/{id}")]
        public async Task<ActionResult<FollowingDto>> GetfollowingsById(int id)
        {
            try
            {
                return await _followingService.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAllPaging([FromQuery] SeacrhPagingRequest request)
        {
            try
            {
                var events = await _followingService.GetFollowingPagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        [HttpPut("update-following")]
        public async Task<IActionResult> Updatefollowing(UpdateFollowingDto followingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _followingService.GetById(followingDto.Id);
                if (data == null)
                    return NotFound();
                await _followingService.UpdateFollowing(followingDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
