using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.Requests;
using BEWebtoon.Requests.UserProfileRequest;
using BEWebtoon.Services;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        public UserProfilesController(IUserProfileService  userProfileService)
        {
            _userProfileService= userProfileService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserProfileDto>>> GetAll()
        {
            try
            {
                return await _userProfileService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfilesById(int id)
        {
            try
            {
                return await _userProfileService.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAllPaging([FromQuery] UserProfileRequest request)
        {
            try
            {
                var events = await _userProfileService.GetUserProfilePagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile([FromForm]UpdateUserProfileDto userProfileDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _userProfileService.GetById(userProfileDto.Id);
                if (data == null)
                    return NotFound();
                await _userProfileService.UpdateUserProfile(userProfileDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfiles(int id)
        {
            try
            {
                var data = await _userProfileService.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay nguoi dung");
                await _userProfileService.DeleteUserProfile(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
