using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Exceptions;
using HRM.Core.Interfaces;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using System.Security.Cryptography;
using HRM.App_Start;
using HRM.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations.Rules;

namespace HRM.Core.AppServices
{
    public class AuthManager : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthManager(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _config = config;
        }
        
        public async Task<TokenModel> Login(LoginDTO request)
        {
            var user = await _userRepository.Login(request.Email, request.Password);
            if (user == null)
                throw new ApiException("Tài khoản hoặc mật khẩu không chính xác")
                    {StatusCode = (int) HttpStatusCode.BadRequest};
            
            return await GenerateToken(user);
        }

        private async Task<TokenModel> GenerateToken(User user)
        {
            var tokenOptions = _config.GetSection("Jwt").Get<JwtConfig>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Employee != null ?user.Employee.FirstName :""),
                new Claim(ClaimTypes.Surname, user.Employee != null ?user.Employee.LastName :""),
                new Claim(ClaimTypes.Role, user.Employee != null ? user.Employee.DepartmentId.ToString() :""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(tokenOptions.Issuer,
                tokenOptions.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials);
            
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Save refresh token
            var refreshTokenEntity = new RefreshToken()
            {
                JwtId = token.Id,
                UserId = user.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpriredAt = DateTime.UtcNow.AddHours(1),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _refreshTokenRepository.Create(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            return new TokenModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        
        public async Task<bool> Logout(TokenModel tokenModel)
        {
            //Check refresh token exist in DB
            var storedToken = await _refreshTokenRepository.GetByToken(tokenModel.RefreshToken);
            if (storedToken == null)
            {
                throw new ApiException("Refresh Token không tồn tại")
                    {StatusCode = (int) HttpStatusCode.BadRequest};
            }

            //Update token is used
            storedToken.IsRevoked = true;
            _refreshTokenRepository.Update(storedToken);
            await _refreshTokenRepository.SaveChangesAsync();
            return true;
        }
        
        public async Task<TokenModel> RenewToken(TokenModel tokenModel)
        {
            var tokenOptions = _config.GetSection("Jwt").Get<JwtConfig>();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(tokenOptions.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                
                //Tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,
                
                //Ký vào token
                ValidateLifetime = false, //Kiểm tra token hết thời hạn
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                //Check AccessToken valid format
                var tokenInVerification =
                    jwtTokenHandler.ValidateToken(tokenModel.AccessToken, tokenValidateParam, out var validatedToken);

                //Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        throw new ApiException("Invalid Token")
                            {StatusCode = (int) HttpStatusCode.BadRequest};
                    }
                }

                //Check AccessToken expire
                var utcExpireDate = long
                    .Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    throw new ApiException("Access Token đã hết hạn")
                        {StatusCode = (int) HttpStatusCode.BadRequest};
                }

                //Check refresh token exist in DB
                var storedToken = await _refreshTokenRepository.GetByToken(tokenModel.RefreshToken);
                if (storedToken == null)
                {
                    throw new ApiException("Refresh Token không tồn tại")
                        {StatusCode = (int) HttpStatusCode.BadRequest};
                }

                //Check refresh token is used/revoked
                if (storedToken.IsUsed)
                {
                    throw new ApiException("Refresh Token đã được sử dụng")
                        {StatusCode = (int) HttpStatusCode.BadRequest};
                }

                if (storedToken.IsRevoked)
                {
                    throw new ApiException("Refresh Token đã bị thu hồi")
                        {StatusCode = (int) HttpStatusCode.BadRequest};
                }

                //Check AccessToken == JwtId RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    throw new ApiException("Access Token không đúng")
                        {StatusCode = (int) HttpStatusCode.BadRequest};
                }

                //Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;
                _refreshTokenRepository.Update(storedToken);
                await _refreshTokenRepository.SaveChangesAsync();

                var tokenUserNew = await _userRepository.GetById(storedToken.UserId);

                //Create new Token  
                return await GenerateToken(tokenUserNew);
            }
            catch (Exception e)
            {
                throw new ApiException("Something went wrong")
                    {StatusCode = (int) HttpStatusCode.BadRequest};
            }
        }
        
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        private DateTime ConvertUnixToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}