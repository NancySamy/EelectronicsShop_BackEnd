using LinkDevelopmentWorkshop.Application.Support;
using LinkDevelopmentWorkshop.Domain.Entities;
using LinkDevelopmentWorkshop.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Services
{
    public class JwtService
    {
        private IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public string GenerateToken(User user)
        {
            var tokenVal = string.Empty;
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
           
                var encPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",user.UserId.ToString()),
                        new Claim("Password",encPassword),
                    };

            tokenVal= JwtUtilities.GenerateJwtEncodedToken(jwt.Key,user.UserName,user.Email,((RoleTypes)user.RoleID).ToString(),jwt.Issuer,jwt.Audience,60*20,claims);
            return tokenVal;
        }
    }
}
