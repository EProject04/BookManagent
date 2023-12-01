using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BEWebtoon.Models;
using BEWebtoon.WebtoonDBContext;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;
using Azure.Core;
using AutoMapper;
using BEWebtoon.Helpers;

namespace BEWebtoon.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;

        public UsersController(IUserService userService, IMapper mapper, SessionManager sessionManager )
        {
            _userService = userService;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }

        [HttpGet("get-all-user")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            try
            {
                return await _userService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<ActionResult<UserDto>> GetUsersById(int id)
        {
            try
            {
                return await _userService.GetById(id);
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
                var events = await _userService.GetUserPagination(request);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _userService.ForgotPassword(forgotPasswordDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }


        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUsers(CreateUserDto users)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _userService.CreateUser(users);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto users)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _userService.RegisterUser(users);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto users)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                await _userService.LoginUser(users);
                var userId = _sessionManager.GetSessionValueInt("UserId");
                var userProfile = await _userService.GetById(userId);
                LoginRequestBody loginBody = _mapper.Map<UserDto, LoginRequestBody>(userProfile);
                return Ok(loginBody);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.Logout();
                return StatusCode(200);
            }catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            try
            {
                var data = await _userService.GetById(id);
                if (data == null)
                    return BadRequest("Khong tim thay nguoi dung");
                await _userService.DeleteUser(id);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Du lieu khong hop le");
                var data = await _userService.GetById(userDto.Id);
                if (data == null)
                    return NotFound();
                await _userService.UpdateUser(userDto);
                return Ok(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
