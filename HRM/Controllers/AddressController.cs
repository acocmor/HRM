using HRM.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRM.Controllers
{
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressManager;

        public AddressController(IAddressService service)
        {
            _addressManager = service;
        }
    }
}