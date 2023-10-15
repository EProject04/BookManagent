using BEWebtoon.DataTransferObject.AuthorDto;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            try
            {
                return await _authorService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorsById(int id)
        {
            try
            {
                return await _authorService.GetById(id);
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
                var events = await _authorService.GetAuthorPagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromForm] UpdateAuthorDto AuthorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _authorService.GetById(AuthorDto.Id);
                if (data == null)
                    return NotFound();
                await _authorService.UpdateAuthor(AuthorDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthors(int id)
        {
            try
            {
                var data = await _authorService.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay nguoi dung");
                await _authorService.DeleteAuthor(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
