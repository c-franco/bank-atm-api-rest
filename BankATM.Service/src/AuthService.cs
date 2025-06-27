using BankATM.Common.Constants;
using BankATM.Common.DTO;
using BankATM.Common.Exceptions;
using BankATM.Common.Settings;
using BankATM.Service.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankATM.Service
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string?> AuthenticateAsync(LoginRequestDTO request)
        {
            if (request.Username == "admin" && request.Password == "123456")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            } 
            else
            {
                throw new UnauthorizedException(GlobalErrors.InvalidCredentials);
            }
        }
    }
}
