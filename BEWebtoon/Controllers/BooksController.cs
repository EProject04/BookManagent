using BEWebtoon.DataTransferObject.BooksDto;
using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.DataTransferObject.UserProfilesDto;
using BEWebtoon.Requests.BookRequest;
using BEWebtoon.Requests.UserProfileRequest;
using BEWebtoon.Services;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAll()
        {
            try
            {
                return await _bookService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBooksById(int id)
        {
            try
            {
                return await _bookService.GetById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooks([FromForm] CreateBookDto books)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _bookService.CreateBook(books);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAllPaging([FromQuery] BookRequest request)
        {
            try
            {
                var events = await _bookService.GetBookPagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookDto BookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _bookService.GetById(BookDto.Id);
                if (data == null)
                    return NotFound();
                await _bookService.UpdateBook(BookDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            try
            {
                var data = await _bookService.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay nguoi dung");
                await _bookService.DeleteBook(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
