using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using YourBudget.Infrastructure.DTO;
using YourBudget.Infrastructure.Extensions;
using YourBudget.Infrastructure.Settings;

namespace YourBudget.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings settings;
        public JwtHandler(JwtSettings settings)
        {
            this.settings = settings;
        }

        public JwtDto CreateToken(Guid userId, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // Subject: do czego odnośni się Token
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role), // Role
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT_ID, Identyfikator
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString(), ClaimValueTypes.Integer64) // Issued at - kiedy token został stwrzony
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)), 
                SecurityAlgorithms.HmacSha256);

            var expires = now.AddMinutes(settings.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: settings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto { Token = token, Expires = expires.ToTimestamp()};
        }
    }
}