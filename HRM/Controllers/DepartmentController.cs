using HRM.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentManager;
        public DepartmentController(IDepartmentService service)
        {
            _departmentManager = service;
        }
    }
}