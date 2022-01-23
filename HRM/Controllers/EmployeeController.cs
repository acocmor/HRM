using HRM.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeManager;
        public EmployeeController(IEmployeeService service)
        {
            _employeeManager = service;
        }
    }
}