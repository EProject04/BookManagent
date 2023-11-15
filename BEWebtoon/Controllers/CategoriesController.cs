using BEWebtoon.DataTransferObject.CategoriesDto;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            try
            {
                return await _categoryService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategorysById(int id)
        {
            try
            {
                return await _categoryService.GetById(id);
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
                var events = await _categoryService.GetCategoryPagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _categoryService.GetById(updateCategoryDto.Id);
                if (data == null)
                    return NotFound();
                await _categoryService.UpdateCategory(updateCategoryDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategorys([FromForm] CreateCategoryDto creatCategoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _categoryService.CreateCategory(creatCategoryDto);
                return Ok(creatCategoryDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorys(int id)
        {
            try
            {
                var data = await _categoryService.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay danh muc");
                await _categoryService.DeleteCategory(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
