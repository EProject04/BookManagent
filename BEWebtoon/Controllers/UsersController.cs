using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BEWebtoon.Models;
using BEWebtoon.WebtoonDBContext;
using BEWebtoon.Services;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Requests;

namespace BEWebtoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
          return await _userService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUsersById(int id)
        {
            return await _userService.GetById(id);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAllPaging([FromQuery] SeacrhPagingRequest request)
        {
            var events = await _userService.GetUserPagination(request);
            return Ok(events);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Du lieu khong hop le");
            var data = await _userService.GetById(userDto.Id);
            if (data == null)
                return NotFound();
            await _userService.UpdateUser(userDto);
            return Ok(200);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateUsers(CreateUserDto users)
        {
          if(!ModelState.IsValid) 
            return BadRequest("Du lieu khong hop le");
          await _userService.CreateUser(users);
          return Ok(200);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
           var data = await _userService.GetById(id);
            if (data == null)
                return BadRequest("Khong tim thay nguoi dung");
            await _userService.DeleteUser(id);
            return Ok(200); 
            
        }
    }
}
