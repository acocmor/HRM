using System;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Employee;
using HRM.Models.Error;
using HRM.Models.Paginate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeManager;
        public EmployeeController(IEmployeeService service)
        {
            _employeeManager = service;
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetEmployeeDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Paginate<GetEmployeeDTO>>> GetAll([FromQuery] GetEmployeeFilter request)
        {
            var data = await _employeeManager.GetAll(request);
            return Ok(new BaseResponse<Paginate<GetEmployeeDTO>>(data, $"Employee List"));
        }
        
                
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetEmployeeDTO>), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetById([FromRoute] Guid id)
        {
            var data = await _employeeManager.GetById(id);
            return Ok(new BaseResponse<GetEmployeeDTO>(data, $"Employee Id: {id}"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(GetEmployeeDTO), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<GetEmployeeDTO>> Create([FromBody] CreateEmployeeDTO request)
        {
            var data = await _employeeManager.Create(request);
            return Ok(new BaseResponse<GetEmployeeDTO>(data, $"Create employee"));
        }
        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetEmployeeDTO>), StatusCodes.Status200OK)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] Guid id, [FromBody] UpdateEmployeeDTO request)
        {
            var data = await _employeeManager.Update(id, request);
            return Ok(new BaseResponse<bool>(data, $"Update employee Id: {id}"));
        }

        
        [Authorize]
        [ProducesResponseType(typeof(Paginate<GetEmployeeDTO>), StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] Guid id)
        {
            var data = await _employeeManager.Delete(id);
            return Ok(new BaseResponse<bool>(data, $"Delete employee: {data}"));
        }
    }
}