using HRM.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderManager;
        public GenderController(IGenderService service)
        {
            _genderManager = service;
        }
    }
}