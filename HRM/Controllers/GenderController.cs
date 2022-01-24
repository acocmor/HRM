using System;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Error;
using HRM.Models.Gender;
using HRM.Models.Paginate;
using HRM.Models.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderManager;
        public GenderController(IGenderService service)
        {
            _genderManager = service;
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetGenderDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetGenderDTO>>> GetAll([FromQuery] GetGenderFilter request)
        {
            var data = await _genderManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetGenderDTO>>(data, $"Gender List"));
        }
        
                
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetGenderDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetGenderDTO>> GetById([FromRoute] Guid id)
        {
            var data = await _genderManager.GetById(id);
            return Ok(new BaseResponse<GetGenderDTO>(data, $"Gender Id: {id}"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(GetGenderDTO), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<GetGenderDTO>> Create([FromBody] CreateGenderDTO request)
        {
            var data = await _genderManager.Create(request);
            return Ok(new BaseResponse<GetGenderDTO>(data, $"Create gender"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetGenderDTO>), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] Guid id, [FromBody] UpdateGenderDTO request)
        {
            var data = await _genderManager.Update(id, request);
            return Ok(new BaseResponse<bool>(data, $"Update Gender Id: {id}"));
        }

        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetGenderDTO>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid id)
        {
            var data = await _genderManager.Delete(id);
            return Ok(new BaseResponse<bool>(data, $"Delete gender: {data}"));
        }
    }
}