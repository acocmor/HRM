using HRM.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionManager;
        public PositionController(IPositionService service)
        {
            _positionManager = service;
        }
    }
}