using System;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Department;
using HRM.Models.Error;
using HRM.Models.Paginate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentManager;
        public DepartmentController(IDepartmentService service)
        {
            _departmentManager = service;
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetDepartmentDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetDepartmentDTO>>> GetAll([FromQuery] GetDepartmentsFilter request)
        {
            var data = await _departmentManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetDepartmentDTO>>(data, $"Departmanet List"));
        }
        
                
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetDepartmentDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetDepartmentDTO>> GetById([FromRoute] Guid id)
        {
            var data = await _departmentManager.GetById(id);
            return Ok(new BaseResponse<GetDepartmentDTO>(data, $"Departmanet Id: {id}"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(GetDepartmentDTO), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<GetDepartmentDTO>> Create([FromBody] CreateDepartmentDTO request)
        {
            var data = await _departmentManager.Create(request);
            return Ok(new BaseResponse<GetDepartmentDTO>(data, $"Create departmanet"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetDepartmentDTO>), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] Guid id, [FromBody] UpdateDepartmentDTO request)
        {
            var data = await _departmentManager.Update(id, request);
            return Ok(new BaseResponse<bool>(data, $"Update departmanet Id: {id}"));
        }

        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetDepartmentDTO>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid id)
        {
            var data = await _departmentManager.Delete(id);
            return Ok(new BaseResponse<bool>(data, $"Delete departmanet: {data}"));
        }
    }
}