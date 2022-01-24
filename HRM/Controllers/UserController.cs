using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRM.Core.Exceptions;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Error;
using HRM.Models.Paginate;
using HRM.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userManager;
        public UserController(IUserService service)
        {
            _userManager = service;
        }
        
        [ProducesResponseType(typeof(Paginate<GetUserDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetUserDTO>>> GetAll([FromQuery] GetUsersFilter request)
        {
            var data = await _userManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetUserDTO>>(data, $"User List"));
        }
        
        [ProducesResponseType(typeof(Paginate<GetUserDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetById([FromRoute] Guid id)
        {
            var data = await _userManager.GetById(id);
            return Ok(new BaseResponse<GetUserDTO>(data, $"User Id: {id}"));
        }
        
        [ProducesResponseType(typeof(Paginate<GetUserDTO>), StatusCodes.Status200OK)]
        [HttpPost()]
        public async Task<ActionResult<GetUserDTO>> Create([FromBody] CreateUserDTO request)
        {
            var data = await _userManager.Create(request);
            return Ok(new BaseResponse<GetUserDTO>(data, $"Create User"));
        }
        
        [ProducesResponseType(typeof(Paginate<GetUserDTO>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid id)
        {
            var data = await _userManager.Delete(id);
            return Ok(new BaseResponse<bool>(data, $"Delete user: {data}"));
        }
    }
}