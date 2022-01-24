using System.Threading.Tasks;
using HRM.Core.Interfaces;
using HRM.Models.Address;
using HRM.Models.Auth;
using HRM.Models.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HRM.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authressManager;

        public AuthController(IAuthService service)
        {
            _authressManager = service;
        }
        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<ActionResult<string>> GetById([FromBody] LoginDTO request)
        {
            var data = await _authressManager.Login(request);
            return Ok(new BaseResponse<string>(data, $"Login Success"));
        }
    }
}