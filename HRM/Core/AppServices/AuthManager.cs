using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Exceptions;
using HRM.Core.Interfaces;
using HRM.Entity.Constracts;
using HRM.Models.Address;
using HRM.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HRM.Core.AppServices
{
    public class AuthManager : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthManager(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }
        
        public async Task<string> Login(LoginDTO request)
        {
            var user = await _userRepository.Login(request.Email, request.Password);
            if (user == null)
                throw new ApiException("Tài khoản hoặc mật khẩu không chính xác")
                    {StatusCode = (int) HttpStatusCode.BadRequest};

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Employee != null ?user.Employee.FirstName :""),
                new Claim(ClaimTypes.Surname, user.Employee != null ?user.Employee.LastName :""),
                new Claim(ClaimTypes.Role, user.Employee != null ? user.Employee.DepartmentId.ToString() :""),
                new Claim(ClaimTypes.Actor, user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            string tokenn = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenn;
        }
    }
}