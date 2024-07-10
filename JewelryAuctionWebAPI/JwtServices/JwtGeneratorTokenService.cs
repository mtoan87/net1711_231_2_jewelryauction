using JewelryAuctionData.JWT;
using JewelryAuctionData.Models;
using JewelryAuctionWebAPI.JwtServices.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JewelryAuctionWebAPI.JwtServices
{
    public class JwtGeneratorTokenService : IJwtGeneratorTokenService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtGeneratorTokenService(IConfiguration configuration)
        {
            _jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();
        }

        public string GenerateToken(Customer cus)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, cus.CustomerId.ToString()),
            new Claim(ClaimTypes.Name, cus.Email),
            new Claim(ClaimTypes.Role, cus.Role)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
