using System.Collections.Generic;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Error;
using HRM.Models.Paginate;
using HRM.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userManager;
        public UserController(IUserService service)
        {
            _userManager = service;
        }

        //[Authorize]
        [ProducesResponseType(typeof(Paginate<GetUserDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetUserDTO>>> GetAllUsers([FromQuery] GetUsersFilter request)
        {
            var data = await _userManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetUserDTO>>(data, $"User List"));
        }
    }
}