using System;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Address;
using HRM.Models.Error;
using HRM.Models.Paginate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressManager;

        public AddressController(IAddressService service)
        {
            _addressManager = service;
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetAddressDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetAddressDTO>>> GetAll([FromQuery] GetAddressFilter request)
        {
            var data = await _addressManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetAddressDTO>>(data, $"Departmanet List"));
        }
        
                
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetAddressDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAddressDTO>> GetById([FromRoute] Guid id)
        {
            var data = await _addressManager.GetById(id);
            return Ok(new BaseResponse<GetAddressDTO>(data, $"Departmanet Id: {id}"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(GetAddressDTO), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<GetAddressDTO>> Create([FromBody] CreateAddressDTO request)
        {
            var data = await _addressManager.Create(request);
            return Ok(new BaseResponse<GetAddressDTO>(data, $"Create address"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetAddressDTO>), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] Guid id, [FromBody] UpdateAddressDTO request)
        {
            var data = await _addressManager.Update(id, request);
            return Ok(new BaseResponse<bool>(data, $"Update address Id: {id}"));
        }

        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetAddressDTO>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid id)
        {
            var data = await _addressManager.Delete(id);
            return Ok(new BaseResponse<bool>(data, $"Delete address: {data}"));
        }
    }
}