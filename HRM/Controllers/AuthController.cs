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
        [HttpPost("login")]
        public async Task<ActionResult<TokenModel>> Login([FromBody] LoginDTO request)
        {
            var data = await _authressManager.Login(request);
            return Ok(new BaseResponse<TokenModel>(data, $"Login Success"));
        }
        
        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout([FromBody] TokenModel tokenModel)
        {
            var data = await _authressManager.Logout(tokenModel);
            return Ok(new BaseResponse<bool>(data));
        }
            
        [HttpPost("renew-token")]
        public async Task<ActionResult<TokenModel>> RenewToken([FromBody] TokenModel tokenModel)
        {
            var data = await _authressManager.RenewToken(tokenModel);
            return Ok(new BaseResponse<TokenModel>(data, $"Refresh Token Success"));
        }
    }
}