using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.Requests;
using BEWebtoon.Services;
using BEWebtoon.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEWebtoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleServices;
        public RolesController(IRoleService roleService) 
        {
            _roleServices = roleService;
        }
        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            try
            {
                return await _roleServices.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRolesById(int id)
        {
            try
            {
                return await _roleServices.GetById(id);
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
                var events = await _roleServices.GetRolePagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto RoleDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _roleServices.GetById(RoleDto.Id);
                if (data == null)
                    return NotFound();
                await _roleServices.UpdateRole(RoleDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoles(CreateRoleDto Roles)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _roleServices.CreateRole(Roles);
                return Ok(Roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            try
            {
                var data = await _roleServices.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay nguoi dung");
                await _roleServices.DeleteRole(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
