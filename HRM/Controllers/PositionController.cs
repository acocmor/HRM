using System;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Error;
using HRM.Models.Paginate;
using HRM.Models.Position;
using HRM.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionManager;
        public PositionController(IPositionService service)
        {
            _positionManager = service;
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetPositionDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetPositionDTO>>> GetAll([FromQuery] GetPositionFilter request)
        {
            var data = await _positionManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetPositionDTO>>(data, $"Position List"));
        }
        
                
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetPositionDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPositionDTO>> GetById([FromRoute] Guid id)
        {
            var data = await _positionManager.GetById(id);
            return Ok(new BaseResponse<GetPositionDTO>(data, $"Position Id: {id}"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(GetPositionDTO), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<GetPositionDTO>> Create([FromBody] CreatePositionDTO request)
        {
            var data = await _positionManager.Create(request);
            return Ok(new BaseResponse<GetPositionDTO>(data, $"Create position"));
        }
        
        //[Authorize]
        [ProducesResponseType(typeof(Paginate<GetPositionDTO>), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] Guid id, [FromBody] UpdatePositionDTO request)
        {
            var data = await _positionManager.Update(id, request);
            return Ok(new BaseResponse<bool>(data, $"Update Position Id: {id}"));
        }

        
        //[Authorize]
        [ProducesResponseType(typeof(Paginate<GetPositionDTO>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid id)
        {
            var data = await _positionManager.Delete(id);
            return Ok(new BaseResponse<bool>(data, $"Delete position: {data}"));
        }
    }
}